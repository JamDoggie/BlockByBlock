using net.minecraft.client;
using net.minecraft.client.entity;
using net.minecraft.client.entity.render;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;

namespace net.minecraft.src
{

    public class TileEntityRenderer
	{
		private System.Collections.IDictionary specialRendererMap = new Hashtable();
		public static TileEntityRenderer instance = new TileEntityRenderer();
		private FontRenderer fontRenderer;
		public static double staticPlayerX;
		public static double staticPlayerY;
		public static double staticPlayerZ;
		public TextureManager renderEngine;
		public World worldObj;
		public EntityLiving entityLivingPlayer;
		public float playerYaw;
		public float playerPitch;
		public double playerX;
		public double playerY;
		public double playerZ;

		private TileEntityRenderer()
		{
			this.specialRendererMap[typeof(TileEntitySign)] = new TileEntitySignRenderer();
			this.specialRendererMap[typeof(TileEntityMobSpawner)] = new TileEntityMobSpawnerRenderer();
			this.specialRendererMap[typeof(TileEntityPiston)] = new TileEntityRendererPiston();
			this.specialRendererMap[typeof(TileEntityChest)] = new TileEntityChestRenderer();
			this.specialRendererMap[typeof(TileEntityEnchantmentTable)] = new RenderEnchantmentTable();
			this.specialRendererMap[typeof(TileEntityEndPortal)] = new RenderEndPortal();
			System.Collections.IEnumerator iterator1 = this.specialRendererMap.Values.GetEnumerator();

			while (iterator1.MoveNext())
			{
				TileEntitySpecialRenderer tileEntitySpecialRenderer2 = (TileEntitySpecialRenderer)iterator1.Current;
				tileEntitySpecialRenderer2.TileEntityRenderer = this;
			}

		}

		public virtual TileEntitySpecialRenderer getSpecialRendererForClass(Type class1)
		{
			TileEntitySpecialRenderer tileEntitySpecialRenderer2 = (TileEntitySpecialRenderer)this.specialRendererMap[class1];
			if (tileEntitySpecialRenderer2 == null && class1 != typeof(TileEntity))
			{
				tileEntitySpecialRenderer2 = this.getSpecialRendererForClass(class1.BaseType);
				this.specialRendererMap[class1] = tileEntitySpecialRenderer2;
			}

			return tileEntitySpecialRenderer2;
		}

		public virtual bool hasSpecialRenderer(TileEntity tileEntity1)
		{
			return this.getSpecialRendererForEntity(tileEntity1) != null;
		}

		public virtual TileEntitySpecialRenderer getSpecialRendererForEntity(TileEntity tileEntity1)
		{
			return tileEntity1 == null ? null : this.getSpecialRendererForClass(tileEntity1.GetType());
		}

		public virtual void cacheActiveRenderInfo(World world1, TextureManager renderEngine2, FontRenderer fontRenderer3, EntityLiving entityLiving4, float f5)
		{
			if (this.worldObj != world1)
			{
				this.cacheSpecialRenderInfo(world1);
			}

			this.renderEngine = renderEngine2;
			this.entityLivingPlayer = entityLiving4;
			this.fontRenderer = fontRenderer3;
			this.playerYaw = entityLiving4.prevRotationYaw + (entityLiving4.rotationYaw - entityLiving4.prevRotationYaw) * f5;
			this.playerPitch = entityLiving4.prevRotationPitch + (entityLiving4.rotationPitch - entityLiving4.prevRotationPitch) * f5;
			this.playerX = entityLiving4.lastTickPosX + (entityLiving4.posX - entityLiving4.lastTickPosX) * (double)f5;
			this.playerY = entityLiving4.lastTickPosY + (entityLiving4.posY - entityLiving4.lastTickPosY) * (double)f5;
			this.playerZ = entityLiving4.lastTickPosZ + (entityLiving4.posZ - entityLiving4.lastTickPosZ) * (double)f5;
		}

		public virtual void func_40742_a()
		{
		}

		public virtual void renderTileEntity(TileEntity tileEntity1, float f2)
		{
			if (tileEntity1.getDistanceFrom(this.playerX, this.playerY, this.playerZ) < 4096.0D)
			{
				int i3 = this.worldObj.GetLightBrightnessForSkyBlocks(tileEntity1.xCoord, tileEntity1.yCoord, tileEntity1.zCoord, 0);
				int i4 = i3 % 65536;
				int i5 = i3 / 65536;
				LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, (float)i4 / 1.0F, (float)i5 / 1.0F);
                Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
				this.renderTileEntityAt(tileEntity1, (double)tileEntity1.xCoord - staticPlayerX, (double)tileEntity1.yCoord - staticPlayerY, (double)tileEntity1.zCoord - staticPlayerZ, f2);
			}

		}

		public virtual void renderTileEntityAt(TileEntity tileEntity1, double d2, double d4, double d6, float f8)
		{
			TileEntitySpecialRenderer tileEntitySpecialRenderer9 = this.getSpecialRendererForEntity(tileEntity1);
			if (tileEntitySpecialRenderer9 != null)
			{
				tileEntitySpecialRenderer9.renderTileEntityAt(tileEntity1, d2, d4, d6, f8);
			}

		}

		public virtual void cacheSpecialRenderInfo(World world1)
		{
			this.worldObj = world1;
			System.Collections.IEnumerator iterator2 = this.specialRendererMap.Values.GetEnumerator();

			while (iterator2.MoveNext())
			{
				TileEntitySpecialRenderer tileEntitySpecialRenderer3 = (TileEntitySpecialRenderer)iterator2.Current;
				if (tileEntitySpecialRenderer3 != null)
				{
					tileEntitySpecialRenderer3.cacheSpecialRenderInfo(world1);
				}
			}

		}

		public virtual FontRenderer FontRenderer
		{
			get
			{
				return this.fontRenderer;
			}
		}
	}

}