using System;
using System.Threading;

namespace net.minecraft.src
{
	internal class ThreadRunIsoClient : Thread
	{
		internal readonly CanvasIsomPreview field_1197_a;

		internal ThreadRunIsoClient(CanvasIsomPreview canvasIsomPreview1)
		{
			this.field_1197_a = canvasIsomPreview1;
		}

		public virtual void run()
		{
			while (CanvasIsomPreview.isRunning(this.field_1197_a))
			{
				this.field_1197_a.render();

				try
				{
					Thread.Sleep(1L);
				}
				catch (Exception)
				{
				}
			}

		}
	}

}