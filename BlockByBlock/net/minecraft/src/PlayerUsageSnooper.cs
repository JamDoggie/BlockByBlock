using System.Collections;

namespace net.minecraft.src
{

	public class PlayerUsageSnooper
	{
		private System.Collections.IDictionary field_52025_a = new Hashtable();
		private readonly URL field_52024_b;

		public PlayerUsageSnooper(string string1)
		{
			try
			{
				this.field_52024_b = new URL("http://snoop.minecraft.net/" + string1);
			}
			catch (MalformedURLException)
			{
				throw new System.ArgumentException();
			}
		}

		public virtual void func_52022_a(string string1, object object2)
		{
			this.field_52025_a[string1] = object2;
		}

		public virtual void func_52021_a()
		{
			PlayerUsageSnooperThread playerUsageSnooperThread1 = new PlayerUsageSnooperThread(this, "reporter");
			playerUsageSnooperThread1.setDaemon(true);
			playerUsageSnooperThread1.Start();
		}

		internal static URL func_52023_a(PlayerUsageSnooper playerUsageSnooper0)
		{
			return playerUsageSnooper0.field_52024_b;
		}

		internal static System.Collections.IDictionary func_52020_b(PlayerUsageSnooper playerUsageSnooper0)
		{
			return playerUsageSnooper0.field_52025_a;
		}
	}

}