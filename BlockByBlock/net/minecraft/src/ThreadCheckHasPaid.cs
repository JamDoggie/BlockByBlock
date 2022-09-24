using System;
using System.Threading;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public class ThreadCheckHasPaid : Thread
	{
		internal readonly Minecraft mc;

		public ThreadCheckHasPaid(Minecraft minecraft1)
		{
			this.mc = minecraft1;
		}

		public virtual void run()
		{
			try
			{
				HttpURLConnection httpURLConnection1 = (HttpURLConnection)(new URL("https://login.minecraft.net/session?name=" + this.mc.session.username + "&session=" + this.mc.session.sessionId)).openConnection();
				httpURLConnection1.connect();
				if (httpURLConnection1.getResponseCode() == 400 && this == null)
				{
					Minecraft.hasPaidCheckTime = DateTimeHelper.CurrentUnixTimeMillis();
				}

				httpURLConnection1.disconnect();
			}
			catch (Exception exception2)
			{
				Console.WriteLine(exception2.ToString());
				Console.Write(exception2.StackTrace);
			}

		}
	}

}