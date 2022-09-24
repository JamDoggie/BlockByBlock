namespace net.minecraft.src
{
	public class EnchantmentWaterWorker : Enchantment
	{
		public EnchantmentWaterWorker(int i1, int i2) : base(i1, i2, EnumEnchantmentType.armor_head)
		{
			Name = "waterWorker";
		}

		public override int getMinEnchantability(int i1)
		{
			return 1;
		}

		public override int getMaxEnchantability(int i1)
		{
			return this.getMinEnchantability(i1) + 40;
		}

		public override int MaxLevel
		{
			get
			{
				return 1;
			}
		}
	}

}