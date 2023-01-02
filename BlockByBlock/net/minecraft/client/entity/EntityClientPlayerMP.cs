using net.minecraft.src;

namespace net.minecraft.client.entity
{
    using Minecraft = Minecraft;

    public class EntityClientPlayerMP : EntityPlayerSP
    {
        public NetClientHandler sendQueue;
        private int inventoryUpdateTickCounter = 0;
        private double oldPosX;
        private double oldMinY;
        private double oldPosY;
        private double oldPosZ;
        private float oldRotationYaw;
        private float oldRotationPitch;
        private bool wasOnGround = false;
        private bool shouldStopSneaking = false;
        private bool wasSneaking = false;
        private int timeSinceMoved = 0;
        private bool hasSetHealth = false;

        public EntityClientPlayerMP(Minecraft minecraft1, World world2, Session session3, NetClientHandler netClientHandler4) : base(minecraft1, world2, session3, 0)
        {
            sendQueue = netClientHandler4;
        }

        public override bool attackEntityFrom(DamageSource damageSource1, int i2)
        {
            return false;
        }

        public override void heal(int i1)
        {
        }

        public override void onUpdate()
        {
            if (worldObj.blockExists(MathHelper.floor_double(posX), 0, MathHelper.floor_double(posZ)))
            {
                base.onUpdate();
                sendMotionUpdates();
            }
        }

        public virtual void sendMotionUpdates()
        {
            if (inventoryUpdateTickCounter++ == 20)
            {
                inventoryUpdateTickCounter = 0;
            }

            bool z1 = Sprinting;
            if (z1 != wasSneaking)
            {
                if (z1)
                {
                    sendQueue.addToSendQueue(new Packet19EntityAction(this, 4));
                }
                else
                {
                    sendQueue.addToSendQueue(new Packet19EntityAction(this, 5));
                }

                wasSneaking = z1;
            }

            bool z2 = Sneaking;
            if (z2 != shouldStopSneaking)
            {
                if (z2)
                {
                    sendQueue.addToSendQueue(new Packet19EntityAction(this, 1));
                }
                else
                {
                    sendQueue.addToSendQueue(new Packet19EntityAction(this, 2));
                }

                shouldStopSneaking = z2;
            }

            double d3 = posX - oldPosX;
            double d5 = boundingBox.minY - oldMinY;
            double d7 = posY - oldPosY;
            double d9 = posZ - oldPosZ;
            double d11 = (double)(rotationYaw - oldRotationYaw);
            double d13 = (double)(rotationPitch - oldRotationPitch);
            bool z15 = d5 != 0.0D || d7 != 0.0D || d3 != 0.0D || d9 != 0.0D;
            bool z16 = d11 != 0.0D || d13 != 0.0D;
            if (ridingEntity != null)
            {
                if (z16)
                {
                    sendQueue.addToSendQueue(new Packet11PlayerPosition(motionX, -999.0D, -999.0D, motionZ, onGround));
                }
                else
                {
                    sendQueue.addToSendQueue(new Packet13PlayerLookMove(motionX, -999.0D, -999.0D, motionZ, rotationYaw, rotationPitch, onGround));
                }

                z15 = false;
            }
            else if (z15 && z16)
            {
                sendQueue.addToSendQueue(new Packet13PlayerLookMove(posX, boundingBox.minY, posY, posZ, rotationYaw, rotationPitch, onGround));
                timeSinceMoved = 0;
            }
            else if (z15)
            {
                sendQueue.addToSendQueue(new Packet11PlayerPosition(posX, boundingBox.minY, posY, posZ, onGround));
                timeSinceMoved = 0;
            }
            else if (z16)
            {
                sendQueue.addToSendQueue(new Packet12PlayerLook(rotationYaw, rotationPitch, onGround));
                timeSinceMoved = 0;
            }
            else
            {
                sendQueue.addToSendQueue(new Packet10Flying(onGround));
                if (wasOnGround == onGround && timeSinceMoved <= 200)
                {
                    ++timeSinceMoved;
                }
                else
                {
                    timeSinceMoved = 0;
                }
            }

            wasOnGround = onGround;
            if (z15)
            {
                oldPosX = posX;
                oldMinY = boundingBox.minY;
                oldPosY = posY;
                oldPosZ = posZ;
            }

            if (z16)
            {
                oldRotationYaw = rotationYaw;
                oldRotationPitch = rotationPitch;
            }

        }

        public override EntityItem dropOneItem()
        {
            sendQueue.addToSendQueue(new Packet14BlockDig(4, 0, 0, 0, 0));
            return null;
        }

        protected internal override void joinEntityItemWithWorld(EntityItem entityItem1)
        {
        }

        public override void sendChatMessage(string string1)
        {
            if (mc.ingameGUI.func_50013_c().Count == 0 || !((string)mc.ingameGUI.func_50013_c()[mc.ingameGUI.func_50013_c().Count - 1]).Equals(string1))
            {
                mc.ingameGUI.func_50013_c().Add(string1);
            }

            sendQueue.addToSendQueue(new Packet3Chat(string1));
        }

        public override void swingItem()
        {
            base.swingItem();
            sendQueue.addToSendQueue(new Packet18Animation(this, 1));
        }

        public override void respawnPlayer()
        {
            sendQueue.addToSendQueue(new Packet9Respawn(dimension, (sbyte)worldObj.difficultySetting, worldObj.WorldInfo.TerrainType, worldObj.Height, 0));
        }

        protected internal override void damageEntity(DamageSource damageSource1, int i2)
        {
            EntityHealth = Health - i2;
        }

        public override void closeScreen()
        {
            sendQueue.addToSendQueue(new Packet101CloseWindow(craftingInventory.windowId));
            inventory.ItemStack = null;
            base.closeScreen();
        }

        public override int Health
        {
            set
            {
                if (hasSetHealth)
                {
                    base.Health = value;
                }
                else
                {
                    EntityHealth = value;
                    hasSetHealth = true;
                }

            }

            get
            {
                return health;
            }
        }

        public override void addStat(StatBase statBase1, int i2)
        {
            if (statBase1 != null)
            {
                if (statBase1.isIndependent)
                {
                    base.addStat(statBase1, i2);
                }

            }
        }

        public virtual void incrementStat(StatBase statBase1, int i2)
        {
            if (statBase1 != null)
            {
                if (!statBase1.isIndependent)
                {
                    base.addStat(statBase1, i2);
                }

            }
        }

        public override void func_50009_aI()
        {
            sendQueue.addToSendQueue(new Packet202PlayerAbilities(capabilities));
        }
    }

}