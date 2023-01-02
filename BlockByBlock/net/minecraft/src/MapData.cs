using BlockByBlock.helpers;
using net.minecraft.client.entity;
using System.Collections;

namespace net.minecraft.src
{

    public class MapData : WorldSavedData
	{
		public int xCenter;
		public int zCenter;
		public sbyte dimension;
		public sbyte scale;
		public sbyte[] colors = new sbyte[16384];
		public int field_28175_g;
		public System.Collections.IList field_28174_h = new ArrayList();
		private System.Collections.IDictionary field_28172_j = new Hashtable();
		public System.Collections.IList playersVisibleOnMap = new ArrayList();

		public MapData(string string1) : base(string1)
		{
		}

		public override void readFromNBT(NBTTagCompound nBTTagCompound1)
		{
			this.dimension = nBTTagCompound1.getByte("dimension");
			this.xCenter = nBTTagCompound1.getInteger("xCenter");
			this.zCenter = nBTTagCompound1.getInteger("zCenter");
			this.scale = nBTTagCompound1.getByte("scale");
			if (this.scale < 0)
			{
				this.scale = 0;
			}

			if (this.scale > 4)
			{
				this.scale = 4;
			}

			short s2 = nBTTagCompound1.getShort("width");
			short s3 = nBTTagCompound1.getShort("height");
			if (s2 == 128 && s3 == 128)
			{
				this.colors = nBTTagCompound1.getByteArray("colors");
			}
			else
			{
				sbyte[] b4 = nBTTagCompound1.getByteArray("colors");
				this.colors = new sbyte[16384];
				int i5 = (128 - s2) / 2;
				int i6 = (128 - s3) / 2;

				for (int i7 = 0; i7 < s3; ++i7)
				{
					int i8 = i7 + i6;
					if (i8 >= 0 || i8 < 128)
					{
						for (int i9 = 0; i9 < s2; ++i9)
						{
							int i10 = i9 + i5;
							if (i10 >= 0 || i10 < 128)
							{
								this.colors[i10 + i8 * 128] = b4[i9 + i7 * s2];
							}
						}
					}
				}
			}

		}

		public override void writeToNBT(NBTTagCompound nBTTagCompound1)
		{
			nBTTagCompound1.setByte("dimension", this.dimension);
			nBTTagCompound1.setInteger("xCenter", this.xCenter);
			nBTTagCompound1.setInteger("zCenter", this.zCenter);
			nBTTagCompound1.setByte("scale", this.scale);
			nBTTagCompound1.setShort("width", (short)128);
			nBTTagCompound1.setShort("height", (short)128);
			nBTTagCompound1.setByteArray("colors", this.colors);
		}

		public virtual void func_28169_a(EntityPlayer entityPlayer1, ItemStack itemStack2)
		{
			if (!this.field_28172_j.Contains(entityPlayer1))
			{
				MapInfo mapInfo3 = new MapInfo(this, entityPlayer1);
				this.field_28172_j[entityPlayer1] = mapInfo3;
				this.field_28174_h.Add(mapInfo3);
			}

			this.playersVisibleOnMap.Clear();

			for (int i14 = 0; i14 < this.field_28174_h.Count; ++i14)
			{
				MapInfo mapInfo4 = (MapInfo)this.field_28174_h[i14];
				if (!mapInfo4.entityplayerObj.isDead && mapInfo4.entityplayerObj.inventory.hasItemStack(itemStack2))
				{
					float f5 = (float)(mapInfo4.entityplayerObj.posX - (double)this.xCenter) / (float)(1 << this.scale);
					float f6 = (float)(mapInfo4.entityplayerObj.posZ - (double)this.zCenter) / (float)(1 << this.scale);
					sbyte b7 = 64;
					sbyte b8 = 64;
					if (f5 >= (float)(-b7) && f6 >= (float)(-b8) && f5 <= (float)b7 && f6 <= (float)b8)
					{
						sbyte b9 = 0;
						sbyte b10 = (sbyte)((int)((double)(f5 * 2.0F) + 0.5D));
						sbyte b11 = (sbyte)((int)((double)(f6 * 2.0F) + 0.5D));
						sbyte b12 = (sbyte)((int)((double)(entityPlayer1.rotationYaw * 16.0F / 360.0F) + 0.5D));
						if (this.dimension < 0)
						{
							int i13 = this.field_28175_g / 10;
							b12 = unchecked((sbyte)(i13 * i13 * 34187121 + i13 * 121 >> 15 & 15));
						}

						if (mapInfo4.entityplayerObj.dimension == this.dimension)
						{
							this.playersVisibleOnMap.Add(new MapCoord(this, b9, b10, b11, b12));
						}
					}
				}
				else
				{
					this.field_28172_j.Remove(mapInfo4.entityplayerObj);
					this.field_28174_h.Remove(mapInfo4);
				}
			}

		}

		public virtual void func_28170_a(int i1, int i2, int i3)
		{
			base.markDirty();

			for (int i4 = 0; i4 < this.field_28174_h.Count; ++i4)
			{
				MapInfo mapInfo5 = (MapInfo)this.field_28174_h[i4];
				if (mapInfo5.field_28119_b[i1] < 0 || mapInfo5.field_28119_b[i1] > i2)
				{
					mapInfo5.field_28119_b[i1] = i2;
				}

				if (mapInfo5.field_28124_c[i1] < 0 || mapInfo5.field_28124_c[i1] < i3)
				{
					mapInfo5.field_28124_c[i1] = i3;
				}
			}

		}

		public virtual void func_28171_a(byte[] b1)
		{
			int i2;
			if (b1[0] == 0)
			{
				i2 = b1[1] & 255;
				int i3 = b1[2] & 255;

				for (int i4 = 0; i4 < b1.Length - 3; ++i4)
				{
					this.colors[(i4 + i3) * 128 + i2] = JTypes.ByteToRawSByteBits(b1[i4 + 3]);
				}

				this.markDirty();
			}
			else if (b1[0] == 1)
			{
				this.playersVisibleOnMap.Clear();

				for (i2 = 0; i2 < (b1.Length - 1) / 3; ++i2)
				{
					sbyte b7 = (sbyte)(JTypes.ByteToRawSByteBits(b1[i2 * 3 + 1]) % 16);
					sbyte b8 = JTypes.ByteToRawSByteBits(b1[i2 * 3 + 2]);
					sbyte b5 = JTypes.ByteToRawSByteBits(b1[i2 * 3 + 3]);
					sbyte b6 = (sbyte)(JTypes.ByteToRawSByteBits(b1[i2 * 3 + 1]) / 16);
					this.playersVisibleOnMap.Add(new MapCoord(this, b7, b8, b5, b6));
				}
			}

		}
	}

}