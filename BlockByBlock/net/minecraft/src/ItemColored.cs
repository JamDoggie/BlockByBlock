namespace net.minecraft.src
{
	public class ItemColored : ItemBlock
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			blockRef = Block.blocksList[this.BlockID];
		}

		private Block blockRef;
		private string[] blockNames;

		public ItemColored(int i1, bool z2) : base(i1)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			if (z2)
			{
				setMaxDamage(0);
				setHasSubtypes(true);
			}

		}

		public override int getColorFromDamage(int i1, int i2)
		{
			return this.blockRef.getRenderColor(i1);
		}

		public override int getIconFromDamage(int i1)
		{
			return this.blockRef.getBlockTextureFromSideAndMetadata(0, i1);
		}

		public override int getMetadata(int i1)
		{
			return i1;
		}

		public virtual ItemColored setBlockNames(string[] string1)
		{
			this.blockNames = string1;
			return this;
		}

		public override string getItemNameIS(ItemStack itemStack1)
		{
			if (this.blockNames == null)
			{
				return base.getItemNameIS(itemStack1);
			}
			else
			{
				int i2 = itemStack1.ItemDamage;
				return i2 >= 0 && i2 < this.blockNames.Length ? base.getItemNameIS(itemStack1) + "." + this.blockNames[i2] : base.getItemNameIS(itemStack1);
			}
		}
	}

}