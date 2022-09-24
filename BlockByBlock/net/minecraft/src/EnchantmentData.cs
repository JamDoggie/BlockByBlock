namespace net.minecraft.src
{
	public class EnchantmentData : WeightedRandomChoice
	{
		public readonly Enchantment enchantmentobj;
		public readonly int enchantmentLevel;

		public EnchantmentData(Enchantment enchantment1, int i2) : base(enchantment1.Weight)
		{
			this.enchantmentobj = enchantment1;
			this.enchantmentLevel = i2;
		}
	}

}