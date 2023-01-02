namespace net.minecraft.src
{
	internal class EnumOptionsMappingHelper
	{
		internal static readonly int[] enumOptionsMappingHelperArray = new int[EnumOptions.values().Length];

		static EnumOptionsMappingHelper()
		{
			
			enumOptionsMappingHelperArray[EnumOptions.INVERT_MOUSE.ordinal()] = 1;
			
			enumOptionsMappingHelperArray[EnumOptions.VIEW_BOBBING.ordinal()] = 2;
			
			enumOptionsMappingHelperArray[EnumOptions.ADVANCED_OPENGL.ordinal()] = 4;
			
			enumOptionsMappingHelperArray[EnumOptions.AMBIENT_OCCLUSION.ordinal()] = 5;
			
			enumOptionsMappingHelperArray[EnumOptions.RENDER_CLOUDS.ordinal()] = 6;
		}
	}
}