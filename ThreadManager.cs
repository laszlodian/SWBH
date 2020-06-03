using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreadTaskManager;

namespace SWB_OptionPackageInstaller
{
    public class ThreadManager
    {
        public static ThreadManager Instance;
        private List<Thread> threadList;
        private Thread _thread;

        public ThreadManager()
        {
            Instance = this;

            CreateThreadList();
        }

        public void CreateThreadList()
        {
            if (threadList != null)
            {
                Trace.TraceInformation("Thread list already exists! clean up and make it's value to null first!");
            }
            else
                threadList = new List<Thread>();
        }

        public void AddThreadToCollection(Thread thread_in)
        {
            threadList.Add(thread_in);
        }

        public void InitThreadPool(Thread thread)
        {
            _thread = thread;
            ThreadPool.QueueUserWorkItem(_ => StartThread(_thread));
        }

        private void StartThread(Thread thread_in)
        {
            thread_in.Start();
        }

        public void WaitAllThreadToFinishWork()
        {
            int numOfThreads = threadList.Count;
            WaitHandle[] waitHandles = new WaitHandle[numOfThreads];

            for (int i = 0; i < numOfThreads; i++)
            {
                var j = i;
                // Or you can use AutoResetEvent/ManualResetEvent
                EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                var thread = new Thread(() =>
                {
                    Thread.Sleep(j * 1000);
                    Trace.TraceInformation(String.Format("Thread {0} exits", threadList.ToArray()[j].Name));
                    handle.Set();
                });
                waitHandles[j] = handle;
                thread.Start();
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void StartThreadAndWaitForFinish(int sleepInterval, CountdownEvent countDown, Thread thread_in)
        {
            try
            {
                new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
                Thread.Sleep(sleepInterval);
            }
            finally
            {
                new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
                // need to signal in a finally block, otherwise an exception may occur and prevent
                // this from being signaled
                countDown.Signal();
            }
        }

        public CountdownEvent StartTasks(Thread thread_in)
        {
            int count = threadList.Count;
            new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
            CountdownEvent countDown = new CountdownEvent(1);

            thread_in.Start();
            new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
            ThreadPool.QueueUserWorkItem(_ => StartThreadAndWaitForFinish(20000, countDown, thread_in));
            new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
            return countDown;
        }

        public void StartAndWaitOneThread(Thread thread_in)
        {
            AddThreadToCollection(thread_in);
            new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
            Trace.TraceInformation("Starting. . .");

            var stopWatch = Stopwatch.StartNew();
            using (CountdownEvent countdownEvent = StartTasks(thread_in))
            {
                new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
                countdownEvent.Wait();
                // waits until the countdownEvent is signalled 100 times
            }
            new Thread(new ThreadStart(() => Trace.TraceInformation("Thread {0} state is {1}", thread_in.Name, thread_in.ThreadState))).Start();
            RemoveFromThreadCollection(thread_in);
            stopWatch.Stop();
            Trace.TraceInformation("Done! Elapsed time: {0} milliseconds", stopWatch.Elapsed.TotalMilliseconds);
        }

        private void RemoveFromThreadCollection(Thread thread)
        {
            threadList.Remove(thread);
        }

        public void ManageThread(Thread thread)
        {
            using (CountdownEvent countdownEvent = WaitThreadToFinishWork(thread))
            {
                countdownEvent.Wait();
            }
        }

        private CountdownEvent WaitThreadToFinishWork(Thread thread)
        {
            CountdownEvent countdown = new CountdownEvent(1);

            ThreadPool.QueueUserWorkItem(_ => StartThreadAndWaitForFinish(20000, countdown, thread));

            return countdown;
        }

        public AutoResetEvent _blockThread = new AutoResetEvent(false);

        private bool _stopThreads = false;

        public void GetAllThreadStartedByApplication()
        {
            Process process = System.Diagnostics.Process.GetCurrentProcess();
            ProcessThreadCollection threadlist = process.Threads;

            foreach (ProcessThread theThread in threadlist)
            {
                Trace.TraceInformation("Thread ID:{0} Priority: {1} Started: {2}", theThread.Id, theThread.PriorityLevel, theThread.StartTime);
            }
        }

        public bool StopThreads
        {
            get { return _stopThreads; }
            set
            {
                _stopThreads = value;
            }
        }
    }
}