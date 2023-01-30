using System.Collections;

namespace net.minecraft.src
{

	using OpenTK.Graphics.OpenGL;
    
	public class GLAllocation
	{
		private static System.Collections.IList textureNames = new ArrayList();
        
		public static unsafe void generateTextureNames(ByteBuffer intBuffer0)
		{
			lock (typeof(GLAllocation))
			{
				byte[] texBuffer = intBuffer0.GetUnderlyingBuffer();
				GL.GenTextures(texBuffer.Length / 4, GetIntBufferFromBytes(texBuffer)); // PORTING TODO: I think this code works, but if something fucks up check this.
																						// Update: something fucked up, and it was because of this.
                
				for (int i = 0; i < texBuffer.Length; i += 4)
				{
					textureNames.Add(intBuffer0.getInt(i));
				}
        
			}
		}
        
		public static unsafe void DeleteTextures()
		{
			lock (typeof(GLAllocation))
			{
				ByteBuffer intBuffer2 = createDirectIntBuffer(textureNames.Count);
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

		public static ByteBuffer createDirectIntBuffer(int i0)
		{
			return createDirectByteBuffer(i0 << 2);
		}

		public static ByteBuffer createDirectFloatBuffer(int i0)
		{
			return createDirectByteBuffer(i0 << 2);
		}

		private static byte[] GetBytesFromBuffer(ByteBuffer buf)
        {
			long pos = buf.position();
			byte[] buffer = new byte[buf.getLimit()];
			buf.get(buffer, 0, buffer.Length);
			buf.position(pos);

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