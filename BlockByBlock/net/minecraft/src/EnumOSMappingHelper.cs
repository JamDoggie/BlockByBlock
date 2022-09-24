namespace net.minecraft.src
{
	public class EnumOSMappingHelper
	{
		public static readonly int[] enumOSMappingArray = new int[(EnumOS2[])Enum.GetValues(typeof(EnumOS2)).Length];

		static EnumOSMappingHelper()
		{
			try
			{
				enumOSMappingArray[EnumOS2.linux.ordinal()] = 1;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOSMappingArray[EnumOS2.solaris.ordinal()] = 2;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOSMappingArray[EnumOS2.windows.ordinal()] = 3;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOSMappingArray[EnumOS2.macos.ordinal()] = 4;
			}
			catch (NoSuchFieldError)
			{
			}

		}
	}

}