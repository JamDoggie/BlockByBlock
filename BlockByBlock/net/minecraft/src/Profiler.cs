using BlockByBlock.helpers;
using System;
using System.Collections;
using System.Collections.Concurrent;

namespace net.minecraft.src
{

	public class Profiler
	{
		public static bool profilingEnabled = false;
		private static System.Collections.IList sectionList = new ArrayList();
		private static List<long> timestampList = new();
		private static string profilingSection = "";
		private static IDictionary<string,long?> profilingMap = new ConcurrentDictionary<string,long?>();

		public static void clearProfiling()
		{
			profilingMap.Clear();
		}

		public static void startSection(string string0)
		{
			if (profilingEnabled)
			{
				if (profilingSection.Length > 0)
				{
					profilingSection = profilingSection + ".";
				}

				profilingSection = profilingSection + string0;
				sectionList.Add(profilingSection);
				timestampList.Add(JTime.NanoTime());
			}
		}

		public static void endSection()
		{
			if (profilingEnabled)
			{
				long j0 = JTime.NanoTime();
				long j2 = ((long?)timestampList.RemoveAndReturn(timestampList.Count - 1)).Value;
				sectionList.RemoveAt(sectionList.Count - 1);
				long j4 = j0 - j2;
				if (profilingMap.ContainsKey(profilingSection))
				{
					profilingMap[profilingSection] = ((long?)profilingMap[profilingSection]).Value + j4;
				}
				else
				{
					profilingMap[profilingSection] = j4;
				}

                if (j4 > 100000000L)
                {
                    Console.WriteLine(profilingSection + " " + j4);
                }

                profilingSection = sectionList.Count > 0 ? (string)sectionList[sectionList.Count - 1] : "";
			}
		}

		public static System.Collections.IList getProfilingData(string string0)
		{
			if (!profilingEnabled)
			{
				return null;
			}
			else
			{
				long j2 = profilingMap.ContainsKey("root") ? ((long?)profilingMap["root"]).Value : 0L;
				long j4 = profilingMap.ContainsKey(string0) ? ((long?)profilingMap[string0]).Value : -1L;
				ArrayList arrayList6 = new ArrayList();
				if (string0.Length > 0)
				{
					string0 = string0 + ".";
				}

				long j7 = 0L;
				System.Collections.IEnumerator iterator9 = profilingMap.Keys.GetEnumerator();

				while (iterator9.MoveNext())
				{
					string string10 = (string)iterator9.Current;
					if (string10.Length > string0.Length && string10.StartsWith(string0, StringComparison.Ordinal) && string10.IndexOf(".", string0.Length + 1, StringComparison.Ordinal) < 0)
					{
						j7 += ((long?)profilingMap[string10]).Value;
					}
				}

				float f19 = (float)j7;
				if (j7 < j4)
				{
					j7 = j4;
				}

				if (j2 < j7)
				{
					j2 = j7;
				}

				System.Collections.IEnumerator iterator20 = profilingMap.Keys.GetEnumerator();

				string string11;
				while (iterator20.MoveNext())
				{
					string11 = (string)iterator20.Current;
					if (string11.Length > string0.Length && string11.StartsWith(string0, StringComparison.Ordinal) && string11.IndexOf(".", string0.Length + 1, StringComparison.Ordinal) < 0)
					{
						long j12 = ((long?)profilingMap[string11]).Value;
						double d14 = (double)j12 * 100.0D / (double)j7;
						double d16 = (double)j12 * 100.0D / (double)j2;
						string string18 = string11.Substring(string0.Length);
						arrayList6.Add(new ProfilerResult(string18, d14, d16));
					}
				}

				iterator20 = profilingMap.Keys.GetEnumerator();

				while (iterator20.MoveNext())
				{
					string11 = (string)iterator20.Current;
					profilingMap[string11] = ((long?)profilingMap[string11]).Value * 999L / 1000L;
				}

				if ((float)j7 > f19)
				{
					arrayList6.Add(new ProfilerResult("unspecified", (double)((float)j7 - f19) * 100.0D / (double)j7, (double)((float)j7 - f19) * 100.0D / (double)j2));
				}

				arrayList6.Sort();
				arrayList6.Insert(0, new ProfilerResult(string0, 100.0D, (double)j7 * 100.0D / (double)j2));
				return arrayList6;
			}
		}

		public static void endStartSection(string string0)
		{
			endSection();
			startSection(string0);
		}
	}

}