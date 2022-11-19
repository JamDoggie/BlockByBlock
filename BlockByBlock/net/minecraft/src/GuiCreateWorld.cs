using System;
using System.Text.RegularExpressions;

namespace net.minecraft.src
{

	public class GuiCreateWorld : GuiScreen
	{
		private GuiScreen parentGuiScreen;
		private GuiTextField textboxWorldName;
		private GuiTextField textboxSeed;
		private string folderName;
		private string gameMode = "survival";
		private bool field_35365_g = true;
		private bool field_40232_h = false;
		private bool createClicked;
		private bool moreOptions;
		private GuiButton gameModeButton;
		private GuiButton moreWorldOptions;
		private GuiButton generateStructuresButton;
		private GuiButton worldTypeButton;
		private string gameModeDescriptionLine1;
		private string gameModeDescriptionLine2;
		private string seed;
		private string localizedNewWorldText;
		private int field_46030_z = 0;

		public GuiCreateWorld(GuiScreen guiScreen1)
		{
			this.parentGuiScreen = guiScreen1;
			this.seed = "";
			this.localizedNewWorldText = StatCollector.translateToLocal("selectWorld.newWorld");
		}

		public override void updateScreen()
		{
			this.textboxWorldName.updateCursorCounter();
			this.textboxSeed.updateCursorCounter();
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			mc.mcApplet.EnableKeyRepeatingEvents(true);
			this.controlList.Clear();
			this.controlList.Add(new GuiButton(0, this.width / 2 - 155, this.height - 28, 150, 20, stringTranslate1.translateKey("selectWorld.create")));
			this.controlList.Add(new GuiButton(1, this.width / 2 + 5, this.height - 28, 150, 20, stringTranslate1.translateKey("gui.cancel")));
			this.controlList.Add(this.gameModeButton = new GuiButton(2, this.width / 2 - 75, 100, 150, 20, stringTranslate1.translateKey("selectWorld.gameMode")));
			this.controlList.Add(this.moreWorldOptions = new GuiButton(3, this.width / 2 - 75, 172, 150, 20, stringTranslate1.translateKey("selectWorld.moreWorldOptions")));
			this.controlList.Add(this.generateStructuresButton = new GuiButton(4, this.width / 2 - 155, 100, 150, 20, stringTranslate1.translateKey("selectWorld.mapFeatures")));
			this.generateStructuresButton.shouldDrawButton = false;
			this.controlList.Add(this.worldTypeButton = new GuiButton(5, this.width / 2 + 5, 100, 150, 20, stringTranslate1.translateKey("selectWorld.mapType")));
			this.worldTypeButton.shouldDrawButton = false;
			this.textboxWorldName = new GuiTextField(this.fontRenderer, this.width / 2 - 100, 60, 200, 20);
			this.textboxWorldName.setFocused(true);
			this.textboxWorldName.Text = this.localizedNewWorldText;
			this.textboxSeed = new GuiTextField(this.fontRenderer, this.width / 2 - 100, 60, 200, 20);
			this.textboxSeed.Text = this.seed;
			this.makeUseableName();
			this.func_35363_g();
		}

		private void makeUseableName()
		{
			this.folderName = this.textboxWorldName.Text.Trim();
			char[] c1 = ChatAllowedCharacters.allowedCharactersArray;
			int i2 = c1.Length;

			for (int i3 = 0; i3 < i2; ++i3)
			{
				char c4 = c1[i3];
				this.folderName = this.folderName.Replace(c4, '_');
			}

			if (MathHelper.stringNullOrLengthZero(this.folderName))
			{
				this.folderName = "World";
			}

			this.folderName = func_25097_a(this.mc.SaveLoader, this.folderName);
		}

		private void func_35363_g()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.gameModeButton.displayString = stringTranslate1.translateKey("selectWorld.gameMode") + " " + stringTranslate1.translateKey("selectWorld.gameMode." + this.gameMode);
			this.gameModeDescriptionLine1 = stringTranslate1.translateKey("selectWorld.gameMode." + this.gameMode + ".line1");
			this.gameModeDescriptionLine2 = stringTranslate1.translateKey("selectWorld.gameMode." + this.gameMode + ".line2");
			this.generateStructuresButton.displayString = stringTranslate1.translateKey("selectWorld.mapFeatures") + " ";
			if (this.field_35365_g)
			{
				this.generateStructuresButton.displayString = this.generateStructuresButton.displayString + stringTranslate1.translateKey("options.on");
			}
			else
			{
				this.generateStructuresButton.displayString = this.generateStructuresButton.displayString + stringTranslate1.translateKey("options.off");
			}

			this.worldTypeButton.displayString = stringTranslate1.translateKey("selectWorld.mapType") + " " + stringTranslate1.translateKey(WorldType.worldTypes[this.field_46030_z].TranslateName);
		}

		static Regex specialCharactersRegex = new Regex("[\\./\"]|COM");

		public static string func_25097_a(ISaveFormat iSaveFormat0, string string1)
		{
			
			for (string1 = specialCharactersRegex.Replace(string1, "_"); iSaveFormat0.getWorldInfo(string1) != null; string1 = string1 + "-")
			{
			}

			return string1;
		}

		public override void onGuiClosed()
		{
			mc.mcApplet.EnableKeyRepeatingEvents(false);
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 1)
				{
					this.mc.displayGuiScreen(this.parentGuiScreen);
				}
				else if (guiButton1.id == 0)
				{
					this.mc.displayGuiScreen((GuiScreen)null);
					if (this.createClicked)
					{
						return;
					}

					this.createClicked = true;
					long j2 = (new Random()).NextInt64();
					string string4 = this.textboxSeed.Text;
					if (!MathHelper.stringNullOrLengthZero(string4))
					{
						try
						{
							long j5 = long.Parse(string4);
							if (j5 != 0L)
							{
								j2 = j5;
							}
						}
						catch (System.FormatException)
						{
							j2 = (long)string4.GetHashCode();
						}
					}

					sbyte b9 = 0;
					if (this.gameMode.Equals("creative"))
					{
						b9 = 1;
						this.mc.playerController = new PlayerControllerCreative(this.mc);
					}
					else
					{
						this.mc.playerController = new PlayerControllerSP(this.mc);
					}

					this.mc.startWorld(this.folderName, this.textboxWorldName.Text, new WorldSettings(j2, b9, this.field_35365_g, this.field_40232_h, WorldType.worldTypes[this.field_46030_z]));
					this.mc.displayGuiScreen((GuiScreen)null);
				}
				else if (guiButton1.id == 3)
				{
					this.moreOptions = !this.moreOptions;
					this.gameModeButton.shouldDrawButton = !this.moreOptions;
					this.generateStructuresButton.shouldDrawButton = this.moreOptions;
					this.worldTypeButton.shouldDrawButton = this.moreOptions;
					StringTranslate stringTranslate8;
					if (this.moreOptions)
					{
						stringTranslate8 = StringTranslate.Instance;
						this.moreWorldOptions.displayString = stringTranslate8.translateKey("gui.done");
					}
					else
					{
						stringTranslate8 = StringTranslate.Instance;
						this.moreWorldOptions.displayString = stringTranslate8.translateKey("selectWorld.moreWorldOptions");
					}
				}
				else if (guiButton1.id == 2)
				{
					if (this.gameMode.Equals("survival"))
					{
						this.field_40232_h = false;
						this.gameMode = "hardcore";
						this.field_40232_h = true;
						this.func_35363_g();
					}
					else if (this.gameMode.Equals("hardcore"))
					{
						this.field_40232_h = false;
						this.gameMode = "creative";
						this.func_35363_g();
						this.field_40232_h = false;
					}
					else
					{
						this.gameMode = "survival";
						this.func_35363_g();
						this.field_40232_h = false;
					}

					this.func_35363_g();
				}
				else if (guiButton1.id == 4)
				{
					this.field_35365_g = !this.field_35365_g;
					this.func_35363_g();
				}
				else if (guiButton1.id == 5)
				{
					++this.field_46030_z;
					if (this.field_46030_z >= WorldType.worldTypes.Length)
					{
						this.field_46030_z = 0;
					}

					while (WorldType.worldTypes[this.field_46030_z] == null || !WorldType.worldTypes[this.field_46030_z].CanBeCreated)
					{
						++this.field_46030_z;
						if (this.field_46030_z >= WorldType.worldTypes.Length)
						{
							this.field_46030_z = 0;
						}
					}

					this.func_35363_g();
				}

			}
		}

		protected internal override void keyTyped(char c1, int i2)
		{
			if (this.textboxWorldName.func_50025_j() && !this.moreOptions)
			{
				this.textboxWorldName.keyTyped(c1, i2);
				this.localizedNewWorldText = this.textboxWorldName.Text;
			}
			else if (this.textboxSeed.func_50025_j() && this.moreOptions)
			{
				this.textboxSeed.keyTyped(c1, i2);
				this.seed = this.textboxSeed.Text;
			}

			if (c1 == (char)13)
			{
				this.actionPerformed((GuiButton)this.controlList[0]);
			}

			((GuiButton)this.controlList[0]).enabled = this.textboxWorldName.Text.Length > 0;
			this.makeUseableName();
		}

		protected internal override void mouseClicked(int i1, int i2, int i3)
		{
			base.mouseClicked(i1, i2, i3);
			if (!this.moreOptions)
			{
				this.textboxWorldName.mouseClicked(i1, i2, i3);
			}
			else
			{
				this.textboxSeed.mouseClicked(i1, i2, i3);
			}

		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			StringTranslate stringTranslate4 = StringTranslate.Instance;
			this.drawDefaultBackground();
			this.drawCenteredString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.create"), this.width / 2, 20, 0xFFFFFF);
			if (!this.moreOptions)
			{
				this.drawString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.enterName"), this.width / 2 - 100, 47, 10526880);
				this.drawString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.resultFolder") + " " + this.folderName, this.width / 2 - 100, 85, 10526880);
				this.textboxWorldName.drawTextBox();
				this.drawString(this.fontRenderer, this.gameModeDescriptionLine1, this.width / 2 - 100, 122, 10526880);
				this.drawString(this.fontRenderer, this.gameModeDescriptionLine2, this.width / 2 - 100, 134, 10526880);
			}
			else
			{
				this.drawString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.enterSeed"), this.width / 2 - 100, 47, 10526880);
				this.drawString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.seedInfo"), this.width / 2 - 100, 85, 10526880);
				this.drawString(this.fontRenderer, stringTranslate4.translateKey("selectWorld.mapFeatures.info"), this.width / 2 - 150, 122, 10526880);
				this.textboxSeed.drawTextBox();
			}

			base.drawScreen(i1, i2, f3);
		}
	}

}