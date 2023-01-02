using System;
using BlockByBlock.java_extensions;
using net.minecraft.client.entity;

namespace net.minecraft.src
{

    public class BlockMobSpawner : BlockContainer
	{
		protected internal BlockMobSpawner(int i1, int i2) : base(i1, i2, Material.rock)
		{
		}

		public override TileEntity BlockEntity
		{
			get
			{
				return new TileEntityMobSpawner();
			}
		}

        // Block by Block: This is not in the original code. I just did this for fun.
        public override bool blockActivated(World world1, int i2, int i3, int i4, EntityPlayer player)
		{
			// Singleplayer only (we can't control what goes on in servers here)
			if (player.inventory.CurrentItem != null && player.inventory.CurrentItem.Item == Item.monsterPlacer && !player.worldObj.isRemote)
			{
				// Change the mob in this spawner based on the item in the player's hand.
				((TileEntityMobSpawner)world1.getBlockTileEntity(i2, i3, i4)).MobID = EntityList.getStringFromID(player.inventory.CurrentItem.ItemDamage);

                return true;
			}

			return base.blockActivated(world1, i2, i3, i4, player);
		}

		public override int idDropped(int i1, RandomExtended random2, int i3)
		{
			return 0;
		}

		public override int quantityDropped(RandomExtended random1)
		{
			return 0;
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}
	}

}