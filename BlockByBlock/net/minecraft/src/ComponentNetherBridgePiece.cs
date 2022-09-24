using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public abstract class ComponentNetherBridgePiece : StructureComponent
	{
		protected internal ComponentNetherBridgePiece(int i1) : base(i1)
		{
		}

		private int getTotalWeight(System.Collections.IList list1)
		{
			bool z2 = false;
			int i3 = 0;

			StructureNetherBridgePieceWeight structureNetherBridgePieceWeight5;
			for (System.Collections.IEnumerator iterator4 = list1.GetEnumerator(); iterator4.MoveNext(); i3 += structureNetherBridgePieceWeight5.field_40697_b)
			{
				structureNetherBridgePieceWeight5 = (StructureNetherBridgePieceWeight)iterator4.Current;
				if (structureNetherBridgePieceWeight5.field_40695_d > 0 && structureNetherBridgePieceWeight5.field_40698_c < structureNetherBridgePieceWeight5.field_40695_d)
				{
					z2 = true;
				}
			}

			return z2 ? i3 : -1;
		}

		private ComponentNetherBridgePiece getNextComponent(ComponentNetherBridgeStartPiece componentNetherBridgeStartPiece1, System.Collections.IList list2, System.Collections.IList list3, RandomExtended random4, int i5, int i6, int i7, int i8, int i9)
		{
			int i10 = this.getTotalWeight(list2);
			bool z11 = i10 > 0 && i9 <= 30;
			int i12 = 0;

			while (i12 < 5 && z11)
			{
				++i12;
				int i13 = random4.Next(i10);
				System.Collections.IEnumerator iterator14 = list2.GetEnumerator();

				while (iterator14.MoveNext())
				{
					StructureNetherBridgePieceWeight structureNetherBridgePieceWeight15 = (StructureNetherBridgePieceWeight)iterator14.Current;
					i13 -= structureNetherBridgePieceWeight15.field_40697_b;
					if (i13 < 0)
					{
						if (!structureNetherBridgePieceWeight15.func_40693_a(i9) || structureNetherBridgePieceWeight15 == componentNetherBridgeStartPiece1.field_40037_a && !structureNetherBridgePieceWeight15.field_40696_e)
						{
							break;
						}

						ComponentNetherBridgePiece componentNetherBridgePiece16 = StructureNetherBridgePieces.createNextComponent(structureNetherBridgePieceWeight15, list3, random4, i5, i6, i7, i8, i9);
						if (componentNetherBridgePiece16 != null)
						{
							++structureNetherBridgePieceWeight15.field_40698_c;
							componentNetherBridgeStartPiece1.field_40037_a = structureNetherBridgePieceWeight15;
							if (!structureNetherBridgePieceWeight15.func_40694_a())
							{
								list2.Remove(structureNetherBridgePieceWeight15);
							}

							return componentNetherBridgePiece16;
						}
					}
				}
			}

			ComponentNetherBridgeEnd componentNetherBridgeEnd17 = ComponentNetherBridgeEnd.func_40023_a(list3, random4, i5, i6, i7, i8, i9);
			return componentNetherBridgeEnd17;
		}

		private StructureComponent getNextComponent(ComponentNetherBridgeStartPiece componentNetherBridgeStartPiece1, System.Collections.IList list2, RandomExtended random3, int i4, int i5, int i6, int i7, int i8, bool z9)
		{
			if (Math.Abs(i4 - componentNetherBridgeStartPiece1.BoundingBox.minX) <= 112 && Math.Abs(i6 - componentNetherBridgeStartPiece1.BoundingBox.minZ) <= 112)
			{
				System.Collections.IList list12 = componentNetherBridgeStartPiece1.field_40035_b;
				if (z9)
				{
					list12 = componentNetherBridgeStartPiece1.field_40036_c;
				}

				ComponentNetherBridgePiece componentNetherBridgePiece11 = this.getNextComponent(componentNetherBridgeStartPiece1, list12, list2, random3, i4, i5, i6, i7, i8 + 1);
				if (componentNetherBridgePiece11 != null)
				{
					list2.Add(componentNetherBridgePiece11);
					componentNetherBridgeStartPiece1.field_40034_d.Add(componentNetherBridgePiece11);
				}

				return componentNetherBridgePiece11;
			}
			else
			{
				ComponentNetherBridgeEnd componentNetherBridgeEnd10 = ComponentNetherBridgeEnd.func_40023_a(list2, random3, i4, i5, i6, i7, i8);
				return componentNetherBridgeEnd10;
			}
		}

		protected internal virtual StructureComponent getNextComponentNormal(ComponentNetherBridgeStartPiece componentNetherBridgeStartPiece1, System.Collections.IList list2, RandomExtended random3, int i4, int i5, bool z6)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX + i4, this.boundingBox.minY + i5, this.boundingBox.maxZ + 1, this.coordBaseMode, this.ComponentType, z6);
			case 1:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i5, this.boundingBox.minZ + i4, this.coordBaseMode, this.ComponentType, z6);
			case 2:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX + i4, this.boundingBox.minY + i5, this.boundingBox.minZ - 1, this.coordBaseMode, this.ComponentType, z6);
			case 3:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i5, this.boundingBox.minZ + i4, this.coordBaseMode, this.ComponentType, z6);
			default:
				return null;
			}
		}

		protected internal virtual StructureComponent getNextComponentX(ComponentNetherBridgeStartPiece componentNetherBridgeStartPiece1, System.Collections.IList list2, RandomExtended random3, int i4, int i5, bool z6)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 1, this.ComponentType, z6);
			case 1:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.minZ - 1, 2, this.ComponentType, z6);
			case 2:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX - 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 1, this.ComponentType, z6);
			case 3:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.minZ - 1, 2, this.ComponentType, z6);
			default:
				return null;
			}
		}

		protected internal virtual StructureComponent getNextComponentZ(ComponentNetherBridgeStartPiece componentNetherBridgeStartPiece1, System.Collections.IList list2, RandomExtended random3, int i4, int i5, bool z6)
		{
			switch (this.coordBaseMode)
			{
			case 0:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 3, this.ComponentType, z6);
			case 1:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.maxZ + 1, 0, this.ComponentType, z6);
			case 2:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.maxX + 1, this.boundingBox.minY + i4, this.boundingBox.minZ + i5, 3, this.ComponentType, z6);
			case 3:
				return this.getNextComponent(componentNetherBridgeStartPiece1, list2, random3, this.boundingBox.minX + i5, this.boundingBox.minY + i4, this.boundingBox.maxZ + 1, 0, this.ComponentType, z6);
			default:
				return null;
			}
		}

		protected internal static bool isAboveGround(StructureBoundingBox structureBoundingBox0)
		{
			return structureBoundingBox0 != null && structureBoundingBox0.minY > 10;
		}
	}

}