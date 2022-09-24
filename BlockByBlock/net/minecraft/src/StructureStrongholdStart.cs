using System;
using System.Collections;

namespace net.minecraft.src
{

	internal class StructureStrongholdStart : StructureStart
	{
		public StructureStrongholdStart(World world1, Random random2, int i3, int i4)
		{
			StructureStrongholdPieces.prepareStructurePieces();
			ComponentStrongholdStairs2 componentStrongholdStairs25 = new ComponentStrongholdStairs2(0, random2, (i3 << 4) + 2, (i4 << 4) + 2);
			this.components.AddLast(componentStrongholdStairs25);
			componentStrongholdStairs25.buildComponent(componentStrongholdStairs25, this.components, random2);
			ArrayList arrayList6 = componentStrongholdStairs25.field_35037_b;

			while (arrayList6.Count > 0)
			{
				int i7 = random2.Next(arrayList6.Count);
				StructureComponent structureComponent8 = (StructureComponent)arrayList6.RemoveAndReturn(i7);
				structureComponent8.buildComponent(componentStrongholdStairs25, this.components, random2);
			}

			this.updateBoundingBox();
			this.markAvailableHeight(world1, random2, 10);
		}
	}

}