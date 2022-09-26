using BidiSharp;
using BlockByBlock;
using BlockByBlock.java_extensions;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace net.minecraft.src
{

	using GL11 = org.lwjgl.opengl.GL11;

	// PORTING TODO: OpenGL code
	public class FontRenderer
	{
		private static readonly Regex chatColorRegex = new Regex("(?i)\\u00A7[0-9A-FK-OR]", RegexOptions.Compiled);
		private int[] charWidth = new int[256];
		public int fontTextureName = 0;
		public int FONT_HEIGHT = 8;
		public RandomExtended fontRandom = new RandomExtended();
		private byte[] glyphWidth = new byte[65536];
		private readonly int[] glyphTextureName = new int[256];
		private int[] colorCode = new int[32];
		private int boundTextureName;
		private readonly RenderEngine renderEngine;
		private float posX;
		private float posY;
		private bool unicodeFlag;
		private bool bidiFlag;
		private float field_50115_n;
		private float field_50116_o;
		private float field_50118_p;
		private float field_50117_q;

		internal FontRenderer()
		{
			this.renderEngine = null;
		}

		public FontRenderer(GameSettings gameSettings1, string string2, RenderEngine renderEngine3, bool z4)
		{
			this.renderEngine = renderEngine3;
			this.unicodeFlag = z4;

			BufferedImage bufferedImage5;
			try
			{
				bufferedImage5 = ImageIO.read(GameEnv.GetResourceAsStream(string2));
				Stream? inputStream6 = GameEnv.GetResourceAsStream("/font/glyph_sizes.bin");
                
				if (inputStream6 != null)
					inputStream6.Read(this.glyphWidth, 0, this.glyphWidth.Length);
			}
			catch (IOException iOException18)
			{
				throw new Exception(iOException18);
			}

			int i19 = bufferedImage5.getWidth();
			int i7 = bufferedImage5.getHeight();
			int[] i8 = new int[i19 * i7];
			bufferedImage5.getRGB(0, 0, i19, i7, i8, 0, i19);

			int i9;
			int i10;
			int i11;
			int i12;
			int i13;
			int i15;
			int i16;
			for (i9 = 0; i9 < 256; ++i9)
			{
				i10 = i9 % 16;
				i11 = i9 / 16;

				for (i12 = 7; i12 >= 0; --i12)
				{
					i13 = i10 * 8 + i12;
					bool z14 = true;

					for (i15 = 0; i15 < 8 && z14; ++i15)
					{
						i16 = (i11 * 8 + i15) * i19;
						int i17 = i8[i13 + i16] & 255;
						if (i17 > 0)
						{
							z14 = false;
						}
					}

					if (!z14)
					{
						break;
					}
				}

				if (i9 == 32)
				{
					i12 = 2;
				}

				this.charWidth[i9] = i12 + 2;
			}

			this.fontTextureName = renderEngine3.allocateAndSetupTexture(bufferedImage5);

			for (i9 = 0; i9 < 32; ++i9)
			{
				i10 = (i9 >> 3 & 1) * 85;
				i11 = (i9 >> 2 & 1) * 170 + i10;
				i12 = (i9 >> 1 & 1) * 170 + i10;
				i13 = (i9 >> 0 & 1) * 170 + i10;
				if (i9 == 6)
				{
					i11 += 85;
				}

				if (gameSettings1.anaglyph)
				{
					int i20 = (i11 * 30 + i12 * 59 + i13 * 11) / 100;
					i15 = (i11 * 30 + i12 * 70) / 100;
					i16 = (i11 * 30 + i13 * 70) / 100;
					i11 = i20;
					i12 = i15;
					i13 = i16;
				}

				if (i9 >= 16)
				{
					i11 /= 4;
					i12 /= 4;
					i13 /= 4;
				}

				this.colorCode[i9] = (i11 & 255) << 16 | (i12 & 255) << 8 | i13 & 255;
			}

		}

		private float func_50112_a(int i1, char c2, bool z3)
		{
			return c2 == (char)32 ? 4.0F : (i1 > 0 && !this.unicodeFlag ? this.func_50106_a(i1 + 32, z3) : this.func_50111_a(c2, z3));
		}

		private float func_50106_a(int i1, bool z2)
		{
			float f3 = (float)(i1 % 16 * 8);
			float f4 = (float)(i1 / 16 * 8);
			float f5 = z2 ? 1.0F : 0.0F;
			if (this.boundTextureName != this.fontTextureName)
			{
				GL11.glBindTexture(GL11.GL_TEXTURE_2D, this.fontTextureName);
				this.boundTextureName = this.fontTextureName;
			}

			float f6 = (float)this.charWidth[i1] - 0.01F;
			GL11.glBegin(GL11.GL_TRIANGLE_STRIP);
			GL11.glTexCoord2f(f3 / 128.0F, f4 / 128.0F);
			GL11.glVertex3f(this.posX + f5, this.posY, 0.0F);
			GL11.glTexCoord2f(f3 / 128.0F, (f4 + 7.99F) / 128.0F);
			GL11.glVertex3f(this.posX - f5, this.posY + 7.99F, 0.0F);
			GL11.glTexCoord2f((f3 + f6) / 128.0F, f4 / 128.0F);
			GL11.glVertex3f(this.posX + f6 + f5, this.posY, 0.0F);
			GL11.glTexCoord2f((f3 + f6) / 128.0F, (f4 + 7.99F) / 128.0F);
			GL11.glVertex3f(this.posX + f6 - f5, this.posY + 7.99F, 0.0F);
			GL11.glEnd();
			return (float)this.charWidth[i1];
		}

		private void loadGlyphTexture(int i1)
		{
			string string3 = string.Format("/font/glyph_{0:X2}.png", new object[]{i1});

			BufferedImage bufferedImage2;
			try
			{
				bufferedImage2 = ImageIO.read(GameEnv.GetResourceAsStream(string3));
			}
			catch (IOException iOException5)
			{
				throw new Exception(iOException5);
			}

			this.glyphTextureName[i1] = this.renderEngine.allocateAndSetupTexture(bufferedImage2);
			this.boundTextureName = this.glyphTextureName[i1];
		}

		private float func_50111_a(char c1, bool z2)
		{
			if (this.glyphWidth[c1] == 0)
			{
				return 0.0F;
			}
			else
			{
				int i3 = c1 / 256;
				if (this.glyphTextureName[i3] == 0)
				{
					this.loadGlyphTexture(i3);
				}

				if (this.boundTextureName != this.glyphTextureName[i3])
				{
					GL11.glBindTexture(GL11.GL_TEXTURE_2D, this.glyphTextureName[i3]);
					this.boundTextureName = this.glyphTextureName[i3];
				}

				int i4 = (int)((uint)this.glyphWidth[c1] >> 4);
				int i5 = this.glyphWidth[c1] & 15;
				float f6 = (float)i4;
				float f7 = (float)(i5 + 1);
				float f8 = (float)(c1 % 16 * 16) + f6;
				float f9 = (float)((c1 & 255) / 16 * 16);
				float f10 = f7 - f6 - 0.02F;
				float f11 = z2 ? 1.0F : 0.0F;
				GL11.glBegin(GL11.GL_TRIANGLE_STRIP);
				GL11.glTexCoord2f(f8 / 256.0F, f9 / 256.0F);
				GL11.glVertex3f(this.posX + f11, this.posY, 0.0F);
				GL11.glTexCoord2f(f8 / 256.0F, (f9 + 15.98F) / 256.0F);
				GL11.glVertex3f(this.posX - f11, this.posY + 7.99F, 0.0F);
				GL11.glTexCoord2f((f8 + f10) / 256.0F, f9 / 256.0F);
				GL11.glVertex3f(this.posX + f10 / 2.0F + f11, this.posY, 0.0F);
				GL11.glTexCoord2f((f8 + f10) / 256.0F, (f9 + 15.98F) / 256.0F);
				GL11.glVertex3f(this.posX + f10 / 2.0F - f11, this.posY + 7.99F, 0.0F);
				GL11.glEnd();
				return (f7 - f6) / 2.0F + 1.0F;
			}
		}

		public virtual int drawStringWithShadow(string string1, int i2, int i3, int i4)
		{
			if (this.bidiFlag)
			{
				string1 = this.bidiReorder(string1);
			}

			int i5 = this.func_50101_a(string1, i2 + 1, i3 + 1, i4, true);
			i5 = Math.Max(i5, this.func_50101_a(string1, i2, i3, i4, false));
			return i5;
		}

		public virtual void drawString(string string1, int i2, int i3, int i4)
		{
			if (this.bidiFlag)
			{
				string1 = this.bidiReorder(string1);
			}

			this.func_50101_a(string1, i2, i3, i4, false);
		}

		private string bidiReorder(string str)
		{
			// PORTING TODO: properly implement this method
			return Bidi.LogicalToVisual(str);
            
			/*if (!string.ReferenceEquals(string1, null) && Bidi.requiresBidi(string1.ToCharArray(), 0, string1.Length))
			{
				BidiSharp.Bidi.
				Bidi bidi2 = new Bidi(string1, -2);
				sbyte[] b3 = new sbyte[bidi2.getRunCount()];
				string[] string4 = new string[b3.Length];

				int i7;
				for (int i5 = 0; i5 < b3.Length; ++i5)
				{
					int i6 = bidi2.getRunStart(i5);
					i7 = bidi2.getRunLimit(i5);
					int i8 = bidi2.getRunLevel(i5);
					string string9 = string1.Substring(i6, i7 - i6);
					b3[i5] = (sbyte)i8;
					string4[i5] = string9;
				}

				string[] string11 = (string[])(string[])string4.Clone();
				Bidi.reorderVisually(b3, 0, string4, 0, b3.Length);
				StringBuilder stringBuilder12 = new StringBuilder();

				for (i7 = 0; i7 < string4.Length; ++i7)
				{
					sbyte b13 = b3[i7];

					int i14;
					for (i14 = 0; i14 < string11.Length; ++i14)
					{
						if (string11[i14].Equals(string4[i7]))
						{
							b13 = b3[i14];
							break;
						}
					}

					if ((b13 & 1) == 0)
					{
						stringBuilder12.Append(string4[i7]);
					}
					else
					{
						for (i14 = string4[i7].Length - 1; i14 >= 0; --i14)
						{
							char c10 = string4[i7][i14];
							if (c10 == (char)40)
							{
								c10 = (char)41;
							}
							else if (c10 == (char)41)
							{
								c10 = (char)40;
							}

							stringBuilder12.Append(c10);
						}
					}
				}

				return stringBuilder12.ToString();
			}
			else
			{
				return string1;
			}*/
		}

		private void renderStringAtPos(string string1, bool z2)
		{
			bool z3 = false;
			bool z4 = false;
			bool z5 = false;
			bool z6 = false;
			bool z7 = false;

			for (int i8 = 0; i8 < string1.Length; ++i8)
			{
				char c9 = string1[i8];
				int i10;
				int i11;
				if (c9 == (char)167 && i8 + 1 < string1.Length)
				{
					i10 = "0123456789abcdefklmnor".IndexOf(string1.ToLower()[i8 + 1]);
					if (i10 < 16)
					{
						z3 = false;
						z4 = false;
						z7 = false;
						z6 = false;
						z5 = false;
						if (i10 < 0 || i10 > 15)
						{
							i10 = 15;
						}

						if (z2)
						{
							i10 += 16;
						}

						i11 = this.colorCode[i10];
						GL11.glColor3f((float)(i11 >> 16) / 255.0F, (float)(i11 >> 8 & 255) / 255.0F, (float)(i11 & 255) / 255.0F);
					}
					else if (i10 == 16)
					{
						z3 = true;
					}
					else if (i10 == 17)
					{
						z4 = true;
					}
					else if (i10 == 18)
					{
						z7 = true;
					}
					else if (i10 == 19)
					{
						z6 = true;
					}
					else if (i10 == 20)
					{
						z5 = true;
					}
					else if (i10 == 21)
					{
						z3 = false;
						z4 = false;
						z7 = false;
						z6 = false;
						z5 = false;
						GL11.glColor4f(this.field_50115_n, this.field_50116_o, this.field_50118_p, this.field_50117_q);
					}

					++i8;
				}
				else
				{
					i10 = ChatAllowedCharacters.allowedCharacters.IndexOf(c9);
					if (z3 && i10 > 0)
					{
						do
						{
							i11 = this.fontRandom.Next(ChatAllowedCharacters.allowedCharacters.Length);
						} while (this.charWidth[i10 + 32] != this.charWidth[i11 + 32]);

						i10 = i11;
					}

					float f14 = this.func_50112_a(i10, c9, z5);
					if (z4)
					{
						++this.posX;
						this.func_50112_a(i10, c9, z5);
						--this.posX;
						++f14;
					}

					Tessellator tessellator12;
					if (z7)
					{
						tessellator12 = Tessellator.instance;
						GL11.glDisable(GL11.GL_TEXTURE_2D);
						tessellator12.startDrawingQuads();
						tessellator12.addVertex((double)this.posX, (double)(this.posY + (float)(this.FONT_HEIGHT / 2)), 0.0D);
						tessellator12.addVertex((double)(this.posX + f14), (double)(this.posY + (float)(this.FONT_HEIGHT / 2)), 0.0D);
						tessellator12.addVertex((double)(this.posX + f14), (double)(this.posY + (float)(this.FONT_HEIGHT / 2) - 1.0F), 0.0D);
						tessellator12.addVertex((double)this.posX, (double)(this.posY + (float)(this.FONT_HEIGHT / 2) - 1.0F), 0.0D);
						tessellator12.draw();
						GL11.glEnable(GL11.GL_TEXTURE_2D);
					}

					if (z6)
					{
						tessellator12 = Tessellator.instance;
						GL11.glDisable(GL11.GL_TEXTURE_2D);
						tessellator12.startDrawingQuads();
						int i13 = z6 ? -1 : 0;
						tessellator12.addVertex((double)(this.posX + (float)i13), (double)(this.posY + (float)this.FONT_HEIGHT), 0.0D);
						tessellator12.addVertex((double)(this.posX + f14), (double)(this.posY + (float)this.FONT_HEIGHT), 0.0D);
						tessellator12.addVertex((double)(this.posX + f14), (double)(this.posY + (float)this.FONT_HEIGHT - 1.0F), 0.0D);
						tessellator12.addVertex((double)(this.posX + (float)i13), (double)(this.posY + (float)this.FONT_HEIGHT - 1.0F), 0.0D);
						tessellator12.draw();
						GL11.glEnable(GL11.GL_TEXTURE_2D);
					}

					this.posX += f14;
				}
			}

		}

		public virtual int func_50101_a(string string1, int i2, int i3, int i4, bool z5)
		{
			if (!string.ReferenceEquals(string1, null))
			{
				this.boundTextureName = 0;
				if ((i4 & -67108864) == 0)
				{
					i4 |= unchecked((int)0xFF000000);
				}

				if (z5)
				{
					i4 = (i4 & 16579836) >> 2 | i4 & (int)unchecked((int)0xFF000000);
				}

				this.field_50115_n = (float)(i4 >> 16 & 255) / 255.0F;
				this.field_50116_o = (float)(i4 >> 8 & 255) / 255.0F;
				this.field_50118_p = (float)(i4 & 255) / 255.0F;
				this.field_50117_q = (float)(i4 >> 24 & 255) / 255.0F;
				GL11.glColor4f(this.field_50115_n, this.field_50116_o, this.field_50118_p, this.field_50117_q);
				this.posX = (float)i2;
				this.posY = (float)i3;
				this.renderStringAtPos(string1, z5);
				return (int)this.posX;
			}
			else
			{
				return 0;
			}
		}

		public virtual int getStringWidth(string string1)
		{
			if (string.ReferenceEquals(string1, null))
			{
				return 0;
			}
			else
			{
				int i2 = 0;
				bool z3 = false;

				for (int i4 = 0; i4 < string1.Length; ++i4)
				{
					char c5 = string1[i4];
					int i6 = this.func_50105_a(c5);
					if (i6 < 0 && i4 < string1.Length - 1)
					{
						++i4;
						c5 = string1[i4];
						if (c5 != (char)108 && c5 != (char)76)
						{
							if (c5 == (char)114 || c5 == (char)82)
							{
								z3 = false;
							}
						}
						else
						{
							z3 = true;
						}

						i6 = this.func_50105_a(c5);
					}

					i2 += i6;
					if (z3)
					{
						++i2;
					}
				}

				return i2;
			}
		}

		public virtual int func_50105_a(char c1)
		{
			if (c1 == (char)167)
			{
				return -1;
			}
			else
			{
				int i2 = ChatAllowedCharacters.allowedCharacters.IndexOf(c1);
				if (i2 >= 0 && !this.unicodeFlag)
				{
					return this.charWidth[i2 + 32];
				}
				else if (this.glyphWidth[c1] != 0)
				{
					int i3 = this.glyphWidth[c1] >> 4;
					int i4 = this.glyphWidth[c1] & 15;
					if (i4 > 7)
					{
						i4 = 15;
						i3 = 0;
					}

					++i4;
					return (i4 - i3) / 2 + 1;
				}
				else
				{
					return 0;
				}
			}
		}

		public virtual string func_50107_a(string string1, int i2)
		{
			return this.func_50104_a(string1, i2, false);
		}

		public virtual string func_50104_a(string string1, int i2, bool z3)
		{
			StringBuilder stringBuilder4 = new StringBuilder();
			int i5 = 0;
			int i6 = z3 ? string1.Length - 1 : 0;
			int i7 = z3 ? -1 : 1;
			bool z8 = false;
			bool z9 = false;

			for (int i10 = i6; i10 >= 0 && i10 < string1.Length && i5 < i2; i10 += i7)
			{
				char c11 = string1[i10];
				int i12 = this.func_50105_a(c11);
				if (z8)
				{
					z8 = false;
					if (c11 != (char)108 && c11 != (char)76)
					{
						if (c11 == (char)114 || c11 == (char)82)
						{
							z9 = false;
						}
					}
					else
					{
						z9 = true;
					}
				}
				else if (i12 < 0)
				{
					z8 = true;
				}
				else
				{
					i5 += i12;
					if (z9)
					{
						++i5;
					}
				}

				if (i5 > i2)
				{
					break;
				}

				if (z3)
				{
					stringBuilder4.Insert(0, c11);
				}
				else
				{
					stringBuilder4.Append(c11);
				}
			}

			return stringBuilder4.ToString();
		}

		public virtual void drawSplitString(string string1, int i2, int i3, int i4, int i5)
		{
			if (this.bidiFlag)
			{
				string1 = this.bidiReorder(string1);
			}

			this.renderSplitStringNoShadow(string1, i2, i3, i4, i5);
		}

		private void renderSplitStringNoShadow(string string1, int i2, int i3, int i4, int i5)
		{
			this.renderSplitString(string1, i2, i3, i4, i5, false);
		}

		private void renderSplitString(string string1, int i2, int i3, int i4, int i5, bool z6)
		{
			string[] string7 = string1.Split("\n", true);
			if (string7.Length > 1)
			{
				for (int i14 = 0; i14 < string7.Length; ++i14)
				{
					this.renderSplitStringNoShadow(string7[i14], i2, i3, i4, i5);
					i3 += this.splitStringWidth(string7[i14], i4);
				}

			}
			else
			{
				string[] string8 = string1.Split(" ", true);
				int i9 = 0;
				string string10 = "";

				while (i9 < string8.Length)
				{
					string string11;
					for (string11 = string10 + string8[i9++] + " "; i9 < string8.Length && this.getStringWidth(string11 + string8[i9]) < i4; string11 = string11 + string8[i9++] + " ")
					{
					}

					int i12;
					for (; this.getStringWidth(string11) > i4; string11 = string10 + string11.Substring(i12))
					{
						for (i12 = 0; this.getStringWidth(string11.Substring(0, i12 + 1)) <= i4; ++i12)
						{
						}

						if (string11.Substring(0, i12).Trim().Length > 0)
						{
							string string13 = string11.Substring(0, i12);
							if (string13.LastIndexOf("\u00a7", StringComparison.Ordinal) >= 0)
							{
								string10 = "\u00a7" + string13[string13.LastIndexOf("\u00a7", StringComparison.Ordinal) + 1];
							}

							this.func_50101_a(string13, i2, i3, i5, z6);
							i3 += this.FONT_HEIGHT;
						}
					}

					if (this.getStringWidth(string11.Trim()) > 0)
					{
						if (string11.LastIndexOf("\u00a7", StringComparison.Ordinal) >= 0)
						{
							string10 = "\u00a7" + string11[string11.LastIndexOf("\u00a7", StringComparison.Ordinal) + 1];
						}

						this.func_50101_a(string11, i2, i3, i5, z6);
						i3 += this.FONT_HEIGHT;
					}
				}

			}
		}

		public virtual int splitStringWidth(string string1, int i2)
		{
			string[] string3 = string1.Split("\n", true);
			int i5;
			if (string3.Length > 1)
			{
				int i9 = 0;

				for (i5 = 0; i5 < string3.Length; ++i5)
				{
					i9 += this.splitStringWidth(string3[i5], i2);
				}

				return i9;
			}
			else
			{
				string[] string4 = string1.Split(" ", true);
				i5 = 0;
				int i6 = 0;

				while (i5 < string4.Length)
				{
					string string7;
					for (string7 = string4[i5++] + " "; i5 < string4.Length && this.getStringWidth(string7 + string4[i5]) < i2; string7 = string7 + string4[i5++] + " ")
					{
					}

					int i8;
					for (; this.getStringWidth(string7) > i2; string7 = string7.Substring(i8))
					{
						for (i8 = 0; this.getStringWidth(string7.Substring(0, i8 + 1)) <= i2; ++i8)
						{
						}

						if (string7.Substring(0, i8).Trim().Length > 0)
						{
							i6 += this.FONT_HEIGHT;
						}
					}

					if (string7.Trim().Length > 0)
					{
						i6 += this.FONT_HEIGHT;
					}
				}

				if (i6 < this.FONT_HEIGHT)
				{
					i6 += this.FONT_HEIGHT;
				}

				return i6;
			}
		}

		public virtual bool UnicodeFlag
		{
			set
			{
				this.unicodeFlag = value;
			}
		}

		public virtual bool BidiFlag
		{
			set
			{
				this.bidiFlag = value;
			}
		}

		public virtual System.Collections.IList func_50108_c(string string1, int i2)
		{
			return func_50113_d(string1, i2).Split("\n", true).ToList();
		}

		internal virtual string func_50113_d(string string1, int i2)
		{
			int i3 = this.func_50102_e(string1, i2);
			if (string1.Length <= i3)
			{
				return string1;
			}
			else
			{
				string string4 = string1.Substring(0, i3);
				string string5 = func_50114_c(string4) + string1.Substring(i3 + (string1[i3] == (char)32 ? 1 : 0));
				return string4 + "\n" + this.func_50113_d(string5, i2);
			}
		}

		private int func_50102_e(string string1, int i2)
		{
			int i3 = string1.Length;
			int i4 = 0;
			int i5 = 0;
			int i6 = -1;

			for (bool z7 = false; i5 < i3; ++i5)
			{
				char c8 = string1[i5];
				switch (c8)
				{
				case ' ':
					i6 = i5;
					goto default;
				default:
					i4 += this.func_50105_a(c8);
					if (z7)
					{
						++i4;
					}
					break;
				case '\u00a7':
					if (i5 != i3)
					{
						++i5;
						char c9 = string1[i5];
						if (c9 != (char)108 && c9 != (char)76)
						{
							if (c9 == (char)114 || c9 == (char)82)
							{
								z7 = false;
							}
						}
						else
						{
							z7 = true;
						}
					}
				break;
				}

				if (c8 == (char)10)
				{
					++i5;
					i6 = i5;
					break;
				}

				if (i4 > i2)
				{
					break;
				}
			}

			return i5 != i3 && i6 != -1 && i6 < i5 ? i6 : i5;
		}

		private static bool func_50110_b(char c0)
		{
			return c0 >= (char)48 && c0 <= (char)57 || c0 >= (char)97 && c0 <= (char)102 || c0 >= (char)65 && c0 <= (char)70;
		}

		private static bool func_50109_c(char c0)
		{
			return c0 >= (char)107 && c0 <= (char)111 || c0 >= (char)75 && c0 <= (char)79 || c0 == (char)114 || c0 == (char)82;
		}

		/// <summary>
		/// This has something to do with chat colors. char 167 is the section symbol (§) in the unicode font. 0x00A7
		/// </summary>
		/// <param name="string0"></param>
		/// <returns></returns>
		private static string func_50114_c(string string0)
		{
			string string1 = "";
			int i2 = -1;
			int i3 = string0.Length;

			while ((i2 = string0.IndexOf((char)167, i2 + 1)) != -1)
			{
				if (i2 < i3 - 1)
				{
					char c4 = string0[i2 + 1];
					if (func_50110_b(c4))
					{
						string1 = "\u00a7" + c4;
					}
					else if (func_50109_c(c4))
					{
						string1 = string1 + "\u00a7" + c4;
					}
				}
			}

			return string1;
		}

		/// <summary>
		/// Something to do with chat colors. This function takes the input string and replaces all parts of the string that match a regex. 
		/// Go to the definition of this method to find the Regex in question.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string func_52014_d(string str)
		{
			return chatColorRegex.Replace(str, "");
		}
	}

}