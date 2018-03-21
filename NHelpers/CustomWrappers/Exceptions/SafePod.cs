// ReSharper disable ArgumentsStyleNamedExpression

namespace EasySharp.NHelpers.CustomWrappers.Exceptions
{
    using System;

    /// <summary>
    ///     Helper class that exposes 2 methods for creation of exception-safe delegate wrappers.
    /// </summary>
    /// <example>
    ///     class Example
    ///     {
    ///         public void Run()
    ///         {
    ///             var safePod1 = SafePod.CreateForValueTypeResult(Calc1);
    ///             var safePod2 = SafePod.CreateForValueTypeResult(Calc2);
    ///             var safePod3 = SafePod.CreateForValueTypeResult(Calc3);
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
    public static class SafePod
    {
        public static Func<TResult?> CreateForValueTypeResult<TResult>(Func<TResult> jobUnit) where TResult : struct
        {
            Func<TResult?> wrapperFunc = () =>
            {
                try { return jobUnit.Invoke(); } catch { return null; }
            };

            return wrapperFunc;
        }

        public static Func<TResult> CreateForReferenceTypeResult<TResult>(Func<TResult> jobUnit) where TResult : class
        {
            Func<TResult> wrapperFunc = () =>
            {
                try { return jobUnit.Invoke(); } catch { return null; }
            };

            return wrapperFunc;
        }
    }
}