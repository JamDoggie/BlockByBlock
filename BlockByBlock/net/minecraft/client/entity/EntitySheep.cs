using BlockByBlock.java_extensions;
using net.minecraft.src;
using System;

namespace net.minecraft.client.entity
{

    public class EntitySheep : EntityAnimal
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            aiEatGrass = new EntityAIEatGrass(this);
        }

        public static readonly float[][] fleeceColorTable = new float[][]
        {
            new float[] {1.0F, 1.0F, 1.0F},
            new float[] {0.95F, 0.7F, 0.2F},
            new float[] {0.9F, 0.5F, 0.85F},
            new float[] {0.6F, 0.7F, 0.95F},
            new float[] {0.9F, 0.9F, 0.2F},
            new float[] {0.5F, 0.8F, 0.1F},
            new float[] {0.95F, 0.7F, 0.8F},
            new float[] {0.3F, 0.3F, 0.3F},
            new float[] {0.6F, 0.6F, 0.6F},
            new float[] {0.3F, 0.6F, 0.7F},
            new float[] {0.7F, 0.4F, 0.9F},
            new float[] {0.2F, 0.4F, 0.8F},
            new float[] {0.5F, 0.4F, 0.3F},
            new float[] {0.4F, 0.5F, 0.2F},
            new float[] {0.8F, 0.3F, 0.3F},
            new float[] {0.1F, 0.1F, 0.1F}
        };
        private int sheepTimer;
        private EntityAIEatGrass aiEatGrass;

        public EntitySheep(World world1) : base(world1)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            texture = "/mob/sheep.png";
            SetSize(0.9F, 1.3F);
            float f2 = 0.23F;
            Navigator.func_48664_a(true);
            tasks.addTask(0, new EntityAISwimming(this));
            tasks.addTask(1, new EntityAIPanic(this, 0.38F));
            tasks.addTask(2, new EntityAIMate(this, f2));
            tasks.addTask(3, new EntityAITempt(this, 0.25F, Item.wheat.shiftedIndex, false));
            tasks.addTask(4, new EntityAIFollowParent(this, 0.25F));
            tasks.addTask(5, aiEatGrass);
            tasks.addTask(6, new EntityAIWander(this, f2));
            tasks.addTask(7, new EntityAIWatchClosest(this, typeof(EntityPlayer), 6.0F));
            tasks.addTask(8, new EntityAILookIdle(this));
        }

        public override bool AIEnabled
        {
            get
            {
                return true;
            }
        }

        protected internal override void updateAITasks()
        {
            sheepTimer = aiEatGrass.func_48396_h();
            base.updateAITasks();
        }

        public override void onLivingUpdate()
        {
            if (worldObj.isRemote)
            {
                sheepTimer = Math.Max(0, sheepTimer - 1);
            }

            base.onLivingUpdate();
        }

        public override int MaxHealth
        {
            get
            {
                return 8;
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, new sbyte?(0));
        }

        protected internal override void dropFewItems(bool z1, int i2)
        {
            if (!Sheared)
            {
                entityDropItem(new ItemStack(Block.cloth.blockID, 1, FleeceColor), 0.0F);
            }

        }

        protected internal override int DropItemId
        {
            get
            {
                return Block.cloth.blockID;
            }
        }

        public override void handleHealthUpdate(sbyte b1)
        {
            if (b1 == 10)
            {
                sheepTimer = 40;
            }
            else
            {
                base.handleHealthUpdate(b1);
            }

        }

        public virtual float func_44003_c(float f1)
        {
            return sheepTimer <= 0 ? 0.0F : sheepTimer >= 4 && sheepTimer <= 36 ? 1.0F : sheepTimer < 4 ? (sheepTimer - f1) / 4.0F : -(sheepTimer - 40 - f1) / 4.0F;
        }

        public virtual float func_44002_d(float f1)
        {
            if (sheepTimer > 4 && sheepTimer <= 36)
            {
                float f2 = (sheepTimer - 4 - f1) / 32.0F;
                return 0.62831855F + 0.21991149F * MathHelper.sin(f2 * 28.7F);
            }
            else
            {
                return sheepTimer > 0 ? 0.62831855F : rotationPitch / 57.295776F;
            }
        }

        public override bool interact(EntityPlayer entityPlayer1)
        {
            ItemStack itemStack2 = entityPlayer1.inventory.CurrentItem;
            if (itemStack2 != null && itemStack2.itemID == Item.shears.shiftedIndex && !Sheared && !Child)
            {
                if (!worldObj.isRemote)
                {
                    Sheared = true;
                    int i3 = 1 + rand.Next(3);

                    for (int i4 = 0; i4 < i3; ++i4)
                    {
                        EntityItem entityItem5 = entityDropItem(new ItemStack(Block.cloth.blockID, 1, FleeceColor), 1.0F);
                        entityItem5.motionY += rand.NextSingle() * 0.05F;
                        entityItem5.motionX += (rand.NextSingle() - rand.NextSingle()) * 0.1F;
                        entityItem5.motionZ += (rand.NextSingle() - rand.NextSingle()) * 0.1F;
                    }
                }

                itemStack2.damageItem(1, entityPlayer1);
            }

            return base.interact(entityPlayer1);
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setBoolean("Sheared", Sheared);
            nBTTagCompound1.setByte("Color", (sbyte)FleeceColor);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            Sheared = nBTTagCompound1.getBoolean("Sheared");
            FleeceColor = nBTTagCompound1.getByte("Color");
        }

        protected internal override string LivingSound
        {
            get
            {
                return "mob.sheep";
            }
        }

        protected internal override string HurtSound
        {
            get
            {
                return "mob.sheep";
            }
        }

        protected internal override string DeathSound
        {
            get
            {
                return "mob.sheep";
            }
        }

        public virtual int FleeceColor
        {
            get
            {
                return dataWatcher.getWatchableObjectByte(16) & 15;
            }
            set
            {
                sbyte b2 = dataWatcher.getWatchableObjectByte(16);
                dataWatcher.updateObject(16, unchecked((sbyte)(b2 & 240 | value & 15)));
            }
        }


        public virtual bool Sheared
        {
            get
            {
                return (dataWatcher.getWatchableObjectByte(16) & 16) != 0;
            }
            set
            {
                sbyte b2 = dataWatcher.getWatchableObjectByte(16);
                if (value)
                {
                    dataWatcher.updateObject(16, (sbyte)(b2 | 16));
                }
                else
                {
                    dataWatcher.updateObject(16, (sbyte)(b2 & -17));
                }

            }
        }


        public static int getRandomFleeceColor(RandomExtended random0)
        {
            int i1 = random0.Next(100);
            return i1 < 5 ? 15 : i1 < 10 ? 7 : i1 < 15 ? 8 : i1 < 18 ? 12 : random0.Next(500) == 0 ? 6 : 0;
        }

        public override EntityAnimal spawnBabyAnimal(EntityAnimal entityAnimal1)
        {
            EntitySheep entitySheep2 = (EntitySheep)entityAnimal1;
            EntitySheep entitySheep3 = new EntitySheep(worldObj);
            if (rand.NextBool())
            {
                entitySheep3.FleeceColor = FleeceColor;
            }
            else
            {
                entitySheep3.FleeceColor = entitySheep2.FleeceColor;
            }

            return entitySheep3;
        }

        public override void eatGrassBonus()
        {
            Sheared = false;
            if (Child)
            {
                int i1 = GrowingAge + 1200;
                if (i1 > 0)
                {
                    i1 = 0;
                }

                GrowingAge = i1;
            }

        }
    }

}