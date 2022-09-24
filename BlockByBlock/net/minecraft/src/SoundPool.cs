using System;
using System.Collections;

namespace net.minecraft.src
{

	public class SoundPool
	{
		private Random rand = new Random();
		private System.Collections.IDictionary nameToSoundPoolEntriesMapping = new Hashtable();
		private System.Collections.IList allSoundPoolEntries = new ArrayList();
		public int numberOfSoundPoolEntries = 0;
		public bool isGetRandomSound = true;

		public virtual SoundPoolEntry addSound(string string1, File file2)
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

				string1 = string1.replaceAll("/", ".");
				if (!this.nameToSoundPoolEntriesMapping.Contains(string1))
				{
					this.nameToSoundPoolEntriesMapping[string1] = new ArrayList();
				}

				SoundPoolEntry soundPoolEntry4 = new SoundPoolEntry(string3, file2.toURI().toURL());
				((System.Collections.IList)this.nameToSoundPoolEntriesMapping[string1]).Add(soundPoolEntry4);
				this.allSoundPoolEntries.Add(soundPoolEntry4);
				++this.numberOfSoundPoolEntries;
				return soundPoolEntry4;
			}
			catch (MalformedURLException malformedURLException5)
			{
				Console.WriteLine(malformedURLException5.ToString());
				Console.Write(malformedURLException5.StackTrace);
				throw new Exception(malformedURLException5);
			}
		}

		public virtual SoundPoolEntry getRandomSoundFromSoundPool(string string1)
		{
			System.Collections.IList list2 = (System.Collections.IList)this.nameToSoundPoolEntriesMapping[string1];
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