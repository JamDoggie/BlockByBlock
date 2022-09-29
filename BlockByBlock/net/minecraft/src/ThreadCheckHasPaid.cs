using System;
using System.Threading;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;
    
	public class ThreadCheckHasPaid
	{
		private static readonly HttpClient http = new HttpClient();

		internal readonly Minecraft mc;

		private Thread thread;

		public ThreadCheckHasPaid(Minecraft minecraft1)
		{
			this.mc = minecraft1;
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
				HttpRequestMessage request = new(HttpMethod.Get, new Uri("https://login.minecraft.net/session?name=" + this.mc.session.username + "&session=" + this.mc.session.sessionId));
				HttpResponseMessage response = http.Send(request);
				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest && this == null)
				{
					Minecraft.hasPaidCheckTime = DateTimeHelper.CurrentUnixTimeMillis();
				}
			}
			catch (Exception exception2)
			{
				Console.WriteLine(exception2.ToString());
				Console.Write(exception2.StackTrace);
			}

		}
	}

}