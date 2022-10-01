using System;
using System.Threading;

namespace net.minecraft.src
{

	using Minecraft = net.minecraft.client.Minecraft;

	// PORTING TODO: This class is not necessary. Should be able to remove.

	public sealed class GameWindowListener
	{
		internal readonly Minecraft mc;
		internal readonly Thread mcThread;

		public GameWindowListener(Minecraft minecraft1, Thread thread2)
		{
			this.mc = minecraft1;
			this.mcThread = thread2;
		}
	}
}