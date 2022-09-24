namespace net.minecraft.src
{
	public sealed class WorldSettings
	{
		private readonly long seed;
		private readonly int gameType;
		private readonly bool mapFeaturesEnabled;
		private readonly bool hardcoreEnabled;
		private readonly WorldType terrainType;

		public WorldSettings(long j1, int i3, bool z4, bool z5, WorldType worldType6)
		{
			this.seed = j1;
			this.gameType = i3;
			this.mapFeaturesEnabled = z4;
			this.hardcoreEnabled = z5;
			this.terrainType = worldType6;
		}

		public long Seed
		{
			get
			{
				return this.seed;
			}
		}

		public int GameType
		{
			get
			{
				return this.gameType;
			}
		}

		public bool HardcoreEnabled
		{
			get
			{
				return this.hardcoreEnabled;
			}
		}

		public bool MapFeaturesEnabled
		{
			get
			{
				return this.mapFeaturesEnabled;
			}
		}

		public WorldType TerrainType
		{
			get
			{
				return this.terrainType;
			}
		}
	}

}