using net.minecraft.client.entity;

namespace net.minecraft.src
{
    public class ItemHoe : Item
	{
		public ItemHoe(int i1, EnumToolMaterial enumToolMaterial2) : base(i1)
		{
			maxStackSize = 1;
			setMaxDamage(enumToolMaterial2.MaxUses);
		}

		public override bool onItemUse(ItemStack itemStack1, EntityPlayer entityPlayer2, World world3, int i4, int i5, int i6, int i7)
		{
			if (!entityPlayer2.canPlayerEdit(i4, i5, i6))
			{
				return false;
			}
			else
			{
				int i8 = world3.getBlockId(i4, i5, i6);
				int i9 = world3.getBlockId(i4, i5 + 1, i6);
				if ((i7 == 0 || i9 != 0 || i8 != Block.grass.blockID) && i8 != Block.dirt.blockID)
				{
					return false;
				}
				else
				{
					Block block10 = Block.tilledField;
					world3.playSoundEffect((double)((float)i4 + 0.5F), (double)((float)i5 + 0.5F), (double)((float)i6 + 0.5F), block10.stepSound.StepSoundName, (block10.stepSound.Volume + 1.0F) / 2.0F, block10.stepSound.Pitch * 0.8F);
					if (world3.isRemote)
					{
						return true;
					}
					else
					{
						world3.setBlockWithNotify(i4, i5, i6, block10.blockID);
						itemStack1.damageItem(1, entityPlayer2);
						return true;
					}
				}
			}
		}

		public override bool Full3D
		{
			get
			{
				return true;
			}
		}
	}

}