using BlockByBlock.helpers;
using net.minecraft.client.entity;
using System;
using System.Collections;

namespace net.minecraft.src
{

    public class Village
	{
		private readonly World worldObj;
		private readonly System.Collections.IList villageDoorInfoList = new ArrayList();
		private readonly ChunkCoordinates centerHelper = new ChunkCoordinates(0, 0, 0);
		private readonly ChunkCoordinates center = new ChunkCoordinates(0, 0, 0);
		private int villageRadius = 0;
		private int lastAddDoorTimestamp = 0;
		private int tickCounter = 0;
		private int numVillagers = 0;
		private System.Collections.IList villageAgressors = new ArrayList();
		private int numIronGolems = 0;

		public Village(World world1)
		{
			worldObj = world1;
		}

		public virtual void tick(int i1)
		{
			tickCounter = i1;
			removeDeadAndOutOfRangeDoors();
			removeDeadAndOldAgressors();
			if (i1 % 20 == 0)
			{
				updateNumVillagers();
			}

			if (i1 % 30 == 0)
			{
				updateNumIronGolems();
			}

			int i2 = numVillagers / 16;
			if (numIronGolems < i2 && villageDoorInfoList.Count > 20 && worldObj.rand.Next(7000) == 0)
			{
				Vec3D vec3D3 = tryGetIronGolemSpawningLocation(MathHelper.floor_float((float)center.posX), MathHelper.floor_float((float)center.posY), MathHelper.floor_float((float)center.posZ), 2, 4, 2);
				if (vec3D3 != null)
				{
					EntityIronGolem entityIronGolem4 = new EntityIronGolem(worldObj);
					entityIronGolem4.SetPosition(vec3D3.xCoord, vec3D3.yCoord, vec3D3.zCoord);
					worldObj.spawnEntityInWorld(entityIronGolem4);
					++numIronGolems;
				}
			}

		}

		private Vec3D tryGetIronGolemSpawningLocation(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			for (int i7 = 0; i7 < 10; ++i7)
			{
				int i8 = i1 + worldObj.rand.Next(16) - 8;
				int i9 = i2 + worldObj.rand.Next(6) - 3;
				int i10 = i3 + worldObj.rand.Next(16) - 8;
				if (isInRange(i8, i9, i10) && isValidIronGolemSpawningLocation(i8, i9, i10, i4, i5, i6))
				{
					return Vec3D.createVector((double)i8, (double)i9, (double)i10);
				}
			}

			return null;
		}

		private bool isValidIronGolemSpawningLocation(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			if (!worldObj.isBlockNormalCube(i1, i2 - 1, i3))
			{
				return false;
			}
			else
			{
				int i7 = i1 - i4 / 2;
				int i8 = i3 - i6 / 2;

				for (int i9 = i7; i9 < i7 + i4; ++i9)
				{
					for (int i10 = i2; i10 < i2 + i5; ++i10)
					{
						for (int i11 = i8; i11 < i8 + i6; ++i11)
						{
							if (worldObj.isBlockNormalCube(i9, i10, i11))
							{
								return false;
							}
						}
					}
				}

				return true;
			}
		}

		private void updateNumIronGolems()
		{
			System.Collections.IList list1 = worldObj.getEntitiesWithinAABB(typeof(EntityIronGolem), AxisAlignedBB.getBoundingBoxFromPool((double)(center.posX - villageRadius), (double)(center.posY - 4), (double)(center.posZ - villageRadius), (double)(center.posX + villageRadius), (double)(center.posY + 4), (double)(center.posZ + villageRadius)));
			numIronGolems = list1.Count;
		}

		private void updateNumVillagers()
		{
			System.Collections.IList list1 = worldObj.getEntitiesWithinAABB(typeof(EntityVillager), AxisAlignedBB.getBoundingBoxFromPool((double)(center.posX - villageRadius), (double)(center.posY - 4), (double)(center.posZ - villageRadius), (double)(center.posX + villageRadius), (double)(center.posY + 4), (double)(center.posZ + villageRadius)));
			numVillagers = list1.Count;
		}

		public virtual ChunkCoordinates Center
		{
			get
			{
				return center;
			}
		}

		public virtual int VillageRadius
		{
			get
			{
				return villageRadius;
			}
		}

		public virtual int NumVillageDoors
		{
			get
			{
				return villageDoorInfoList.Count;
			}
		}

		public virtual int TicksSinceLastDoorAdding
		{
			get
			{
				return tickCounter - lastAddDoorTimestamp;
			}
		}

		public virtual int NumVillagers
		{
			get
			{
				return numVillagers;
			}
		}

		public virtual bool isInRange(int i1, int i2, int i3)
		{
			return center.getDistanceSquared(i1, i2, i3) < (float)(villageRadius * villageRadius);
		}

		public virtual System.Collections.IList VillageDoorInfoList
		{
			get
			{
				return villageDoorInfoList;
			}
		}

		public virtual VillageDoorInfo findNearestDoor(int i1, int i2, int i3)
		{
			VillageDoorInfo villageDoorInfo4 = null;
			int i5 = int.MaxValue;
			System.Collections.IEnumerator iterator6 = villageDoorInfoList.GetEnumerator();

			while (iterator6.MoveNext())
			{
				VillageDoorInfo villageDoorInfo7 = (VillageDoorInfo)iterator6.Current;
				int i8 = villageDoorInfo7.getDistanceSquared(i1, i2, i3);
				if (i8 < i5)
				{
					villageDoorInfo4 = villageDoorInfo7;
					i5 = i8;
				}
			}

			return villageDoorInfo4;
		}

		public virtual VillageDoorInfo findNearestDoorUnrestricted(int i1, int i2, int i3)
		{
			VillageDoorInfo villageDoorInfo4 = null;
			int i5 = int.MaxValue;
			System.Collections.IEnumerator iterator6 = villageDoorInfoList.GetEnumerator();

			while (iterator6.MoveNext())
			{
				VillageDoorInfo villageDoorInfo7 = (VillageDoorInfo)iterator6.Current;
				int i8 = villageDoorInfo7.getDistanceSquared(i1, i2, i3);
				if (i8 > 256)
				{
					i8 *= 1000;
				}
				else
				{
					i8 = villageDoorInfo7.DoorOpeningRestrictionCounter;
				}

				if (i8 < i5)
				{
					villageDoorInfo4 = villageDoorInfo7;
					i5 = i8;
				}
			}

			return villageDoorInfo4;
		}

		public virtual VillageDoorInfo getVillageDoorAt(int i1, int i2, int i3)
		{
			if (center.getDistanceSquared(i1, i2, i3) > (float)(villageRadius * villageRadius))
			{
				return null;
			}
			else
			{
				System.Collections.IEnumerator iterator4 = villageDoorInfoList.GetEnumerator();

				VillageDoorInfo villageDoorInfo5;
				do
				{
					if (!iterator4.MoveNext())
					{
						return null;
					}

					villageDoorInfo5 = (VillageDoorInfo)iterator4.Current;
				} while (villageDoorInfo5.posX != i1 || villageDoorInfo5.posZ != i3 || Math.Abs(villageDoorInfo5.posY - i2) > 1);

				return villageDoorInfo5;
			}
		}

		public virtual void addVillageDoorInfo(VillageDoorInfo villageDoorInfo1)
		{
			villageDoorInfoList.Add(villageDoorInfo1);
			centerHelper.posX += villageDoorInfo1.posX;
			centerHelper.posY += villageDoorInfo1.posY;
			centerHelper.posZ += villageDoorInfo1.posZ;
			updateVillageRadiusAndCenter();
			lastAddDoorTimestamp = villageDoorInfo1.lastActivityTimestamp;
		}

		public virtual bool Annihilated
		{
			get
			{
				return villageDoorInfoList.Count == 0;
			}
		}

		public virtual void addOrRenewAgressor(EntityLiving entityLiving1)
		{
			System.Collections.IEnumerator iterator2 = villageAgressors.GetEnumerator();

			VillageAgressor villageAgressor3;
			do
			{
				if (!iterator2.MoveNext())
				{
					villageAgressors.Add(new VillageAgressor(this, entityLiving1, tickCounter));
					return;
				}

				villageAgressor3 = (VillageAgressor)iterator2.Current;
			} while (villageAgressor3.agressor != entityLiving1);

			villageAgressor3.agressionTime = tickCounter;
		}

		public virtual EntityLiving findNearestVillageAggressor(EntityLiving entityLiving1)
		{
			double d2 = double.MaxValue;
			VillageAgressor villageAgressor4 = null;

			for (int i5 = 0; i5 < villageAgressors.Count; ++i5)
			{
				VillageAgressor villageAgressor6 = (VillageAgressor)villageAgressors[i5];
				double d7 = villageAgressor6.agressor.getDistanceSqToEntity(entityLiving1);
				if (d7 <= d2)
				{
					villageAgressor4 = villageAgressor6;
					d2 = d7;
				}
			}

			return villageAgressor4 != null ? villageAgressor4.agressor : null;
		}

		private void removeDeadAndOldAgressors()
		{
			for (int i = villageAgressors.Count - 1; i >= 0; i--)
            {
                VillageAgressor villageAgressor = (VillageAgressor)villageAgressors[i];

                if (!villageAgressor.agressor.EntityAlive || Math.Abs(tickCounter - villageAgressor.agressionTime) > 300)

                    villageAgressors.RemoveAt(i);
            }
		}

		List<VillageDoorInfo> doorsToRemove = new();

		private void removeDeadAndOutOfRangeDoors()
		{
			bool z1 = false;
			bool z2 = worldObj.rand.Next(50) == 0;
			System.Collections.IEnumerator iterator3 = villageDoorInfoList.GetEnumerator();

			doorsToRemove.Clear();

			while (true)
			{
				VillageDoorInfo villageDoorInfo4;
				do
				{
					if (!iterator3.MoveNext())
					{
						if (z1)
						{
							updateVillageRadiusAndCenter();
						}

						goto doorRemove;
					}
                    
					villageDoorInfo4 = (VillageDoorInfo)iterator3.Current;
					if (z2)
					{
						villageDoorInfo4.resetDoorOpeningRestrictionCounter();
					}
				} while (isBlockDoor(villageDoorInfo4.posX, villageDoorInfo4.posY, villageDoorInfo4.posZ) && Math.Abs(tickCounter - villageDoorInfo4.lastActivityTimestamp) <= 1200);

				if (villageDoorInfo4 == null)
					continue;

				if (!isBlockDoor(villageDoorInfo4.posX, villageDoorInfo4.posY, villageDoorInfo4.posZ) || Math.Abs(tickCounter - villageDoorInfo4.lastActivityTimestamp) > 1200)
                {
					centerHelper.posX -= villageDoorInfo4.posX;
					centerHelper.posY -= villageDoorInfo4.posY;
					centerHelper.posZ -= villageDoorInfo4.posZ;
					z1 = true;
					villageDoorInfo4.isDetachedFromVillageFlag = true;

					doorsToRemove.Add((VillageDoorInfo)iterator3.Current);
					Console.WriteLine("door gone!");
				}
			}

            doorRemove:
			villageDoorInfoList.RemoveAll(doorsToRemove);

		}

		private bool isBlockDoor(int i1, int i2, int i3)
		{
			int i4 = worldObj.getBlockId(i1, i2, i3);
			return i4 <= 0 ? false : i4 == Block.doorWood.blockID;
		}

		private void updateVillageRadiusAndCenter()
		{
			int i1 = villageDoorInfoList.Count;
			if (i1 == 0)
			{
				center.set(0, 0, 0);
				villageRadius = 0;
			}
			else
			{
				center.set(centerHelper.posX / i1, centerHelper.posY / i1, centerHelper.posZ / i1);
				int i2 = 0;

				VillageDoorInfo villageDoorInfo4;
				for (System.Collections.IEnumerator iterator3 = villageDoorInfoList.GetEnumerator(); iterator3.MoveNext(); i2 = Math.Max(villageDoorInfo4.getDistanceSquared(center.posX, center.posY, center.posZ), i2))
				{
					villageDoorInfo4 = (VillageDoorInfo)iterator3.Current;
				}

				villageRadius = Math.Max(32, (int)Math.Sqrt((double)i2) + 1);
			}
		}
	}

}