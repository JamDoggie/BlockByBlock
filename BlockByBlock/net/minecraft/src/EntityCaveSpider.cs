namespace net.minecraft.src
{
	public class EntityCaveSpider : EntitySpider
	{
		public EntityCaveSpider(World world1) : base(world1)
		{
			this.texture = "/mob/cavespider.png";
			this.setSize(0.7F, 0.5F);
		}

		public override int MaxHealth
		{
			get
			{
				return 12;
			}
		}

		public override float spiderScaleAmount()
		{
			return 0.7F;
		}

		public override bool attackEntityAsMob(Entity entity1)
		{
			if (base.attackEntityAsMob(entity1))
			{
				if (entity1 is EntityLiving)
				{
					sbyte b2 = 0;
					if (this.worldObj.difficultySetting > 1)
					{
						if (this.worldObj.difficultySetting == 2)
						{
							b2 = 7;
						}
						else if (this.worldObj.difficultySetting == 3)
						{
							b2 = 15;
						}
					}

					if (b2 > 0)
					{
						((EntityLiving)entity1).addPotionEffect(new PotionEffect(Potion.poison.id, b2 * 20, 0));
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}
	}

}