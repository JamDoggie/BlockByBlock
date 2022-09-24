using BlockByBlock;
using System;
using System.IO;
using System.Text;

namespace net.minecraft.src
{

	public class ChatAllowedCharacters
	{
		public static readonly string allowedCharacters = AllowedCharacters;
		public static readonly char[] allowedCharactersArray = new char[]{'/', '\n', '\r', '\t', '\u0000', '\f', '`', '?', '*', '\\', '<', '>', '|', '\"', ':'};

		private static string AllowedCharacters
		{
			get
			{
				string string0 = "";
    
				try
				{
					StreamReader bufferedReader1 = new StreamReader(GameEnv.GetResourceAsStream("/font.txt"), Encoding.UTF8);
					string string2 = "";
    
					while (!ReferenceEquals((string2 = bufferedReader1.ReadLine()), null))
					{
						if (!string2.StartsWith("#", StringComparison.Ordinal))
						{
							string0 = string0 + string2;
						}
					}
    
					bufferedReader1.Close();
				}
				catch (Exception)
				{
				}
    
				return string0;
			}
		}

		public static bool isAllowedCharacter(char c0)
		{
			return c0 != (char)167 && (allowedCharacters.IndexOf(c0) >= 0 || c0 > (char)32);
		}

		public static string func_52019_a(string string0)
		{
			StringBuilder stringBuilder1 = new StringBuilder();
			char[] c2 = string0.ToCharArray();
			int i3 = c2.Length;

			for (int i4 = 0; i4 < i3; ++i4)
			{
				char c5 = c2[i4];
				if (isAllowedCharacter(c5))
				{
					stringBuilder1.Append(c5);
				}
			}

			return stringBuilder1.ToString();
		}
	}

}