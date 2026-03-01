namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

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
			this.sendQueue = netClientHandler4;
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
			if (this.worldObj.blockExists(MathHelper.floor_double(this.posX), 0, MathHelper.floor_double(this.posZ)))
			{
				base.onUpdate();
				this.sendMotionUpdates();
			}
		}

		public virtual void sendMotionUpdates()
		{
			if (this.inventoryUpdateTickCounter++ == 20)
			{
				this.inventoryUpdateTickCounter = 0;
			}

			bool z1 = this.Sprinting;
			if (z1 != this.wasSneaking)
			{
				if (z1)
				{
					this.sendQueue.addToSendQueue(new Packet19EntityAction(this, 4));
				}
				else
				{
					this.sendQueue.addToSendQueue(new Packet19EntityAction(this, 5));
				}

				this.wasSneaking = z1;
			}

			bool z2 = this.Sneaking;
			if (z2 != this.shouldStopSneaking)
			{
				if (z2)
				{
					this.sendQueue.addToSendQueue(new Packet19EntityAction(this, 1));
				}
				else
				{
					this.sendQueue.addToSendQueue(new Packet19EntityAction(this, 2));
				}

				this.shouldStopSneaking = z2;
			}

			double d3 = this.posX - this.oldPosX;
			double d5 = this.boundingBox.minY - this.oldMinY;
			double d7 = this.posY - this.oldPosY;
			double d9 = this.posZ - this.oldPosZ;
			double d11 = (double)(this.rotationYaw - this.oldRotationYaw);
			double d13 = (double)(this.rotationPitch - this.oldRotationPitch);
			bool z15 = d5 != 0.0D || d7 != 0.0D || d3 != 0.0D || d9 != 0.0D;
			bool z16 = d11 != 0.0D || d13 != 0.0D;
			if (this.ridingEntity != null)
			{
				if (z16)
				{
					this.sendQueue.addToSendQueue(new Packet11PlayerPosition(this.motionX, -999.0D, -999.0D, this.motionZ, this.onGround));
				}
				else
				{
					this.sendQueue.addToSendQueue(new Packet13PlayerLookMove(this.motionX, -999.0D, -999.0D, this.motionZ, this.rotationYaw, this.rotationPitch, this.onGround));
				}

				z15 = false;
			}
			else if (z15 && z16)
			{
				this.sendQueue.addToSendQueue(new Packet13PlayerLookMove(this.posX, this.boundingBox.minY, this.posY, this.posZ, this.rotationYaw, this.rotationPitch, this.onGround));
				this.timeSinceMoved = 0;
			}
			else if (z15)
			{
				this.sendQueue.addToSendQueue(new Packet11PlayerPosition(this.posX, this.boundingBox.minY, this.posY, this.posZ, this.onGround));
				this.timeSinceMoved = 0;
			}
			else if (z16)
			{
				this.sendQueue.addToSendQueue(new Packet12PlayerLook(this.rotationYaw, this.rotationPitch, this.onGround));
				this.timeSinceMoved = 0;
			}
			else
			{
				this.sendQueue.addToSendQueue(new Packet10Flying(this.onGround));
				if (this.wasOnGround == this.onGround && this.timeSinceMoved <= 200)
				{
					++this.timeSinceMoved;
				}
				else
				{
					this.timeSinceMoved = 0;
				}
			}

			this.wasOnGround = this.onGround;
			if (z15)
			{
				this.oldPosX = this.posX;
				this.oldMinY = this.boundingBox.minY;
				this.oldPosY = this.posY;
				this.oldPosZ = this.posZ;
			}

			if (z16)
			{
				this.oldRotationYaw = this.rotationYaw;
				this.oldRotationPitch = this.rotationPitch;
			}

		}

		public override EntityItem dropOneItem()
		{
			this.sendQueue.addToSendQueue(new Packet14BlockDig(4, 0, 0, 0, 0));
			return null;
		}

		protected internal override void joinEntityItemWithWorld(EntityItem entityItem1)
		{
		}

		public override void sendChatMessage(string string1)
		{
			if (this.mc.ingameGUI.func_50013_c().Count == 0 || !((string)this.mc.ingameGUI.func_50013_c()[this.mc.ingameGUI.func_50013_c().Count - 1]).Equals(string1))
			{
				this.mc.ingameGUI.func_50013_c().Add(string1);
			}

			this.sendQueue.addToSendQueue(new Packet3Chat(string1));
		}

		public override void swingItem()
		{
			base.swingItem();
			this.sendQueue.addToSendQueue(new Packet18Animation(this, 1));
		}

		public override void respawnPlayer()
		{
			this.sendQueue.addToSendQueue(new Packet9Respawn(this.dimension, (sbyte)this.worldObj.difficultySetting, this.worldObj.WorldInfo.TerrainType, this.worldObj.Height, 0));
		}

		protected internal override void damageEntity(DamageSource damageSource1, int i2)
		{
			this.EntityHealth = this.Health - i2;
		}

		public override void closeScreen()
		{
			this.sendQueue.addToSendQueue(new Packet101CloseWindow(this.craftingInventory.windowId));
			this.inventory.ItemStack = (ItemStack)null;
			base.closeScreen();
		}

		public override int Health
		{
			set
			{
				if (this.hasSetHealth)
				{
					base.Health = value;
				}
				else
				{
					this.EntityHealth = value;
					this.hasSetHealth = true;
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
			this.sendQueue.addToSendQueue(new Packet202PlayerAbilities(this.capabilities));
		}
	}

}