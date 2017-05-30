namespace EasySharp.NHelpers.ExtensionMethods
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Wrappers;

    // NO DOCUMENTATION
    // DO NOT USE YET
    // TODO To be reviewed
    public static class ThreadHelper
    {
        public interface IThreadExecutionStageBuilder
        {
            IThreadExecutionStageBuilder StageThread(Action thread, params int[] waitForThreads);
            IThreadExecutionStageBuilder BuildCountdownEvents();
            void RunStagedThreadsChain();
        }

        public static IThreadExecutionStageBuilder InitThreadExecutionOrder(this Thread thread)
        {
            return new ThreadExecutionStageBuilder();
        }

        private class ThreadExecutionStageBuilder : IThreadExecutionStageBuilder
        {
            private readonly List<ThreadWrapper> _threadWrappersChain;
            private bool _isBuilt = false;

            public ThreadExecutionStageBuilder()
            {
                _threadWrappersChain = new List<ThreadWrapper>();
            }

            public IThreadExecutionStageBuilder StageThread(Action thread, params int[] waitForThreads)
            {
                if (thread != null)
                {
                    _threadWrappersChain.Add(
                        new ThreadWrapper(
                            $"Wrapper {_threadWrappersChain.Count}",
                            thread,
                            waitForThreads ?? new int[0]));
                }
                return this;
            }

            public IThreadExecutionStageBuilder BuildCountdownEvents()
            {
                if (_isBuilt) return this;

                for (int i = 0; i < _threadWrappersChain.Count; i++)
                {
                    foreach (int waitForThreadIndex in _threadWrappersChain[i].WaitForThreads)
                    {
                        _threadWrappersChain[waitForThreadIndex]
                            .InjectCountsownEvent(
                                _threadWrappersChain[i].PersonalCountdownEvent);
                    }
                }
                _isBuilt = true;

                return this;
            }

            public void RunStagedThreadsChain()
            {
                if (!_isBuilt) BuildCountdownEvents();

                foreach (ThreadWrapper threadWrapper in _threadWrappersChain)
                {
                    threadWrapper.Run();
                }
            }
        }
    }
}