using net.minecraft.src;

namespace net.minecraft.client.entity
{
    public abstract class EntityTameable : EntityAnimal
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            aiSit = new EntityAISit(this);
        }

        protected internal EntityAISit aiSit;

        public EntityTameable(World world1) : base(world1)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, (sbyte)0);
            dataWatcher.addObject(17, "");
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            if (ReferenceEquals(OwnerName, null))
            {
                nBTTagCompound1.setString("Owner", "");
            }
            else
            {
                nBTTagCompound1.setString("Owner", OwnerName);
            }

            nBTTagCompound1.setBoolean("Sitting", Sitting);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            string string2 = nBTTagCompound1.getString("Owner");
            if (string2.Length > 0)
            {
                setOwner(string2);
                Tamed = true;
            }

            aiSit.func_48407_a(nBTTagCompound1.getBoolean("Sitting"));
        }

        protected internal virtual void func_48142_a(bool z1)
        {
            string string2 = "heart";
            if (!z1)
            {
                string2 = "smoke";
            }

            for (int i3 = 0; i3 < 7; ++i3)
            {
                double d4 = rand.NextGaussian() * 0.02D;
                double d6 = rand.NextGaussian() * 0.02D;
                double d8 = rand.NextGaussian() * 0.02D;
                worldObj.spawnParticle(string2, posX + (double)(rand.NextSingle() * width * 2.0F) - width, posY + 0.5D + (double)(rand.NextSingle() * height), posZ + (double)(rand.NextSingle() * width * 2.0F) - width, d4, d6, d8);
            }

        }

        public override void handleHealthUpdate(sbyte b1)
        {
            if (b1 == 7)
            {
                func_48142_a(true);
            }
            else if (b1 == 6)
            {
                func_48142_a(false);
            }
            else
            {
                base.handleHealthUpdate(b1);
            }

        }

        public virtual bool Tamed
        {
            get
            {
                return (dataWatcher.getWatchableObjectByte(16) & 4) != 0;
            }
            set
            {
                sbyte b2 = dataWatcher.getWatchableObjectByte(16);
                if (value)
                {
                    dataWatcher.updateObject(16, (sbyte)(b2 | 4));
                }
                else
                {
                    dataWatcher.updateObject(16, (sbyte)(b2 & -5));
                }

            }
        }


        public virtual bool Sitting
        {
            get
            {
                return (dataWatcher.getWatchableObjectByte(16) & 1) != 0;
            }
        }

        public virtual void func_48140_f(bool z1)
        {
            sbyte b2 = dataWatcher.getWatchableObjectByte(16);
            if (z1)
            {
                dataWatcher.updateObject(16, (sbyte)(b2 | 1));
            }
            else
            {
                dataWatcher.updateObject(16, (sbyte)(b2 & -2));
            }

        }

        public virtual string OwnerName
        {
            get
            {
                return dataWatcher.getWatchableObjectString(17);
            }
        }

        public void setOwner(string string1)
        {
            dataWatcher.updateObject(17, string1);
        }

        public EntityLiving getOwner()
        {
            return worldObj.getPlayerEntityByName(OwnerName);
        }


        public virtual EntityAISit func_50008_ai()
        {
            return aiSit;
        }
    }

}