using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace net.minecraft.src
{

	public class ThreadDownloadImageData
	{
		public Image<Bgra32> image;
		public int referenceCount = 1;
		public int textureName = -1;
		public bool textureSetupComplete = false;

		public ThreadDownloadImageData(string string1, ImageBuffer imageBuffer2)
		{
			(new ThreadDownloadImage(this, string1, imageBuffer2)).Start();
		}
	}

}