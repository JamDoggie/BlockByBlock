using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class StructureStrongholdPieces
	{
		private static readonly StructureStrongholdPieceWeight[] pieceWeightArray = new StructureStrongholdPieceWeight[]
		{
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdStraight), 40, 0),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdPrison), 5, 5),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdLeftTurn), 20, 0),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdRightTurn), 20, 0),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdRoomCrossing), 10, 6),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdStairsStraight), 5, 5),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdStairs), 5, 5),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdCrossing), 5, 4),
			new StructureStrongholdPieceWeight(typeof(ComponentStrongholdChestCorridor), 5, 4),
			new StructureStrongholdPieceWeight2(typeof(ComponentStrongholdLibrary), 10, 2),
			new StructureStrongholdPieceWeight3(typeof(ComponentStrongholdPortalRoom), 20, 1)
		};
		private static System.Collections.IList structurePieceList;
		private static Type strongComponentType;
		internal static int totalWeight = 0;
		private static readonly StructureStrongholdStones strongholdStones = new StructureStrongholdStones((StructureStrongholdPieceWeight2)null);

		public static void prepareStructurePieces()
		{
			structurePieceList = new ArrayList();
			StructureStrongholdPieceWeight[] structureStrongholdPieceWeight0 = pieceWeightArray;
			int i1 = structureStrongholdPieceWeight0.Length;

			for (int i2 = 0; i2 < i1; ++i2)
			{
				StructureStrongholdPieceWeight structureStrongholdPieceWeight3 = structureStrongholdPieceWeight0[i2];
				structureStrongholdPieceWeight3.instancesSpawned = 0;
				structurePieceList.Add(structureStrongholdPieceWeight3);
			}

			strongComponentType = null;
		}

		private static bool canAddStructurePieces()
		{
			bool z0 = false;
			totalWeight = 0;

			StructureStrongholdPieceWeight structureStrongholdPieceWeight2;
			for (System.Collections.IEnumerator iterator1 = structurePieceList.GetEnumerator(); iterator1.MoveNext(); totalWeight += structureStrongholdPieceWeight2.pieceWeight)
			{
				structureStrongholdPieceWeight2 = (StructureStrongholdPieceWeight)iterator1.Current;
				if (structureStrongholdPieceWeight2.instancesLimit > 0 && structureStrongholdPieceWeight2.instancesSpawned < structureStrongholdPieceWeight2.instancesLimit)
				{
					z0 = true;
				}
			}

			return z0;
		}

		private static ComponentStronghold getStrongholdComponentFromWeightedPiece(Type class0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			object object8 = null;
			if (class0 == typeof(ComponentStrongholdStraight))
			{
				object8 = ComponentStrongholdStraight.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdPrison))
			{
				object8 = ComponentStrongholdPrison.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdLeftTurn))
			{
				object8 = ComponentStrongholdLeftTurn.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdRightTurn))
			{
				object8 = ComponentStrongholdRightTurn.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdRoomCrossing))
			{
				object8 = ComponentStrongholdRoomCrossing.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdStairsStraight))
			{
				object8 = ComponentStrongholdStairsStraight.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdStairs))
			{
				object8 = ComponentStrongholdStairs.getStrongholdStairsComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdCrossing))
			{
				object8 = ComponentStrongholdCrossing.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdChestCorridor))
			{
				object8 = ComponentStrongholdChestCorridor.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdLibrary))
			{
				object8 = ComponentStrongholdLibrary.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class0 == typeof(ComponentStrongholdPortalRoom))
			{
				object8 = ComponentStrongholdPortalRoom.findValidPlacement(list1, random2, i3, i4, i5, i6, i7);
			}

			return (ComponentStronghold)object8;
		}

		private static ComponentStronghold getNextComponent(ComponentStrongholdStairs2 componentStrongholdStairs20, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			if (!canAddStructurePieces())
			{
				return null;
			}
			else
			{
				if (strongComponentType != null)
				{
					ComponentStronghold componentStronghold8 = getStrongholdComponentFromWeightedPiece(strongComponentType, list1, random2, i3, i4, i5, i6, i7);
					strongComponentType = null;
					if (componentStronghold8 != null)
					{
						return componentStronghold8;
					}
				}

				int i13 = 0;

				while (i13 < 5)
				{
					++i13;
					int i9 = random2.Next(totalWeight);
					System.Collections.IEnumerator iterator10 = structurePieceList.GetEnumerator();

					while (iterator10.MoveNext())
					{
						StructureStrongholdPieceWeight structureStrongholdPieceWeight11 = (StructureStrongholdPieceWeight)iterator10.Current;
						i9 -= structureStrongholdPieceWeight11.pieceWeight;
						if (i9 < 0)
						{
							if (!structureStrongholdPieceWeight11.canSpawnMoreStructuresOfType(i7) || structureStrongholdPieceWeight11 == componentStrongholdStairs20.field_35038_a)
							{
								break;
							}

							ComponentStronghold componentStronghold12 = getStrongholdComponentFromWeightedPiece(structureStrongholdPieceWeight11.pieceClass, list1, random2, i3, i4, i5, i6, i7);
							if (componentStronghold12 != null)
							{
								++structureStrongholdPieceWeight11.instancesSpawned;
								componentStrongholdStairs20.field_35038_a = structureStrongholdPieceWeight11;
								if (!structureStrongholdPieceWeight11.canSpawnMoreStructures())
								{
									structurePieceList.Remove(structureStrongholdPieceWeight11);
								}

								return componentStronghold12;
							}
						}
					}
				}

				StructureBoundingBox structureBoundingBox14 = ComponentStrongholdCorridor.func_35051_a(list1, random2, i3, i4, i5, i6);
				if (structureBoundingBox14 != null && structureBoundingBox14.minY > 1)
				{
					return new ComponentStrongholdCorridor(i7, random2, structureBoundingBox14, i6);
				}
				else
				{
					return null;
				}
			}
		}

		private static StructureComponent getNextValidComponent(ComponentStrongholdStairs2 componentStrongholdStairs20, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			if (i7 > 50)
			{
				return null;
			}
			else if (Math.Abs(i3 - componentStrongholdStairs20.BoundingBox.minX) <= 112 && Math.Abs(i5 - componentStrongholdStairs20.BoundingBox.minZ) <= 112)
			{
				ComponentStronghold componentStronghold8 = getNextComponent(componentStrongholdStairs20, list1, random2, i3, i4, i5, i6, i7 + 1);
				if (componentStronghold8 != null)
				{
					list1.Add(componentStronghold8);
					componentStrongholdStairs20.field_35037_b.Add(componentStronghold8);
				}

				return componentStronghold8;
			}
			else
			{
				return null;
			}
		}

		internal static StructureComponent getNextValidComponentAccess(ComponentStrongholdStairs2 componentStrongholdStairs20, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			return getNextValidComponent(componentStrongholdStairs20, list1, random2, i3, i4, i5, i6, i7);
		}

		internal static Type setComponentType(Type class0)
		{
			strongComponentType = class0;
			return class0;
		}

		internal static StructureStrongholdStones StrongholdStones
		{
			get
			{
				return strongholdStones;
			}
		}
	}

}