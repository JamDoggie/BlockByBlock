using System;
using System.Collections;

namespace net.minecraft.src
{

	public abstract class EntityLiving : Entity
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			health = this.MaxHealth;
		}

		public int heartsHalvesLife = 20;
		public float field_9365_p;
		public float field_9363_r;
		public float renderYawOffset = 0.0F;
		public float prevRenderYawOffset = 0.0F;
		public float rotationYawHead = 0.0F;
		public float prevRotationYawHead = 0.0F;
		protected internal float field_9362_u;
		protected internal float field_9361_v;
		protected internal float field_9360_w;
		protected internal float field_9359_x;
		protected internal bool field_9358_y = true;
		protected internal string texture = "/mob/char.png";
		protected internal bool field_9355_A = true;
		protected internal float field_9353_B = 0.0F;
		protected internal string entityType = null;
		protected internal float field_9349_D = 1.0F;
		protected internal int scoreValue = 0;
		protected internal float field_9345_F = 0.0F;
		public float landMovementFactor = 0.1F;
		public float jumpMovementFactor = 0.02F;
		public float prevSwingProgress;
		public float swingProgress;
		protected internal int health;
		public int prevHealth;
		protected internal int carryoverDamage;
		private int livingSoundTime;
		public int hurtTime;
		public int maxHurtTime;
		public float attackedAtYaw = 0.0F;
		public int deathTime = 0;
		public int attackTime = 0;
		public float prevCameraPitch;
		public float cameraPitch;
		protected internal bool dead = false;
		protected internal int experienceValue;
		public int field_9326_T = -1;
		public float field_9325_U = (float)(MathHelper.NextDouble * (double)0.9F + (double)0.1F);
		public float field_705_Q;
		public float field_704_R;
		public float field_703_S;
		protected internal EntityPlayer attackingPlayer = null;
		protected internal int recentlyHit = 0;
		private EntityLiving entityLivingToAttack = null;
		private int revengeTimer = 0;
		private EntityLiving lastAttackingEntity = null;
		public int arrowHitTempCounter = 0;
		public int arrowHitTimer = 0;
		protected internal Hashtable activePotionsMap = new Hashtable();
		private bool potionsNeedUpdate = true;
		private int field_39002_c;
		private EntityLookHelper lookHelper;
		private EntityMoveHelper moveHelper;
		private EntityJumpHelper jumpHelper;
		private EntityBodyHelper bodyHelper;
		private PathNavigate navigator;
		protected internal EntityAITasks tasks = new EntityAITasks();
		protected internal EntityAITasks targetTasks = new EntityAITasks();
		private EntityLiving attackTarget;
		private EntitySenses field_48104_at;
		private float field_48111_au;
		private ChunkCoordinates homePosition = new ChunkCoordinates(0, 0, 0);
		private float maximumHomeDistance = -1.0F;
		protected internal int newPosRotationIncrements;
		protected internal double newPosX;
		protected internal double newPosY;
		protected internal double newPosZ;
		protected internal double newRotationYaw;
		protected internal double newRotationPitch;
		internal float field_9348_ae = 0.0F;
		protected internal int naturalArmorRating = 0;
		protected internal int entityAge = 0;
		protected internal float moveStrafing;
		protected internal float moveForward;
		protected internal float randomYawVelocity;
		protected internal bool isJumping = false;
		protected internal float defaultPitch = 0.0F;
		protected internal float moveSpeed = 0.7F;
		private int jumpTicks = 0;
		private Entity currentTarget;
		protected internal int numTicksToChaseTarget = 0;

		public EntityLiving(World world1) : base(world1)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.preventEntitySpawning = true;
			this.lookHelper = new EntityLookHelper(this);
			this.moveHelper = new EntityMoveHelper(this);
			this.jumpHelper = new EntityJumpHelper(this);
			this.bodyHelper = new EntityBodyHelper(this);
			this.navigator = new PathNavigate(this, world1, 16.0F);
			this.field_48104_at = new EntitySenses(this);
			this.field_9363_r = (float)(MathHelper.NextDouble + 1.0D) * 0.01F;
			this.setPosition(this.posX, this.posY, this.posZ);
			this.field_9365_p = (float)MathHelper.NextDouble * 12398.0F;
			this.rotationYaw = (float)(MathHelper.NextDouble * (double)(float)Math.PI * 2.0D);
			this.rotationYawHead = this.rotationYaw;
			this.stepHeight = 0.5F;
		}

		public virtual EntityLookHelper LookHelper
		{
			get
			{
				return this.lookHelper;
			}
		}

		public virtual EntityMoveHelper MoveHelper
		{
			get
			{
				return this.moveHelper;
			}
		}

		public virtual EntityJumpHelper JumpHelper
		{
			get
			{
				return this.jumpHelper;
			}
		}

		public virtual PathNavigate Navigator
		{
			get
			{
				return this.navigator;
			}
		}

		public virtual EntitySenses func_48090_aM()
		{
			return this.field_48104_at;
		}

		public virtual Random RNG
		{
			get
			{
				return this.rand;
			}
		}

		public virtual EntityLiving AITarget
		{
			get
			{
				return this.entityLivingToAttack;
			}
		}

		public virtual EntityLiving LastAttackingEntity
		{
			get
			{
				return this.lastAttackingEntity;
			}
			set
			{
				if (value is EntityLiving)
				{
					this.lastAttackingEntity = (EntityLiving)value;
				}
    
			}
		}


		public virtual int Age
		{
			get
			{
				return this.entityAge;
			}
		}

		public override void func_48079_f(float f1)
		{
			this.rotationYawHead = f1;
		}

		public virtual float func_48101_aR()
		{
			return this.field_48111_au;
		}

		public virtual void func_48098_g(float f1)
		{
			this.field_48111_au = f1;
			this.MoveForward = f1;
		}

		public virtual bool attackEntityAsMob(Entity entity1)
		{
			this.setLastAttackingEntity(entity1);
			return false;
		}

		public virtual EntityLiving AttackTarget
		{
			get
			{
				return this.attackTarget;
			}
			set
			{
				this.attackTarget = value;
			}
		}


		public virtual bool func_48100_a(Type class1)
		{
			return typeof(EntityCreeper) != class1 && typeof(EntityGhast) != class1;
		}

		public virtual void eatGrassBonus()
		{
		}

		public virtual bool WithinHomeDistanceCurrentPosition
		{
			get
			{
				return this.isWithinHomeDistance(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ));
			}
		}

		public virtual bool isWithinHomeDistance(int i1, int i2, int i3)
		{
			return this.maximumHomeDistance == -1.0F ? true : this.homePosition.getDistanceSquared(i1, i2, i3) < this.maximumHomeDistance * this.maximumHomeDistance;
		}

		public virtual void setHomeArea(int i1, int i2, int i3, int i4)
		{
			this.homePosition.set(i1, i2, i3);
			this.maximumHomeDistance = (float)i4;
		}

		public virtual ChunkCoordinates HomePosition
		{
			get
			{
				return this.homePosition;
			}
		}

		public virtual float MaximumHomeDistance
		{
			get
			{
				return this.maximumHomeDistance;
			}
		}

		public virtual void detachHome()
		{
			this.maximumHomeDistance = -1.0F;
		}

		public virtual bool hasHome()
		{
			return this.maximumHomeDistance != -1.0F;
		}

		public virtual EntityLiving RevengeTarget
		{
			set
			{
				this.entityLivingToAttack = value;
				this.revengeTimer = this.entityLivingToAttack != null ? 60 : 0;
			}
		}

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(8, this.field_39002_c);
		}

		public virtual bool canEntityBeSeen(Entity entity1)
		{
			return this.worldObj.rayTraceBlocks(Vec3D.createVector(this.posX, this.posY + (double)this.EyeHeight, this.posZ), Vec3D.createVector(entity1.posX, entity1.posY + (double)entity1.EyeHeight, entity1.posZ)) == null;
		}

		public override string Texture
		{
			get
			{
				return this.texture;
			}
		}

		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

		public override bool canBePushed()
		{
			return !this.isDead;
		}

		public override float EyeHeight
		{
			get
			{
				return this.height * 0.85F;
			}
		}

		public virtual int TalkInterval
		{
			get
			{
				return 80;
			}
		}

		public virtual void playLivingSound()
		{
			string string1 = this.LivingSound;
			if (!string.ReferenceEquals(string1, null))
			{
				this.worldObj.playSoundAtEntity(this, string1, this.SoundVolume, this.SoundPitch);
			}

		}

		public override void onEntityUpdate()
		{
			this.prevSwingProgress = this.swingProgress;
			base.onEntityUpdate();
			Profiler.startSection("mobBaseTick");
			if (this.EntityAlive && this.rand.Next(1000) < this.livingSoundTime++)
			{
				this.livingSoundTime = -this.TalkInterval;
				this.playLivingSound();
			}

			if (this.EntityAlive && this.EntityInsideOpaqueBlock && this.attackEntityFrom(DamageSource.inWall, 1))
			{
				;
			}

			if (this.ImmuneToFire || this.worldObj.isRemote)
			{
				this.extinguish();
			}

			if (this.EntityAlive && this.isInsideOfMaterial(Material.water) && !this.canBreatheUnderwater() && !this.activePotionsMap.ContainsKey(Potion.waterBreathing.id))
			{
				this.Air = this.decreaseAirSupply(this.Air);
				if (this.Air == -20)
				{
					this.Air = 0;

					for (int i1 = 0; i1 < 8; ++i1)
					{
						float f2 = this.rand.nextFloat() - this.rand.nextFloat();
						float f3 = this.rand.nextFloat() - this.rand.nextFloat();
						float f4 = this.rand.nextFloat() - this.rand.nextFloat();
						this.worldObj.spawnParticle("bubble", this.posX + (double)f2, this.posY + (double)f3, this.posZ + (double)f4, this.motionX, this.motionY, this.motionZ);
					}

					this.attackEntityFrom(DamageSource.drown, 2);
				}

				this.extinguish();
			}
			else
			{
				this.Air = 300;
			}

			this.prevCameraPitch = this.cameraPitch;
			if (this.attackTime > 0)
			{
				--this.attackTime;
			}

			if (this.hurtTime > 0)
			{
				--this.hurtTime;
			}

			if (this.heartsLife > 0)
			{
				--this.heartsLife;
			}

			if (this.health <= 0)
			{
				this.onDeathUpdate();
			}

			if (this.recentlyHit > 0)
			{
				--this.recentlyHit;
			}
			else
			{
				this.attackingPlayer = null;
			}

			if (this.lastAttackingEntity != null && !this.lastAttackingEntity.EntityAlive)
			{
				this.lastAttackingEntity = null;
			}

			if (this.entityLivingToAttack != null)
			{
				if (!this.entityLivingToAttack.EntityAlive)
				{
					this.RevengeTarget = (EntityLiving)null;
				}
				else if (this.revengeTimer > 0)
				{
					--this.revengeTimer;
				}
				else
				{
					this.RevengeTarget = (EntityLiving)null;
				}
			}

			this.updatePotionEffects();
			this.field_9359_x = this.field_9360_w;
			this.prevRenderYawOffset = this.renderYawOffset;
			this.prevRotationYawHead = this.rotationYawHead;
			this.prevRotationYaw = this.rotationYaw;
			this.prevRotationPitch = this.rotationPitch;
			Profiler.endSection();
		}

		protected internal virtual void onDeathUpdate()
		{
			++this.deathTime;
			if (this.deathTime == 20)
			{
				int i1;
				if (!this.worldObj.isRemote && (this.recentlyHit > 0 || this.Player) && !this.Child)
				{
					i1 = this.getExperiencePoints(this.attackingPlayer);

					while (i1 > 0)
					{
						int i2 = EntityXPOrb.getXPSplit(i1);
						i1 -= i2;
						this.worldObj.spawnEntityInWorld(new EntityXPOrb(this.worldObj, this.posX, this.posY, this.posZ, i2));
					}
				}

				this.onEntityDeath();
				this.setDead();

				for (i1 = 0; i1 < 20; ++i1)
				{
					double d8 = this.rand.nextGaussian() * 0.02D;
					double d4 = this.rand.nextGaussian() * 0.02D;
					double d6 = this.rand.nextGaussian() * 0.02D;
					this.worldObj.spawnParticle("explode", this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, d8, d4, d6);
				}
			}

		}

		protected internal virtual int decreaseAirSupply(int i1)
		{
			return i1 - 1;
		}

		protected internal virtual int getExperiencePoints(EntityPlayer entityPlayer1)
		{
			return this.experienceValue;
		}

		protected internal virtual bool Player
		{
			get
			{
				return false;
			}
		}

		public virtual void spawnExplosionParticle()
		{
			for (int i1 = 0; i1 < 20; ++i1)
			{
				double d2 = this.rand.nextGaussian() * 0.02D;
				double d4 = this.rand.nextGaussian() * 0.02D;
				double d6 = this.rand.nextGaussian() * 0.02D;
				double d8 = 10.0D;
				this.worldObj.spawnParticle("explode", this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width - d2 * d8, this.posY + (double)(this.rand.nextFloat() * this.height) - d4 * d8, this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width - d6 * d8, d2, d4, d6);
			}

		}

		public override void updateRidden()
		{
			base.updateRidden();
			this.field_9362_u = this.field_9361_v;
			this.field_9361_v = 0.0F;
			this.fallDistance = 0.0F;
		}

		public override void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
		{
			this.yOffset = 0.0F;
			this.newPosX = d1;
			this.newPosY = d3;
			this.newPosZ = d5;
			this.newRotationYaw = (double)f7;
			this.newRotationPitch = (double)f8;
			this.newPosRotationIncrements = i9;
		}

		public override void onUpdate()
		{
			base.onUpdate();
			if (this.arrowHitTempCounter > 0)
			{
				if (this.arrowHitTimer <= 0)
				{
					this.arrowHitTimer = 60;
				}

				--this.arrowHitTimer;
				if (this.arrowHitTimer <= 0)
				{
					--this.arrowHitTempCounter;
				}
			}

			this.onLivingUpdate();
			double d1 = this.posX - this.prevPosX;
			double d3 = this.posZ - this.prevPosZ;
			float f5 = MathHelper.sqrt_double(d1 * d1 + d3 * d3);
			float f6 = this.renderYawOffset;
			float f7 = 0.0F;
			this.field_9362_u = this.field_9361_v;
			float f8 = 0.0F;
			if (f5 > 0.05F)
			{
				f8 = 1.0F;
				f7 = f5 * 3.0F;
				f6 = (float)Math.Atan2(d3, d1) * 180.0F / (float)Math.PI - 90.0F;
			}

			if (this.swingProgress > 0.0F)
			{
				f6 = this.rotationYaw;
			}

			if (!this.onGround)
			{
				f8 = 0.0F;
			}

			this.field_9361_v += (f8 - this.field_9361_v) * 0.3F;
			if (this.AIEnabled)
			{
				this.bodyHelper.func_48650_a();
			}
			else
			{
				float f9;
				for (f9 = f6 - this.renderYawOffset; f9 < -180.0F; f9 += 360.0F)
				{
				}

				while (f9 >= 180.0F)
				{
					f9 -= 360.0F;
				}

				this.renderYawOffset += f9 * 0.3F;

				float f10;
				for (f10 = this.rotationYaw - this.renderYawOffset; f10 < -180.0F; f10 += 360.0F)
				{
				}

				while (f10 >= 180.0F)
				{
					f10 -= 360.0F;
				}

				bool z11 = f10 < -90.0F || f10 >= 90.0F;
				if (f10 < -75.0F)
				{
					f10 = -75.0F;
				}

				if (f10 >= 75.0F)
				{
					f10 = 75.0F;
				}

				this.renderYawOffset = this.rotationYaw - f10;
				if (f10 * f10 > 2500.0F)
				{
					this.renderYawOffset += f10 * 0.2F;
				}

				if (z11)
				{
					f7 *= -1.0F;
				}
			}

			while (this.rotationYaw - this.prevRotationYaw < -180.0F)
			{
				this.prevRotationYaw -= 360.0F;
			}

			while (this.rotationYaw - this.prevRotationYaw >= 180.0F)
			{
				this.prevRotationYaw += 360.0F;
			}

			while (this.renderYawOffset - this.prevRenderYawOffset < -180.0F)
			{
				this.prevRenderYawOffset -= 360.0F;
			}

			while (this.renderYawOffset - this.prevRenderYawOffset >= 180.0F)
			{
				this.prevRenderYawOffset += 360.0F;
			}

			while (this.rotationPitch - this.prevRotationPitch < -180.0F)
			{
				this.prevRotationPitch -= 360.0F;
			}

			while (this.rotationPitch - this.prevRotationPitch >= 180.0F)
			{
				this.prevRotationPitch += 360.0F;
			}

			while (this.rotationYawHead - this.prevRotationYawHead < -180.0F)
			{
				this.prevRotationYawHead -= 360.0F;
			}

			while (this.rotationYawHead - this.prevRotationYawHead >= 180.0F)
			{
				this.prevRotationYawHead += 360.0F;
			}

			this.field_9360_w += f7;
		}

		protected internal override void setSize(float f1, float f2)
		{
			base.setSize(f1, f2);
		}

		public virtual void heal(int i1)
		{
			if (this.health > 0)
			{
				this.health += i1;
				if (this.health > this.MaxHealth)
				{
					this.health = this.MaxHealth;
				}

				this.heartsLife = this.heartsHalvesLife / 2;
			}
		}

		public abstract int MaxHealth {get;}

		public virtual int Health
		{
			get
			{
				return this.health;
			}
		}

		public virtual int EntityHealth
		{
			set
			{
				this.health = value;
				if (value > this.MaxHealth)
				{
					value = this.MaxHealth;
				}
    
			}
		}

		public override bool attackEntityFrom(DamageSource damageSource1, int i2)
		{
			if (this.worldObj.isRemote)
			{
				return false;
			}
			else
			{
				this.entityAge = 0;
				if (this.health <= 0)
				{
					return false;
				}
				else if (damageSource1.fireDamage() && this.isPotionActive(Potion.fireResistance))
				{
					return false;
				}
				else
				{
					this.field_704_R = 1.5F;
					bool z3 = true;
					if ((float)this.heartsLife > (float)this.heartsHalvesLife / 2.0F)
					{
						if (i2 <= this.naturalArmorRating)
						{
							return false;
						}

						this.damageEntity(damageSource1, i2 - this.naturalArmorRating);
						this.naturalArmorRating = i2;
						z3 = false;
					}
					else
					{
						this.naturalArmorRating = i2;
						this.prevHealth = this.health;
						this.heartsLife = this.heartsHalvesLife;
						this.damageEntity(damageSource1, i2);
						this.hurtTime = this.maxHurtTime = 10;
					}

					this.attackedAtYaw = 0.0F;
					Entity entity4 = damageSource1.Entity;
					if (entity4 != null)
					{
						if (entity4 is EntityLiving)
						{
							this.RevengeTarget = (EntityLiving)entity4;
						}

						if (entity4 is EntityPlayer)
						{
							this.recentlyHit = 60;
							this.attackingPlayer = (EntityPlayer)entity4;
						}
						else if (entity4 is EntityWolf)
						{
							EntityWolf entityWolf5 = (EntityWolf)entity4;
							if (entityWolf5.Tamed)
							{
								this.recentlyHit = 60;
								this.attackingPlayer = null;
							}
						}
					}

					if (z3)
					{
						this.worldObj.setEntityState(this, (sbyte)2);
						this.setBeenAttacked();
						if (entity4 != null)
						{
							double d9 = entity4.posX - this.posX;

							double d7;
							for (d7 = entity4.posZ - this.posZ; d9 * d9 + d7 * d7 < 1.0E-4D; d7 = (MathHelper.NextDouble - MathHelper.NextDouble) * 0.01D)
							{
								d9 = (MathHelper.NextDouble - MathHelper.NextDouble) * 0.01D;
							}

							this.attackedAtYaw = (float)(Math.Atan2(d7, d9) * 180.0D / (double)(float)Math.PI) - this.rotationYaw;
							this.knockBack(entity4, i2, d9, d7);
						}
						else
						{
							this.attackedAtYaw = (float)((int)(MathHelper.NextDouble * 2.0D) * 180);
						}
					}

					if (this.health <= 0)
					{
						if (z3)
						{
							this.worldObj.playSoundAtEntity(this, this.DeathSound, this.SoundVolume, this.SoundPitch);
						}

						this.onDeath(damageSource1);
					}
					else if (z3)
					{
						this.worldObj.playSoundAtEntity(this, this.HurtSound, this.SoundVolume, this.SoundPitch);
					}

					return true;
				}
			}
		}

		private float SoundPitch
		{
			get
			{
				return this.Child ? (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.5F : (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F;
			}
		}

		public override void performHurtAnimation()
		{
			this.hurtTime = this.maxHurtTime = 10;
			this.attackedAtYaw = 0.0F;
		}

		public virtual int TotalArmorValue
		{
			get
			{
				return 0;
			}
		}

		protected internal virtual void damageArmor(int i1)
		{
		}

		protected internal virtual int applyArmorCalculations(DamageSource damageSource1, int i2)
		{
			if (!damageSource1.Unblockable)
			{
				int i3 = 25 - this.TotalArmorValue;
				int i4 = i2 * i3 + this.carryoverDamage;
				this.damageArmor(i2);
				i2 = i4 / 25;
				this.carryoverDamage = i4 % 25;
			}

			return i2;
		}

		protected internal virtual int applyPotionDamageCalculations(DamageSource damageSource1, int i2)
		{
			if (this.isPotionActive(Potion.resistance))
			{
				int i3 = (this.getActivePotionEffect(Potion.resistance).Amplifier + 1) * 5;
				int i4 = 25 - i3;
				int i5 = i2 * i4 + this.carryoverDamage;
				i2 = i5 / 25;
				this.carryoverDamage = i5 % 25;
			}

			return i2;
		}

		protected internal virtual void damageEntity(DamageSource damageSource1, int i2)
		{
			i2 = this.applyArmorCalculations(damageSource1, i2);
			i2 = this.applyPotionDamageCalculations(damageSource1, i2);
			this.health -= i2;
		}

		protected internal virtual float SoundVolume
		{
			get
			{
				return 1.0F;
			}
		}

		protected internal virtual string LivingSound
		{
			get
			{
				return null;
			}
		}

		protected internal virtual string HurtSound
		{
			get
			{
				return "damage.hurtflesh";
			}
		}

		protected internal virtual string DeathSound
		{
			get
			{
				return "damage.hurtflesh";
			}
		}

		public virtual void knockBack(Entity entity1, int i2, double d3, double d5)
		{
			this.isAirBorne = true;
			float f7 = MathHelper.sqrt_double(d3 * d3 + d5 * d5);
			float f8 = 0.4F;
			this.motionX /= 2.0D;
			this.motionY /= 2.0D;
			this.motionZ /= 2.0D;
			this.motionX -= d3 / (double)f7 * (double)f8;
			this.motionY += (double)f8;
			this.motionZ -= d5 / (double)f7 * (double)f8;
			if (this.motionY > (double)0.4F)
			{
				this.motionY = (double)0.4F;
			}

		}

		public virtual void onDeath(DamageSource damageSource1)
		{
			Entity entity2 = damageSource1.Entity;
			if (this.scoreValue >= 0 && entity2 != null)
			{
				entity2.addToPlayerScore(this, this.scoreValue);
			}

			if (entity2 != null)
			{
				entity2.onKillEntity(this);
			}

			this.dead = true;
			if (!this.worldObj.isRemote)
			{
				int i3 = 0;
				if (entity2 is EntityPlayer)
				{
					i3 = EnchantmentHelper.getLootingModifier(((EntityPlayer)entity2).inventory);
				}

				if (!this.Child)
				{
					this.dropFewItems(this.recentlyHit > 0, i3);
					if (this.recentlyHit > 0)
					{
						int i4 = this.rand.Next(200) - i3;
						if (i4 < 5)
						{
							this.dropRareDrop(i4 <= 0 ? 1 : 0);
						}
					}
				}
			}

			this.worldObj.setEntityState(this, (sbyte)3);
		}

		protected internal virtual void dropRareDrop(int i1)
		{
		}

		protected internal virtual void dropFewItems(bool z1, int i2)
		{
			int i3 = this.DropItemId;
			if (i3 > 0)
			{
				int i4 = this.rand.Next(3);
				if (i2 > 0)
				{
					i4 += this.rand.Next(i2 + 1);
				}

				for (int i5 = 0; i5 < i4; ++i5)
				{
					this.dropItem(i3, 1);
				}
			}

		}

		protected internal virtual int DropItemId
		{
			get
			{
				return 0;
			}
		}

		protected internal override void fall(float f1)
		{
			base.fall(f1);
			int i2 = (int)Math.Ceiling((double)(f1 - 3.0F));
			if (i2 > 0)
			{
				if (i2 > 4)
				{
					this.worldObj.playSoundAtEntity(this, "damage.fallbig", 1.0F, 1.0F);
				}
				else
				{
					this.worldObj.playSoundAtEntity(this, "damage.fallsmall", 1.0F, 1.0F);
				}

				this.attackEntityFrom(DamageSource.fall, i2);
				int i3 = this.worldObj.getBlockId(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY - (double)0.2F - (double)this.yOffset), MathHelper.floor_double(this.posZ));
				if (i3 > 0)
				{
					StepSound stepSound4 = Block.blocksList[i3].stepSound;
					this.worldObj.playSoundAtEntity(this, stepSound4.StepSound, stepSound4.Volume * 0.5F, stepSound4.Pitch * 0.75F);
				}
			}

		}

		public virtual void moveEntityWithHeading(float f1, float f2)
		{
			double d3;
			if (this.InWater)
			{
				d3 = this.posY;
				this.moveFlying(f1, f2, this.AIEnabled ? 0.04F : 0.02F);
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= (double)0.8F;
				this.motionY *= (double)0.8F;
				this.motionZ *= (double)0.8F;
				this.motionY -= 0.02D;
				if (this.isCollidedHorizontally && this.isOffsetPositionInLiquid(this.motionX, this.motionY + (double)0.6F - this.posY + d3, this.motionZ))
				{
					this.motionY = (double)0.3F;
				}
			}
			else if (this.handleLavaMovement())
			{
				d3 = this.posY;
				this.moveFlying(f1, f2, 0.02F);
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= 0.5D;
				this.motionY *= 0.5D;
				this.motionZ *= 0.5D;
				this.motionY -= 0.02D;
				if (this.isCollidedHorizontally && this.isOffsetPositionInLiquid(this.motionX, this.motionY + (double)0.6F - this.posY + d3, this.motionZ))
				{
					this.motionY = (double)0.3F;
				}
			}
			else
			{
				float f8 = 0.91F;
				if (this.onGround)
				{
					f8 = 0.54600006F;
					int i4 = this.worldObj.getBlockId(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ));
					if (i4 > 0)
					{
						f8 = Block.blocksList[i4].slipperiness * 0.91F;
					}
				}

				float f9 = 0.16277136F / (f8 * f8 * f8);
				float f5;
				if (this.onGround)
				{
					if (this.AIEnabled)
					{
						f5 = this.func_48101_aR();
					}
					else
					{
						f5 = this.landMovementFactor;
					}

					f5 *= f9;
				}
				else
				{
					f5 = this.jumpMovementFactor;
				}

				this.moveFlying(f1, f2, f5);
				f8 = 0.91F;
				if (this.onGround)
				{
					f8 = 0.54600006F;
					int i6 = this.worldObj.getBlockId(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ));
					if (i6 > 0)
					{
						f8 = Block.blocksList[i6].slipperiness * 0.91F;
					}
				}

				if (this.OnLadder)
				{
					float f10 = 0.15F;
					if (this.motionX < (double)(-f10))
					{
						this.motionX = (double)(-f10);
					}

					if (this.motionX > (double)f10)
					{
						this.motionX = (double)f10;
					}

					if (this.motionZ < (double)(-f10))
					{
						this.motionZ = (double)(-f10);
					}

					if (this.motionZ > (double)f10)
					{
						this.motionZ = (double)f10;
					}

					this.fallDistance = 0.0F;
					if (this.motionY < -0.15D)
					{
						this.motionY = -0.15D;
					}

					bool z7 = this.Sneaking && this is EntityPlayer;
					if (z7 && this.motionY < 0.0D)
					{
						this.motionY = 0.0D;
					}
				}

				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				if (this.isCollidedHorizontally && this.OnLadder)
				{
					this.motionY = 0.2D;
				}

				this.motionY -= 0.08D;
				this.motionY *= (double)0.98F;
				this.motionX *= (double)f8;
				this.motionZ *= (double)f8;
			}

			this.field_705_Q = this.field_704_R;
			d3 = this.posX - this.prevPosX;
			double d11 = this.posZ - this.prevPosZ;
			float f12 = MathHelper.sqrt_double(d3 * d3 + d11 * d11) * 4.0F;
			if (f12 > 1.0F)
			{
				f12 = 1.0F;
			}

			this.field_704_R += (f12 - this.field_704_R) * 0.4F;
			this.field_703_S += this.field_704_R;
		}

		public virtual bool OnLadder
		{
			get
			{
				int i1 = MathHelper.floor_double(this.posX);
				int i2 = MathHelper.floor_double(this.boundingBox.minY);
				int i3 = MathHelper.floor_double(this.posZ);
				int i4 = this.worldObj.getBlockId(i1, i2, i3);
				return i4 == Block.ladder.blockID || i4 == Block.vine.blockID;
			}
		}

		public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
		{
			nBTTagCompound1.setShort("Health", (short)this.health);
			nBTTagCompound1.setShort("HurtTime", (short)this.hurtTime);
			nBTTagCompound1.setShort("DeathTime", (short)this.deathTime);
			nBTTagCompound1.setShort("AttackTime", (short)this.attackTime);
			if (this.activePotionsMap.Count > 0)
			{
				NBTTagList nBTTagList2 = new NBTTagList();
				System.Collections.IEnumerator iterator3 = this.activePotionsMap.Values.GetEnumerator();

				while (iterator3.MoveNext())
				{
					PotionEffect potionEffect4 = (PotionEffect)iterator3.Current;
					NBTTagCompound nBTTagCompound5 = new NBTTagCompound();
					nBTTagCompound5.setByte("Id", (sbyte)potionEffect4.PotionID);
					nBTTagCompound5.setByte("Amplifier", (sbyte)potionEffect4.Amplifier);
					nBTTagCompound5.setInteger("Duration", potionEffect4.Duration);
					nBTTagList2.appendTag(nBTTagCompound5);
				}

				nBTTagCompound1.setTag("ActiveEffects", nBTTagList2);
			}

		}

		public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
		{
			if (this.health < -32768)
			{
				this.health = -32768;
			}

			this.health = nBTTagCompound1.getShort("Health");
			if (!nBTTagCompound1.hasKey("Health"))
			{
				this.health = this.MaxHealth;
			}

			this.hurtTime = nBTTagCompound1.getShort("HurtTime");
			this.deathTime = nBTTagCompound1.getShort("DeathTime");
			this.attackTime = nBTTagCompound1.getShort("AttackTime");
			if (nBTTagCompound1.hasKey("ActiveEffects"))
			{
				NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("ActiveEffects");

				for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
				{
					NBTTagCompound nBTTagCompound4 = (NBTTagCompound)nBTTagList2.tagAt(i3);
					sbyte b5 = nBTTagCompound4.getByte("Id");
					sbyte b6 = nBTTagCompound4.getByte("Amplifier");
					int i7 = nBTTagCompound4.getInteger("Duration");
					this.activePotionsMap[Convert.ToInt32(b5)] = new PotionEffect(b5, i7, b6);
				}
			}

		}

		public override bool EntityAlive
		{
			get
			{
				return !this.isDead && this.health > 0;
			}
		}

		public virtual bool canBreatheUnderwater()
		{
			return false;
		}

		public virtual float MoveForward
		{
			set
			{
				this.moveForward = value;
			}
		}

		public virtual bool Jumping
		{
			set
			{
				this.isJumping = value;
			}
		}

		public virtual void onLivingUpdate()
		{
			if (this.jumpTicks > 0)
			{
				--this.jumpTicks;
			}

			if (this.newPosRotationIncrements > 0)
			{
				double d1 = this.posX + (this.newPosX - this.posX) / (double)this.newPosRotationIncrements;
				double d3 = this.posY + (this.newPosY - this.posY) / (double)this.newPosRotationIncrements;
				double d5 = this.posZ + (this.newPosZ - this.posZ) / (double)this.newPosRotationIncrements;

				double d7;
				for (d7 = this.newRotationYaw - (double)this.rotationYaw; d7 < -180.0D; d7 += 360.0D)
				{
				}

				while (d7 >= 180.0D)
				{
					d7 -= 360.0D;
				}

				this.rotationYaw = (float)((double)this.rotationYaw + d7 / (double)this.newPosRotationIncrements);
				this.rotationPitch = (float)((double)this.rotationPitch + (this.newRotationPitch - (double)this.rotationPitch) / (double)this.newPosRotationIncrements);
				--this.newPosRotationIncrements;
				this.setPosition(d1, d3, d5);
				this.setRotation(this.rotationYaw, this.rotationPitch);
				System.Collections.IList list9 = this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox.contract(8.0D / 256D, 0.0D, 8.0D / 256D));
				if (list9.Count > 0)
				{
					double d10 = 0.0D;

					for (int i12 = 0; i12 < list9.Count; ++i12)
					{
						AxisAlignedBB axisAlignedBB13 = (AxisAlignedBB)list9[i12];
						if (axisAlignedBB13.maxY > d10)
						{
							d10 = axisAlignedBB13.maxY;
						}
					}

					d3 += d10 - this.boundingBox.minY;
					this.setPosition(d1, d3, d5);
				}
			}

			Profiler.startSection("ai");
			if (this.MovementBlocked)
			{
				this.isJumping = false;
				this.moveStrafing = 0.0F;
				this.moveForward = 0.0F;
				this.randomYawVelocity = 0.0F;
			}
			else if (this.ClientWorld)
			{
				if (this.AIEnabled)
				{
					Profiler.startSection("newAi");
					this.updateAITasks();
					Profiler.endSection();
				}
				else
				{
					Profiler.startSection("oldAi");
					this.updateEntityActionState();
					Profiler.endSection();
					this.rotationYawHead = this.rotationYaw;
				}
			}

			Profiler.endSection();
			bool z14 = this.InWater;
			bool z2 = this.handleLavaMovement();
			if (this.isJumping)
			{
				if (z14)
				{
					this.motionY += (double)0.04F;
				}
				else if (z2)
				{
					this.motionY += (double)0.04F;
				}
				else if (this.onGround && this.jumpTicks == 0)
				{
					this.jump();
					this.jumpTicks = 10;
				}
			}
			else
			{
				this.jumpTicks = 0;
			}

			this.moveStrafing *= 0.98F;
			this.moveForward *= 0.98F;
			this.randomYawVelocity *= 0.9F;
			float f15 = this.landMovementFactor;
			this.landMovementFactor *= this.SpeedModifier;
			this.moveEntityWithHeading(this.moveStrafing, this.moveForward);
			this.landMovementFactor = f15;
			Profiler.startSection("push");
			System.Collections.IList list4 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand((double)0.2F, 0.0D, (double)0.2F));
			if (list4 != null && list4.Count > 0)
			{
				for (int i16 = 0; i16 < list4.Count; ++i16)
				{
					Entity entity6 = (Entity)list4[i16];
					if (entity6.canBePushed())
					{
						entity6.applyEntityCollision(this);
					}
				}
			}

			Profiler.endSection();
		}

		protected internal virtual bool AIEnabled
		{
			get
			{
				return false;
			}
		}

		protected internal virtual bool ClientWorld
		{
			get
			{
				return !this.worldObj.isRemote;
			}
		}

		protected internal virtual bool MovementBlocked
		{
			get
			{
				return this.health <= 0;
			}
		}

		public virtual bool Blocking
		{
			get
			{
				return false;
			}
		}

		protected internal virtual void jump()
		{
			this.motionY = (double)0.42F;
			if (this.isPotionActive(Potion.jump))
			{
				this.motionY += (double)((float)(this.getActivePotionEffect(Potion.jump).Amplifier + 1) * 0.1F);
			}

			if (this.Sprinting)
			{
				float f1 = this.rotationYaw * 0.017453292F;
				this.motionX -= (double)(MathHelper.sin(f1) * 0.2F);
				this.motionZ += (double)(MathHelper.cos(f1) * 0.2F);
			}

			this.isAirBorne = true;
		}

		protected internal virtual bool canDespawn()
		{
			return true;
		}

		protected internal virtual void despawnEntity()
		{
			EntityPlayer entityPlayer1 = this.worldObj.getClosestPlayerToEntity(this, -1.0D);
			if (entityPlayer1 != null)
			{
				double d2 = entityPlayer1.posX - this.posX;
				double d4 = entityPlayer1.posY - this.posY;
				double d6 = entityPlayer1.posZ - this.posZ;
				double d8 = d2 * d2 + d4 * d4 + d6 * d6;
				if (this.canDespawn() && d8 > 16384.0D)
				{
					this.setDead();
				}

				if (this.entityAge > 600 && this.rand.Next(800) == 0 && d8 > 1024.0D && this.canDespawn())
				{
					this.setDead();
				}
				else if (d8 < 1024.0D)
				{
					this.entityAge = 0;
				}
			}

		}

		protected internal virtual void updateAITasks()
		{
			++this.entityAge;
			Profiler.startSection("checkDespawn");
			this.despawnEntity();
			Profiler.endSection();
			Profiler.startSection("sensing");
			this.field_48104_at.clearSensingCache();
			Profiler.endSection();
			Profiler.startSection("targetSelector");
			this.targetTasks.onUpdateTasks();
			Profiler.endSection();
			Profiler.startSection("goalSelector");
			this.tasks.onUpdateTasks();
			Profiler.endSection();
			Profiler.startSection("navigation");
			this.navigator.onUpdateNavigation();
			Profiler.endSection();
			Profiler.startSection("mob tick");
			this.updateAITick();
			Profiler.endSection();
			Profiler.startSection("controls");
			this.moveHelper.onUpdateMoveHelper();
			this.lookHelper.onUpdateLook();
			this.jumpHelper.doJump();
			Profiler.endSection();
		}

		protected internal virtual void updateAITick()
		{
		}

		protected internal virtual void updateEntityActionState()
		{
			++this.entityAge;
			this.despawnEntity();
			this.moveStrafing = 0.0F;
			this.moveForward = 0.0F;
			float f1 = 8.0F;
			if (this.rand.nextFloat() < 0.02F)
			{
				EntityPlayer entityPlayer2 = this.worldObj.getClosestPlayerToEntity(this, (double)f1);
				if (entityPlayer2 != null)
				{
					this.currentTarget = entityPlayer2;
					this.numTicksToChaseTarget = 10 + this.rand.Next(20);
				}
				else
				{
					this.randomYawVelocity = (this.rand.nextFloat() - 0.5F) * 20.0F;
				}
			}

			if (this.currentTarget != null)
			{
				this.faceEntity(this.currentTarget, 10.0F, (float)this.VerticalFaceSpeed);
				if (this.numTicksToChaseTarget-- <= 0 || this.currentTarget.isDead || this.currentTarget.getDistanceSqToEntity(this) > (double)(f1 * f1))
				{
					this.currentTarget = null;
				}
			}
			else
			{
				if (this.rand.nextFloat() < 0.05F)
				{
					this.randomYawVelocity = (this.rand.nextFloat() - 0.5F) * 20.0F;
				}

				this.rotationYaw += this.randomYawVelocity;
				this.rotationPitch = this.defaultPitch;
			}

			bool z4 = this.InWater;
			bool z3 = this.handleLavaMovement();
			if (z4 || z3)
			{
				this.isJumping = this.rand.nextFloat() < 0.8F;
			}

		}

		public virtual int VerticalFaceSpeed
		{
			get
			{
				return 40;
			}
		}

		public virtual void faceEntity(Entity entity1, float f2, float f3)
		{
			double d4 = entity1.posX - this.posX;
			double d8 = entity1.posZ - this.posZ;
			double d6;
			if (entity1 is EntityLiving)
			{
				EntityLiving entityLiving10 = (EntityLiving)entity1;
				d6 = this.posY + (double)this.EyeHeight - (entityLiving10.posY + (double)entityLiving10.EyeHeight);
			}
			else
			{
				d6 = (entity1.boundingBox.minY + entity1.boundingBox.maxY) / 2.0D - (this.posY + (double)this.EyeHeight);
			}

			double d14 = (double)MathHelper.sqrt_double(d4 * d4 + d8 * d8);
			float f12 = (float)(Math.Atan2(d8, d4) * 180.0D / (double)(float)Math.PI) - 90.0F;
			float f13 = (float)(-(Math.Atan2(d6, d14) * 180.0D / (double)(float)Math.PI));
			this.rotationPitch = -this.updateRotation(this.rotationPitch, f13, f3);
			this.rotationYaw = this.updateRotation(this.rotationYaw, f12, f2);
		}

		private float updateRotation(float f1, float f2, float f3)
		{
			float f4;
			for (f4 = f2 - f1; f4 < -180.0F; f4 += 360.0F)
			{
			}

			while (f4 >= 180.0F)
			{
				f4 -= 360.0F;
			}

			if (f4 > f3)
			{
				f4 = f3;
			}

			if (f4 < -f3)
			{
				f4 = -f3;
			}

			return f1 + f4;
		}

		public virtual void onEntityDeath()
		{
		}

		public virtual bool CanSpawnHere
		{
			get
			{
				return this.worldObj.checkIfAABBIsClear(this.boundingBox) && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Count == 0 && !this.worldObj.isAnyLiquid(this.boundingBox);
			}
		}

		protected internal override void kill()
		{
			this.attackEntityFrom(DamageSource.outOfWorld, 4);
		}

		public virtual float getSwingProgress(float f1)
		{
			float f2 = this.swingProgress - this.prevSwingProgress;
			if (f2 < 0.0F)
			{
				++f2;
			}

			return this.prevSwingProgress + f2 * f1;
		}

		public virtual Vec3D getPosition(float f1)
		{
			if (f1 == 1.0F)
			{
				return Vec3D.createVector(this.posX, this.posY, this.posZ);
			}
			else
			{
				double d2 = this.prevPosX + (this.posX - this.prevPosX) * (double)f1;
				double d4 = this.prevPosY + (this.posY - this.prevPosY) * (double)f1;
				double d6 = this.prevPosZ + (this.posZ - this.prevPosZ) * (double)f1;
				return Vec3D.createVector(d2, d4, d6);
			}
		}

		public override Vec3D LookVec
		{
			get
			{
				return this.getLook(1.0F);
			}
		}

		public virtual Vec3D getLook(float f1)
		{
			float f2;
			float f3;
			float f4;
			float f5;
			if (f1 == 1.0F)
			{
				f2 = MathHelper.cos(-this.rotationYaw * 0.017453292F - (float)Math.PI);
				f3 = MathHelper.sin(-this.rotationYaw * 0.017453292F - (float)Math.PI);
				f4 = -MathHelper.cos(-this.rotationPitch * 0.017453292F);
				f5 = MathHelper.sin(-this.rotationPitch * 0.017453292F);
				return Vec3D.createVector((double)(f3 * f4), (double)f5, (double)(f2 * f4));
			}
			else
			{
				f2 = this.prevRotationPitch + (this.rotationPitch - this.prevRotationPitch) * f1;
				f3 = this.prevRotationYaw + (this.rotationYaw - this.prevRotationYaw) * f1;
				f4 = MathHelper.cos(-f3 * 0.017453292F - (float)Math.PI);
				f5 = MathHelper.sin(-f3 * 0.017453292F - (float)Math.PI);
				float f6 = -MathHelper.cos(-f2 * 0.017453292F);
				float f7 = MathHelper.sin(-f2 * 0.017453292F);
				return Vec3D.createVector((double)(f5 * f6), (double)f7, (double)(f4 * f6));
			}
		}

		public virtual float RenderSizeModifier
		{
			get
			{
				return 1.0F;
			}
		}

		public virtual MovingObjectPosition rayTrace(double d1, float f3)
		{
			Vec3D vec3D4 = this.getPosition(f3);
			Vec3D vec3D5 = this.getLook(f3);
			Vec3D vec3D6 = vec3D4.addVector(vec3D5.xCoord * d1, vec3D5.yCoord * d1, vec3D5.zCoord * d1);
			return this.worldObj.rayTraceBlocks(vec3D4, vec3D6);
		}

		public virtual int MaxSpawnedInChunk
		{
			get
			{
				return 4;
			}
		}

		public virtual ItemStack HeldItem
		{
			get
			{
				return null;
			}
		}

		public override void handleHealthUpdate(sbyte b1)
		{
			if (b1 == 2)
			{
				this.field_704_R = 1.5F;
				this.heartsLife = this.heartsHalvesLife;
				this.hurtTime = this.maxHurtTime = 10;
				this.attackedAtYaw = 0.0F;
				this.worldObj.playSoundAtEntity(this, this.HurtSound, this.SoundVolume, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				this.attackEntityFrom(DamageSource.generic, 0);
			}
			else if (b1 == 3)
			{
				this.worldObj.playSoundAtEntity(this, this.DeathSound, this.SoundVolume, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				this.health = 0;
				this.onDeath(DamageSource.generic);
			}
			else
			{
				base.handleHealthUpdate(b1);
			}

		}

		public virtual bool PlayerSleeping
		{
			get
			{
				return false;
			}
		}

		public virtual int getItemIcon(ItemStack itemStack1, int i2)
		{
			return itemStack1.IconIndex;
		}

		protected internal virtual void updatePotionEffects()
		{
			System.Collections.IEnumerator iterator1 = this.activePotionsMap.Keys.GetEnumerator();

			while (iterator1.MoveNext())
			{
				int? integer2 = (int?)iterator1.Current;
				PotionEffect potionEffect3 = (PotionEffect)this.activePotionsMap[integer2];
				if (!potionEffect3.onUpdate(this) && !this.worldObj.isRemote)
				{
//JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
					iterator1.remove();
					this.onFinishedPotionEffect(potionEffect3);
				}
			}

			int i9;
			if (this.potionsNeedUpdate)
			{
				if (!this.worldObj.isRemote)
				{
					if (this.activePotionsMap.Count > 0)
					{
						i9 = PotionHelper.func_40354_a(this.activePotionsMap.Values);
						this.dataWatcher.updateObject(8, i9);
					}
					else
					{
						this.dataWatcher.updateObject(8, 0);
					}
				}

				this.potionsNeedUpdate = false;
			}

			if (this.rand.nextBoolean())
			{
				i9 = this.dataWatcher.getWatchableObjectInt(8);
				if (i9 > 0)
				{
					double d10 = (double)(i9 >> 16 & 255) / 255.0D;
					double d5 = (double)(i9 >> 8 & 255) / 255.0D;
					double d7 = (double)(i9 >> 0 & 255) / 255.0D;
					this.worldObj.spawnParticle("mobSpell", this.posX + (this.rand.NextDouble() - 0.5D) * (double)this.width, this.posY + this.rand.NextDouble() * (double)this.height - (double)this.yOffset, this.posZ + (this.rand.NextDouble() - 0.5D) * (double)this.width, d10, d5, d7);
				}
			}

		}

		public virtual void clearActivePotions()
		{
			System.Collections.IEnumerator iterator1 = this.activePotionsMap.Keys.GetEnumerator();

			while (iterator1.MoveNext())
			{
				int? integer2 = (int?)iterator1.Current;
				PotionEffect potionEffect3 = (PotionEffect)this.activePotionsMap[integer2];
				if (!this.worldObj.isRemote)
				{
//JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
					iterator1.remove();
					this.onFinishedPotionEffect(potionEffect3);
				}
			}

		}

		public virtual System.Collections.ICollection ActivePotionEffects
		{
			get
			{
				return this.activePotionsMap.Values;
			}
		}

		public virtual bool isPotionActive(Potion potion1)
		{
			return this.activePotionsMap.ContainsKey(potion1.id);
		}

		public virtual PotionEffect getActivePotionEffect(Potion potion1)
		{
			return (PotionEffect)this.activePotionsMap[potion1.id];
		}

		public virtual void addPotionEffect(PotionEffect potionEffect1)
		{
			if (this.isPotionApplicable(potionEffect1))
			{
				if (this.activePotionsMap.ContainsKey(potionEffect1.PotionID))
				{
					((PotionEffect)this.activePotionsMap[potionEffect1.PotionID]).combine(potionEffect1);
					this.onChangedPotionEffect((PotionEffect)this.activePotionsMap[potionEffect1.PotionID]);
				}
				else
				{
					this.activePotionsMap[potionEffect1.PotionID] = potionEffect1;
					this.onNewPotionEffect(potionEffect1);
				}

			}
		}

		public virtual bool isPotionApplicable(PotionEffect potionEffect1)
		{
			if (this.CreatureAttribute == EnumCreatureAttribute.UNDEAD)
			{
				int i2 = potionEffect1.PotionID;
				if (i2 == Potion.regeneration.id || i2 == Potion.poison.id)
				{
					return false;
				}
			}

			return true;
		}

		public virtual bool EntityUndead
		{
			get
			{
				return this.CreatureAttribute == EnumCreatureAttribute.UNDEAD;
			}
		}

		public virtual void removePotionEffect(int i1)
		{
			this.activePotionsMap.Remove(i1);
		}

		protected internal virtual void onNewPotionEffect(PotionEffect potionEffect1)
		{
			this.potionsNeedUpdate = true;
		}

		protected internal virtual void onChangedPotionEffect(PotionEffect potionEffect1)
		{
			this.potionsNeedUpdate = true;
		}

		protected internal virtual void onFinishedPotionEffect(PotionEffect potionEffect1)
		{
			this.potionsNeedUpdate = true;
		}

		protected internal virtual float SpeedModifier
		{
			get
			{
				float f1 = 1.0F;
				if (this.isPotionActive(Potion.moveSpeed))
				{
					f1 *= 1.0F + 0.2F * (float)(this.getActivePotionEffect(Potion.moveSpeed).Amplifier + 1);
				}
    
				if (this.isPotionActive(Potion.moveSlowdown))
				{
					f1 *= 1.0F - 0.15F * (float)(this.getActivePotionEffect(Potion.moveSlowdown).Amplifier + 1);
				}
    
				return f1;
			}
		}

		public virtual void setPositionAndUpdate(double d1, double d3, double d5)
		{
			this.setLocationAndAngles(d1, d3, d5, this.rotationYaw, this.rotationPitch);
		}

		public virtual bool Child
		{
			get
			{
				return false;
			}
		}

		public virtual EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.UNDEFINED;
			}
		}

		public virtual void renderBrokenItemStack(ItemStack itemStack1)
		{
			this.worldObj.playSoundAtEntity(this, "random.break", 0.8F, 0.8F + this.worldObj.rand.nextFloat() * 0.4F);

			for (int i2 = 0; i2 < 5; ++i2)
			{
				Vec3D vec3D3 = Vec3D.createVector(((double)this.rand.nextFloat() - 0.5D) * 0.1D, MathHelper.NextDouble * 0.1D + 0.1D, 0.0D);
				vec3D3.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
				vec3D3.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
				Vec3D vec3D4 = Vec3D.createVector(((double)this.rand.nextFloat() - 0.5D) * 0.3D, (double)(-this.rand.nextFloat()) * 0.6D - 0.3D, 0.6D);
				vec3D4.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
				vec3D4.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
				vec3D4 = vec3D4.addVector(this.posX, this.posY + (double)this.EyeHeight, this.posZ);
				this.worldObj.spawnParticle("iconcrack_" + itemStack1.Item.shiftedIndex, vec3D4.xCoord, vec3D4.yCoord, vec3D4.zCoord, vec3D3.xCoord, vec3D3.yCoord + 0.05D, vec3D3.zCoord);
			}

		}
	}

}