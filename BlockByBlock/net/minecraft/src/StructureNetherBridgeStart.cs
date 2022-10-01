using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	internal class StructureNetherBridgeStart : StructureStart
	{
		public StructureNetherBridgeStart(World world1, RandomExtended random2, int i3, int i4)
		{
			ComponentNetherBridgeStartPiece componentNetherBridgeStartPiece5 = new ComponentNetherBridgeStartPiece(random2, (i3 << 4) + 2, (i4 << 4) + 2);
			this.components.Add(componentNetherBridgeStartPiece5);
			componentNetherBridgeStartPiece5.buildComponent(componentNetherBridgeStartPiece5, this.components, random2);
			List<StructureComponent> arrayList6 = componentNetherBridgeStartPiece5.field_40034_d;

			while (arrayList6.Count > 0)
			{
				int i7 = random2.Next(arrayList6.Count);
				StructureComponent structureComponent8 = arrayList6.RemoveAndReturn(i7);
				structureComponent8.buildComponent(componentNetherBridgeStartPiece5, this.components, random2);
			}

			this.updateBoundingBox();
			this.setRandomHeight(world1, random2, 48, 70);
		}
	}

}