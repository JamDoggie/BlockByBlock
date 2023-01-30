using BlockByBlock.net.minecraft.render;
using net.minecraft.client;
using OpenTK.Graphics.OpenGL;
using System;

namespace net.minecraft.src
{
	public class GuiTextField : Gui
	{
		private readonly FontRenderer fontRenderer;
		private readonly int xPos;
		private readonly int yPos;
		private readonly int width;
		private readonly int height;
		private string text = "";
		private int maxStringLength = 32;
		private int cursorCounter;
		private bool field_50044_j = true;
		private bool field_50045_k = true;
		private bool isFocused = false;
		private bool field_50043_m = true;
		private int field_50041_n = 0;
		private int field_50042_o = 0;
		private int field_50048_p = 0;
		private int field_50047_q = 14737632;
		private int field_50046_r = 7368816;

		public GuiTextField(FontRenderer fontRenderer1, int xPos, int yPos, int width, int height)
		{
			this.fontRenderer = fontRenderer1;
			this.xPos = xPos;
			this.yPos = yPos;
			this.width = width;
			this.height = height;
		}

		public virtual void updateCursorCounter()
		{
			++this.cursorCounter;
		}

		public virtual string Text
		{
			set
			{
				if (value.Length > this.maxStringLength)
				{
					this.text = value.Substring(0, this.maxStringLength);
				}
				else
				{
					this.text = value;
				}
    
				this.func_50038_e();
			}
			get
			{
				return this.text;
			}
		}


		public virtual string getSelectedText()
		{
			int i1 = this.field_50042_o < this.field_50048_p ? this.field_50042_o : this.field_50048_p;
			int i2 = this.field_50042_o < this.field_50048_p ? this.field_50048_p : this.field_50042_o;
			return this.text.Substring(i1, i2 - i1);
		}

		public virtual void writeText(string string1)
		{
			string string2 = "";
			string string3 = ChatAllowedCharacters.func_52019_a(string1);
			int i4 = this.field_50042_o < this.field_50048_p ? this.field_50042_o : this.field_50048_p;
			int i5 = this.field_50042_o < this.field_50048_p ? this.field_50048_p : this.field_50042_o;
			int i6 = this.maxStringLength - this.text.Length - (i4 - this.field_50048_p);
			bool z7 = false;
			if (this.text.Length > 0)
			{
				string2 = string2 + this.text.Substring(0, i4);
			}

			int i8;
			if (i6 < string3.Length)
			{
				string2 = string2 + string3.Substring(0, i6);
				i8 = i6;
			}
			else
			{
				string2 = string2 + string3;
				i8 = string3.Length;
			}

			if (this.text.Length > 0 && i5 < this.text.Length)
			{
				string2 = string2 + this.text.Substring(i5);
			}

			this.text = string2;
			this.func_50023_d(i4 - this.field_50048_p + i8);
		}

		public virtual void func_50021_a(int i1)
		{
			if (this.text.Length != 0)
			{
				if (this.field_50048_p != this.field_50042_o)
				{
					this.writeText("");
				}
				else
				{
					this.deleteFromCursor(this.getNthWordFromCursor(i1) - this.field_50042_o);
				}
			}
		}

		public virtual void deleteFromCursor(int i1)
		{
			if (this.text.Length != 0)
			{
				if (this.field_50048_p != this.field_50042_o)
				{
					this.writeText("");
				}
				else
				{
					bool z2 = i1 < 0;
					int i3 = z2 ? this.field_50042_o + i1 : this.field_50042_o;
					int i4 = z2 ? this.field_50042_o : this.field_50042_o + i1;
					string string5 = "";
					if (i3 >= 0)
					{
						string5 = this.text.Substring(0, i3);
					}

					if (i4 < this.text.Length)
					{
						string5 = string5 + this.text.Substring(i4);
					}

					this.text = string5;
					if (z2)
					{
						this.func_50023_d(i1);
					}

				}
			}
		}

		public virtual int getNthWordFromCursor(int i1)
		{
			return this.getNthWordFromPos(i1, this.getCursorPosition());
		}

		public virtual int getNthWordFromPos(int i1, int i2)
		{
			int i3 = i2;
			bool z4 = i1 < 0;
			int i5 = Math.Abs(i1);

			for (int i6 = 0; i6 < i5; ++i6)
			{
				if (!z4)
				{
					int i7 = this.text.Length;
					i3 = this.text.IndexOf((char)32, i3);
					if (i3 == -1)
					{
						i3 = i7;
					}
					else
					{
						while (i3 < i7 && this.text[i3] == (char)32)
						{
							++i3;
						}
					}
				}
				else
				{
					while (i3 > 0 && this.text[i3 - 1] == (char)32)
					{
						--i3;
					}

					while (i3 > 0 && this.text[i3 - 1] != (char)32)
					{
						--i3;
					}
				}
			}

			return i3;
		}

		public virtual void func_50023_d(int i1)
		{
			this.func_50030_e(this.field_50048_p + i1);
		}

		public virtual void func_50030_e(int i1)
		{
			this.field_50042_o = i1;
			int i2 = this.text.Length;
			if (this.field_50042_o < 0)
			{
				this.field_50042_o = 0;
			}

			if (this.field_50042_o > i2)
			{
				this.field_50042_o = i2;
			}

			this.func_50032_g(this.field_50042_o);
		}

		public virtual void func_50034_d()
		{
			this.func_50030_e(0);
		}

		public virtual void func_50038_e()
		{
			this.func_50030_e(this.text.Length);
		}

		public virtual bool keyTyped(char c1, int i2)
		{
			if (this.field_50043_m && this.isFocused)
			{
				switch (c1)
				{
				case '\u0001':
					this.func_50038_e();
					this.func_50032_g(0);
					return true;
				case '\u0003':
					GuiScreen.setClipboardString(this.getSelectedText());
					return true;
				case '\u0016':
					this.writeText(GuiScreen.ClipboardString);
					return true;
				case '\u0018':
					GuiScreen.setClipboardString(this.getSelectedText());
					this.writeText("");
					return true;
				default:
					switch (i2)
					{
					case 14:
						if (GuiScreen.isControlDown())
						{
							this.func_50021_a(-1);
						}
						else
						{
							this.deleteFromCursor(-1);
						}

						return true;
					case 199:
						if (GuiScreen.isShiftDown())
						{
							this.func_50032_g(0);
						}
						else
						{
							this.func_50034_d();
						}

						return true;
					case 203:
						if (GuiScreen.isShiftDown())
						{
							if (GuiScreen.isControlDown())
							{
								this.func_50032_g(this.getNthWordFromPos(-1, this.func_50036_k()));
							}
							else
							{
								this.func_50032_g(this.func_50036_k() - 1);
							}
						}
						else if (GuiScreen.isControlDown())
						{
							this.func_50030_e(this.getNthWordFromCursor(-1));
						}
						else
						{
							this.func_50023_d(-1);
						}

						return true;
					case 205:
						if (GuiScreen.isShiftDown())
						{
							if (GuiScreen.isControlDown())
							{
								this.func_50032_g(this.getNthWordFromPos(1, this.func_50036_k()));
							}
							else
							{
								this.func_50032_g(this.func_50036_k() + 1);
							}
						}
						else if (GuiScreen.isControlDown())
						{
							this.func_50030_e(this.getNthWordFromCursor(1));
						}
						else
						{
							this.func_50023_d(1);
						}

						return true;
					case 207:
						if (GuiScreen.isShiftDown())
						{
							this.func_50032_g(this.text.Length);
						}
						else
						{
							this.func_50038_e();
						}

						return true;
					case 211:
						if (GuiScreen.isControlDown())
						{
							this.func_50021_a(1);
						}
						else
						{
							this.deleteFromCursor(1);
						}

						return true;
					default:
						if (ChatAllowedCharacters.isAllowedCharacter(c1))
						{
							this.writeText(Convert.ToString(c1));
							return true;
						}
						else
						{
							return false;
						}
					}
				break;
				}
			}
			else
			{
				return false;
			}
		}

		public virtual void mouseClicked(int i1, int i2, int i3)
		{
			bool z4 = i1 >= this.xPos && i1 < this.xPos + this.width && i2 >= this.yPos && i2 < this.yPos + this.height;
			if (this.field_50045_k)
			{
				this.setFocused(this.field_50043_m && z4);
			}

			if (this.isFocused && i3 == 0)
			{
				int i5 = i1 - this.xPos;
				if (this.field_50044_j)
				{
					i5 -= 4;
				}

				string string6 = this.fontRenderer.func_50107_a(this.text.Substring(this.field_50041_n), this.func_50019_l());
				this.func_50030_e(this.fontRenderer.func_50107_a(string6, i5).Length + this.field_50041_n);
			}

		}

		public virtual void drawTextBox()
		{
			if (this.func_50022_i())
			{
				drawRect(this.xPos - 1, this.yPos - 1, this.xPos + this.width + 1, this.yPos + this.height + 1, -6250336);
				drawRect(this.xPos, this.yPos, this.xPos + this.width, this.yPos + this.height, unchecked((int)0xFF000000));
			}

			int i1 = this.field_50043_m ? this.field_50047_q : this.field_50046_r;
			int i2 = this.field_50042_o - this.field_50041_n;
			int i3 = this.field_50048_p - this.field_50041_n;
			string string4 = this.fontRenderer.func_50107_a(this.text.Substring(this.field_50041_n), this.func_50019_l());
			bool z5 = i2 >= 0 && i2 <= string4.Length;
			bool z6 = this.isFocused && this.cursorCounter / 6 % 2 == 0 && z5;
			int i7 = this.field_50044_j ? this.xPos + 4 : this.xPos;
			int i8 = this.field_50044_j ? this.yPos + (this.height - 8) / 2 : this.yPos;
			int i9 = i7;
			if (i3 > string4.Length)
			{
				i3 = string4.Length;
			}

			if (string4.Length > 0)
			{
				string string10 = z5 ? string4.Substring(0, i2) : string4;
				i9 = this.fontRenderer.drawStringWithShadow(string10, i7, i8, i1);
			}

			bool z13 = this.field_50042_o < this.text.Length || this.text.Length >= this.func_50040_g();
			int i11 = i9;
			if (!z5)
			{
				i11 = i2 > 0 ? i7 + this.width : i7;
			}
			else if (z13)
			{
				i11 = i9 - 1;
				--i9;
			}

			if (string4.Length > 0 && z5 && i2 < string4.Length)
			{
				this.fontRenderer.drawStringWithShadow(string4.Substring(i2), i9, i8, i1);
			}

			if (z6)
			{
				if (z13)
				{
					Gui.drawRect(i11, i8 - 1, i11 + 1, i8 + 1 + this.fontRenderer.FONT_HEIGHT, -3092272);
				}
				else
				{
					this.fontRenderer.drawStringWithShadow("_", i11, i8, i1);
				}
			}

			if (i3 != i2)
			{
				int i12 = i7 + this.fontRenderer.getStringWidth(string4.Substring(0, i3));
				this.func_50029_c(i11, i8 - 1, i12 - 1, i8 + 1 + this.fontRenderer.FONT_HEIGHT);
			}

		}

		private void func_50029_c(int i1, int i2, int i3, int i4)
		{
			int i5;
			if (i1 < i3)
			{
				i5 = i1;
				i1 = i3;
				i3 = i5;
			}

			if (i2 < i4)
			{
				i5 = i2;
				i2 = i4;
				i4 = i5;
			}

			Tessellator tessellator6 = Tessellator.instance;
            Minecraft.renderPipeline.SetColor(0.0F, 0.0F, 255.0F, 255.0F);
            Minecraft.renderPipeline.SetState(RenderState.TextureState, false);
            GL.Enable(EnableCap.ColorLogicOp);
			GL.LogicOp(LogicOp.OrReverse);
			tessellator6.startDrawingQuads();
			tessellator6.AddVertex((double)i1, (double)i4, 0.0D);
			tessellator6.AddVertex((double)i3, (double)i4, 0.0D);
			tessellator6.AddVertex((double)i3, (double)i2, 0.0D);
			tessellator6.AddVertex((double)i1, (double)i2, 0.0D);
			tessellator6.DrawImmediate();
			GL.Disable(EnableCap.ColorLogicOp);
			Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
		}

		public virtual int MaxStringLength
		{
			set
			{
				this.maxStringLength = value;
				if (this.text.Length > value)
				{
					this.text = this.text.Substring(0, value);
				}
    
			}
		}

		public virtual int func_50040_g()
		{
			return this.maxStringLength;
		}

		public virtual int getCursorPosition()
		{
			return this.field_50042_o;
		}

		public virtual bool func_50022_i()
		{
			return this.field_50044_j;
		}

		public virtual void func_50027_a(bool z1)
		{
			this.field_50044_j = z1;
		}

		public virtual void setFocused(bool z1)
		{
			if (z1 && !this.isFocused)
			{
				this.cursorCounter = 0;
			}

			this.isFocused = z1;
		}

		public virtual bool func_50025_j()
		{
			return this.isFocused;
		}

		public virtual int func_50036_k()
		{
			return this.field_50048_p;
		}

		public virtual int func_50019_l()
		{
			return this.func_50022_i() ? this.width - 8 : this.width;
		}

		public virtual void func_50032_g(int i1)
		{
			int i2 = this.text.Length;
			if (i1 > i2)
			{
				i1 = i2;
			}

			if (i1 < 0)
			{
				i1 = 0;
			}

			this.field_50048_p = i1;
			if (this.fontRenderer != null)
			{
				if (this.field_50041_n > i2)
				{
					this.field_50041_n = i2;
				}

				int i3 = this.func_50019_l();
				string string4 = this.fontRenderer.func_50107_a(this.text.Substring(this.field_50041_n), i3);
				int i5 = string4.Length + this.field_50041_n;
				if (i1 == this.field_50041_n)
				{
					this.field_50041_n -= this.fontRenderer.func_50104_a(this.text, i3, true).Length;
				}

				if (i1 > i5)
				{
					this.field_50041_n += i1 - i5;
				}
				else if (i1 <= this.field_50041_n)
				{
					this.field_50041_n -= this.field_50041_n - i1;
				}

				if (this.field_50041_n < 0)
				{
					this.field_50041_n = 0;
				}

				if (this.field_50041_n > i2)
				{
					this.field_50041_n = i2;
				}
			}

		}

		public virtual void func_50026_c(bool z1)
		{
			this.field_50045_k = z1;
		}
	}

}