using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EmptyChunk : Chunk
	{
		public EmptyChunk(World world1, int i2, int i3) : base(world1, i2, i3)
		{
		}

		public override bool isAtLocation(int i1, int i2)
		{
			return i1 == this.xPosition && i2 == this.zPosition;
		}

		public override int getHeightValue(int i1, int i2)
		{
			return 0;
		}

		public override void generateHeightMap()
		{
		}

		public override void generateSkylightMap()
		{
		}

		public override void func_4143_d()
		{
		}

		public override int getBlockID(int i1, int i2, int i3)
		{
			return 0;
		}

		public override int getBlockLightOpacity(int i1, int i2, int i3)
		{
			return 255;
		}

		public override bool setBlockIDWithMetadata(int i1, int i2, int i3, int i4, int i5)
		{
			return true;
		}

		public override bool setBlockID(int i1, int i2, int i3, int i4)
		{
			return true;
		}

		public override int getBlockMetadata(int i1, int i2, int i3)
		{
			return 0;
		}

		public override bool setBlockMetadata(int i1, int i2, int i3, int i4)
		{
			return false;
		}

		public override int getSavedLightValue(EnumSkyBlock enumSkyBlock1, int i2, int i3, int i4)
		{
			return 0;
		}

		public override void setLightValue(EnumSkyBlock enumSkyBlock1, int i2, int i3, int i4, int i5)
		{
		}

		public override int getBlockLightValue(int i1, int i2, int i3, int i4)
		{
			return 0;
		}

		public override void addEntity(Entity entity1)
		{
		}

		public override void removeEntity(Entity entity1)
		{
		}

		public override void removeEntityAtIndex(Entity entity1, int i2)
		{
		}

		public override bool canBlockSeeTheSky(int i1, int i2, int i3)
		{
			return false;
		}

		public override TileEntity getChunkBlockTileEntity(int i1, int i2, int i3)
		{
			return null;
		}

		public override void addTileEntity(TileEntity tileEntity1)
		{
		}

		public override void setChunkBlockTileEntity(int i1, int i2, int i3, TileEntity tileEntity4)
		{
		}

		public override void removeChunkBlockTileEntity(int i1, int i2, int i3)
		{
		}

		public override void onChunkLoad()
		{
		}

		public override void onChunkUnload()
		{
		}

		public override void setChunkModified()
		{
		}

		public override void getEntitiesWithinAABBForEntity(Entity entity1, AxisAlignedBB axisAlignedBB2, System.Collections.IList list3)
		{
		}

		public override void getEntitiesOfTypeWithinAAAB(Type class1, AxisAlignedBB axisAlignedBB2, System.Collections.IList list3)
		{
		}

		public override bool needsSaving(bool z1)
		{
			return false;
		}

		public override RandomExtended getRandomWithSeed(long j1)
		{
			return new RandomExtended(this.worldObj.Seed + (long)(this.xPosition * this.xPosition * 4987142) + (long)(this.xPosition * 5947611) + (long)(this.zPosition * this.zPosition) * 4392871L + (long)(this.zPosition * 389711) ^ j1);
		}

		public override bool Empty
		{
			get
			{
				return true;
			}
		}

		public override bool getAreLevelsEmpty(int i1, int i2)
		{
			return true;
		}
	}

}