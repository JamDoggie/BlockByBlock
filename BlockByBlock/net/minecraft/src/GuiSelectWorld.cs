using System.Linq;
using System.Collections.Generic;

namespace net.minecraft.src
{

	public class GuiSelectWorld : GuiScreen
	{
        private readonly string dateFormat = "M/dd/y h:mm tt";
        protected internal GuiScreen parentScreen;
		protected internal string screenTitle = "Select world";
		private bool selected = false;
		private int selectedWorld;
		private List<SaveFormatComparator> saveList;
		private GuiWorldSlot worldSlotContainer;
		private string localizedWorldText;
		private string localizedMustConvertText;
		private string[] localizedGameModeText = new string[2];
		private bool deleting;
		private GuiButton buttonRename;
		private GuiButton buttonSelect;
		private GuiButton buttonDelete;

		public GuiSelectWorld(GuiScreen guiScreen1)
		{
			this.parentScreen = guiScreen1;
		}

		public override void initGui()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.screenTitle = stringTranslate1.translateKey("selectWorld.title");
			this.localizedWorldText = stringTranslate1.translateKey("selectWorld.world");
			this.localizedMustConvertText = stringTranslate1.translateKey("selectWorld.conversion");
			this.localizedGameModeText[0] = stringTranslate1.translateKey("gameMode.survival");
			this.localizedGameModeText[1] = stringTranslate1.translateKey("gameMode.creative");
			this.loadSaves();
			this.worldSlotContainer = new GuiWorldSlot(this);
			this.worldSlotContainer.registerScrollButtons(this.controlList, 4, 5);
			this.initButtons();
		}

		private void loadSaves()
		{
			ISaveFormat iSaveFormat1 = this.mc.SaveLoader;
			this.saveList = iSaveFormat1.SaveList;
			saveList.Sort();
            this.selectedWorld = -1;
		}

		protected internal virtual string getSaveFileName(int i1)
		{
			return (this.saveList[i1]).FileName;
		}

		protected internal virtual string getSaveName(int i1)
		{
			string string2 = (this.saveList[i1]).DisplayName;
			if (string.ReferenceEquals(string2, null) || MathHelper.stringNullOrLengthZero(string2))
			{
				StringTranslate stringTranslate3 = StringTranslate.Instance;
				string2 = stringTranslate3.translateKey("selectWorld.world") + " " + (i1 + 1);
			}

			return string2;
		}

		public virtual void initButtons()
		{
			StringTranslate stringTranslate1 = StringTranslate.Instance;
			this.controlList.Add(this.buttonSelect = new GuiButton(1, this.width / 2 - 154, this.height - 52, 150, 20, stringTranslate1.translateKey("selectWorld.select")));
			this.controlList.Add(this.buttonDelete = new GuiButton(6, this.width / 2 - 154, this.height - 28, 70, 20, stringTranslate1.translateKey("selectWorld.rename")));
			this.controlList.Add(this.buttonRename = new GuiButton(2, this.width / 2 - 74, this.height - 28, 70, 20, stringTranslate1.translateKey("selectWorld.delete")));
			this.controlList.Add(new GuiButton(3, this.width / 2 + 4, this.height - 52, 150, 20, stringTranslate1.translateKey("selectWorld.create")));
			this.controlList.Add(new GuiButton(0, this.width / 2 + 4, this.height - 28, 150, 20, stringTranslate1.translateKey("gui.cancel")));
			this.buttonSelect.enabled = false;
			this.buttonRename.enabled = false;
			this.buttonDelete.enabled = false;
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			if (guiButton1.enabled)
			{
				if (guiButton1.id == 2)
				{
					string string2 = this.getSaveName(this.selectedWorld);
					if (!string.ReferenceEquals(string2, null))
					{
						this.deleting = true;
						StringTranslate stringTranslate3 = StringTranslate.Instance;
						string string4 = stringTranslate3.translateKey("selectWorld.deleteQuestion");
						string string5 = "\'" + string2 + "\' " + stringTranslate3.translateKey("selectWorld.deleteWarning");
						string string6 = stringTranslate3.translateKey("selectWorld.deleteButton");
						string string7 = stringTranslate3.translateKey("gui.cancel");
						GuiYesNo guiYesNo8 = new GuiYesNo(this, string4, string5, string6, string7, this.selectedWorld);
						this.mc.displayGuiScreen(guiYesNo8);
					}
				}
				else if (guiButton1.id == 1)
				{
					this.selectWorld(this.selectedWorld);
				}
				else if (guiButton1.id == 3)
				{
					this.mc.displayGuiScreen(new GuiCreateWorld(this));
				}
				else if (guiButton1.id == 6)
				{
					this.mc.displayGuiScreen(new GuiRenameWorld(this, this.getSaveFileName(this.selectedWorld)));
				}
				else if (guiButton1.id == 0)
				{
					this.mc.displayGuiScreen(this.parentScreen);
				}
				else
				{
					this.worldSlotContainer.actionPerformed(guiButton1);
				}

			}
		}

		public virtual void selectWorld(int i1)
		{
			this.mc.displayGuiScreen((GuiScreen)null);
			if (!this.selected)
			{
				this.selected = true;
				int i2 = ((SaveFormatComparator)this.saveList[i1]).GameType;
				if (i2 == 0)
				{
					this.mc.playerController = new PlayerControllerSP(this.mc);
				}
				else
				{
					this.mc.playerController = new PlayerControllerCreative(this.mc);
				}

				string string3 = this.getSaveFileName(i1);
				if (string.ReferenceEquals(string3, null))
				{
					string3 = "World" + i1;
				}

				this.mc.startWorld(string3, this.getSaveName(i1), (WorldSettings)null);
				this.mc.displayGuiScreen((GuiScreen)null);
			}
		}

		public override void confirmClicked(bool z1, int i2)
		{
			if (this.deleting)
			{
				this.deleting = false;
				if (z1)
				{
					ISaveFormat iSaveFormat3 = this.mc.SaveLoader;
					iSaveFormat3.flushCache();
					iSaveFormat3.deleteWorldDirectory(this.getSaveFileName(i2));
					this.loadSaves();
				}

				this.mc.displayGuiScreen(this);
			}

		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.worldSlotContainer.drawScreen(i1, i2, f3);
			this.drawCenteredString(this.fontRenderer, this.screenTitle, this.width / 2, 20, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
		}

		internal static System.Collections.IList getSize(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.saveList;
		}

		internal static int onElementSelected(GuiSelectWorld guiSelectWorld0, int i1)
		{
			return guiSelectWorld0.selectedWorld = i1;
		}

		internal static int getSelectedWorld(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.selectedWorld;
		}

		internal static GuiButton getSelectButton(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.buttonSelect;
		}

		internal static GuiButton getRenameButton(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.buttonRename;
		}

		internal static GuiButton getDeleteButton(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.buttonDelete;
		}

		internal static string getLocalizedWorldName(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.localizedWorldText;
		}

		internal static string getDateFormat(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.dateFormat;
		}

		internal static string getLocalizedMustConvert(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.localizedMustConvertText;
		}

		internal static string[] getLocalizedGameMode(GuiSelectWorld guiSelectWorld0)
		{
			return guiSelectWorld0.localizedGameModeText;
		}
	}

}