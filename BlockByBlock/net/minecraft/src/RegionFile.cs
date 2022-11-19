using BlockByBlock.java_extensions;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

// PORT NOTE: This class is untested. Come back to this later. Not *too* much was changed, but there was some I/O conversion work to be done.
namespace net.minecraft.src
{

	public class RegionFile
	{
		private static readonly byte[] emptySector = new byte[4096];
		private readonly FileInfo fileName;
		private FileStream dataFile;
		private BinaryWriter binaryWriter;
		private BinaryReader binaryReader;
		private readonly int[] offsets = new int[1024];
		private readonly int[] chunkTimestamps = new int[1024];
		private ArrayList sectorFree;
		private int sizeDelta;
		private long lastModified = 0L;

		public RegionFile(FileInfo file1)
		{
			this.fileName = file1;
			//this.debugln("REGION LOAD " + this.fileName);
			this.sizeDelta = 0;

			try
			{
				if (file1.Exists)
				{
					this.lastModified = file1.LastWriteTime.Ticks;
				}

                this.dataFile = new FileStream(file1.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				binaryWriter = new BinaryWriter(dataFile);
                binaryReader = new BinaryReader(dataFile);

                int i2;
				if (this.dataFile.Length < 4096L)
				{
					for (i2 = 0; i2 < 1024; ++i2)
					{
						binaryWriter.WriteBigEndian(0);
					}

					for (i2 = 0; i2 < 1024; ++i2)
					{
						binaryWriter.WriteBigEndian(0);
					}

					sizeDelta += 8192;
				}

				if ((dataFile.Length & 4095L) != 0L)
				{
					for (i2 = 0; i2 < (dataFile.Length & 4095L); ++i2)
					{
						binaryWriter.WriteBigEndian(0);
					}
				}

				i2 = (int)dataFile.Length / 4096;
				sectorFree = new ArrayList(i2);

				int i3;
				for (i3 = 0; i3 < i2; ++i3)
				{
					sectorFree.Add(true);
				}

				sectorFree[0] = false;
				sectorFree[1] = false;
				binaryWriter.Seek(0, SeekOrigin.Begin);

				int i4;
				for (i3 = 0; i3 < 1024; ++i3)
				{
					i4 = binaryReader.ReadInt32BigEndian();
					offsets[i3] = i4;
					if (i4 != 0 && (i4 >> 8) + (i4 & 255) <= sectorFree.Count)
					{
						for (int i5 = 0; i5 < (i4 & 255); ++i5)
						{
							sectorFree[(i4 >> 8) + i5] = false;
						}
					}
				}

				for (i3 = 0; i3 < 1024; ++i3)
				{
					i4 = binaryReader.ReadInt32BigEndian();
					chunkTimestamps[i3] = i4;
				}
			}
			catch (IOException iOException6)
			{
				close();
				Console.WriteLine(iOException6.ToString());
				Console.Write(iOException6.StackTrace);
			}

		}

		private void debug(string string1)
		{
			Console.Write(string1);
		}

		private void debugln(string string1)
		{
			this.debug(string1 + "\n");
		}

		private void debug(string string1, int i2, int i3, string string4)
		{
			this.debug("REGION " + string1 + " " + this.fileName.Name + "[" + i2 + "," + i3 + "] = " + string4);
		}

		private void debug(string string1, int i2, int i3, int i4, string string5)
		{
			this.debug("REGION " + string1 + " " + this.fileName.Name + "[" + i2 + "," + i3 + "] " + i4 + "B = " + string5);
		}

		private void debugln(string string1, int i2, int i3, string string4)
		{
			this.debug(string1, i2, i3, string4 + "\n");
		}
        
		public virtual BinaryReader getChunkDataInputStream(int i1, int i2)
		{
			lock (this)
			{
				if (this.outOfBounds(i1, i2))
				{
					this.debugln("READ", i1, i2, "out of bounds");
					return null;
				}
				else
				{
					try
					{
						int i3 = this.getOffset(i1, i2);
						if (i3 == 0)
						{
							return null;
						}
						else
						{
							int i4 = i3 >> 8;
							int i5 = i3 & 255;
							if (i4 + i5 > this.sectorFree.Count)
							{
								this.debugln("READ", i1, i2, "invalid sector");
								return null;
							}
							else
							{
								binaryReader.BaseStream.Seek((long)(i4 * 4096), SeekOrigin.Begin);
								int i6 = binaryReader.ReadInt32BigEndian();
								if (i6 > 4096 * i5)
								{
									this.debugln("READ", i1, i2, "invalid length: " + i6 + " > 4096 * " + i5);
									return null;
								}
								else if (i6 <= 0)
								{
									this.debugln("READ", i1, i2, "invalid length: " + i6 + " < 1");
									return null;
								}
								else
								{
									sbyte b7 = binaryReader.ReadSByte();
									byte[] b8;
									BinaryReader dataInputStream9;
									if (b7 == 1)
									{
										b8 = new byte[i6 - 1];
										binaryReader.Read(b8, 0, b8.Length);
                                        
										dataInputStream9 = new BinaryReader(new GZipStream(new MemoryStream(b8), CompressionMode.Decompress));
										return dataInputStream9;
									}
									else if (b7 == 2)
									{
										b8 = new byte[i6 - 1];
										binaryReader.Read(b8, 0, b8.Length);
                                        
										dataInputStream9 = new BinaryReader(new InflaterInputStream(new MemoryStream(b8)));
										return dataInputStream9;
									}
									else
									{
										debugln("READ", i1, i2, "unknown version " + b7);
										return null;
									}
								}
							}
						}
					}
					catch (IOException)
					{
						this.debugln("READ", i1, i2, "exception");
						return null;
					}
				}
			}
		}

		public virtual BinaryWriter? getChunkDataOutputStream(int i1, int i2)
		{
			return outOfBounds(i1, i2) ? null : new BinaryWriter(new DeflaterOutputStream(new RegionFileChunkBuffer(this, i1, i2)));
		}

		protected internal virtual void write(int i1, int i2, byte[] b3, int i4)
		{
			lock (this)
			{
				try
				{
					int i5 = this.getOffset(i1, i2);
					int i6 = i5 >> 8;
					int i7 = i5 & 255;
					int i8 = (i4 + 5) / 4096 + 1;
					if (i8 >= 256)
					{
						return;
					}
        
					if (i6 != 0 && i7 == i8)
					{
						//this.debug("SAVE", i1, i2, i4, "rewrite");
						this.write(i6, b3, i4);
					}
					else
					{
						int i9;
						for (i9 = 0; i9 < i7; ++i9)
						{
							this.sectorFree[i6 + i9] = true;
						}
        
						i9 = this.sectorFree.IndexOf(true);
						int i10 = 0;
						int i11;
						if (i9 != -1)
						{
							for (i11 = i9; i11 < this.sectorFree.Count; ++i11)
							{
								if (i10 != 0)
								{
									if (((bool?)this.sectorFree[i11]).Value)
									{
										++i10;
									}
									else
									{
										i10 = 0;
									}
								}
								else if (((bool?)this.sectorFree[i11]).Value)
								{
									i9 = i11;
									i10 = 1;
								}
        
								if (i10 >= i8)
								{
									break;
								}
							}
						}
        
						if (i10 >= i8)
						{
							//this.debug("SAVE", i1, i2, i4, "reuse");
							i6 = i9;
							this.setOffset(i1, i2, i9 << 8 | i8);
        
							for (i11 = 0; i11 < i8; ++i11)
							{
								this.sectorFree[i6 + i11] = false;
							}
        
							this.write(i6, b3, i4);
						}
						else
						{
							//this.debug("SAVE", i1, i2, i4, "grow");
							binaryWriter.Seek((int)dataFile.Length, SeekOrigin.Begin);
							i6 = this.sectorFree.Count;
        
							for (i11 = 0; i11 < i8; ++i11)
							{
								binaryWriter.Write(emptySector);
								this.sectorFree.Add(false);
							}
        
							this.sizeDelta += 4096 * i8;
							this.write(i6, b3, i4);
							this.setOffset(i1, i2, i6 << 8 | i8);
						}
					}
        
					this.setChunkTimestamp(i1, i2, (int)(DateTimeHelper.CurrentUnixTimeMillis() / 1000L));
				}
				catch (IOException iOException12)
				{
					Console.WriteLine(iOException12.ToString());
					Console.Write(iOException12.StackTrace);
				}
        
			}
		}
        
		private void write(int i1, byte[] b2, int i3)
		{
			binaryWriter.Seek(i1 * 4096, SeekOrigin.Begin);
			binaryWriter.WriteBigEndian(i3 + 1);
			binaryWriter.Write((sbyte)2);
			binaryWriter.Write(b2, 0, i3);
		}

		private bool outOfBounds(int i1, int i2)
		{
			return i1 < 0 || i1 >= 32 || i2 < 0 || i2 >= 32;
		}

		private int getOffset(int i1, int i2)
		{
			return this.offsets[i1 + i2 * 32];
		}

		public virtual bool isChunkSaved(int x, int z)
		{
			return this.getOffset(x, z) != 0;
		}

		private void setOffset(int i1, int i2, int value)
		{
			this.offsets[i1 + i2 * 32] = value;
			binaryWriter.Seek(((i1 + i2 * 32) * 4), SeekOrigin.Begin);
			binaryWriter.WriteBigEndian(value);
		}
        
		private void setChunkTimestamp(int i1, int i2, int timeStamp)
		{
			this.chunkTimestamps[i1 + i2 * 32] = timeStamp;
			binaryWriter.Seek((4096 + (i1 + i2 * 32) * 4), SeekOrigin.Begin);
			binaryWriter.WriteBigEndian(timeStamp);
		}
        
		public virtual void close()
		{
			dataFile.Dispose();
		}
	}

}