namespace EasySharp.NHelpers.ExceptionsDealing.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Containers;

    /// <summary />
    /// <remarks>
    ///     See <see cref="https://www.pluralsight.com/tech-blog/maybe" /> for more information about
    ///     <see cref="Maybe{T}" /> pattern.
    ///     <seealso cref="https://github.com/pluralsight/maybe-dotnet" />
    /// </remarks>
    public static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value) where T : class
        {
            return value != null
                ? Maybe.Some(value)
                : Maybe<T>.None;
        }

        public static Maybe<T> ToMaybe<T>(this T? nullable) where T : struct
        {
            return nullable.HasValue
                ? Maybe.Some(nullable.Value)
                : Maybe<T>.None;
        }

        public static Maybe<string> NoneIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s)
                ? Maybe<string>.None
                : Maybe.Some(s);
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T> self) where T : class
        {
            return self.FirstOrDefault().ToMaybe();
        }

        public static Maybe<T> FirstOrNone<T>(this IEnumerable<T?> self) where T : struct
        {
            return self.FirstOrDefault().ToMaybe();
        }


        /// <summary>
        ///     If <paramref name="monad"/> has a value, the method return the monad immediately. Else, it tries to compute the value using
        ///     <paramref name="monadGeneratorFunc" />, and if it fails the <see cref="Maybe{T}.None" /> is returned, otherwise is
        ///     returned a new monad using <see cref="Maybe{T}.Some" /> constructor.
        /// </summary>
        /// <remarks><seealso cref="https://stackoverflow.com/a/7801857/5259296"/></remarks>
        /// <example>
        ///     [Test]
        ///     public void Value_ReturnsTheValueOfTheFirstSuccessfulTryGet()
        ///     {
        ///         Assert.That(
        ///             Maybe&lt;double&gt;.None
        ///                 .TryGet(() =&gt; { throw new Exception(); })
        ///                 .TryGet(() =&gt; 0)
        ///                 .TryGet(() =&gt; 1)
        ///                 .ThrowIfNone(() =&gt; new NoCalcsWorkedException())
        ///                 .Value,
        ///             Is.EqualTo(0));
        ///     }
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="monad"></param>
        /// <param name="monadGeneratorFunc"></param>
        /// <returns>An instance of <see cref="Maybe{T}"/> monad.</returns>
        public static Maybe<T> TryGet<T>(this Maybe<T> monad, Func<T> monadGeneratorFunc)
        {
            // If monad has a value, just return monad - we want to return the value
            // of the *first* successful TryGet.
            if (monad.HasValue)
            {
                return monad;
            }

            try
            {
                T value = monadGeneratorFunc();

                // We were able to successfully get a value. Wrap it in a Maybe
                // so that we can continue to chain.
                return Maybe<T>.Some(value);
            }
            catch
            {
                // We were unable to get a value. There's nothing else we can do.
                // Hopefully, another TryGet or ThrowIfNone will handle the None.
                return Maybe<T>.None;
            }
        }


        /// <summary>
        ///     Throws the exception specified within the <paramref name="throwFunc" />, if the passed <paramref name="monad" />
        ///     has no value. Otherwise it returns the original <paramref name="monad" />.
        /// </summary>
        /// <remarks><seealso cref="https://stackoverflow.com/a/7801857/5259296"/></remarks>
        /// <example>
        ///     [Test]
        ///     public void ThrowIfNone_ThrowsTheSpecifiedException_GivenNoSuccessfulTryGet()
        ///     {
        ///          Assert.That(() =&gt;
        ///             Maybe&lt;double&gt;.None
        ///                 .TryGet(() =&gt; { throw new Exception(); })
        ///                 .TryGet(() =&gt; { throw new Exception(); })
        ///                 .TryGet(() =&gt; { throw new Exception(); })
        ///                 .ThrowIfNone(() =&gt; new NoCalcsWorkedException())
        ///                 .Value,
        ///             Throws.TypeOf&lt;NoCalcsWorkedException&gt;());
        ///     }
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="monad"></param>
        /// <param name="throwFunc"></param>
        /// <returns>The original <paramref name="monad" /> if no exception was thrown.</returns>
        public static Maybe<T> ThrowIfNone<T>(this Maybe<T> monad, Func<Exception> throwFunc)
        {
            if (!monad.HasValue)
            {
                // If monad does not have a value by now, give up and throw.
                throw throwFunc();
            }

            // Otherwise, pass it on - someone else should unwrap the Maybe and
            // use its value.
            return monad;
        }
    }
}