using BlockByBlock.logging;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace net.minecraft.src
{

	public class PostHttp
	{
		private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Takes an input stream and creats a URL parameter list from it.
        /// Ex. "a=1&b=2&c=3"
        /// </summary>
        /// <param name="map0"></param>
        /// <returns></returns>
        public static string getParametersString(Dictionary<string, object> map0)
		{
            StringBuilder stringBuilder1 = new StringBuilder();
            foreach (string string1 in map0.Keys)
            {
                if (stringBuilder1.Length > 0)
                {
                    stringBuilder1.Append('&');
                }
                stringBuilder1.Append(string1);
                stringBuilder1.Append('=');
                stringBuilder1.Append(map0[string1]);
            }
            return stringBuilder1.ToString();
        }

		public static string POSTResult(Uri uRL0, Dictionary<string, object> map1, bool z2)
		{
			return POSTResult(uRL0, getParametersString(map1), z2);
		}

		public static string POSTResult(Uri uRL0, string string1, bool z2) // PORTING TODO: Ensure this works as originally intended. As far as I can tell, this is only used for snooper stats.
		{
			try
			{
				HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = uRL0;
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(string1);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = httpClient.SendAsync(request).Result;
				if (response.IsSuccessStatusCode)
				{
                    return response.Content.ReadAsStringAsync().Result;
                }
				else
				{
					if (!z2)
					{
						Logger.getLogger("Minecraft").log(Level.SEVERE, "Could not post to " + uRL0);
					}
					return string.Empty;
				}
			}
			catch (Exception exception9)
			{
				if (!z2)
				{
					Logger.getLogger("Minecraft").log(Level.SEVERE, "Could not post to " + uRL0, exception9);
				}

				return string.Empty;
			}
		}
	}

}