using System;
using System.Threading;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	public sealed class GameWindowListener : WindowAdapter
	{
		internal readonly Minecraft mc;
		internal readonly Thread mcThread;

		public GameWindowListener(Minecraft minecraft1, Thread thread2)
		{
			this.mc = minecraft1;
			this.mcThread = thread2;
		}

		public void windowClosing(WindowEvent windowEvent1)
		{
			this.mc.shutdown();

			try
			{
				this.mcThread.Join();
			}
			catch (InterruptedException interruptedException3)
			{
				Console.WriteLine(interruptedException3.ToString());
				Console.Write(interruptedException3.StackTrace);
			}

			Environment.Exit(0);
		}
	}

}