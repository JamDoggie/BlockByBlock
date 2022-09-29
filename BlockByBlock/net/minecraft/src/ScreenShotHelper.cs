using System;

namespace net.minecraft.src
{

	using BufferUtils = org.lwjgl.BufferUtils;
	using GL11 = org.lwjgl.opengl.GL11;

	public class ScreenShotHelper
	{
		private static string dateFormat = "yyyy-MM-dd_HH.mm.ss";
		private static ByteBuffer buffer;
		private static sbyte[] pixelData;
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
					buffer = BufferUtils.createByteBuffer(i2 * i3 * 3);
				}

				if (imageData == null || imageData.Length < i2 * i3 * 3)
				{
					pixelData = new sbyte[i2 * i3 * 3];
					imageData = new int[i2 * i3];
				}

				GL11.glPixelStorei(GL11.GL_PACK_ALIGNMENT, 1);
				GL11.glPixelStorei(GL11.GL_UNPACK_ALIGNMENT, 1);
				buffer.clear();
				GL11.glReadPixels(0, 0, i2, i3, GL11.GL_RGB, GL11.GL_UNSIGNED_BYTE, buffer);
				buffer.clear();
				string string5 = DateTime.Now.ToString(dateFormat);
				FileInfo file6;
				int i7;
				if (string.ReferenceEquals(string1, null))
				{
					for (i7 = 1; (file6 = new FileInfo(screenshotsDirectory.FullName + '/' + string5 + (i7 == 1 ? "" : "_" + i7) + ".png")).Exists; ++i7)
					{ // ????
					}
				}
				else
				{
					file6 = new FileInfo(screenshotsDirectory.FullName + '/' + string1);
				}

				buffer.get(pixelData);

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

				BufferedImage bufferedImage15 = new BufferedImage(i2, i3, 1);
				bufferedImage15.setRGB(0, 0, i2, i3, imageData, 0, i2);
				ImageIO.write(bufferedImage15, "png", file6);
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