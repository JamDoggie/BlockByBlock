using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using net.minecraft.client.entity.render;
using net.minecraft.client.entity.render.model;
using BlockByBlock.net.minecraft.client.entity.render;

namespace net.minecraft.src
{

    public class RenderManager
	{
		private System.Collections.IDictionary entityRenderMap = new Hashtable();
		public static RenderManager instance = new RenderManager();
		private FontRenderer fontRenderer;
		public static double renderPosX;
		public static double renderPosY;
		public static double renderPosZ;
		public TextureManager renderEngine;
		public ItemRenderer itemRenderer;
		public World worldObj;
		public EntityLiving livingPlayer;
		public float playerViewY;
		public float playerViewX;
		public GameSettings options;
		public double field_1222_l;
		public double field_1221_m;
		public double field_1220_n;

		private RenderManager()
		{
			this.entityRenderMap[typeof(EntitySpider)] = new RenderSpider();
			this.entityRenderMap[typeof(EntityCaveSpider)] = new RenderSpider();
			this.entityRenderMap[typeof(EntityPig)] = new RenderPig(new ModelPig(), new ModelPig(0.5F), 0.7F);
			this.entityRenderMap[typeof(EntitySheep)] = new RenderSheep(new ModelSheep2(), new ModelSheep1(), 0.7F);
			this.entityRenderMap[typeof(EntityCow)] = new RenderCow(new ModelCow(), 0.7F);
			this.entityRenderMap[typeof(EntityMooshroom)] = new RenderMooshroom(new ModelCow(), 0.7F);
			this.entityRenderMap[typeof(EntityWolf)] = new RenderWolf(new ModelWolf(), 0.5F);
			this.entityRenderMap[typeof(EntityChicken)] = new RenderChicken(new ModelChicken(), 0.3F);
			this.entityRenderMap[typeof(EntityOcelot)] = new RenderOcelot(new ModelOcelot(), 0.4F);
			this.entityRenderMap[typeof(EntitySilverfish)] = new RenderSilverfish();
			this.entityRenderMap[typeof(EntityCreeper)] = new RenderCreeper();
			this.entityRenderMap[typeof(EntityEnderman)] = new RenderEnderman();
			this.entityRenderMap[typeof(EntitySnowman)] = new RenderSnowMan();
			this.entityRenderMap[typeof(EntitySkeleton)] = new RenderBiped(new ModelSkeleton(), 0.5F);
			this.entityRenderMap[typeof(EntityBlaze)] = new RenderBlaze();
			this.entityRenderMap[typeof(EntityZombie)] = new RenderBiped(new ModelZombie(), 0.5F);
			this.entityRenderMap[typeof(EntitySlime)] = new RenderSlime(new ModelSlime(16), new ModelSlime(0), 0.25F);
			this.entityRenderMap[typeof(EntityMagmaCube)] = new RenderMagmaCube();
			this.entityRenderMap[typeof(EntityPlayer)] = new RenderPlayer();
			this.entityRenderMap[typeof(EntityGiantZombie)] = new RenderGiantZombie(new ModelZombie(), 0.5F, 6.0F);
			this.entityRenderMap[typeof(EntityGhast)] = new RenderGhast();
			this.entityRenderMap[typeof(EntitySquid)] = new RenderSquid(new ModelSquid(), 0.7F);
			this.entityRenderMap[typeof(EntityVillager)] = new RenderVillager();
			this.entityRenderMap[typeof(EntityIronGolem)] = new RenderIronGolem();
			this.entityRenderMap[typeof(EntityLiving)] = new RenderLiving(new ModelBiped(), 0.5F);
			this.entityRenderMap[typeof(EntityDragon)] = new RenderDragon();
			this.entityRenderMap[typeof(EntityEnderCrystal)] = new RenderEnderCrystal();
			this.entityRenderMap[typeof(Entity)] = new RenderEntity();
			this.entityRenderMap[typeof(EntityPainting)] = new RenderPainting();
			this.entityRenderMap[typeof(EntityArrow)] = new RenderArrow();
			this.entityRenderMap[typeof(EntitySnowball)] = new RenderSnowball(Item.snowball.getIconFromDamage(0));
			this.entityRenderMap[typeof(EntityEnderPearl)] = new RenderSnowball(Item.enderPearl.getIconFromDamage(0));
			this.entityRenderMap[typeof(EntityEnderEye)] = new RenderSnowball(Item.eyeOfEnder.getIconFromDamage(0));
			this.entityRenderMap[typeof(EntityEgg)] = new RenderSnowball(Item.egg.getIconFromDamage(0));
			this.entityRenderMap[typeof(EntityPotion)] = new RenderSnowball(154);
			this.entityRenderMap[typeof(EntityExpBottle)] = new RenderSnowball(Item.expBottle.getIconFromDamage(0));
			this.entityRenderMap[typeof(EntityFireball)] = new RenderFireball(2.0F);
			this.entityRenderMap[typeof(EntitySmallFireball)] = new RenderFireball(0.5F);
			this.entityRenderMap[typeof(EntityItem)] = new RenderItem();
			this.entityRenderMap[typeof(EntityXPOrb)] = new RenderXPOrb();
			this.entityRenderMap[typeof(EntityTNTPrimed)] = new RenderTNTPrimed();
			this.entityRenderMap[typeof(EntityFallingSand)] = new RenderFallingSand();
			this.entityRenderMap[typeof(EntityMinecart)] = new RenderMinecart();
			this.entityRenderMap[typeof(EntityBoat)] = new RenderBoat();
			this.entityRenderMap[typeof(EntityFishHook)] = new RenderFish();
			this.entityRenderMap[typeof(EntityLightningBolt)] = new RenderLightningBolt();
			System.Collections.IEnumerator iterator1 = this.entityRenderMap.Values.GetEnumerator();

			while (iterator1.MoveNext())
			{
				Renderer render2 = (Renderer)iterator1.Current;
				render2.RenderManager = this;
			}

		}

		public virtual Renderer getEntityClassRenderObject(Type class1)
		{
			Renderer render2 = (Renderer)this.entityRenderMap[class1];
			if (render2 == null && class1 != typeof(Entity))
			{
				render2 = this.getEntityClassRenderObject(class1.BaseType);
				this.entityRenderMap[class1] = render2;
			}

			return render2;
		}

		public virtual Renderer getEntityRenderObject(Entity entity1)
		{
			return this.getEntityClassRenderObject(entity1.GetType());
		}

		public virtual void cacheActiveRenderInfo(World world1, TextureManager renderEngine2, FontRenderer fontRenderer3, EntityLiving entityLiving4, GameSettings gameSettings5, float f6)
		{
			this.worldObj = world1;
			this.renderEngine = renderEngine2;
			this.options = gameSettings5;
			this.livingPlayer = entityLiving4;
			this.fontRenderer = fontRenderer3;
			if (entityLiving4.PlayerSleeping)
			{
				int i7 = world1.getBlockId(MathHelper.floor_double(entityLiving4.posX), MathHelper.floor_double(entityLiving4.posY), MathHelper.floor_double(entityLiving4.posZ));
				if (i7 == Block.bed.blockID)
				{
					int i8 = world1.getBlockMetadata(MathHelper.floor_double(entityLiving4.posX), MathHelper.floor_double(entityLiving4.posY), MathHelper.floor_double(entityLiving4.posZ));
					int i9 = i8 & 3;
					this.playerViewY = (float)(i9 * 90 + 180);
					this.playerViewX = 0.0F;
				}
			}
			else
			{
				this.playerViewY = entityLiving4.prevRotationYaw + (entityLiving4.rotationYaw - entityLiving4.prevRotationYaw) * f6;
				this.playerViewX = entityLiving4.prevRotationPitch + (entityLiving4.rotationPitch - entityLiving4.prevRotationPitch) * f6;
			}

			if (gameSettings5.thirdPersonView == 2)
			{
				this.playerViewY += 180.0F;
			}

			this.field_1222_l = entityLiving4.lastTickPosX + (entityLiving4.posX - entityLiving4.lastTickPosX) * (double)f6;
			this.field_1221_m = entityLiving4.lastTickPosY + (entityLiving4.posY - entityLiving4.lastTickPosY) * (double)f6;
			this.field_1220_n = entityLiving4.lastTickPosZ + (entityLiving4.posZ - entityLiving4.lastTickPosZ) * (double)f6;
		}

		public virtual void renderEntity(Entity entity1, float f2)
		{
			double d3 = entity1.lastTickPosX + (entity1.posX - entity1.lastTickPosX) * (double)f2;
			double d5 = entity1.lastTickPosY + (entity1.posY - entity1.lastTickPosY) * (double)f2;
			double d7 = entity1.lastTickPosZ + (entity1.posZ - entity1.lastTickPosZ) * (double)f2;
			float f9 = entity1.prevRotationYaw + (entity1.rotationYaw - entity1.prevRotationYaw) * f2;
			int i10 = entity1.getBrightnessForRender(f2);
			if (entity1.Burning)
			{
				i10 = 15728880;
			}

			int i11 = i10 % 65536;
			int i12 = i10 / 65536;
			LightmapManager.setLightmapTextureCoords(LightmapManager.lightmapTexUnit, (float)i11 / 1.0F, (float)i12 / 1.0F);
            Minecraft.renderPipeline.SetColor(1.0F, 1.0F, 1.0F, 1.0F);
			this.renderEntityWithPosYaw(entity1, d3 - renderPosX, d5 - renderPosY, d7 - renderPosZ, f9, f2);
		}

		public virtual void renderEntityWithPosYaw(Entity entity1, double d2, double d4, double d6, float f8, float f9)
		{
			Renderer render10 = this.getEntityRenderObject(entity1);
			if (render10 != null)
			{
				render10.doRender(entity1, d2, d4, d6, f8, f9);
				render10.doRenderShadowAndFire(entity1, d2, d4, d6, f8, f9);
			}

		}

		public virtual void set(World world1)
		{
			this.worldObj = world1;
		}

		public virtual double getDistanceToCamera(double d1, double d3, double d5)
		{
			double d7 = d1 - this.field_1222_l;
			double d9 = d3 - this.field_1221_m;
			double d11 = d5 - this.field_1220_n;
			return d7 * d7 + d9 * d9 + d11 * d11;
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