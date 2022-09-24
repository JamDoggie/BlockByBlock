namespace net.minecraft.src
{
	public class PotionHealth : Potion
	{
		public PotionHealth(int i1, bool z2, int i3) : base(i1, z2, i3)
		{
		}

		public override bool Instant
		{
			get
			{
				return true;
			}
		}

		public override bool isReady(int i1, int i2)
		{
			return i1 >= 1;
		}
	}

}