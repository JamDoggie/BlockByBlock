using System;
using System.IO;
using System.Threading;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	using Document = org.w3c.dom.Document;
	using Element = org.w3c.dom.Element;
	using Node = org.w3c.dom.Node;
	using NodeList = org.w3c.dom.NodeList;

	public class ThreadDownloadResources : Thread
	{
		public File resourcesFolder;
		private Minecraft mc;
		private bool closing = false;

		public ThreadDownloadResources(File file1, Minecraft minecraft2)
		{
			this.mc = minecraft2;
			this.setName("Resource download thread");
			this.setDaemon(true);
			this.resourcesFolder = new File(file1, "resources/");
			if (!this.resourcesFolder.exists() && !this.resourcesFolder.mkdirs())
			{
				throw new Exception("The working directory could not be created: " + this.resourcesFolder);
			}
		}

		public virtual void run()
		{
			try
			{
				URL uRL1 = new URL("http://s3.amazonaws.com/MinecraftResources/");
				DocumentBuilderFactory documentBuilderFactory2 = DocumentBuilderFactory.newInstance();
				DocumentBuilder documentBuilder3 = documentBuilderFactory2.newDocumentBuilder();
				Document document4 = documentBuilder3.parse(uRL1.openStream());
				NodeList nodeList5 = document4.getElementsByTagName("Contents");

				for (int i6 = 0; i6 < 2; ++i6)
				{
					for (int i7 = 0; i7 < nodeList5.getLength(); ++i7)
					{
						Node node8 = nodeList5.item(i7);
						if (node8.getNodeType() == 1)
						{
							Element element9 = (Element)node8;
							string string10 = ((Element)element9.getElementsByTagName("Key").item(0)).getChildNodes().item(0).getNodeValue();
							long j11 = long.Parse(((Element)element9.getElementsByTagName("Size").item(0)).getChildNodes().item(0).getNodeValue());
							if (j11 > 0L)
							{
								this.downloadAndInstallResource(uRL1, string10, j11, i6);
								if (this.closing)
								{
									return;
								}
							}
						}
					}
				}
			}
			catch (Exception exception13)
			{
				this.loadResource(this.resourcesFolder, "");
				Console.WriteLine(exception13.ToString());
				Console.Write(exception13.StackTrace);
			}

		}

		public virtual void reloadResources()
		{
			this.loadResource(this.resourcesFolder, "");
		}

		private void loadResource(File file1, string string2)
		{
			File[] file3 = file1.listFiles();

			for (int i4 = 0; i4 < file3.Length; ++i4)
			{
				if (file3[i4].isDirectory())
				{
					this.loadResource(file3[i4], string2 + file3[i4].getName() + "/");
				}
				else
				{
					try
					{
						this.mc.installResource(string2 + file3[i4].getName(), file3[i4]);
					}
					catch (Exception)
					{
						Console.WriteLine("Failed to add " + string2 + file3[i4].getName());
					}
				}
			}

		}

		private void downloadAndInstallResource(URL uRL1, string string2, long j3, int i5)
		{
			try
			{
				int i6 = string2.IndexOf("/", StringComparison.Ordinal);
				string string7 = string2.Substring(0, i6);
				if (!string7.Equals("sound") && !string7.Equals("newsound"))
				{
					if (i5 != 1)
					{
						return;
					}
				}
				else if (i5 != 0)
				{
					return;
				}

				File file8 = new File(this.resourcesFolder, string2);
				if (!file8.exists() || file8.length() != j3)
				{
					file8.getParentFile().mkdirs();
					string string9 = string2.replaceAll(" ", "%20");
					this.downloadResource(new URL(uRL1, string9), file8, j3);
					if (this.closing)
					{
						return;
					}
				}

				this.mc.installResource(string2, file8);
			}
			catch (Exception exception10)
			{
				Console.WriteLine(exception10.ToString());
				Console.Write(exception10.StackTrace);
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: private void downloadResource(java.net.URL uRL1, java.io.File file2, long j3) throws java.io.IOException
		private void downloadResource(URL uRL1, File file2, long j3)
		{
			sbyte[] b5 = new sbyte[4096];
			DataInputStream dataInputStream6 = new DataInputStream(uRL1.openStream());
			DataOutputStream dataOutputStream7 = new DataOutputStream(new FileStream(file2, FileMode.Create, FileAccess.Write));
			bool z8 = false;

			do
			{
				int i9;
				if ((i9 = dataInputStream6.read(b5)) < 0)
				{
					dataInputStream6.close();
					dataOutputStream7.close();
					return;
				}

				dataOutputStream7.write(b5, 0, i9);
			} while (!this.closing);

		}

		public virtual void closeMinecraft()
		{
			this.closing = true;
		}
	}

}