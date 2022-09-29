using System;
using System.Threading;

namespace net.minecraft.src
{

	internal class ThreadStatSyncherSend
	{
		internal readonly System.Collections.IDictionary field_27233_a;
		internal readonly StatsSyncher syncher;

		private Thread thread;

		internal ThreadStatSyncherSend(StatsSyncher statsSyncher1, System.Collections.IDictionary map2)
		{
			syncher = statsSyncher1;
			field_27233_a = map2;
			thread = new Thread(() => run());
		}

		public virtual void Start()
        {
			thread.Start();
        }

		public virtual void run()
		{
			try
			{
				StatsSyncher.func_27412_a(this.syncher, this.field_27233_a, StatsSyncher.getUnsentDataFile(this.syncher), StatsSyncher.getUnsentTempFile(this.syncher), StatsSyncher.getUnsentOldFile(this.syncher));
			}
			catch (Exception exception5)
			{
				Console.WriteLine(exception5.ToString());
				Console.Write(exception5.StackTrace);
			}
			finally
			{
				StatsSyncher.setBusy(this.syncher, false);
			}

		}
	}

}