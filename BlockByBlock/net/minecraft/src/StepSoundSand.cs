namespace net.minecraft.src
{
	internal sealed class StepSoundSand : StepSound
	{
		internal StepSoundSand(string string1, float f2, float f3) : base(string1, f2, f3)
		{
		}

		public override string BreakSound
		{
			get
			{
				return "step.gravel";
			}
		}
	}

}