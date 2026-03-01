namespace net.minecraft.src
{
	public class EntityGiantZombie : EntityMob
	{
		public EntityGiantZombie(World world1) : base(world1)
		{
			this.texture = "/mob/zombie.png";
			this.moveSpeed = 0.5F;
			this.attackStrength = 50;
			this.yOffset *= 6.0F;
			this.setSize(this.width * 6.0F, this.height * 6.0F);
		}

		public override int MaxHealth
		{
			get
			{
				return 100;
			}
		}

		public override float getBlockPathWeight(int i1, int i2, int i3)
		{
			return this.worldObj.getLightBrightness(i1, i2, i3) - 0.5F;
		}
	}

}