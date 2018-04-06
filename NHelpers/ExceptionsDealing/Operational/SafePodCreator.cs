// ReSharper disable ArgumentsStyleNamedExpression

namespace EasySharp.NHelpers.ExceptionsDealing.Operational
{
    using System;

    /// <summary>
    ///     Exception-safe delegate wrappers creator.
    /// </summary>
    /// <example>
    ///     class Example
    ///     {
    ///         public void Run()
    ///         {
    ///             var safePod1 = SafePodCreator.MakeValueTypePod(Calc1);
    ///             var safePod2 = SafePodCreator.MakeValueTypePod(Calc2);
    ///             var safePod3 = SafePodCreator.MakeValueTypePod(Calc3);
    ///             var w = safePod1() ??
    ///                     safePod2() ??
    ///                     safePod3() ??
    ///                     throw new NoCalcsWorkedException("No function was executed successfully");
    ///             Console.Out.WriteLine($"result = {w}"); // w = 2.000001
    ///         }
    ///         private static double Calc1() =&gt; throw new Exception("Intentionally thrown exception");
    ///         private static double Calc2() =&gt; 2.000001;
    ///         private static double Calc3() =&gt; 3.000001;
    ///     }
    /// </example>
    /// <remarks><seealso cref="https://stackoverflow.com/a/49465494/5259296"/></remarks>
    public static class SafePodCreator
    {
        public static Func<TResult?> MakeValueTypePod<TResult>(Func<TResult> jobUnit) where TResult : struct
        {
            Func<TResult?> wrapperFunc = () =>
            {
                try { return jobUnit.Invoke(); } catch { return null; }
            };

            return wrapperFunc;
        }

        public static Func<TResult> MakeReferenceTypePod<TResult>(Func<TResult> jobUnit) where TResult : class
        {
            Func<TResult> wrapperFunc = () =>
            {
                try { return jobUnit.Invoke(); } catch { return null; }
            };

            return wrapperFunc;
        }
    }
}