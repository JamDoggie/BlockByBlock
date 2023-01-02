using BlockByBlock.helpers;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;
using System;
using System.Collections;
using System.Drawing;

namespace net.minecraft.src
{

    public abstract class BiomeGenBase
	{
		public static readonly BiomeGenBase[] biomeList = new BiomeGenBase[256];
		public static readonly BiomeGenBase ocean = (new BiomeGenOcean(0)).setColor(112).setBiomeName("Ocean").setMinMaxHeight(-1.0F, 0.4F);
		public static readonly BiomeGenBase plains = (new BiomeGenPlains(1)).setColor(9286496).setBiomeName("Plains").setTemperatureRainfall(0.8F, 0.4F);
		public static readonly BiomeGenBase desert = (new BiomeGenDesert(2)).setColor(16421912).setBiomeName("Desert").setDisableRain().setTemperatureRainfall(2.0F, 0.0F).setMinMaxHeight(0.1F, 0.2F);
		public static readonly BiomeGenBase extremeHills = (new BiomeGenHills(3)).setColor(6316128).setBiomeName("Extreme Hills").setMinMaxHeight(0.2F, 1.3F).setTemperatureRainfall(0.2F, 0.3F);
		public static readonly BiomeGenBase forest = (new BiomeGenForest(4)).setColor(353825).setBiomeName("Forest").func_4124_a(5159473).setTemperatureRainfall(0.7F, 0.8F);
		public static readonly BiomeGenBase taiga = (new BiomeGenTaiga(5)).setColor(747097).setBiomeName("Taiga").func_4124_a(5159473).func_50086_b().setTemperatureRainfall(0.05F, 0.8F).setMinMaxHeight(0.1F, 0.4F);
		public static readonly BiomeGenBase swampland = (new BiomeGenSwamp(6)).setColor(522674).setBiomeName("Swampland").func_4124_a(9154376).setMinMaxHeight(-0.2F, 0.1F).setTemperatureRainfall(0.8F, 0.9F);
		public static readonly BiomeGenBase river = (new BiomeGenRiver(7)).setColor(255).setBiomeName("River").setMinMaxHeight(-0.5F, 0.0F);
		public static readonly BiomeGenBase hell = (new BiomeGenHell(8)).setColor(16711680).setBiomeName("Hell").setDisableRain().setTemperatureRainfall(2.0F, 0.0F);
		public static readonly BiomeGenBase sky = (new BiomeGenEnd(9)).setColor(8421631).setBiomeName("Sky").setDisableRain();
		public static readonly BiomeGenBase frozenOcean = (new BiomeGenOcean(10)).setColor(9474208).setBiomeName("FrozenOcean").func_50086_b().setMinMaxHeight(-1.0F, 0.5F).setTemperatureRainfall(0.0F, 0.5F);
		public static readonly BiomeGenBase frozenRiver = (new BiomeGenRiver(11)).setColor(10526975).setBiomeName("FrozenRiver").func_50086_b().setMinMaxHeight(-0.5F, 0.0F).setTemperatureRainfall(0.0F, 0.5F);
		public static readonly BiomeGenBase icePlains = (new BiomeGenSnow(12)).setColor(0xFFFFFF).setBiomeName("Ice Plains").func_50086_b().setTemperatureRainfall(0.0F, 0.5F);
		public static readonly BiomeGenBase iceMountains = (new BiomeGenSnow(13)).setColor(10526880).setBiomeName("Ice Mountains").func_50086_b().setMinMaxHeight(0.2F, 1.2F).setTemperatureRainfall(0.0F, 0.5F);
		public static readonly BiomeGenBase mushroomIsland = (new BiomeGenMushroomIsland(14)).setColor(16711935).setBiomeName("MushroomIsland").setTemperatureRainfall(0.9F, 1.0F).setMinMaxHeight(0.2F, 1.0F);
		public static readonly BiomeGenBase mushroomIslandShore = (new BiomeGenMushroomIsland(15)).setColor(10486015).setBiomeName("MushroomIslandShore").setTemperatureRainfall(0.9F, 1.0F).setMinMaxHeight(-1.0F, 0.1F);
		public static readonly BiomeGenBase beach = (new BiomeGenBeach(16)).setColor(16440917).setBiomeName("Beach").setTemperatureRainfall(0.8F, 0.4F).setMinMaxHeight(0.0F, 0.1F);
		public static readonly BiomeGenBase desertHills = (new BiomeGenDesert(17)).setColor(13786898).setBiomeName("DesertHills").setDisableRain().setTemperatureRainfall(2.0F, 0.0F).setMinMaxHeight(0.2F, 0.7F);
		public static readonly BiomeGenBase forestHills = (new BiomeGenForest(18)).setColor(2250012).setBiomeName("ForestHills").func_4124_a(5159473).setTemperatureRainfall(0.7F, 0.8F).setMinMaxHeight(0.2F, 0.6F);
		public static readonly BiomeGenBase taigaHills = (new BiomeGenTaiga(19)).setColor(1456435).setBiomeName("TaigaHills").func_50086_b().func_4124_a(5159473).setTemperatureRainfall(0.05F, 0.8F).setMinMaxHeight(0.2F, 0.7F);
		public static readonly BiomeGenBase extremeHillsEdge = (new BiomeGenHills(20)).setColor(7501978).setBiomeName("Extreme Hills Edge").setMinMaxHeight(0.2F, 0.8F).setTemperatureRainfall(0.2F, 0.3F);
		public static readonly BiomeGenBase jungle = (new BiomeGenJungle(21)).setColor(5470985).setBiomeName("Jungle").func_4124_a(5470985).setTemperatureRainfall(1.2F, 0.9F).setMinMaxHeight(0.2F, 0.4F);
		public static readonly BiomeGenBase jungleHills = (new BiomeGenJungle(22)).setColor(2900485).setBiomeName("JungleHills").func_4124_a(5470985).setTemperatureRainfall(1.2F, 0.9F).setMinMaxHeight(1.8F, 0.2F);
		public string biomeName;
		public int color;
		public sbyte topBlock = (sbyte)Block.grass.blockID;
		public sbyte fillerBlock = (sbyte)Block.dirt.blockID;
		public int field_6502_q = 5169201;
		public float minHeight = 0.1F;
		public float maxHeight = 0.3F;
		public float temperature = 0.5F;
		public float rainfall = 0.5F;
		public int waterColorMultiplier = 0xFFFFFF;
		public BiomeDecorator biomeDecorator;
		protected internal System.Collections.IList spawnableMonsterList = new ArrayList();
		protected internal System.Collections.IList spawnableCreatureList = new ArrayList();
		protected internal System.Collections.IList spawnableWaterCreatureList = new ArrayList();
		private bool enableSnow;
		private bool enableRain = true;
		public readonly int biomeID;
		protected internal WorldGenTrees worldGenTrees = new WorldGenTrees(false);
		protected internal WorldGenBigTree worldGenBigTree = new WorldGenBigTree(false);
		protected internal WorldGenForest worldGenForest = new WorldGenForest(false);
		protected internal WorldGenSwamp worldGenSwamp = new WorldGenSwamp();

		protected internal BiomeGenBase(int i1)
		{
			this.biomeID = i1;
			biomeList[i1] = this;
			this.biomeDecorator = this.createBiomeDecorator();
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntitySheep), 12, 4, 4));
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntityPig), 10, 4, 4));
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntityChicken), 10, 4, 4));
			this.spawnableCreatureList.Add(new SpawnListEntry(typeof(EntityCow), 8, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntitySpider), 10, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityZombie), 10, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntitySkeleton), 10, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityCreeper), 10, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntitySlime), 10, 4, 4));
			this.spawnableMonsterList.Add(new SpawnListEntry(typeof(EntityEnderman), 1, 1, 4));
			this.spawnableWaterCreatureList.Add(new SpawnListEntry(typeof(EntitySquid), 10, 4, 4));
		}

		protected internal virtual BiomeDecorator createBiomeDecorator()
		{
			return new BiomeDecorator(this);
		}

		private BiomeGenBase setTemperatureRainfall(float f1, float f2)
		{
			if (f1 > 0.1F && f1 < 0.2F)
			{
				throw new System.ArgumentException("Please avoid temperatures in the range 0.1 - 0.2 because of snow");
			}
			else
			{
				this.temperature = f1;
				this.rainfall = f2;
				return this;
			}
		}

		private BiomeGenBase setMinMaxHeight(float f1, float f2)
		{
			this.minHeight = f1;
			this.maxHeight = f2;
			return this;
		}

		private BiomeGenBase setDisableRain()
		{
			this.enableRain = false;
			return this;
		}

		public virtual WorldGenerator getRandomWorldGenForTrees(RandomExtended random1)
		{
			return (WorldGenerator)(random1.Next(10) == 0 ? this.worldGenBigTree : this.worldGenTrees);
		}

		public virtual WorldGenerator func_48410_b(RandomExtended random1)
		{
			return new WorldGenTallGrass(Block.tallGrass.blockID, 1);
		}

		protected internal virtual BiomeGenBase func_50086_b()
		{
			this.enableSnow = true;
			return this;
		}

		protected internal virtual BiomeGenBase setBiomeName(string string1)
		{
			this.biomeName = string1;
			return this;
		}

		protected internal virtual BiomeGenBase func_4124_a(int i1)
		{
			this.field_6502_q = i1;
			return this;
		}

		protected internal virtual BiomeGenBase setColor(int i1)
		{
			this.color = i1;
			return this;
		}

		public virtual int getSkyColorByTemp(float f1)
		{
			f1 /= 3.0F;
			if (f1 < -1.0F)
			{
				f1 = -1.0F;
			}

			if (f1 > 1.0F)
			{
				f1 = 1.0F;
			}

			return (ColorHelpers.ColorFromHSV(0.62222224F - f1 * 0.05F, 0.5F + f1 * 0.1F, 1.0F)).ToArgb();
		}

		public virtual IList? getSpawnableList(EnumCreatureType enumCreatureType1)
		{
			return enumCreatureType1 == EnumCreatureType.monster ? this.spawnableMonsterList : (enumCreatureType1 == EnumCreatureType.creature ? this.spawnableCreatureList : (enumCreatureType1 == EnumCreatureType.waterCreature ? this.spawnableWaterCreatureList : null));
		}

		public virtual bool EnableSnow
		{
			get
			{
				return this.enableSnow;
			}
		}

		public virtual bool canSpawnLightningBolt()
		{
			return this.enableSnow ? false : this.enableRain;
		}

		public virtual bool HighHumidity
		{
			get
			{
				return this.rainfall > 0.85F;
			}
		}

		public virtual float SpawningChance
		{
			get
			{
				return 0.1F;
			}
		}

		public int IntRainfall
		{
			get
			{
				return (int)(this.rainfall * 65536.0F);
			}
		}

		public int IntTemperature
		{
			get
			{
				return (int)(this.temperature * 65536.0F);
			}
		}

		public float FloatRainfall
		{
			get
			{
				return this.rainfall;
			}
		}

		public float FloatTemperature
		{
			get
			{
				return this.temperature;
			}
		}

		public virtual void decorate(World world1, RandomExtended random2, int i3, int i4)
		{
			this.biomeDecorator.decorate(world1, random2, i3, i4);
		}

		public virtual int BiomeGrassColor
		{
			get
			{
				double d1 = (double)MathHelper.clamp_float(this.FloatTemperature, 0.0F, 1.0F);
				double d3 = (double)MathHelper.clamp_float(this.FloatRainfall, 0.0F, 1.0F);
				return ColorizerGrass.getGrassColor(d1, d3);
			}
		}

		public virtual int BiomeFoliageColor
		{
			get
			{
				double d1 = (double)MathHelper.clamp_float(this.FloatTemperature, 0.0F, 1.0F);
				double d3 = (double)MathHelper.clamp_float(this.FloatRainfall, 0.0F, 1.0F);
				return ColorizerFoliage.getFoliageColor(d1, d3);
			}
		}
	}

}