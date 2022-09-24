using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BlockMobSpawner : BlockContainer
	{
		protected internal BlockMobSpawner(int i1, int i2) : base(i1, i2, Material.rock)
		{
		}

		public override TileEntity BlockEntity
		{
			get
			{
				return new TileEntityMobSpawner();
			}
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return 0;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}
	}

}