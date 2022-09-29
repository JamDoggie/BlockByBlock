using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class WeightedRandom
	{
		public static int getTotalWeight(System.Collections.ICollection collection0)
		{
			int i1 = 0;

			WeightedRandomChoice weightedRandomChoice3;
			for (System.Collections.IEnumerator iterator2 = collection0.GetEnumerator(); iterator2.MoveNext(); i1 += weightedRandomChoice3.itemWeight)
			{
				weightedRandomChoice3 = (WeightedRandomChoice)iterator2.Current;
			}

			return i1;
		}

		public static WeightedRandomChoice getRandomItem(RandomExtended random0, System.Collections.ICollection collection1, int i2)
		{
			if (i2 <= 0)
			{
				throw new System.ArgumentException();
			}
			else
			{
				int i3 = random0.Next(i2);
				System.Collections.IEnumerator iterator4 = collection1.GetEnumerator();

				WeightedRandomChoice weightedRandomChoice5;
				do
				{
					if (!iterator4.MoveNext())
					{
						return null;
					}

					weightedRandomChoice5 = (WeightedRandomChoice)iterator4.Current;
					i3 -= weightedRandomChoice5.itemWeight;
				} while (i3 >= 0);

				return weightedRandomChoice5;
			}
		}

		public static WeightedRandomChoice getRandomItem(RandomExtended random0, System.Collections.ICollection collection1)
		{
			return getRandomItem(random0, collection1, getTotalWeight(collection1));
		}

		public static int getTotalWeight(WeightedRandomChoice[] weightedRandomChoice0)
		{
			int i1 = 0;
			WeightedRandomChoice[] weightedRandomChoice2 = weightedRandomChoice0;
			int i3 = weightedRandomChoice0.Length;

			for (int i4 = 0; i4 < i3; ++i4)
			{
				WeightedRandomChoice weightedRandomChoice5 = weightedRandomChoice2[i4];
				i1 += weightedRandomChoice5.itemWeight;
			}

			return i1;
		}

		public static WeightedRandomChoice getRandomItem(RandomExtended random0, WeightedRandomChoice[] weightedRandomChoice1, int i2)
		{
			if (i2 <= 0)
			{
				throw new System.ArgumentException();
			}
			else
			{
				int i3 = random0.Next(i2);
				WeightedRandomChoice[] weightedRandomChoice4 = weightedRandomChoice1;
				int i5 = weightedRandomChoice1.Length;

				for (int i6 = 0; i6 < i5; ++i6)
				{
					WeightedRandomChoice weightedRandomChoice7 = weightedRandomChoice4[i6];
					i3 -= weightedRandomChoice7.itemWeight;
					if (i3 < 0)
					{
						return weightedRandomChoice7;
					}
				}

				return null;
			}
		}

		public static WeightedRandomChoice getRandomItem(RandomExtended random0, WeightedRandomChoice[] weightedRandomChoice1)
		{
			return getRandomItem(random0, weightedRandomChoice1, getTotalWeight(weightedRandomChoice1));
		}
	}

}