namespace net.minecraft.src
{
	internal sealed class StatTypeDistance : IStatType
	{
		public string format(int i1)
		{
			double d3 = (double)i1 / 100.0D;
			double d5 = d3 / 1000.0D;
			return d5 > 0.5D ? StatBase.DecimalFormat.format(d5) + " km" : (d3 > 0.5D ? StatBase.DecimalFormat.format(d3) + " m" : i1 + " cm");
		}
	}

}