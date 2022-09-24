using System;

namespace net.minecraft.src
{
	public class StructureVillagePieceWeight
	{
		public Type villagePieceClass;
		public readonly int villagePieceWeight;
		public int villagePiecesSpawned;
		public int villagePiecesLimit;

		public StructureVillagePieceWeight(Type class1, int i2, int i3)
		{
			this.villagePieceClass = class1;
			this.villagePieceWeight = i2;
			this.villagePiecesLimit = i3;
		}

		public virtual bool canSpawnMoreVillagePiecesOfType(int i1)
		{
			return this.villagePiecesLimit == 0 || this.villagePiecesSpawned < this.villagePiecesLimit;
		}

		public virtual bool canSpawnMoreVillagePieces()
		{
			return this.villagePiecesLimit == 0 || this.villagePiecesSpawned < this.villagePiecesLimit;
		}
	}

}