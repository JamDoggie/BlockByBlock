using System;

namespace net.minecraft.src
{
	public class SpawnListEntry : WeightedRandomChoice
	{
		public Type entityClass;
		public int minGroupCount;
		public int maxGroupCount;

		public SpawnListEntry(Type class1, int i2, int i3, int i4) : base(i2)
		{
			this.entityClass = class1;
			this.minGroupCount = i3;
			this.maxGroupCount = i4;
		}
	}

}