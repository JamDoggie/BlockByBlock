using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class StructureNetherBridgePieces
	{
		private static readonly StructureNetherBridgePieceWeight[] primaryComponents = new StructureNetherBridgePieceWeight[]
		{
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeStraight), 30, 0, true),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCrossing3), 10, 4),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCrossing), 10, 4),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeStairs), 10, 3),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeThrone), 5, 2),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeEntrance), 5, 1)
		};
		private static readonly StructureNetherBridgePieceWeight[] secondaryComponents = new StructureNetherBridgePieceWeight[]
		{
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCorridor5), 25, 0, true),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCrossing2), 15, 5),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCorridor2), 5, 10),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCorridor), 5, 10),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCorridor3), 10, 3, true),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeCorridor4), 7, 2),
			new StructureNetherBridgePieceWeight(typeof(ComponentNetherBridgeNetherStalkRoom), 5, 2)
		};

		private static ComponentNetherBridgePiece createNextComponentRandom(StructureNetherBridgePieceWeight structureNetherBridgePieceWeight0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			Type class8 = structureNetherBridgePieceWeight0.field_40699_a;
			object object9 = null;
			if (class8 == typeof(ComponentNetherBridgeStraight))
			{
				object9 = ComponentNetherBridgeStraight.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCrossing3))
			{
				object9 = ComponentNetherBridgeCrossing3.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCrossing))
			{
				object9 = ComponentNetherBridgeCrossing.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeStairs))
			{
				object9 = ComponentNetherBridgeStairs.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeThrone))
			{
				object9 = ComponentNetherBridgeThrone.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeEntrance))
			{
				object9 = ComponentNetherBridgeEntrance.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCorridor5))
			{
				object9 = ComponentNetherBridgeCorridor5.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCorridor2))
			{
				object9 = ComponentNetherBridgeCorridor2.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCorridor))
			{
				object9 = ComponentNetherBridgeCorridor.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCorridor3))
			{
				object9 = ComponentNetherBridgeCorridor3.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCorridor4))
			{
				object9 = ComponentNetherBridgeCorridor4.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeCrossing2))
			{
				object9 = ComponentNetherBridgeCrossing2.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}
			else if (class8 == typeof(ComponentNetherBridgeNetherStalkRoom))
			{
				object9 = ComponentNetherBridgeNetherStalkRoom.createValidComponent(list1, random2, i3, i4, i5, i6, i7);
			}

			return (ComponentNetherBridgePiece)object9;
		}

		internal static ComponentNetherBridgePiece createNextComponent(StructureNetherBridgePieceWeight structureNetherBridgePieceWeight0, System.Collections.IList list1, RandomExtended random2, int i3, int i4, int i5, int i6, int i7)
		{
			return createNextComponentRandom(structureNetherBridgePieceWeight0, list1, random2, i3, i4, i5, i6, i7);
		}

		internal static StructureNetherBridgePieceWeight[] PrimaryComponents
		{
			get
			{
				return primaryComponents;
			}
		}

		internal static StructureNetherBridgePieceWeight[] SecondaryComponents
		{
			get
			{
				return secondaryComponents;
			}
		}
	}

}