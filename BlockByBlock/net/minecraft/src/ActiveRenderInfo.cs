using BlockByBlock.helpers;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{

	public class ActiveRenderInfo
	{
		public static float objectX = 0.0F;
		public static float objectY = 0.0F;
		public static float objectZ = 0.0F;
		private static int[] viewport = new int[16];
		private static float[] modelview = new float[16];
		private static float[] projection = new float[16];
		private static float[] objectCoords = new float[3];
		public static float rotationX;
		public static float rotationXZ;
		public static float rotationZ;
		public static float rotationYZ;
		public static float rotationXY;

		public static void updateRenderInfo(EntityPlayer entityPlayer0, bool z1)
		{
			GL.GetFloat(GetPName.ModelviewMatrix, modelview);
			GL.GetFloat(GetPName.ProjectionMatrix, projection);
			GL.GetInteger(GetPName.Viewport, viewport);
			float f2 = (float)((viewport[0] + viewport[2]) / 2);
			float f3 = (float)((viewport[1] + viewport[3]) / 2);
			Glu.UnProject(f2, f3, 0.0F, modelview, projection, viewport, objectCoords);
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

		public static Vec3D projectViewFromEntity(EntityLiving entityLiving0, double d1)
		{
			double d3 = entityLiving0.prevPosX + (entityLiving0.posX - entityLiving0.prevPosX) * d1;
			double d5 = entityLiving0.prevPosY + (entityLiving0.posY - entityLiving0.prevPosY) * d1 + (double)entityLiving0.EyeHeight;
			double d7 = entityLiving0.prevPosZ + (entityLiving0.posZ - entityLiving0.prevPosZ) * d1;
			double d9 = d3 + (double)(objectX * 1.0F);
			double d11 = d5 + (double)(objectY * 1.0F);
			double d13 = d7 + (double)(objectZ * 1.0F);
			return Vec3D.createVector(d9, d11, d13);
		}

		public static int getBlockIdAtEntityViewpoint(World world0, EntityLiving entityLiving1, float f2)
		{
			Vec3D vec3D3 = projectViewFromEntity(entityLiving1, (double)f2);
			ChunkPosition chunkPosition4 = new ChunkPosition(vec3D3);
			int i5 = world0.getBlockId(chunkPosition4.x, chunkPosition4.y, chunkPosition4.z);
			if (i5 != 0 && Block.blocksList[i5].blockMaterial.Liquid)
			{
				float f6 = BlockFluid.getFluidHeightPercent(world0.getBlockMetadata(chunkPosition4.x, chunkPosition4.y, chunkPosition4.z)) - 0.11111111F;
				float f7 = (float)(chunkPosition4.y + 1) - f6;
				if (vec3D3.yCoord >= (double)f7)
				{
					i5 = world0.getBlockId(chunkPosition4.x, chunkPosition4.y + 1, chunkPosition4.z);
				}
			}

			return i5;
		}
	}

}