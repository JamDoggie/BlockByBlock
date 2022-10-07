using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class BiomeDecorator
	{
		protected internal World currentWorld;
		protected internal RandomExtended randomGenerator;
		protected internal int chunk_X;
		protected internal int chunk_Z;
		protected internal BiomeGenBase biome;
		protected internal WorldGenerator clayGen = new WorldGenClay(4);
		protected internal WorldGenerator sandGen = new WorldGenSand(7, Block.sand.blockID);
		protected internal WorldGenerator gravelAsSandGen = new WorldGenSand(6, Block.gravel.blockID);
		protected internal WorldGenerator dirtGen = new WorldGenMinable(Block.dirt.blockID, 32);
		protected internal WorldGenerator gravelGen = new WorldGenMinable(Block.gravel.blockID, 32);
		protected internal WorldGenerator coalGen = new WorldGenMinable(Block.oreCoal.blockID, 16);
		protected internal WorldGenerator ironGen = new WorldGenMinable(Block.oreIron.blockID, 8);
		protected internal WorldGenerator goldGen = new WorldGenMinable(Block.oreGold.blockID, 8);
		protected internal WorldGenerator redstoneGen = new WorldGenMinable(Block.oreRedstone.blockID, 7);
		protected internal WorldGenerator diamondGen = new WorldGenMinable(Block.oreDiamond.blockID, 7);
		protected internal WorldGenerator lapisGen = new WorldGenMinable(Block.oreLapis.blockID, 6);
		protected internal WorldGenerator plantYellowGen = new WorldGenFlowers(Block.plantYellow.blockID);
		protected internal WorldGenerator plantRedGen = new WorldGenFlowers(Block.plantRed.blockID);
		protected internal WorldGenerator mushroomBrownGen = new WorldGenFlowers(Block.mushroomBrown.blockID);
		protected internal WorldGenerator mushroomRedGen = new WorldGenFlowers(Block.mushroomRed.blockID);
		protected internal WorldGenerator bigMushroomGen = new WorldGenBigMushroom();
		protected internal WorldGenerator reedGen = new WorldGenReed();
		protected internal WorldGenerator cactusGen = new WorldGenCactus();
		protected internal WorldGenerator waterlilyGen = new WorldGenWaterlily();
		protected internal int waterlilyPerChunk = 0;
		protected internal int treesPerChunk = 0;
		protected internal int flowersPerChunk = 2;
		protected internal int grassPerChunk = 1;
		protected internal int deadBushPerChunk = 0;
		protected internal int mushroomsPerChunk = 0;
		protected internal int reedsPerChunk = 0;
		protected internal int cactiPerChunk = 0;
		protected internal int sandPerChunk = 1;
		protected internal int sandPerChunk2 = 3;
		protected internal int clayPerChunk = 1;
		protected internal int bigMushroomsPerChunk = 0;
		public bool generateLakes = true;

		public BiomeDecorator(BiomeGenBase biomeGenBase1)
		{
			this.biome = biomeGenBase1;
		}

		public virtual void decorate(World world1, RandomExtended random2, int i3, int i4)
		{
			if (this.currentWorld != null)
			{
				throw new Exception("Already decorating!!");
			}
			else
			{
				this.currentWorld = world1;
				this.randomGenerator = random2;
				this.chunk_X = i3;
				this.chunk_Z = i4;
				this.decorate();
				this.currentWorld = null;
				this.randomGenerator = null;
			}
		}

		protected internal virtual void decorate()
		{
			this.generateOres();

			int i1;
			int i2;
			int i3;
			for (i1 = 0; i1 < this.sandPerChunk2; ++i1)
			{
				i2 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i3 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.sandGen.generate(this.currentWorld, this.randomGenerator, i2, this.currentWorld.getTopSolidOrLiquidBlock(i2, i3), i3);
			}

			for (i1 = 0; i1 < this.clayPerChunk; ++i1)
			{
				i2 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i3 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.clayGen.generate(this.currentWorld, this.randomGenerator, i2, this.currentWorld.getTopSolidOrLiquidBlock(i2, i3), i3);
			}

			for (i1 = 0; i1 < this.sandPerChunk; ++i1)
			{
				i2 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i3 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.sandGen.generate(this.currentWorld, this.randomGenerator, i2, this.currentWorld.getTopSolidOrLiquidBlock(i2, i3), i3);
			}

			i1 = this.treesPerChunk;
			if (this.randomGenerator.Next(10) == 0)
			{
				++i1;
			}

			int i4;
			for (i2 = 0; i2 < i1; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				WorldGenerator worldGenerator5 = this.biome.getRandomWorldGenForTrees(this.randomGenerator);
				worldGenerator5.setScale(1.0D, 1.0D, 1.0D);
				worldGenerator5.generate(this.currentWorld, this.randomGenerator, i3, this.currentWorld.getHeightValue(i3, i4), i4);
			}

			for (i2 = 0; i2 < this.bigMushroomsPerChunk; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.bigMushroomGen.generate(this.currentWorld, this.randomGenerator, i3, this.currentWorld.getHeightValue(i3, i4), i4);
			}

			int i7;
			for (i2 = 0; i2 < this.flowersPerChunk; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.randomGenerator.Next(128);
				i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.plantYellowGen.generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
				if (this.randomGenerator.Next(4) == 0)
				{
					i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					i4 = this.randomGenerator.Next(128);
					i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					this.plantRedGen.generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
				}
			}

			for (i2 = 0; i2 < this.grassPerChunk; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.randomGenerator.Next(128);
				i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				WorldGenerator worldGenerator6 = this.biome.func_48410_b(this.randomGenerator);
				worldGenerator6.generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
			}

			for (i2 = 0; i2 < this.deadBushPerChunk; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.randomGenerator.Next(128);
				i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				(new WorldGenDeadBush(Block.deadBush.blockID)).generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
			}

			for (i2 = 0; i2 < this.waterlilyPerChunk; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;

				for (i7 = this.randomGenerator.Next(128); i7 > 0 && this.currentWorld.getBlockId(i3, i7 - 1, i4) == 0; --i7)
				{
				}

				this.waterlilyGen.generate(this.currentWorld, this.randomGenerator, i3, i7, i4);
			}

			for (i2 = 0; i2 < this.mushroomsPerChunk; ++i2)
			{
				if (this.randomGenerator.Next(4) == 0)
				{
					i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					i7 = this.currentWorld.getHeightValue(i3, i4);
					this.mushroomBrownGen.generate(this.currentWorld, this.randomGenerator, i3, i7, i4);
				}

				if (this.randomGenerator.Next(8) == 0)
				{
					i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					i7 = this.randomGenerator.Next(128);
					this.mushroomRedGen.generate(this.currentWorld, this.randomGenerator, i3, i7, i4);
				}
			}

			if (this.randomGenerator.Next(4) == 0)
			{
				i2 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i3 = this.randomGenerator.Next(128);
				i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.mushroomBrownGen.generate(this.currentWorld, this.randomGenerator, i2, i3, i4);
			}

			if (this.randomGenerator.Next(8) == 0)
			{
				i2 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i3 = this.randomGenerator.Next(128);
				i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.mushroomRedGen.generate(this.currentWorld, this.randomGenerator, i2, i3, i4);
			}

			for (i2 = 0; i2 < this.reedsPerChunk; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				i7 = this.randomGenerator.Next(128);
				this.reedGen.generate(this.currentWorld, this.randomGenerator, i3, i7, i4);
			}

			for (i2 = 0; i2 < 10; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.randomGenerator.Next(128);
				i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.reedGen.generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
			}

			if (this.randomGenerator.Next(32) == 0)
			{
				i2 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i3 = this.randomGenerator.Next(128);
				i4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				(new WorldGenPumpkin()).generate(this.currentWorld, this.randomGenerator, i2, i3, i4);
			}

			for (i2 = 0; i2 < this.cactiPerChunk; ++i2)
			{
				i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				i4 = this.randomGenerator.Next(128);
				i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.cactusGen.generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
			}

			if (this.generateLakes)
			{
				for (i2 = 0; i2 < 50; ++i2)
				{
					i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					i4 = this.randomGenerator.Next(this.randomGenerator.Next(120) + 8);
					i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					(new WorldGenLiquids(Block.waterMoving.blockID)).generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
				}

				for (i2 = 0; i2 < 20; ++i2)
				{
					i3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					i4 = this.randomGenerator.Next(this.randomGenerator.Next(this.randomGenerator.Next(112) + 8) + 8);
					i7 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					(new WorldGenLiquids(Block.lavaMoving.blockID)).generate(this.currentWorld, this.randomGenerator, i3, i4, i7);
				}
			}

		}

		protected internal virtual void genStandardOre1(int i1, WorldGenerator worldGenerator2, int i3, int i4)
		{
			for (int i5 = 0; i5 < i1; ++i5)
			{
				int i6 = this.chunk_X + this.randomGenerator.Next(16);
				int i7 = this.randomGenerator.Next(i4 - i3) + i3;
				int i8 = this.chunk_Z + this.randomGenerator.Next(16);
				worldGenerator2.generate(this.currentWorld, this.randomGenerator, i6, i7, i8);
			}

		}

		protected internal virtual void genStandardOre2(int i1, WorldGenerator worldGenerator2, int i3, int i4)
		{
			for (int i5 = 0; i5 < i1; ++i5)
			{
				int i6 = this.chunk_X + this.randomGenerator.Next(16);
				int i7 = this.randomGenerator.Next(i4) + this.randomGenerator.Next(i4) + (i3 - i4);
				int i8 = this.chunk_Z + this.randomGenerator.Next(16);
				worldGenerator2.generate(this.currentWorld, this.randomGenerator, i6, i7, i8);
			}

		}

		protected internal virtual void generateOres()
		{
			this.genStandardOre1(20, this.dirtGen, 0, 128);
			this.genStandardOre1(10, this.gravelGen, 0, 128);
			this.genStandardOre1(20, this.coalGen, 0, 128);
			this.genStandardOre1(20, this.ironGen, 0, 64);
			this.genStandardOre1(2, this.goldGen, 0, 32);
			this.genStandardOre1(8, this.redstoneGen, 0, 16);
			this.genStandardOre1(1, this.diamondGen, 0, 16);
			this.genStandardOre2(1, this.lapisGen, 16, 16);
		}
	}

}