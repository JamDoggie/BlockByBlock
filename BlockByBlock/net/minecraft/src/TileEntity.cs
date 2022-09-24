using System;
using System.Collections;

namespace net.minecraft.src
{

	public class TileEntity
	{
		private static System.Collections.IDictionary nameToClassMap = new Hashtable();
		private static System.Collections.IDictionary classToNameMap = new Hashtable();
		public World worldObj;
		public int xCoord;
		public int yCoord;
		public int zCoord;
		protected internal bool tileEntityInvalid;
		public int blockMetadata = -1;
		public Block blockType;

		private static void addMapping(Type class0, string string1)
		{
			if (classToNameMap.Contains(string1))
			{
				throw new System.ArgumentException("Duplicate id: " + string1);
			}
			else
			{
				nameToClassMap[string1] = class0;
				classToNameMap[class0] = string1;
			}
		}

		public virtual void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			this.xCoord = nBTTagCompound1.getInteger("x");
			this.yCoord = nBTTagCompound1.getInteger("y");
			this.zCoord = nBTTagCompound1.getInteger("z");
		}

		public virtual void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			string string2 = (string)classToNameMap[this.GetType()];
			if (string.ReferenceEquals(string2, null))
			{
				throw new Exception(this.GetType() + " is missing a mapping! This is a bug!");
			}
			else
			{
				nBTTagCompound1.setString("id", string2);
				nBTTagCompound1.setInteger("x", this.xCoord);
				nBTTagCompound1.setInteger("y", this.yCoord);
				nBTTagCompound1.setInteger("z", this.zCoord);
			}
		}

		public virtual void updateEntity()
		{
		}

		public static TileEntity createAndLoadEntity(NBTTagCompound nBTTagCompound0)
		{
			TileEntity tileEntity1 = null;

			try
			{
				Type class2 = (Type)nameToClassMap[nBTTagCompound0.getString("id")];
				if (class2 != null)
				{
					tileEntity1 = (TileEntity)System.Activator.CreateInstance(class2);
				}
			}
			catch (Exception exception3)
			{
				Console.WriteLine(exception3.ToString());
				Console.Write(exception3.StackTrace);
			}

			if (tileEntity1 != null)
			{
				tileEntity1.readFromNBT(nBTTagCompound0);
			}
			else
			{
				Console.WriteLine("Skipping TileEntity with id " + nBTTagCompound0.getString("id"));
			}

			return tileEntity1;
		}

		public virtual int BlockMetadata
		{
			get
			{
				if (this.blockMetadata == -1)
				{
					this.blockMetadata = this.worldObj.getBlockMetadata(this.xCoord, this.yCoord, this.zCoord);
				}
    
				return this.blockMetadata;
			}
		}

		public virtual void onInventoryChanged()
		{
			if (this.worldObj != null)
			{
				this.blockMetadata = this.worldObj.getBlockMetadata(this.xCoord, this.yCoord, this.zCoord);
				this.worldObj.updateTileEntityChunkAndDoNothing(this.xCoord, this.yCoord, this.zCoord, this);
			}

		}

		public virtual double getDistanceFrom(double d1, double d3, double d5)
		{
			double d7 = (double)this.xCoord + 0.5D - d1;
			double d9 = (double)this.yCoord + 0.5D - d3;
			double d11 = (double)this.zCoord + 0.5D - d5;
			return d7 * d7 + d9 * d9 + d11 * d11;
		}

		public virtual Block BlockType
		{
			get
			{
				if (this.blockType == null)
				{
					this.blockType = Block.blocksList[this.worldObj.getBlockId(this.xCoord, this.yCoord, this.zCoord)];
				}
    
				return this.blockType;
			}
		}

		public virtual bool Invalid
		{
			get
			{
				return this.tileEntityInvalid;
			}
		}

		public virtual void invalidate()
		{
			this.tileEntityInvalid = true;
		}

		public virtual void validate()
		{
			this.tileEntityInvalid = false;
		}

		public virtual void onTileEntityPowered(int i1, int i2)
		{
		}

		public virtual void updateContainingBlockInfo()
		{
			this.blockType = null;
			this.blockMetadata = -1;
		}

		static TileEntity()
		{
			addMapping(typeof(TileEntityFurnace), "Furnace");
			addMapping(typeof(TileEntityChest), "Chest");
			addMapping(typeof(TileEntityRecordPlayer), "RecordPlayer");
			addMapping(typeof(TileEntityDispenser), "Trap");
			addMapping(typeof(TileEntitySign), "Sign");
			addMapping(typeof(TileEntityMobSpawner), "MobSpawner");
			addMapping(typeof(TileEntityNote), "Music");
			addMapping(typeof(TileEntityPiston), "Piston");
			addMapping(typeof(TileEntityBrewingStand), "Cauldron");
			addMapping(typeof(TileEntityEnchantmentTable), "EnchantTable");
			addMapping(typeof(TileEntityEndPortal), "Airportal");
		}
	}

}