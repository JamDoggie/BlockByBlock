using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemSword : Item
	{
		private int weaponDamage;
		private readonly EnumToolMaterial toolMaterial;

		public ItemSword(int i1, EnumToolMaterial enumToolMaterial2) : base(i1)
		{
			this.toolMaterial = enumToolMaterial2;
			this.maxStackSize = 1;
			setMaxDamage(enumToolMaterial2.MaxUses);
			this.weaponDamage = 4 + enumToolMaterial2.DamageVsEntity;
		}

		public override float getStrVsBlock(ItemStack itemStack1, Block block2)
		{
			return block2.blockID == Block.web.blockID ? 15.0F : 1.5F;
		}

		public override bool hitEntity(ItemStack itemStack1, EntityLiving entityLiving2, EntityLiving entityLiving3)
		{
			itemStack1.damageItem(1, entityLiving3);
			return true;
		}

		public override bool onBlockDestroyed(ItemStack itemStack1, int i2, int i3, int i4, int i5, EntityLiving entityLiving6)
		{
			itemStack1.damageItem(2, entityLiving6);
			return true;
		}

		public override int getDamageVsEntity(Entity entity1)
		{
			return this.weaponDamage;
		}

		public override bool Full3D
		{
			get
			{
				return true;
			}
		}

		public override EnumAction getItemUseAction(ItemStack itemStack1)
		{
			return EnumAction.block;
		}

		public override int getMaxItemUseDuration(ItemStack itemStack1)
		{
			return 72000;
		}

		public override ItemStack onItemRightClick(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			entityPlayer3.setItemInUse(itemStack1, this.getMaxItemUseDuration(itemStack1));
			return itemStack1;
		}

		public override bool canHarvestBlock(Block block1)
		{
			return block1.blockID == Block.web.blockID;
		}

		public override int ItemEnchantability
		{
			get
			{
				return toolMaterial.Enchantability;
			}
		}
	}

}