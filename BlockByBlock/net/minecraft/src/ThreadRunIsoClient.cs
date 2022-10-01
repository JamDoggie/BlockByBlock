/*using System;
using System.Threading;

namespace net.minecraft.src
{
	internal class ThreadRunIsoClient
	{
		internal readonly CanvasIsomPreview field_1197_a;

		public Thread thread;

		internal ThreadRunIsoClient(CanvasIsomPreview canvasIsomPreview1)
		{
			field_1197_a = canvasIsomPreview1;
			thread = new Thread(() => run());
		}

		public virtual void Start()
        {
			thread.Start();
        }

		public virtual void run()
		{
			while (CanvasIsomPreview.isRunning(field_1197_a))
			{
				field_1197_a.render();

				try
				{
					Thread.Sleep(1);
				}
				catch (Exception)
				{
				}
			}
		}
	}
}*/ // PORTING TODO: Don't think this class is used. Make sure.