using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentVillageStartPiece : ComponentVillageWell
	{
		public WorldChunkManager worldChunkMngr;
		public int terrainType;
		public StructureVillagePieceWeight structVillagePieceWeight;
		public List<StructureVillagePieceWeight> structureVillageWeightedPieceList;
		public List<StructureComponent> field_35108_e = new();
		public List<StructureComponent> field_35106_f = new();

		public ComponentVillageStartPiece(WorldChunkManager worldChunkManager1, int i2, RandomExtended random3, int i4, int i5, List<StructureVillagePieceWeight> arrayList6, int i7) : base(0, random3, i4, i5)
		{
			worldChunkMngr = worldChunkManager1;
			structureVillageWeightedPieceList = arrayList6;
			terrainType = i7;
		}

		public virtual WorldChunkManager WorldChunkManager
		{
			get
			{
				return this.worldChunkMngr;
			}
		}
	}

}