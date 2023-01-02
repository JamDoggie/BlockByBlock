using System.Collections;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public sealed class ItemStack
	{
		public int stackSize;
		public int animationsToGo;
		public int itemID;
		public NBTTagCompound stackTagCompound;
		private int itemDamage;

		public ItemStack(Block block1) : this((Block)block1, 1)
		{
		}

		public ItemStack(Block block1, int i2) : this(block1.blockID, i2, 0)
		{
		}

		public ItemStack(Block block1, int i2, int i3) : this(block1.blockID, i2, i3)
		{
		}

		public ItemStack(Item item1) : this(item1.shiftedIndex, 1, 0)
		{
		}

		public ItemStack(Item item1, int i2) : this(item1.shiftedIndex, i2, 0)
		{
		}

		public ItemStack(Item item1, int i2, int i3) : this(item1.shiftedIndex, i2, i3)
		{
		}

		public ItemStack(int i1, int i2, int i3)
		{
			stackSize = 0;
			itemID = i1;
			stackSize = i2;
			itemDamage = i3;
		}

		public static ItemStack loadItemStackFromNBT(NBTTagCompound nBTTagCompound0)
		{
			ItemStack itemStack1 = new ItemStack();
			itemStack1.readFromNBT(nBTTagCompound0);
			return itemStack1.Item != null ? itemStack1 : null;
		}

		private ItemStack()
		{
			this.stackSize = 0;
		}

		public ItemStack splitStack(int i1)
		{
			ItemStack itemStack2 = new ItemStack(this.itemID, i1, this.itemDamage);
			if (this.stackTagCompound != null)
			{
				itemStack2.stackTagCompound = (NBTTagCompound)this.stackTagCompound.copy();
			}

			this.stackSize -= i1;
			return itemStack2;
		}

		public Item Item
		{
			get
			{
				return net.minecraft.src.Item.itemsList[this.itemID];
			}
		}

		public int IconIndex
		{
			get
			{
				return this.Item.getIconIndex(this);
			}
		}

		public bool useItem(EntityPlayer entityPlayer1, World world2, int i3, int i4, int i5, int i6)
		{
			bool z7 = this.Item.onItemUse(this, entityPlayer1, world2, i3, i4, i5, i6);
			if (z7)
			{
				entityPlayer1.addStat(StatList.objectUseStats[this.itemID], 1);
			}

			return z7;
		}

		public float getStrVsBlock(Block block1)
		{
			return this.Item.getStrVsBlock(this, block1);
		}

		public ItemStack useItemRightClick(World world1, EntityPlayer entityPlayer2)
		{
			return this.Item.onItemRightClick(this, world1, entityPlayer2);
		}

		public ItemStack onFoodEaten(World world1, EntityPlayer entityPlayer2)
		{
			return this.Item.onFoodEaten(this, world1, entityPlayer2);
		}

		public NBTTagCompound writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			nBTTagCompound1.setShort("id", (short)this.itemID);
			nBTTagCompound1.setByte("Count", (sbyte)this.stackSize);
			nBTTagCompound1.setShort("Damage", (short)this.itemDamage);
			if (this.stackTagCompound != null)
			{
				nBTTagCompound1.setTag("tag", this.stackTagCompound);
			}

			return nBTTagCompound1;
		}

		public void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			this.itemID = nBTTagCompound1.getShort("id");
			this.stackSize = nBTTagCompound1.getByte("Count");
			this.itemDamage = nBTTagCompound1.getShort("Damage");
			if (nBTTagCompound1.hasKey("tag"))
			{
				this.stackTagCompound = nBTTagCompound1.getCompoundTag("tag");
			}

		}

		public int MaxStackSize
		{
			get
			{
				return this.Item.ItemStackLimit;
			}
		}

		public bool Stackable
		{
			get
			{
				return this.MaxStackSize > 1 && (!this.ItemStackDamageable || !this.ItemDamaged);
			}
		}

		public bool ItemStackDamageable
		{
			get
			{
				return net.minecraft.src.Item.itemsList[this.itemID].MaxDamage > 0;
			}
		}

		public bool HasSubtypes
		{
			get
			{
				return net.minecraft.src.Item.itemsList[this.itemID].HasSubtypes;
			}
		}

		public bool ItemDamaged
		{
			get
			{
				return this.ItemStackDamageable && this.itemDamage > 0;
			}
		}

		public int ItemDamageForDisplay
		{
			get
			{
				return this.itemDamage;
			}
		}

		public int ItemDamage
		{
			get
			{
				return this.itemDamage;
			}
			set
			{
				this.itemDamage = value;
			}
		}


		public int MaxDamage
		{
			get
			{
				return net.minecraft.src.Item.itemsList[this.itemID].MaxDamage;
			}
		}

		public void damageItem(int i1, EntityLiving entityLiving2)
		{
			if (this.ItemStackDamageable)
			{
				if (i1 > 0 && entityLiving2 is EntityPlayer)
				{
					int i3 = EnchantmentHelper.getUnbreakingModifier(((EntityPlayer)entityLiving2).inventory);
					if (i3 > 0 && entityLiving2.worldObj.rand.Next(i3 + 1) > 0)
					{
						return;
					}
				}

				this.itemDamage += i1;
				if (this.itemDamage > this.MaxDamage)
				{
					entityLiving2.renderBrokenItemStack(this);
					if (entityLiving2 is EntityPlayer)
					{
						((EntityPlayer)entityLiving2).addStat(StatList.objectBreakStats[this.itemID], 1);
					}

					--this.stackSize;
					if (this.stackSize < 0)
					{
						this.stackSize = 0;
					}

					this.itemDamage = 0;
				}

			}
		}

		public void hitEntity(EntityLiving entityLiving1, EntityPlayer entityPlayer2)
		{
			bool z3 = net.minecraft.src.Item.itemsList[this.itemID].hitEntity(this, entityLiving1, entityPlayer2);
			if (z3)
			{
				entityPlayer2.addStat(StatList.objectUseStats[this.itemID], 1);
			}

		}

		public void onDestroyBlock(int i1, int i2, int i3, int i4, EntityPlayer entityPlayer5)
		{
			bool z6 = net.minecraft.src.Item.itemsList[this.itemID].onBlockDestroyed(this, i1, i2, i3, i4, entityPlayer5);
			if (z6)
			{
				entityPlayer5.addStat(StatList.objectUseStats[this.itemID], 1);
			}

		}

		public int getDamageVsEntity(Entity entity1)
		{
			return net.minecraft.src.Item.itemsList[this.itemID].getDamageVsEntity(entity1);
		}

		public bool canHarvestBlock(Block block1)
		{
			return net.minecraft.src.Item.itemsList[this.itemID].canHarvestBlock(block1);
		}

		public void onItemDestroyedByUse(EntityPlayer entityPlayer1)
		{
		}

		public void useItemOnEntity(EntityLiving entityLiving1)
		{
			net.minecraft.src.Item.itemsList[this.itemID].useItemOnEntity(this, entityLiving1);
		}

		public ItemStack copy()
		{
			ItemStack itemStack1 = new ItemStack(this.itemID, this.stackSize, this.itemDamage);
			if (this.stackTagCompound != null)
			{
				itemStack1.stackTagCompound = (NBTTagCompound)this.stackTagCompound.copy();
				if (!itemStack1.stackTagCompound.Equals(this.stackTagCompound))
				{
					return itemStack1;
				}
			}

			return itemStack1;
		}

		public static bool func_46154_a(ItemStack itemStack0, ItemStack itemStack1)
		{
			return itemStack0 == null && itemStack1 == null ? true : (itemStack0 != null && itemStack1 != null ? (itemStack0.stackTagCompound == null && itemStack1.stackTagCompound != null ? false : itemStack0.stackTagCompound == null || itemStack0.stackTagCompound.Equals(itemStack1.stackTagCompound)) : false);
		}

		public static bool areItemStacksEqual(ItemStack itemStack0, ItemStack itemStack1)
		{
			return itemStack0 == null && itemStack1 == null ? true : (itemStack0 != null && itemStack1 != null ? itemStack0.isItemStackEqual(itemStack1) : false);
		}

		private bool isItemStackEqual(ItemStack itemStack1)
		{
			return this.stackSize != itemStack1.stackSize ? false : (this.itemID != itemStack1.itemID ? false : (this.itemDamage != itemStack1.itemDamage ? false : (this.stackTagCompound == null && itemStack1.stackTagCompound != null ? false : this.stackTagCompound == null || this.stackTagCompound.Equals(itemStack1.stackTagCompound))));
		}

		public bool isItemEqual(ItemStack itemStack1)
		{
			return this.itemID == itemStack1.itemID && this.itemDamage == itemStack1.itemDamage;
		}

		public static ItemStack copyItemStack(ItemStack itemStack0)
		{
			return itemStack0 == null ? null : itemStack0.copy();
		}

		public override string ToString()
		{
			return this.stackSize + "x" + net.minecraft.src.Item.itemsList[this.itemID].ItemName + "@" + this.itemDamage;
		}

		public void updateAnimation(World world1, Entity entity2, int i3, bool z4)
		{
			if (this.animationsToGo > 0)
			{
				--this.animationsToGo;
			}

			net.minecraft.src.Item.itemsList[this.itemID].onUpdate(this, world1, entity2, i3, z4);
		}

		public void onCrafting(World world1, EntityPlayer entityPlayer2, int i3)
		{
			entityPlayer2.addStat(StatList.objectCraftStats[this.itemID], i3);
			net.minecraft.src.Item.itemsList[this.itemID].onCreated(this, world1, entityPlayer2);
		}

		public bool isStackEqual(ItemStack itemStack1)
		{
			return this.itemID == itemStack1.itemID && this.stackSize == itemStack1.stackSize && this.itemDamage == itemStack1.itemDamage;
		}

		public int MaxItemUseDuration
		{
			get
			{
				return this.Item.getMaxItemUseDuration(this);
			}
		}

		public EnumAction ItemUseAction
		{
			get
			{
				return this.Item.getItemUseAction(this);
			}
		}

		public void onPlayerStoppedUsing(World world1, EntityPlayer entityPlayer2, int i3)
		{
			this.Item.onPlayerStoppedUsing(this, world1, entityPlayer2, i3);
		}

		public bool hasTagCompound()
		{
			return this.stackTagCompound != null;
		}

		public NBTTagCompound TagCompound
		{
			get
			{
				return this.stackTagCompound;
			}
			set
			{
				this.stackTagCompound = value;
			}
		}

		public NBTTagList EnchantmentTagList
		{
			get
			{
				return this.stackTagCompound == null ? null : (NBTTagList)this.stackTagCompound.getTag("ench");
			}
		}


		public System.Collections.IList ItemNameandInformation
		{
			get
			{
				ArrayList arrayList1 = new ArrayList();
				Item item2 = net.minecraft.src.Item.itemsList[this.itemID];
				arrayList1.Add(item2.getItemDisplayName(this));
				item2.addInformation(this, arrayList1);
				if (this.hasTagCompound())
				{
					NBTTagList nBTTagList3 = this.EnchantmentTagList;
					if (nBTTagList3 != null)
					{
						for (int i4 = 0; i4 < nBTTagList3.tagCount(); ++i4)
						{
							short s5 = ((NBTTagCompound)nBTTagList3.tagAt(i4)).getShort("id");
							short s6 = ((NBTTagCompound)nBTTagList3.tagAt(i4)).getShort("lvl");
							if (Enchantment.enchantmentsList[s5] != null)
							{
								arrayList1.Add(Enchantment.enchantmentsList[s5].getTranslatedName(s6));
							}
						}
					}
				}
    
				return arrayList1;
			}
		}

		public bool hasEffect()
		{
			return this.Item.hasEffect(this);
		}

		public EnumRarity Rarity
		{
			get
			{
				return this.Item.getRarity(this);
			}
		}

		public bool ItemEnchantable
		{
			get
			{
				return !this.Item.isItemTool(this) ? false :!this.ItemEnchanted;
			}
		}

		public void addEnchantment(Enchantment enchantment1, int i2)
		{
			if (this.stackTagCompound == null)
			{
				this.TagCompound = new NBTTagCompound();
			}

			if (!this.stackTagCompound.hasKey("ench"))
			{
				this.stackTagCompound.setTag("ench", new NBTTagList("ench"));
			}

			NBTTagList nBTTagList3 = (NBTTagList)this.stackTagCompound.getTag("ench");
			NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
			nBTTagCompound4.setShort("id", (short)enchantment1.effectId);
			nBTTagCompound4.setShort("lvl", (short)((sbyte)i2));
			nBTTagList3.appendTag(nBTTagCompound4);
		}

		public bool ItemEnchanted
		{
			get
			{
				return this.stackTagCompound != null && this.stackTagCompound.hasKey("ench");
			}
		}
	}

}