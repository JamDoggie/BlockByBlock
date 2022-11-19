using BlockByBlock.java_extensions;
using System;

namespace net.minecraft.src
{

	public class WorldGenBigTree : WorldGenerator
	{
		internal static readonly sbyte[] otherCoordPairs = new sbyte[]{(sbyte)2, (sbyte)0, (sbyte)0, (sbyte)1, (sbyte)2, (sbyte)1};
		internal RandomExtended rand = new RandomExtended();
		internal World worldObj;
		internal int[] basePos = new int[]{0, 0, 0};
		internal int heightLimit = 0;
		internal int height;
		internal double heightAttenuation = 0.618D;
		internal double branchDensity = 1.0D;
		internal double branchSlope = 0.381D;
		internal double scaleWidth = 1.0D;
		internal double leafDensity = 1.0D;
		internal int trunkSize = 1;
		internal int heightLimitLimit = 12;
		internal int leafDistanceLimit = 4;
		internal int[][] leafNodes;

		public WorldGenBigTree(bool z1) : base(z1)
		{
		}

		internal virtual void generateLeafNodeList()
		{
			this.height = (int)((double)this.heightLimit * this.heightAttenuation);
			if (this.height >= this.heightLimit)
			{
				this.height = this.heightLimit - 1;
			}

			int i1 = (int)(1.382D + Math.Pow(this.leafDensity * (double)this.heightLimit / 13.0D, 2.0D));
			if (i1 < 1)
			{
				i1 = 1;
			}
            
			int[][] i2 = RectangularArrays.RectangularIntArray(i1 * this.heightLimit, 4);
			int i3 = this.basePos[1] + this.heightLimit - this.leafDistanceLimit;
			int i4 = 1;
			int i5 = this.basePos[1] + this.height;
			int i6 = i3 - this.basePos[1];
			i2[0][0] = this.basePos[0];
			i2[0][1] = i3;
			i2[0][2] = this.basePos[2];
			i2[0][3] = i5;
			--i3;

			while (true)
			{
				while (i6 >= 0)
				{
					int i7 = 0;
					float f8 = this.layerSize(i6);
					if (f8 < 0.0F)
					{
						--i3;
						--i6;
					}
					else
					{
						for (double d9 = 0.5D; i7 < i1; ++i7)
						{
							double d11 = this.scaleWidth * (double)f8 * ((double)this.rand.NextSingle() + 0.328D);
							double d13 = (double)this.rand.NextSingle() * 2.0D * 3.14159D;
							int i15 = MathHelper.floor_double(d11 * Math.Sin(d13) + (double)this.basePos[0] + d9);
							int i16 = MathHelper.floor_double(d11 * Math.Cos(d13) + (double)this.basePos[2] + d9);
							int[] i17 = new int[]{i15, i3, i16};
							int[] i18 = new int[]{i15, i3 + this.leafDistanceLimit, i16};
							if (this.checkBlockLine(i17, i18) == -1)
							{
								int[] i19 = new int[]{this.basePos[0], this.basePos[1], this.basePos[2]};
								double d20 = Math.Sqrt(Math.Pow((double)Math.Abs(this.basePos[0] - i17[0]), 2.0D) + Math.Pow((double)Math.Abs(this.basePos[2] - i17[2]), 2.0D));
								double d22 = d20 * this.branchSlope;
								if ((double)i17[1] - d22 > (double)i5)
								{
									i19[1] = i5;
								}
								else
								{
									i19[1] = (int)((double)i17[1] - d22);
								}

								if (this.checkBlockLine(i19, i17) == -1)
								{
									i2[i4][0] = i15;
									i2[i4][1] = i3;
									i2[i4][2] = i16;
									i2[i4][3] = i19[1];
									++i4;
								}
							}
						}

						--i3;
						--i6;
					}
				}

//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
//ORIGINAL LINE: this.leafNodes = new int[i4][4];
				this.leafNodes = RectangularArrays.RectangularIntArray(i4, 4);
				Array.Copy(i2, 0, this.leafNodes, 0, i4);
				return;
			}
		}

		internal virtual void genTreeLayer(int i1, int i2, int i3, float f4, sbyte b5, int i6)
		{
			int i7 = (int)((double)f4 + 0.618D);
			sbyte b8 = otherCoordPairs[b5];
			sbyte b9 = otherCoordPairs[b5 + 3];
			int[] i10 = new int[]{i1, i2, i3};
			int[] i11 = new int[]{0, 0, 0};
			int i12 = -i7;
			int i13 = -i7;

			for (i11[b5] = i10[b5]; i12 <= i7; ++i12)
			{
				i11[b8] = i10[b8] + i12;
				i13 = -i7;

				while (true)
				{
					while (true)
					{
						if (i13 > i7)
						{
							goto label32Continue;
						}

						double d15 = Math.Sqrt(Math.Pow((double)Math.Abs(i12) + 0.5D, 2.0D) + Math.Pow((double)Math.Abs(i13) + 0.5D, 2.0D));
						if (d15 > (double)f4)
						{
							++i13;
						}
						else
						{
							i11[b9] = i10[b9] + i13;
							int i14 = this.worldObj.getBlockId(i11[0], i11[1], i11[2]);
							if (i14 != 0 && i14 != 18)
							{
								++i13;
							}
							else
							{
								this.setBlockAndMetadata(this.worldObj, i11[0], i11[1], i11[2], i6, 0);
								++i13;
							}
						}
					}
				}
				label32Continue:;
			}
			label32Break:;

		}

		internal virtual float layerSize(int i1)
		{
			if ((double)i1 < (double)((float)this.heightLimit) * 0.3D)
			{
				return -1.618F;
			}
			else
			{
				float f2 = (float)this.heightLimit / 2.0F;
				float f3 = (float)this.heightLimit / 2.0F - (float)i1;
				float f4;
				if (f3 == 0.0F)
				{
					f4 = f2;
				}
				else if (Math.Abs(f3) >= f2)
				{
					f4 = 0.0F;
				}
				else
				{
					f4 = (float)Math.Sqrt(Math.Pow((double)Math.Abs(f2), 2.0D) - Math.Pow((double)Math.Abs(f3), 2.0D));
				}

				f4 *= 0.5F;
				return f4;
			}
		}

		internal virtual float leafSize(int i1)
		{
			return i1 >= 0 && i1 < this.leafDistanceLimit ? (i1 != 0 && i1 != this.leafDistanceLimit - 1 ? 3.0F : 2.0F) : -1.0F;
		}

		internal virtual void generateLeafNode(int i1, int i2, int i3)
		{
			int i4 = i2;

			for (int i5 = i2 + this.leafDistanceLimit; i4 < i5; ++i4)
			{
				float f6 = this.leafSize(i4 - i2);
				this.genTreeLayer(i1, i4, i3, f6, (sbyte)1, 18);
			}

		}

		internal virtual void placeBlockLine(int[] i1, int[] i2, int i3)
		{
			int[] i4 = new int[]{0, 0, 0};
			sbyte b5 = 0;

			sbyte b6;
			for (b6 = 0; b5 < 3; ++b5)
			{
				i4[b5] = i2[b5] - i1[b5];
				if (Math.Abs(i4[b5]) > Math.Abs(i4[b6]))
				{
					b6 = b5;
				}
			}

			if (i4[b6] != 0)
			{
				sbyte b7 = otherCoordPairs[b6];
				sbyte b8 = otherCoordPairs[b6 + 3];
				sbyte b9;
				if (i4[b6] > 0)
				{
					b9 = 1;
				}
				else
				{
					b9 = -1;
				}

				double d10 = (double)i4[b7] / (double)i4[b6];
				double d12 = (double)i4[b8] / (double)i4[b6];
				int[] i14 = new int[]{0, 0, 0};
				int i15 = 0;

				for (int i16 = i4[b6] + b9; i15 != i16; i15 += b9)
				{
					i14[b6] = MathHelper.floor_double((double)(i1[b6] + i15) + 0.5D);
					i14[b7] = MathHelper.floor_double((double)i1[b7] + (double)i15 * d10 + 0.5D);
					i14[b8] = MathHelper.floor_double((double)i1[b8] + (double)i15 * d12 + 0.5D);
					this.setBlockAndMetadata(this.worldObj, i14[0], i14[1], i14[2], i3, 0);
				}

			}
		}

		internal virtual void generateLeaves()
		{
			int i1 = 0;

			for (int i2 = this.leafNodes.Length; i1 < i2; ++i1)
			{
				int i3 = this.leafNodes[i1][0];
				int i4 = this.leafNodes[i1][1];
				int i5 = this.leafNodes[i1][2];
				this.generateLeafNode(i3, i4, i5);
			}

		}

		internal virtual bool leafNodeNeedsBase(int i1)
		{
			return (double)i1 >= (double)this.heightLimit * 0.2D;
		}

		internal virtual void generateTrunk()
		{
			int i1 = this.basePos[0];
			int i2 = this.basePos[1];
			int i3 = this.basePos[1] + this.height;
			int i4 = this.basePos[2];
			int[] i5 = new int[]{i1, i2, i4};
			int[] i6 = new int[]{i1, i3, i4};
			this.placeBlockLine(i5, i6, 17);
			if (this.trunkSize == 2)
			{
				++i5[0];
				++i6[0];
				this.placeBlockLine(i5, i6, 17);
				++i5[2];
				++i6[2];
				this.placeBlockLine(i5, i6, 17);
				i5[0] += -1;
				i6[0] += -1;
				this.placeBlockLine(i5, i6, 17);
			}

		}

		internal virtual void generateLeafNodeBases()
		{
			int i1 = 0;
			int i2 = this.leafNodes.Length;

			for (int[] i3 = new int[]{this.basePos[0], this.basePos[1], this.basePos[2]}; i1 < i2; ++i1)
			{
				int[] i4 = this.leafNodes[i1];
				int[] i5 = new int[]{i4[0], i4[1], i4[2]};
				i3[1] = i4[3];
				int i6 = i3[1] - this.basePos[1];
				if (this.leafNodeNeedsBase(i6))
				{
					this.placeBlockLine(i3, i5, 17);
				}
			}

		}

		internal virtual int checkBlockLine(int[] i1, int[] i2)
		{
			int[] i3 = new int[]{0, 0, 0};
			sbyte b4 = 0;

			sbyte b5;
			for (b5 = 0; b4 < 3; ++b4)
			{
				i3[b4] = i2[b4] - i1[b4];
				if (Math.Abs(i3[b4]) > Math.Abs(i3[b5]))
				{
					b5 = b4;
				}
			}

			if (i3[b5] == 0)
			{
				return -1;
			}
			else
			{
				sbyte b6 = otherCoordPairs[b5];
				sbyte b7 = otherCoordPairs[b5 + 3];
				sbyte b8;
				if (i3[b5] > 0)
				{
					b8 = 1;
				}
				else
				{
					b8 = -1;
				}

				double d9 = (double)i3[b6] / (double)i3[b5];
				double d11 = (double)i3[b7] / (double)i3[b5];
				int[] i13 = new int[]{0, 0, 0};
				int i14 = 0;

				int i15;
				for (i15 = i3[b5] + b8; i14 != i15; i14 += b8)
				{
					i13[b5] = i1[b5] + i14;
					i13[b6] = MathHelper.floor_double((double)i1[b6] + (double)i14 * d9);
					i13[b7] = MathHelper.floor_double((double)i1[b7] + (double)i14 * d11);
					int i16 = this.worldObj.getBlockId(i13[0], i13[1], i13[2]);
					if (i16 != 0 && i16 != 18)
					{
						break;
					}
				}

				return i14 == i15 ? -1 : Math.Abs(i14);
			}
		}

		internal virtual bool validTreeLocation()
		{
			int[] i1 = new int[]{this.basePos[0], this.basePos[1], this.basePos[2]};
			int[] i2 = new int[]{this.basePos[0], this.basePos[1] + this.heightLimit - 1, this.basePos[2]};
			int i3 = this.worldObj.getBlockId(this.basePos[0], this.basePos[1] - 1, this.basePos[2]);
			if (i3 != 2 && i3 != 3)
			{
				return false;
			}
			else
			{
				int i4 = this.checkBlockLine(i1, i2);
				if (i4 == -1)
				{
					return true;
				}
				else if (i4 < 6)
				{
					return false;
				}
				else
				{
					this.heightLimit = i4;
					return true;
				}
			}
		}

		public override void setScale(double d1, double d3, double d5)
		{
			this.heightLimitLimit = (int)(d1 * 12.0D);
			if (d1 > 0.5D)
			{
				this.leafDistanceLimit = 5;
			}

			this.scaleWidth = d3;
			this.leafDensity = d5;
		}

		public override bool generate(World world1, RandomExtended random2, int i3, int i4, int i5)
		{
			this.worldObj = world1;
			long j6 = random2.NextInt64();
			rand.SetSeed(j6);
			this.basePos[0] = i3;
			this.basePos[1] = i4;
			this.basePos[2] = i5;
			if (this.heightLimit == 0)
			{
				this.heightLimit = 5 + this.rand.Next(this.heightLimitLimit);
			}

			if (!this.validTreeLocation())
			{
				return false;
			}
			else
			{
				this.generateLeafNodeList();
				this.generateLeaves();
				this.generateTrunk();
				this.generateLeafNodeBases();
				return true;
			}
		}
	}

}