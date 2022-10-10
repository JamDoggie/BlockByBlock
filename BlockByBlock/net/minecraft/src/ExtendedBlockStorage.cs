namespace net.minecraft.src
{
	public class ExtendedBlockStorage
	{
		private int yBase;
		private int blockRefCount;
		private int tickRefCount;
		private volatile sbyte[] _blockLSBArray;
		private sbyte[] blockLSBArray
        {
            get
            {
				lock(typeof(ExtendedBlockStorage))
                {
					return _blockLSBArray;
				}
			}

			set
            {
                lock(typeof(ExtendedBlockStorage))
                {
					_blockLSBArray = value;
				}
            }
        }
		private NibbleArray blockMSBArray;
		private NibbleArray blockMetadataArray;
		private NibbleArray blocklightArray;
		private NibbleArray skylightArray;

		public ExtendedBlockStorage(int i1)
		{
			this.yBase = i1;
			this.blockLSBArray = new sbyte[4096];
			this.blockMetadataArray = new NibbleArray(this.blockLSBArray.Length, 4);
			this.skylightArray = new NibbleArray(this.blockLSBArray.Length, 4);
			this.blocklightArray = new NibbleArray(this.blockLSBArray.Length, 4);
		}

		public virtual int getExtBlockID(int i1, int i2, int i3)
		{
			int i4 = this.blockLSBArray[i2 << 8 | i3 << 4 | i1] & 255;
			return this.blockMSBArray != null ? this.blockMSBArray.get(i1, i2, i3) << 8 | i4 : i4;
		}

		public virtual void setExtBlockID(int i1, int i2, int i3, int i4)
		{
			int i5 = this.blockLSBArray[i2 << 8 | i3 << 4 | i1] & 255;
			if (this.blockMSBArray != null)
			{
				i5 |= this.blockMSBArray.get(i1, i2, i3) << 8;
			}

			if (i5 == 0 && i4 != 0)
			{
				++this.blockRefCount;
				if (Block.blocksList[i4] != null && Block.blocksList[i4].TickRandomly)
				{
					++this.tickRefCount;
				}
			}
			else if (i5 != 0 && i4 == 0)
			{
				--this.blockRefCount;
				if (Block.blocksList[i5] != null && Block.blocksList[i5].TickRandomly)
				{
					--this.tickRefCount;
				}
			}
			else if (Block.blocksList[i5] != null && Block.blocksList[i5].TickRandomly && (Block.blocksList[i4] == null || !Block.blocksList[i4].TickRandomly))
			{
				--this.tickRefCount;
			}
			else if ((Block.blocksList[i5] == null || !Block.blocksList[i5].TickRandomly) && Block.blocksList[i4] != null && Block.blocksList[i4].TickRandomly)
			{
				++this.tickRefCount;
			}

			this.blockLSBArray[i2 << 8 | i3 << 4 | i1] = unchecked((sbyte)(i4 & 255));
			if (i4 > 255)
			{
				if (this.blockMSBArray == null)
				{
					this.blockMSBArray = new NibbleArray(this.blockLSBArray.Length, 4);
				}

				this.blockMSBArray.set(i1, i2, i3, (i4 & 3840) >> 8);
			}
			else if (this.blockMSBArray != null)
			{
				this.blockMSBArray.set(i1, i2, i3, 0);
			}

		}

		public virtual int getExtBlockMetadata(int i1, int i2, int i3)
		{
			return this.blockMetadataArray.get(i1, i2, i3);
		}

		public virtual void setExtBlockMetadata(int i1, int i2, int i3, int i4)
		{
			this.blockMetadataArray.set(i1, i2, i3, i4);
		}

		public virtual bool IsEmpty
		{
			get
			{
				return this.blockRefCount == 0;
			}
		}

		public virtual bool NeedsRandomTick
		{
			get
			{
				return this.tickRefCount > 0;
			}
		}

		public virtual int YLocation
		{
			get
			{
				return this.yBase;
			}
		}

		public virtual void setExtSkylightValue(int i1, int i2, int i3, int i4)
		{
			this.skylightArray.set(i1, i2, i3, i4);
		}

		public virtual int getExtSkylightValue(int i1, int i2, int i3)
		{
			return this.skylightArray.get(i1, i2, i3);
		}

		public virtual void setExtBlocklightValue(int i1, int i2, int i3, int i4)
		{
			this.blocklightArray.set(i1, i2, i3, i4);
		}

		public virtual int getExtBlocklightValue(int i1, int i2, int i3)
		{
			return this.blocklightArray.get(i1, i2, i3);
		}

		public virtual void func_48708_d()
		{
			this.blockRefCount = 0;
			this.tickRefCount = 0;

			for (int i1 = 0; i1 < 16; ++i1)
			{
				for (int i2 = 0; i2 < 16; ++i2)
				{
					for (int i3 = 0; i3 < 16; ++i3)
					{
						int i4 = this.getExtBlockID(i1, i2, i3);
						if (i4 > 0)
						{
							if (Block.blocksList[i4] == null)
							{
								this.blockLSBArray[i2 << 8 | i3 << 4 | i1] = 0;
								if (this.blockMSBArray != null)
								{
									this.blockMSBArray.set(i1, i2, i3, 0);
								}
							}
							else
							{
								++this.blockRefCount;
								if (Block.blocksList[i4].TickRandomly)
								{
									++this.tickRefCount;
								}
							}
						}
					}
				}
			}

		}

		public virtual void func_48711_e()
		{
		}

		public virtual int func_48700_f()
		{
			return this.blockRefCount;
		}

		public virtual sbyte[] func_48692_g()
		{
			return this.blockLSBArray;
		}

		public virtual void func_48715_h()
		{
			this.blockMSBArray = null;
		}

		public virtual NibbleArray BlockMSBArray
		{
			get
			{
				return this.blockMSBArray;
			}
			set
			{
				this.blockMSBArray = value;
			}
		}

		public virtual NibbleArray func_48697_j()
		{
			return this.blockMetadataArray;
		}

		public virtual NibbleArray BlocklightArray
		{
			get
			{
				return this.blocklightArray;
			}
			set
			{
				this.blocklightArray = value;
			}
		}

		public virtual NibbleArray SkylightArray
		{
			get
			{
				return this.skylightArray;
			}
			set
			{
				this.skylightArray = value;
			}
		}

		public virtual sbyte[] BlockLSBArray
		{
			set
			{
				this.blockLSBArray = value;
			}
		}


		public virtual NibbleArray BlockMetadataArray
		{
			set
			{
				this.blockMetadataArray = value;
			}
		}



		public virtual NibbleArray createBlockMSBArray()
		{
			this.blockMSBArray = new NibbleArray(this.blockLSBArray.Length, 4);
			return this.blockMSBArray;
		}
	}

}