namespace net.minecraft.src
{
	public class EnumOSMappingHelper
	{
		public static readonly int[] enumOSMappingArray = new int[((EnumOS2[])Enum.GetValues(typeof(EnumOS2))).Length];

		static EnumOSMappingHelper()
		{
			enumOSMappingArray[(int)EnumOS2.linux] = 1;

			enumOSMappingArray[(int)EnumOS2.freebsd] = 2;

			enumOSMappingArray[(int)EnumOS2.windows] = 3;

			enumOSMappingArray[(int)EnumOS2.macos] = 4;

		}
	}

}