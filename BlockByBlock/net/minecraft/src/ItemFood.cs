using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemFood : Item
	{
		public readonly int field_35430_a;
		private readonly int healAmount;
		private readonly float saturationModifier;
		private readonly bool isWolfsFavoriteMeat;
		private bool alwaysEdible;
		private int potionId;
		private int potionDuration;
		private int potionAmplifier;
		private float potionEffectProbability;

		public ItemFood(int i1, int i2, float f3, bool z4) : base(i1)
		{
			this.field_35430_a = 32;
			this.healAmount = i2;
			this.isWolfsFavoriteMeat = z4;
			this.saturationModifier = f3;
		}

		public ItemFood(int i1, int i2, bool z3) : this(i1, i2, 0.6F, z3)
		{
		}

		public override ItemStack onFoodEaten(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			--itemStack1.stackSize;
			entityPlayer3.FoodStats.addStats(this);
			world2.playSoundAtEntity(entityPlayer3, "random.burp", 0.5F, world2.rand.NextSingle() * 0.1F + 0.9F);
			if (!world2.isRemote && this.potionId > 0 && world2.rand.NextSingle() < this.potionEffectProbability)
			{
				entityPlayer3.addPotionEffect(new PotionEffect(this.potionId, this.potionDuration * 20, this.potionAmplifier));
			}

			return itemStack1;
		}

		public override int getMaxItemUseDuration(ItemStack itemStack1)
		{
			return 32;
		}

		public override EnumAction getItemUseAction(ItemStack itemStack1)
		{
			return EnumAction.eat;
		}

		public override ItemStack onItemRightClick(ItemStack itemStack1, World world2, EntityPlayer entityPlayer3)
		{
			if (entityPlayer3.canEat(this.alwaysEdible))
			{
				entityPlayer3.setItemInUse(itemStack1, this.getMaxItemUseDuration(itemStack1));
			}

			return itemStack1;
		}

		public virtual int HealAmount
		{
			get
			{
				return this.healAmount;
			}
		}

		public virtual float SaturationModifier
		{
			get
			{
				return this.saturationModifier;
			}
		}

		public virtual bool WolfsFavoriteMeat
		{
			get
			{
				return this.isWolfsFavoriteMeat;
			}
		}

		public virtual ItemFood setPotionEffect(int i1, int i2, int i3, float f4)
		{
			this.potionId = i1;
			this.potionDuration = i2;
			this.potionAmplifier = i3;
			this.potionEffectProbability = f4;
			return this;
		}

		public virtual ItemFood setAlwaysEdible()
		{
			this.alwaysEdible = true;
			return this;
		}

		public override Item setItemName(string string1)
		{
			return base.setItemName(string1);
		}
	}

}