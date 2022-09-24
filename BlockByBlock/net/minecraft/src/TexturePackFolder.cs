using System;
using System.IO;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	using GL11 = org.lwjgl.opengl.GL11;

	public class TexturePackFolder : TexturePackBase
	{
		private int field_48191_e = -1;
		private BufferedImage field_48189_f;
		private File field_48190_g;

		public TexturePackFolder(File file1)
		{
			this.texturePackFileName = file1.getName();
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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void func_6485_a(net.minecraft.client.Minecraft minecraft1) throws java.io.IOException
		public override void func_6485_a(Minecraft minecraft1)
		{
			Stream inputStream2 = null;

			try
			{
				try
				{
					inputStream2 = this.getResourceAsStream("pack.txt");
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
					this.field_48189_f = ImageIO.read(inputStream2);
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
				GL11.glBindTexture(GL11.GL_TEXTURE_2D, minecraft1.renderEngine.getTexture("/gui/unknown_pack.png"));
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
				File file2 = new File(this.field_48190_g, string1.Substring(1));
				if (file2.exists())
				{
					return new BufferedInputStream(new FileStream(file2, FileMode.Open, FileAccess.Read));
				}
			}
			catch (Exception)
			{
			}

			return typeof(TexturePackBase).getResourceAsStream(string1);
		}
	}

}