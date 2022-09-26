namespace net.minecraft.src
{
	internal class GuiChatConfirmLink : GuiConfirmOpenLink
	{
		internal readonly ChatClickData field_50056_a;
		internal readonly GuiChat field_50055_b;

		internal GuiChatConfirmLink(GuiChat guiChat1, GuiScreen guiScreen2, string string3, int i4, ChatClickData chatClickData5) : base(guiScreen2, string3, i4)
		{
			this.field_50055_b = guiChat1;
			this.field_50056_a = chatClickData5;
		}

		public override void copyLink()
		{
			setClipboardString(this.field_50056_a.func_50088_a());
		}
	}

}