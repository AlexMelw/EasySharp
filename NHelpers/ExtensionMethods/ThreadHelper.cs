namespace EasySharp.NHelpers.ExtensionMethods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using Wrappers;

    public static class ThreadHelper
    {
        /// <summary>
        ///     The given <see cref="Thread" /> Extension Method creates and returns an instance of the class that
        ///     implements both <see cref="IThreadExecutionBuiltOrder" /> and <see cref="IThreadExecutionStageInitializer" />
        /// </summary>
        /// <param name="thread">The current thread</param>
        /// <returns>
        ///     returns an instance of the class that implements both <see cref="IThreadExecutionBuiltOrder" /> and
        ///     <see cref="IThreadExecutionStageInitializer" />
        /// </returns>
        public static IThreadExecutionStageInitializer InitThreadExecutionOrder(this Thread thread)
        {
            return new ThreadExecutionStageBuilder();
        }

        /// <summary>
        ///     <para>Allows to stage threads, build and run Threads Execution Sequence.</para>
        ///     <para>
        ///         The class implements <see cref="IThreadExecutionBuiltOrder" /> and
        ///         <see cref="IThreadExecutionStageInitializer" />
        ///     </para>
        /// </summary>
        /// <example>
        /// 
        ///            var builtThreadExecutionOrder = Thread.CurrentThread
        ///                .InitThreadExecutionOrder()
        ///                .StageThread(2, () =&gt; Console.WriteLine($"Thread_2 executed"), 0, 1)
        ///                .StageThread(0, () =&gt; Console.WriteLine($"Thread_0 executed"))
        ///                .StageThread(1, () =&gt; Console.WriteLine($"Thread_1 executed"), 0)
        ///                .StageThread(3, () =&gt; Console.WriteLine($"Thread_3 executed"), 0)
        ///                .Build();
        /// 
        ///            builtThreadExecutionOrder.RunStagedThreadsChain(); // Deferred run
        /// </example>
        /// <example>
        ///             Thread.CurrentThread
        ///                .InitThreadExecutionOrder()
        ///                .StageThread(2, () =&gt; Console.WriteLine($"Thread_2 executed"), 0, 1)
        ///                .StageThread(0, () =&gt; Console.WriteLine($"Thread_0 executed"))
        ///                .StageThread(1, () =&gt; Console.WriteLine($"Thread_1 executed"), 0)
        ///                .StageThread(3, () =&gt; Console.WriteLine($"Thread_3 executed"), 0)
        ///                .Build()
        ///                .RunStagedThreadsChain(); // Instant run
        /// </example>
        private class ThreadExecutionStageBuilder :
            IThreadExecutionBuiltOrder, IThreadExecutionStageInitializer
        {
            private List<ThreadWrapper> _threadWrappersChain;
            private bool _isBuilt;

            #region CONSTRUCTORS

            public ThreadExecutionStageBuilder()
            {
                _threadWrappersChain = new List<ThreadWrapper>();
            }

            #endregion

            /// <summary>
            ///     <para>Builds the Threads Execution Sequence.</para>
            ///     <para>The method is defined by <see cref="IThreadAutomaticExecutionOrderBuilder" /> interface.</para>
            /// </summary>
            /// <remarks>All properties <see cref="ThreadWrapper.ThreadWrapperIdentifier" /> are negative</remarks>
            /// <returns></returns>
            IThreadExecutionBuiltOrder IThreadAutomaticExecutionOrderBuilder.Build()
            {
                if (_isBuilt) return this;

                if (_threadWrappersChain.Any(wrapper => wrapper.ThreadWrapperIdentifier >= 0))
                {
                    throw new Exception(
                        $@"There are manually set properties ""{nameof(ThreadWrapper.ThreadWrapperIdentifier)}""");
                }

                foreach (var threadWrapper in _threadWrappersChain)
                {
                    foreach (int awaitedThreadIndex in threadWrapper.WaitForThreads)
                    {
                        _threadWrappersChain[awaitedThreadIndex]
                            .InjectCountsownEvent(threadWrapper.PersonalCountdownEvent);
                    }
                }
                _isBuilt = true;

                return this;
            }

            /// <summary>
            ///     <para>Builds the Threads Execution Sequence.</para>
            ///     <para>The method is defined by <see cref="IThreadManualExecutionOrderBuilder" /> interface.</para>
            /// </summary>
            /// <remarks>All properties <see cref="ThreadWrapper.ThreadWrapperIdentifier" /> are nonnegative</remarks>
            /// <returns></returns>
            IThreadExecutionBuiltOrder IThreadManualExecutionOrderBuilder.Build()
            {
                if (_isBuilt) return this;

                if (_threadWrappersChain.Any(wrapper => wrapper.ThreadWrapperIdentifier < 0))
                {
                    throw new Exception(
                        $@"There are not set several properties ""{nameof(ThreadWrapper.ThreadWrapperIdentifier)}""");
                }

                _threadWrappersChain = _threadWrappersChain
                    .OrderBy(wrapper => wrapper.ThreadWrapperIdentifier)
                    .ToList();

                foreach (var threadWrapper in _threadWrappersChain)
                {
                    foreach (int awaitedThreadIndex in threadWrapper.WaitForThreads)
                    {
                        _threadWrappersChain[awaitedThreadIndex]
                            .InjectCountsownEvent(threadWrapper.PersonalCountdownEvent);
                    }
                }
                _isBuilt = true;

                return this;
            }


            private void StageThread(int manuallySetIdentifier, Expression<Action> thread, params int[] waitForThreads)
            {
                if (thread != null)
                {
                    _threadWrappersChain.Add(
                        new ThreadWrapper(
                            threadWrapperIdentifier: manuallySetIdentifier,
                            dependenciesNumber: _threadWrappersChain.Count,
                            threadActionExpression: thread,
                            waitForThreads: waitForThreads ?? new int[0]));
                }
            }

            /// <summary>
            ///     <para>
            ///         Stages the <see cref="Expression{Action}" /> as a <see cref="ThreadWrapper" /> within the Thread Execution
            ///         Order.
            ///     </para>
            ///     <para>The method is defined by <see cref="IThreadManualExecutionStageBuilder" /> interface.</para>
            /// </summary>
            /// <param name="manuallySetIdentifier"></param>
            /// <param name="thread"></param>
            /// <param name="waitForThreads"></param>
            /// <returns></returns>
            IThreadManualExecutionStageBuilder IThreadManualExecutionStageBuilder.StageThread(
                int manuallySetIdentifier,
                Expression<Action> thread,
                params int[] waitForThreads)
            {
                StageThread(
                    manuallySetIdentifier: manuallySetIdentifier,
                    thread: thread,
                    waitForThreads: waitForThreads);

                return this;
            }

            /// <summary>
            ///     <para>
            ///         Stages the <see cref="Expression{Action}" /> as a <see cref="ThreadWrapper" /> within the Thread Execution
            ///         Order.
            ///     </para>
            ///     <para>The method is defined by <see cref="IThreadAutomaticExecutionStageBuilder" /> interface.</para>
            /// </summary>
            /// <param name="thread"></param>
            /// <param name="waitForThreads"></param>
            /// <returns></returns>
            [Obsolete("Not recommended. Set manually the thread wrapper identifier instead.")]
            IThreadAutomaticExecutionStageBuilder IThreadAutomaticExecutionStageBuilder.StageThread(
                Expression<Action> thread,
                params int[] waitForThreads)
            {
                StageThread(
                    manuallySetIdentifier: -1, // -1 stands for: NOT SET
                    thread: thread,
                    waitForThreads: waitForThreads);

                return this;
            }

            /// <summary>
            /// <para>Runs the Threads Execution Sequence if it was built successfully, otherwise throws an exception.</para>
            /// </summary>
            void IThreadExecutionBuiltOrder.RunStagedThreadsChain()
            {
                RunStagedThreadsChain();
            }

            private void RunStagedThreadsChain()
            {
                if (!_isBuilt)
                {
                    throw new Exception("Threads Execution Sequence is not built.");
                }

                foreach (var threadWrapper in _threadWrappersChain)
                {
                    threadWrapper.Run();
                }
            }
        }
    }

    /// <summary>
    ///     The <c>InitThreadExecutionOrder</c> <see cref="Thread" /> extension method returns an instance of the class that
    ///     implements <see cref="IThreadExecutionStageInitializer" />
    /// </summary>
    /// <remarks>Intentional ambiguity: for restricting the use of either Manually or Automatic Thread Staging.</remarks>
    public interface IThreadExecutionStageInitializer :
        IThreadManualExecutionStageBuilder,
        IThreadAutomaticExecutionStageBuilder { }

    /// <summary>
    ///     The <c>Build()</c> method returns an instance of the class that implements
    ///     <see cref="IThreadExecutionBuiltOrder" />
    /// </summary>
    public interface IThreadExecutionBuiltOrder
    {
        void RunStagedThreadsChain();
    }

    /// <summary>
    ///     <see cref="IThreadAutomaticExecutionOrderBuilder" /> is implemented by ThreadExecutionStageBuilder for restricting
    ///     the use of only Automatic Thread Staging.
    /// </summary>
    public interface IThreadAutomaticExecutionStageBuilder : IThreadAutomaticExecutionOrderBuilder
    {
        IThreadAutomaticExecutionStageBuilder StageThread(Expression<Action> thread, params int[] waitForThreads);
    }

    /// <summary>
    ///     <see cref="IThreadManualExecutionOrderBuilder" /> is implemented by ThreadExecutionStageBuilder for restricting
    ///     the use of only Manual Thread Staging.
    /// </summary>
    public interface IThreadManualExecutionStageBuilder : IThreadManualExecutionOrderBuilder
    {
        IThreadManualExecutionStageBuilder StageThread(int manuallySetIdentifier, Expression<Action> thread,
            params int[] waitForThreads);
    }

    /// <summary>
    ///     The <c>StageThread()</c> method returns an instance of the class that implements
    ///     <see cref="IThreadAutomaticExecutionOrderBuilder" />
    /// </summary>
    public interface IThreadAutomaticExecutionOrderBuilder
    {
        IThreadExecutionBuiltOrder Build();
    }

    /// <summary>
    ///     The <c>StageThread()</c> method returns an instance of the class that implements
    ///     <see cref="IThreadManualExecutionOrderBuilder" />
    /// </summary>
    public interface IThreadManualExecutionOrderBuilder
    {
        IThreadExecutionBuiltOrder Build();
    }

    // ------------------------------------------- Diamond Interface Inheritance ---------------------------------------------

    //public interface IThreadExecutionStageBuilder
    //{
    //    IThreadExecutionBuiltOrder Build();
    //}

    //public interface IThreadManualExecutionStageBuilder : IThreadExecutionStageBuilder
    //{
    //    IThreadManualExecutionStageBuilder StageThread(int manuallySetIdentifier, Expression<Action> thread,
    //        params int[] waitForThreads);
    //}

    //public interface IThreadAutomaticExecutionStageBuilder : IThreadExecutionStageBuilder
    //{
    //    IThreadAutomaticExecutionStageBuilder StageThread(Expression<Action> thread, params int[] waitForThreads);
    //}
}