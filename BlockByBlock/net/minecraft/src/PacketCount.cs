using System.Collections;

namespace net.minecraft.src
{

	public class PacketCount
	{
		public static bool allowCounting = true;
		private static readonly System.Collections.IDictionary packetCountForID = new Hashtable();
		private static readonly System.Collections.IDictionary sizeCountForID = new Hashtable();
		private static readonly object @lock = new object();

		public static void countPacket(int i0, long j1)
		{
			if (allowCounting)
			{
				object object3 = @lock;
				lock (@lock)
				{
					if (packetCountForID.Contains(i0))
					{
						packetCountForID[i0] = ((long?)packetCountForID[i0]).Value + 1L;
						sizeCountForID[i0] = ((long?)sizeCountForID[i0]).Value + j1;
					}
					else
					{
						packetCountForID[i0] = 1L;
						sizeCountForID[i0] = j1;
					}

				}
			}
		}
	}
}