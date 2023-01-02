using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class Potion
	{
		public static readonly Potion[] potionTypes = new Potion[32];
		public const Potion field_35676_b = null;
		public static readonly Potion moveSpeed = (new Potion(1, false, 8171462)).setPotionName("potion.moveSpeed").setIconIndex(0, 0);
		public static readonly Potion moveSlowdown = (new Potion(2, true, 5926017)).setPotionName("potion.moveSlowdown").setIconIndex(1, 0);
		public static readonly Potion digSpeed = (new Potion(3, false, 14270531)).setPotionName("potion.digSpeed").setIconIndex(2, 0).setEffectiveness(1.5D);
		public static readonly Potion digSlowdown = (new Potion(4, true, 4866583)).setPotionName("potion.digSlowDown").setIconIndex(3, 0);
		public static readonly Potion damageBoost = (new Potion(5, false, 9643043)).setPotionName("potion.damageBoost").setIconIndex(4, 0);
		public static readonly Potion heal = (new PotionHealth(6, false, 16262179)).setPotionName("potion.heal");
		public static readonly Potion harm = (new PotionHealth(7, true, 4393481)).setPotionName("potion.harm");
		public static readonly Potion jump = (new Potion(8, false, 7889559)).setPotionName("potion.jump").setIconIndex(2, 1);
		public static readonly Potion confusion = (new Potion(9, true, 5578058)).setPotionName("potion.confusion").setIconIndex(3, 1).setEffectiveness(0.25D);
		public static readonly Potion regeneration = (new Potion(10, false, 13458603)).setPotionName("potion.regeneration").setIconIndex(7, 0).setEffectiveness(0.25D);
		public static readonly Potion resistance = (new Potion(11, false, 10044730)).setPotionName("potion.resistance").setIconIndex(6, 1);
		public static readonly Potion fireResistance = (new Potion(12, false, 14981690)).setPotionName("potion.fireResistance").setIconIndex(7, 1);
		public static readonly Potion waterBreathing = (new Potion(13, false, 3035801)).setPotionName("potion.waterBreathing").setIconIndex(0, 2);
		public static readonly Potion invisibility = (new Potion(14, false, 8356754)).setPotionName("potion.invisibility").setIconIndex(0, 1).setPotionUnusable();
		public static readonly Potion blindness = (new Potion(15, true, 2039587)).setPotionName("potion.blindness").setIconIndex(5, 1).setEffectiveness(0.25D);
		public static readonly Potion nightVision = (new Potion(16, false, 2039713)).setPotionName("potion.nightVision").setIconIndex(4, 1).setPotionUnusable();
		public static readonly Potion hunger = (new Potion(17, true, 5797459)).setPotionName("potion.hunger").setIconIndex(1, 1);
		public static readonly Potion weakness = (new Potion(18, true, 4738376)).setPotionName("potion.weakness").setIconIndex(5, 0);
		public static readonly Potion poison = (new Potion(19, true, 5149489)).setPotionName("potion.poison").setIconIndex(6, 0).setEffectiveness(0.25D);
		public const Potion field_35688_v = null;
		public const Potion field_35687_w = null;
		public const Potion field_35697_x = null;
		public const Potion field_35696_y = null;
		public const Potion field_35695_z = null;
		public const Potion field_35667_A = null;
		public const Potion field_35668_B = null;
		public const Potion field_35669_C = null;
		public const Potion field_35663_D = null;
		public const Potion field_35664_E = null;
		public const Potion field_35665_F = null;
		public const Potion field_35666_G = null;
		public readonly int id;
		private string name = "";
		private int statusIconIndex = -1;
		private readonly bool isBadEffect;
		private double effectiveness;
		private bool usable;
		private readonly int liquidColor;

		protected internal Potion(int i1, bool z2, int i3)
		{
			this.id = i1;
			potionTypes[i1] = this;
			this.isBadEffect = z2;
			if (z2)
			{
				this.effectiveness = 0.5D;
			}
			else
			{
				this.effectiveness = 1.0D;
			}

			this.liquidColor = i3;
		}

		protected internal virtual Potion setIconIndex(int i1, int i2)
		{
			this.statusIconIndex = i1 + i2 * 8;
			return this;
		}

		public virtual int Id
		{
			get
			{
				return this.id;
			}
		}

		public virtual void performEffect(EntityLiving entityLiving1, int i2)
		{
			if (this.id == regeneration.id)
			{
				if (entityLiving1.Health < entityLiving1.MaxHealth)
				{
					entityLiving1.heal(1);
				}
			}
			else if (this.id == poison.id)
			{
				if (entityLiving1.Health > 1)
				{
					entityLiving1.attackEntityFrom(DamageSource.magic, 1);
				}
			}
			else if (this.id == hunger.id && entityLiving1 is EntityPlayer)
			{
				((EntityPlayer)entityLiving1).addExhaustion(0.025F * (float)(i2 + 1));
			}
			else if (this.id == heal.id && !entityLiving1.EntityUndead || this.id == harm.id && entityLiving1.EntityUndead)
			{
				entityLiving1.heal(6 << i2);
			}
			else if (this.id == harm.id && !entityLiving1.EntityUndead || this.id == heal.id && entityLiving1.EntityUndead)
			{
				entityLiving1.attackEntityFrom(DamageSource.magic, 6 << i2);
			}

		}

		public virtual void affectEntity(EntityLiving entityLiving1, EntityLiving entityLiving2, int i3, double d4)
		{
			int i6;
			if ((this.id != heal.id || entityLiving2.EntityUndead) && (this.id != harm.id || !entityLiving2.EntityUndead))
			{
				if (this.id == harm.id && !entityLiving2.EntityUndead || this.id == heal.id && entityLiving2.EntityUndead)
				{
					i6 = (int)(d4 * (double)(6 << i3) + 0.5D);
					if (entityLiving1 == null)
					{
						entityLiving2.attackEntityFrom(DamageSource.magic, i6);
					}
					else
					{
						entityLiving2.attackEntityFrom(DamageSource.causeIndirectMagicDamage(entityLiving2, entityLiving1), i6);
					}
				}
			}
			else
			{
				i6 = (int)(d4 * (double)(6 << i3) + 0.5D);
				entityLiving2.heal(i6);
			}

		}

		public virtual bool Instant
		{
			get
			{
				return false;
			}
		}

		public virtual bool isReady(int i1, int i2)
		{
			if (this.id != regeneration.id && this.id != poison.id)
			{
				return this.id == hunger.id;
			}
			else
			{
				int i3 = 25 >> i2;
				return i3 > 0 ? i1 % i3 == 0 : true;
			}
		}

		public virtual Potion setPotionName(string string1)
		{
			this.name = string1;
			return this;
		}

		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		public virtual bool hasStatusIcon()
		{
			return this.statusIconIndex >= 0;
		}

		public virtual int StatusIconIndex
		{
			get
			{
				return this.statusIconIndex;
			}
		}

		public virtual bool BadEffect
		{
			get
			{
				return this.isBadEffect;
			}
		}

		public static string getDurationString(PotionEffect potionEffect0)
		{
			int i1 = potionEffect0.Duration;
			int i2 = i1 / 20;
			int i3 = i2 / 60;
			i2 %= 60;
			return i2 < 10 ? i3 + ":0" + i2 : i3 + ":" + i2;
		}

		protected internal virtual Potion setEffectiveness(double d1)
		{
			this.effectiveness = d1;
			return this;
		}

		public virtual double Effectiveness
		{
			get
			{
				return this.effectiveness;
			}
		}

		public virtual Potion setPotionUnusable()
		{
			this.usable = true;
			return this;
		}

		public virtual bool Usable
		{
			get
			{
				return this.usable;
			}
		}

		public virtual int LiquidColor
		{
			get
			{
				return this.liquidColor;
			}
		}
	}

}