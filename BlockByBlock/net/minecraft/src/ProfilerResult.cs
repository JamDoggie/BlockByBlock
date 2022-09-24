using System;

namespace net.minecraft.src
{
	public sealed class ProfilerResult : IComparable
	{
		public double sectionPercentage;
		public double globalPercentage;
		public string name;

		public ProfilerResult(string string1, double d2, double d4)
		{
			this.name = string1;
			this.sectionPercentage = d2;
			this.globalPercentage = d4;
		}

		public int compareProfilerResult(ProfilerResult profilerResult1)
		{
			return profilerResult1.sectionPercentage < this.sectionPercentage ? -1 : (profilerResult1.sectionPercentage > this.sectionPercentage ? 1 : string.CompareOrdinal(profilerResult1.name, this.name));
		}

		public int DisplayColor
		{
			get
			{
				return (this.name.GetHashCode() & 11184810) + 4473924;
			}
		}

		public int CompareTo(object object1)
		{
			return this.compareProfilerResult((ProfilerResult)object1);
		}
	}

}