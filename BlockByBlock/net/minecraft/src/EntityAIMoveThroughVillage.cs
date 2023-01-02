using System.Collections;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAIMoveThroughVillage : EntityAIBase
	{
		private EntityCreature theEntity;
		private float field_48290_b;
		private PathEntity field_48291_c;
		private VillageDoorInfo? doorInfo;
		private bool field_48289_e;
		private System.Collections.IList doorList = new ArrayList();

		public EntityAIMoveThroughVillage(EntityCreature entityCreature1, float f2, bool z3)
		{
			this.theEntity = entityCreature1;
			this.field_48290_b = f2;
			this.field_48289_e = z3;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			trimCheckedDoorQueue();
			if (this.field_48289_e && this.theEntity.worldObj.Daytime)
			{
				return false;
			}
			else
			{
				Village village1 = this.theEntity.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.theEntity.posX), MathHelper.floor_double(this.theEntity.posY), MathHelper.floor_double(this.theEntity.posZ), 0);
				if (village1 == null)
				{
					return false;
				}
				else
				{
					this.doorInfo = this.func_48284_a(village1);
					if (this.doorInfo == null)
					{
						return false;
					}
					else
					{
						bool z2 = this.theEntity.Navigator.func_48665_b();
						this.theEntity.Navigator.BreakDoors = false;
						this.field_48291_c = this.theEntity.Navigator.getPathToXYZ((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ);
						this.theEntity.Navigator.BreakDoors = z2;
						if (this.field_48291_c != null)
						{
							return true;
						}
						else
						{
							Vec3D? vec3D3 = RandomPositionGenerator.func_48620_a(this.theEntity, 10, 7, Vec3D.createVector((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ));
							if (vec3D3 == null)
							{
								return false;
							}
							else
							{
								this.theEntity.Navigator.BreakDoors = false;
								this.field_48291_c = this.theEntity.Navigator.getPathToXYZ(vec3D3.xCoord, vec3D3.yCoord, vec3D3.zCoord);
								this.theEntity.Navigator.BreakDoors = z2;
								return this.field_48291_c != null;
							}
						}
					}
				}
			}
		}

		public override bool continueExecuting()
		{
			if (this.theEntity.Navigator.noPath())
			{
				return false;
			}
			else
			{
				float f1 = this.theEntity.width + 4.0F;
				return this.theEntity.getDistanceSq((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ) > (double)(f1 * f1);
			}
		}

		public override void startExecuting()
		{
			this.theEntity.Navigator.setPath(this.field_48291_c, this.field_48290_b);
		}

		public override void resetTask()
		{
			if (this.theEntity.Navigator.noPath() || this.theEntity.getDistanceSq((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ) < 16.0D)
			{
				this.doorList.Add(this.doorInfo);
			}

		}

		private VillageDoorInfo? func_48284_a(Village village1)
		{
			VillageDoorInfo? villageDoorInfo2 = null;
			int i3 = int.MaxValue;
			System.Collections.IList list4 = village1.VillageDoorInfoList;
			System.Collections.IEnumerator iterator5 = list4.GetEnumerator();

			while (iterator5.MoveNext())
			{
				VillageDoorInfo villageDoorInfo6 = (VillageDoorInfo)iterator5.Current;
				int i7 = villageDoorInfo6.getDistanceSquared(MathHelper.floor_double(this.theEntity.posX), MathHelper.floor_double(this.theEntity.posY), MathHelper.floor_double(this.theEntity.posZ));
				if (i7 < i3 && !this.hasCheckedDoor(villageDoorInfo6))
				{
					villageDoorInfo2 = villageDoorInfo6;
					i3 = i7;
				}
			}

			return villageDoorInfo2;
		}
		
		/// <summary>
		/// PORTING TODO: ensure this method name is accurate. I tried my best naming this one, as it was unmapped before.
		/// </summary>
		/// <param name="villageDoorInfo1"></param>
		/// <returns></returns>
		private bool hasCheckedDoor(VillageDoorInfo villageDoorInfo1)
		{
            foreach(VillageDoorInfo door in doorList)
            {
                if (villageDoorInfo1.posX == door.posX && villageDoorInfo1.posY == door.posY && villageDoorInfo1.posZ == door.posZ)
                {
                    return true;
                }
            }

			return false;
		}

		private void trimCheckedDoorQueue()
		{
			if (this.doorList.Count > 15)
			{
				this.doorList.RemoveAt(0);
			}

		}
	}

}