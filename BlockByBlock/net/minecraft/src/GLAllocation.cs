using System.Collections;

namespace net.minecraft.src
{

	using GL11 = org.lwjgl.opengl.GL11;

	public class GLAllocation
	{
		private static System.Collections.IList displayLists = new ArrayList();
		private static System.Collections.IList textureNames = new ArrayList();

		public static int generateDisplayLists(int i0)
		{
			lock (typeof(GLAllocation))
			{
				int i1 = GL11.glGenLists(i0);
				displayLists.Add(i1);
				displayLists.Add(i0);
				return i1;
			}
		}

		public static void generateTextureNames(IntBuffer intBuffer0)
		{
			lock (typeof(GLAllocation))
			{
				GL11.glGenTextures(intBuffer0);
        
				for (int i1 = (int)intBuffer0.position(); i1 < intBuffer0.limit(); ++i1)
				{
					textureNames.Add(intBuffer0.getInt(i1));
				}
        
			}
		}

		public static void deleteDisplayLists(int i0)
		{
			lock (typeof(GLAllocation))
			{
				int i1 = displayLists.IndexOf(i0);
				GL11.glDeleteLists(((int?)displayLists[i1]).Value, ((int?)displayLists[i1 + 1]).Value);
				displayLists.RemoveAt(i1);
				displayLists.RemoveAt(i1);
			}
		}

		public static void deleteTexturesAndDisplayLists()
		{
			lock (typeof(GLAllocation))
			{
				for (int i0 = 0; i0 < displayLists.Count; i0 += 2)
				{
					GL11.glDeleteLists(((int?)displayLists[i0]).Value, ((int?)displayLists[i0 + 1]).Value);
				}
        
				IntBuffer intBuffer2 = createDirectIntBuffer(textureNames.Count);
				intBuffer2.flip();
				GL11.glDeleteTextures(intBuffer2);
        
				for (int i1 = 0; i1 < textureNames.Count; ++i1)
				{
					intBuffer2.putInt(((int?)textureNames[i1]).Value);
				}
        
				intBuffer2.flip();
				GL11.glDeleteTextures(intBuffer2);
				displayLists.Clear();
				textureNames.Clear();
			}
		}

		public static ByteBuffer createDirectByteBuffer(int i0)
		{
			lock (typeof(GLAllocation))
			{
				ByteBuffer byteBuffer1 = ByteBuffer.allocateDirect(i0);
				return byteBuffer1;
			}
		}

		public static IntBuffer createDirectIntBuffer(int i0)
		{
			return createDirectByteBuffer(i0 << 2).asIntBuffer();
		}

		public static FloatBuffer createDirectFloatBuffer(int i0)
		{
			return createDirectByteBuffer(i0 << 2).asFloatBuffer();
		}
	}

}