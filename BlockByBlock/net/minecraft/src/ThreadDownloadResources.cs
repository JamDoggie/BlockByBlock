using BlockByBlock.helpers;
using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class ThreadDownloadResources
	{
		public DirectoryInfo resourcesFolder;
		private Minecraft mc;
		private bool closing = false;

		public Thread thread;

		public ThreadDownloadResources(DirectoryInfo mcDirectory, Minecraft minecraft2)
		{
			thread = new Thread(() => run());
			mc = minecraft2;
			thread.Name = "Resource download thread";
			thread.IsBackground = true;
			resourcesFolder = new DirectoryInfo(mcDirectory + "/resources/");
			if (!resourcesFolder.Exists)
			{
				resourcesFolder.Create();

				if (!resourcesFolder.Exists)
				{
					throw new Exception("The working directory could not be created: " + resourcesFolder);
				}
			}
		}

		public virtual void Start() => thread.Start();

		public virtual void run()
		{
			try
			{
				Uri uri = new Uri("http://s3.amazonaws.com/MinecraftResources/");

				XmlSerializer serializer = new XmlSerializer(typeof(MinecraftResourcesFile));
				MinecraftResourcesFile? resourcesFile = (MinecraftResourcesFile?)serializer.Deserialize(SystemHelpers.httpClient.GetStreamAsync(uri).Result);

				if (resourcesFile == null)
					throw new Exception("Could not download resources file.");
                
				for (int i6 = 0; i6 < 2; ++i6)
				{
					foreach (MinecraftResource resource in resourcesFile.Contents)
					{
                        string resourceKey = resource.Key;
						long size = resource.Size;
                        
						if (size > 0L)
                        {
							downloadAndInstallResource(uri, resourceKey, size, i6);
							if (closing)
							{
								return;
							}
						}
                    }
				}
			}
			catch (Exception exception13)
			{
				loadResource(resourcesFolder, "");
				Console.WriteLine(exception13.ToString());
				Console.Write(exception13.StackTrace);
			}

		}

		public virtual void reloadResources()
		{
			loadResource(resourcesFolder, "");
		}

		private void loadResource(DirectoryInfo baseDir, string string2)
		{
			FileInfo[] files = baseDir.GetFiles();
			DirectoryInfo[] dirs = baseDir.GetDirectories();
			string[] dirContents = new string[files.Length + dirs.Length];

			for (int i = 0; i < files.Length; i++)
				dirContents[i] = files[i].Name;

			for (int i = 0; i < dirs.Length; i++)
				dirContents[i + files.Length] = dirs[i].Name;

			for (int i = 0; i < dirContents.Length; ++i)
			{
				if (Directory.Exists(dirContents[i]))
				{
					DirectoryInfo dir = new DirectoryInfo(dirContents[i]);
					loadResource(dir, string2 + dir.Name + "/");
				}
				else
				{
					if (File.Exists(dirContents[i]))
					{
						FileInfo file = new FileInfo(dirContents[i]);

						try
						{
							mc.installResource(string2 + file.Name, file);
						}
						catch (Exception)
						{
							Console.WriteLine("Failed to add " + string2 + file.Name);
						}
					}
				}
			}

		}

		private void downloadAndInstallResource(Uri uri, string resourceKey, long fileSize, int i5)
		{
			try
			{
				int i6 = resourceKey.IndexOf("/", StringComparison.Ordinal);
				string string7 = resourceKey.Substring(0, i6);
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

				FileInfo file = new FileInfo(resourcesFolder.FullName + '/' + resourceKey);
				if (!file.Exists || file.Length != fileSize)
				{
					file.Directory?.Create();
					string webSafeResourceName = resourceKey.Replace(" ", "%20");
					downloadResource(new Uri(uri, webSafeResourceName), file, fileSize);
					if (closing)
					{
						return;
					}
				}

				mc.installResource(resourceKey, file);
			}
			catch (Exception exception10)
			{
				Console.WriteLine(exception10.ToString());
				Console.Write(exception10.StackTrace);
			}

		}

		private void downloadResource(Uri uri, FileInfo resource, long expectedFileSize)
		{
			byte[] b5 = new byte[4096];

			BinaryReader reader = new BinaryReader(SystemHelpers.httpClient.GetStreamAsync(uri).Result);
			BinaryWriter writer = new BinaryWriter(new FileStream(resource.FullName, FileMode.Create, FileAccess.Write));
			bool z8 = false;

			do
			{
				int i9;
				if ((i9 = reader.Read(b5)) <= 0)
				{
					reader.Close();
					writer.Close();
					return;
				}

				writer.Write(b5, 0, i9);
			} while (!closing);

		}

		public virtual void closeMinecraft()
		{
			closing = true;
		}
	}

    // NOTE: this is not a vanilla Minecraft class. This is used to parse the XML file retrived from Mojang's servers when downloading game resources.
    [XmlRoot("ListBucketResult", Namespace = "http://s3.amazonaws.com/doc/2006-03-01/")]
    public class MinecraftResourcesFile
	{
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Prefix")]
        public string Prefix { get; set; }

        [XmlElement("Marker")]
        public string Marker { get; set; }

        [XmlElement("MaxKeys")]
        public int MaxKeys { get; set; }

        [XmlElement("IsTruncated")]
        public bool IsTruncated { get; set; }

        [XmlElement("Contents")]
        public List<MinecraftResource> Contents { get; set; }
    }

    // NOTE: this is not a vanilla Minecraft class. This is used to parse the XML file retrived from Mojang's servers when downloading game resources.
    public class MinecraftResource
    {
        [XmlElement("Key")]
        public string Key { get; set; }

        [XmlElement("LastModified")]
        public DateTime LastModified { get; set; }

        [XmlElement("ETag")]
        public string ETag { get; set; }

        [XmlElement("Size")]
        public long Size { get; set; }

        [XmlElement("StorageClass")]
        public string StorageClass { get; set; }
    }
}