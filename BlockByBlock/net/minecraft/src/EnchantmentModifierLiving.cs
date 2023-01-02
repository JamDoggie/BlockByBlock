using net.minecraft.client.entity;

namespace net.minecraft.src
{
    internal sealed class EnchantmentModifierLiving : IEnchantmentModifier
	{
		public int livingModifier;
		public EntityLiving entityLiving;

		private EnchantmentModifierLiving()
		{
		}

		public void calculateModifier(Enchantment enchantment1, int i2)
		{
			this.livingModifier += enchantment1.calcModifierLiving(i2, this.entityLiving);
		}

		internal EnchantmentModifierLiving(Empty3 empty31) : this()
		{
		}
	}

}