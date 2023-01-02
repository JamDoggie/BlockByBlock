using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class DamageSource
	{
		public static DamageSource inFire = (new DamageSource("inFire")).setFireDamage();
		public static DamageSource onFire = (new DamageSource("onFire")).setDamageBypassesArmor().setFireDamage();
		public static DamageSource lava = (new DamageSource("lava")).setFireDamage();
		public static DamageSource inWall = (new DamageSource("inWall")).setDamageBypassesArmor();
		public static DamageSource drown = (new DamageSource("drown")).setDamageBypassesArmor();
		public static DamageSource starve = (new DamageSource("starve")).setDamageBypassesArmor();
		public static DamageSource cactus = new DamageSource("cactus");
		public static DamageSource fall = (new DamageSource("fall")).setDamageBypassesArmor();
		public static DamageSource outOfWorld = (new DamageSource("outOfWorld")).setDamageBypassesArmor().setDamageAllowedInCreativeMode();
		public static DamageSource generic = (new DamageSource("generic")).setDamageBypassesArmor();
		public static DamageSource explosion = new DamageSource("explosion");
		public static DamageSource magic = (new DamageSource("magic")).setDamageBypassesArmor();
		private bool isUnblockable = false;
		private bool isDamageAllowedInCreativeMode = false;
		private float hungerDamage = 0.3F;
		// JAVA TO C# CONVERTER NOTE: Field name conflicts with a method name of the current type:
		private bool fireDamage;
		private bool projectile;
		public string damageType;

		public static DamageSource causeMobDamage(EntityLiving entityLiving0)
		{
			return new EntityDamageSource("mob", entityLiving0);
		}

		public static DamageSource causePlayerDamage(EntityPlayer entityPlayer0)
		{
			return new EntityDamageSource("player", entityPlayer0);
		}

		public static DamageSource causeArrowDamage(EntityArrow entityArrow0, Entity entity1)
		{
			return (new EntityDamageSourceIndirect("arrow", entityArrow0, entity1)).setProjectile();
		}

		public static DamageSource causeFireballDamage(EntityFireball entityFireball0, Entity entity1)
		{
			return (new EntityDamageSourceIndirect("fireball", entityFireball0, entity1)).setFireDamage().setProjectile();
		}

		public static DamageSource causeThrownDamage(Entity entity0, Entity entity1)
		{
			return (new EntityDamageSourceIndirect("thrown", entity0, entity1)).setProjectile();
		}

		public static DamageSource causeIndirectMagicDamage(Entity entity0, Entity entity1)
		{
			return (new EntityDamageSourceIndirect("indirectMagic", entity0, entity1)).setDamageBypassesArmor();
		}

		public virtual bool Projectile
		{
			get
			{
				return this.projectile;
			}
		}

		public virtual DamageSource setProjectile()
		{
			this.projectile = true;
			return this;
		}

		public virtual bool Unblockable
		{
			get
			{
				return this.isUnblockable;
			}
		}

		public virtual float HungerDamage
		{
			get
			{
				return this.hungerDamage;
			}
		}

		public virtual bool canHarmInCreative()
		{
			return this.isDamageAllowedInCreativeMode;
		}

		protected internal DamageSource(string string1)
		{
			this.damageType = string1;
		}

		public virtual Entity SourceOfDamage
		{
			get
			{
				return this.Entity;
			}
		}

		public virtual Entity Entity
		{
			get
			{
				return null;
			}
		}

		protected internal virtual DamageSource setDamageBypassesArmor()
		{
			this.isUnblockable = true;
			this.hungerDamage = 0.0F;
			return this;
		}

		protected internal virtual DamageSource setDamageAllowedInCreativeMode()
		{
			this.isDamageAllowedInCreativeMode = true;
			return this;
		}

		protected internal virtual DamageSource setFireDamage()
		{
			this.fireDamage = true;
			return this;
		}

		public virtual bool getFireDamage()
		{
			return this.fireDamage;
		}

		public virtual string DamageType
		{
			get
			{
				return this.damageType;
			}
		}
	}

}