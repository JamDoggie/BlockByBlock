namespace net.minecraft.src
{

	public interface ISaveFormat
	{
		string FormatName {get;}

		ISaveHandler getSaveLoader(string string1, bool z2);

		List<SaveFormatComparator> SaveList {get;}

		void flushCache();

		WorldInfo? getWorldInfo(string string1);

		void deleteWorldDirectory(string string1);

		void renameWorld(string string1, string string2);

		bool isOldMapFormat(string string1);

		bool convertMapFormat(string string1, IProgressUpdate iProgressUpdate2);
	}

}