using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace net.minecraft.src
{

	public class IsoImageBuffer
	{
		public Image<Bgra32> image;
		public World level;
		public int x;
		public int y;
		public bool rendered = false;
		public bool noContent = false;
		public int lastVisible = 0;
		public bool addedToRenderQueue = false;

		public IsoImageBuffer(World world1, int i2, int i3)
		{
			this.level = world1;
			this.init(i2, i3);
		}

		public virtual void init(int i1, int i2)
		{
			this.rendered = false;
			this.x = i1;
			this.y = i2;
			this.lastVisible = 0;
			this.addedToRenderQueue = false;
		}

		public virtual void init(World world1, int i2, int i3)
		{
			this.level = world1;
			this.init(i2, i3);
		}
	}

}