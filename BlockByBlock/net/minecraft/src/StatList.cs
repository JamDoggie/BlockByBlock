using System.Collections;
using System.Collections.Generic;

namespace net.minecraft.src
{

	public class StatList
	{
		protected internal static System.Collections.IDictionary oneShotStats = new Hashtable();
		public static System.Collections.IList allStats = new ArrayList();
		public static System.Collections.IList generalStats = new ArrayList();
		public static System.Collections.IList itemStats = new ArrayList();
		public static System.Collections.IList objectMineStats = new ArrayList();
		public static StatBase startGameStat = (new StatBasic(1000, "stat.startGame")).initIndependentStat().registerStat();
		public static StatBase createWorldStat = (new StatBasic(1001, "stat.createWorld")).initIndependentStat().registerStat();
		public static StatBase loadWorldStat = (new StatBasic(1002, "stat.loadWorld")).initIndependentStat().registerStat();
		public static StatBase joinMultiplayerStat = (new StatBasic(1003, "stat.joinMultiplayer")).initIndependentStat().registerStat();
		public static StatBase leaveGameStat = (new StatBasic(1004, "stat.leaveGame")).initIndependentStat().registerStat();
		public static StatBase minutesPlayedStat = (new StatBasic(1100, "stat.playOneMinute", StatBase.timeStatType)).initIndependentStat().registerStat();
		public static StatBase distanceWalkedStat = (new StatBasic(2000, "stat.walkOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceSwumStat = (new StatBasic(2001, "stat.swimOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceFallenStat = (new StatBasic(2002, "stat.fallOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceClimbedStat = (new StatBasic(2003, "stat.climbOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceFlownStat = (new StatBasic(2004, "stat.flyOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceDoveStat = (new StatBasic(2005, "stat.diveOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceByMinecartStat = (new StatBasic(2006, "stat.minecartOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceByBoatStat = (new StatBasic(2007, "stat.boatOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase distanceByPigStat = (new StatBasic(2008, "stat.pigOneCm", StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase jumpStat = (new StatBasic(2010, "stat.jump")).initIndependentStat().registerStat();
		public static StatBase dropStat = (new StatBasic(2011, "stat.drop")).initIndependentStat().registerStat();
		public static StatBase damageDealtStat = (new StatBasic(2020, "stat.damageDealt")).registerStat();
		public static StatBase damageTakenStat = (new StatBasic(2021, "stat.damageTaken")).registerStat();
		public static StatBase deathsStat = (new StatBasic(2022, "stat.deaths")).registerStat();
		public static StatBase mobKillsStat = (new StatBasic(2023, "stat.mobKills")).registerStat();
		public static StatBase playerKillsStat = (new StatBasic(2024, "stat.playerKills")).registerStat();
		public static StatBase fishCaughtStat = (new StatBasic(2025, "stat.fishCaught")).registerStat();
		public static StatBase[] mineBlockStatArray = initMinableStats("stat.mineBlock", 16777216);
		public static StatBase[] objectCraftStats;
		public static StatBase[] objectUseStats;
		public static StatBase[] objectBreakStats;
		private static bool blockStatsInitialized;
		private static bool itemStatsInitialized;

		public static void func_27360_a()
		{
		}

		public static void initBreakableStats()
		{
			objectUseStats = initUsableStats(objectUseStats, "stat.useItem", 16908288, 0, 256);
			objectBreakStats = initBreakStats(objectBreakStats, "stat.breakItem", 16973824, 0, 256);
			blockStatsInitialized = true;
			initCraftableStats();
		}

		public static void initStats()
		{
			objectUseStats = initUsableStats(objectUseStats, "stat.useItem", 16908288, 256, 32000);
			objectBreakStats = initBreakStats(objectBreakStats, "stat.breakItem", 16973824, 256, 32000);
			itemStatsInitialized = true;
			initCraftableStats();
		}

		public static void initCraftableStats()
		{
			if (blockStatsInitialized && itemStatsInitialized)
			{
				HashSet<object> hashSet0 = new HashSet<object>();
				System.Collections.IEnumerator iterator1 = CraftingManager.Instance.RecipeList.GetEnumerator();

				while (iterator1.MoveNext())
				{
					IRecipe iRecipe2 = (IRecipe)iterator1.Current;
					hashSet0.Add(iRecipe2.RecipeOutput.itemID);
				}

				iterator1 = FurnaceRecipes.smelting().SmeltingList.Values.GetEnumerator();

				while (iterator1.MoveNext())
				{
					ItemStack itemStack4 = (ItemStack)iterator1.Current;
					hashSet0.Add(itemStack4.itemID);
				}

				objectCraftStats = new StatBase[32000];
				iterator1 = hashSet0.GetEnumerator();

				while (iterator1.MoveNext())
				{
					int? integer5 = (int?)iterator1.Current;
					if (Item.itemsList[integer5.Value] != null)
					{
						string string3 = StatCollector.translateToLocalFormatted("stat.craftItem", new object[]{Item.itemsList[integer5.Value].StatName});
						objectCraftStats[integer5.Value] = (new StatCrafting(16842752 + integer5.Value, string3, integer5.Value)).registerStat();
					}
				}

				replaceAllSimilarBlocks(objectCraftStats);
			}
		}

		private static StatBase[] initMinableStats(string string0, int i1)
		{
			StatBase[] statBase2 = new StatBase[256];

			for (int i3 = 0; i3 < 256; ++i3)
			{
				if (Block.blocksList[i3] != null && Block.blocksList[i3].EnableStats)
				{
					string string4 = StatCollector.translateToLocalFormatted(string0, new object[]{Block.blocksList[i3].translateBlockName()});
					statBase2[i3] = (new StatCrafting(i1 + i3, string4, i3)).registerStat();
					objectMineStats.Add((StatCrafting)statBase2[i3]);
				}
			}

			replaceAllSimilarBlocks(statBase2);
			return statBase2;
		}

		private static StatBase[] initUsableStats(StatBase[] statBase0, string string1, int i2, int i3, int i4)
		{
			if (statBase0 == null)
			{
				statBase0 = new StatBase[32000];
			}

			for (int i5 = i3; i5 < i4; ++i5)
			{
				if (Item.itemsList[i5] != null)
				{
					string string6 = StatCollector.translateToLocalFormatted(string1, new object[]{Item.itemsList[i5].StatName});
					statBase0[i5] = (new StatCrafting(i2 + i5, string6, i5)).registerStat();
					if (i5 >= 256)
					{
						itemStats.Add((StatCrafting)statBase0[i5]);
					}
				}
			}

			replaceAllSimilarBlocks(statBase0);
			return statBase0;
		}

		private static StatBase[] initBreakStats(StatBase[] statBase0, string string1, int i2, int i3, int i4)
		{
			if (statBase0 == null)
			{
				statBase0 = new StatBase[32000];
			}

			for (int i5 = i3; i5 < i4; ++i5)
			{
				if (Item.itemsList[i5] != null && Item.itemsList[i5].Damageable)
				{
					string string6 = StatCollector.translateToLocalFormatted(string1, new object[]{Item.itemsList[i5].StatName});
					statBase0[i5] = (new StatCrafting(i2 + i5, string6, i5)).registerStat();
				}
			}

			replaceAllSimilarBlocks(statBase0);
			return statBase0;
		}

		private static void replaceAllSimilarBlocks(StatBase[] statBase0)
		{
			replaceSimilarBlocks(statBase0, Block.waterStill.blockID, Block.waterMoving.blockID);
			replaceSimilarBlocks(statBase0, Block.lavaStill.blockID, Block.lavaStill.blockID);
			replaceSimilarBlocks(statBase0, Block.pumpkinLantern.blockID, Block.pumpkin.blockID);
			replaceSimilarBlocks(statBase0, Block.stoneOvenActive.blockID, Block.stoneOvenIdle.blockID);
			replaceSimilarBlocks(statBase0, Block.oreRedstoneGlowing.blockID, Block.oreRedstone.blockID);
			replaceSimilarBlocks(statBase0, Block.redstoneRepeaterActive.blockID, Block.redstoneRepeaterIdle.blockID);
			replaceSimilarBlocks(statBase0, Block.torchRedstoneActive.blockID, Block.torchRedstoneIdle.blockID);
			replaceSimilarBlocks(statBase0, Block.mushroomRed.blockID, Block.mushroomBrown.blockID);
			replaceSimilarBlocks(statBase0, Block.stairDouble.blockID, Block.stairSingle.blockID);
			replaceSimilarBlocks(statBase0, Block.grass.blockID, Block.dirt.blockID);
			replaceSimilarBlocks(statBase0, Block.tilledField.blockID, Block.dirt.blockID);
		}

		private static void replaceSimilarBlocks(StatBase[] statBase0, int i1, int i2)
		{
			if (statBase0[i1] != null && statBase0[i2] == null)
			{
				statBase0[i2] = statBase0[i1];
			}
			else
			{
				allStats.Remove(statBase0[i1]);
				objectMineStats.Remove(statBase0[i1]);
				generalStats.Remove(statBase0[i1]);
				statBase0[i1] = statBase0[i2];
			}
		}

		public static StatBase getOneShotStat(int i0)
		{
			return (StatBase)oneShotStats[i0];
		}

		static StatList()
		{
			AchievementList.init();
			blockStatsInitialized = false;
			itemStatsInitialized = false;
		}
	}

}