using BlockByBlock.java_extensions;
using System;
using System.Collections;

namespace net.minecraft.src
{

	public class SoundPool
	{
		private RandomExtended rand = new RandomExtended();
		private Dictionary<string, System.Collections.IList> nameToSoundPoolEntriesMapping = new();
		private System.Collections.IList allSoundPoolEntries = new ArrayList();
		public int numberOfSoundPoolEntries = 0;
		public bool isGetRandomSound = true;

		public virtual SoundPoolEntry addSound(string string1, FileInfo file2)
		{
			try
			{
				string string3 = string1;
				string1 = string1.Substring(0, string1.IndexOf(".", StringComparison.Ordinal));
				if (this.isGetRandomSound)
				{
					while (char.IsDigit(string1[string1.Length - 1]))
					{
						string1 = string1.Substring(0, string1.Length - 1);
					}
				}

				string1 = string1.Replace("/", ".");
				if (!this.nameToSoundPoolEntriesMapping.ContainsKey(string1))
				{
					this.nameToSoundPoolEntriesMapping[string1] = new ArrayList();
				}

				SoundPoolEntry soundPoolEntry4 = new SoundPoolEntry(string3, new Uri(file2.FullName));
				((System.Collections.IList)this.nameToSoundPoolEntriesMapping[string1]).Add(soundPoolEntry4);
				this.allSoundPoolEntries.Add(soundPoolEntry4);
				++this.numberOfSoundPoolEntries;
				return soundPoolEntry4;
			}
			catch (UriFormatException malformedURLException5)
			{
				Console.WriteLine(malformedURLException5.ToString());
				Console.Write(malformedURLException5.StackTrace);
				throw malformedURLException5;
			}
		}

		public virtual SoundPoolEntry? getRandomSoundFromSoundPool(string? string1)
		{
			if (string1 == null)
				return null;

			this.nameToSoundPoolEntriesMapping.TryGetValue(string1, out IList? list2);
			return list2 == null ? null : (SoundPoolEntry)list2[this.rand.Next(list2.Count)];
		}

		public virtual SoundPoolEntry RandomSound
		{
			get
			{
				return this.allSoundPoolEntries.Count == 0 ? null : (SoundPoolEntry)this.allSoundPoolEntries[this.rand.Next(this.allSoundPoolEntries.Count)];
			}
		}
	}

}