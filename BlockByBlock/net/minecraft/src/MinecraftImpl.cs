namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;
	using MinecraftApplet = net.minecraft.client.MinecraftApplet;

	public sealed class MinecraftImpl : Minecraft
	{
		

		public MinecraftImpl(MinecraftApplet minecraftApplet3, int i4, int i5, bool z6) : base(minecraftApplet3, minecraftApplet3, i4, i5, z6)
		{
			
		}

		public override void displayUnexpectedThrowable(UnexpectedThrowable unexpectedThrowable1)
		{
			// TODO: crash handler window
		}
	}

}