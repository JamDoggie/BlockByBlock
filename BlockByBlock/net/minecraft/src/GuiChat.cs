using System;
using System.Collections;
using System.Text;

namespace net.minecraft.src
{

	using Keyboard = org.lwjgl.input.Keyboard;
	using Mouse = org.lwjgl.input.Mouse;

	public class GuiChat : GuiScreen
	{
		private string field_50062_b = "";
		private int field_50063_c = -1;
		private bool field_50060_d = false;
		private string field_50061_e = "";
		private string field_50059_f = "";
		private int field_50067_h = 0;
		private System.Collections.IList field_50068_i = new ArrayList();
		private URI field_50065_j = null;
		protected internal GuiTextField field_50064_a;
		private string field_50066_k = "";

		public GuiChat()
		{
		}

		public GuiChat(string string1)
		{
			this.field_50066_k = string1;
		}

		public override void initGui()
		{
			Keyboard.enableRepeatEvents(true);
			this.field_50063_c = this.mc.ingameGUI.func_50013_c().Count;
			this.field_50064_a = new GuiTextField(this.fontRenderer, 4, this.height - 12, this.width - 4, 12);
			this.field_50064_a.MaxStringLength = 100;
			this.field_50064_a.func_50027_a(false);
			this.field_50064_a.func_50033_b(true);
			this.field_50064_a.Text = this.field_50066_k;
			this.field_50064_a.func_50026_c(false);
		}

		public override void onGuiClosed()
		{
			Keyboard.enableRepeatEvents(false);
			this.mc.ingameGUI.func_50014_d();
		}

		public override void updateScreen()
		{
			this.field_50064_a.updateCursorCounter();
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			if (i2 == 15)
			{
				this.completePlayerName();
			}
			else
			{
				this.field_50060_d = false;
			}

			if (i2 == 1)
			{
				this.mc.displayGuiScreen((GuiScreen)null);
			}
			else if (i2 == 28)
			{
				string string3 = this.field_50064_a.Text.Trim();
				if (string3.Length > 0 && !this.mc.lineIsCommand(string3))
				{
					this.mc.thePlayer.sendChatMessage(string3);
				}

				this.mc.displayGuiScreen((GuiScreen)null);
			}
			else if (i2 == 200)
			{
				this.func_50058_a(-1);
			}
			else if (i2 == 208)
			{
				this.func_50058_a(1);
			}
			else if (i2 == 201)
			{
				this.mc.ingameGUI.func_50011_a(19);
			}
			else if (i2 == 209)
			{
				this.mc.ingameGUI.func_50011_a(-19);
			}
			else
			{
				this.field_50064_a.func_50037_a(c1, i2);
			}

		}

		public override void handleMouseInput()
		{
			base.handleMouseInput();
			int i1 = Mouse.getEventDWheel();
			if (i1 != 0)
			{
				if (i1 > 1)
				{
					i1 = 1;
				}

				if (i1 < -1)
				{
					i1 = -1;
				}

				if (!func_50049_m())
				{
					i1 *= 7;
				}

				this.mc.ingameGUI.func_50011_a(i1);
			}

		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			if (i3 == 0)
			{
				ChatClickData chatClickData4 = this.mc.ingameGUI.func_50012_a(Mouse.getX(), Mouse.getY());
				if (chatClickData4 != null)
				{
					URI uRI5 = chatClickData4.getURIFromChatLine();
					if (uRI5 != null)
					{
						this.field_50065_j = uRI5;
						this.mc.displayGuiScreen(new GuiChatConfirmLink(this, this, chatClickData4.func_50088_a(), 0, chatClickData4));
						return;
					}
				}
			}

			this.field_50064_a.mouseClicked(i1, i2, i3);
			base.mouseClicked(i1, i2, i3);
		}

		public override void confirmClicked(bool z1, int i2)
		{
			if (i2 == 0)
			{
				if (z1)
				{
					try
					{
						Type class3 = Type.GetType("java.awt.Desktop");
						object object4 = class3.GetMethod("getDesktop", new Type[0]).invoke((object)null, new object[0]);
						class3.GetMethod("browse", new Type[]{typeof(URI)}).invoke(object4, new object[]{this.field_50065_j});
					}
					catch (Exception throwable5)
					{
						Console.WriteLine(throwable5.ToString());
						Console.Write(throwable5.StackTrace);
					}
				}

				this.field_50065_j = null;
				this.mc.displayGuiScreen(this);
			}

		}

		public virtual void completePlayerName()
		{
			System.Collections.IEnumerator iterator2;
			GuiPlayerInfo guiPlayerInfo3;
			if (this.field_50060_d)
			{
				this.field_50064_a.func_50021_a(-1);
				if (this.field_50067_h >= this.field_50068_i.Count)
				{
					this.field_50067_h = 0;
				}
			}
			else
			{
				int i1 = this.field_50064_a.func_50028_c(-1);
				if (this.field_50064_a.func_50035_h() - i1 < 1)
				{
					return;
				}

				this.field_50068_i.Clear();
				this.field_50061_e = this.field_50064_a.Text.Substring(i1);
				this.field_50059_f = this.field_50061_e.ToLower();
				iterator2 = ((EntityClientPlayerMP)this.mc.thePlayer).sendQueue.playerNames.GetEnumerator();

				while (iterator2.MoveNext())
				{
					guiPlayerInfo3 = (GuiPlayerInfo)iterator2.Current;
					if (guiPlayerInfo3.nameStartsWith(this.field_50059_f))
					{
						this.field_50068_i.Add(guiPlayerInfo3);
					}
				}

				if (this.field_50068_i.Count == 0)
				{
					return;
				}

				this.field_50060_d = true;
				this.field_50067_h = 0;
				this.field_50064_a.func_50020_b(i1 - this.field_50064_a.func_50035_h());
			}

			if (this.field_50068_i.Count > 1)
			{
				StringBuilder stringBuilder4 = new StringBuilder();

				for (iterator2 = this.field_50068_i.GetEnumerator(); iterator2.MoveNext(); stringBuilder4.Append(guiPlayerInfo3.name))
				{
					guiPlayerInfo3 = (GuiPlayerInfo)iterator2.Current;
					if (stringBuilder4.Length > 0)
					{
						stringBuilder4.Append(", ");
					}
				}

				this.mc.ingameGUI.addChatMessage(stringBuilder4.ToString());
			}

			this.field_50064_a.func_50031_b(((GuiPlayerInfo)this.field_50068_i[this.field_50067_h++]).name);
		}

		public virtual void func_50058_a(int i1)
		{
			int i2 = this.field_50063_c + i1;
			int i3 = this.mc.ingameGUI.func_50013_c().Count;
			if (i2 < 0)
			{
				i2 = 0;
			}

			if (i2 > i3)
			{
				i2 = i3;
			}

			if (i2 != this.field_50063_c)
			{
				if (i2 == i3)
				{
					this.field_50063_c = i3;
					this.field_50064_a.Text = this.field_50062_b;
				}
				else
				{
					if (this.field_50063_c == i3)
					{
						this.field_50062_b = this.field_50064_a.Text;
					}

					this.field_50064_a.Text = (string)this.mc.ingameGUI.func_50013_c()[i2];
					this.field_50063_c = i2;
				}
			}
		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			drawRect(2, this.height - 14, this.width - 2, this.height - 2, int.MinValue);
			this.field_50064_a.drawTextBox();
			base.drawScreen(i1, i2, f3);
		}
	}

}