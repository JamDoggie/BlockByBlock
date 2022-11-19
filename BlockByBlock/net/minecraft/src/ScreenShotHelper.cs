using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace net.minecraft.src
{

	public class ScreenShotHelper
	{
		private static string dateFormat = "yyyy-MM-dd_HH.mm.ss";
		private static ByteBuffer buffer;
		private static byte[] pixelData;
		private static int[] imageData;

		public static string saveScreenshot(DirectoryInfo mcDirectory, int i1, int i2)
		{
			return func_35879_a(mcDirectory, null, i1, i2);
		}

		public static string func_35879_a(DirectoryInfo mcDirectory, string string1, int i2, int i3)
		{
			try
			{
				DirectoryInfo screenshotsDirectory = new DirectoryInfo(mcDirectory + "/screenshots");
				screenshotsDirectory.Create();
				if (buffer == null || buffer.capacity() < i2 * i3)
				{
					buffer = ByteBuffer.allocate(i2 * i3 * 3);
				}

				if (imageData == null || imageData.Length < i2 * i3 * 3)
				{
					pixelData = new byte[i2 * i3 * 3];
					imageData = new int[i2 * i3];
				}

				GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
				GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
				buffer.clear();
				byte[] bytes = new byte[i2 * i3 * 3];
				GL.ReadPixels(0, 0, i2, i3, PixelFormat.Rgb, PixelType.UnsignedByte, bytes);
				buffer.clear();
				string string5 = DateTime.Now.ToString(dateFormat);
				FileInfo file6;
				int i7;
				if (string.ReferenceEquals(string1, null))
				{
					for (i7 = 1; (file6 = new FileInfo(screenshotsDirectory.FullName + '/' + string5 + (i7 == 1 ? "" : "_" + i7) + ".png")).Exists; ++i7)
					{
					}
				}
				else
				{
					file6 = new FileInfo(screenshotsDirectory.FullName + '/' + string1);
				}
                
				Array.Copy(bytes, pixelData, pixelData.Length);

				for (i7 = 0; i7 < i2; ++i7)
				{
					for (int i8 = 0; i8 < i3; ++i8)
					{
						int i9 = i7 + (i3 - i8 - 1) * i2;
						int i10 = pixelData[i9 * 3 + 0] & 255;
						int i11 = pixelData[i9 * 3 + 1] & 255;
						int i12 = pixelData[i9 * 3 + 2] & 255;
						int i13 = unchecked((int)0xFF000000) | i10 << 16 | i11 << 8 | i12;
						imageData[i7 + i8 * i2] = i13;
					}
				}

				Image<Bgra32> bufferedImage15 = new Image<Bgra32>(i2, i3);

                // Load the image data into bufferedImage15
                for (int i8 = 0; i8 < i2; ++i8)
                {
                    for (int i9 = 0; i9 < i3; ++i9)
                    {
                        int i10 = imageData[i8 + i9 * i2];
                        int i11 = i10 >> 16 & 255;
                        int i12 = i10 >> 8 & 255;
                        int i13 = i10 & 255;
                        bufferedImage15[i8, i9] = new Bgra32((byte)i11, (byte)i12, (byte)i13, 255);
                    }
                }

                bufferedImage15.SaveAsPng(file6.FullName);
                return "Saved screenshot as " + file6.Name;
			}
			catch (Exception exception14)
			{
				Console.WriteLine(exception14.ToString());
				Console.Write(exception14.StackTrace);
				return "Failed to save: " + exception14;
			}
		}
	}

}