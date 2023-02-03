using net.minecraft.client;
using OpenTK.Graphics.OpenGL;

namespace net.minecraft.src
{
    
	public class RenderList
	{
		private int X2;
		private int Y2;
		private int Z2;
		private double X1;
		private double Y1;
		private double Z1;
		private ByteBuffer displayLists { get; set; } = GLAllocation.createDirectIntBuffer(65536);
		private bool field_1235_h = false;
		private bool field_1234_i = false;

		public virtual void SetPosition(int i1, int i2, int i3, double x, double y, double z)
		{
			field_1235_h = true;
			displayLists.clear();
			X2 = i1;
			Y2 = i2;
			Z2 = i3;
			X1 = x;
			Y1 = y;
			Z1 = z;
		}

		public virtual bool PositionMatches(int i1, int i2, int i3)
		{
			return !this.field_1235_h ? false : i1 == this.X2 && i2 == this.Y2 && i3 == this.Z2;
		}

		public virtual void AddRenderList(int i1)
		{
			this.displayLists.putInt(i1);
			if (this.displayLists.remaining() == 0)
			{
				this.CallAndTransformDisplayLists();
			}

		}

        byte[] intBufCache = new byte[65536 * 4];

		/// <summary>
		/// Not really sure exactly what this does, name might not be quite right.
		/// </summary>
        public virtual void CallAndTransformDisplayLists()
		{
			if (this.field_1235_h)
			{
				if (!this.field_1234_i)
				{
					this.displayLists.flip();
					this.field_1234_i = true;
				}

				if (this.displayLists.remaining() > 0)
				{
                    Minecraft.renderPipeline.ModelMatrix.PushMatrix();
                    Minecraft.renderPipeline.ModelMatrix.Translate((float)((double)this.X2 - this.X1), (float)((double)this.Y2 - this.Y1), (float)((double)this.Z2 - this.Z1));
					Array.Copy(displayLists.GetUnderlyingBuffer(), intBufCache, displayLists.remaining());
					GL.CallLists((int)displayLists.remaining() / 4, ListNameType.UnsignedInt, intBufCache); // PORTING TODO: this is sus because I ported this while tired.

                    Minecraft.renderPipeline.ModelMatrix.PopMatrix();
				}

			}
		}

		public virtual void func_859_b()
		{
			this.field_1235_h = false;
			this.field_1234_i = false;
		}
	}

}