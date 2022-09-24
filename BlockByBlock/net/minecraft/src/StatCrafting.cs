namespace net.minecraft.src
{
	public class StatCrafting : StatBase
	{
		private readonly int itemID;

		public StatCrafting(int i1, string string2, int i3) : base(i1, string2)
		{
			this.itemID = i3;
		}

		public virtual int ItemID
		{
			get
			{
				return this.itemID;
			}
		}
	}

}