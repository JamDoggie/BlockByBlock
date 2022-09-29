namespace net.minecraft.src
{
	internal sealed class StatTypeSimple : IStatType
	{
		public string format(int i1)
		{
			return ((long)i1).ToString(StatBase.NumberFormat.NumberFormat);
		}
	}

}