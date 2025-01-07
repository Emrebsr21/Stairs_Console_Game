using System;
using System.Threading;

namespace ClassLibrary1
{

    using static System.Net.Mime.MediaTypeNames;


    public class Stairs
    {
        private Thread stairsThread;
        private bool running;
        private bool paused;
        private readonly object pauseLock = new object();

        public Stairs()
        {
            stairsThread = new Thread(PrintStairs);
            running = false;
            paused = false;
        }
        public void Start()
        {
            if (stairsThread.ThreadState == ThreadState.Unstarted || stairsThread.ThreadState == ThreadState.Stopped)
            {
                stairsThread = new Thread(PrintStairs);
                stairsThread.Start();
                Console.WriteLine("Starting Stairs now:");
            }
            else if (paused)
            {
                Resume();
            }
        }

        public void Pause()
        {
            lock (pauseLock)
            {
                paused = true;
                Console.WriteLine("Stairs have been paused:");
            }
        }

        public void Resume()
        {
            lock (pauseLock)
            {
                paused = false;
                Monitor.Pulse(pauseLock);
                Console.WriteLine("Stairs have been started from beginning:");
            }
        }

        public void Stop()
        {
            running = false;
            if (paused) Resume(); 
            stairsThread.Join();
        }

        private void PrintStairs()
        {
            running = true;
            int step = 0;
            while (running)
            {
                lock (pauseLock)
                {
                    if (paused)
                    {
                        Monitor.Wait(pauseLock);
                    }
                }

                if (!running) break;
                //Console.WriteLine();
                Console.WriteLine(new string(' ', step) + "|_");
                step++;
                Thread.Sleep(500); 
            }
        }

        public void Restart()
        {
            Stop();
            stairsThread = new Thread(PrintStairs);
            stairsThread.Start();
        }
    }
}

