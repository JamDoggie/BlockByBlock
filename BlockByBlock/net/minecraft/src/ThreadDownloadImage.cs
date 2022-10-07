using BlockByBlock.helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Threading;

namespace net.minecraft.src
{

	internal class ThreadDownloadImage
	{
		internal readonly string location;
		internal readonly ImageBuffer buffer;
		internal readonly ThreadDownloadImageData imageData;

		public Thread thread;

		internal ThreadDownloadImage(ThreadDownloadImageData threadDownloadImageData1, string string2, ImageBuffer imageBuffer3)
		{
			imageData = threadDownloadImageData1;
			location = string2;
			buffer = imageBuffer3;
			thread = new Thread(() => run());
		}

		public virtual void Start()
        {
			thread.Start();
        }

		public virtual void run()
		{
			try
			{
				Uri uRL2 = new Uri(location);
				HttpResponseMessage response = SystemHelpers.httpClient.Send(new HttpRequestMessage(HttpMethod.Get, uRL2));
				
				if ((int)response.StatusCode / 100 == 4)
				{
					return;
				}

				if (buffer == null)
				{
					imageData.image = Image.Load<Bgra32>(response.Content.ReadAsStream());
				}
				else
				{
					imageData.image = buffer.parseUserSkin(Image.Load<Bgra32>(response.Content.ReadAsStream()));
				}
			}
			catch (Exception exception6)
			{
				Console.WriteLine(exception6.ToString());
				Console.Write(exception6.StackTrace);
			}
		}
	}

}