using BlockByBlock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Kajabity.Tools.Java;
using System.Text.RegularExpressions;

namespace net.minecraft.src
{

	public class StringTranslate
	{
		private static StringTranslate instance = new StringTranslate();
		private JavaProperties translateTable = new JavaProperties();
		private Dictionary<string,string> languageList;
		private string currentLanguage;
		private bool isUnicode;
		private Regex pattern = new Regex(@"%[+\-0-9]*\.*([0-9]*)([xXeEfFdDgG])");

        private StringTranslate()
		{
			loadLanguageList();
			Language = "en_US";
		}

		public static StringTranslate Instance
		{
			get
			{
				return instance;
			}
		}

        /// <summary>
        /// Loads all of the display names per language. Ex. en_US=English, fr_FR=Francais, etc.
        /// </summary>
        private void loadLanguageList()
		{
			Dictionary<string, string> treeMap1 = new();

			try
			{
				StreamReader bufferedReader2 = new StreamReader(GameEnv.GetResourceAsStream("/lang/languages.txt"), Encoding.UTF8);

				for (string? string3 = bufferedReader2.ReadLine(); string3 != null; string3 = bufferedReader2.ReadLine())
				{
					string[] string4 = string3.Split("=", true);
					if (string4 != null && string4.Length == 2)
					{
						treeMap1[string4[0]] = string4[1];
					}
				}
			}
			catch (IOException iOException5)
			{
				Console.WriteLine(iOException5.ToString());
				Console.Write(iOException5.StackTrace);
				return;
			}

			languageList = treeMap1;
		}

		public virtual Dictionary<string,string> LanguageList
		{
			get
			{
				return languageList;
			}
		}
        
		private void loadLanguage(JavaProperties properties1, string string2)
		{
			StreamReader bufferedReader3 = new StreamReader(GameEnv.GetResourceAsStream("/lang/" + string2 + ".lang"), Encoding.UTF8);

			for (string string4 = bufferedReader3.ReadLine(); !string.ReferenceEquals(string4, null); string4 = bufferedReader3.ReadLine())
			{
				string4 = string4.Trim();
				if (!string4.StartsWith("#", StringComparison.Ordinal))
				{
					string[] string5 = string4.Split("=", true);
					if (string5 != null && string5.Length == 2)
					{
						properties1.SetProperty(string5[0], string5[1]);
					}
				}
			}

		}

		public virtual string Language
		{
			set
			{
				if (!value.Equals(currentLanguage))
				{
					JavaProperties properties2 = new JavaProperties();
    
					try
					{
						loadLanguage(properties2, "en_US");
					}
					catch (IOException)
					{
					}
    
					isUnicode = false;
					if (!"en_US".Equals(value))
					{
						try
						{
							loadLanguage(properties2, value);
							System.Collections.IEnumerator enumeration3 = properties2.PropertyNames();
    
							while (true)
							{
								while (true)
								{
									object object5;
									do
									{
										if (!enumeration3.MoveNext() || isUnicode)
										{
											goto label47Break;
										}
    
										string object4 = (string)enumeration3.Current;
										object5 = properties2.GetProperty(object4);
									} while (object5 == null);
    
									string string6 = object5.ToString();
    
									for (int i7 = 0; i7 < string6.Length; ++i7)
									{
										if (string6[i7] >= (char)256)
										{
											isUnicode = true;
											break;
										}
									}
								}
								label47Continue:;
							}
							label47Break:;
						}
						catch (IOException iOException9)
						{
							Console.WriteLine(iOException9.ToString());
							Console.Write(iOException9.StackTrace);
							return;
						}
					}
    
					currentLanguage = value;
					translateTable = properties2;
				}
			}
		}

		public virtual string CurrentLanguage
		{
			get
			{
				return currentLanguage;
			}
		}

		public virtual bool Unicode
		{
			get
			{
				return isUnicode;
			}
		}

		public virtual string translateKey(string string1)
		{
			return translateTable.GetProperty(string1, string1);
		}

		public virtual string translateKeyFormat(string string1, params object[] object2)
		{
			string string3 = translateTable.GetProperty(string1, string1);

            return java.lang.String.format(string3, object2);
        }

        public virtual string translateNamedKey(string string1)
		{
			return translateTable.GetProperty(string1 + ".name", "");
		}

		public static bool isBidrectional(string string0)
		{
			return "ar_SA".Equals(string0) || "he_IL".Equals(string0);
		}
	}

}