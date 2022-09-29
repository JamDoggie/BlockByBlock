namespace net.minecraft.src
{
	internal sealed class StatTypeTime : IStatType
	{
		public string format(int i1)
		{
			double d2 = (double)i1 / 20.0D;
			double d4 = d2 / 60.0D;
			double d6 = d4 / 60.0D;
			double d8 = d6 / 24.0D;
			double d10 = d8 / 365.0D;
			return d10 > 0.5D ? d10.ToString(StatBase.DecimalFormat) + " y" : (d8 > 0.5D ? d8.ToString(StatBase.DecimalFormat) + " d" : (d6 > 0.5D ? d6.ToString(StatBase.DecimalFormat) + " h" : (d4 > 0.5D ? d4.ToString(StatBase.DecimalFormat) + " m" : d2 + " s")));
		}
	}

}