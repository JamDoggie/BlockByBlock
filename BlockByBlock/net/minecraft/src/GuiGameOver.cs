using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class GuiGameOver : GuiScreen
	{
		private int cooldownTimer;

		public override void initGui()
		{
			this.controlList.Clear();
			if (this.mc.theWorld.WorldInfo.HardcoreModeEnabled)
			{
				this.controlList.Add(new GuiButton(1, this.width / 2 - 100, this.height / 4 + 96, StatCollector.translateToLocal("deathScreen.deleteWorld")));
			}
			else
			{
				this.controlList.Add(new GuiButton(1, this.width / 2 - 100, this.height / 4 + 72, StatCollector.translateToLocal("deathScreen.respawn")));
				this.controlList.Add(new GuiButton(2, this.width / 2 - 100, this.height / 4 + 96, StatCollector.translateToLocal("deathScreen.titleScreen")));
				if (this.mc.session == null)
				{
					((GuiButton)this.controlList[1]).enabled = false;
				}
			}

			GuiButton guiButton2;
			for (System.Collections.IEnumerator iterator1 = this.controlList.GetEnumerator(); iterator1.MoveNext(); guiButton2.enabled = false)
			{
				guiButton2 = (GuiButton)iterator1.Current;
			}

		}

		protected internal override void keyTyped(char c1, int i2)
		{
		}

		protected internal override void actionPerformed(GuiButton guiButton1)
		{
			switch (guiButton1.id)
			{
			case 1:
				if (this.mc.theWorld.WorldInfo.HardcoreModeEnabled)
				{
					string string2 = this.mc.theWorld.SaveHandler.SaveDirectoryName;
					this.mc.exitToMainMenu("Deleting world");
					ISaveFormat iSaveFormat3 = this.mc.SaveLoader;
					iSaveFormat3.flushCache();
					iSaveFormat3.deleteWorldDirectory(string2);
					this.mc.displayGuiScreen(new GuiMainMenu());
				}
				else
				{
					this.mc.thePlayer.respawnPlayer();
					this.mc.displayGuiScreen((GuiScreen)null);
				}
				break;
			case 2:
				if (this.mc.MultiplayerWorld)
				{
					this.mc.theWorld.sendQuittingDisconnectingPacket();
				}

				this.mc.changeWorld1((World)null);
				this.mc.displayGuiScreen(new GuiMainMenu());
			break;
			}

		}

		public override void drawScreen(int i1, int i2, float f3)
		{
			this.drawGradientRect(0, 0, this.width, this.height, 1615855616, -1602211792);
            Minecraft.renderPipeline.ModelMatrix.PushMatrix();
            Minecraft.renderPipeline.ModelMatrix.Scale(2.0F, 2.0F, 2.0F);
			bool z4 = this.mc.theWorld.WorldInfo.HardcoreModeEnabled;
			string string5 = z4 ? StatCollector.translateToLocal("deathScreen.title.hardcore") : StatCollector.translateToLocal("deathScreen.title");
			this.drawCenteredString(this.fontRenderer, string5, this.width / 2 / 2, 30, 0xFFFFFF);
            Minecraft.renderPipeline.ModelMatrix.PopMatrix();
			if (z4)
			{
				this.drawCenteredString(this.fontRenderer, StatCollector.translateToLocal("deathScreen.hardcoreInfo"), this.width / 2, 144, 0xFFFFFF);
			}

			this.drawCenteredString(this.fontRenderer, StatCollector.translateToLocal("deathScreen.score") + ": \u00a7e" + this.mc.thePlayer.Score, this.width / 2, 100, 0xFFFFFF);
			base.drawScreen(i1, i2, f3);
		}

		public override bool doesGuiPauseGame()
		{
			return false;
		}

		public override void updateScreen()
		{
			base.updateScreen();
			++this.cooldownTimer;
			GuiButton guiButton2;
			if (this.cooldownTimer == 20)
			{
				for (System.Collections.IEnumerator iterator1 = this.controlList.GetEnumerator(); iterator1.MoveNext(); guiButton2.enabled = true)
				{
					guiButton2 = (GuiButton)iterator1.Current;
				}
			}

		}
	}

}