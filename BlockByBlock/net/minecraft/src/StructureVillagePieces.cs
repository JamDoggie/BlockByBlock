using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class StructureVillagePieces
	{
		public static List<StructureVillagePieceWeight> getStructureVillageWeightedPieceList(RandomExtended random0, int i1)
		{
			List<StructureVillagePieceWeight> arrayList2 = new();
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageHouse4_Garden), 4, MathHelper.getRandomIntegerInRange(random0, 2 + i1, 4 + i1 * 2)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageChurch), 20, MathHelper.getRandomIntegerInRange(random0, 0 + i1, 1 + i1)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageHouse1), 20, MathHelper.getRandomIntegerInRange(random0, 0 + i1, 2 + i1)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageWoodHut), 3, MathHelper.getRandomIntegerInRange(random0, 2 + i1, 5 + i1 * 3)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageHall), 15, MathHelper.getRandomIntegerInRange(random0, 0 + i1, 2 + i1)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageField), 3, MathHelper.getRandomIntegerInRange(random0, 1 + i1, 4 + i1)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageField2), 3, MathHelper.getRandomIntegerInRange(random0, 2 + i1, 4 + i1 * 2)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageHouse2), 15, MathHelper.getRandomIntegerInRange(random0, 0, 1 + i1)));
			arrayList2.Add(new StructureVillagePieceWeight(typeof(ComponentVillageHouse3), 8, MathHelper.getRandomIntegerInRange(random0, 0 + i1, 3 + i1 * 2)));

			for (int i = arrayList2.Count - 1; i >= 0; i--)
            {
				StructureVillagePieceWeight pieceWeight = arrayList2[i];
				if (pieceWeight.villagePiecesLimit == 0)
				{
					arrayList2.Remove(pieceWeight);
				}
			}

			return arrayList2;
		}

		private static int getAvailablePieceWeight(List<StructureVillagePieceWeight> arrayList0)
		{
			bool z1 = false;
			int i2 = 0;

			StructureVillagePieceWeight structureVillagePieceWeight4;
			for (System.Collections.IEnumerator iterator3 = arrayList0.GetEnumerator(); iterator3.MoveNext(); i2 += structureVillagePieceWeight4.villagePieceWeight)
			{
				structureVillagePieceWeight4 = (StructureVillagePieceWeight)iterator3.Current;
				if (structureVillagePieceWeight4.villagePiecesLimit > 0 && structureVillagePieceWeight4.villagePiecesSpawned < structureVillagePieceWeight4.villagePiecesLimit)
				{
					z1 = true;
				}
			}

			return z1 ? i2 : -1;
		}

		private static ComponentVillage getVillageComponentFromWeightedPiece(StructureVillagePieceWeight structureVillagePieceWeight0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			Type class8 = structureVillagePieceWeight0.villagePieceClass;
			object object9 = null;
			if (class8 == typeof(ComponentVillageHouse4_Garden))
			{
				object9 = ComponentVillageHouse4_Garden.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageChurch))
			{
				object9 = ComponentVillageChurch.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageHouse1))
			{
				object9 = ComponentVillageHouse1.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageWoodHut))
			{
				object9 = ComponentVillageWoodHut.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageHall))
			{
				object9 = ComponentVillageHall.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageField))
			{
				object9 = ComponentVillageField.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageField2))
			{
				object9 = ComponentVillageField2.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageHouse2))
			{
				object9 = ComponentVillageHouse2.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentVillageHouse3))
			{
				object9 = ComponentVillageHouse3.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}

			return (ComponentVillage)object9;
		}

		private static ComponentVillage getNextVillageComponent(ComponentVillageStartPiece componentVillageStartPiece0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			int i8 = getAvailablePieceWeight(componentVillageStartPiece0.structureVillageWeightedPieceList);
			if (i8 <= 0)
			{
				return null;
			}
			else
			{
				int i9 = 0;

				while (i9 < 5)
				{
					++i9;
					int i10 = random2.Next(i8);
					System.Collections.IEnumerator iterator11 = componentVillageStartPiece0.structureVillageWeightedPieceList.GetEnumerator();

					while (iterator11.MoveNext())
					{
						StructureVillagePieceWeight structureVillagePieceWeight12 = (StructureVillagePieceWeight)iterator11.Current;
						i10 -= structureVillagePieceWeight12.villagePieceWeight;
						if (i10 < 0)
						{
							if (!structureVillagePieceWeight12.canSpawnMoreVillagePiecesOfType(i7) || structureVillagePieceWeight12 == componentVillageStartPiece0.structVillagePieceWeight && componentVillageStartPiece0.structureVillageWeightedPieceList.Count > 1)
							{
								break;
							}

							ComponentVillage componentVillage13 = getVillageComponentFromWeightedPiece(structureVillagePieceWeight12, list1, random2, i3, i4, i5, i6, i7);
							if (componentVillage13 != null)
							{
								++structureVillagePieceWeight12.villagePiecesSpawned;
								componentVillageStartPiece0.structVillagePieceWeight = structureVillagePieceWeight12;
								if (!structureVillagePieceWeight12.canSpawnMoreVillagePieces())
								{
									componentVillageStartPiece0.structureVillageWeightedPieceList.Remove(structureVillagePieceWeight12);
								}

								return componentVillage13;
							}
						}
					}
				}

				StructureBoundingBox structureBoundingBox14 = ComponentVillageTorch.findValidPlacement(list1, random2, i3, i4, i5, i6);
				if (structureBoundingBox14 != null)
				{
					return new ComponentVillageTorch(i7, random2, structureBoundingBox14, i6);
				}
				else
				{
					return null;
				}
			}
		}

		private static StructureComponent getNextVillageStructureComponent(ComponentVillageStartPiece componentVillageStartPiece0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			if (i7 > 50)
			{
				return null;
			}
			else if (Math.Abs(i3 - componentVillageStartPiece0.BoundingBox.minX) <= 112 && Math.Abs(i5 - componentVillageStartPiece0.BoundingBox.minZ) <= 112)
			{
				ComponentVillage componentVillage8 = getNextVillageComponent(componentVillageStartPiece0, list1, random2, i3, i4, i5, i6, i7 + 1);
				if (componentVillage8 != null)
				{
					int i9 = (componentVillage8.boundingBox.minX + componentVillage8.boundingBox.maxX) / 2;
					int i10 = (componentVillage8.boundingBox.minZ + componentVillage8.boundingBox.maxZ) / 2;
					int i11 = componentVillage8.boundingBox.maxX - componentVillage8.boundingBox.minX;
					int i12 = componentVillage8.boundingBox.maxZ - componentVillage8.boundingBox.minZ;
					int i13 = i11 > i12 ? i11 : i12;
					if (componentVillageStartPiece0.WorldChunkManager.areBiomesViable(i9, i10, i13 / 2 + 4, MapGenVillage.villageSpawnBiomes))
					{
						list1.Add(componentVillage8);
						componentVillageStartPiece0.field_35108_e.Add(componentVillage8);
						return componentVillage8;
					}
				}

				return null;
			}
			else
			{
				return null;
			}
		}

		private static StructureComponent getNextComponentVillagePath(ComponentVillageStartPiece componentVillageStartPiece0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			if (i7 > 3 + componentVillageStartPiece0.terrainType)
			{
				return null;
			}
			else if (Math.Abs(i3 - componentVillageStartPiece0.BoundingBox.minX) <= 112 && Math.Abs(i5 - componentVillageStartPiece0.BoundingBox.minZ) <= 112)
			{
				StructureBoundingBox structureBoundingBox8 = ComponentVillagePathGen.func_35087_a(componentVillageStartPiece0, list1, random2, i3, i4, i5, i6);
				if (structureBoundingBox8 != null && structureBoundingBox8.minY > 10)
				{
					ComponentVillagePathGen componentVillagePathGen9 = new ComponentVillagePathGen(i7, random2, structureBoundingBox8, i6);
					int i10 = (componentVillagePathGen9.boundingBox.minX + componentVillagePathGen9.boundingBox.maxX) / 2;
					int i11 = (componentVillagePathGen9.boundingBox.minZ + componentVillagePathGen9.boundingBox.maxZ) / 2;
					int i12 = componentVillagePathGen9.boundingBox.maxX - componentVillagePathGen9.boundingBox.minX;
					int i13 = componentVillagePathGen9.boundingBox.maxZ - componentVillagePathGen9.boundingBox.minZ;
					int i14 = i12 > i13 ? i12 : i13;
					if (componentVillageStartPiece0.WorldChunkManager.areBiomesViable(i10, i11, i14 / 2 + 4, MapGenVillage.villageSpawnBiomes))
					{
						list1.Add(componentVillagePathGen9);
						componentVillageStartPiece0.field_35106_f.Add(componentVillagePathGen9);
						return componentVillagePathGen9;
					}
				}

				return null;
			}
			else
			{
				return null;
			}
		}

		internal static StructureComponent getNextStructureComponent(ComponentVillageStartPiece componentVillageStartPiece0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			return getNextVillageStructureComponent(componentVillageStartPiece0, list1, random2, i3, i4, i5, i6, i7);
		}

		internal static StructureComponent getNextStructureComponentVillagePath(ComponentVillageStartPiece componentVillageStartPiece0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			return getNextComponentVillagePath(componentVillageStartPiece0, list1, random2, i3, i4, i5, i6, i7);
		}
	}

}