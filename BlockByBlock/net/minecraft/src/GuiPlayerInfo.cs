using System;

namespace net.minecraft.src
{
	public class GuiPlayerInfo
	{
		public readonly string name;
		private readonly string nameinLowerCase;
		public int responseTime;

		public GuiPlayerInfo(string string1)
		{
			this.name = string1;
			this.nameinLowerCase = string1.ToLower();
		}

		public virtual bool nameStartsWith(string string1)
		{
			return this.nameinLowerCase.StartsWith(string1, StringComparison.Ordinal);
		}
	}

}