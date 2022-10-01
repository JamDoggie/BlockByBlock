namespace net.minecraft.src
{
    using OpenTK.Windowing.Desktop;
    using Minecraft = net.minecraft.client.Minecraft;
	using MinecraftApplet = net.minecraft.client.MinecraftApplet;

	// PORTING TODO: Java window code

	public class MinecraftAppletImpl : Minecraft
	{
		internal readonly MinecraftApplet mainFrame;
        
		public MinecraftAppletImpl(MinecraftApplet minecraftApplet1, NativeWindow component2, MinecraftApplet minecraftApplet4, int i5, int i6, bool z7) : base(component2, minecraftApplet4, i5, i6, z7)
		{
			this.mainFrame = minecraftApplet1;
		}

		public override void displayUnexpectedThrowable(UnexpectedThrowable unexpectedThrowable1)
		{
			// PORTING TODO: show crash report panel here.
		}
	}

}