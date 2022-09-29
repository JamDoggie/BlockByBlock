using System;
using System.Collections;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class TexturePackList
	{
		private List<TexturePackBase> availableTexturePacks_Conflict = new();
		private TexturePackBase defaultTexturePack = new TexturePackDefault();
		public TexturePackBase selectedTexturePack;
		private System.Collections.IDictionary field_6538_d = new Hashtable();
		private Minecraft mc;
		private DirectoryInfo texturePackDir;
		private string currentTexturePack;

		public TexturePackList(Minecraft minecraft1, DirectoryInfo mcDirectory)
		{
			mc = minecraft1;
			texturePackDir = new DirectoryInfo(mcDirectory + "/texturepacks");
			if (!texturePackDir.Exists)
			{
				texturePackDir.Create();
			}

			currentTexturePack = minecraft1.gameSettings.skin;
			updateAvaliableTexturePacks();
			selectedTexturePack.func_6482_a();
		}

		public virtual bool setTexturePack(TexturePackBase texturePackBase1)
		{
			if (texturePackBase1 == selectedTexturePack)
			{
				return false;
			}
			else
			{
				selectedTexturePack.closeTexturePackFile();
				currentTexturePack = texturePackBase1.texturePackFileName;
				selectedTexturePack = texturePackBase1;
				mc.gameSettings.skin = currentTexturePack;
				mc.gameSettings.saveOptions();
				selectedTexturePack.func_6482_a();
				return true;
			}
		}

		public virtual void updateAvaliableTexturePacks()
		{
			List<TexturePackBase> arrayList1 = new();
			selectedTexturePack = null;
			arrayList1.Add(defaultTexturePack);
			if (texturePackDir.Exists)
			{
				FileInfo[] file2 = texturePackDir.GetFiles();
                DirectoryInfo[] directories = texturePackDir.GetDirectories();

                
                FileInfo[] file3 = file2;
				int i4 = file2.Length;

				for (int i = 0; i < i4; ++i)
				{
					FileInfo file6 = file3[i];
					string string7;
					TexturePackBase texturePackBase13;
					if (file6.Name.ToLower().EndsWith(".zip", StringComparison.Ordinal))
					{
						string7 = file6.Name + ":" + file6.Length + ":" + file6.LastWriteTime;

						try
						{
							if (!field_6538_d.Contains(string7))
							{
								TexturePackCustom texturePackCustom14 = new TexturePackCustom(file6);
								texturePackCustom14.texturePackID = string7;
								field_6538_d[string7] = texturePackCustom14;
								texturePackCustom14.func_6485_a(mc);
							}

							texturePackBase13 = (TexturePackBase)field_6538_d[string7];
							if (texturePackBase13.texturePackFileName.Equals(currentTexturePack))
							{
								selectedTexturePack = texturePackBase13;
							}

							arrayList1.Add(texturePackBase13);
						}
						catch (IOException iOException10)
						{
							Console.WriteLine(iOException10.ToString());
							Console.Write(iOException10.StackTrace);
						}
					}
					
				}

                for(int i = 0; i < directories.Length; i++)
                {
					DirectoryInfo dir = directories[i];
					string string7;
					TexturePackBase texturePackBase13;
					if (File.Exists(dir + "/pack.txt"))
					{
						string7 = dir.Name + ":folder:" + dir.LastWriteTime;

						try
						{
							if (!field_6538_d.Contains(string7))
							{
								TexturePackFolder texturePackFolder8 = new TexturePackFolder(dir.FullName);
								texturePackFolder8.texturePackID = string7;
								field_6538_d[string7] = texturePackFolder8;
								texturePackFolder8.func_6485_a(mc);
							}

							texturePackBase13 = (TexturePackBase)field_6538_d[string7];
							if (texturePackBase13.texturePackFileName.Equals(currentTexturePack))
							{
								selectedTexturePack = texturePackBase13;
							}

							arrayList1.Add(texturePackBase13);
						}
						catch (IOException iOException9)
						{
							Console.WriteLine(iOException9.ToString());
							Console.Write(iOException9.StackTrace);
						}
					}
				}
			}

			if (selectedTexturePack == null)
			{
				selectedTexturePack = defaultTexturePack;
			}

			availableTexturePacks_Conflict.RemoveAll(arrayList1);
			System.Collections.IEnumerator iterator11 = availableTexturePacks_Conflict.GetEnumerator();

			while (iterator11.MoveNext())
			{
				TexturePackBase texturePackBase12 = (TexturePackBase)iterator11.Current;
				texturePackBase12.unbindThumbnailTexture(mc);
				field_6538_d.Remove(texturePackBase12.texturePackID);
			}

			availableTexturePacks_Conflict = arrayList1;
		}

		public virtual System.Collections.IList availableTexturePacks()
		{
			return new ArrayList(availableTexturePacks_Conflict);
		}
	}

}