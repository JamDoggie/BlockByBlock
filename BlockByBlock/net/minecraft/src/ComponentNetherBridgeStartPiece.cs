using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentNetherBridgeStartPiece : ComponentNetherBridgeCrossing3
	{
		public StructureNetherBridgePieceWeight field_40037_a;
		public System.Collections.IList field_40035_b = new ArrayList();
		public System.Collections.IList field_40036_c;
		public List<StructureComponent> field_40034_d = new();

		public ComponentNetherBridgeStartPiece(RandomExtended random1, int i2, int i3) : base(random1, i2, i3)
		{
			StructureNetherBridgePieceWeight[] structureNetherBridgePieceWeight4 = StructureNetherBridgePieces.PrimaryComponents;
			int i5 = structureNetherBridgePieceWeight4.Length;

			int i6;
			StructureNetherBridgePieceWeight structureNetherBridgePieceWeight7;
			for (i6 = 0; i6 < i5; ++i6)
			{
				structureNetherBridgePieceWeight7 = structureNetherBridgePieceWeight4[i6];
				structureNetherBridgePieceWeight7.field_40698_c = 0;
				this.field_40035_b.Add(structureNetherBridgePieceWeight7);
			}

			this.field_40036_c = new ArrayList();
			structureNetherBridgePieceWeight4 = StructureNetherBridgePieces.SecondaryComponents;
			i5 = structureNetherBridgePieceWeight4.Length;

			for (i6 = 0; i6 < i5; ++i6)
			{
				structureNetherBridgePieceWeight7 = structureNetherBridgePieceWeight4[i6];
				structureNetherBridgePieceWeight7.field_40698_c = 0;
				this.field_40036_c.Add(structureNetherBridgePieceWeight7);
			}

		}
	}

}