using BlockByBlock;
using ICSharpCode.SharpZipLib.Zip;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class TexturePackCustom : TexturePackBase
	{
		private ZipFile texturePackZipFile;
		private int texturePackName = -1;
		private Image<Bgra32> texturePackThumbnail;
		private FileInfo texturePackFile;

		public TexturePackCustom(FileInfo file1)
		{
			texturePackFileName = file1.Name;
			texturePackFile = file1;
		}

		private string truncateString(string string1)
		{
			if (!string.ReferenceEquals(string1, null) && string1.Length > 34)
			{
				string1 = string1.Substring(0, 34);
			}

			return string1;
		}
        
		public override void func_6485_a(Minecraft minecraft1)
		{
			ZipFile zipFile2 = null;
			Stream inputStream3 = null;

			try
			{
				zipFile2 = new ZipFile(texturePackFile.FullName);

				try
				{
					inputStream3 = zipFile2.GetInputStream(zipFile2.GetEntry("pack.txt"));
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
					inputStream3 = zipFile2.GetInputStream(zipFile2.GetEntry("pack.png"));
					this.texturePackThumbnail = Image.Load<Bgra32>(inputStream3);
					inputStream3.Close();
				}
				catch (Exception)
				{
				}
                
				zipFile2.Close();
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
					zipFile2.Close();
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
				GL.BindTexture(TextureTarget.Texture2D, minecraft1.renderEngine.getTexture("/gui/unknown_pack.png"));
			}

		}

		public override void func_6482_a()
		{
			try
			{
				texturePackZipFile = new ZipFile(texturePackFile.FullName);
			}
			catch (Exception)
			{
			}

		}

		public override void closeTexturePackFile()
		{
			try
			{
				this.texturePackZipFile.Close();
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
				ZipEntry zipEntry2 = this.texturePackZipFile.GetEntry(string1.Substring(1));
				if (zipEntry2 != null)
				{
					return this.texturePackZipFile.GetInputStream(zipEntry2);
				}
			}
			catch (Exception)
			{
			}

			return GameEnv.GetResourceAsStream(string1);
		}
	}

}