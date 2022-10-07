/*namespace net.minecraft.src
{

	using MinecraftApplet = net.minecraft.client.MinecraftApplet;

	public class CanvasMinecraftApplet : Canvas
	{
		internal readonly MinecraftApplet mcApplet;

		public CanvasMinecraftApplet(MinecraftApplet minecraftApplet1)
		{
			this.mcApplet = minecraftApplet1;
		}

		public virtual void addNotify()
		{
			lock (this)
			{
				base.addNotify();
				this.mcApplet.startMainThread();
			}
		}

		public virtual void removeNotify()
		{
			lock (this)
			{
				this.mcApplet.shutdown();
				base.removeNotify();
			}
		}
	}

}*/