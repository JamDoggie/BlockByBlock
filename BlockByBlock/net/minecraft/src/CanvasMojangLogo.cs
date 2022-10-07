/*namespace net.minecraft.src
{
    
	internal class CanvasMojangLogo : Canvas
	{
		private BufferedImage logo;

		public CanvasMojangLogo()
		{
			try
			{
				this.logo = ImageIO.read(typeof(PanelCrashReport).getResource("/gui/crash_logo.png"));
			}
			catch (IOException)
			{
			}

			sbyte b1 = 100;
			this.setPreferredSize(new Dimension(b1, b1));
			this.setMinimumSize(new Dimension(b1, b1));
		}

		public virtual void paint(Graphics graphics1)
		{
			base.paint(graphics1);
			graphics1.drawImage(this.logo, this.getWidth() / 2 - this.logo.getWidth() / 2, 32, (ImageObserver)null);
		}
	}

}*/