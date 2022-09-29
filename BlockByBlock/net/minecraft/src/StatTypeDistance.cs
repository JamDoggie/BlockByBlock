namespace net.minecraft.src
{
	internal sealed class StatTypeDistance : IStatType
	{
		public string format(int i1)
		{
			double d3 = (double)i1 / 100.0D;
			double d5 = d3 / 1000.0D;
			return d5 > 0.5D ? d5.ToString(StatBase.DecimalFormat) + " km" : (d3 > 0.5D ? d3.ToString(StatBase.DecimalFormat) + " m" : i1 + " cm");
		}
	}

}