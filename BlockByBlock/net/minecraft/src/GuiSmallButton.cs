namespace net.minecraft.src
{
	public class GuiSmallButton : GuiButton
	{
		private readonly EnumOptions enumOptions;

		public GuiSmallButton(int i1, int i2, int i3, string string4) : this(i1, i2, i3, (EnumOptions)null, string4)
		{
		}

		public GuiSmallButton(int i1, int i2, int i3, int i4, int i5, string string6) : base(i1, i2, i3, i4, i5, string6)
		{
			this.enumOptions = null;
		}

		public GuiSmallButton(int i1, int i2, int i3, EnumOptions enumOptions4, string string5) : base(i1, i2, i3, 150, 20, string5)
		{
			this.enumOptions = enumOptions4;
		}

		public virtual EnumOptions returnEnumOptions()
		{
			return this.enumOptions;
		}
	}

}