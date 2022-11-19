using System;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class EnchantmentNameParts
	{
		public static readonly EnchantmentNameParts instance = new EnchantmentNameParts();
		private RandomExtended rand = new RandomExtended();
		private string[] wordList = "the elder scrolls klaatu berata niktu xyzzy bless curse light darkness fire air earth water hot dry cold wet ignite snuff embiggen twist shorten stretch fiddle destroy imbue galvanize enchant free limited range of towards inside sphere cube self other ball mental physical grow shrink demon elemental spirit animal creature beast humanoid undead fresh stale ".Split(" ");

		public virtual string generateRandomEnchantName()
		{
			int i1 = this.rand.Next(2) + 3;
			string string2 = "";

			for (int i3 = 0; i3 < i1; ++i3)
			{
				if (i3 > 0)
				{
					string2 = string2 + " ";
				}

				string2 = string2 + this.wordList[this.rand.Next(this.wordList.Length)];
			}

			return string2;
		}

		public virtual long RandSeed
		{
			set
			{
				rand.SetSeed(value);
			}
		}
	}

}