namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;
	using MinecraftApplet = net.minecraft.client.MinecraftApplet;

	public sealed class MinecraftImpl : Minecraft
	{
		internal readonly Frame mcFrame;

		public MinecraftImpl(Component component1, Canvas canvas2, MinecraftApplet minecraftApplet3, int i4, int i5, bool z6, Frame frame7) : base(component1, canvas2, minecraftApplet3, i4, i5, z6)
		{
			this.mcFrame = frame7;
		}

		public override void displayUnexpectedThrowable(UnexpectedThrowable unexpectedThrowable1)
		{
			this.mcFrame.removeAll();
			this.mcFrame.add(new PanelCrashReport(unexpectedThrowable1), "Center");
			this.mcFrame.validate();
		}
	}

}