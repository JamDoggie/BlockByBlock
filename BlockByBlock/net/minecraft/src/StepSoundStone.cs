namespace net.minecraft.src
{
	internal sealed class StepSoundStone : StepSound
	{
		internal StepSoundStone(string string1, float f2, float f3) : base(string1, f2, f3)
		{
		}

		public override string BreakSound
		{
			get
			{
				return "random.glass";
			}
		}
	}

}