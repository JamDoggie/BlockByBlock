using BlockByBlock;
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
			return GameEnv.GetResourceAsStream(string1);
		}
	}

}