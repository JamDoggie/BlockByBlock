namespace net.minecraft.src
{
	internal class EnumDoorHelper
	{
		internal static readonly int[] doorEnum = new int[((EnumDoor[])Enum.GetValues(typeof(EnumDoor))).Length];

		static EnumDoorHelper()
		{
				doorEnum[(int)EnumDoor.OPENING] = 1;

				doorEnum[(int)EnumDoor.WOOD_DOOR] = 2;

				doorEnum[(int)EnumDoor.GRATES] = 3;

				doorEnum[(int)EnumDoor.IRON_DOOR] = 4;
		}
	}
}