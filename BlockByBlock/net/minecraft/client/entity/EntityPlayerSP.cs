using BlockByBlock.net.minecraft.client.entity.particle;
using net.minecraft.src;

namespace net.minecraft.client.entity
{
    using Minecraft = Minecraft;

    public class EntityPlayerSP : EntityPlayer
    {
        public MovementInput movementInput;
        protected internal Minecraft mc;
        protected internal int sprintToggleTimer = 0;
        public int sprintingTicksLeft = 0;
        public float renderArmYaw;
        public float renderArmPitch;
        public float prevRenderArmYaw;
        public float prevRenderArmPitch;
        private MouseFilter field_21903_bJ = new MouseFilter();
        private MouseFilter field_21904_bK = new MouseFilter();
        private MouseFilter field_21902_bL = new MouseFilter();

        public EntityPlayerSP(Minecraft minecraft1, World world2, Session session3, int i4) : base(world2)
        {
            mc = minecraft1;
            dimension = i4;
            if (session3 != null && !ReferenceEquals(session3.username, null) && session3.username.Length > 0)
            {
                skinUrl = "http://s3.amazonaws.com/MinecraftSkins/" + session3.username + ".png";
            }

            username = session3.username;
        }

        public override void moveEntity(double d1, double d3, double d5)
        {
            base.moveEntity(d1, d3, d5);
        }

        public override void updateEntityActionState()
        {
            base.updateEntityActionState();
            moveStrafing = movementInput.moveStrafe;
            moveForward = movementInput.moveForward;
            isJumping = movementInput.jump;
            prevRenderArmYaw = renderArmYaw;
            prevRenderArmPitch = renderArmPitch;
            renderArmPitch = (float)(renderArmPitch + (double)(rotationPitch - renderArmPitch) * 0.5D);
            renderArmYaw = (float)(renderArmYaw + (double)(rotationYaw - renderArmYaw) * 0.5D);
        }

        protected internal override bool ClientWorld
        {
            get
            {
                return true;
            }
        }

        public override void onLivingUpdate()
        {
            if (sprintingTicksLeft > 0)
            {
                --sprintingTicksLeft;
                if (sprintingTicksLeft == 0)
                {
                    Sprinting = false;
                }
            }

            if (sprintToggleTimer > 0)
            {
                --sprintToggleTimer;
            }

            if (mc.playerController.IsPanoramaCamera())
            {
                posX = posZ = 0.5D;
                posX = 0.0D;
                posZ = 0.0D;
                rotationYaw = ticksExisted / 12.0F;
                rotationPitch = 10.0F;
                posY = 68.5D;
            }
            else
            {
                if (!mc.statFileWriter.hasAchievementUnlocked(AchievementList.openInventory))
                {
                    mc.guiAchievement.queueAchievementInformation(AchievementList.openInventory);
                }

                prevTimeInPortal = timeInPortal;
                bool z1;
                if (inPortal)
                {
                    if (!worldObj.isRemote && ridingEntity != null)
                    {
                        mountEntity(null);
                    }

                    if (mc.currentScreen != null)
                    {
                        mc.displayGuiScreen(null);
                    }

                    if (timeInPortal == 0.0F)
                    {
                        mc.sndManager.playSoundFX("portal.trigger", 1.0F, rand.NextSingle() * 0.4F + 0.8F);
                    }

                    timeInPortal += 0.0125F;
                    if (timeInPortal >= 1.0F)
                    {
                        timeInPortal = 1.0F;
                        if (!worldObj.isRemote)
                        {
                            timeUntilPortal = 10;
                            mc.sndManager.playSoundFX("portal.travel", 1.0F, rand.NextSingle() * 0.4F + 0.8F);
                            z1 = false;
                            sbyte b5;
                            if (dimension == -1)
                            {
                                b5 = 0;
                            }
                            else
                            {
                                b5 = -1;
                            }

                            mc.usePortal(b5);
                            triggerAchievement(AchievementList.portal);
                        }
                    }

                    inPortal = false;
                }
                else if (isPotionActive(Potion.confusion) && getActivePotionEffect(Potion.confusion).Duration > 60)
                {
                    timeInPortal += 0.006666667F;
                    if (timeInPortal > 1.0F)
                    {
                        timeInPortal = 1.0F;
                    }
                }
                else
                {
                    if (timeInPortal > 0.0F)
                    {
                        timeInPortal -= 0.05F;
                    }

                    if (timeInPortal < 0.0F)
                    {
                        timeInPortal = 0.0F;
                    }
                }

                if (timeUntilPortal > 0)
                {
                    --timeUntilPortal;
                }

                z1 = movementInput.jump;
                float f2 = 0.8F;
                bool z3 = movementInput.moveForward >= f2;
                movementInput.func_52013_a();
                if (UsingItem)
                {
                    movementInput.moveStrafe *= 0.2F;
                    movementInput.moveForward *= 0.2F;
                    sprintToggleTimer = 0;
                }

                if (movementInput.sneak && ySize < 0.2F)
                {
                    ySize = 0.2F;
                }

                pushOutOfBlocks(posX - width * 0.35D, boundingBox.minY + 0.5D, posZ + width * 0.35D);
                pushOutOfBlocks(posX - width * 0.35D, boundingBox.minY + 0.5D, posZ - width * 0.35D);
                pushOutOfBlocks(posX + width * 0.35D, boundingBox.minY + 0.5D, posZ - width * 0.35D);
                pushOutOfBlocks(posX + width * 0.35D, boundingBox.minY + 0.5D, posZ + width * 0.35D);
                bool z4 = FoodStats.FoodLevel > 6.0F;
                if (onGround && !z3 && movementInput.moveForward >= f2 && !Sprinting && z4 && !UsingItem && !isPotionActive(Potion.blindness))
                {
                    if (sprintToggleTimer == 0)
                    {
                        sprintToggleTimer = 7;
                    }
                    else
                    {
                        Sprinting = true;
                        sprintToggleTimer = 0;
                    }
                }

                if (Sneaking)
                {
                    sprintToggleTimer = 0;
                }

                if (Sprinting && (movementInput.moveForward < f2 || isCollidedHorizontally || !z4))
                {
                    Sprinting = false;
                }

                if (capabilities.allowFlying && !z1 && movementInput.jump)
                {
                    if (flyToggleTimer == 0)
                    {
                        flyToggleTimer = 7;
                    }
                    else
                    {
                        capabilities.isFlying = !capabilities.isFlying;
                        func_50009_aI();
                        flyToggleTimer = 0;
                    }
                }

                if (capabilities.isFlying)
                {
                    if (movementInput.sneak)
                    {
                        motionY -= 0.15D;
                    }

                    if (movementInput.jump)
                    {
                        motionY += 0.15D;
                    }
                }

                base.onLivingUpdate();
                if (onGround && capabilities.isFlying)
                {
                    capabilities.isFlying = false;
                    func_50009_aI();
                }

            }
        }

        public override void travelToTheEnd(int i1)
        {
            if (!worldObj.isRemote)
            {
                if (dimension == 1 && i1 == 1)
                {
                    triggerAchievement(AchievementList.theEnd2);
                    mc.displayGuiScreen(new GuiWinGame());
                }
                else
                {
                    triggerAchievement(AchievementList.theEnd);
                    mc.sndManager.playSoundFX("portal.travel", 1.0F, rand.NextSingle() * 0.4F + 0.8F);
                    mc.usePortal(1);
                }

            }
        }

        public virtual float FOVMultiplier
        {
            get
            {
                float f1 = 1.0F;
                if (capabilities.isFlying)
                {
                    f1 *= 1.1F;
                }

                f1 *= (landMovementFactor * SpeedModifier / speedOnGround + 1.0F) / 2.0F;
                if (UsingItem && ItemInUse.itemID == Item.bow.shiftedIndex)
                {
                    int i2 = ItemInUseDuration;
                    float f3 = i2 / 20.0F;
                    if (f3 > 1.0F)
                    {
                        f3 = 1.0F;
                    }
                    else
                    {
                        f3 *= f3;
                    }

                    f1 *= 1.0F - f3 * 0.15F;
                }

                return f1;
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nBTTagCompound1)
        {
            base.writeEntityToNBT(nBTTagCompound1);
            nBTTagCompound1.setInteger("Score", score);
        }

        public override void readEntityFromNBT(NBTTagCompound nBTTagCompound1)
        {
            base.readEntityFromNBT(nBTTagCompound1);
            score = nBTTagCompound1.getInteger("Score");
        }

        public override void closeScreen()
        {
            base.closeScreen();
            mc.displayGuiScreen(null);
        }

        public override void displayGUIEditSign(TileEntitySign tileEntitySign1)
        {
            mc.displayGuiScreen(new GuiEditSign(tileEntitySign1));
        }

        public override void displayGUIChest(IInventory iInventory1)
        {
            mc.displayGuiScreen(new GuiChest(inventory, iInventory1));
        }

        public override void displayWorkbenchGUI(int i1, int i2, int i3)
        {
            mc.displayGuiScreen(new GuiCrafting(inventory, worldObj, i1, i2, i3));
        }

        public override void displayGUIEnchantment(int i1, int i2, int i3)
        {
            mc.displayGuiScreen(new GuiEnchantment(inventory, worldObj, i1, i2, i3));
        }

        public override void displayGUIFurnace(TileEntityFurnace tileEntityFurnace1)
        {
            mc.displayGuiScreen(new GuiFurnace(inventory, tileEntityFurnace1));
        }

        public override void displayGUIBrewingStand(TileEntityBrewingStand tileEntityBrewingStand1)
        {
            mc.displayGuiScreen(new GuiBrewingStand(inventory, tileEntityBrewingStand1));
        }

        public override void displayGUIDispenser(TileEntityDispenser tileEntityDispenser1)
        {
            mc.displayGuiScreen(new GuiDispenser(inventory, tileEntityDispenser1));
        }

        public override void onCriticalHit(Entity entity1)
        {
            mc.effectRenderer.addEffect(new EntityCrit2FX(mc.theWorld, entity1));
        }

        public override void onEnchantmentCritical(Entity entity1)
        {
            EntityCrit2FX entityCrit2FX2 = new EntityCrit2FX(mc.theWorld, entity1, "magicCrit");
            mc.effectRenderer.addEffect(entityCrit2FX2);
        }

        public override void onItemPickup(Entity entity1, int i2)
        {
            mc.effectRenderer.addEffect(new EntityPickupFX(mc.theWorld, entity1, this, -0.5F));
        }

        public virtual void sendChatMessage(string string1)
        {
        }

        public override bool Sneaking
        {
            get
            {
                return movementInput.sneak && !sleeping;
            }
        }

        public virtual int Health
        {
            set
            {
                int i2 = health - value;
                if (i2 <= 0)
                {
                    EntityHealth = value;
                    if (i2 < 0)
                    {
                        heartsLife = heartsHalvesLife / 2;
                    }
                }
                else
                {
                    naturalArmorRating = i2;
                    EntityHealth = Health;
                    heartsLife = heartsHalvesLife;
                    damageEntity(DamageSource.generic, i2);
                    hurtTime = maxHurtTime = 10;
                }

            }

            get
            {
                return health;
            }
        }

        public override void respawnPlayer()
        {
            mc.respawn(false, 0, false);
        }

        public override void func_6420_o()
        {
        }

        public override void addChatMessage(string string1)
        {
            mc.ingameGUI.addChatMessageTranslate(string1);
        }

        public override void addStat(StatBase statBase1, int i2)
        {
            if (statBase1 != null)
            {
                if (statBase1.IsAchievement)
                {
                    Achievement achievement3 = (Achievement)statBase1;
                    if (achievement3.parentAchievement == null || mc.statFileWriter.hasAchievementUnlocked(achievement3.parentAchievement))
                    {
                        if (!mc.statFileWriter.hasAchievementUnlocked(achievement3))
                        {
                            mc.guiAchievement.queueTakenAchievement(achievement3);
                        }

                        mc.statFileWriter.readStat(statBase1, i2);
                    }
                }
                else
                {
                    mc.statFileWriter.readStat(statBase1, i2);
                }

            }
        }

        private bool isBlockTranslucent(int i1, int i2, int i3)
        {
            return worldObj.isBlockNormalCube(i1, i2, i3);
        }

        protected internal override bool pushOutOfBlocks(double d1, double d3, double d5)
        {
            int i7 = MathHelper.floor_double(d1);
            int i8 = MathHelper.floor_double(d3);
            int i9 = MathHelper.floor_double(d5);
            double d10 = d1 - i7;
            double d12 = d5 - i9;
            if (isBlockTranslucent(i7, i8, i9) || isBlockTranslucent(i7, i8 + 1, i9))
            {
                bool z14 = !isBlockTranslucent(i7 - 1, i8, i9) && !isBlockTranslucent(i7 - 1, i8 + 1, i9);
                bool z15 = !isBlockTranslucent(i7 + 1, i8, i9) && !isBlockTranslucent(i7 + 1, i8 + 1, i9);
                bool z16 = !isBlockTranslucent(i7, i8, i9 - 1) && !isBlockTranslucent(i7, i8 + 1, i9 - 1);
                bool z17 = !isBlockTranslucent(i7, i8, i9 + 1) && !isBlockTranslucent(i7, i8 + 1, i9 + 1);
                sbyte b18 = -1;
                double d19 = 9999.0D;
                if (z14 && d10 < d19)
                {
                    d19 = d10;
                    b18 = 0;
                }

                if (z15 && 1.0D - d10 < d19)
                {
                    d19 = 1.0D - d10;
                    b18 = 1;
                }

                if (z16 && d12 < d19)
                {
                    d19 = d12;
                    b18 = 4;
                }

                if (z17 && 1.0D - d12 < d19)
                {
                    d19 = 1.0D - d12;
                    b18 = 5;
                }

                float f21 = 0.1F;
                if (b18 == 0)
                {
                    motionX = -f21;
                }

                if (b18 == 1)
                {
                    motionX = f21;
                }

                if (b18 == 4)
                {
                    motionZ = -f21;
                }

                if (b18 == 5)
                {
                    motionZ = f21;
                }
            }

            return false;
        }

        public override bool Sprinting
        {
            set
            {
                base.Sprinting = value;
                sprintingTicksLeft = value ? 600 : 0;
            }
        }

        public virtual void setXPStats(float f1, int i2, int i3)
        {
            experience = f1;
            experienceTotal = i2;
            experienceLevel = i3;
        }
    }

}