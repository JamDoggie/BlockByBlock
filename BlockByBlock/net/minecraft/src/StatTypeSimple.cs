namespace net.minecraft.src
{
	internal sealed class StatTypeSimple : IStatType
	{
		public string format(int i1)
		{
			return StatBase.NumberFormat.format((long)i1);
		}
	}

}