using System;
using System.IO;
using System.Text;
using System.Threading;

namespace net.minecraft.src
{

	public class StatsSyncher
	{
		private volatile bool isBusy = false;
		private volatile System.Collections.IDictionary field_27437_b = null;
		private volatile System.Collections.IDictionary field_27436_c = null;
		private StatFileWriter statFileWriter;
		private FileInfo unsentDataFile;
		private FileInfo dataFile;
		private FileInfo unsentTempFile;
		private FileInfo tempFile;
		private FileInfo unsentOldFile;
		private FileInfo oldFile;
		private Session theSession;
		private int field_27427_l = 0;
		private int field_27426_m = 0;

		public StatsSyncher(Session session1, StatFileWriter statFileWriter2, DirectoryInfo statsFolder)
		{
			unsentDataFile = new FileInfo(statsFolder + "/stats_" + session1.username.ToLower() + "_unsent.dat");
			dataFile = new FileInfo(statsFolder + "/stats_" + session1.username.ToLower() + ".dat");
			unsentOldFile = new FileInfo(statsFolder + "/stats_" + session1.username.ToLower() + "_unsent.old");
			oldFile = new FileInfo(statsFolder + "/stats_" + session1.username.ToLower() + ".old");
			unsentTempFile = new FileInfo(statsFolder + "/stats_" + session1.username.ToLower() + "_unsent.tmp");
			tempFile = new FileInfo(statsFolder + "/stats_" + session1.username.ToLower() + ".tmp");
			if (!session1.username.ToLower().Equals(session1.username))
			{
				func_28214_a(statsFolder, "stats_" + session1.username + "_unsent.dat", unsentDataFile);
				func_28214_a(statsFolder, "stats_" + session1.username + ".dat", dataFile);
				func_28214_a(statsFolder, "stats_" + session1.username + "_unsent.old", unsentOldFile);
				func_28214_a(statsFolder, "stats_" + session1.username + ".old", oldFile);
				func_28214_a(statsFolder, "stats_" + session1.username + "_unsent.tmp", unsentTempFile);
				func_28214_a(statsFolder, "stats_" + session1.username + ".tmp", tempFile);
			}

			statFileWriter = statFileWriter2;
			theSession = session1;
			if (unsentDataFile.Exists)
			{
				statFileWriter2.func_27179_a(func_27415_a(unsentDataFile, unsentTempFile, unsentOldFile));
			}

			beginReceiveStats();
		}

		private void func_28214_a(DirectoryInfo statsFolder, string string2, FileInfo file3)
		{
			FileInfo file4 = new FileInfo(statsFolder.FullName + '/' + string2);
			if (file4.Exists && !file3.Exists)
			{
				file4.MoveTo(file3.FullName);
			}

		}

		private System.Collections.IDictionary func_27415_a(FileInfo file1, FileInfo file2, FileInfo file3)
		{
			return file1.Exists ? func_27408_a(file1) : (file3.Exists ? func_27408_a(file3) : (file2.Exists ? func_27408_a(file2) : null));
		}

		private System.Collections.IDictionary func_27408_a(FileInfo file1)
		{
			StreamReader bufferedReader2 = null;

			try
			{
                bufferedReader2 = new StreamReader(new FileStream(file1.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read));
                string string3 = "";
				StringBuilder stringBuilder4 = new StringBuilder();

				while (!string.ReferenceEquals((string3 = bufferedReader2.ReadLine()), null))
				{
					stringBuilder4.Append(string3);
				}

				System.Collections.IDictionary map5 = StatFileWriter.getStatsFromJson(stringBuilder4.ToString());
				return map5;
			}
			catch (Exception exception15)
			{
				Console.WriteLine(exception15.ToString());
				Console.Write(exception15.StackTrace);
			}
			finally
			{
				if (bufferedReader2 != null)
				{
					try
					{
						bufferedReader2.Close();
					}
					catch (Exception exception14)
					{
						Console.WriteLine(exception14.ToString());
						Console.Write(exception14.StackTrace);
					}
				}

			}

			return null;
		}
        
		private void func_27410_a(System.Collections.IDictionary map1, FileInfo file2, FileInfo file3, FileInfo file4)
		{
			StreamWriter printWriter5 = new StreamWriter(new FileStream(file3.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite));

			try
			{
				printWriter5.WriteLine(StatFileWriter.getFileContentsToWrite(theSession.username, "local", map1));
			}
			finally
			{
				printWriter5.Close();
				printWriter5.Dispose();
			}

			if (file4.Exists)
			{
				file4.Delete();
			}

			if (file2.Exists)
			{
				file2.MoveTo(file4.FullName);
			}

			file3.MoveTo(file2.FullName, true);
		}

		public virtual void beginReceiveStats()
		{
			if (isBusy)
			{
				throw new System.InvalidOperationException("Can\'t get stats from server while StatsSyncher is busy!");
			}
			else
			{
				field_27427_l = 100;
				isBusy = true;
				(new ThreadStatSyncherReceive(this)).Start();
			}
		}

		public virtual void beginSendStats(System.Collections.IDictionary map1)
		{
			if (isBusy)
			{
				throw new System.InvalidOperationException("Can\'t save stats while StatsSyncher is busy!");
			}
			else
			{
				field_27427_l = 100;
				isBusy = true;
				(new ThreadStatSyncherSend(this, map1)).Start();
			}
		}

		public virtual void syncStatsFileWithMap(System.Collections.IDictionary map1)
		{
			int i2 = 30;

			while (isBusy)
			{
				--i2;
				if (i2 <= 0)
				{
					break;
				}

				Thread.Sleep(100);
			}

			isBusy = true;

			try
			{
				func_27410_a(map1, unsentDataFile, unsentTempFile, unsentOldFile);
			}
			catch (Exception exception8)
			{
				Console.WriteLine(exception8.ToString());
				Console.Write(exception8.StackTrace);
			}
			finally
			{
				isBusy = false;
			}

		}

		public virtual bool func_27420_b()
		{
			return field_27427_l <= 0 && !isBusy && field_27436_c == null;
		}

		public virtual void func_27425_c()
		{
			if (field_27427_l > 0)
			{
				--field_27427_l;
			}

			if (field_27426_m > 0)
			{
				--field_27426_m;
			}

			if (field_27436_c != null)
			{
				statFileWriter.func_27187_c(field_27436_c);
				field_27436_c = null;
			}

			if (field_27437_b != null)
			{
				statFileWriter.func_27180_b(field_27437_b);
				field_27437_b = null;
			}

		}

		internal static System.Collections.IDictionary func_27422_a(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.field_27437_b;
		}

		internal static FileInfo func_27423_b(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.dataFile;
		}

		internal static FileInfo func_27411_c(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.tempFile;
		}

		internal static FileInfo func_27413_d(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.oldFile;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: static void func_27412_a(StatsSyncher statsSyncher0, java.util.Map map1, java.io.File file2, java.io.File file3, java.io.File file4) throws java.io.IOException
		internal static void func_27412_a(StatsSyncher statsSyncher0, System.Collections.IDictionary map1, FileInfo file2, FileInfo file3, FileInfo file4)
		{
			statsSyncher0.func_27410_a(map1, file2, file3, file4);
		}

		internal static System.Collections.IDictionary func_27421_a(StatsSyncher statsSyncher0, System.Collections.IDictionary map1)
		{
			return statsSyncher0.field_27437_b = map1;
		}

		internal static System.Collections.IDictionary func_27409_a(StatsSyncher statsSyncher0, FileInfo file1, FileInfo file2, FileInfo file3)
		{
			return statsSyncher0.func_27415_a(file1, file2, file3);
		}

		internal static bool setBusy(StatsSyncher statsSyncher0, bool z1)
		{
			return statsSyncher0.isBusy = z1;
		}

		internal static FileInfo getUnsentDataFile(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.unsentDataFile;
		}

		internal static FileInfo getUnsentTempFile(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.unsentTempFile;
		}

		internal static FileInfo getUnsentOldFile(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.unsentOldFile;
		}
	}

}