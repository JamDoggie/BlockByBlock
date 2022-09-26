using System;

namespace net.minecraft.src
{
	public class SaveFormatComparator : IComparable
	{
		private readonly string fileName;
		private readonly string displayName;
		private readonly long lastTimePlayed;
		private readonly long sizeOnDisk;
		private readonly bool requiresConversion;
		private readonly int gameType;
		private readonly bool hardcore;

		public SaveFormatComparator(string string1, string string2, long j3, long j5, int i7, bool z8, bool z9)
		{
			this.fileName = string1;
			this.displayName = string2;
			this.lastTimePlayed = j3;
			this.sizeOnDisk = j5;
			this.gameType = i7;
			this.requiresConversion = z8;
			this.hardcore = z9;
		}

		public virtual string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		public virtual string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		public virtual bool getRequiresConversion()
		{
			return requiresConversion;
		}

		public virtual long LastTimePlayed
		{
			get
			{
				return this.lastTimePlayed;
			}
		}

		public virtual int CompareTo(SaveFormatComparator saveFormatComparator1)
		{
			return this.lastTimePlayed < saveFormatComparator1.lastTimePlayed ? 1 : (this.lastTimePlayed > saveFormatComparator1.lastTimePlayed ? -1 : string.CompareOrdinal(this.fileName, saveFormatComparator1.fileName));
		}

		public virtual int GameType
		{
			get
			{
				return this.gameType;
			}
		}

		public virtual bool HardcoreModeEnabled
		{
			get
			{
				return this.hardcore;
			}
		}

		public virtual int CompareTo(object object1)
		{
			return this.CompareTo((SaveFormatComparator)object1);
		}
	}

}