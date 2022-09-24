using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace net.minecraft.src
{

	public class StringTranslate
	{
		private static StringTranslate instance = new StringTranslate();
		private Properties translateTable = new Properties();
		private SortedDictionary languageList;
		private string currentLanguage;
		private bool isUnicode;

		private StringTranslate()
		{
			this.loadLanguageList();
			this.Language = "en_US";
		}

		public static StringTranslate Instance
		{
			get
			{
				return instance;
			}
		}

		private void loadLanguageList()
		{
			SortedDictionary treeMap1 = new SortedDictionary();

			try
			{
				StreamReader bufferedReader2 = new StreamReader(typeof(StringTranslate).getResourceAsStream("/lang/languages.txt"), Encoding.UTF8);

				for (string string3 = bufferedReader2.ReadLine(); !string.ReferenceEquals(string3, null); string3 = bufferedReader2.ReadLine())
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

			this.languageList = treeMap1;
		}

		public virtual SortedDictionary LanguageList
		{
			get
			{
				return this.languageList;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: private void loadLanguage(java.util.Properties properties1, String string2) throws java.io.IOException
		private void loadLanguage(Properties properties1, string string2)
		{
			StreamReader bufferedReader3 = new StreamReader(typeof(StringTranslate).getResourceAsStream("/lang/" + string2 + ".lang"), Encoding.UTF8);

			for (string string4 = bufferedReader3.ReadLine(); !string.ReferenceEquals(string4, null); string4 = bufferedReader3.ReadLine())
			{
				string4 = string4.Trim();
				if (!string4.StartsWith("#", StringComparison.Ordinal))
				{
					string[] string5 = string4.Split("=", true);
					if (string5 != null && string5.Length == 2)
					{
						properties1.setProperty(string5[0], string5[1]);
					}
				}
			}

		}

		public virtual string Language
		{
			set
			{
				if (!value.Equals(this.currentLanguage))
				{
					Properties properties2 = new Properties();
    
					try
					{
						this.loadLanguage(properties2, "en_US");
					}
					catch (IOException)
					{
					}
    
					this.isUnicode = false;
					if (!"en_US".Equals(value))
					{
						try
						{
							this.loadLanguage(properties2, value);
							System.Collections.IEnumerator enumeration3 = properties2.propertyNames();
    
							while (true)
							{
								while (true)
								{
									object object5;
									do
									{
	//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
										if (!enumeration3.hasMoreElements() || this.isUnicode)
										{
											goto label47Break;
										}
    
	//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
										object object4 = enumeration3.nextElement();
										object5 = properties2.get(object4);
									} while (object5 == null);
    
									string string6 = object5.ToString();
    
									for (int i7 = 0; i7 < string6.Length; ++i7)
									{
										if (string6[i7] >= (char)256)
										{
											this.isUnicode = true;
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
    
					this.currentLanguage = value;
					this.translateTable = properties2;
				}
			}
		}

		public virtual string CurrentLanguage
		{
			get
			{
				return this.currentLanguage;
			}
		}

		public virtual bool Unicode
		{
			get
			{
				return this.isUnicode;
			}
		}

		public virtual string translateKey(string string1)
		{
			return this.translateTable.getProperty(string1, string1);
		}

		public virtual string translateKeyFormat(string string1, params object[] object2)
		{
			string string3 = this.translateTable.getProperty(string1, string1);
			return String.format(string3, object2);
		}

		public virtual string translateNamedKey(string string1)
		{
			return this.translateTable.getProperty(string1 + ".name", "");
		}

		public static bool isBidrectional(string string0)
		{
			return "ar_SA".Equals(string0) || "he_IL".Equals(string0);
		}
	}

}