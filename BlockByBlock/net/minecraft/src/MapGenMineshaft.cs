using System;

namespace net.minecraft.src
{
	public class MapGenMineshaft : MapGenStructure
	{
		protected internal override bool canSpawnStructureAtCoords(int i1, int i2)
		{
			return this.rand.Next(100) == 0 && this.rand.Next(80) < Math.Max(Math.Abs(i1), Math.Abs(i2));
		}

		protected internal override StructureStart getStructureStart(int i1, int i2)
		{
			return new StructureMineshaftStart(this.worldObj, this.rand, i1, i2);
		}
	}

}