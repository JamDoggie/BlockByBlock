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
			return d10 > 0.5D ? StatBase.DecimalFormat.format(d10) + " y" : (d8 > 0.5D ? StatBase.DecimalFormat.format(d8) + " d" : (d6 > 0.5D ? StatBase.DecimalFormat.format(d6) + " h" : (d4 > 0.5D ? StatBase.DecimalFormat.format(d4) + " m" : d2 + " s")));
		}
	}

}