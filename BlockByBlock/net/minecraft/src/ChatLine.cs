namespace net.minecraft.src
{
	public class ChatLine
	{
		public string message;
		public int updateCounter;

		public ChatLine(string string1)
		{
			message = string1;
			updateCounter = 0;
		}
	}

}