namespace net.minecraft.src
{
	internal class OsMap
	{
		internal static readonly int[] osValues = new int[(EnumOS1[])Enum.GetValues(typeof(EnumOS1)).Length];

		static OsMap()
		{
			try
			{
				osValues[EnumOS1.linux.ordinal()] = 1;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				osValues[EnumOS1.solaris.ordinal()] = 2;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				osValues[EnumOS1.windows.ordinal()] = 3;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				osValues[EnumOS1.macos.ordinal()] = 4;
			}
			catch (NoSuchFieldError)
			{
			}

		}
	}

}