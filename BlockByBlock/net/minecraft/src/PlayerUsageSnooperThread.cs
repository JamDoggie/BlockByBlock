using System.Threading;

namespace net.minecraft.src
{
	internal class PlayerUsageSnooperThread
	{
		internal readonly PlayerUsageSnooper field_52012_a;

		public Thread thread;

		internal PlayerUsageSnooperThread(PlayerUsageSnooper playerUsageSnooper1, string string2) 
		{
			this.field_52012_a = playerUsageSnooper1;
			thread = new Thread(() => run());
		}

		public virtual void Start()
        {
			thread.Start();
        }

		public virtual void run()
		{
			PostHttp.POSTResult(PlayerUsageSnooper.func_52023_a(this.field_52012_a), PlayerUsageSnooper.func_52020_b(this.field_52012_a), true);
		}
	}

}