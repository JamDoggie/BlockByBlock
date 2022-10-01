using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class StructureMineshaftStart : StructureStart
	{
		public StructureMineshaftStart(World world1, RandomExtended random2, int i3, int i4)
		{
			ComponentMineshaftRoom componentMineshaftRoom5 = new ComponentMineshaftRoom(0, random2, (i3 << 4) + 2, (i4 << 4) + 2);
			this.components.Add(componentMineshaftRoom5);
			componentMineshaftRoom5.buildComponent(componentMineshaftRoom5, this.components, random2);
			this.updateBoundingBox();
			this.markAvailableHeight(world1, random2, 10);
		}
	}

}