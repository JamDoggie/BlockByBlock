using System.Collections;

namespace net.minecraft.src
{

	public class IntCache
	{
		private static int intCacheSize = 256;
		private static List<int[]> freeSmallArrays = new List<int[]>();
		private static List<int[]> inUseSmallArrays = new List<int[]>();
		private static List<int[]> freeLargeArrays = new List<int[]>();
		private static List<int[]> inUseLargeArrays = new List<int[]>();

		public static int[] getIntCache(int i0)
		{
			int[] i1;
			if (i0 <= 256)
			{
				if (freeSmallArrays.Count == 0)
				{
					i1 = new int[256];
					inUseSmallArrays.Add(i1);
					return i1;
				}
				else
				{
					i1 = (int[])freeSmallArrays.RemoveAndReturn(freeSmallArrays.Count - 1);
					inUseSmallArrays.Add(i1);
					return i1;
				}
			}
			else if (i0 > intCacheSize)
			{
				intCacheSize = i0;
				freeLargeArrays.Clear();
				inUseLargeArrays.Clear();
				i1 = new int[intCacheSize];
				inUseLargeArrays.Add(i1);
				return i1;
			}
			else if (freeLargeArrays.Count == 0)
			{
				i1 = new int[intCacheSize];
				inUseLargeArrays.Add(i1);
				return i1;
			}
			else
			{
				i1 = (int[])freeLargeArrays.RemoveAndReturn(freeLargeArrays.Count - 1);
				inUseLargeArrays.Add(i1);
				return i1;
			}
		}

		public static void resetIntCache()
		{
			if (freeLargeArrays.Count > 0)
			{
				freeLargeArrays.RemoveAt(freeLargeArrays.Count - 1);
			}

			if (freeSmallArrays.Count > 0)
			{
				freeSmallArrays.RemoveAt(freeSmallArrays.Count - 1);
			}

			freeLargeArrays.AddRange(inUseLargeArrays);
			freeSmallArrays.AddRange(inUseSmallArrays);
			inUseLargeArrays.Clear();
			inUseSmallArrays.Clear();
		}
	}

}