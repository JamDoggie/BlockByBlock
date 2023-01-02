using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class TileEntityChest : TileEntity, IInventory
	{
		private ItemStack[] chestContents = new ItemStack[36];
		public bool adjacentChestChecked = false;
		public TileEntityChest adjacentChestZNeg;
		public TileEntityChest adjacentChestXPos;
		public TileEntityChest adjacentChestXNeg;
		public TileEntityChest adjacentChestZPos;
		public float lidAngle;
		public float prevLidAngle;
		public int numUsingPlayers;
		private int ticksSinceSync;

		public virtual int SizeInventory
		{
			get
			{
				return 27;
			}
		}

		public virtual ItemStack getStackInSlot(int i1)
		{
			return this.chestContents[i1];
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			if (this.chestContents[i1] != null)
			{
				ItemStack itemStack3;
				if (this.chestContents[i1].stackSize <= i2)
				{
					itemStack3 = this.chestContents[i1];
					this.chestContents[i1] = null;
					this.onInventoryChanged();
					return itemStack3;
				}
				else
				{
					itemStack3 = this.chestContents[i1].splitStack(i2);
					if (this.chestContents[i1].stackSize == 0)
					{
						this.chestContents[i1] = null;
					}

					this.onInventoryChanged();
					return itemStack3;
				}
			}
			else
			{
				return null;
			}
		}

		public virtual ItemStack getStackInSlotOnClosing(int i1)
		{
			if (this.chestContents[i1] != null)
			{
				ItemStack itemStack2 = this.chestContents[i1];
				this.chestContents[i1] = null;
				return itemStack2;
			}
			else
			{
				return null;
			}
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			this.chestContents[i1] = itemStack2;
			if (itemStack2 != null && itemStack2.stackSize > this.InventoryStackLimit)
			{
				itemStack2.stackSize = this.InventoryStackLimit;
			}

			this.onInventoryChanged();
		}

		public virtual string InvName
		{
			get
			{
				return "container.chest";
			}
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readFromNBT(nBTTagCompound1);
			NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("Items");
			this.chestContents = new ItemStack[this.SizeInventory];

			for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
			{
				NBTTagCompound nBTTagCompound4 = (NBTTagCompound)nBTTagList2.tagAt(i3);
				int i5 = nBTTagCompound4.getByte("Slot") & 255;
				if (i5 >= 0 && i5 < this.chestContents.Length)
				{
					this.chestContents[i5] = ItemStack.loadItemStackFromNBT(nBTTagCompound4);
				}
			}

		}

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			NBTTagList nBTTagList2 = new NBTTagList();

			for (int i3 = 0; i3 < this.chestContents.Length; ++i3)
			{
				if (this.chestContents[i3] != null)
				{
					NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
					nBTTagCompound4.setByte("Slot", (sbyte)i3);
					this.chestContents[i3].writeToNBT(nBTTagCompound4);
					nBTTagList2.appendTag(nBTTagCompound4);
				}
			}

			nBTTagCompound1.setTag("Items", nBTTagList2);
		}

		public virtual int InventoryStackLimit
		{
			get
			{
				return 64;
			}
		}

		public virtual bool isUseableByPlayer(EntityPlayer entityPlayer1)
		{
			return this.worldObj.getBlockTileEntity(this.xCoord, this.yCoord, this.zCoord) != this ? false : entityPlayer1.getDistanceSq((double)this.xCoord + 0.5D, (double)this.yCoord + 0.5D, (double)this.zCoord + 0.5D) <= 64.0D;
		}

		public override void updateContainingBlockInfo()
		{
			base.updateContainingBlockInfo();
			this.adjacentChestChecked = false;
		}

		public virtual void checkForAdjacentChests()
		{
			if (!this.adjacentChestChecked)
			{
				this.adjacentChestChecked = true;
				this.adjacentChestZNeg = null;
				this.adjacentChestXPos = null;
				this.adjacentChestXNeg = null;
				this.adjacentChestZPos = null;
				if (this.worldObj.getBlockId(this.xCoord - 1, this.yCoord, this.zCoord) == Block.chest.blockID)
				{
					this.adjacentChestXNeg = (TileEntityChest)this.worldObj.getBlockTileEntity(this.xCoord - 1, this.yCoord, this.zCoord);
				}

				if (this.worldObj.getBlockId(this.xCoord + 1, this.yCoord, this.zCoord) == Block.chest.blockID)
				{
					this.adjacentChestXPos = (TileEntityChest)this.worldObj.getBlockTileEntity(this.xCoord + 1, this.yCoord, this.zCoord);
				}

				if (this.worldObj.getBlockId(this.xCoord, this.yCoord, this.zCoord - 1) == Block.chest.blockID)
				{
					this.adjacentChestZNeg = (TileEntityChest)this.worldObj.getBlockTileEntity(this.xCoord, this.yCoord, this.zCoord - 1);
				}

				if (this.worldObj.getBlockId(this.xCoord, this.yCoord, this.zCoord + 1) == Block.chest.blockID)
				{
					this.adjacentChestZPos = (TileEntityChest)this.worldObj.getBlockTileEntity(this.xCoord, this.yCoord, this.zCoord + 1);
				}

				if (this.adjacentChestZNeg != null)
				{
					this.adjacentChestZNeg.updateContainingBlockInfo();
				}

				if (this.adjacentChestZPos != null)
				{
					this.adjacentChestZPos.updateContainingBlockInfo();
				}

				if (this.adjacentChestXPos != null)
				{
					this.adjacentChestXPos.updateContainingBlockInfo();
				}

				if (this.adjacentChestXNeg != null)
				{
					this.adjacentChestXNeg.updateContainingBlockInfo();
				}

			}
		}

		public override void updateEntity()
		{
			base.updateEntity();
			this.checkForAdjacentChests();
			if (++this.ticksSinceSync % 20 * 4 == 0)
			{
				this.worldObj.playNoteAt(this.xCoord, this.yCoord, this.zCoord, 1, this.numUsingPlayers);
			}

			this.prevLidAngle = this.lidAngle;
			float f1 = 0.1F;
			double d4;
			if (this.numUsingPlayers > 0 && this.lidAngle == 0.0F && this.adjacentChestZNeg == null && this.adjacentChestXNeg == null)
			{
				double d2 = (double)this.xCoord + 0.5D;
				d4 = (double)this.zCoord + 0.5D;
				if (this.adjacentChestZPos != null)
				{
					d4 += 0.5D;
				}

				if (this.adjacentChestXPos != null)
				{
					d2 += 0.5D;
				}

				this.worldObj.playSoundEffect(d2, (double)this.yCoord + 0.5D, d4, "random.chestopen", 0.5F, this.worldObj.rand.NextSingle() * 0.1F + 0.9F);
			}

			if (this.numUsingPlayers == 0 && this.lidAngle > 0.0F || this.numUsingPlayers > 0 && this.lidAngle < 1.0F)
			{
				float f8 = this.lidAngle;
				if (this.numUsingPlayers > 0)
				{
					this.lidAngle += f1;
				}
				else
				{
					this.lidAngle -= f1;
				}

				if (this.lidAngle > 1.0F)
				{
					this.lidAngle = 1.0F;
				}

				float f3 = 0.5F;
				if (this.lidAngle < f3 && f8 >= f3 && this.adjacentChestZNeg == null && this.adjacentChestXNeg == null)
				{
					d4 = (double)this.xCoord + 0.5D;
					double d6 = (double)this.zCoord + 0.5D;
					if (this.adjacentChestZPos != null)
					{
						d6 += 0.5D;
					}

					if (this.adjacentChestXPos != null)
					{
						d4 += 0.5D;
					}

					this.worldObj.playSoundEffect(d4, (double)this.yCoord + 0.5D, d6, "random.chestclosed", 0.5F, this.worldObj.rand.NextSingle() * 0.1F + 0.9F);
				}

				if (this.lidAngle < 0.0F)
				{
					this.lidAngle = 0.0F;
				}
			}

		}

		public override void onTileEntityPowered(int i1, int i2)
		{
			if (i1 == 1)
			{
				this.numUsingPlayers = i2;
			}

		}

		public virtual void openChest()
		{
			++this.numUsingPlayers;
			this.worldObj.playNoteAt(this.xCoord, this.yCoord, this.zCoord, 1, this.numUsingPlayers);
		}

		public virtual void closeChest()
		{
			--this.numUsingPlayers;
			this.worldObj.playNoteAt(this.xCoord, this.yCoord, this.zCoord, 1, this.numUsingPlayers);
		}

		public override void invalidate()
		{
			this.updateContainingBlockInfo();
			this.checkForAdjacentChests();
			base.invalidate();
		}
	}

}