namespace net.minecraft.src
{
	internal class EnumOptionsMappingHelper
	{
		internal static readonly int[] enumOptionsMappingHelperArray = new int[EnumOptions.values().Length];

		static EnumOptionsMappingHelper()
		{
			try
			{
				enumOptionsMappingHelperArray[EnumOptions.INVERT_MOUSE.ordinal()] = 1;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOptionsMappingHelperArray[EnumOptions.VIEW_BOBBING.ordinal()] = 2;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOptionsMappingHelperArray[EnumOptions.ANAGLYPH.ordinal()] = 3;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOptionsMappingHelperArray[EnumOptions.ADVANCED_OPENGL.ordinal()] = 4;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOptionsMappingHelperArray[EnumOptions.AMBIENT_OCCLUSION.ordinal()] = 5;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumOptionsMappingHelperArray[EnumOptions.RENDER_CLOUDS.ordinal()] = 6;
			}
			catch (NoSuchFieldError)
			{
			}

		}
	}

}