using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;

namespace net.minecraft.src
{


	public class StatFileWriter
	{
		private System.Collections.IDictionary field_25102_a = new Hashtable();
		private System.Collections.IDictionary field_25101_b = new Hashtable();
		private bool field_27189_c = false;
		private StatsSyncher statsSyncher;

		public StatFileWriter(Session session1, DirectoryInfo mcDirectory)
		{
			DirectoryInfo statsFolder = new DirectoryInfo(mcDirectory + "/stats");
			if (!statsFolder.Exists)
			{
				statsFolder.Create();
			}
            
			FileInfo[] file4 = mcDirectory.GetFiles();
			int i5 = file4.Length;

			for (int i6 = 0; i6 < i5; ++i6)
			{
				FileInfo file7 = file4[i6];
				if (file7.Name.StartsWith("stats_") && file7.Extension.EndsWith(".dat"))
				{
					FileInfo file8 = new FileInfo(statsFolder.FullName + '/' + file7.Name);
					if (!file8.Exists)
					{
						Console.WriteLine("Relocating " + file7.Name);
						file7.MoveTo(file8.FullName);
					}
				}
			}

			this.statsSyncher = new StatsSyncher(session1, this, statsFolder);
		}

		public virtual void readStat(StatBase statBase1, int i2)
		{
			this.writeStatToMap(this.field_25101_b, statBase1, i2);
			this.writeStatToMap(this.field_25102_a, statBase1, i2);
			this.field_27189_c = true;
		}

		private void writeStatToMap(System.Collections.IDictionary map1, StatBase statBase2, int i3)
		{
			int? integer4 = (int?)map1[statBase2];
			int i5 = integer4 == null ? 0 : integer4.Value;
			map1[statBase2] = i5 + i3;
		}

		public virtual System.Collections.IDictionary func_27176_a()
		{
			return new Hashtable(this.field_25101_b);
		}

		public virtual void func_27179_a(System.Collections.IDictionary map1)
		{
			if (map1 != null)
			{
				this.field_27189_c = true;
				System.Collections.IEnumerator iterator2 = map1.Keys.GetEnumerator();

				while (iterator2.MoveNext())
				{
					StatBase statBase3 = (StatBase)iterator2.Current;
					this.writeStatToMap(this.field_25101_b, statBase3, ((int?)map1[statBase3]).Value);
					this.writeStatToMap(this.field_25102_a, statBase3, ((int?)map1[statBase3]).Value);
				}

			}
		}

		public virtual void func_27180_b(System.Collections.IDictionary map1)
		{
			if (map1 != null)
			{
				System.Collections.IEnumerator iterator2 = map1.Keys.GetEnumerator();

				while (iterator2.MoveNext())
				{
					StatBase statBase3 = (StatBase)iterator2.Current;
					int? integer4 = (int?)this.field_25101_b[statBase3];
					int i5 = integer4 == null ? 0 : integer4.Value;
					this.field_25102_a[statBase3] = ((int?)map1[statBase3]).Value + i5;
				}

			}
		}

		public virtual void func_27187_c(System.Collections.IDictionary map1)
		{
			if (map1 != null)
			{
				this.field_27189_c = true;
				System.Collections.IEnumerator iterator2 = map1.Keys.GetEnumerator();

				while (iterator2.MoveNext())
				{
					StatBase statBase3 = (StatBase)iterator2.Current;
					this.writeStatToMap(this.field_25101_b, statBase3, ((int?)map1[statBase3]).Value);
				}

			}
		}

		public static System.Collections.IDictionary? getStatsFromJson(string string0)
		{
			Hashtable hashMap1 = new Hashtable();
            
			try
			{
				StatFileJson? statFile = JsonConvert.DeserializeObject<StatFileJson>(string0);
                
				if (statFile == null || statFile.stats == null)
					return null;

				string string2 = "local";
				StringBuilder stringBuilder3 = new StringBuilder();
				

				foreach (KeyValuePair<string,int> pair in statFile.stats)
				{
					int k = int.Parse(pair.Key);
					int v = pair.Value;
					StatBase statBase12 = StatList.getOneShotStat(k);
					if (statBase12 == null)
					{
						Console.WriteLine(k + " is not a valid stat");
					}
					else
					{
						stringBuilder3.Append(StatList.getOneShotStat(k).statGuid).Append(",");
						stringBuilder3.Append(v).Append(',');
						hashMap1[statBase12] = v;
					}
				}

				MD5String mD5String14 = new MD5String(string2);
				string jsonChecksum = mD5String14.getMD5String(stringBuilder3.ToString());
				if (jsonChecksum != statFile.checksum)
				{
					Console.WriteLine("CHECKSUM MISMATCH"); // PORTING TODO: this probably will get triggered because I have a feeling getMD5String won't have java parity. Come back to this.
					return null;
				}
			}
			catch (JsonSerializationException invalidSyntaxException13)
			{
				Console.WriteLine(invalidSyntaxException13.ToString());
				Console.Write(invalidSyntaxException13.StackTrace);
			}

			return hashMap1;
		}

		public static string getFileContentsToWrite(string string0, string string1, System.Collections.IDictionary stats)
		{
			StringBuilder stringBuilder3 = new StringBuilder();
			StringBuilder stringBuilder4 = new StringBuilder();
			bool z5 = true;
			stringBuilder3.Append("{\r\n");
			if (!string.ReferenceEquals(string0, null) && !string.ReferenceEquals(string1, null))
			{
				stringBuilder3.Append("  \"user\":{\r\n");
				stringBuilder3.Append("    \"name\":\"").Append(string0).Append("\",\r\n");
				stringBuilder3.Append("    \"sessionid\":\"").Append(string1).Append("\"\r\n");
				stringBuilder3.Append("  },\r\n");
			}

			stringBuilder3.Append("  \"stats-change\":[");
			System.Collections.IEnumerator iterator6 = stats.Keys.GetEnumerator();

			while (iterator6.MoveNext())
			{
				StatBase statBase7 = (StatBase)iterator6.Current;
				if (!z5)
				{
					stringBuilder3.Append("},");
				}
				else
				{
					z5 = false;
				}

				stringBuilder3.Append("\r\n    {\"").Append(statBase7.statId).Append("\":").Append(stats[statBase7]);
				stringBuilder4.Append(statBase7.statGuid).Append(",");
				stringBuilder4.Append(stats[statBase7]).Append(",");
			}

			if (!z5)
			{
				stringBuilder3.Append("}");
			}

			MD5String mD5String8 = new MD5String(string1);
			stringBuilder3.Append("\r\n  ],\r\n");
			stringBuilder3.Append("  \"checksum\":\"").Append(mD5String8.getMD5String(stringBuilder4.ToString())).Append("\"\r\n");
			stringBuilder3.Append("}");
			return stringBuilder3.ToString();
		}

		public virtual bool hasAchievementUnlocked(Achievement achievement1)
		{
			return this.field_25102_a.Contains(achievement1);
		}

		public virtual bool canUnlockAchievement(Achievement achievement1)
		{
			return achievement1.parentAchievement == null || this.hasAchievementUnlocked(achievement1.parentAchievement);
		}

		public virtual int writeStat(StatBase statBase1)
		{
			int? integer2 = (int?)this.field_25102_a[statBase1];
			return integer2 == null ? 0 : integer2.Value;
		}

		public virtual void func_27175_b()
		{
		}

		public virtual void syncStats()
		{
			this.statsSyncher.syncStatsFileWithMap(this.func_27176_a());
		}

		public virtual void func_27178_d()
		{
			if (this.field_27189_c && this.statsSyncher.func_27420_b())
			{
				this.statsSyncher.beginSendStats(this.func_27176_a());
			}

			this.statsSyncher.func_27425_c();
		}
	}

	public class StatFileJson
    {
		public StatFileUser user { get; set; }

        public Dictionary<string, int> stats { get; set; }

        public string checksum { get; set; }
    }

	public class StatFileUser
    {
        public string name { get; set; }
		public string sessionid { get; set; }
    }
}