using System;
using System.Collections;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class TexturePackList
	{
//JAVA TO C# CONVERTER NOTE: Field name conflicts with a method name of the current type:
		private System.Collections.IList availableTexturePacks_Conflict = new ArrayList();
		private TexturePackBase defaultTexturePack = new TexturePackDefault();
		public TexturePackBase selectedTexturePack;
		private System.Collections.IDictionary field_6538_d = new Hashtable();
		private Minecraft mc;
		private File texturePackDir;
		private string currentTexturePack;

		public TexturePackList(Minecraft minecraft1, File file2)
		{
			this.mc = minecraft1;
			this.texturePackDir = new File(file2, "texturepacks");
			if (this.texturePackDir.exists())
			{
				if (!this.texturePackDir.isDirectory())
				{
					this.texturePackDir.delete();
					this.texturePackDir.mkdirs();
				}
			}
			else
			{
				this.texturePackDir.mkdirs();
			}

			this.currentTexturePack = minecraft1.gameSettings.skin;
			this.updateAvaliableTexturePacks();
			this.selectedTexturePack.func_6482_a();
		}

		public virtual bool setTexturePack(TexturePackBase texturePackBase1)
		{
			if (texturePackBase1 == this.selectedTexturePack)
			{
				return false;
			}
			else
			{
				this.selectedTexturePack.closeTexturePackFile();
				this.currentTexturePack = texturePackBase1.texturePackFileName;
				this.selectedTexturePack = texturePackBase1;
				this.mc.gameSettings.skin = this.currentTexturePack;
				this.mc.gameSettings.saveOptions();
				this.selectedTexturePack.func_6482_a();
				return true;
			}
		}

		public virtual void updateAvaliableTexturePacks()
		{
			ArrayList arrayList1 = new ArrayList();
			this.selectedTexturePack = null;
			arrayList1.Add(this.defaultTexturePack);
			if (this.texturePackDir.exists() && this.texturePackDir.isDirectory())
			{
				File[] file2 = this.texturePackDir.listFiles();
				File[] file3 = file2;
				int i4 = file2.Length;

				for (int i5 = 0; i5 < i4; ++i5)
				{
					File file6 = file3[i5];
					string string7;
					TexturePackBase texturePackBase13;
					if (file6.isFile() && file6.getName().ToLower().EndsWith(".zip", StringComparison.Ordinal))
					{
						string7 = file6.getName() + ":" + file6.length() + ":" + file6.lastModified();

						try
						{
							if (!this.field_6538_d.Contains(string7))
							{
								TexturePackCustom texturePackCustom14 = new TexturePackCustom(file6);
								texturePackCustom14.texturePackID = string7;
								this.field_6538_d[string7] = texturePackCustom14;
								texturePackCustom14.func_6485_a(this.mc);
							}

							texturePackBase13 = (TexturePackBase)this.field_6538_d[string7];
							if (texturePackBase13.texturePackFileName.Equals(this.currentTexturePack))
							{
								this.selectedTexturePack = texturePackBase13;
							}

							arrayList1.Add(texturePackBase13);
						}
						catch (IOException iOException10)
						{
							Console.WriteLine(iOException10.ToString());
							Console.Write(iOException10.StackTrace);
						}
					}
					else if (file6.isDirectory() && (new File(file6, "pack.txt")).exists())
					{
						string7 = file6.getName() + ":folder:" + file6.lastModified();

						try
						{
							if (!this.field_6538_d.Contains(string7))
							{
								TexturePackFolder texturePackFolder8 = new TexturePackFolder(file6);
								texturePackFolder8.texturePackID = string7;
								this.field_6538_d[string7] = texturePackFolder8;
								texturePackFolder8.func_6485_a(this.mc);
							}

							texturePackBase13 = (TexturePackBase)this.field_6538_d[string7];
							if (texturePackBase13.texturePackFileName.Equals(this.currentTexturePack))
							{
								this.selectedTexturePack = texturePackBase13;
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

			if (this.selectedTexturePack == null)
			{
				this.selectedTexturePack = this.defaultTexturePack;
			}

			this.availableTexturePacks_Conflict.RemoveAll(arrayList1);
			System.Collections.IEnumerator iterator11 = this.availableTexturePacks_Conflict.GetEnumerator();

			while (iterator11.MoveNext())
			{
				TexturePackBase texturePackBase12 = (TexturePackBase)iterator11.Current;
				texturePackBase12.unbindThumbnailTexture(this.mc);
				this.field_6538_d.Remove(texturePackBase12.texturePackID);
			}

			this.availableTexturePacks_Conflict = arrayList1;
		}

		public virtual System.Collections.IList availableTexturePacks()
		{
			return new ArrayList(this.availableTexturePacks_Conflict);
		}
	}

}