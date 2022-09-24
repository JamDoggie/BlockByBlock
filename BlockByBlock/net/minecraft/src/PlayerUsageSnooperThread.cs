using System.Threading;

namespace net.minecraft.src
{
	internal class PlayerUsageSnooperThread : Thread
	{
		internal readonly PlayerUsageSnooper field_52012_a;

		internal PlayerUsageSnooperThread(PlayerUsageSnooper playerUsageSnooper1, string string2) : base(string2)
		{
			this.field_52012_a = playerUsageSnooper1;
		}

		public virtual void run()
		{
			PostHttp.func_52018_a(PlayerUsageSnooper.func_52023_a(this.field_52012_a), PlayerUsageSnooper.func_52020_b(this.field_52012_a), true);
		}
	}

}