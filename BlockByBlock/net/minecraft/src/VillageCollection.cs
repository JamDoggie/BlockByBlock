using BlockByBlock.helpers;
using System;
using System.Collections;

namespace net.minecraft.src
{

	public class VillageCollection
	{
		private World worldObj;
		private readonly System.Collections.IList villagerPositionsList = new ArrayList();
		private readonly System.Collections.IList newDoors = new ArrayList();
		private readonly System.Collections.IList villageList = new ArrayList();
		private int tickCounter = 0;

		public VillageCollection(World world1)
		{
			this.worldObj = world1;
		}

		public virtual void addVillagerPosition(int i1, int i2, int i3)
		{
			if (this.villagerPositionsList.Count <= 64)
			{
				if (!this.isVillagerPositionPresent(i1, i2, i3))
				{
					this.villagerPositionsList.Add(new ChunkCoordinates(i1, i2, i3));
				}

			}
		}

		public virtual void tick()
		{
			++this.tickCounter;
			System.Collections.IEnumerator iterator1 = this.villageList.GetEnumerator();

			while (iterator1.MoveNext())
			{
				Village village2 = (Village)iterator1.Current;
				village2.tick(this.tickCounter);
			}

			this.removeAnnihilatedVillages();
			this.dropOldestVillagerPosition();
			this.addNewDoorsToVillageOrCreateVillage();
		}

		private void removeAnnihilatedVillages()
		{
            for (int i = villageList.Count - 1; i >= 0; i--)
            {
                if (((Village)villageList[i]).Annihilated)
                    villageList.RemoveAt(i);
            }
        }

		public virtual System.Collections.IList func_48554_b()
		{
			return this.villageList;
		}

		public virtual Village findNearestVillage(int i1, int i2, int i3, int i4)
		{
			Village village5 = null;
			float f6 = float.MaxValue;
			System.Collections.IEnumerator iterator7 = this.villageList.GetEnumerator();

			while (iterator7.MoveNext())
			{
				Village village8 = (Village)iterator7.Current;
				float f9 = village8.Center.getDistanceSquared(i1, i2, i3);
				if (f9 < f6)
				{
					int i10 = i4 + village8.VillageRadius;
					if (f9 <= (float)(i10 * i10))
					{
						village5 = village8;
						f6 = f9;
					}
				}
			}

			return village5;
		}

		private void dropOldestVillagerPosition()
		{
			if (this.villagerPositionsList.Count > 0)
			{
				this.addUnassignedWoodenDoorsAroundToNewDoorsList((ChunkCoordinates)this.villagerPositionsList.RemoveAndReturn(0));
			}
		}

		private void addNewDoorsToVillageOrCreateVillage()
		{
			for (int i1 = 0; i1 < this.newDoors.Count; ++i1)
			{
				VillageDoorInfo villageDoorInfo2 = (VillageDoorInfo)this.newDoors[i1];
				bool z3 = false;
				System.Collections.IEnumerator iterator4 = this.villageList.GetEnumerator();

				while (iterator4.MoveNext())
				{
					Village village5 = (Village)iterator4.Current;
					int i6 = (int)village5.Center.getEuclideanDistanceTo(villageDoorInfo2.posX, villageDoorInfo2.posY, villageDoorInfo2.posZ);
					if (i6 <= 32 + village5.VillageRadius)
					{
						village5.addVillageDoorInfo(villageDoorInfo2);
						z3 = true;
						break;
					}
				}

				if (!z3)
				{
					Village village7 = new Village(this.worldObj);
					village7.addVillageDoorInfo(villageDoorInfo2);
					this.villageList.Add(village7);
				}
			}

			this.newDoors.Clear();
		}

		private void addUnassignedWoodenDoorsAroundToNewDoorsList(ChunkCoordinates chunkCoordinates1)
		{
			sbyte b2 = 16;
			sbyte b3 = 4;
			sbyte b4 = 16;

			for (int i5 = chunkCoordinates1.posX - b2; i5 < chunkCoordinates1.posX + b2; ++i5)
			{
				for (int i6 = chunkCoordinates1.posY - b3; i6 < chunkCoordinates1.posY + b3; ++i6)
				{
					for (int i7 = chunkCoordinates1.posZ - b4; i7 < chunkCoordinates1.posZ + b4; ++i7)
					{
						if (this.isWoodenDoorAt(i5, i6, i7))
						{
							VillageDoorInfo villageDoorInfo8 = this.getVillageDoorAt(i5, i6, i7);
							if (villageDoorInfo8 == null)
							{
								this.addDoorToNewListIfAppropriate(i5, i6, i7);
							}
							else
							{
								villageDoorInfo8.lastActivityTimestamp = this.tickCounter;
							}
						}
					}
				}
			}

		}

		private VillageDoorInfo getVillageDoorAt(int i1, int i2, int i3)
		{
			System.Collections.IEnumerator iterator4 = this.newDoors.GetEnumerator();

			VillageDoorInfo villageDoorInfo5;
			do
			{
				if (!iterator4.MoveNext())
				{
					iterator4 = this.villageList.GetEnumerator();

					VillageDoorInfo villageDoorInfo6;
					do
					{
						if (!iterator4.MoveNext())
						{
							return null;
						}

						Village village7 = (Village)iterator4.Current;
						villageDoorInfo6 = village7.getVillageDoorAt(i1, i2, i3);
					} while (villageDoorInfo6 == null);

					return villageDoorInfo6;
				}

				villageDoorInfo5 = (VillageDoorInfo)iterator4.Current;
			} while (villageDoorInfo5.posX != i1 || villageDoorInfo5.posZ != i3 || Math.Abs(villageDoorInfo5.posY - i2) > 1);

			return villageDoorInfo5;
		}

		private void addDoorToNewListIfAppropriate(int i1, int i2, int i3)
		{
			int i4 = ((BlockDoor)Block.doorWood).getDoorOrientation(this.worldObj, i1, i2, i3);
			int i5;
			int i6;
			if (i4 != 0 && i4 != 2)
			{
				i5 = 0;

				for (i6 = -5; i6 < 0; ++i6)
				{
					if (this.worldObj.canBlockSeeTheSky(i1, i2, i3 + i6))
					{
						--i5;
					}
				}

				for (i6 = 1; i6 <= 5; ++i6)
				{
					if (this.worldObj.canBlockSeeTheSky(i1, i2, i3 + i6))
					{
						++i5;
					}
				}

				if (i5 != 0)
				{
					this.newDoors.Add(new VillageDoorInfo(i1, i2, i3, 0, i5 > 0 ? -2 : 2, this.tickCounter));
				}
			}
			else
			{
				i5 = 0;

				for (i6 = -5; i6 < 0; ++i6)
				{
					if (this.worldObj.canBlockSeeTheSky(i1 + i6, i2, i3))
					{
						--i5;
					}
				}

				for (i6 = 1; i6 <= 5; ++i6)
				{
					if (this.worldObj.canBlockSeeTheSky(i1 + i6, i2, i3))
					{
						++i5;
					}
				}

				if (i5 != 0)
				{
					this.newDoors.Add(new VillageDoorInfo(i1, i2, i3, i5 > 0 ? -2 : 2, 0, this.tickCounter));
				}
			}

		}

		private bool isVillagerPositionPresent(int i1, int i2, int i3)
		{
			System.Collections.IEnumerator iterator4 = this.villagerPositionsList.GetEnumerator();

			ChunkCoordinates chunkCoordinates5;
			do
			{
				if (!iterator4.MoveNext())
				{
					return false;
				}

				chunkCoordinates5 = (ChunkCoordinates)iterator4.Current;
			} while (chunkCoordinates5.posX != i1 || chunkCoordinates5.posY != i2 || chunkCoordinates5.posZ != i3);

			return true;
		}

		private bool isWoodenDoorAt(int i1, int i2, int i3)
		{
			int i4 = this.worldObj.getBlockId(i1, i2, i3);
			return i4 == Block.doorWood.blockID;
		}
	}

}