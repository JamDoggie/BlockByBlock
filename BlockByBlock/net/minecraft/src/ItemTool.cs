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
			this.toolMaterial = enumToolMaterial3;
			this.blocksEffectiveAgainst = block4;
			this.maxStackSize = 1;
			this.MaxDamage = enumToolMaterial3.getMaxUses();
			this.efficiencyOnProperMaterial = enumToolMaterial3.getEfficiencyOnProperMaterial();
			this.damageVsEntity = i2 + enumToolMaterial3.getDamageVsEntity();
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
				return this.toolMaterial.getEnchantability();
			}
		}
	}

}