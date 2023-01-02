using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class EntityAIBeg : EntityAIBase
	{
		private EntityWolf theWolf;
		private EntityPlayer field_48348_b;
		private World field_48349_c;
		private float field_48346_d;
		private int field_48347_e;

		public EntityAIBeg(EntityWolf entityWolf1, float f2)
		{
			this.theWolf = entityWolf1;
			this.field_48349_c = entityWolf1.worldObj;
			this.field_48346_d = f2;
			this.MutexBits = 2;
		}

		public override bool shouldExecute()
		{
			this.field_48348_b = this.field_48349_c.getClosestPlayerToEntity(this.theWolf, (double)this.field_48346_d);
			return this.field_48348_b == null ? false : this.func_48345_a(this.field_48348_b);
		}

		public override bool continueExecuting()
		{
			return !this.field_48348_b.EntityAlive ? false : (this.theWolf.getDistanceSqToEntity(this.field_48348_b) > (double)(this.field_48346_d * this.field_48346_d) ? false : this.field_48347_e > 0 && this.func_48345_a(this.field_48348_b));
		}

		public override void startExecuting()
		{
			this.theWolf.func_48150_h(true);
			this.field_48347_e = 40 + this.theWolf.RNG.Next(40);
		}

		public override void resetTask()
		{
			this.theWolf.func_48150_h(false);
			this.field_48348_b = null;
		}

		public override void updateTask()
		{
			this.theWolf.LookHelper.setLookPosition(this.field_48348_b.posX, this.field_48348_b.posY + (double)this.field_48348_b.EyeHeight, this.field_48348_b.posZ, 10.0F, (float)this.theWolf.VerticalFaceSpeed);
			--this.field_48347_e;
		}

		private bool func_48345_a(EntityPlayer entityPlayer1)
		{
			ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
			return itemStack2 == null ? false : (!this.theWolf.Tamed && itemStack2.itemID == Item.bone.shiftedIndex ? true : this.theWolf.isWheat(itemStack2));
		}
	}

}