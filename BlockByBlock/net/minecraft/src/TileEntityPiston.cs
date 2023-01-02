using BlockByBlock.helpers;
using net.minecraft.client.entity;
using System.Collections;

namespace net.minecraft.src
{

    public class TileEntityPiston : TileEntity
	{
		private int storedBlockID;
		private int storedMetadata;
		private int storedOrientation;
		private bool extending;
		private bool shouldHeadBeRendered;
		private float progress;
		private float lastProgress;
		private static System.Collections.IList pushedObjects = new ArrayList();

		public TileEntityPiston()
		{
		}

		public TileEntityPiston(int i1, int i2, int i3, bool z4, bool z5)
		{
			this.storedBlockID = i1;
			this.storedMetadata = i2;
			this.storedOrientation = i3;
			this.extending = z4;
			this.shouldHeadBeRendered = z5;
		}

		public virtual int StoredBlockID
		{
			get
			{
				return this.storedBlockID;
			}
		}

		public override int BlockMetadata
		{
			get
			{
				return this.storedMetadata;
			}
		}

		public virtual bool Extending
		{
			get
			{
				return this.extending;
			}
		}

		public virtual int PistonOrientation
		{
			get
			{
				return this.storedOrientation;
			}
		}

		public virtual bool shouldRenderHead()
		{
			return this.shouldHeadBeRendered;
		}

		public virtual float getProgress(float f1)
		{
			if (f1 > 1.0F)
			{
				f1 = 1.0F;
			}

			return this.lastProgress + (this.progress - this.lastProgress) * f1;
		}

		public virtual float getOffsetX(float f1)
		{
			return this.extending ? (this.getProgress(f1) - 1.0F) * (float)Facing.offsetsXForSide[this.storedOrientation] : (1.0F - this.getProgress(f1)) * (float)Facing.offsetsXForSide[this.storedOrientation];
		}

		public virtual float getOffsetY(float f1)
		{
			return this.extending ? (this.getProgress(f1) - 1.0F) * (float)Facing.offsetsYForSide[this.storedOrientation] : (1.0F - this.getProgress(f1)) * (float)Facing.offsetsYForSide[this.storedOrientation];
		}

		public virtual float getOffsetZ(float f1)
		{
			return this.extending ? (this.getProgress(f1) - 1.0F) * (float)Facing.offsetsZForSide[this.storedOrientation] : (1.0F - this.getProgress(f1)) * (float)Facing.offsetsZForSide[this.storedOrientation];
		}

		private void updatePushedObjects(float f1, float f2)
		{
			if (!this.extending)
			{
				--f1;
			}
			else
			{
				f1 = 1.0F - f1;
			}

			AxisAlignedBB axisAlignedBB3 = Block.pistonMoving.getAxisAlignedBB(this.worldObj, this.xCoord, this.yCoord, this.zCoord, this.storedBlockID, f1, this.storedOrientation);
			if (axisAlignedBB3 != null)
			{
				System.Collections.IList list4 = this.worldObj.getEntitiesWithinAABBExcludingEntity((Entity)null, axisAlignedBB3);
				if (list4.Count > 0)
				{
					pushedObjects.AddRange(list4);
					System.Collections.IEnumerator iterator5 = pushedObjects.GetEnumerator();

					while (iterator5.MoveNext())
					{
						Entity entity6 = (Entity)iterator5.Current;
						entity6.moveEntity((double)(f2 * (float)Facing.offsetsXForSide[this.storedOrientation]), (double)(f2 * (float)Facing.offsetsYForSide[this.storedOrientation]), (double)(f2 * (float)Facing.offsetsZForSide[this.storedOrientation]));
					}

					pushedObjects.Clear();
				}
			}

		}

		public virtual void clearPistonTileEntity()
		{
			if (this.lastProgress < 1.0F && this.worldObj != null)
			{
				this.lastProgress = this.progress = 1.0F;
				this.worldObj.removeBlockTileEntity(this.xCoord, this.yCoord, this.zCoord);
				this.invalidate();
				if (this.worldObj.getBlockId(this.xCoord, this.yCoord, this.zCoord) == Block.pistonMoving.blockID)
				{
					this.worldObj.setBlockAndMetadataWithNotify(this.xCoord, this.yCoord, this.zCoord, this.storedBlockID, this.storedMetadata);
				}
			}

		}

		public override void updateEntity()
		{
			this.lastProgress = this.progress;
			if (this.lastProgress >= 1.0F)
			{
				this.updatePushedObjects(1.0F, 0.25F);
				this.worldObj.removeBlockTileEntity(this.xCoord, this.yCoord, this.zCoord);
				this.invalidate();
				if (this.worldObj.getBlockId(this.xCoord, this.yCoord, this.zCoord) == Block.pistonMoving.blockID)
				{
					this.worldObj.setBlockAndMetadataWithNotify(this.xCoord, this.yCoord, this.zCoord, this.storedBlockID, this.storedMetadata);
				}

			}
			else
			{
				this.progress += 0.5F;
				if (this.progress >= 1.0F)
				{
					this.progress = 1.0F;
				}

				if (this.extending)
				{
					this.updatePushedObjects(this.progress, this.progress - this.lastProgress + 0.0625F);
				}

			}
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			base.readFromNBT(nBTTagCompound1);
			this.storedBlockID = nBTTagCompound1.getInteger("blockId");
			this.storedMetadata = nBTTagCompound1.getInteger("blockData");
			this.storedOrientation = nBTTagCompound1.getInteger("facing");
			this.lastProgress = this.progress = nBTTagCompound1.getFloat("progress");
			this.extending = nBTTagCompound1.getBoolean("extending");
		}

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			base.writeToNBT(nBTTagCompound1);
			nBTTagCompound1.setInteger("blockId", this.storedBlockID);
			nBTTagCompound1.setInteger("blockData", this.storedMetadata);
			nBTTagCompound1.setInteger("facing", this.storedOrientation);
			nBTTagCompound1.setFloat("progress", this.lastProgress);
			nBTTagCompound1.setBoolean("extending", this.extending);
		}
	}

}