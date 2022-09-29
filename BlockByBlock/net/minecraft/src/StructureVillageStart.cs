using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	internal class StructureVillageStart : StructureStart
	{
		private bool hasMoreThanTwoComponents = false;

		public StructureVillageStart(World world1, RandomExtended random2, int i3, int i4, int i5)
		{
			List<StructureVillagePieceWeight> arrayList7 = StructureVillagePieces.getStructureVillageWeightedPieceList(random2, i5);
			ComponentVillageStartPiece componentVillageStartPiece8 = new ComponentVillageStartPiece(world1.WorldChunkManager, 0, random2, (i3 << 4) + 2, (i4 << 4) + 2, arrayList7, i5);
			components.Add(componentVillageStartPiece8);
			componentVillageStartPiece8.buildComponent(componentVillageStartPiece8, this.components, random2);
			List<StructureComponent> arrayList9 = componentVillageStartPiece8.field_35106_f;
			List<StructureComponent> arrayList10 = componentVillageStartPiece8.field_35108_e;

			int i11;
			while (arrayList9.Count > 0 || arrayList10.Count > 0)
			{
				StructureComponent structureComponent12;
				if (arrayList9.Count > 0)
				{
					i11 = random2.Next(arrayList9.Count);
					structureComponent12 = (StructureComponent)arrayList9.RemoveAndReturn(i11);
					structureComponent12.buildComponent(componentVillageStartPiece8, this.components, random2);
				}
				else
				{
					i11 = random2.Next(arrayList10.Count);
					structureComponent12 = (StructureComponent)arrayList10.RemoveAndReturn(i11);
					structureComponent12.buildComponent(componentVillageStartPiece8, this.components, random2);
				}
			}

			this.updateBoundingBox();
			i11 = 0;
			System.Collections.IEnumerator iterator14 = this.components.GetEnumerator();

			while (iterator14.MoveNext())
			{
				StructureComponent structureComponent13 = (StructureComponent)iterator14.Current;
				if (!(structureComponent13 is ComponentVillageRoadPiece))
				{
					++i11;
				}
			}

			this.hasMoreThanTwoComponents = i11 > 2;
		}

		public override bool SizeableStructure
		{
			get
			{
				return this.hasMoreThanTwoComponents;
			}
		}
	}

}