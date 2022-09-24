namespace net.minecraft.src
{
	public class StatCollector
	{
		private static StringTranslate localizedName = StringTranslate.Instance;

		public static string translateToLocal(string string0)
		{
			return localizedName.translateKey(string0);
		}

		public static string translateToLocalFormatted(string string0, params object[] object1)
		{
			return localizedName.translateKeyFormat(string0, object1);
		}
	}

}