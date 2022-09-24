using System.IO;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public abstract class TexturePackBase
	{
		public string texturePackFileName;
		public string firstDescriptionLine;
		public string secondDescriptionLine;
		public string texturePackID;

		public virtual void func_6482_a()
		{
		}

		public virtual void closeTexturePackFile()
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void func_6485_a(net.minecraft.client.Minecraft minecraft1) throws java.io.IOException
		public virtual void func_6485_a(Minecraft minecraft1)
		{
		}

		public virtual void unbindThumbnailTexture(Minecraft minecraft1)
		{
		}

		public virtual void bindThumbnailTexture(Minecraft minecraft1)
		{
		}

		public virtual Stream getResourceAsStream(string string1)
		{
			return typeof(TexturePackBase).getResourceAsStream(string1);
		}
	}

}