using net.minecraft.client;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace net.minecraft.src
{
	public class MouseHelper
	{
		private MinecraftApplet windowComponent;
		public float deltaX;
		public float deltaY;
		private int field_1115_e { get; set; } = 10;
        
		public MouseHelper(MinecraftApplet component1)
		{
			this.windowComponent = component1;
		}

		public virtual void grabMouseCursor()
		{
			windowComponent.CursorState = OpenTK.Windowing.Common.CursorState.Grabbed;
			this.deltaX = 0;
			this.deltaY = 0;
		}

		public virtual void ungrabMouseCursor()
		{
			windowComponent.CursorState = OpenTK.Windowing.Common.CursorState.Normal;
		}

		public virtual void mouseXYChange()
		{
            if (Minecraft.previousMouseX == null || Minecraft.previousMouseY == null)
            {
				deltaX = 0;
				deltaY = 0;
				return;
            }
            unsafe
            {
				GLFW.GetCursorPos(windowComponent.WindowPtr, out double mouseX, out double mouseY);
				deltaX = (float)(mouseX - Minecraft.previousMouseX);
				deltaY = (float)(Minecraft.previousMouseY - mouseY);
			}
		}
	}

}