using System;
using System.Threading;

namespace net.minecraft.src
{
	internal class ThreadStatSyncherReceive : Thread
	{
		internal readonly StatsSyncher syncher;

		internal ThreadStatSyncherReceive(StatsSyncher statsSyncher1)
		{
			this.syncher = statsSyncher1;
		}

		public virtual void run()
		{
			try
			{
				if (StatsSyncher.func_27422_a(this.syncher) != null)
				{
					StatsSyncher.func_27412_a(this.syncher, StatsSyncher.func_27422_a(this.syncher), StatsSyncher.func_27423_b(this.syncher), StatsSyncher.func_27411_c(this.syncher), StatsSyncher.func_27413_d(this.syncher));
				}
				else if (StatsSyncher.func_27423_b(this.syncher).exists())
				{
					StatsSyncher.func_27421_a(this.syncher, StatsSyncher.func_27409_a(this.syncher, StatsSyncher.func_27423_b(this.syncher), StatsSyncher.func_27411_c(this.syncher), StatsSyncher.func_27413_d(this.syncher)));
				}
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