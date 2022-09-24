using System;
using System.IO;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	using GL11 = org.lwjgl.opengl.GL11;

	public class TexturePackCustom : TexturePackBase
	{
		private ZipFile texturePackZipFile;
		private int texturePackName = -1;
		private BufferedImage texturePackThumbnail;
		private File texturePackFile;

		public TexturePackCustom(File file1)
		{
			this.texturePackFileName = file1.getName();
			this.texturePackFile = file1;
		}

		private string truncateString(string string1)
		{
			if (!string.ReferenceEquals(string1, null) && string1.Length > 34)
			{
				string1 = string1.Substring(0, 34);
			}

			return string1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void func_6485_a(net.minecraft.client.Minecraft minecraft1) throws java.io.IOException
		public override void func_6485_a(Minecraft minecraft1)
		{
			ZipFile zipFile2 = null;
			Stream inputStream3 = null;

			try
			{
				zipFile2 = new ZipFile(this.texturePackFile);

				try
				{
					inputStream3 = zipFile2.getInputStream(zipFile2.getEntry("pack.txt"));
					StreamReader bufferedReader4 = new StreamReader(inputStream3);
					this.firstDescriptionLine = this.truncateString(bufferedReader4.ReadLine());
					this.secondDescriptionLine = this.truncateString(bufferedReader4.ReadLine());
					bufferedReader4.Close();
					inputStream3.Close();
				}
				catch (Exception)
				{
				}

				try
				{
					inputStream3 = zipFile2.getInputStream(zipFile2.getEntry("pack.png"));
					this.texturePackThumbnail = ImageIO.read(inputStream3);
					inputStream3.Close();
				}
				catch (Exception)
				{
				}

				zipFile2.close();
			}
			catch (Exception exception21)
			{
				Console.WriteLine(exception21.ToString());
				Console.Write(exception21.StackTrace);
			}
			finally
			{
				try
				{
					inputStream3.Close();
				}
				catch (Exception)
				{
				}

				try
				{
					zipFile2.close();
				}
				catch (Exception)
				{
				}

			}

		}

		public override void unbindThumbnailTexture(Minecraft minecraft1)
		{
			if (this.texturePackThumbnail != null)
			{
				minecraft1.renderEngine.deleteTexture(this.texturePackName);
			}

			this.closeTexturePackFile();
		}

		public override void bindThumbnailTexture(Minecraft minecraft1)
		{
			if (this.texturePackThumbnail != null && this.texturePackName < 0)
			{
				this.texturePackName = minecraft1.renderEngine.allocateAndSetupTexture(this.texturePackThumbnail);
			}

			if (this.texturePackThumbnail != null)
			{
				minecraft1.renderEngine.bindTexture(this.texturePackName);
			}
			else
			{
				GL11.glBindTexture(GL11.GL_TEXTURE_2D, minecraft1.renderEngine.getTexture("/gui/unknown_pack.png"));
			}

		}

		public override void func_6482_a()
		{
			try
			{
				this.texturePackZipFile = new ZipFile(this.texturePackFile);
			}
			catch (Exception)
			{
			}

		}

		public override void closeTexturePackFile()
		{
			try
			{
				this.texturePackZipFile.close();
			}
			catch (Exception)
			{
			}

			this.texturePackZipFile = null;
		}

		public override Stream getResourceAsStream(string string1)
		{
			try
			{
				ZipEntry zipEntry2 = this.texturePackZipFile.getEntry(string1.Substring(1));
				if (zipEntry2 != null)
				{
					return this.texturePackZipFile.getInputStream(zipEntry2);
				}
			}
			catch (Exception)
			{
			}

			return typeof(TexturePackBase).getResourceAsStream(string1);
		}
	}

}