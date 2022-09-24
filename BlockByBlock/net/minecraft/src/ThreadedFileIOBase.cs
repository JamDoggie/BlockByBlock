using System;
using System.Collections;
using System.Threading;

namespace net.minecraft.src
{

	public class ThreadedFileIOBase : ThreadStart
	{
		public static readonly ThreadedFileIOBase threadedIOInstance = new ThreadedFileIOBase();
		private System.Collections.IList threadedIOQueue = Collections.synchronizedList(new ArrayList());
		private volatile long writeQueuedCounter = 0L;
		private volatile long savedIOCounter = 0L;
		private volatile bool isThreadWaiting = false;

		private ThreadedFileIOBase()
		{
			Thread thread1 = new Thread(this, "File IO Thread");
			thread1.setPriority(1);
			thread1.Start();
		}

		public virtual void run()
		{
			while (true)
			{
				this.processQueue();
			}
		}

		private void processQueue()
		{
			for (int i1 = 0; i1 < this.threadedIOQueue.Count; ++i1)
			{
				IThreadedFileIO iThreadedFileIO2 = (IThreadedFileIO)this.threadedIOQueue[i1];
				bool z3 = iThreadedFileIO2.writeNextIO();
				if (!z3)
				{
					this.threadedIOQueue.RemoveAt(i1--);
					++this.savedIOCounter;
				}

				try
				{
					if (!this.isThreadWaiting)
					{
						Thread.Sleep(10L);
					}
					else
					{
						Thread.Sleep(0L);
					}
				}
				catch (InterruptedException interruptedException6)
				{
					Console.WriteLine(interruptedException6.ToString());
					Console.Write(interruptedException6.StackTrace);
				}
			}

			if (this.threadedIOQueue.Count == 0)
			{
				try
				{
					Thread.Sleep(25L);
				}
				catch (InterruptedException interruptedException5)
				{
					Console.WriteLine(interruptedException5.ToString());
					Console.Write(interruptedException5.StackTrace);
				}
			}

		}

		public virtual void queueIO(IThreadedFileIO iThreadedFileIO1)
		{
			if (!this.threadedIOQueue.Contains(iThreadedFileIO1))
			{
				++this.writeQueuedCounter;
				this.threadedIOQueue.Add(iThreadedFileIO1);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void waitForFinish() throws InterruptedException
		public virtual void waitForFinish()
		{
			this.isThreadWaiting = true;

			while (this.writeQueuedCounter != this.savedIOCounter)
			{
				Thread.Sleep(10L);
			}

			this.isThreadWaiting = false;
		}
	}

}