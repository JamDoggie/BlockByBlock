using System;
using System.Collections;
using System.Threading;

namespace net.minecraft.src
{
    
	public class ThreadedFileIOBase
	{
		public static readonly ThreadedFileIOBase threadedIOInstance = new ThreadedFileIOBase();
		private ArrayList threadedIOQueue = ArrayList.Synchronized(new ArrayList());

		private long writeCounterBacking = 0L;
		private object writeCounterLock = new();
		private long writeQueuedCounter
        {
			get
            {
				lock (writeCounterLock)
					return writeCounterBacking;
            }

			set
            {
				lock (writeCounterLock)
					writeCounterBacking = value;
            }
        }

		private long savedIOCounterBacking = 0L;
		private object savedIOLock = new();
		private long savedIOCounter
		{
			get
			{
				lock (savedIOLock)
					return savedIOCounterBacking;

			}

			set
			{
				lock (savedIOLock)
					savedIOCounterBacking = value;
			}
		}

		private volatile bool isThreadWaiting = false;

		private ThreadedFileIOBase()
		{
			Thread thread1 = new Thread(() => run());
			thread1.Name = "File IO Thread";
			thread1.Priority = ThreadPriority.Lowest;
			thread1.Start();
		}

		public virtual void run()
		{
			while (true)
			{
				processQueue();
			}
		}

		private void processQueue()
		{
			for (int i1 = 0; i1 < threadedIOQueue.Count; ++i1)
			{
				IThreadedFileIO iThreadedFileIO2 = (IThreadedFileIO)threadedIOQueue[i1];
				bool z3 = iThreadedFileIO2.writeNextIO();
				if (!z3)
				{
					threadedIOQueue.RemoveAt(i1--);
					++savedIOCounter;
				}

				try
				{
					if (!isThreadWaiting)
					{
						Thread.Sleep(10);
					}
					else
					{
						Thread.Sleep(0);
					}
				}
				catch (ThreadInterruptedException interruptedException6)
				{
					Console.WriteLine(interruptedException6.ToString());
					Console.Write(interruptedException6.StackTrace);
				}
			}

			if (threadedIOQueue.Count == 0)
			{
				try
				{
					Thread.Sleep(25);
				}
				catch (ThreadInterruptedException interruptedException5)
				{
					Console.WriteLine(interruptedException5.ToString());
					Console.Write(interruptedException5.StackTrace);
				}
			}

		}

		public virtual void queueIO(IThreadedFileIO iThreadedFileIO1)
		{
			if (!threadedIOQueue.Contains(iThreadedFileIO1))
			{
				++writeQueuedCounter;
				threadedIOQueue.Add(iThreadedFileIO1);
			}
		}
        
		public virtual void waitForFinish()
		{
			isThreadWaiting = true;

			while (writeQueuedCounter != savedIOCounter)
			{
				Thread.Sleep(10);
			}

			isThreadWaiting = false;
		}
	}

}