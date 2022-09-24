using BlockByBlock;
using System;
using System.Collections;
using System.IO;

namespace net.minecraft.src
{

	public class AchievementMap
	{
		public static AchievementMap instance = new AchievementMap();
		private System.Collections.IDictionary guidMap = new Hashtable();

		private AchievementMap()
		{
			try
			{
				StreamReader bufferedReader1 = new StreamReader(GameEnv.GetResourceAsStream("/achievement/map.txt"));

				string string2;
				while (!ReferenceEquals((string2 = bufferedReader1.ReadLine()), null))
				{
					string[] string3 = string2.Split(",", true);
					int i4 = int.Parse(string3[0]);
					this.guidMap[i4] = string3[1];
				}

				bufferedReader1.Close();
			}
			catch (Exception exception5)
			{
				Console.WriteLine(exception5.ToString());
				Console.Write(exception5.StackTrace);
			}

		}

		public static string getGuid(int i0)
		{
			return (string)instance.guidMap[i0];
		}
	}

}