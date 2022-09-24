namespace net.minecraft.src
{
	public interface IProgressUpdate
	{
		void displaySavingString(string string1);

		void displayLoadingString(string string1);

		int LoadingProgress {set;}
	}

}