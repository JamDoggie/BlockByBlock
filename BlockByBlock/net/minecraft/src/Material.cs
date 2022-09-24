namespace net.minecraft.src
{
	public class Material
	{
		public static readonly Material air = new MaterialTransparent(MapColor.airColor);
		public static readonly Material grass = new Material(MapColor.grassColor);
		public static readonly Material ground = new Material(MapColor.dirtColor);
		public static readonly Material wood = (new Material(MapColor.woodColor)).setBurning();
		public static readonly Material rock = (new Material(MapColor.stoneColor)).setNoHarvest();
		public static readonly Material iron = (new Material(MapColor.ironColor)).setNoHarvest();
		public static readonly Material water = (new MaterialLiquid(MapColor.waterColor)).setNoPushMobility();
		public static readonly Material lava = (new MaterialLiquid(MapColor.tntColor)).setNoPushMobility();
		public static readonly Material leaves = (new Material(MapColor.foliageColor)).setBurning().setTranslucent().setNoPushMobility();
		public static readonly Material plants = (new MaterialLogic(MapColor.foliageColor)).setNoPushMobility();
		public static readonly Material vine = (new MaterialLogic(MapColor.foliageColor)).setBurning().setNoPushMobility().setGroundCover();
		public static readonly Material sponge = new Material(MapColor.clothColor);
		public static readonly Material cloth = (new Material(MapColor.clothColor)).setBurning();
		public static readonly Material fire = (new MaterialTransparent(MapColor.airColor)).setNoPushMobility();
		public static readonly Material sand = new Material(MapColor.sandColor);
		public static readonly Material circuits = (new MaterialLogic(MapColor.airColor)).setNoPushMobility();
		public static readonly Material glass = (new Material(MapColor.airColor)).setTranslucent();
		public static readonly Material redstoneLight = new Material(MapColor.airColor);
		public static readonly Material tnt = (new Material(MapColor.tntColor)).setBurning().setTranslucent();
		public static readonly Material unused = (new Material(MapColor.foliageColor)).setNoPushMobility();
		public static readonly Material ice = (new Material(MapColor.iceColor)).setTranslucent();
		public static readonly Material snow = (new MaterialLogic(MapColor.snowColor)).setGroundCover().setTranslucent().setNoHarvest().setNoPushMobility();
		public static readonly Material craftedSnow = (new Material(MapColor.snowColor)).setNoHarvest();
		public static readonly Material cactus = (new Material(MapColor.foliageColor)).setTranslucent().setNoPushMobility();
		public static readonly Material clay = new Material(MapColor.clayColor);
		public static readonly Material pumpkin = (new Material(MapColor.foliageColor)).setNoPushMobility();
		public static readonly Material dragonEgg = (new Material(MapColor.foliageColor)).setNoPushMobility();
		public static readonly Material portal = (new MaterialPortal(MapColor.airColor)).setImmovableMobility();
		public static readonly Material cake = (new Material(MapColor.airColor)).setNoPushMobility();
		public static readonly Material web = (new MaterialWeb(MapColor.clothColor)).setNoHarvest().setNoPushMobility();
		public static readonly Material piston = (new Material(MapColor.stoneColor)).setImmovableMobility();
		private bool canBurn;
		private bool groundCover;
		private bool isTranslucent;
		public readonly MapColor materialMapColor;
		private bool canHarvest = true;
		private int mobilityFlag;

		public Material(MapColor mapColor1)
		{
			this.materialMapColor = mapColor1;
		}

		public virtual bool Liquid
		{
			get
			{
				return false;
			}
		}

		public virtual bool Solid
		{
			get
			{
				return true;
			}
		}

		public virtual bool CanBlockGrass
		{
			get
			{
				return true;
			}
		}

		public virtual bool blocksMovement()
		{
			return true;
		}

		private Material setTranslucent()
		{
			this.isTranslucent = true;
			return this;
		}

		protected internal virtual Material setNoHarvest()
		{
			this.canHarvest = false;
			return this;
		}

		protected internal virtual Material setBurning()
		{
			this.canBurn = true;
			return this;
		}

		public virtual bool CanBurn
		{
			get
			{
				return this.canBurn;
			}
		}

		public virtual Material setGroundCover()
		{
			this.groundCover = true;
			return this;
		}

		public virtual bool GroundCover
		{
			get
			{
				return this.groundCover;
			}
		}

		public virtual bool Opaque
		{
			get
			{
				return this.isTranslucent ? false : this.blocksMovement();
			}
		}

		public virtual bool Harvestable
		{
			get
			{
				return this.canHarvest;
			}
		}

		public virtual int MaterialMobility
		{
			get
			{
				return this.mobilityFlag;
			}
		}

		protected internal virtual Material setNoPushMobility()
		{
			this.mobilityFlag = 1;
			return this;
		}

		protected internal virtual Material setImmovableMobility()
		{
			this.mobilityFlag = 2;
			return this;
		}
	}

}