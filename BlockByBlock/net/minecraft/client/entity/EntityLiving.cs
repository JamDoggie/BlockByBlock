using BlockByBlock.java_extensions;
using net.minecraft.src;
using System;
using System.Collections;
using System.Linq;

namespace net.minecraft.client.entity
{

    public abstract class EntityLiving : Entity
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            health = MaxHealth;
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
        public float field_9325_U = (float)(portinghelpers.MathHelper.NextDouble * (double)0.9F + (double)0.1F);
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
        protected internal Dictionary<int, PotionEffect> activePotionsMap = new();
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
            preventEntitySpawning = true;
            lookHelper = new EntityLookHelper(this);
            moveHelper = new EntityMoveHelper(this);
            jumpHelper = new EntityJumpHelper(this);
            bodyHelper = new EntityBodyHelper(this);
            navigator = new PathNavigate(this, world1, 16.0F);
            field_48104_at = new EntitySenses(this);
            field_9363_r = (float)(portinghelpers.MathHelper.NextDouble + 1.0D) * 0.01F;
            SetPosition(posX, posY, posZ);
            field_9365_p = (float)portinghelpers.MathHelper.NextDouble * 12398.0F;
            rotationYaw = (float)(portinghelpers.MathHelper.NextDouble * (double)(float)Math.PI * 2.0D);
            rotationYawHead = rotationYaw;
            stepHeight = 0.5F;
        }

        public virtual EntityLookHelper LookHelper
        {
            get
            {
                return lookHelper;
            }
        }

        public virtual EntityMoveHelper MoveHelper
        {
            get
            {
                return moveHelper;
            }
        }

        public virtual EntityJumpHelper JumpHelper
        {
            get
            {
                return jumpHelper;
            }
        }

        public virtual PathNavigate Navigator
        {
            get
            {
                return navigator;
            }
        }

        public virtual EntitySenses func_48090_aM()
        {
            return field_48104_at;
        }

        public virtual RandomExtended RNG
        {
            get
            {
                return rand;
            }
        }

        public virtual EntityLiving AITarget
        {
            get
            {
                return entityLivingToAttack;
            }
        }

        public virtual EntityLiving LastAttackingEntity
        {
            get
            {
                return lastAttackingEntity;
            }
            set
            {
                if (value is EntityLiving)
                {
                    lastAttackingEntity = value;
                }

            }
        }


        public virtual int Age
        {
            get
            {
                return entityAge;
            }
        }

        public override void func_48079_f(float f1)
        {
            rotationYawHead = f1;
        }

        public virtual float func_48101_aR()
        {
            return field_48111_au;
        }

        public virtual void func_48098_g(float f1)
        {
            field_48111_au = f1;
            MoveForward = f1;
        }

        public void setLastAttackingEntity(Entity entity1)
        {
            if (entity1 is EntityLiving)
            {
                lastAttackingEntity = (EntityLiving)entity1;
            }
        }

        public virtual bool attackEntityAsMob(Entity entity1)
        {
            setLastAttackingEntity(entity1);
            return false;
        }

        public virtual EntityLiving? AttackTarget
        {
            get
            {
                return attackTarget;
            }
            set
            {
                attackTarget = value;
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
                return isWithinHomeDistance(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ));
            }
        }

        public virtual bool isWithinHomeDistance(int i1, int i2, int i3)
        {
            return maximumHomeDistance == -1.0F ? true : homePosition.getDistanceSquared(i1, i2, i3) < maximumHomeDistance * maximumHomeDistance;
        }

        public virtual void setHomeArea(int i1, int i2, int i3, int i4)
        {
            homePosition.set(i1, i2, i3);
            maximumHomeDistance = i4;
        }

        public virtual ChunkCoordinates HomePosition
        {
            get
            {
                return homePosition;
            }
        }

        public virtual float MaximumHomeDistance
        {
            get
            {
                return maximumHomeDistance;
            }
        }

        public virtual void detachHome()
        {
            maximumHomeDistance = -1.0F;
        }

        public virtual bool hasHome()
        {
            return maximumHomeDistance != -1.0F;
        }

        public virtual EntityLiving RevengeTarget
        {
            set
            {
                entityLivingToAttack = value;
                revengeTimer = entityLivingToAttack != null ? 60 : 0;
            }
        }

        protected internal override void entityInit()
        {
            dataWatcher.addObject(8, field_39002_c);
        }

        public virtual bool canEntityBeSeen(Entity entity1)
        {
            return worldObj.rayTraceBlocks(Vec3D.createVector(posX, posY + (double)EyeHeight, posZ), Vec3D.createVector(entity1.posX, entity1.posY + (double)entity1.EyeHeight, entity1.posZ)) == null;
        }

        public override string Texture
        {
            get
            {
                return texture;
            }
        }

        public override bool canBeCollidedWith()
        {
            return !isDead;
        }

        public override bool canBePushed()
        {
            return !isDead;
        }

        public override float EyeHeight
        {
            get
            {
                return height * 0.85F;
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
            string string1 = LivingSound;
            if (!ReferenceEquals(string1, null))
            {
                worldObj.playSoundAtEntity(this, string1, SoundVolume, SoundPitch);
            }

        }

        public override void onEntityUpdate()
        {
            prevSwingProgress = swingProgress;
            base.onEntityUpdate();
            Profiler.startSection("mobBaseTick");
            if (EntityAlive && rand.Next(1000) < livingSoundTime++)
            {
                livingSoundTime = -TalkInterval;
                playLivingSound();
            }

            if (EntityAlive && EntityInsideOpaqueBlock && attackEntityFrom(DamageSource.inWall, 1))
            {
                ;
            }

            if (ImmuneToFire || worldObj.isRemote)
            {
                extinguish();
            }

            if (EntityAlive && isInsideOfMaterial(Material.water) && !canBreatheUnderwater() && !activePotionsMap.ContainsKey(Potion.waterBreathing.id))
            {
                Air = decreaseAirSupply(Air);
                if (Air == -20)
                {
                    Air = 0;

                    for (int i1 = 0; i1 < 8; ++i1)
                    {
                        float f2 = rand.NextSingle() - rand.NextSingle();
                        float f3 = rand.NextSingle() - rand.NextSingle();
                        float f4 = rand.NextSingle() - rand.NextSingle();
                        worldObj.spawnParticle("bubble", posX + (double)f2, posY + (double)f3, posZ + (double)f4, motionX, motionY, motionZ);
                    }

                    attackEntityFrom(DamageSource.drown, 2);
                }

                extinguish();
            }
            else
            {
                Air = 300;
            }

            prevCameraPitch = cameraPitch;
            if (attackTime > 0)
            {
                --attackTime;
            }

            if (hurtTime > 0)
            {
                --hurtTime;
            }

            if (heartsLife > 0)
            {
                --heartsLife;
            }

            if (health <= 0)
            {
                onDeathUpdate();
            }

            if (recentlyHit > 0)
            {
                --recentlyHit;
            }
            else
            {
                attackingPlayer = null;
            }

            if (lastAttackingEntity != null && !lastAttackingEntity.EntityAlive)
            {
                lastAttackingEntity = null;
            }

            if (entityLivingToAttack != null)
            {
                if (!entityLivingToAttack.EntityAlive)
                {
                    RevengeTarget = null;
                }
                else if (revengeTimer > 0)
                {
                    --revengeTimer;
                }
                else
                {
                    RevengeTarget = null;
                }
            }

            updatePotionEffects();
            field_9359_x = field_9360_w;
            prevRenderYawOffset = renderYawOffset;
            prevRotationYawHead = rotationYawHead;
            prevRotationYaw = rotationYaw;
            prevRotationPitch = rotationPitch;
            Profiler.endSection();
        }

        protected internal virtual void onDeathUpdate()
        {
            ++deathTime;
            if (deathTime == 20)
            {
                int i1;
                if (!worldObj.isRemote && (recentlyHit > 0 || Player) && !Child)
                {
                    i1 = getExperiencePoints(attackingPlayer);

                    while (i1 > 0)
                    {
                        int i2 = EntityXPOrb.getXPSplit(i1);
                        i1 -= i2;
                        worldObj.spawnEntityInWorld(new EntityXPOrb(worldObj, posX, posY, posZ, i2));
                    }
                }

                onEntityDeath();
                setDead();

                for (i1 = 0; i1 < 20; ++i1)
                {
                    double d8 = rand.NextGaussian() * 0.02D;
                    double d4 = rand.NextGaussian() * 0.02D;
                    double d6 = rand.NextGaussian() * 0.02D;
                    worldObj.spawnParticle("explode", posX + (double)(rand.NextSingle() * width * 2.0F) - width, posY + (double)(rand.NextSingle() * height), posZ + (double)(rand.NextSingle() * width * 2.0F) - width, d8, d4, d6);
                }
            }

        }

        protected internal virtual int decreaseAirSupply(int i1)
        {
            return i1 - 1;
        }

        protected internal virtual int getExperiencePoints(EntityPlayer entityPlayer1)
        {
            return experienceValue;
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
                double d2 = rand.NextGaussian() * 0.02D;
                double d4 = rand.NextGaussian() * 0.02D;
                double d6 = rand.NextGaussian() * 0.02D;
                double d8 = 10.0D;
                worldObj.spawnParticle("explode", posX + (double)(rand.NextSingle() * width * 2.0F) - width - d2 * d8, posY + (double)(rand.NextSingle() * height) - d4 * d8, posZ + (double)(rand.NextSingle() * width * 2.0F) - width - d6 * d8, d2, d4, d6);
            }

        }

        public override void updateRidden()
        {
            base.updateRidden();
            field_9362_u = field_9361_v;
            field_9361_v = 0.0F;
            fallDistance = 0.0F;
        }

        public override void setPositionAndRotation2(double d1, double d3, double d5, float f7, float f8, int i9)
        {
            yOffset = 0.0F;
            newPosX = d1;
            newPosY = d3;
            newPosZ = d5;
            newRotationYaw = f7;
            newRotationPitch = f8;
            newPosRotationIncrements = i9;
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (arrowHitTempCounter > 0)
            {
                if (arrowHitTimer <= 0)
                {
                    arrowHitTimer = 60;
                }

                --arrowHitTimer;
                if (arrowHitTimer <= 0)
                {
                    --arrowHitTempCounter;
                }
            }

            onLivingUpdate();
            double d1 = posX - prevPosX;
            double d3 = posZ - prevPosZ;
            float f5 = MathHelper.sqrt_double(d1 * d1 + d3 * d3);
            float f6 = renderYawOffset;
            float f7 = 0.0F;
            field_9362_u = field_9361_v;
            float f8 = 0.0F;
            if (f5 > 0.05F)
            {
                f8 = 1.0F;
                f7 = f5 * 3.0F;
                f6 = (float)Math.Atan2(d3, d1) * 180.0F / (float)Math.PI - 90.0F;
            }

            if (swingProgress > 0.0F)
            {
                f6 = rotationYaw;
            }

            if (!onGround)
            {
                f8 = 0.0F;
            }

            field_9361_v += (f8 - field_9361_v) * 0.3F;
            if (AIEnabled)
            {
                bodyHelper.func_48650_a();
            }
            else
            {
                float f9;
                for (f9 = f6 - renderYawOffset; f9 < -180.0F; f9 += 360.0F)
                {
                }

                while (f9 >= 180.0F)
                {
                    f9 -= 360.0F;
                }

                renderYawOffset += f9 * 0.3F;

                float f10;
                for (f10 = rotationYaw - renderYawOffset; f10 < -180.0F; f10 += 360.0F)
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

                renderYawOffset = rotationYaw - f10;
                if (f10 * f10 > 2500.0F)
                {
                    renderYawOffset += f10 * 0.2F;
                }

                if (z11)
                {
                    f7 *= -1.0F;
                }
            }

            while (rotationYaw - prevRotationYaw < -180.0F)
            {
                prevRotationYaw -= 360.0F;
            }

            while (rotationYaw - prevRotationYaw >= 180.0F)
            {
                prevRotationYaw += 360.0F;
            }

            while (renderYawOffset - prevRenderYawOffset < -180.0F)
            {
                prevRenderYawOffset -= 360.0F;
            }

            while (renderYawOffset - prevRenderYawOffset >= 180.0F)
            {
                prevRenderYawOffset += 360.0F;
            }

            while (rotationPitch - prevRotationPitch < -180.0F)
            {
                prevRotationPitch -= 360.0F;
            }

            while (rotationPitch - prevRotationPitch >= 180.0F)
            {
                prevRotationPitch += 360.0F;
            }

            while (rotationYawHead - prevRotationYawHead < -180.0F)
            {
                prevRotationYawHead -= 360.0F;
            }

            while (rotationYawHead - prevRotationYawHead >= 180.0F)
            {
                prevRotationYawHead += 360.0F;
            }

            field_9360_w += f7;
        }

        protected internal override void SetSize(float f1, float f2)
        {
            base.SetSize(f1, f2);
        }

        public virtual void heal(int i1)
        {
            if (health > 0)
            {
                health += i1;
                if (health > MaxHealth)
                {
                    health = MaxHealth;
                }

                heartsLife = heartsHalvesLife / 2;
            }
        }

        public abstract int MaxHealth { get; }

        public virtual int Health
        {
            get
            {
                return health;
            }
        }

        public virtual int EntityHealth
        {
            set
            {
                health = value;
                if (value > MaxHealth)
                {
                    value = MaxHealth;
                }

            }
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (worldObj.isRemote)
            {
                return false;
            }
            else
            {
                entityAge = 0;
                if (health <= 0)
                {
                    return false;
                }
                else if (damageSource1.getFireDamage() && isPotionActive(Potion.fireResistance))
                {
                    return false;
                }
                else
                {
                    field_704_R = 1.5F;
                    bool z3 = true;
                    if (heartsLife > heartsHalvesLife / 2.0F)
                    {
                        if (i2 <= naturalArmorRating)
                        {
                            return false;
                        }

                        damageEntity(damageSource1, i2 - naturalArmorRating);
                        naturalArmorRating = i2;
                        z3 = false;
                    }
                    else
                    {
                        naturalArmorRating = i2;
                        prevHealth = health;
                        heartsLife = heartsHalvesLife;
                        damageEntity(damageSource1, i2);
                        hurtTime = maxHurtTime = 10;
                    }

                    attackedAtYaw = 0.0F;
                    Entity entity4 = damageSource1.Entity;
                    if (entity4 != null)
                    {
                        if (entity4 is EntityLiving)
                        {
                            RevengeTarget = (EntityLiving)entity4;
                        }

                        if (entity4 is EntityPlayer)
                        {
                            recentlyHit = 60;
                            attackingPlayer = (EntityPlayer)entity4;
                        }
                        else if (entity4 is EntityWolf)
                        {
                            EntityWolf entityWolf5 = (EntityWolf)entity4;
                            if (entityWolf5.Tamed)
                            {
                                recentlyHit = 60;
                                attackingPlayer = null;
                            }
                        }
                    }

                    if (z3)
                    {
                        worldObj.setEntityState(this, 2);
                        setBeenAttacked();
                        if (entity4 != null)
                        {
                            double d9 = entity4.posX - posX;

                            double d7;
                            for (d7 = entity4.posZ - posZ; d9 * d9 + d7 * d7 < 1.0E-4D; d7 = (portinghelpers.MathHelper.NextDouble - portinghelpers.MathHelper.NextDouble) * 0.01D)
                            {
                                d9 = (portinghelpers.MathHelper.NextDouble - portinghelpers.MathHelper.NextDouble) * 0.01D;
                            }

                            attackedAtYaw = (float)(Math.Atan2(d7, d9) * 180.0D / (double)(float)Math.PI) - rotationYaw;
                            knockBack(entity4, i2, d9, d7);
                        }
                        else
                        {
                            attackedAtYaw = (int)(portinghelpers.MathHelper.NextDouble * 2.0D) * 180;
                        }
                    }

                    if (health <= 0)
                    {
                        if (z3)
                        {
                            worldObj.playSoundAtEntity(this, DeathSound, SoundVolume, SoundPitch);
                        }

                        onDeath(damageSource1);
                    }
                    else if (z3)
                    {
                        worldObj.playSoundAtEntity(this, HurtSound, SoundVolume, SoundPitch);
                    }

                    return true;
                }
            }
        }

        private float SoundPitch
        {
            get
            {
                return Child ? (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.5F : (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F;
            }
        }

        public override void performHurtAnimation()
        {
            hurtTime = maxHurtTime = 10;
            attackedAtYaw = 0.0F;
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
                int i3 = 25 - TotalArmorValue;
                int i4 = i2 * i3 + carryoverDamage;
                damageArmor(i2);
                i2 = i4 / 25;
                carryoverDamage = i4 % 25;
            }

            return i2;
        }

        protected internal virtual int applyPotionDamageCalculations(DamageSource damageSource1, int i2)
        {
            if (isPotionActive(Potion.resistance))
            {
                int i3 = (getActivePotionEffect(Potion.resistance).Amplifier + 1) * 5;
                int i4 = 25 - i3;
                int i5 = i2 * i4 + carryoverDamage;
                i2 = i5 / 25;
                carryoverDamage = i5 % 25;
            }

            return i2;
        }

        protected internal virtual void damageEntity(DamageSource damageSource1, int i2)
        {
            i2 = applyArmorCalculations(damageSource1, i2);
            i2 = applyPotionDamageCalculations(damageSource1, i2);
            health -= i2;
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
            isAirBorne = true;
            float f7 = MathHelper.sqrt_double(d3 * d3 + d5 * d5);
            float f8 = 0.4F;
            motionX /= 2.0D;
            motionY /= 2.0D;
            motionZ /= 2.0D;
            motionX -= d3 / (double)f7 * (double)f8;
            motionY += f8;
            motionZ -= d5 / (double)f7 * (double)f8;
            if (motionY > (double)0.4F)
            {
                motionY = 0.4F;
            }

        }

        public virtual void onDeath(DamageSource damageSource1)
        {
            Entity entity2 = damageSource1.Entity;
            if (scoreValue >= 0 && entity2 != null)
            {
                entity2.addToPlayerScore(this, scoreValue);
            }

            if (entity2 != null)
            {
                entity2.onKillEntity(this);
            }

            dead = true;
            if (!worldObj.isRemote)
            {
                int i3 = 0;
                if (entity2 is EntityPlayer)
                {
                    i3 = EnchantmentHelper.getLootingModifier(((EntityPlayer)entity2).inventory);
                }

                if (!Child)
                {
                    dropFewItems(recentlyHit > 0, i3);
                    if (recentlyHit > 0)
                    {
                        int i4 = rand.Next(200) - i3;
                        if (i4 < 5)
                        {
                            dropRareDrop(i4 <= 0 ? 1 : 0);
                        }
                    }
                }
            }

            worldObj.setEntityState(this, 3);
        }

        protected internal virtual void dropRareDrop(int i1)
        {
        }

        protected internal virtual void dropFewItems(bool z1, int i2)
        {
            int i3 = DropItemId;
            if (i3 > 0)
            {
                int i4 = rand.Next(3);
                if (i2 > 0)
                {
                    i4 += rand.Next(i2 + 1);
                }

                for (int i5 = 0; i5 < i4; ++i5)
                {
                    dropItem(i3, 1);
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
                    worldObj.playSoundAtEntity(this, "damage.fallbig", 1.0F, 1.0F);
                }
                else
                {
                    worldObj.playSoundAtEntity(this, "damage.fallsmall", 1.0F, 1.0F);
                }

                attackEntityFrom(DamageSource.fall, i2);
                int i3 = worldObj.getBlockId(MathHelper.floor_double(posX), MathHelper.floor_double(posY - (double)0.2F - yOffset), MathHelper.floor_double(posZ));
                if (i3 > 0)
                {
                    StepSound stepSound4 = Block.blocksList[i3].stepSound;
                    worldObj.playSoundAtEntity(this, stepSound4.StepSoundName, stepSound4.Volume * 0.5F, stepSound4.Pitch * 0.75F);
                }
            }

        }

        public virtual void moveEntityWithHeading(float f1, float f2)
        {
            double d3;
            if (InWater)
            {
                d3 = posY;
                moveFlying(f1, f2, AIEnabled ? 0.04F : 0.02F);
                moveEntity(motionX, motionY, motionZ);
                motionX *= 0.8F;
                motionY *= 0.8F;
                motionZ *= 0.8F;
                motionY -= 0.02D;
                if (isCollidedHorizontally && isOffsetPositionInLiquid(motionX, motionY + (double)0.6F - posY + d3, motionZ))
                {
                    motionY = 0.3F;
                }
            }
            else if (handleLavaMovement())
            {
                d3 = posY;
                moveFlying(f1, f2, 0.02F);
                moveEntity(motionX, motionY, motionZ);
                motionX *= 0.5D;
                motionY *= 0.5D;
                motionZ *= 0.5D;
                motionY -= 0.02D;
                if (isCollidedHorizontally && isOffsetPositionInLiquid(motionX, motionY + (double)0.6F - posY + d3, motionZ))
                {
                    motionY = 0.3F;
                }
            }
            else
            {
                float f8 = 0.91F;
                if (onGround)
                {
                    f8 = 0.54600006F;
                    int i4 = worldObj.getBlockId(MathHelper.floor_double(posX), MathHelper.floor_double(boundingBox.minY) - 1, MathHelper.floor_double(posZ));
                    if (i4 > 0)
                    {
                        f8 = Block.blocksList[i4].slipperiness * 0.91F;
                    }
                }

                float f9 = 0.16277136F / (f8 * f8 * f8);
                float f5;
                if (onGround)
                {
                    if (AIEnabled)
                    {
                        f5 = func_48101_aR();
                    }
                    else
                    {
                        f5 = landMovementFactor;
                    }

                    f5 *= f9;
                }
                else
                {
                    f5 = jumpMovementFactor;
                }

                moveFlying(f1, f2, f5);
                f8 = 0.91F;
                if (onGround)
                {
                    f8 = 0.54600006F;
                    int i6 = worldObj.getBlockId(MathHelper.floor_double(posX), MathHelper.floor_double(boundingBox.minY) - 1, MathHelper.floor_double(posZ));
                    if (i6 > 0)
                    {
                        f8 = Block.blocksList[i6].slipperiness * 0.91F;
                    }
                }

                if (OnLadder)
                {
                    float f10 = 0.15F;
                    if (motionX < (double)-f10)
                    {
                        motionX = -f10;
                    }

                    if (motionX > (double)f10)
                    {
                        motionX = f10;
                    }

                    if (motionZ < (double)-f10)
                    {
                        motionZ = -f10;
                    }

                    if (motionZ > (double)f10)
                    {
                        motionZ = f10;
                    }

                    fallDistance = 0.0F;
                    if (motionY < -0.15D)
                    {
                        motionY = -0.15D;
                    }

                    bool z7 = Sneaking && this is EntityPlayer;
                    if (z7 && motionY < 0.0D)
                    {
                        motionY = 0.0D;
                    }
                }

                moveEntity(motionX, motionY, motionZ);
                if (isCollidedHorizontally && OnLadder)
                {
                    motionY = 0.2D;
                }

                motionY -= 0.08D;
                motionY *= 0.98F;
                motionX *= f8;
                motionZ *= f8;
            }

            field_705_Q = field_704_R;
            d3 = posX - prevPosX;
            double d11 = posZ - prevPosZ;
            float f12 = MathHelper.sqrt_double(d3 * d3 + d11 * d11) * 4.0F;
            if (f12 > 1.0F)
            {
                f12 = 1.0F;
            }

            field_704_R += (f12 - field_704_R) * 0.4F;
            field_703_S += field_704_R;
        }

        public virtual bool OnLadder
        {
            get
            {
                int i1 = MathHelper.floor_double(posX);
                int i2 = MathHelper.floor_double(boundingBox.minY);
                int i3 = MathHelper.floor_double(posZ);
                int i4 = worldObj.getBlockId(i1, i2, i3);
                return i4 == Block.ladder.blockID || i4 == Block.vine.blockID;
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            nBTTagCompound1.setShort("Health", (short)health);
            nBTTagCompound1.setShort("HurtTime", (short)hurtTime);
            nBTTagCompound1.setShort("DeathTime", (short)deathTime);
            nBTTagCompound1.setShort("AttackTime", (short)attackTime);
            if (activePotionsMap.Count > 0)
            {
                NBTTagList nBTTagList2 = new NBTTagList();
                IEnumerator iterator3 = activePotionsMap.Values.GetEnumerator();

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
            if (health < -32768)
            {
                health = -32768;
            }

            health = nBTTagCompound1.getShort("Health");
            if (!nBTTagCompound1.hasKey("Health"))
            {
                health = MaxHealth;
            }

            hurtTime = nBTTagCompound1.getShort("HurtTime");
            deathTime = nBTTagCompound1.getShort("DeathTime");
            attackTime = nBTTagCompound1.getShort("AttackTime");
            if (nBTTagCompound1.hasKey("ActiveEffects"))
            {
                NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("ActiveEffects");

                for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
                {
                    NBTTagCompound nBTTagCompound4 = (NBTTagCompound)nBTTagList2.tagAt(i3);
                    sbyte b5 = nBTTagCompound4.getByte("Id");
                    sbyte b6 = nBTTagCompound4.getByte("Amplifier");
                    int i7 = nBTTagCompound4.getInteger("Duration");
                    activePotionsMap[Convert.ToInt32(b5)] = new PotionEffect(b5, i7, b6);
                }
            }

        }

        public override bool EntityAlive
        {
            get
            {
                return !isDead && health > 0;
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
                moveForward = value;
            }
        }

        public virtual bool Jumping
        {
            set
            {
                isJumping = value;
            }
        }

        public virtual void onLivingUpdate()
        {
            if (jumpTicks > 0)
            {
                --jumpTicks;
            }

            if (newPosRotationIncrements > 0)
            {
                double d1 = posX + (newPosX - posX) / newPosRotationIncrements;
                double d3 = posY + (newPosY - posY) / newPosRotationIncrements;
                double d5 = posZ + (newPosZ - posZ) / newPosRotationIncrements;

                double d7;
                for (d7 = newRotationYaw - rotationYaw; d7 < -180.0D; d7 += 360.0D)
                {
                }

                while (d7 >= 180.0D)
                {
                    d7 -= 360.0D;
                }

                rotationYaw = (float)(rotationYaw + d7 / newPosRotationIncrements);
                rotationPitch = (float)(rotationPitch + (newRotationPitch - rotationPitch) / newPosRotationIncrements);
                --newPosRotationIncrements;
                SetPosition(d1, d3, d5);
                setRotation(rotationYaw, rotationPitch);
                IList list9 = worldObj.getCollidingBoundingBoxes(this, boundingBox.contract(8.0D / 256D, 0.0D, 8.0D / 256D));
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

                    d3 += d10 - boundingBox.minY;
                    SetPosition(d1, d3, d5);
                }
            }

            Profiler.startSection("ai");
            if (MovementBlocked)
            {
                isJumping = false;
                moveStrafing = 0.0F;
                moveForward = 0.0F;
                randomYawVelocity = 0.0F;
            }
            else if (ClientWorld)
            {
                if (AIEnabled)
                {
                    Profiler.startSection("newAi");
                    updateAITasks();
                    Profiler.endSection();
                }
                else
                {
                    Profiler.startSection("oldAi");
                    updateEntityActionState();
                    Profiler.endSection();
                    rotationYawHead = rotationYaw;
                }
            }

            Profiler.endSection();
            bool z14 = InWater;
            bool z2 = handleLavaMovement();
            if (isJumping)
            {
                if (z14)
                {
                    motionY += 0.04F;
                }
                else if (z2)
                {
                    motionY += 0.04F;
                }
                else if (onGround && jumpTicks == 0)
                {
                    jump();
                    jumpTicks = 10;
                }
            }
            else
            {
                jumpTicks = 0;
            }

            moveStrafing *= 0.98F;
            moveForward *= 0.98F;
            randomYawVelocity *= 0.9F;
            float f15 = landMovementFactor;
            landMovementFactor *= SpeedModifier;
            moveEntityWithHeading(moveStrafing, moveForward);
            landMovementFactor = f15;
            Profiler.startSection("push");
            IList list4 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand((double)0.2F, 0.0D, (double)0.2F));
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

        public virtual bool AIEnabled
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
                return !worldObj.isRemote;
            }
        }

        protected internal virtual bool MovementBlocked
        {
            get
            {
                return health <= 0;
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
            motionY = 0.42F;
            if (isPotionActive(Potion.jump))
            {
                motionY += (getActivePotionEffect(Potion.jump).Amplifier + 1) * 0.1F;
            }

            if (Sprinting)
            {
                float f1 = rotationYaw * 0.017453292F;
                motionX -= MathHelper.sin(f1) * 0.2F;
                motionZ += MathHelper.cos(f1) * 0.2F;
            }

            isAirBorne = true;
        }

        protected internal virtual bool canDespawn()
        {
            return true;
        }

        protected internal virtual void despawnEntity()
        {
            EntityPlayer entityPlayer1 = worldObj.getClosestPlayerToEntity(this, -1.0D);
            if (entityPlayer1 != null)
            {
                double d2 = entityPlayer1.posX - posX;
                double d4 = entityPlayer1.posY - posY;
                double d6 = entityPlayer1.posZ - posZ;
                double d8 = d2 * d2 + d4 * d4 + d6 * d6;
                if (canDespawn() && d8 > 16384.0D)
                {
                    setDead();
                }

                if (entityAge > 600 && rand.Next(800) == 0 && d8 > 1024.0D && canDespawn())
                {
                    setDead();
                }
                else if (d8 < 1024.0D)
                {
                    entityAge = 0;
                }
            }

        }

        protected internal virtual void updateAITasks()
        {
            ++entityAge;
            Profiler.startSection("checkDespawn");
            despawnEntity();
            Profiler.endSection();
            Profiler.startSection("sensing");
            field_48104_at.clearSensingCache();
            Profiler.endSection();
            Profiler.startSection("targetSelector");
            targetTasks.onUpdateTasks();
            Profiler.endSection();
            Profiler.startSection("goalSelector");
            tasks.onUpdateTasks();
            Profiler.endSection();
            Profiler.startSection("navigation");
            navigator.onUpdateNavigation();
            Profiler.endSection();
            Profiler.startSection("mob tick");
            updateAITick();
            Profiler.endSection();
            Profiler.startSection("controls");
            moveHelper.onUpdateMoveHelper();
            lookHelper.onUpdateLook();
            jumpHelper.doJump();
            Profiler.endSection();
        }

        protected internal virtual void updateAITick()
        {
        }

        public virtual void updateEntityActionState()
        {
            ++entityAge;
            despawnEntity();
            moveStrafing = 0.0F;
            moveForward = 0.0F;
            float f1 = 8.0F;
            if (rand.NextSingle() < 0.02F)
            {
                EntityPlayer entityPlayer2 = worldObj.getClosestPlayerToEntity(this, (double)f1);
                if (entityPlayer2 != null)
                {
                    currentTarget = entityPlayer2;
                    numTicksToChaseTarget = 10 + rand.Next(20);
                }
                else
                {
                    randomYawVelocity = (rand.NextSingle() - 0.5F) * 20.0F;
                }
            }

            if (currentTarget != null)
            {
                faceEntity(currentTarget, 10.0F, VerticalFaceSpeed);
                if (numTicksToChaseTarget-- <= 0 || currentTarget.isDead || currentTarget.getDistanceSqToEntity(this) > (double)(f1 * f1))
                {
                    currentTarget = null;
                }
            }
            else
            {
                if (rand.NextSingle() < 0.05F)
                {
                    randomYawVelocity = (rand.NextSingle() - 0.5F) * 20.0F;
                }

                rotationYaw += randomYawVelocity;
                rotationPitch = defaultPitch;
            }

            bool z4 = InWater;
            bool z3 = handleLavaMovement();
            if (z4 || z3)
            {
                isJumping = rand.NextSingle() < 0.8F;
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
            double d4 = entity1.posX - posX;
            double d8 = entity1.posZ - posZ;
            double d6;
            if (entity1 is EntityLiving)
            {
                EntityLiving entityLiving10 = (EntityLiving)entity1;
                d6 = posY + (double)EyeHeight - (entityLiving10.posY + (double)entityLiving10.EyeHeight);
            }
            else
            {
                d6 = (entity1.boundingBox.minY + entity1.boundingBox.maxY) / 2.0D - (posY + (double)EyeHeight);
            }

            double d14 = (double)MathHelper.sqrt_double(d4 * d4 + d8 * d8);
            float f12 = (float)(Math.Atan2(d8, d4) * 180.0D / (double)(float)Math.PI) - 90.0F;
            float f13 = (float)-(Math.Atan2(d6, d14) * 180.0D / (double)(float)Math.PI);
            rotationPitch = -updateRotation(rotationPitch, f13, f3);
            rotationYaw = updateRotation(rotationYaw, f12, f2);
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
                return worldObj.checkIfAABBIsClear(boundingBox) && worldObj.getCollidingBoundingBoxes(this, boundingBox).Count == 0 && !worldObj.isAnyLiquid(boundingBox);
            }
        }

        protected internal override void kill()
        {
            attackEntityFrom(DamageSource.outOfWorld, 4);
        }

        public virtual float getSwingProgress(float f1)
        {
            float f2 = swingProgress - prevSwingProgress;
            if (f2 < 0.0F)
            {
                ++f2;
            }

            return prevSwingProgress + f2 * f1;
        }

        public virtual Vec3D getPosition(float f1)
        {
            if (f1 == 1.0F)
            {
                return Vec3D.createVector(posX, posY, posZ);
            }
            else
            {
                double d2 = prevPosX + (posX - prevPosX) * (double)f1;
                double d4 = prevPosY + (posY - prevPosY) * (double)f1;
                double d6 = prevPosZ + (posZ - prevPosZ) * (double)f1;
                return Vec3D.createVector(d2, d4, d6);
            }
        }

        public override Vec3D LookVec
        {
            get
            {
                return getLook(1.0F);
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
                f2 = MathHelper.cos(-rotationYaw * 0.017453292F - (float)Math.PI);
                f3 = MathHelper.sin(-rotationYaw * 0.017453292F - (float)Math.PI);
                f4 = -MathHelper.cos(-rotationPitch * 0.017453292F);
                f5 = MathHelper.sin(-rotationPitch * 0.017453292F);
                return Vec3D.createVector((double)(f3 * f4), (double)f5, (double)(f2 * f4));
            }
            else
            {
                f2 = prevRotationPitch + (rotationPitch - prevRotationPitch) * f1;
                f3 = prevRotationYaw + (rotationYaw - prevRotationYaw) * f1;
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
            Vec3D vec3D4 = getPosition(f3);
            Vec3D vec3D5 = getLook(f3);
            Vec3D vec3D6 = vec3D4.addVector(vec3D5.xCoord * d1, vec3D5.yCoord * d1, vec3D5.zCoord * d1);
            return worldObj.rayTraceBlocks(vec3D4, vec3D6);
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
                field_704_R = 1.5F;
                heartsLife = heartsHalvesLife;
                hurtTime = maxHurtTime = 10;
                attackedAtYaw = 0.0F;
                worldObj.playSoundAtEntity(this, HurtSound, SoundVolume, (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F);
                attackEntityFrom(DamageSource.generic, 0);
            }
            else if (b1 == 3)
            {
                worldObj.playSoundAtEntity(this, DeathSound, SoundVolume, (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F);
                health = 0;
                onDeath(DamageSource.generic);
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
            for (int i = activePotionsMap.Keys.Count - 1; i >= 0; i--) // PORTING TODO: Possible logic difference, original code used an iterator and deleted as it went. 
                                                                       // You can't do that with C# iterators, so I opted to iterate backwards here instead.
            {
                int potionId = activePotionsMap.Keys.ElementAt(i);
                PotionEffect potionEffect3 = activePotionsMap[potionId];
                if (!potionEffect3.onUpdate(this) && !worldObj.isRemote)
                {
                    activePotionsMap.Remove(potionId);
                    onFinishedPotionEffect(potionEffect3);
                }
            }

            int i9;
            if (potionsNeedUpdate)
            {
                if (!worldObj.isRemote)
                {
                    if (activePotionsMap.Count > 0)
                    {
                        i9 = PotionHelper.func_40354_a(activePotionsMap.Values);
                        dataWatcher.updateObject(8, i9);
                    }
                    else
                    {
                        dataWatcher.updateObject(8, 0);
                    }
                }

                potionsNeedUpdate = false;
            }

            if (rand.NextBool())
            {
                i9 = dataWatcher.getWatchableObjectInt(8);
                if (i9 > 0)
                {
                    double d10 = (i9 >> 16 & 255) / 255.0D;
                    double d5 = (i9 >> 8 & 255) / 255.0D;
                    double d7 = (i9 >> 0 & 255) / 255.0D;
                    worldObj.spawnParticle("mobSpell", posX + (rand.NextDouble() - 0.5D) * width, posY + rand.NextDouble() * height - yOffset, posZ + (rand.NextDouble() - 0.5D) * width, d10, d5, d7);
                }
            }

        }

        public virtual void clearActivePotions()
        {
            foreach (PotionEffect potionEffect in activePotionsMap.Values)
            {
                onFinishedPotionEffect(potionEffect);
            }

            activePotionsMap.Clear();

            /* PORTING TODO: possible logic difference. */
        }

        public virtual ICollection ActivePotionEffects
        {
            get
            {
                return activePotionsMap.Values;
            }
        }

        public virtual bool isPotionActive(Potion potion1)
        {
            return activePotionsMap.ContainsKey(potion1.id);
        }

        public virtual PotionEffect getActivePotionEffect(Potion potion1)
        {
            return activePotionsMap[potion1.id];
        }

        public virtual void addPotionEffect(PotionEffect potionEffect1)
        {
            if (isPotionApplicable(potionEffect1))
            {
                if (activePotionsMap.ContainsKey(potionEffect1.PotionID))
                {
                    activePotionsMap[potionEffect1.PotionID].combine(potionEffect1);
                    onChangedPotionEffect(activePotionsMap[potionEffect1.PotionID]);
                }
                else
                {
                    activePotionsMap[potionEffect1.PotionID] = potionEffect1;
                    onNewPotionEffect(potionEffect1);
                }

            }
        }

        public virtual bool isPotionApplicable(PotionEffect potionEffect1)
        {
            if (CreatureAttribute == EnumCreatureAttribute.UNDEAD)
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
                return CreatureAttribute == EnumCreatureAttribute.UNDEAD;
            }
        }

        public virtual void removePotionEffect(int i1)
        {
            activePotionsMap.Remove(i1);
        }

        protected internal virtual void onNewPotionEffect(PotionEffect potionEffect1)
        {
            potionsNeedUpdate = true;
        }

        protected internal virtual void onChangedPotionEffect(PotionEffect potionEffect1)
        {
            potionsNeedUpdate = true;
        }

        protected internal virtual void onFinishedPotionEffect(PotionEffect potionEffect1)
        {
            potionsNeedUpdate = true;
        }

        protected internal virtual float SpeedModifier
        {
            get
            {
                float f1 = 1.0F;
                if (isPotionActive(Potion.moveSpeed))
                {
                    f1 *= 1.0F + 0.2F * (getActivePotionEffect(Potion.moveSpeed).Amplifier + 1);
                }

                if (isPotionActive(Potion.moveSlowdown))
                {
                    f1 *= 1.0F - 0.15F * (getActivePotionEffect(Potion.moveSlowdown).Amplifier + 1);
                }

                return f1;
            }
        }

        public virtual void setPositionAndUpdate(double d1, double d3, double d5)
        {
            setLocationAndAngles(d1, d3, d5, rotationYaw, rotationPitch);
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
            worldObj.playSoundAtEntity(this, "random.break", 0.8F, 0.8F + worldObj.rand.NextSingle() * 0.4F);

            for (int i2 = 0; i2 < 5; ++i2)
            {
                Vec3D vec3D3 = Vec3D.createVector(((double)rand.NextSingle() - 0.5D) * 0.1D, portinghelpers.MathHelper.NextDouble * 0.1D + 0.1D, 0.0D);
                vec3D3.rotateAroundX(-rotationPitch * (float)Math.PI / 180.0F);
                vec3D3.rotateAroundY(-rotationYaw * (float)Math.PI / 180.0F);
                Vec3D vec3D4 = Vec3D.createVector(((double)rand.NextSingle() - 0.5D) * 0.3D, (double)-rand.NextSingle() * 0.6D - 0.3D, 0.6D);
                vec3D4.rotateAroundX(-rotationPitch * (float)Math.PI / 180.0F);
                vec3D4.rotateAroundY(-rotationYaw * (float)Math.PI / 180.0F);
                vec3D4 = vec3D4.addVector(posX, posY + (double)EyeHeight, posZ);
                worldObj.spawnParticle("iconcrack_" + itemStack1.Item.shiftedIndex, vec3D4.xCoord, vec3D4.yCoord, vec3D4.zCoord, vec3D3.xCoord, vec3D3.yCoord + 0.05D, vec3D3.zCoord);
            }

        }
    }

}