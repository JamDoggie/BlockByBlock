using System;

namespace net.minecraft.src
{
	public class StructureStrongholdPieceWeight
	{
		public Type pieceClass;
		public readonly int pieceWeight;
		public int instancesSpawned;
		public int instancesLimit;

		public StructureStrongholdPieceWeight(Type class1, int i2, int i3)
		{
			this.pieceClass = class1;
			this.pieceWeight = i2;
			this.instancesLimit = i3;
		}

		public virtual bool canSpawnMoreStructuresOfType(int i1)
		{
			return this.instancesLimit == 0 || this.instancesSpawned < this.instancesLimit;
		}

		public virtual bool canSpawnMoreStructures()
		{
			return this.instancesLimit == 0 || this.instancesSpawned < this.instancesLimit;
		}
	}

}