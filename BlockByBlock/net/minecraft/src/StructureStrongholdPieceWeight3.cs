using System;

namespace net.minecraft.src
{
	internal sealed class StructureStrongholdPieceWeight3 : StructureStrongholdPieceWeight
	{
		internal StructureStrongholdPieceWeight3(Type class1, int i2, int i3) : base(class1, i2, i3)
		{
		}

		public override bool canSpawnMoreStructuresOfType(int i1)
		{
			return base.canSpawnMoreStructuresOfType(i1) && i1 > 5;
		}
	}

}