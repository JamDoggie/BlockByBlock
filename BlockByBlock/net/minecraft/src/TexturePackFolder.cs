using BlockByBlock;
using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class TexturePackFolder : TexturePackBase
	{
		private int field_48191_e = -1;
		private Image<Bgra32> field_48189_f;
		private string field_48190_g;

		public TexturePackFolder(string file1)
		{
			this.texturePackFileName = file1;
			this.field_48190_g = file1;
		}

		private string func_48188_b(string string1)
		{
			if (!string.ReferenceEquals(string1, null) && string1.Length > 34)
			{
				string1 = string1.Substring(0, 34);
			}

			return string1;
		}

		public override void func_6485_a(Minecraft minecraft1)
		{
			Stream inputStream2 = null;

			try
			{
				try
				{
					inputStream2 = getResourceAsStream("pack.txt");
					StreamReader bufferedReader3 = new StreamReader(inputStream2);
					this.firstDescriptionLine = this.func_48188_b(bufferedReader3.ReadLine());
					this.secondDescriptionLine = this.func_48188_b(bufferedReader3.ReadLine());
					bufferedReader3.Close();
					inputStream2.Close();
				}
				catch (Exception)
				{
				}

				try
				{
					inputStream2 = this.getResourceAsStream("pack.png");
					this.field_48189_f = Image.Load<Bgra32>(inputStream2);
					inputStream2.Close();
				}
				catch (Exception)
				{
				}
			}
			catch (Exception exception16)
			{
				Console.WriteLine(exception16.ToString());
				Console.Write(exception16.StackTrace);
			}
			finally
			{
				try
				{
					inputStream2.Close();
				}
				catch (Exception)
				{
				}

			}

		}

		public override void unbindThumbnailTexture(Minecraft minecraft1)
		{
			if (this.field_48189_f != null)
			{
				minecraft1.renderEngine.deleteTexture(this.field_48191_e);
			}

			this.closeTexturePackFile();
		}

		public override void bindThumbnailTexture(Minecraft minecraft1)
		{
			if (this.field_48189_f != null && this.field_48191_e < 0)
			{
				this.field_48191_e = minecraft1.renderEngine.allocateAndSetupTexture(this.field_48189_f);
			}

			if (this.field_48189_f != null)
			{
				minecraft1.renderEngine.bindTexture(this.field_48191_e);
			}
			else
			{
				GL.BindTexture(TextureTarget.Texture2D, minecraft1.renderEngine.getTexture("/gui/unknown_pack.png"));
			}

		}

		public override void func_6482_a()
		{
		}

		public override void closeTexturePackFile()
		{
		}

		public override Stream getResourceAsStream(string string1)
		{
			try
			{
				FileInfo file2 = new FileInfo(field_48190_g + "/" + string1.Substring(1));
				if (file2.Exists)
				{
					return new FileStream(file2.FullName, FileMode.Open, FileAccess.Read);
				}
			}
			catch (Exception)
			{
			}

			return GameEnv.GetResourceAsStream(string1);
		}
	}

}