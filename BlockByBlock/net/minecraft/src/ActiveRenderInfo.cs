using BlockByBlock.helpers;
using net.minecraft.client;
using net.minecraft.client.entity;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

    public class ActiveRenderInfo
	{
		public static float objectX = 0.0F;
		public static float objectY = 0.0F;
		public static float objectZ = 0.0F;
		private static int[] viewport = new int[16];
		private static float[] objectCoords = new float[3];
		public static float rotationX;
		public static float rotationXZ;
		public static float rotationZ;
		public static float rotationYZ;
		public static float rotationXY;

		public static void updateRenderInfo(EntityPlayer entityPlayer0, bool z1)
		{
			GL.GetInteger(GetPName.Viewport, viewport);
			float f2 = (float)((viewport[0] + viewport[2]) / 2);
			float f3 = (float)((viewport[1] + viewport[3]) / 2);
			Glu.UnProject(f2, f3, 0.0F, Minecraft.renderPipeline.ModelMatrix.GetMatrix(), Minecraft.renderPipeline.ProjectionMatrix.GetMatrix(), viewport, objectCoords);
			objectX = objectCoords[0];
			objectY = objectCoords[1];
			objectZ = objectCoords[2];
			int i4 = z1 ? 1 : 0;
			float f5 = entityPlayer0.rotationPitch;
			float f6 = entityPlayer0.rotationYaw;
			rotationX = MathHelper.cos(f6 * (float)Math.PI / 180.0F) * (float)(1 - i4 * 2);
			rotationZ = MathHelper.sin(f6 * (float)Math.PI / 180.0F) * (float)(1 - i4 * 2);
			rotationYZ = -rotationZ * MathHelper.sin(f5 * (float)Math.PI / 180.0F) * (float)(1 - i4 * 2);
			rotationXY = rotationX * MathHelper.sin(f5 * (float)Math.PI / 180.0F) * (float)(1 - i4 * 2);
			rotationXZ = MathHelper.cos(f5 * (float)Math.PI / 180.0F);
		}

		public static Vec3D projectViewFromEntity(EntityLiving ent, double scale)
		{
			double d3 = ent.prevPosX + (ent.posX - ent.prevPosX) * scale;
			double d5 = ent.prevPosY + (ent.posY - ent.prevPosY) * scale + (double)ent.EyeHeight;
			double d7 = ent.prevPosZ + (ent.posZ - ent.prevPosZ) * scale;
            
			double x = d3 + (double)(objectX * 1.0F);
			double y = d5 + (double)(objectY * 1.0F);
			double z = d7 + (double)(objectZ * 1.0F);
            
			return Vec3D.createVector(x, y, z);
		}

		public static int getBlockIdAtEntityViewpoint(World world0, EntityLiving entityLiving1, float f2)
		{
			Vec3D vec3D3 = projectViewFromEntity(entityLiving1, (double)f2);
			ChunkPosition chunkPosition4 = new(vec3D3);
			int blockId = world0.getBlockId(chunkPosition4.x, chunkPosition4.y, chunkPosition4.z);
			if (blockId != 0 && Block.blocksList[blockId].blockMaterial.Liquid)
			{
				float f6 = BlockFluid.getFluidHeightPercent(world0.getBlockMetadata(chunkPosition4.x, chunkPosition4.y, chunkPosition4.z)) - 0.11111111F;
				float f7 = (float)(chunkPosition4.y + 1) - f6;
				if (vec3D3.yCoord >= (double)f7)
				{
					blockId = world0.getBlockId(chunkPosition4.x, chunkPosition4.y + 1, chunkPosition4.z);
				}
			}

			return blockId;
		}
	}

}