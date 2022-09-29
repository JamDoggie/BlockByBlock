using System.Collections;

namespace net.minecraft.src
{

	public class PlayerUsageSnooper
	{
		private Dictionary<string,object> field_52025_a = new();
		private readonly Uri field_52024_b;

		public PlayerUsageSnooper(string string1)
		{
			field_52024_b = new Uri("http://snoop.minecraft.net/" + string1);
		}

		public virtual void func_52022_a(string string1, object object2)
		{
			field_52025_a[string1] = object2;
		}

		public virtual void func_52021_a()
		{
			PlayerUsageSnooperThread playerUsageSnooperThread1 = new PlayerUsageSnooperThread(this, "reporter");
			playerUsageSnooperThread1.thread.IsBackground = true;
			playerUsageSnooperThread1.Start();
		}

		internal static Uri func_52023_a(PlayerUsageSnooper playerUsageSnooper0)
		{
			return playerUsageSnooper0.field_52024_b;
		}

		internal static Dictionary<string,object> func_52020_b(PlayerUsageSnooper playerUsageSnooper0)
		{
			return playerUsageSnooper0.field_52025_a;
		}
	}

}