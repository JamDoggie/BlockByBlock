
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace net.minecraft.src
{

	public interface ImageBuffer
	{
		Image<Bgra32> parseUserSkin(Image<Bgra32> bufferedImage1);
	}

}