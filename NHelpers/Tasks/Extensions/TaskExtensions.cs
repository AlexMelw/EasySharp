namespace EasySharp.NHelpers.Tasks.Extensions
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        /// <summary>
        ///     It does the same thing as <c>ConfigureAwait(false)</c>, but just differentiates it in a more visual manner.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         I prefer to always state explicitly how I want the task continuation to occur. So even though a
        ///         <see cref="Task" />'s default is <c>ConfigureAwait(true)</c>, I still specify it as such so that I am always
        ///         cognizant of what's happening
        ///         "under the hood".
        ///     </para>
        ///     <para>
        ///         However, when you look at a lot of code, some with <c>ConfigureAwait(true)</c> and some with
        ///         <c>ConfigureAwait(false)</c>, it's not easy to spot where they differ. So I use either
        ///         <c>ConfigureAwait(false)</c>, or a useful extension method,
        ///         <c>ContinueOnCapturedContext()</c>. It does the same thing, but just differentiates it from
        ///         <c>ConfigureAwait(false)</c> in a more visual manner.
        ///     </para>
        ///     <para>Some good points to remember:</para>
        ///     <list type="number">
        ///         <item>
        ///             The synchronization context's job is simply to coordinate communication with a resource (UI thread,
        ///             request context, etc).
        ///         </item>
        ///         <item>
        ///             When execution continues after an await, generated code ensures that the code runs on the same
        ///             synchronization context that it did before(by default).
        ///         </item>
        ///         <item>
        ///             Use <c>ConfigureAwait(false)</c> to tell async code that it does not have to continue on the same context.
        ///             It's important to use this in library code to prevent deadlocks.
        ///         </item>
        ///     </list>
        /// </remarks>
        /// <typeparam name="TResult">The type of the result produced by this <see cref="Task{TResult}" />.</typeparam>
        /// <param name="task"><see cref="Task{TResult}" /> to be awaited</param>
        /// <returns>
        ///     <see cref="ConfiguredTaskAwaitable{TResult}" /> that provides an awaitable object that enables configured
        ///     awaits on a task.
        /// </returns>
        public static ConfiguredTaskAwaitable<TResult> ContinueOnCapturedContext<TResult>(this Task<TResult> task)
        {
            return task.ConfigureAwait(true);
        }


        /// <summary>
        ///     It does the same thing as <c>ConfigureAwait(false)</c>, but just differentiates it in a more visual manner.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         I prefer to always state explicitly how I want the task continuation to occur. So even though a
        ///         <see cref="Task" />'s default is <c>ConfigureAwait(true)</c>, I still specify it as such so that I am always
        ///         cognizant of what's happening
        ///         "under the hood".
        ///     </para>
        ///     <para>
        ///         However, when you look at a lot of code, some with <c>ConfigureAwait(true)</c> and some with
        ///         <c>ConfigureAwait(false)</c>, it's not easy to spot where they differ. So I use either
        ///         <c>ConfigureAwait(false)</c>, or a useful extension method,
        ///         <c>ContinueOnCapturedContext()</c>. It does the same thing, but just differentiates it from
        ///         <c>ConfigureAwait(false)</c> in a more visual manner.
        ///     </para>
        ///     <para>Some good points to remember:</para>
        ///     <list type="number">
        ///         <item>
        ///             The synchronization context's job is simply to coordinate communication with a resource (UI thread,
        ///             request context, etc).
        ///         </item>
        ///         <item>
        ///             When execution continues after an await, generated code ensures that the code runs on the same
        ///             synchronization context that it did before(by default).
        ///         </item>
        ///         <item>
        ///             Use <c>ConfigureAwait(false)</c> to tell async code that it does not have to continue on the same context.
        ///             It's important to use this in library code to prevent deadlocks.
        ///         </item>
        ///     </list>
        /// </remarks>
        /// <param name="task"><see cref="Task" /> to be awaited</param>
        /// <returns>
        ///     <see cref="ConfiguredTaskAwaitable" /> that provides an awaitable object that enables configured
        ///     awaits on a task.
        /// </returns>
        public static ConfiguredTaskAwaitable ContinueOnCapturedContext(this Task task)
        {
            return task.ConfigureAwait(true);
        }
    }
}