using System;

namespace net.minecraft.src
{

	public class EntityPotion : EntityThrowable
	{
		private int potionDamage;

		public EntityPotion(World world1) : base(world1)
		{
		}

		public EntityPotion(World world1, EntityLiving entityLiving2, int i3) : base(world1, entityLiving2)
		{
			this.potionDamage = i3;
		}

		public EntityPotion(World world1, double d2, double d4, double d6, int i8) : base(world1, d2, d4, d6)
		{
			this.potionDamage = i8;
		}

		protected internal override float func_40075_e()
		{
			return 0.05F;
		}

		protected internal override float func_40077_c()
		{
			return 0.5F;
		}

		protected internal override float func_40074_d()
		{
			return -20.0F;
		}

		public virtual int PotionDamage
		{
			get
			{
				return this.potionDamage;
			}
		}

		// WTF????
		protected internal override void onImpact(MovingObjectPosition movingObjectPosition1)
		{
			if (!this.worldObj.isRemote)
			{
				System.Collections.IList list2 = Item.potion.getEffects(this.potionDamage);
				if (list2 != null && list2.Count > 0)
				{
					AxisAlignedBB axisAlignedBB3 = this.boundingBox.expand(4.0D, 2.0D, 4.0D);
					System.Collections.IList list4 = this.worldObj.getEntitiesWithinAABB(typeof(EntityLiving), axisAlignedBB3);
					if (list4 != null && list4.Count > 0)
					{
						System.Collections.IEnumerator iterator5 = list4.GetEnumerator();

						while (true)
						{
							Entity entity6;
							double d7;
							do
							{
								if (!iterator5.MoveNext())
								{
									goto label48Break;
								}
                                
								entity6 = (Entity)iterator5.Current;
								d7 = this.getDistanceSqToEntity(entity6);
							} while (d7 >= 16.0D);

							double d9 = 1.0D - Math.Sqrt(d7) / 4.0D;
							if (entity6 == movingObjectPosition1.entityHit)
							{
								d9 = 1.0D;
							}

							System.Collections.IEnumerator iterator11 = list2.GetEnumerator();

							while (iterator11.MoveNext())
							{
								PotionEffect potionEffect12 = (PotionEffect)iterator11.Current;
								int i13 = potionEffect12.PotionID;
								if (Potion.potionTypes[i13].Instant)
								{
									Potion.potionTypes[i13].affectEntity(this.thrower, (EntityLiving)entity6, potionEffect12.Amplifier, d9);
								}
								else
								{
									int i14 = (int)(d9 * (double)potionEffect12.Duration + 0.5D);
									if (i14 > 20)
									{
										((EntityLiving)entity6).addPotionEffect(new PotionEffect(i13, i14, potionEffect12.Amplifier));
									}
								}
							}
							label48Continue:;
						}
						label48Break:;
					}
				}

				this.worldObj.playAuxSFX(2002, (int)(long)Math.Round(this.posX, MidpointRounding.AwayFromZero), (int)(long)Math.Round(this.posY, MidpointRounding.AwayFromZero), (int)(long)Math.Round(this.posZ, MidpointRounding.AwayFromZero), this.potionDamage);
				this.setDead();
			}

		}
	}

}