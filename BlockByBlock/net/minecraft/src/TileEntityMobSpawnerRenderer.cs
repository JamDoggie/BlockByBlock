using net.minecraft.client;
using OpenTK.Graphics.OpenGL;
using System.Collections;

namespace net.minecraft.src
{

	public class TileEntityMobSpawnerRenderer : TileEntitySpecialRenderer
	{
		private System.Collections.IDictionary entityHashMap = new Hashtable();

		public virtual void renderTileEntityMobSpawner(TileEntityMobSpawner tileEntityMobSpawner1, double d2, double d4, double d6, float f8)
		{
            Minecraft.newRenderer.ModelMatrix.PushMatrix();
            Minecraft.newRenderer.ModelMatrix.Translate((float)d2 + 0.5F, (float)d4, (float)d6 + 0.5F);
			Entity entity9 = (Entity)this.entityHashMap[tileEntityMobSpawner1.MobID];
			if (entity9 == null)
			{
				entity9 = EntityList.createEntityByName(tileEntityMobSpawner1.MobID, (World)null);
				this.entityHashMap[tileEntityMobSpawner1.MobID] = entity9;
			}

			if (entity9 != null)
			{
				entity9.World = tileEntityMobSpawner1.worldObj;
				float f10 = 0.4375F;
                Minecraft.newRenderer.ModelMatrix.Translate(0.0F, 0.4F, 0.0F);
                Minecraft.newRenderer.ModelMatrix.Rotate((float)(tileEntityMobSpawner1.yaw2 + (tileEntityMobSpawner1.yaw - tileEntityMobSpawner1.yaw2) * (double)f8) * 10.0F, 0.0F, 1.0F, 0.0F);
                Minecraft.newRenderer.ModelMatrix.Rotate(-30.0F, 1.0F, 0.0F, 0.0F);
                Minecraft.newRenderer.ModelMatrix.Translate(0.0F, -0.4F, 0.0F);
                Minecraft.newRenderer.ModelMatrix.Scale(f10, f10, f10);
				entity9.setLocationAndAngles(d2, d4, d6, 0.0F, 0.0F);
				RenderManager.instance.renderEntityWithPosYaw(entity9, 0.0D, 0.0D, 0.0D, 0.0F, f8);
			}

            Minecraft.newRenderer.ModelMatrix.PopMatrix();
		}

		public override void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8)
		{
			this.renderTileEntityMobSpawner((TileEntityMobSpawner)tileEntity1, d2, d4, d6, f8);
		}
	}

}