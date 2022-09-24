namespace net.minecraft.src
{
	public class Direction
	{
		public static readonly int[] offsetX = new int[]{0, -1, 0, 1};
		public static readonly int[] offsetZ = new int[]{1, 0, -1, 0};
		public static readonly int[] headInvisibleFace = new int[]{3, 4, 2, 5};
		public static readonly int[] vineGrowth = new int[]{-1, -1, 2, 0, 1, 3};
		public static readonly int[] footInvisibleFaceRemap = new int[]{2, 3, 0, 1};
		public static readonly int[] enderEyeMetaToDirection = new int[]{1, 2, 3, 0};
        public static readonly int[] field_35868_g = new int[] { 3, 0, 1, 2 }; // You really hate to see an unmapped field that is,
																			   // for all intents and purposes, impossible to tell what it does.
        public static readonly int[][] bedDirection = new int[][]
		{
			new int[] {1, 0, 3, 2, 5, 4},
			new int[] {1, 0, 5, 4, 2, 3},
			new int[] {1, 0, 2, 3, 4, 5},
			new int[] {1, 0, 4, 5, 3, 2}
		};
	}

}