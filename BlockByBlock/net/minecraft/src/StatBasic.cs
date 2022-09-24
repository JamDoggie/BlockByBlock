namespace net.minecraft.src
{
	public class StatBasic : StatBase
	{
		public StatBasic(int i1, string string2, IStatType iStatType3) : base(i1, string2, iStatType3)
		{
		}

		public StatBasic(int i1, string string2) : base(i1, string2)
		{
		}

		public override StatBase registerStat()
		{
			base.registerStat();
			StatList.generalStats.Add(this);
			return this;
		}
	}

}