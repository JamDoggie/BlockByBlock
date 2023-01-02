using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemTool : Item
	{
		private Block[] blocksEffectiveAgainst;
		protected internal float efficiencyOnProperMaterial = 4.0F;
		private int damageVsEntity;
		protected internal EnumToolMaterial toolMaterial;

		protected internal ItemTool(int i1, int i2, EnumToolMaterial enumToolMaterial3, Block[] block4) : base(i1)
		{
			toolMaterial = enumToolMaterial3;
			blocksEffectiveAgainst = block4;
			maxStackSize = 1;
			setMaxDamage(enumToolMaterial3.MaxUses);
			efficiencyOnProperMaterial = enumToolMaterial3.EfficiencyOnProperMaterial;
			damageVsEntity = i2 + enumToolMaterial3.DamageVsEntity;
		}

		public override float getStrVsBlock(ItemStack itemStack1, Block block2)
		{
			for (int i3 = 0; i3 < this.blocksEffectiveAgainst.Length; ++i3)
			{
				if (this.blocksEffectiveAgainst[i3] == block2)
				{
					return this.efficiencyOnProperMaterial;
				}
			}

			return 1.0F;
		}

		public override bool hitEntity(ItemStack itemStack1, EntityLiving entityLiving2, EntityLiving entityLiving3)
		{
			itemStack1.damageItem(2, entityLiving3);
			return true;
		}

		public override bool onBlockDestroyed(ItemStack itemStack1, int i2, int i3, int i4, int i5, EntityLiving entityLiving6)
		{
			itemStack1.damageItem(1, entityLiving6);
			return true;
		}

		public override int getDamageVsEntity(Entity entity1)
		{
			return this.damageVsEntity;
		}

		public override bool Full3D
		{
			get
			{
				return true;
			}
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