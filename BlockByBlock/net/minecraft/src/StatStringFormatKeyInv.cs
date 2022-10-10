using System;

namespace net.minecraft.src
{
	using Minecraft = net.minecraft.client.Minecraft;

	public class StatStringFormatKeyInv : IStatStringFormat
	{
		internal readonly Minecraft mc;

		public StatStringFormatKeyInv(Minecraft minecraft1)
		{
			this.mc = minecraft1;
		}

		public virtual string formatString(string string1)
		{
			try
			{
				return java.lang.String.format(string1, new object[] { GameSettings.getKeyDisplayString(this.mc.gameSettings.keyBindInventory.keyCode) });
			}
			catch (Exception exception3)
			{
				return "Error: " + exception3.Message;
			}
		}
	}

}