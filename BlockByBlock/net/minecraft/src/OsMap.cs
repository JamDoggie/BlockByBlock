namespace net.minecraft.src
{
	internal class OsMap
	{
		internal static readonly int[] osValues = new int[((EnumOS1[])Enum.GetValues(typeof(EnumOS1))).Length];

		static OsMap()
		{
			osValues[(int)EnumOS1.linux] = 1;
			osValues[(int)EnumOS1.solaris] = 2;
			osValues[(int)EnumOS1.windows] = 3;
			osValues[(int)EnumOS1.macos] = 4;
		}
	}

}