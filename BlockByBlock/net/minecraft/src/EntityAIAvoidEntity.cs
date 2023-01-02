using System;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EntityAIAvoidEntity : EntityAIBase
	{
		private EntityCreature theEntity;
		private float field_48242_b;
		private float field_48243_c;
		private Entity field_48240_d;
		private float field_48241_e;
		private PathEntity field_48238_f;
		private PathNavigate entityPathNavigate;
		private Type targetEntityClass;

		public EntityAIAvoidEntity(EntityCreature entityCreature1, Type class2, float f3, float f4, float f5)
		{
			this.theEntity = entityCreature1;
			this.targetEntityClass = class2;
			this.field_48241_e = f3;
			this.field_48242_b = f4;
			this.field_48243_c = f5;
			this.entityPathNavigate = entityCreature1.Navigator;
			this.MutexBits = 1;
		}

		public override bool shouldExecute()
		{
			if (this.targetEntityClass == typeof(EntityPlayer))
			{
				if (this.theEntity is EntityTameable && ((EntityTameable)this.theEntity).Tamed)
				{
					return false;
				}

				this.field_48240_d = this.theEntity.worldObj.getClosestPlayerToEntity(this.theEntity, (double)this.field_48241_e);
				if (this.field_48240_d == null)
				{
					return false;
				}
			}
			else
			{
				System.Collections.IList list1 = this.theEntity.worldObj.getEntitiesWithinAABB(this.targetEntityClass, this.theEntity.boundingBox.expand((double)this.field_48241_e, 3.0D, (double)this.field_48241_e));
				if (list1.Count == 0)
				{
					return false;
				}

				this.field_48240_d = (Entity)list1[0];
			}

			if (!this.theEntity.func_48090_aM().canSee(this.field_48240_d))
			{
				return false;
			}
			else
			{
				Vec3D vec3D2 = RandomPositionGenerator.func_48623_b(this.theEntity, 16, 7, Vec3D.createVector(this.field_48240_d.posX, this.field_48240_d.posY, this.field_48240_d.posZ));
				if (vec3D2 == null)
				{
					return false;
				}
				else if (this.field_48240_d.getDistanceSq(vec3D2.xCoord, vec3D2.yCoord, vec3D2.zCoord) < this.field_48240_d.getDistanceSqToEntity(this.theEntity))
				{
					return false;
				}
				else
				{
					this.field_48238_f = this.entityPathNavigate.getPathToXYZ(vec3D2.xCoord, vec3D2.yCoord, vec3D2.zCoord);
					return this.field_48238_f == null ? false : this.field_48238_f.func_48639_a(vec3D2);
				}
			}
		}

		public override bool continueExecuting()
		{
			return !this.entityPathNavigate.noPath();
		}

		public override void startExecuting()
		{
			this.entityPathNavigate.setPath(this.field_48238_f, this.field_48242_b);
		}

		public override void resetTask()
		{
			this.field_48240_d = null;
		}

		public override void updateTask()
		{
			if (this.theEntity.getDistanceSqToEntity(this.field_48240_d) < 49.0D)
			{
				this.theEntity.Navigator.Speed = this.field_48243_c;
			}
			else
			{
				this.theEntity.Navigator.Speed = this.field_48242_b;
			}

		}
	}

}