using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class ItemRecord : Item
	{
		public readonly string recordName;

		protected internal ItemRecord(int i1, string string2) : base(i1)
		{
			this.recordName = string2;
			this.maxStackSize = 1;
		}

		public override bool onItemUse(ItemStack itemStack1, EntityPlayer entityPlayer2, World world3, int i4, int i5, int i6, int i7)
		{
			if (world3.getBlockId(i4, i5, i6) == Block.jukebox.blockID && world3.getBlockMetadata(i4, i5, i6) == 0)
			{
				if (world3.isRemote)
				{
					return true;
				}
				else
				{
					((BlockJukeBox)Block.jukebox).insertRecord(world3, i4, i5, i6, this.shiftedIndex);
					world3.playAuxSFXAtEntity(null, 1005, i4, i5, i6, this.shiftedIndex);
					--itemStack1.stackSize;
					return true;
				}
			}
			else
			{
				return false;
			}
		}

		public override void addInformation(ItemStack itemStack1, System.Collections.IList list2)
		{
			list2.Add("C418 - " + this.recordName);
		}

		public override EnumRarity getRarity(ItemStack itemStack1)
		{
			return EnumRarity.rare;
		}
	}

}