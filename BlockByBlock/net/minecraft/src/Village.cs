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
			this.worldObj = world1;
		}

		public virtual void tick(int i1)
		{
			this.tickCounter = i1;
			this.removeDeadAndOutOfRangeDoors();
			this.removeDeadAndOldAgressors();
			if (i1 % 20 == 0)
			{
				this.updateNumVillagers();
			}

			if (i1 % 30 == 0)
			{
				this.updateNumIronGolems();
			}

			int i2 = this.numVillagers / 16;
			if (this.numIronGolems < i2 && this.villageDoorInfoList.Count > 20 && this.worldObj.rand.Next(7000) == 0)
			{
				Vec3D vec3D3 = this.tryGetIronGolemSpawningLocation(MathHelper.floor_float((float)this.center.posX), MathHelper.floor_float((float)this.center.posY), MathHelper.floor_float((float)this.center.posZ), 2, 4, 2);
				if (vec3D3 != null)
				{
					EntityIronGolem entityIronGolem4 = new EntityIronGolem(this.worldObj);
					entityIronGolem4.setPosition(vec3D3.xCoord, vec3D3.yCoord, vec3D3.zCoord);
					this.worldObj.spawnEntityInWorld(entityIronGolem4);
					++this.numIronGolems;
				}
			}

		}

		private Vec3D tryGetIronGolemSpawningLocation(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			for (int i7 = 0; i7 < 10; ++i7)
			{
				int i8 = i1 + this.worldObj.rand.Next(16) - 8;
				int i9 = i2 + this.worldObj.rand.Next(6) - 3;
				int i10 = i3 + this.worldObj.rand.Next(16) - 8;
				if (this.isInRange(i8, i9, i10) && this.isValidIronGolemSpawningLocation(i8, i9, i10, i4, i5, i6))
				{
					return Vec3D.createVector((double)i8, (double)i9, (double)i10);
				}
			}

			return null;
		}

		private bool isValidIronGolemSpawningLocation(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			if (!this.worldObj.isBlockNormalCube(i1, i2 - 1, i3))
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
							if (this.worldObj.isBlockNormalCube(i9, i10, i11))
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
			System.Collections.IList list1 = this.worldObj.getEntitiesWithinAABB(typeof(EntityIronGolem), AxisAlignedBB.getBoundingBoxFromPool((double)(this.center.posX - this.villageRadius), (double)(this.center.posY - 4), (double)(this.center.posZ - this.villageRadius), (double)(this.center.posX + this.villageRadius), (double)(this.center.posY + 4), (double)(this.center.posZ + this.villageRadius)));
			this.numIronGolems = list1.Count;
		}

		private void updateNumVillagers()
		{
			System.Collections.IList list1 = this.worldObj.getEntitiesWithinAABB(typeof(EntityVillager), AxisAlignedBB.getBoundingBoxFromPool((double)(this.center.posX - this.villageRadius), (double)(this.center.posY - 4), (double)(this.center.posZ - this.villageRadius), (double)(this.center.posX + this.villageRadius), (double)(this.center.posY + 4), (double)(this.center.posZ + this.villageRadius)));
			this.numVillagers = list1.Count;
		}

		public virtual ChunkCoordinates Center
		{
			get
			{
				return this.center;
			}
		}

		public virtual int VillageRadius
		{
			get
			{
				return this.villageRadius;
			}
		}

		public virtual int NumVillageDoors
		{
			get
			{
				return this.villageDoorInfoList.Count;
			}
		}

		public virtual int TicksSinceLastDoorAdding
		{
			get
			{
				return this.tickCounter - this.lastAddDoorTimestamp;
			}
		}

		public virtual int NumVillagers
		{
			get
			{
				return this.numVillagers;
			}
		}

		public virtual bool isInRange(int i1, int i2, int i3)
		{
			return this.center.getDistanceSquared(i1, i2, i3) < (float)(this.villageRadius * this.villageRadius);
		}

		public virtual System.Collections.IList VillageDoorInfoList
		{
			get
			{
				return this.villageDoorInfoList;
			}
		}

		public virtual VillageDoorInfo findNearestDoor(int i1, int i2, int i3)
		{
			VillageDoorInfo villageDoorInfo4 = null;
			int i5 = int.MaxValue;
			System.Collections.IEnumerator iterator6 = this.villageDoorInfoList.GetEnumerator();

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
			System.Collections.IEnumerator iterator6 = this.villageDoorInfoList.GetEnumerator();

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
			if (this.center.getDistanceSquared(i1, i2, i3) > (float)(this.villageRadius * this.villageRadius))
			{
				return null;
			}
			else
			{
				System.Collections.IEnumerator iterator4 = this.villageDoorInfoList.GetEnumerator();

				VillageDoorInfo villageDoorInfo5;
				do
				{
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					if (!iterator4.hasNext())
					{
						return null;
					}

//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					villageDoorInfo5 = (VillageDoorInfo)iterator4.next();
				} while (villageDoorInfo5.posX != i1 || villageDoorInfo5.posZ != i3 || Math.Abs(villageDoorInfo5.posY - i2) > 1);

				return villageDoorInfo5;
			}
		}

		public virtual void addVillageDoorInfo(VillageDoorInfo villageDoorInfo1)
		{
			this.villageDoorInfoList.Add(villageDoorInfo1);
			this.centerHelper.posX += villageDoorInfo1.posX;
			this.centerHelper.posY += villageDoorInfo1.posY;
			this.centerHelper.posZ += villageDoorInfo1.posZ;
			this.updateVillageRadiusAndCenter();
			this.lastAddDoorTimestamp = villageDoorInfo1.lastActivityTimestamp;
		}

		public virtual bool Annihilated
		{
			get
			{
				return this.villageDoorInfoList.Count == 0;
			}
		}

		public virtual void addOrRenewAgressor(EntityLiving entityLiving1)
		{
			System.Collections.IEnumerator iterator2 = this.villageAgressors.GetEnumerator();

			VillageAgressor villageAgressor3;
			do
			{
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
				if (!iterator2.hasNext())
				{
					this.villageAgressors.Add(new VillageAgressor(this, entityLiving1, this.tickCounter));
					return;
				}

//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
				villageAgressor3 = (VillageAgressor)iterator2.next();
			} while (villageAgressor3.agressor != entityLiving1);

			villageAgressor3.agressionTime = this.tickCounter;
		}

		public virtual EntityLiving findNearestVillageAggressor(EntityLiving entityLiving1)
		{
			double d2 = double.MaxValue;
			VillageAgressor villageAgressor4 = null;

			for (int i5 = 0; i5 < this.villageAgressors.Count; ++i5)
			{
				VillageAgressor villageAgressor6 = (VillageAgressor)this.villageAgressors[i5];
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
			System.Collections.IEnumerator iterator1 = this.villageAgressors.GetEnumerator();

			while (true)
			{
				VillageAgressor villageAgressor2;
				do
				{
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					if (!iterator1.hasNext())
					{
						return;
					}

//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					villageAgressor2 = (VillageAgressor)iterator1.next();
				} while (villageAgressor2.agressor.EntityAlive && Math.Abs(this.tickCounter - villageAgressor2.agressionTime) <= 300);

//JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
				iterator1.remove();
			}
		}

		private void removeDeadAndOutOfRangeDoors()
		{
			bool z1 = false;
			bool z2 = this.worldObj.rand.Next(50) == 0;
			System.Collections.IEnumerator iterator3 = this.villageDoorInfoList.GetEnumerator();

			while (true)
			{
				VillageDoorInfo villageDoorInfo4;
				do
				{
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					if (!iterator3.hasNext())
					{
						if (z1)
						{
							this.updateVillageRadiusAndCenter();
						}

						return;
					}

//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					villageDoorInfo4 = (VillageDoorInfo)iterator3.next();
					if (z2)
					{
						villageDoorInfo4.resetDoorOpeningRestrictionCounter();
					}
				} while (this.isBlockDoor(villageDoorInfo4.posX, villageDoorInfo4.posY, villageDoorInfo4.posZ) && Math.Abs(this.tickCounter - villageDoorInfo4.lastActivityTimestamp) <= 1200);

				this.centerHelper.posX -= villageDoorInfo4.posX;
				this.centerHelper.posY -= villageDoorInfo4.posY;
				this.centerHelper.posZ -= villageDoorInfo4.posZ;
				z1 = true;
				villageDoorInfo4.isDetachedFromVillageFlag = true;
//JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
				iterator3.remove();
			}
		}

		private bool isBlockDoor(int i1, int i2, int i3)
		{
			int i4 = this.worldObj.getBlockId(i1, i2, i3);
			return i4 <= 0 ? false : i4 == Block.doorWood.blockID;
		}

		private void updateVillageRadiusAndCenter()
		{
			int i1 = this.villageDoorInfoList.Count;
			if (i1 == 0)
			{
				this.center.set(0, 0, 0);
				this.villageRadius = 0;
			}
			else
			{
				this.center.set(this.centerHelper.posX / i1, this.centerHelper.posY / i1, this.centerHelper.posZ / i1);
				int i2 = 0;

				VillageDoorInfo villageDoorInfo4;
				for (System.Collections.IEnumerator iterator3 = this.villageDoorInfoList.GetEnumerator(); iterator3.MoveNext(); i2 = Math.Max(villageDoorInfo4.getDistanceSquared(this.center.posX, this.center.posY, this.center.posZ), i2))
				{
					villageDoorInfo4 = (VillageDoorInfo)iterator3.Current;
				}

				this.villageRadius = Math.Max(32, (int)Math.Sqrt((double)i2) + 1);
			}
		}
	}

}