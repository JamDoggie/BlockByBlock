using System;
using System.Collections;
using System.IO;
using System.Text;

namespace net.minecraft.src
{

	public class PostHttp
	{
		public static string func_52016_a(System.Collections.IDictionary map0)
		{
			StringBuilder stringBuilder1 = new StringBuilder();
			System.Collections.IEnumerator iterator2 = map0.SetOfKeyValuePairs().GetEnumerator();

			while (iterator2.MoveNext())
			{
				DictionaryEntry map$Entry3 = (DictionaryEntry)iterator2.Current;
				if (stringBuilder1.Length > 0)
				{
					stringBuilder1.Append('&');
				}

				try
				{
					stringBuilder1.Append(URLEncoder.encode((string)map$Entry3.Key, "UTF-8"));
				}
				catch (UnsupportedEncodingException unsupportedEncodingException6)
				{
					Console.WriteLine(unsupportedEncodingException6.ToString());
					Console.Write(unsupportedEncodingException6.StackTrace);
				}

				if (map$Entry3.Value != null)
				{
					stringBuilder1.Append('=');

					try
					{
						stringBuilder1.Append(URLEncoder.encode(map$Entry3.Value.ToString(), "UTF-8"));
					}
					catch (UnsupportedEncodingException unsupportedEncodingException5)
					{
						Console.WriteLine(unsupportedEncodingException5.ToString());
						Console.Write(unsupportedEncodingException5.StackTrace);
					}
				}
			}

			return stringBuilder1.ToString();
		}

		public static string func_52018_a(URL uRL0, System.Collections.IDictionary map1, bool z2)
		{
			return func_52017_a(uRL0, func_52016_a(map1), z2);
		}

		public static string func_52017_a(URL uRL0, string string1, bool z2)
		{
			try
			{
				HttpURLConnection httpURLConnection4 = (HttpURLConnection)uRL0.openConnection();
				httpURLConnection4.setRequestMethod("POST");
				httpURLConnection4.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
				httpURLConnection4.setRequestProperty("Content-Length", "" + string1.GetBytes().length);
				httpURLConnection4.setRequestProperty("Content-Language", "en-US");
				httpURLConnection4.setUseCaches(false);
				httpURLConnection4.setDoInput(true);
				httpURLConnection4.setDoOutput(true);
				DataOutputStream dataOutputStream5 = new DataOutputStream(httpURLConnection4.getOutputStream());
				dataOutputStream5.writeBytes(string1);
				dataOutputStream5.flush();
				dataOutputStream5.close();
				StreamReader bufferedReader6 = new StreamReader(httpURLConnection4.getInputStream());
				StringBuilder stringBuffer8 = new StringBuilder();

				string string7;
				while (!string.ReferenceEquals((string7 = bufferedReader6.ReadLine()), null))
				{
					stringBuffer8.Append(string7);
					stringBuffer8.Append('\r');
				}

				bufferedReader6.Close();
				return stringBuffer8.ToString();
			}
			catch (Exception exception9)
			{
				if (!z2)
				{
					Logger.getLogger("Minecraft").log(Level.SEVERE, "Could not post to " + uRL0, exception9);
				}

				return "";
			}
		}
	}

}