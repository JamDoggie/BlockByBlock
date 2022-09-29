namespace net.minecraft.src
{
	public class StepSound
	{
		public readonly string stepSoundName;
		public readonly float stepSoundVolume;
		public readonly float stepSoundPitch;

		public StepSound(string string1, float f2, float f3)
		{
			this.stepSoundName = string1;
			this.stepSoundVolume = f2;
			this.stepSoundPitch = f3;
		}

		public virtual float Volume
		{
			get
			{
				return this.stepSoundVolume;
			}
		}

		public virtual float Pitch
		{
			get
			{
				return this.stepSoundPitch;
			}
		}

		public virtual string BreakSound
		{
			get
			{
				return "step." + this.stepSoundName;
			}
		}

		public virtual string StepSoundName
		{
			get
			{
				return "step." + this.stepSoundName;
			}
		}
	}

}