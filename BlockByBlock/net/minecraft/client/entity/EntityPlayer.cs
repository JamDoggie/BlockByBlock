using System;
using net.minecraft.src;

namespace net.minecraft.client.entity
{

    public abstract class EntityPlayer : EntityLiving
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {
            inventory = new InventoryPlayer(this);
        }

        public InventoryPlayer inventory;
        public Container inventorySlots;
        public Container craftingInventory;
        protected internal FoodStats foodStats = new FoodStats();
        protected internal int flyToggleTimer = 0;
        public sbyte field_9371_f = 0;
        public int score = 0;
        public float prevCameraYaw;
        public float cameraYaw;
        public bool isSwinging = false;
        public int swingProgressInt = 0;
        public string username;
        public int dimension;
        public string playerCloakUrl;
        public int xpCooldown = 0;
        public double field_20066_r;
        public double field_20065_s;
        public double field_20064_t;
        public double field_20063_u;
        public double field_20062_v;
        public double field_20061_w;
        protected internal bool sleeping;
        public ChunkCoordinates playerLocation;
        private int sleepTimer;
        public float field_22063_x;
        public float field_22062_y;
        public float field_22061_z;
        private ChunkCoordinates spawnChunk;
        private ChunkCoordinates startMinecartRidingCoordinate;
        public int timeUntilPortal = 20;
        protected internal bool inPortal = false;
        public float timeInPortal;
        public float prevTimeInPortal;
        public PlayerCapabilities capabilities = new PlayerCapabilities();
        public int experienceLevel;
        public int experienceTotal;
        public float experience;
        private ItemStack itemInUse;
        private int itemInUseCount;
        protected internal float speedOnGround = 0.1F;
        protected internal float speedInAir = 0.02F;
        public EntityFishHook fishEntity = null;

        public EntityPlayer(World world1) : base(world1)
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            inventorySlots = new ContainerPlayer(inventory, !world1.isRemote);
            craftingInventory = inventorySlots;
            yOffset = 1.62F;
            ChunkCoordinates chunkCoordinates2 = world1.SpawnPoint;
            setLocationAndAngles(chunkCoordinates2.posX + 0.5D, chunkCoordinates2.posY + 1, chunkCoordinates2.posZ + 0.5D, 0.0F, 0.0F);
            entityType = "humanoid";
            field_9353_B = 180.0F;
            fireResistance = 20;
            texture = "/mob/char.png";
        }

        public override int MaxHealth
        {
            get
            {
                return 20;
            }
        }

        protected internal override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, (sbyte)0);
            dataWatcher.addObject(17, (sbyte)0);
        }

        public virtual ItemStack ItemInUse
        {
            get
            {
                return itemInUse;
            }
        }

        public virtual int ItemInUseCount
        {
            get
            {
                return itemInUseCount;
            }
        }

        public virtual bool UsingItem
        {
            get
            {
                return itemInUse != null;
            }
        }

        public virtual int ItemInUseDuration
        {
            get
            {
                return UsingItem ? itemInUse.MaxItemUseDuration - itemInUseCount : 0;
            }
        }

        public virtual void stopUsingItem()
        {
            if (itemInUse != null)
            {
                itemInUse.onPlayerStoppedUsing(worldObj, this, itemInUseCount);
            }

            clearItemInUse();
        }

        public virtual void clearItemInUse()
        {
            itemInUse = null;
            itemInUseCount = 0;
            if (!worldObj.isRemote)
            {
                Eating = false;
            }

        }

        public override bool Blocking
        {
            get
            {
                return UsingItem && Item.itemsList[itemInUse.itemID].getItemUseAction(itemInUse) == EnumAction.block;
            }
        }

        public override void onUpdate()
        {
            if (itemInUse != null)
            {
                ItemStack itemStack1 = inventory.CurrentItem;
                if (itemStack1 != itemInUse)
                {
                    clearItemInUse();
                }
                else
                {
                    if (itemInUseCount <= 25 && itemInUseCount % 4 == 0)
                    {
                        updateItemUse(itemStack1, 5);
                    }

                    if (--itemInUseCount == 0 && !worldObj.isRemote)
                    {
                        onItemUseFinish();
                    }
                }
            }

            if (xpCooldown > 0)
            {
                --xpCooldown;
            }

            if (PlayerSleeping)
            {
                ++sleepTimer;
                if (sleepTimer > 100)
                {
                    sleepTimer = 100;
                }

                if (!worldObj.isRemote)
                {
                    if (!InBed)
                    {
                        wakeUpPlayer(true, true, false);
                    }
                    else if (worldObj.Daytime)
                    {
                        wakeUpPlayer(false, true, true);
                    }
                }
            }
            else if (sleepTimer > 0)
            {
                ++sleepTimer;
                if (sleepTimer >= 110)
                {
                    sleepTimer = 0;
                }
            }

            base.onUpdate();
            if (!worldObj.isRemote && craftingInventory != null && !craftingInventory.canInteractWith(this))
            {
                closeScreen();
                craftingInventory = inventorySlots;
            }

            if (capabilities.isFlying)
            {
                for (int i9 = 0; i9 < 8; ++i9)
                {
                }
            }

            if (Burning && capabilities.disableDamage)
            {
                extinguish();
            }

            field_20066_r = field_20063_u;
            field_20065_s = field_20062_v;
            field_20064_t = field_20061_w;
            double d10 = posX - field_20063_u;
            double d3 = posY - field_20062_v;
            double d5 = posZ - field_20061_w;
            double d7 = 10.0D;
            if (d10 > d7)
            {
                field_20066_r = field_20063_u = posX;
            }

            if (d5 > d7)
            {
                field_20064_t = field_20061_w = posZ;
            }

            if (d3 > d7)
            {
                field_20065_s = field_20062_v = posY;
            }

            if (d10 < -d7)
            {
                field_20066_r = field_20063_u = posX;
            }

            if (d5 < -d7)
            {
                field_20064_t = field_20061_w = posZ;
            }

            if (d3 < -d7)
            {
                field_20065_s = field_20062_v = posY;
            }

            field_20063_u += d10 * 0.25D; // x
            field_20061_w += d5 * 0.25D; // z
            field_20062_v += d3 * 0.25D; // y
            addStat(StatList.minutesPlayedStat, 1);
            if (ridingEntity == null)
            {
                startMinecartRidingCoordinate = null;
            }

            if (!worldObj.isRemote)
            {
                foodStats.onUpdate(this);
            }

        }

        protected internal virtual void updateItemUse(ItemStack itemStack1, int i2)
        {
            if (itemStack1.ItemUseAction == EnumAction.drink)
            {
                worldObj.playSoundAtEntity(this, "random.drink", 0.5F, worldObj.rand.NextSingle() * 0.1F + 0.9F);
            }

            if (itemStack1.ItemUseAction == EnumAction.eat)
            {
                for (int i3 = 0; i3 < i2; ++i3)
                {
                    Vec3D vec3D4 = Vec3D.createVector(((double)rand.NextSingle() - 0.5D) * 0.1D, portinghelpers.MathHelper.NextDouble * 0.1D + 0.1D, 0.0D);
                    vec3D4.rotateAroundX(-rotationPitch * (float)Math.PI / 180.0F);
                    vec3D4.rotateAroundY(-rotationYaw * (float)Math.PI / 180.0F);
                    Vec3D vec3D5 = Vec3D.createVector(((double)rand.NextSingle() - 0.5D) * 0.3D, (double)-rand.NextSingle() * 0.6D - 0.3D, 0.6D);
                    vec3D5.rotateAroundX(-rotationPitch * (float)Math.PI / 180.0F);
                    vec3D5.rotateAroundY(-rotationYaw * (float)Math.PI / 180.0F);
                    vec3D5 = vec3D5.addVector(posX, posY + (double)EyeHeight, posZ);
                    worldObj.spawnParticle("iconcrack_" + itemStack1.Item.shiftedIndex, vec3D5.xCoord, vec3D5.yCoord, vec3D5.zCoord, vec3D4.xCoord, vec3D4.yCoord + 0.05D, vec3D4.zCoord);
                }

                worldObj.playSoundAtEntity(this, "random.eat", 0.5F + 0.5F * rand.Next(2), (rand.NextSingle() - rand.NextSingle()) * 0.2F + 1.0F);
            }

        }

        protected internal virtual void onItemUseFinish()
        {
            if (itemInUse != null)
            {
                updateItemUse(itemInUse, 16);
                int i1 = itemInUse.stackSize;
                ItemStack itemStack2 = itemInUse.onFoodEaten(worldObj, this);
                if (itemStack2 != itemInUse || itemStack2 != null && itemStack2.stackSize != i1)
                {
                    inventory.mainInventory[inventory.currentItem] = itemStack2;
                    if (itemStack2.stackSize == 0)
                    {
                        inventory.mainInventory[inventory.currentItem] = null;
                    }
                }

                clearItemInUse();
            }

        }

        public override void handleHealthUpdate(sbyte b1)
        {
            if (b1 == 9)
            {
                onItemUseFinish();
            }
            else
            {
                base.handleHealthUpdate(b1);
            }

        }

        protected internal override bool MovementBlocked
        {
            get
            {
                return Health <= 0 || PlayerSleeping;
            }
        }

        public virtual void closeScreen()
        {
            craftingInventory = inventorySlots;
        }

        public override void updateCloak()
        {
            playerCloakUrl = "http://s3.amazonaws.com/MinecraftCloaks/" + username + ".png";
            cloakUrl = playerCloakUrl;
        }

        public override void updateRidden()
        {
            double d1 = posX;
            double d3 = posY;
            double d5 = posZ;
            base.updateRidden();
            prevCameraYaw = cameraYaw;
            cameraYaw = 0.0F;
            addMountedMovementStat(posX - d1, posY - d3, posZ - d5);
        }

        public override void preparePlayerToSpawn()
        {
            yOffset = 1.62F;
            SetSize(0.6F, 1.8F);
            base.preparePlayerToSpawn();
            EntityHealth = MaxHealth;
            deathTime = 0;
        }

        private int SwingSpeedModifier
        {
            get
            {
                return isPotionActive(Potion.digSpeed) ? 6 - (1 + getActivePotionEffect(Potion.digSpeed).Amplifier) * 1 : isPotionActive(Potion.digSlowdown) ? 6 + (1 + getActivePotionEffect(Potion.digSlowdown).Amplifier) * 2 : 6;
            }
        }

        public override void updateEntityActionState()
        {
            int i1 = SwingSpeedModifier;
            if (isSwinging)
            {
                ++swingProgressInt;
                if (swingProgressInt >= i1)
                {
                    swingProgressInt = 0;
                    isSwinging = false;
                }
            }
            else
            {
                swingProgressInt = 0;
            }

            swingProgress = swingProgressInt / (float)i1;
        }

        public override void onLivingUpdate()
        {
            if (flyToggleTimer > 0)
            {
                --flyToggleTimer;
            }

            if (worldObj.difficultySetting == 0 && Health < MaxHealth && ticksExisted % 20 * 12 == 0)
            {
                heal(1);
            }

            inventory.decrementAnimations();
            prevCameraYaw = cameraYaw;
            base.onLivingUpdate();
            landMovementFactor = speedOnGround;
            jumpMovementFactor = speedInAir;
            if (Sprinting)
            {
                landMovementFactor = (float)(landMovementFactor + speedOnGround * 0.3D);
                jumpMovementFactor = (float)(jumpMovementFactor + speedInAir * 0.3D);
            }

            float f1 = MathHelper.sqrt_double(motionX * motionX + motionZ * motionZ);
            float f2 = (float)Math.Atan(-motionY * (double)0.2F) * 15.0F;
            if (f1 > 0.1F)
            {
                f1 = 0.1F;
            }

            if (!onGround || Health <= 0)
            {
                f1 = 0.0F;
            }

            if (onGround || Health <= 0)
            {
                f2 = 0.0F;
            }

            cameraYaw += (f1 - cameraYaw) * 0.4F;
            cameraPitch += (f2 - cameraPitch) * 0.8F;
            if (Health > 0)
            {
                System.Collections.IList list3 = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand(1.0D, 0.0D, 1.0D));
                if (list3 != null)
                {
                    for (int i4 = 0; i4 < list3.Count; ++i4)
                    {
                        Entity entity5 = (Entity)list3[i4];
                        if (!entity5.isDead)
                        {
                            collideWithPlayer(entity5);
                        }
                    }
                }
            }

        }

        private void collideWithPlayer(Entity entity1)
        {
            entity1.onCollideWithPlayer(this);
        }

        public virtual int Score
        {
            get
            {
                return score;
            }
        }

        public override void onDeath(DamageSource damageSource1)
        {
            base.onDeath(damageSource1);
            SetSize(0.2F, 0.2F);
            SetPosition(posX, posY, posZ);
            motionY = 0.1F;
            if (username.Equals("Notch"))
            {
                dropPlayerItemWithRandomChoice(new ItemStack(Item.appleRed, 1), true);
            }

            inventory.dropAllItems();
            if (damageSource1 != null)
            {
                motionX = -MathHelper.cos((attackedAtYaw + rotationYaw) * (float)Math.PI / 180.0F) * 0.1F;
                motionZ = -MathHelper.sin((attackedAtYaw + rotationYaw) * (float)Math.PI / 180.0F) * 0.1F;
            }
            else
            {
                motionX = motionZ = 0.0D;
            }

            yOffset = 0.1F;
            addStat(StatList.deathsStat, 1);
        }

        public override void addToPlayerScore(Entity entity1, int i2)
        {
            score += i2;
            if (entity1 is EntityPlayer)
            {
                addStat(StatList.playerKillsStat, 1);
            }
            else
            {
                addStat(StatList.mobKillsStat, 1);
            }

        }

        protected internal override int decreaseAirSupply(int i1)
        {
            int i2 = EnchantmentHelper.getRespiration(inventory);
            return i2 > 0 && rand.Next(i2 + 1) > 0 ? i1 : base.decreaseAirSupply(i1);
        }

        public virtual EntityItem dropOneItem()
        {
            return dropPlayerItemWithRandomChoice(inventory.decrStackSize(inventory.currentItem, 1), false);
        }

        public virtual EntityItem dropPlayerItem(ItemStack itemStack1)
        {
            return dropPlayerItemWithRandomChoice(itemStack1, false);
        }

        public virtual EntityItem dropPlayerItemWithRandomChoice(ItemStack itemStack1, bool z2)
        {
            if (itemStack1 == null)
            {
                return null;
            }
            else
            {
                EntityItem entityItem3 = new EntityItem(worldObj, posX, posY - (double)0.3F + (double)EyeHeight, posZ, itemStack1);
                entityItem3.delayBeforeCanPickup = 40;
                float f4 = 0.1F;
                float f5;
                if (z2)
                {
                    f5 = rand.NextSingle() * 0.5F;
                    float f6 = rand.NextSingle() * (float)Math.PI * 2.0F;
                    entityItem3.motionX = -MathHelper.sin(f6) * f5;
                    entityItem3.motionZ = MathHelper.cos(f6) * f5;
                    entityItem3.motionY = 0.2F;
                }
                else
                {
                    f4 = 0.3F;
                    entityItem3.motionX = -MathHelper.sin(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI) * f4;
                    entityItem3.motionZ = MathHelper.cos(rotationYaw / 180.0F * (float)Math.PI) * MathHelper.cos(rotationPitch / 180.0F * (float)Math.PI) * f4;
                    entityItem3.motionY = -MathHelper.sin(rotationPitch / 180.0F * (float)Math.PI) * f4 + 0.1F;
                    f4 = 0.02F;
                    f5 = rand.NextSingle() * (float)Math.PI * 2.0F;
                    f4 *= rand.NextSingle();
                    entityItem3.motionX += Math.Cos((double)f5) * (double)f4;
                    entityItem3.motionY += (rand.NextSingle() - rand.NextSingle()) * 0.1F;
                    entityItem3.motionZ += Math.Sin((double)f5) * (double)f4;
                }

                joinEntityItemWithWorld(entityItem3);
                addStat(StatList.dropStat, 1);
                return entityItem3;
            }
        }

        protected internal virtual void joinEntityItemWithWorld(EntityItem entityItem1)
        {
            worldObj.spawnEntityInWorld(entityItem1);
        }

        public virtual float getCurrentPlayerStrVsBlock(Block block1)
        {
            float f2 = inventory.getStrVsBlock(block1);
            float f3 = f2;
            int i4 = EnchantmentHelper.getEfficiencyModifier(inventory);
            if (i4 > 0 && inventory.canHarvestBlock(block1))
            {
                f3 = f2 + (i4 * i4 + 1);
            }

            if (isPotionActive(Potion.digSpeed))
            {
                f3 *= 1.0F + (getActivePotionEffect(Potion.digSpeed).Amplifier + 1) * 0.2F;
            }

            if (isPotionActive(Potion.digSlowdown))
            {
                f3 *= 1.0F - (getActivePotionEffect(Potion.digSlowdown).Amplifier + 1) * 0.2F;
            }

            if (isInsideOfMaterial(Material.water) && !EnchantmentHelper.getAquaAffinityModifier(inventory))
            {
                f3 /= 5.0F;
            }

            if (!onGround)
            {
                f3 /= 5.0F;
            }

            return f3;
        }

        public virtual bool canHarvestBlock(Block block1)
        {
            return inventory.canHarvestBlock(block1);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("Inventory");
            inventory.readFromNBT(nBTTagList2);
            dimension = nBTTagCompound1.getInteger("Dimension");
            sleeping = nBTTagCompound1.getBoolean("Sleeping");
            sleepTimer = nBTTagCompound1.getShort("SleepTimer");
            experience = nBTTagCompound1.getFloat("XpP");
            experienceLevel = nBTTagCompound1.getInteger("XpLevel");
            experienceTotal = nBTTagCompound1.getInteger("XpTotal");
            if (sleeping)
            {
                playerLocation = new ChunkCoordinates(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ));
                wakeUpPlayer(true, true, false);
            }

            if (nBTTagCompound1.hasKey("SpawnX") && nBTTagCompound1.hasKey("SpawnY") && nBTTagCompound1.hasKey("SpawnZ"))
            {
                spawnChunk = new ChunkCoordinates(nBTTagCompound1.getInteger("SpawnX"), nBTTagCompound1.getInteger("SpawnY"), nBTTagCompound1.getInteger("SpawnZ"));
            }

            foodStats.readNBT(nBTTagCompound1);
            capabilities.readCapabilitiesFromNBT(nBTTagCompound1);
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setTag("Inventory", inventory.writeToNBT(new NBTTagList()));
            nBTTagCompound1.setInteger("Dimension", dimension);
            nBTTagCompound1.setBoolean("Sleeping", sleeping);
            nBTTagCompound1.setShort("SleepTimer", (short)sleepTimer);
            nBTTagCompound1.setFloat("XpP", experience);
            nBTTagCompound1.setInteger("XpLevel", experienceLevel);
            nBTTagCompound1.setInteger("XpTotal", experienceTotal);
            if (spawnChunk != null)
            {
                nBTTagCompound1.setInteger("SpawnX", spawnChunk.posX);
                nBTTagCompound1.setInteger("SpawnY", spawnChunk.posY);
                nBTTagCompound1.setInteger("SpawnZ", spawnChunk.posZ);
            }

            foodStats.writeNBT(nBTTagCompound1);
            capabilities.writeCapabilitiesToNBT(nBTTagCompound1);
        }

        public virtual void displayGUIChest(IInventory iInventory1)
        {
        }

        public virtual void displayGUIEnchantment(int i1, int i2, int i3)
        {
        }

        public virtual void displayWorkbenchGUI(int i1, int i2, int i3)
        {
        }

        public virtual void onItemPickup(Entity entity1, int i2)
        {
        }

        public override float EyeHeight
        {
            get
            {
                return 0.12F;
            }
        }

        protected internal virtual void resetHeight()
        {
            yOffset = 1.62F;
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            if (capabilities.disableDamage && !damageSource1.canHarmInCreative())
            {
                return false;
            }
            else
            {
                entityAge = 0;
                if (Health <= 0)
                {
                    return false;
                }
                else
                {
                    if (PlayerSleeping && !worldObj.isRemote)
                    {
                        wakeUpPlayer(true, true, false);
                    }

                    Entity entity3 = damageSource1.Entity;
                    if (entity3 is EntityMob || entity3 is EntityArrow)
                    {
                        if (worldObj.difficultySetting == 0)
                        {
                            i2 = 0;
                        }

                        if (worldObj.difficultySetting == 1)
                        {
                            i2 = i2 / 2 + 1;
                        }

                        if (worldObj.difficultySetting == 3)
                        {
                            i2 = i2 * 3 / 2;
                        }
                    }

                    if (i2 == 0)
                    {
                        return false;
                    }
                    else
                    {
                        Entity entity4 = entity3;
                        if (entity3 is EntityArrow && ((EntityArrow)entity3).shootingEntity != null)
                        {
                            entity4 = ((EntityArrow)entity3).shootingEntity;
                        }

                        if (entity4 is EntityLiving)
                        {
                            alertWolves((EntityLiving)entity4, false);
                        }

                        addStat(StatList.damageTakenStat, i2);
                        return base.attackEntityFrom(damageSource1, i2);
                    }
                }
            }
        }

        protected internal override int applyPotionDamageCalculations(DamageSource damageSource1, int i2)
        {
            int i3 = base.applyPotionDamageCalculations(damageSource1, i2);
            if (i3 <= 0)
            {
                return 0;
            }
            else
            {
                int i4 = EnchantmentHelper.getEnchantmentModifierDamage(inventory, damageSource1);
                if (i4 > 20)
                {
                    i4 = 20;
                }

                if (i4 > 0 && i4 <= 20)
                {
                    int i5 = 25 - i4;
                    int i6 = i3 * i5 + carryoverDamage;
                    i3 = i6 / 25;
                    carryoverDamage = i6 % 25;
                }

                return i3;
            }
        }

        protected internal virtual bool PVPEnabled
        {
            get
            {
                return false;
            }
        }

        protected internal virtual void alertWolves(EntityLiving entityLiving1, bool z2)
        {
            if (!(entityLiving1 is EntityCreeper) && !(entityLiving1 is EntityGhast))
            {
                if (entityLiving1 is EntityWolf)
                {
                    EntityWolf entityWolf3 = (EntityWolf)entityLiving1;
                    if (entityWolf3.Tamed && username.Equals(entityWolf3.OwnerName))
                    {
                        return;
                    }
                }

                if (!(entityLiving1 is EntityPlayer) || PVPEnabled)
                {
                    System.Collections.IList list7 = worldObj.getEntitiesWithinAABB(typeof(EntityWolf), AxisAlignedBB.getBoundingBoxFromPool(posX, posY, posZ, posX + 1.0D, posY + 1.0D, posZ + 1.0D).expand(16.0D, 4.0D, 16.0D));
                    System.Collections.IEnumerator iterator4 = list7.GetEnumerator();

                    while (true)
                    {
                        EntityWolf entityWolf6;
                        do
                        {
                            do
                            {
                                do
                                {
                                    do
                                    {
                                        if (!iterator4.MoveNext())
                                        {
                                            return;
                                        }

                                        Entity entity5 = (Entity)iterator4.Current;
                                        entityWolf6 = (EntityWolf)entity5;
                                    } while (!entityWolf6.Tamed);
                                } while (entityWolf6.EntityToAttack != null);
                            } while (!username.Equals(entityWolf6.OwnerName));
                        } while (z2 && entityWolf6.Sitting);

                        entityWolf6.func_48140_f(false);
                        entityWolf6.Target = entityLiving1;
                    }
                }
            }
        }

        protected internal override void damageArmor(int i1)
        {
            inventory.damageArmor(i1);
        }

        public override int TotalArmorValue
        {
            get
            {
                return inventory.TotalArmorValue;
            }
        }

        protected internal override void damageEntity(DamageSource damageSource1, int i2)
        {
            if (!damageSource1.Unblockable && Blocking)
            {
                i2 = 1 + i2 >> 1;
            }

            i2 = applyArmorCalculations(damageSource1, i2);
            i2 = applyPotionDamageCalculations(damageSource1, i2);
            addExhaustion(damageSource1.HungerDamage);
            health -= i2;
        }

        public virtual void displayGUIFurnace(TileEntityFurnace tileEntityFurnace1)
        {
        }

        public virtual void displayGUIDispenser(TileEntityDispenser tileEntityDispenser1)
        {
        }

        public virtual void displayGUIEditSign(TileEntitySign tileEntitySign1)
        {
        }

        public virtual void displayGUIBrewingStand(TileEntityBrewingStand tileEntityBrewingStand1)
        {
        }

        public virtual void useCurrentItemOnEntity(Entity entity1)
        {
            if (!entity1.interact(this))
            {
                ItemStack itemStack2 = CurrentEquippedItem;
                if (itemStack2 != null && entity1 is EntityLiving)
                {
                    itemStack2.useItemOnEntity((EntityLiving)entity1);
                    if (itemStack2.stackSize <= 0)
                    {
                        itemStack2.onItemDestroyedByUse(this);
                        destroyCurrentEquippedItem();
                    }
                }

            }
        }

        public virtual ItemStack CurrentEquippedItem
        {
            get
            {
                return inventory.CurrentItem;
            }
        }

        public virtual void destroyCurrentEquippedItem()
        {
            inventory.setInventorySlotContents(inventory.currentItem, null);
        }

        public override double YOffset
        {
            get
            {
                return (double)(yOffset - 0.5F);
            }
        }

        public virtual void swingItem()
        {
            if (!isSwinging || swingProgressInt >= SwingSpeedModifier / 2 || swingProgressInt < 0)
            {
                swingProgressInt = -1;
                isSwinging = true;
            }

        }

        public virtual void attackTargetEntityWithCurrentItem(Entity entity1)
        {
            if (entity1.canAttackWithItem())
            {
                int i2 = inventory.getDamageVsEntity(entity1);
                if (isPotionActive(Potion.damageBoost))
                {
                    i2 += 3 << getActivePotionEffect(Potion.damageBoost).Amplifier;
                }

                if (isPotionActive(Potion.weakness))
                {
                    i2 -= 2 << getActivePotionEffect(Potion.weakness).Amplifier;
                }

                int i3 = 0;
                int i4 = 0;
                if (entity1 is EntityLiving)
                {
                    i4 = EnchantmentHelper.getEnchantmentModifierLiving(inventory, (EntityLiving)entity1);
                    i3 += EnchantmentHelper.getKnockbackModifier(inventory, (EntityLiving)entity1);
                }

                if (Sprinting)
                {
                    ++i3;
                }

                if (i2 > 0 || i4 > 0)
                {
                    bool z5 = fallDistance > 0.0F && !onGround && !OnLadder && !InWater && !isPotionActive(Potion.blindness) && ridingEntity == null && entity1 is EntityLiving;
                    if (z5)
                    {
                        i2 += rand.Next(i2 / 2 + 2);
                    }

                    i2 += i4;
                    bool z6 = entity1.attackEntityFrom(DamageSource.causePlayerDamage(this), i2);
                    if (z6)
                    {
                        if (i3 > 0)
                        {
                            entity1.addVelocity((double)(-MathHelper.sin(rotationYaw * (float)Math.PI / 180.0F) * i3 * 0.5F), 0.1D, (double)(MathHelper.cos(rotationYaw * (float)Math.PI / 180.0F) * i3 * 0.5F));
                            motionX *= 0.6D;
                            motionZ *= 0.6D;
                            Sprinting = false;
                        }

                        if (z5)
                        {
                            onCriticalHit(entity1);
                        }

                        if (i4 > 0)
                        {
                            onEnchantmentCritical(entity1);
                        }

                        if (i2 >= 18)
                        {
                            triggerAchievement(AchievementList.overkill);
                        }

                        if (entity1 is EntityLiving)
                        {
                            LastAttackingEntity = (EntityLiving)entity1;
                        }
                    }

                    ItemStack itemStack7 = CurrentEquippedItem;
                    if (itemStack7 != null && entity1 is EntityLiving)
                    {
                        itemStack7.hitEntity((EntityLiving)entity1, this);
                        if (itemStack7.stackSize <= 0)
                        {
                            itemStack7.onItemDestroyedByUse(this);
                            destroyCurrentEquippedItem();
                        }
                    }

                    if (entity1 is EntityLiving)
                    {
                        if (entity1.EntityAlive)
                        {
                            alertWolves((EntityLiving)entity1, true);
                        }

                        addStat(StatList.damageDealtStat, i2);
                        int i8 = EnchantmentHelper.getFireAspectModifier(inventory, (EntityLiving)entity1);
                        if (i8 > 0)
                        {
                            entity1.Fire = i8 * 4;
                        }
                    }

                    addExhaustion(0.3F);
                }

            }
        }

        public virtual void onCriticalHit(Entity entity1)
        {
        }

        public virtual void onEnchantmentCritical(Entity entity1)
        {
        }

        public virtual void respawnPlayer()
        {
        }

        public abstract void func_6420_o();

        public virtual void onItemStackChanged(ItemStack itemStack1)
        {
        }

        public override void setDead()
        {
            base.setDead();
            inventorySlots.onCraftGuiClosed(this);
            if (craftingInventory != null)
            {
                craftingInventory.onCraftGuiClosed(this);
            }

        }

        public override bool EntityInsideOpaqueBlock
        {
            get
            {
                return !sleeping && base.EntityInsideOpaqueBlock;
            }
        }

        public virtual EnumStatus sleepInBedAt(int i1, int i2, int i3)
        {
            if (!worldObj.isRemote)
            {
                if (PlayerSleeping || !EntityAlive)
                {
                    return EnumStatus.OTHER_PROBLEM;
                }

                if (!worldObj.worldProvider.func_48217_e())
                {
                    return EnumStatus.NOT_POSSIBLE_HERE;
                }

                if (worldObj.Daytime)
                {
                    return EnumStatus.NOT_POSSIBLE_NOW;
                }

                if (Math.Abs(posX - i1) > 3.0D || Math.Abs(posY - i2) > 2.0D || Math.Abs(posZ - i3) > 3.0D)
                {
                    return EnumStatus.TOO_FAR_AWAY;
                }

                double d4 = 8.0D;
                double d6 = 5.0D;
                System.Collections.IList list8 = worldObj.getEntitiesWithinAABB(typeof(EntityMob), AxisAlignedBB.getBoundingBoxFromPool(i1 - d4, i2 - d6, i3 - d4, i1 + d4, i2 + d6, i3 + d4));
                if (list8.Count > 0)
                {
                    return EnumStatus.NOT_SAFE;
                }
            }

            SetSize(0.2F, 0.2F);
            yOffset = 0.2F;
            if (worldObj.blockExists(i1, i2, i3))
            {
                int i9 = worldObj.getBlockMetadata(i1, i2, i3);
                int i5 = BlockDirectional.getDirection(i9);
                float f10 = 0.5F;
                float f7 = 0.5F;
                switch (i5)
                {
                    case 0:
                        f7 = 0.9F;
                        break;
                    case 1:
                        f10 = 0.1F;
                        break;
                    case 2:
                        f7 = 0.1F;
                        break;
                    case 3:
                        f10 = 0.9F;
                        break;
                }

                func_22052_e(i5);
                SetPosition((double)(i1 + f10), (double)(i2 + 0.9375F), (double)(i3 + f7));
            }
            else
            {
                SetPosition((double)(i1 + 0.5F), (double)(i2 + 0.9375F), (double)(i3 + 0.5F));
            }

            sleeping = true;
            sleepTimer = 0;
            playerLocation = new ChunkCoordinates(i1, i2, i3);
            motionX = motionZ = motionY = 0.0D;
            if (!worldObj.isRemote)
            {
                worldObj.updateAllPlayersSleepingFlag();
            }

            return EnumStatus.OK;
        }

        private void func_22052_e(int i1)
        {
            field_22063_x = 0.0F;
            field_22061_z = 0.0F;
            switch (i1)
            {
                case 0:
                    field_22061_z = -1.8F;
                    break;
                case 1:
                    field_22063_x = 1.8F;
                    break;
                case 2:
                    field_22061_z = 1.8F;
                    break;
                case 3:
                    field_22063_x = -1.8F;
                    break;
            }

        }

        public virtual void wakeUpPlayer(bool z1, bool z2, bool z3)
        {
            SetSize(0.6F, 1.8F);
            resetHeight();
            ChunkCoordinates chunkCoordinates4 = playerLocation;
            ChunkCoordinates chunkCoordinates5 = playerLocation;
            if (chunkCoordinates4 != null && worldObj.getBlockId(chunkCoordinates4.posX, chunkCoordinates4.posY, chunkCoordinates4.posZ) == Block.bed.blockID)
            {
                BlockBed.setBedOccupied(worldObj, chunkCoordinates4.posX, chunkCoordinates4.posY, chunkCoordinates4.posZ, false);
                chunkCoordinates5 = BlockBed.getNearestEmptyChunkCoordinates(worldObj, chunkCoordinates4.posX, chunkCoordinates4.posY, chunkCoordinates4.posZ, 0);
                if (chunkCoordinates5 == null)
                {
                    chunkCoordinates5 = new ChunkCoordinates(chunkCoordinates4.posX, chunkCoordinates4.posY + 1, chunkCoordinates4.posZ);
                }

                SetPosition((double)(chunkCoordinates5.posX + 0.5F), (double)(chunkCoordinates5.posY + yOffset + 0.1F), (double)(chunkCoordinates5.posZ + 0.5F));
            }

            sleeping = false;
            if (!worldObj.isRemote && z2)
            {
                worldObj.updateAllPlayersSleepingFlag();
            }

            if (z1)
            {
                sleepTimer = 0;
            }
            else
            {
                sleepTimer = 100;
            }

            if (z3)
            {
                SpawnChunk = playerLocation;
            }

        }

        private bool InBed
        {
            get
            {
                return worldObj.getBlockId(playerLocation.posX, playerLocation.posY, playerLocation.posZ) == Block.bed.blockID;
            }
        }

        public static ChunkCoordinates verifyRespawnCoordinates(World world0, ChunkCoordinates chunkCoordinates1)
        {
            IChunkProvider iChunkProvider2 = world0.ChunkProvider;
            iChunkProvider2.loadChunk(chunkCoordinates1.posX - 3 >> 4, chunkCoordinates1.posZ - 3 >> 4);
            iChunkProvider2.loadChunk(chunkCoordinates1.posX + 3 >> 4, chunkCoordinates1.posZ - 3 >> 4);
            iChunkProvider2.loadChunk(chunkCoordinates1.posX - 3 >> 4, chunkCoordinates1.posZ + 3 >> 4);
            iChunkProvider2.loadChunk(chunkCoordinates1.posX + 3 >> 4, chunkCoordinates1.posZ + 3 >> 4);
            if (world0.getBlockId(chunkCoordinates1.posX, chunkCoordinates1.posY, chunkCoordinates1.posZ) != Block.bed.blockID)
            {
                return null;
            }
            else
            {
                ChunkCoordinates chunkCoordinates3 = BlockBed.getNearestEmptyChunkCoordinates(world0, chunkCoordinates1.posX, chunkCoordinates1.posY, chunkCoordinates1.posZ, 0);
                return chunkCoordinates3;
            }
        }

        public virtual float BedOrientationInDegrees
        {
            get
            {
                if (playerLocation != null)
                {
                    int i1 = worldObj.getBlockMetadata(playerLocation.posX, playerLocation.posY, playerLocation.posZ);
                    int i2 = BlockDirectional.getDirection(i1);
                    switch (i2)
                    {
                        case 0:
                            return 90.0F;
                        case 1:
                            return 0.0F;
                        case 2:
                            return 270.0F;
                        case 3:
                            return 180.0F;
                    }
                }

                return 0.0F;
            }
        }

        public override bool PlayerSleeping
        {
            get
            {
                return sleeping;
            }
        }

        public virtual bool PlayerFullyAsleep
        {
            get
            {
                return sleeping && sleepTimer >= 100;
            }
        }

        public virtual int SleepTimer
        {
            get
            {
                return sleepTimer;
            }
        }

        public virtual void addChatMessage(string string1)
        {
        }

        public virtual ChunkCoordinates SpawnChunk
        {
            get
            {
                return spawnChunk;
            }
            set
            {
                if (value != null)
                {
                    spawnChunk = new ChunkCoordinates(value);
                }
                else
                {
                    spawnChunk = null;
                }

            }
        }


        public virtual void triggerAchievement(StatBase statBase1)
        {
            addStat(statBase1, 1);
        }

        public virtual void addStat(StatBase statBase1, int i2)
        {
        }

        protected internal override void jump()
        {
            base.jump();
            addStat(StatList.jumpStat, 1);
            if (Sprinting)
            {
                addExhaustion(0.8F);
            }
            else
            {
                addExhaustion(0.2F);
            }

        }

        public override void moveEntityWithHeading(float f1, float f2)
        {
            double d3 = posX;
            double d5 = posY;
            double d7 = posZ;
            if (capabilities.isFlying)
            {
                double d9 = motionY;
                float f11 = jumpMovementFactor;
                jumpMovementFactor = 0.05F;
                base.moveEntityWithHeading(f1, f2);
                motionY = d9 * 0.6D;
                jumpMovementFactor = f11;
            }
            else
            {
                base.moveEntityWithHeading(f1, f2);
            }

            addMovementStat(posX - d3, posY - d5, posZ - d7);
        }

        public virtual void addMovementStat(double d1, double d3, double d5)
        {
            if (ridingEntity == null)
            {
                int i7;
                if (isInsideOfMaterial(Material.water))
                {
                    i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d3 * d3 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
                    if (i7 > 0)
                    {
                        addStat(StatList.distanceDoveStat, i7);
                        addExhaustion(0.015F * i7 * 0.01F);
                    }
                }
                else if (InWater)
                {
                    i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
                    if (i7 > 0)
                    {
                        addStat(StatList.distanceSwumStat, i7);
                        addExhaustion(0.015F * i7 * 0.01F);
                    }
                }
                else if (OnLadder)
                {
                    if (d3 > 0.0D)
                    {
                        addStat(StatList.distanceClimbedStat, (int)(long)Math.Round(d3 * 100.0D, MidpointRounding.AwayFromZero));
                    }
                }
                else if (onGround)
                {
                    i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
                    if (i7 > 0)
                    {
                        addStat(StatList.distanceWalkedStat, i7);
                        if (Sprinting)
                        {
                            addExhaustion(0.099999994F * i7 * 0.01F);
                        }
                        else
                        {
                            addExhaustion(0.01F * i7 * 0.01F);
                        }
                    }
                }
                else
                {
                    i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
                    if (i7 > 25)
                    {
                        addStat(StatList.distanceFlownStat, i7);
                    }
                }

            }
        }

        private void addMountedMovementStat(double d1, double d3, double d5)
        {
            if (ridingEntity != null)
            {
                int i7 = (int)Math.Round(MathHelper.sqrt_double(d1 * d1 + d3 * d3 + d5 * d5) * 100.0F, MidpointRounding.AwayFromZero);
                if (i7 > 0)
                {
                    if (ridingEntity is EntityMinecart)
                    {
                        addStat(StatList.distanceByMinecartStat, i7);
                        if (startMinecartRidingCoordinate == null)
                        {
                            startMinecartRidingCoordinate = new ChunkCoordinates(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ));
                        }
                        else if (startMinecartRidingCoordinate.getEuclideanDistanceTo(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ)) >= 1000.0D)
                        {
                            addStat(AchievementList.onARail, 1);
                        }
                    }
                    else if (ridingEntity is EntityBoat)
                    {
                        addStat(StatList.distanceByBoatStat, i7);
                    }
                    else if (ridingEntity is EntityPig)
                    {
                        addStat(StatList.distanceByPigStat, i7);
                    }
                }
            }

        }

        protected internal override void fall(float f1)
        {
            if (!capabilities.allowFlying)
            {
                if (f1 >= 2.0F)
                {
                    addStat(StatList.distanceFallenStat, (int)(long)Math.Round((double)f1 * 100.0D, MidpointRounding.AwayFromZero));
                }

                base.fall(f1);
            }
        }

        public override void onKillEntity(EntityLiving entityLiving1)
        {
            if (entityLiving1 is EntityMob)
            {
                triggerAchievement(AchievementList.killEnemy);
            }

        }

        public override int getItemIcon(ItemStack itemStack1, int i2)
        {
            int i3 = base.getItemIcon(itemStack1, i2);
            if (itemStack1.itemID == Item.fishingRod.shiftedIndex && fishEntity != null)
            {
                i3 = itemStack1.IconIndex + 16;
            }
            else
            {
                if (itemStack1.Item.func_46058_c())
                {
                    return itemStack1.Item.func_46057_a(itemStack1.ItemDamage, i2);
                }

                if (itemInUse != null && itemStack1.itemID == Item.bow.shiftedIndex)
                {
                    int i4 = itemStack1.MaxItemUseDuration - itemInUseCount;
                    if (i4 >= 18)
                    {
                        return 133;
                    }

                    if (i4 > 13)
                    {
                        return 117;
                    }

                    if (i4 > 0)
                    {
                        return 101;
                    }
                }
            }

            return i3;
        }

        public override void setInPortal()
        {
            if (timeUntilPortal > 0)
            {
                timeUntilPortal = 10;
            }
            else
            {
                inPortal = true;
            }
        }

        public virtual void addExperience(int i1)
        {
            score += i1;
            int i2 = int.MaxValue - experienceTotal;
            if (i1 > i2)
            {
                i1 = i2;
            }

            experience += i1 / (float)xpBarCap();

            for (experienceTotal += i1; experience >= 1.0F; experience /= xpBarCap())
            {
                experience = (experience - 1.0F) * xpBarCap();
                increaseLevel();
            }

        }

        public virtual void removeExperience(int i1)
        {
            experienceLevel -= i1;
            if (experienceLevel < 0)
            {
                experienceLevel = 0;
            }

        }

        public virtual int xpBarCap()
        {
            return 7 + (experienceLevel * 7 >> 1);
        }

        private void increaseLevel()
        {
            ++experienceLevel;
        }

        public virtual void addExhaustion(float f1)
        {
            if (!capabilities.disableDamage)
            {
                if (!worldObj.isRemote)
                {
                    foodStats.addExhaustion(f1);
                }

            }
        }

        public virtual FoodStats FoodStats
        {
            get
            {
                return foodStats;
            }
        }

        public virtual bool canEat(bool z1)
        {
            return (z1 || foodStats.needFood()) && !capabilities.disableDamage;
        }

        public virtual bool shouldHeal()
        {
            return Health > 0 && Health < MaxHealth;
        }

        public virtual void setItemInUse(ItemStack itemStack1, int i2)
        {
            if (itemStack1 != itemInUse)
            {
                itemInUse = itemStack1;
                itemInUseCount = i2;
                if (!worldObj.isRemote)
                {
                    Eating = true;
                }

            }
        }

        public virtual bool canPlayerEdit(int i1, int i2, int i3)
        {
            return true;
        }

        protected internal override int getExperiencePoints(EntityPlayer entityPlayer1)
        {
            int i2 = experienceLevel * 7;
            return i2 > 100 ? 100 : i2;
        }

        protected internal override bool Player
        {
            get
            {
                return true;
            }
        }

        public virtual void travelToTheEnd(int i1)
        {
        }

        public virtual void copyPlayer(EntityPlayer entityPlayer1)
        {
            inventory.copyInventory(entityPlayer1.inventory);
            health = entityPlayer1.health;
            foodStats = entityPlayer1.foodStats;
            experienceLevel = entityPlayer1.experienceLevel;
            experienceTotal = entityPlayer1.experienceTotal;
            experience = entityPlayer1.experience;
            score = entityPlayer1.score;
        }

        protected internal override bool canTriggerWalking()
        {
            return !capabilities.isFlying;
        }

        public virtual void func_50009_aI()
        {
        }
    }

}