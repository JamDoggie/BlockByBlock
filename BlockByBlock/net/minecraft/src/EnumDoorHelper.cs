namespace net.minecraft.src
{
	internal class EnumDoorHelper
	{
		internal static readonly int[] doorEnum = new int[(EnumDoor[])Enum.GetValues(typeof(EnumDoor)).Length];

		static EnumDoorHelper()
		{
			try
			{
				doorEnum[EnumDoor.OPENING.ordinal()] = 1;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				doorEnum[EnumDoor.WOOD_DOOR.ordinal()] = 2;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				doorEnum[EnumDoor.GRATES.ordinal()] = 3;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				doorEnum[EnumDoor.IRON_DOOR.ordinal()] = 4;
			}
			catch (NoSuchFieldError)
			{
			}

		}
	}

}