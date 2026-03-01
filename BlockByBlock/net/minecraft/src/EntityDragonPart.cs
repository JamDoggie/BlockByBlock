namespace net.minecraft.src
{
	public class EntityDragonPart : Entity
	{
		public readonly EntityDragonBase entityDragonObj;
		public readonly string name;

		public EntityDragonPart(EntityDragonBase entityDragonBase1, string string2, float f3, float f4) : base(entityDragonBase1.worldObj)
		{
			this.setSize(f3, f4);
			this.entityDragonObj = entityDragonBase1;
			this.name = string2;
		}

		protected internal override void entityInit()
		{
		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
		}

		public override bool canBeCollidedWith()
		{
			return true;
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			return this.entityDragonObj.attackEntityFromPart(this, damageSource1, i2);
		}

		public override bool isEntityEqual(Entity entity1)
		{
			return this == entity1 || this.entityDragonObj == entity1;
		}
	}

}