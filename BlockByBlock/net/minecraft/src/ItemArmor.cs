namespace net.minecraft.src
{
	public class ItemArmor : Item
	{
		private static readonly int[] maxDamageArray = new int[]{11, 16, 15, 13};
		public readonly int armorType;
		public readonly int damageReduceAmount;
		public readonly int renderIndex;
		private readonly EnumArmorMaterial material;

		public ItemArmor(int i1, EnumArmorMaterial enumArmorMaterial2, int i3, int i4) : base(i1)
		{
			this.material = enumArmorMaterial2;
			this.armorType = i4;
			this.renderIndex = i3;
			this.damageReduceAmount = enumArmorMaterial2.getDamageReductionAmount(i4);
			this.setMaxDamage(enumArmorMaterial2.getDurability(i4));
			this.maxStackSize = 1;
		}

		public override int ItemEnchantability
		{
			get
			{
				return this.material.Enchantability;
			}
		}

		internal static int[] MaxDamageArray
		{
			get
			{
				return maxDamageArray;
			}
		}
	}

}