namespace net.minecraft.src
{
	public class Facing
	{
		public static readonly int[] faceToSide = new int[]{1, 0, 3, 2, 5, 4};
		public static readonly int[] offsetsXForSide = new int[]{0, 0, 0, 0, -1, 1};
		public static readonly int[] offsetsYForSide = new int[]{-1, 1, 0, 0, 0, 0};
		public static readonly int[] offsetsZForSide = new int[]{0, 0, -1, 1, 0, 0};
	}
}