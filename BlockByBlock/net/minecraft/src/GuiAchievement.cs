namespace net.minecraft.src
{
    using BlockByBlock.net.minecraft.render;
    using net.minecraft.client.entity.render;
    using OpenTK.Graphics.OpenGL;
    using Minecraft = net.minecraft.client.Minecraft;

    public class GuiAchievement : Gui
	{
		private Minecraft theGame;
		private int achievementWindowWidth;
		private int achievementWindowHeight;
		private string achievementGetLocalText;
		private string achievementStatName;
		private Achievement theAchievement;
		private long achievementTime;
		private RenderItem itemRender;
		private bool haveAchiement;

		public GuiAchievement(Minecraft minecraft1)
		{
			this.theGame = minecraft1;
			this.itemRender = new RenderItem();
		}

		public virtual void queueTakenAchievement(Achievement achievement1)
		{
			this.achievementGetLocalText = StatCollector.translateToLocal("achievement.get");
			this.achievementStatName = StatCollector.translateToLocal(achievement1.Name);
			this.achievementTime = DateTimeHelper.CurrentUnixTimeMillis();
			this.theAchievement = achievement1;
			this.haveAchiement = false;
		}

		public virtual void queueAchievementInformation(Achievement achievement1)
		{
			this.achievementGetLocalText = StatCollector.translateToLocal(achievement1.Name);
			this.achievementStatName = achievement1.Description;
			this.achievementTime = DateTimeHelper.CurrentUnixTimeMillis() - 2500L;
			this.theAchievement = achievement1;
			this.haveAchiement = true;
		}

		private void updateAchievementWindowScale()
		{
			GL.Viewport(0, 0, this.theGame.displayWidth, this.theGame.displayHeight);
			Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();
            Minecraft.renderPipeline.ModelMatrix.LoadIdentity();
			this.achievementWindowWidth = this.theGame.displayWidth;
			this.achievementWindowHeight = this.theGame.displayHeight;
			ScaledResolution scaledResolution1 = new ScaledResolution(this.theGame.gameSettings, this.theGame.displayWidth, this.theGame.displayHeight);
			this.achievementWindowWidth = scaledResolution1.ScaledWidth;
			this.achievementWindowHeight = scaledResolution1.ScaledHeight;
			GL.Clear(ClearBufferMask.DepthBufferBit);
            Minecraft.renderPipeline.ProjectionMatrix.LoadIdentity();
            Minecraft.renderPipeline.ProjectionMatrix.Ortho(0.0D, (double)this.achievementWindowWidth, (double)this.achievementWindowHeight, 0.0D, 1000.0D, 3000.0D);
            Minecraft.renderPipeline.ModelMatrix.LoadIdentity();
            Minecraft.renderPipeline.ModelMatrix.Translate(0.0F, 0.0F, -2000.0F);
		}

		public virtual void updateAchievementWindow()
		{
			if (this.theAchievement != null && this.achievementTime != 0L)
			{
				double d1 = (double)(DateTimeHelper.CurrentUnixTimeMillis() - this.achievementTime) / 3000.0D;
				if (this.haveAchiement || d1 >= 0.0D && d1 <= 1.0D)
				{
					this.updateAchievementWindowScale();
					GL.Disable(EnableCap.DepthTest);
					GL.DepthMask(false);
					double d3 = d1 * 2.0D;
					if (d3 > 1.0D)
					{
						d3 = 2.0D - d3;
					}

					d3 *= 4.0D;
					d3 = 1.0D - d3;
					if (d3 < 0.0D)
					{
						d3 = 0.0D;
					}

					d3 *= d3;
					d3 *= d3;
					int i5 = this.achievementWindowWidth - 160;
					int i6 = 0 - (int)(d3 * 36.0D);
					int i7 = this.theGame.renderEngine.getTexture("/achievement/bg.png");
                    Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
                    Minecraft.renderPipeline.SetState(RenderState.TextureState, true);
                    GL.BindTexture(TextureTarget.Texture2D, i7);
					Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
					this.drawTexturedModalRect(i5, i6, 96, 202, 160, 32);
					if (this.haveAchiement)
					{
						this.theGame.fontRenderer.drawSplitString(this.achievementStatName, i5 + 30, i6 + 7, 120, -1);
					}
					else
					{
						this.theGame.fontRenderer.drawString(this.achievementGetLocalText, i5 + 30, i6 + 7, -256);
						this.theGame.fontRenderer.drawString(this.achievementStatName, i5 + 30, i6 + 18, -1);
					}
                    
					GameLighting.EnableGUIStandardItemLighting();
					Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
					Minecraft.renderPipeline.SetState(RenderState.ColorMaterialState, true);
					Minecraft.renderPipeline.SetState(RenderState.LightingState, true);
					this.itemRender.renderItemIntoGUI(this.theGame.fontRenderer, this.theGame.renderEngine, this.theAchievement.theItemStack, i5 + 8, i6 + 8);
					Minecraft.renderPipeline.SetState(RenderState.LightingState, false);
					GL.DepthMask(true);
					GL.Enable(EnableCap.DepthTest);
				}
				else
				{
					this.achievementTime = 0L;
				}
			}
		}
	}

}