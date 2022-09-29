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
			villagePieceClass = class1;
			villagePieceWeight = i2;
			villagePiecesLimit = i3;
		}

		public virtual bool canSpawnMoreVillagePiecesOfType(int i1)
		{
			return villagePiecesLimit == 0 || villagePiecesSpawned < villagePiecesLimit;
		}

		public virtual bool canSpawnMoreVillagePieces()
		{
			return villagePiecesLimit == 0 || villagePiecesSpawned < villagePiecesLimit;
		}
	}

}