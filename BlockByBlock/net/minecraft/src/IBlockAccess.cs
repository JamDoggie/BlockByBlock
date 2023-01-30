namespace net.minecraft.src
{
	public interface IBlockAccess
	{
		int getBlockId(int i1, int i2, int i3);

		TileEntity getBlockTileEntity(int i1, int i2, int i3);

		int GetLightBrightnessForSkyBlocks(int x, int y, int z, int minimumBlockLight);

		float getBrightness(int i1, int i2, int i3, int i4);

		float getLightBrightness(int i1, int i2, int i3);

		int getBlockMetadata(int i1, int i2, int i3);

		Material getBlockMaterial(int i1, int i2, int i3);

		bool isBlockOpaqueCube(int i1, int i2, int i3);

		bool isBlockNormalCube(int i1, int i2, int i3);

		bool isAirBlock(int i1, int i2, int i3);

		BiomeGenBase getBiomeGenForCoords(int i1, int i2);

		int Height {get;}

		bool getChunksEmpty_IDK();
	}

}