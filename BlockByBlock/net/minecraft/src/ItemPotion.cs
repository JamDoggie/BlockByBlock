using System.Collections;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class ItemPotion : Item
	{
		private Hashtable effectCache = new Hashtable();

		public ItemPotion(int i1) : base(i1)
		{
			setMaxStackSize(1);
			setHasSubtypes(true);
			setMaxDamage(0);
		}

		public virtual System.Collections.IList getEffects(ItemStack itemStack1)
		{
			return this.getEffects(itemStack1.ItemDamage);
		}

		public virtual List<PotionEffect> getEffects(int i1)
		{
			List<PotionEffect>? list2 = effectCache[i1] as List<PotionEffect>;
			if (list2 == null)
			{
				list2 = PotionHelper.getPotionEffects(i1, false);
				effectCache[i1] = list2;
			}

			return list2;
		}

		public override ItemStack onFoodEaten(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			--itemStack1.stackSize;
			if (!world2.isRemote)
			{
				System.Collections.IList list4 = this.getEffects(itemStack1);
				if (list4 != null)
				{
					System.Collections.IEnumerator iterator5 = list4.GetEnumerator();

					while (iterator5.MoveNext())
					{
						PotionEffect potionEffect6 = (PotionEffect)iterator5.Current;
						entityPlayer3.addPotionEffect(new PotionEffect(potionEffect6));
					}
				}
			}

			if (itemStack1.stackSize <= 0)
			{
				return new ItemStack(Item.glassBottle);
			}
			else
			{
				entityPlayer3.inventory.addItemStackToInventory(new ItemStack(Item.glassBottle));
				return itemStack1;
			}
		}

		public override int getMaxItemUseDuration(ItemStack itemStack1)
		{
			return 32;
		}

		public override EnumAction getItemUseAction(ItemStack itemStack1)
		{
			return EnumAction.drink;
		}

		public override ItemStack onItemRightClick(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			if (isSplash(itemStack1.ItemDamage))
			{
				--itemStack1.stackSize;
				world2.playSoundAtEntity(entityPlayer3, "random.bow", 0.5F, 0.4F / (itemRand.NextSingle() * 0.4F + 0.8F));
				if (!world2.isRemote)
				{
					world2.spawnEntityInWorld(new EntityPotion(world2, entityPlayer3, itemStack1.ItemDamage));
				}

				return itemStack1;
			}
			else
			{
				entityPlayer3.setItemInUse(itemStack1, this.getMaxItemUseDuration(itemStack1));
				return itemStack1;
			}
		}

		public override bool onItemUse(ItemStack itemStack1, EntityPlayer entityPlayer2, World world3, int i4, int i5, int i6, int i7)
		{
			return false;
		}

		public override int getIconFromDamage(int i1)
		{
			return isSplash(i1) ? 154 : 140;
		}

		public override int func_46057_a(int i1, int i2)
		{
			return i2 == 0 ? 141 : base.func_46057_a(i1, i2);
		}

		public static bool isSplash(int i0)
		{
			return (i0 & 16384) != 0;
		}

		public override int getColorFromDamage(int i1, int i2)
		{
			return i2 > 0 ? 0xFFFFFF : PotionHelper.func_40358_a(i1, false);
		}

		public override bool func_46058_c()
		{
			return true;
		}

		public virtual bool isEffectInstant(int i1)
		{
			System.Collections.IList list2 = this.getEffects(i1);
			if (list2 != null && list2.Count > 0)
			{
				foreach (PotionEffect potionEffect in list2)
                {
                    if (Potion.potionTypes[potionEffect.PotionID].Instant)
                    {
						return true;
                    }
                }
                
				return false;
			}
			else
			{
				return false;
			}
		}

		public override string getItemDisplayName(ItemStack itemStack1)
		{
			if (itemStack1.ItemDamage == 0)
			{
				return StatCollector.translateToLocal("item.emptyPotion.name").Trim();
			}
			else
			{
				string string2 = "";
				if (isSplash(itemStack1.ItemDamage))
				{
					string2 = StatCollector.translateToLocal("potion.prefix.grenade").Trim() + " ";
				}

				System.Collections.IList list3 = Item.potion.getEffects(itemStack1);
				string string4;
				if (list3 != null && list3.Count > 0)
				{
					string4 = ((PotionEffect)list3[0]).EffectName;
					string4 = string4 + ".postfix";
					return string2 + StatCollector.translateToLocal(string4).Trim();
				}
				else
				{
					string4 = PotionHelper.func_40359_b(itemStack1.ItemDamage);
					return StatCollector.translateToLocal(string4).Trim() + " " + base.getItemDisplayName(itemStack1);
				}
			}
		}

		public override void addInformation(ItemStack itemStack1, System.Collections.IList list2)
		{
			if (itemStack1.ItemDamage != 0)
			{
				System.Collections.IList list3 = Item.potion.getEffects(itemStack1);
				if (list3 != null && list3.Count > 0)
				{
					System.Collections.IEnumerator iterator7 = list3.GetEnumerator();

					while (iterator7.MoveNext())
					{
						PotionEffect potionEffect5 = (PotionEffect)iterator7.Current;
						string string6 = StatCollector.translateToLocal(potionEffect5.EffectName).Trim();
						if (potionEffect5.Amplifier > 0)
						{
							string6 = string6 + " " + StatCollector.translateToLocal("potion.potency." + potionEffect5.Amplifier).Trim();
						}

						if (potionEffect5.Duration > 20)
						{
							string6 = string6 + " (" + Potion.getDurationString(potionEffect5) + ")";
						}

						if (Potion.potionTypes[potionEffect5.PotionID].BadEffect)
						{
							list2.Add("\u00a7c" + string6);
						}
						else
						{
							list2.Add("\u00a77" + string6);
						}
					}
				}
				else
				{
					string string4 = StatCollector.translateToLocal("potion.empty").Trim();
					list2.Add("\u00a77" + string4);
				}

			}
		}

		public override bool hasEffect(ItemStack itemStack1)
		{
			System.Collections.IList list2 = this.getEffects(itemStack1);
			return list2 != null && list2.Count > 0;
		}
	}

}