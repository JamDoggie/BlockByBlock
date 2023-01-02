using System.Linq;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class TileEntityBrewingStand : TileEntity, IInventory
	{
		private ItemStack[] brewingItemStacks = new ItemStack[4];
		private int brewTime;
		private int filledSlots;
		private int ingredientID;

		public virtual string InvName
		{
			get
			{
				return "container.brewing";
			}
		}

		public virtual int SizeInventory
		{
			get
			{
				return brewingItemStacks.Length;
			}
		}

		public override void updateEntity()
		{
			if (brewTime > 0)
			{
				--brewTime;
				if (brewTime == 0)
				{
					brewPotions();
					onInventoryChanged();
				}
				else if (!canBrew())
				{
					brewTime = 0;
					onInventoryChanged();
				}
				else if (ingredientID != brewingItemStacks[3].itemID)
				{
					brewTime = 0;
					onInventoryChanged();
				}
			}
			else if (canBrew())
			{
				brewTime = 400;
				ingredientID = brewingItemStacks[3].itemID;
			}

			int i1 = FilledSlots;
			if (i1 != filledSlots)
			{
				filledSlots = i1;
				worldObj.setBlockMetadataWithNotify(xCoord, yCoord, zCoord, i1);
			}

			base.updateEntity();
		}

		public virtual int BrewTime
		{
			get
			{
				return brewTime;
			}
			set
			{
				brewTime = value;
			}
		}

		private bool canBrew()
		{
			if (brewingItemStacks[3] != null && brewingItemStacks[3].stackSize > 0)
			{
				ItemStack itemStack1 = brewingItemStacks[3];
				if (!Item.itemsList[itemStack1.itemID].PotionIngredient)
				{
					return false;
				}
				else
				{
					bool z2 = false;

					for (int i3 = 0; i3 < 3; ++i3)
					{
						if (brewingItemStacks[i3] != null && brewingItemStacks[i3].itemID == Item.potion.shiftedIndex)
						{
							int i4 = brewingItemStacks[i3].ItemDamage;
							int i5 = getPotionResult(i4, itemStack1);
							if (!ItemPotion.isSplash(i4) && ItemPotion.isSplash(i5))
							{
								z2 = true;
								break;
							}

							List<PotionEffect> list6 = Item.potion.getEffects(i4);
							List<PotionEffect> list7 = Item.potion.getEffects(i5);
                            
							if ((i4 <= 0 || list6 != list7) && (list6 == null || !list6.SequenceEqual(list7) && list7 != null) && i4 != i5)
							{
								z2 = true;
								break;
							}
						}
					}

					return z2;
				}
			}
			else
			{
				return false;
			}
		}

		private void brewPotions()
		{
			if (canBrew())
			{
				ItemStack itemStack1 = brewingItemStacks[3];

				for (int i2 = 0; i2 < 3; ++i2)
				{
					if (brewingItemStacks[i2] != null && brewingItemStacks[i2].itemID == Item.potion.shiftedIndex)
					{
						int i3 = brewingItemStacks[i2].ItemDamage;
						int i4 = getPotionResult(i3, itemStack1);
						List<PotionEffect> list5 = Item.potion.getEffects(i3);
						List<PotionEffect> list6 = Item.potion.getEffects(i4);
						if ((i3 <= 0 || list5 != list6) && (list5 == null || !list5.SequenceEqual(list6) && list6 != null))
						{
							if (i3 != i4)
							{
								brewingItemStacks[i2].ItemDamage = i4;
							}
						}
						else if (!ItemPotion.isSplash(i3) && ItemPotion.isSplash(i4))
						{
							brewingItemStacks[i2].ItemDamage = i4;
						}
					}
				}

				if (Item.itemsList[itemStack1.itemID].hasContainerItem())
				{
					brewingItemStacks[3] = new ItemStack(Item.itemsList[itemStack1.itemID].ContainerItem);
				}
				else
				{
					--brewingItemStacks[3].stackSize;
					if (brewingItemStacks[3].stackSize <= 0)
					{
						brewingItemStacks[3] = null;
					}
				}

			}
		}

		private int getPotionResult(int i1, ItemStack itemStack2)
		{
			return itemStack2 == null ? i1 : (Item.itemsList[itemStack2.itemID].PotionIngredient ? PotionHelper.applyIngredient(i1, Item.itemsList[itemStack2.itemID].PotionEffect) : i1);
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readFromNBT(nBTTagCompound1);
			NBTTagList nBTTagList2 = nBTTagCompound1.getTagList("Items");
			brewingItemStacks = new ItemStack[SizeInventory];

			for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
			{
				NBTTagCompound nBTTagCompound4 = (NBTTagCompound)nBTTagList2.tagAt(i3);
				sbyte b5 = nBTTagCompound4.getByte("Slot");
				if (b5 >= 0 && b5 < brewingItemStacks.Length)
				{
					brewingItemStacks[b5] = ItemStack.loadItemStackFromNBT(nBTTagCompound4);
				}
			}

			brewTime = nBTTagCompound1.getShort("BrewTime");
		}

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			nBTTagCompound1.setShort("BrewTime", (short)brewTime);
			NBTTagList nBTTagList2 = new NBTTagList();

			for (int i3 = 0; i3 < brewingItemStacks.Length; ++i3)
			{
				if (brewingItemStacks[i3] != null)
				{
					NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
					nBTTagCompound4.setByte("Slot", (sbyte)i3);
					brewingItemStacks[i3].writeToNBT(nBTTagCompound4);
					nBTTagList2.appendTag(nBTTagCompound4);
				}
			}

			nBTTagCompound1.setTag("Items", nBTTagList2);
		}

		public virtual ItemStack getStackInSlot(int i1)
		{
			return i1 >= 0 && i1 < brewingItemStacks.Length ? brewingItemStacks[i1] : null;
		}

		public virtual ItemStack decrStackSize(int i1, int i2)
		{
			if (i1 >= 0 && i1 < brewingItemStacks.Length)
			{
				ItemStack itemStack3 = brewingItemStacks[i1];
				brewingItemStacks[i1] = null;
				return itemStack3;
			}
			else
			{
				return null;
			}
		}

		public virtual ItemStack getStackInSlotOnClosing(int i1)
		{
			if (i1 >= 0 && i1 < brewingItemStacks.Length)
			{
				ItemStack itemStack2 = brewingItemStacks[i1];
				brewingItemStacks[i1] = null;
				return itemStack2;
			}
			else
			{
				return null;
			}
		}

		public virtual void setInventorySlotContents(int i1, ItemStack itemStack2)
		{
			if (i1 >= 0 && i1 < brewingItemStacks.Length)
			{
				brewingItemStacks[i1] = itemStack2;
			}

		}

		public virtual int InventoryStackLimit
		{
			get
			{
				return 1;
			}
		}

		public virtual bool isUseableByPlayer(EntityPlayer entityPlayer1)
		{
			return worldObj.getBlockTileEntity(xCoord, yCoord, zCoord) != this ? false : entityPlayer1.getDistanceSq((double)xCoord + 0.5D, (double)yCoord + 0.5D, (double)zCoord + 0.5D) <= 64.0D;
		}

		public virtual void openChest()
		{
		}

		public virtual void closeChest()
		{
		}


		public virtual int FilledSlots
		{
			get
			{
				int i1 = 0;
    
				for (int i2 = 0; i2 < 3; ++i2)
				{
					if (brewingItemStacks[i2] != null)
					{
						i1 |= 1 << i2;
					}
				}
    
				return i1;
			}
		}
	}

}