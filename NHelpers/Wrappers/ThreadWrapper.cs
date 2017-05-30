namespace EasySharp.NHelpers.Wrappers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

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