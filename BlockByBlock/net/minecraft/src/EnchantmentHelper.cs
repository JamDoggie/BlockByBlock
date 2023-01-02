using System;
using System.Collections;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class EnchantmentHelper
	{
		private static readonly RandomExtended enchantmentRand = new RandomExtended();
		private static readonly EnchantmentModifierDamage enchantmentModifierDamage = new EnchantmentModifierDamage((Empty3)null);
		private static readonly EnchantmentModifierLiving enchantmentModifierLiving = new EnchantmentModifierLiving((Empty3)null);

		public static int getEnchantmentLevel(int i0, ItemStack itemStack1)
		{
			if (itemStack1 == null)
			{
				return 0;
			}
			else
			{
				NBTTagList nBTTagList2 = itemStack1.EnchantmentTagList;
				if (nBTTagList2 == null)
				{
					return 0;
				}
				else
				{
					for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
					{
						short s4 = ((NBTTagCompound)nBTTagList2.tagAt(i3)).getShort("id");
						short s5 = ((NBTTagCompound)nBTTagList2.tagAt(i3)).getShort("lvl");
						if (s4 == i0)
						{
							return s5;
						}
					}

					return 0;
				}
			}
		}

		private static int getMaxEnchantmentLevel(int i0, ItemStack[] itemStack1)
		{
			int i2 = 0;
			ItemStack[] itemStack3 = itemStack1;
			int i4 = itemStack1.Length;

			for (int i5 = 0; i5 < i4; ++i5)
			{
				ItemStack itemStack6 = itemStack3[i5];
				int i7 = getEnchantmentLevel(i0, itemStack6);
				if (i7 > i2)
				{
					i2 = i7;
				}
			}

			return i2;
		}

		private static void applyEnchantmentModifier(IEnchantmentModifier iEnchantmentModifier0, ItemStack itemStack1)
		{
			if (itemStack1 != null)
			{
				NBTTagList nBTTagList2 = itemStack1.EnchantmentTagList;
				if (nBTTagList2 != null)
				{
					for (int i3 = 0; i3 < nBTTagList2.tagCount(); ++i3)
					{
						short s4 = ((NBTTagCompound)nBTTagList2.tagAt(i3)).getShort("id");
						short s5 = ((NBTTagCompound)nBTTagList2.tagAt(i3)).getShort("lvl");
						if (Enchantment.enchantmentsList[s4] != null)
						{
							iEnchantmentModifier0.calculateModifier(Enchantment.enchantmentsList[s4], s5);
						}
					}

				}
			}
		}

		private static void applyEnchantmentModifierArray(IEnchantmentModifier iEnchantmentModifier0, ItemStack[] itemStack1)
		{
			ItemStack[] itemStack2 = itemStack1;
			int i3 = itemStack1.Length;

			for (int i4 = 0; i4 < i3; ++i4)
			{
				ItemStack itemStack5 = itemStack2[i4];
				applyEnchantmentModifier(iEnchantmentModifier0, itemStack5);
			}

		}

		public static int getEnchantmentModifierDamage(InventoryPlayer inventoryPlayer0, DamageSource damageSource1)
		{
			enchantmentModifierDamage.damageModifier = 0;
			enchantmentModifierDamage.damageSource = damageSource1;
			applyEnchantmentModifierArray(enchantmentModifierDamage, inventoryPlayer0.armorInventory);
			if (enchantmentModifierDamage.damageModifier > 25)
			{
				enchantmentModifierDamage.damageModifier = 25;
			}

			return (enchantmentModifierDamage.damageModifier + 1 >> 1) + enchantmentRand.Next((enchantmentModifierDamage.damageModifier >> 1) + 1);
		}

		public static int getEnchantmentModifierLiving(InventoryPlayer inventoryPlayer0, EntityLiving entityLiving1)
		{
			enchantmentModifierLiving.livingModifier = 0;
			enchantmentModifierLiving.entityLiving = entityLiving1;
			applyEnchantmentModifier(enchantmentModifierLiving, inventoryPlayer0.CurrentItem);
			return enchantmentModifierLiving.livingModifier > 0 ? 1 + enchantmentRand.Next(enchantmentModifierLiving.livingModifier) : 0;
		}

		public static int getKnockbackModifier(InventoryPlayer inventoryPlayer0, EntityLiving entityLiving1)
		{
			return getEnchantmentLevel(Enchantment.knockback.effectId, inventoryPlayer0.CurrentItem);
		}

		public static int getFireAspectModifier(InventoryPlayer inventoryPlayer0, EntityLiving entityLiving1)
		{
			return getEnchantmentLevel(Enchantment.fireAspect.effectId, inventoryPlayer0.CurrentItem);
		}

		public static int getRespiration(InventoryPlayer inventoryPlayer0)
		{
			return getMaxEnchantmentLevel(Enchantment.respiration.effectId, inventoryPlayer0.armorInventory);
		}

		public static int getEfficiencyModifier(InventoryPlayer inventoryPlayer0)
		{
			return getEnchantmentLevel(Enchantment.efficiency.effectId, inventoryPlayer0.CurrentItem);
		}

		public static int getUnbreakingModifier(InventoryPlayer inventoryPlayer0)
		{
			return getEnchantmentLevel(Enchantment.unbreaking.effectId, inventoryPlayer0.CurrentItem);
		}

		public static bool getSilkTouchModifier(InventoryPlayer inventoryPlayer0)
		{
			return getEnchantmentLevel(Enchantment.silkTouch.effectId, inventoryPlayer0.CurrentItem) > 0;
		}

		public static int getFortuneModifier(InventoryPlayer inventoryPlayer0)
		{
			return getEnchantmentLevel(Enchantment.fortune.effectId, inventoryPlayer0.CurrentItem);
		}

		public static int getLootingModifier(InventoryPlayer inventoryPlayer0)
		{
			return getEnchantmentLevel(Enchantment.looting.effectId, inventoryPlayer0.CurrentItem);
		}

		public static bool getAquaAffinityModifier(InventoryPlayer inventoryPlayer0)
		{
			return getMaxEnchantmentLevel(Enchantment.aquaAffinity.effectId, inventoryPlayer0.armorInventory) > 0;
		}

		public static int calcItemStackEnchantability(RandomExtended random0, int i1, int i2, ItemStack itemStack3)
		{
			Item item4 = itemStack3.Item;
			int i5 = item4.ItemEnchantability;
			if (i5 <= 0)
			{
				return 0;
			}
			else
			{
				if (i2 > 30)
				{
					i2 = 30;
				}

				i2 = 1 + (i2 >> 1) + random0.Next(i2 + 1);
				int i6 = random0.Next(5) + i2;
				return i1 == 0 ? (i6 >> 1) + 1 : (i1 == 1 ? i6 * 2 / 3 + 1 : i6);
			}
		}

		public static void func_48441_a(RandomExtended random0, ItemStack itemStack1, int i2)
		{
			System.Collections.IList list3 = buildEnchantmentList(random0, itemStack1, i2);
			if (list3 != null)
			{
				System.Collections.IEnumerator iterator4 = list3.GetEnumerator();

				while (iterator4.MoveNext())
				{
					EnchantmentData enchantmentData5 = (EnchantmentData)iterator4.Current;
					itemStack1.addEnchantment(enchantmentData5.enchantmentobj, enchantmentData5.enchantmentLevel);
				}
			}

		}
        
		public static System.Collections.IList buildEnchantmentList(RandomExtended random0, ItemStack itemStack1, int i2)
		{
			Item item3 = itemStack1.Item;
			int i4 = item3.ItemEnchantability;
			if (i4 <= 0)
			{
				return null;
			}
			else
			{
				i4 = 1 + random0.Next((i4 >> 1) + 1) + random0.Next((i4 >> 1) + 1);
				int i5 = i4 + i2;
				float f6 = (random0.NextSingle() + random0.NextSingle() - 1.0F) * 0.25F;
				int i7 = (int)((float)i5 * (1.0F + f6) + 0.5F);
				ArrayList arrayList8 = null;
				System.Collections.IDictionary map9 = mapEnchantmentData(i7, itemStack1);
				if (map9 != null && map9.Count > 0)
				{
					EnchantmentData enchantmentData10 = (EnchantmentData)WeightedRandom.getRandomItem(random0, map9.Values);
					if (enchantmentData10 != null)
					{
						arrayList8 = new ArrayList();
						arrayList8.Add(enchantmentData10);

						for (int i11 = i7 >> 1; random0.Next(50) <= i11; i11 >>= 1)
						{
							System.Collections.IEnumerator iterator12 = map9.Keys.GetEnumerator();

							List<int> toRemove = new();

							while (iterator12.MoveNext())
							{
								int? integer13 = (int?)iterator12.Current;
								bool z14 = true;
								System.Collections.IEnumerator iterator15 = arrayList8.GetEnumerator();

								while (iterator15.MoveNext())
								{
									EnchantmentData enchantmentData16 = (EnchantmentData)iterator15.Current;
									if (!enchantmentData16.enchantmentobj.canApplyTogether(Enchantment.enchantmentsList[integer13.Value]))
									{
										z14 = false;
										break;
									}
								}

								if (!z14)
								{
									toRemove.Add(integer13.Value);
								}
							}

							foreach(int i in toRemove)
                            {
								map9.Remove(i);
                            }

							if (map9.Count > 0)
							{
								EnchantmentData enchantmentData17 = (EnchantmentData)WeightedRandom.getRandomItem(random0, map9.Values);
								arrayList8.Add(enchantmentData17);
							}
						}
					}
				}

				return arrayList8;
			}
		}

		public static System.Collections.IDictionary mapEnchantmentData(int i0, ItemStack itemStack1)
		{
			Item item2 = itemStack1.Item;
			Hashtable hashMap3 = null;
			Enchantment[] enchantment4 = Enchantment.enchantmentsList;
			int i5 = enchantment4.Length;

			for (int i6 = 0; i6 < i5; ++i6)
			{
				Enchantment enchantment7 = enchantment4[i6];
				if (enchantment7 != null && enchantment7.type.canEnchantItem(item2))
				{
					for (int i8 = enchantment7.MinLevel; i8 <= enchantment7.MaxLevel; ++i8)
					{
						if (i0 >= enchantment7.getMinEnchantability(i8) && i0 <= enchantment7.getMaxEnchantability(i8))
						{
							if (hashMap3 == null)
							{
								hashMap3 = new Hashtable();
							}

							hashMap3[enchantment7.effectId] = new EnchantmentData(enchantment7, i8);
						}
					}
				}
			}

			return hashMap3;
		}
	}

}