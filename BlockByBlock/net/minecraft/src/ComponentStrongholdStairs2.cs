using System;
using System.Collections;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class ComponentStrongholdStairs2 : ComponentStrongholdStairs
	{
		public StructureStrongholdPieceWeight field_35038_a;
		public ComponentStrongholdPortalRoom portalRoom;
		public List<StructureComponent> field_35037_b = new();
        
		public ComponentStrongholdStairs2(int i1, RandomExtended random2, int i3, int i4) : base(0, random2, i3, i4)
		{
		}

		public override ChunkPosition Center
		{
			get
			{
				return this.portalRoom != null ? this.portalRoom.Center : base.Center;
			}
		}
	}

}