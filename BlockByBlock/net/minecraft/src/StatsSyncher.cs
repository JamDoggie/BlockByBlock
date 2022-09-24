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
		private File unsentDataFile;
		private File dataFile;
		private File unsentTempFile;
		private File tempFile;
		private File unsentOldFile;
		private File oldFile;
		private Session theSession;
		private int field_27427_l = 0;
		private int field_27426_m = 0;

		public StatsSyncher(Session session1, StatFileWriter statFileWriter2, File file3)
		{
			this.unsentDataFile = new File(file3, "stats_" + session1.username.ToLower() + "_unsent.dat");
			this.dataFile = new File(file3, "stats_" + session1.username.ToLower() + ".dat");
			this.unsentOldFile = new File(file3, "stats_" + session1.username.ToLower() + "_unsent.old");
			this.oldFile = new File(file3, "stats_" + session1.username.ToLower() + ".old");
			this.unsentTempFile = new File(file3, "stats_" + session1.username.ToLower() + "_unsent.tmp");
			this.tempFile = new File(file3, "stats_" + session1.username.ToLower() + ".tmp");
			if (!session1.username.ToLower().Equals(session1.username))
			{
				this.func_28214_a(file3, "stats_" + session1.username + "_unsent.dat", this.unsentDataFile);
				this.func_28214_a(file3, "stats_" + session1.username + ".dat", this.dataFile);
				this.func_28214_a(file3, "stats_" + session1.username + "_unsent.old", this.unsentOldFile);
				this.func_28214_a(file3, "stats_" + session1.username + ".old", this.oldFile);
				this.func_28214_a(file3, "stats_" + session1.username + "_unsent.tmp", this.unsentTempFile);
				this.func_28214_a(file3, "stats_" + session1.username + ".tmp", this.tempFile);
			}

			this.statFileWriter = statFileWriter2;
			this.theSession = session1;
			if (this.unsentDataFile.exists())
			{
				statFileWriter2.func_27179_a(this.func_27415_a(this.unsentDataFile, this.unsentTempFile, this.unsentOldFile));
			}

			this.beginReceiveStats();
		}

		private void func_28214_a(File file1, string string2, File file3)
		{
			File file4 = new File(file1, string2);
			if (file4.exists() && !file4.isDirectory() && !file3.exists())
			{
				file4.renameTo(file3);
			}

		}

		private System.Collections.IDictionary func_27415_a(File file1, File file2, File file3)
		{
			return file1.exists() ? this.func_27408_a(file1) : (file3.exists() ? this.func_27408_a(file3) : (file2.exists() ? this.func_27408_a(file2) : null));
		}

		private System.Collections.IDictionary func_27408_a(File file1)
		{
			StreamReader bufferedReader2 = null;

			try
			{
				bufferedReader2 = new StreamReader(file1);
				string string3 = "";
				StringBuilder stringBuilder4 = new StringBuilder();

				while (!string.ReferenceEquals((string3 = bufferedReader2.ReadLine()), null))
				{
					stringBuilder4.Append(string3);
				}

				System.Collections.IDictionary map5 = StatFileWriter.func_27177_a(stringBuilder4.ToString());
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

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: private void func_27410_a(java.util.Map map1, java.io.File file2, java.io.File file3, java.io.File file4) throws java.io.IOException
		private void func_27410_a(System.Collections.IDictionary map1, File file2, File file3, File file4)
		{
			PrintWriter printWriter5 = new PrintWriter(new StreamWriter(file3, false));

			try
			{
				printWriter5.print(StatFileWriter.func_27185_a(this.theSession.username, "local", map1));
			}
			finally
			{
				printWriter5.close();
			}

			if (file4.exists())
			{
				file4.delete();
			}

			if (file2.exists())
			{
				file2.renameTo(file4);
			}

			file3.renameTo(file2);
		}

		public virtual void beginReceiveStats()
		{
			if (this.isBusy)
			{
				throw new System.InvalidOperationException("Can\'t get stats from server while StatsSyncher is busy!");
			}
			else
			{
				this.field_27427_l = 100;
				this.isBusy = true;
				(new ThreadStatSyncherReceive(this)).Start();
			}
		}

		public virtual void beginSendStats(System.Collections.IDictionary map1)
		{
			if (this.isBusy)
			{
				throw new System.InvalidOperationException("Can\'t save stats while StatsSyncher is busy!");
			}
			else
			{
				this.field_27427_l = 100;
				this.isBusy = true;
				(new ThreadStatSyncherSend(this, map1)).Start();
			}
		}

		public virtual void syncStatsFileWithMap(System.Collections.IDictionary map1)
		{
			int i2 = 30;

			while (this.isBusy)
			{
				--i2;
				if (i2 <= 0)
				{
					break;
				}

				try
				{
					Thread.Sleep(100L);
				}
				catch (InterruptedException interruptedException10)
				{
					Console.WriteLine(interruptedException10.ToString());
					Console.Write(interruptedException10.StackTrace);
				}
			}

			this.isBusy = true;

			try
			{
				this.func_27410_a(map1, this.unsentDataFile, this.unsentTempFile, this.unsentOldFile);
			}
			catch (Exception exception8)
			{
				Console.WriteLine(exception8.ToString());
				Console.Write(exception8.StackTrace);
			}
			finally
			{
				this.isBusy = false;
			}

		}

		public virtual bool func_27420_b()
		{
			return this.field_27427_l <= 0 && !this.isBusy && this.field_27436_c == null;
		}

		public virtual void func_27425_c()
		{
			if (this.field_27427_l > 0)
			{
				--this.field_27427_l;
			}

			if (this.field_27426_m > 0)
			{
				--this.field_27426_m;
			}

			if (this.field_27436_c != null)
			{
				this.statFileWriter.func_27187_c(this.field_27436_c);
				this.field_27436_c = null;
			}

			if (this.field_27437_b != null)
			{
				this.statFileWriter.func_27180_b(this.field_27437_b);
				this.field_27437_b = null;
			}

		}

		internal static System.Collections.IDictionary func_27422_a(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.field_27437_b;
		}

		internal static File func_27423_b(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.dataFile;
		}

		internal static File func_27411_c(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.tempFile;
		}

		internal static File func_27413_d(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.oldFile;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: static void func_27412_a(StatsSyncher statsSyncher0, java.util.Map map1, java.io.File file2, java.io.File file3, java.io.File file4) throws java.io.IOException
		internal static void func_27412_a(StatsSyncher statsSyncher0, System.Collections.IDictionary map1, File file2, File file3, File file4)
		{
			statsSyncher0.func_27410_a(map1, file2, file3, file4);
		}

		internal static System.Collections.IDictionary func_27421_a(StatsSyncher statsSyncher0, System.Collections.IDictionary map1)
		{
			return statsSyncher0.field_27437_b = map1;
		}

		internal static System.Collections.IDictionary func_27409_a(StatsSyncher statsSyncher0, File file1, File file2, File file3)
		{
			return statsSyncher0.func_27415_a(file1, file2, file3);
		}

		internal static bool setBusy(StatsSyncher statsSyncher0, bool z1)
		{
			return statsSyncher0.isBusy = z1;
		}

		internal static File getUnsentDataFile(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.unsentDataFile;
		}

		internal static File getUnsentTempFile(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.unsentTempFile;
		}

		internal static File getUnsentOldFile(StatsSyncher statsSyncher0)
		{
			return statsSyncher0.unsentOldFile;
		}
	}

}