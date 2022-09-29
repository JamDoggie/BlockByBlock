using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public abstract class StructurePieceBlockSelector
	{
		protected internal int selectedBlockId;
		protected internal int selectedBlockMetaData;

		public abstract void selectBlocks(RandomExtended random1, int i2, int i3, int i4, bool z5);

		public virtual int SelectedBlockId
		{
			get
			{
				return this.selectedBlockId;
			}
		}

		public virtual int SelectedBlockMetaData
		{
			get
			{
				return this.selectedBlockMetaData;
			}
		}
	}

}