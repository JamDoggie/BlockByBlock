using System.Collections;

namespace net.minecraft.src
{

	using OpenTK.Graphics.OpenGL;
    
	public class GLAllocation
	{
		private static System.Collections.IList displayLists = new ArrayList();
		private static System.Collections.IList textureNames = new ArrayList();

		public static int generateDisplayLists(int i0)
		{
			lock (typeof(GLAllocation))
			{
				int i1 = GL.GenLists(i0);
				displayLists.Add(i1);
				displayLists.Add(i0);
				return i1;
			}
		}
        
		public static unsafe void generateTextureNames(IntBuffer intBuffer0)
		{
			lock (typeof(GLAllocation))
			{
				byte[] texBuffer = GetBytesFromBuffer(intBuffer0);
				GL.GenTextures(texBuffer.Length / 4, GetIntBufferFromBytes(texBuffer)); // PORTING TODO: I think this code works, but if something fucks up check this.
                
				for (int i1 = (int)intBuffer0.position(); i1 < intBuffer0.getLimit(); ++i1)
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
				GL.DeleteLists(((int?)displayLists[i1]).Value, ((int?)displayLists[i1 + 1]).Value);
				displayLists.RemoveAt(i1);
				displayLists.RemoveAt(i1);
			}
		}

		public static unsafe void deleteTexturesAndDisplayLists()
		{
			lock (typeof(GLAllocation))
			{
				for (int i0 = 0; i0 < displayLists.Count; i0 += 2)
				{
					GL.DeleteLists(((int?)displayLists[i0]).Value, ((int?)displayLists[i0 + 1]).Value);
				}
        
				IntBuffer intBuffer2 = createDirectIntBuffer(textureNames.Count);
				intBuffer2.flip();
                
				byte[] texBuffer = GetBytesFromBuffer(intBuffer2);
				GL.DeleteTextures(texBuffer.Length / 4, GetIntBufferFromBytes(texBuffer));
        
				for (int i1 = 0; i1 < textureNames.Count; ++i1)
				{
					intBuffer2.putInt(((int?)textureNames[i1]).Value);
				}
        
				intBuffer2.flip();
				byte[] texBuffer2 = GetBytesFromBuffer(intBuffer2);
				GL.DeleteTextures(texBuffer2.Length / 4, GetIntBufferFromBytes(texBuffer2));
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

		private static byte[] GetBytesFromBuffer(ByteBuffer buf)
        {
			byte[] buffer = new byte[buf.getLimit()];
			buf.get(buffer, 0, buffer.Length);

			return buffer;
		}

		/// <summary>
		/// NOTE: this method uses pinning (fixed statements). Caution is advised as this can cause the GC to be noticeably less efficient unless you know what you're doing.
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
        public static unsafe int* GetIntBufferFromBytes(byte[] bytes)
        {
            fixed (byte* p = bytes)
            {
                return (int*)p;
            }
        }
	}

}