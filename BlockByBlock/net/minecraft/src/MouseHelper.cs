using net.minecraft.client;
using System;

namespace net.minecraft.src
{

	using LWJGLException = org.lwjgl.LWJGLException;
	using Cursor = org.lwjgl.input.Cursor;
	using Mouse = org.lwjgl.input.Mouse;

	// PORTING TODO: OpenGL code; input;
    //				Java input code
	public class MouseHelper
	{
		private MinecraftApplet windowComponent;
		private Cursor cursor;
		public int deltaX;
		public int deltaY;
		private int field_1115_e = 10;
        
		public MouseHelper(MinecraftApplet component1)
		{
			this.windowComponent = component1;
			IntBuffer intBuffer2 = GLAllocation.createDirectIntBuffer(1);
			intBuffer2.putInt(0);
			intBuffer2.flip();
			IntBuffer intBuffer3 = GLAllocation.createDirectIntBuffer(1024);

			try
			{
				this.cursor = new Cursor(32, 32, 16, 16, 1, intBuffer3, intBuffer2);
			}
			catch (LWJGLException lWJGLException5)
			{
				Console.WriteLine(lWJGLException5.ToString());
				Console.Write(lWJGLException5.StackTrace);
			}

		}

		public virtual void grabMouseCursor()
		{
			Mouse.setGrabbed(true);
			this.deltaX = 0;
			this.deltaY = 0;
		}

		public virtual void ungrabMouseCursor()
		{
			Mouse.setCursorPosition(this.windowComponent.getWidth() / 2, this.windowComponent.getHeight() / 2);
			Mouse.setGrabbed(false);
		}

		public virtual void mouseXYChange()
		{
			this.deltaX = Mouse.getDX();
			this.deltaY = Mouse.getDY();
		}
	}

}