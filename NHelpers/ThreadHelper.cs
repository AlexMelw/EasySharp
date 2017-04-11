using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySharp.NHelpers
{
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

    public class ThreadWrapper
    {
        public string Name { get; private set; }
        public CountdownEvent PersonalCountdownEvent { get; }
        public List<CountdownEvent> InjectedCountdownEventsList { get; }
        public int[] WaitForThreads { get; }


        private Action _threadAction;
        private Thread _thread;

        public ThreadWrapper(string wrapperName, Action threadAction, int[] waitForThreads)
        {
            Name = wrapperName;
            _threadAction = threadAction;

            if (waitForThreads.Length > 0)
                PersonalCountdownEvent = new CountdownEvent(waitForThreads.Length);

            WaitForThreads = waitForThreads;
            InjectedCountdownEventsList = new List<CountdownEvent>();
        }

        public void InjectCountsownEvent(CountdownEvent countdownEvent)
        {
            InjectedCountdownEventsList.Add(countdownEvent);
        }

        public void Run()
        {
            _thread = new Thread(() =>
            {
                PersonalCountdownEvent?.Wait();

                _threadAction();

                foreach (CountdownEvent countdownEvent in InjectedCountdownEventsList)
                {
                    countdownEvent.Signal();
                }
            });

            _thread.Start();
        }
    }
}