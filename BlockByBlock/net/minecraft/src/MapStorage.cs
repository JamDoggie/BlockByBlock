using BlockByBlock.java_extensions;
using System;
using System.Collections;
using System.IO;

namespace net.minecraft.src
{

	public class MapStorage
	{
		private ISaveHandler saveHandler;
		private System.Collections.IDictionary loadedDataMap = new Hashtable();
		private System.Collections.IList loadedDataList = new ArrayList();
		private System.Collections.IDictionary idCounts = new Hashtable();

		public MapStorage(ISaveHandler iSaveHandler1)
		{
			this.saveHandler = iSaveHandler1;
			this.loadIdCounts();
		}

		public virtual WorldSavedData loadData(Type class1, string string2)
		{
			WorldSavedData worldSavedData3 = (WorldSavedData)this.loadedDataMap[string2];
			if (worldSavedData3 != null)
			{
				return worldSavedData3;
			}
			else
			{
				if (this.saveHandler != null)
				{
					try
					{
						FileInfo file4 = saveHandler.getMapFileFromName(string2);
						if (file4 != null && file4.Exists)
						{
							try
							{
                                worldSavedData3 = (WorldSavedData)Activator.CreateInstance(class1, new object[] { string2 });
                            }
							catch (Exception exception7)
							{
								throw new Exception("Failed to instantiate " + class1.ToString(), exception7);
							}

							FileStream fileInputStream5 = new FileStream(file4.FullName, FileMode.Open, FileAccess.Read);
							NBTTagCompound nBTTagCompound6 = CompressedStreamTools.readCompressed(fileInputStream5);
							fileInputStream5.Close();
							worldSavedData3.readFromNBT(nBTTagCompound6.getCompoundTag("data"));
						}
					}
					catch (Exception exception8)
					{
						Console.WriteLine(exception8.ToString());
						Console.Write(exception8.StackTrace);
					}
				}

				if (worldSavedData3 != null)
				{
					this.loadedDataMap[string2] = worldSavedData3;
					this.loadedDataList.Add(worldSavedData3);
				}

				return worldSavedData3;
			}
		}

		public virtual void setData(string string1, WorldSavedData worldSavedData2)
		{
			if (worldSavedData2 == null)
			{
				throw new Exception("Can\'t set null data");
			}
			else
			{
				if (this.loadedDataMap.Contains(string1))
				{
					this.loadedDataList.Remove(loadedDataMap.RemoveAndReturn(string1));
				}

				this.loadedDataMap[string1] = worldSavedData2;
				this.loadedDataList.Add(worldSavedData2);
			}
		}

		public virtual void saveAllData()
		{
			for (int i1 = 0; i1 < this.loadedDataList.Count; ++i1)
			{
				WorldSavedData worldSavedData2 = (WorldSavedData)this.loadedDataList[i1];
				if (worldSavedData2.Dirty)
				{
					this.saveData(worldSavedData2);
					worldSavedData2.Dirty = false;
				}
			}

		}

		private void saveData(WorldSavedData worldSavedData1)
		{
			if (this.saveHandler != null)
			{
				try
				{
					FileInfo file2 = saveHandler.getMapFileFromName(worldSavedData1.mapName);
					if (file2 != null)
					{
						NBTTagCompound nBTTagCompound3 = new NBTTagCompound();
						worldSavedData1.writeToNBT(nBTTagCompound3);
						NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
						nBTTagCompound4.setCompoundTag("data", nBTTagCompound3);
						FileStream fileOutputStream5 = new FileStream(file2.FullName, FileMode.Create, FileAccess.Write);
						CompressedStreamTools.writeCompressed(nBTTagCompound4, fileOutputStream5);
						fileOutputStream5.Close();
					}
				}
				catch (Exception exception6)
				{
					Console.WriteLine(exception6.ToString());
					Console.Write(exception6.StackTrace);
				}

			}
		}

		private void loadIdCounts()
		{
			try
			{
				this.idCounts.Clear();
				if (this.saveHandler == null)
				{
					return;
				}

				FileInfo file1 = saveHandler.getMapFileFromName("idcounts");
				if (file1 != null && file1.Exists)
				{
					BinaryReader dataInputStream2 = new BinaryReader(new FileStream(file1.FullName, FileMode.Open, FileAccess.Read));
					NBTTagCompound nBTTagCompound3 = CompressedStreamTools.read(dataInputStream2);
					dataInputStream2.Dispose();
					System.Collections.IEnumerator iterator4 = nBTTagCompound3.Tags.GetEnumerator();

					while (iterator4.MoveNext())
					{
						NBTBase nBTBase5 = (NBTBase)iterator4.Current;
						if (nBTBase5 is NBTTagShort)
						{
							NBTTagShort nBTTagShort6 = (NBTTagShort)nBTBase5;
							string string7 = nBTTagShort6.Name;
							short s8 = nBTTagShort6.data;
							this.idCounts[string7] = s8;
						}
					}
				}
			}
			catch (Exception exception9)
			{
				Console.WriteLine(exception9.ToString());
				Console.Write(exception9.StackTrace);
			}

		}

		public virtual int getUniqueDataId(string string1)
		{
			short? short2 = (short?)this.idCounts[string1];
			if (short2 == null)
			{
				short2 = (short)0;
			}
			else
			{
				short2 = (short)(short2.Value + 1);
			}

			this.idCounts[string1] = short2;
			if (this.saveHandler == null)
			{
				return short2.Value;
			}
			else
			{
				try
				{
					FileInfo file3 = saveHandler.getMapFileFromName("idcounts");
					if (file3 != null)
					{
						NBTTagCompound nBTTagCompound4 = new NBTTagCompound();
						System.Collections.IEnumerator iterator5 = this.idCounts.Keys.GetEnumerator();

						while (iterator5.MoveNext())
						{
							string string6 = (string)iterator5.Current;
							short s7 = ((short?)this.idCounts[string6]).Value;
							nBTTagCompound4.setShort(string6, s7);
						}

						BinaryWriter dataOutputStream9 = new BinaryWriter(new FileStream(file3.FullName, FileMode.Create, FileAccess.Write));
						CompressedStreamTools.write(nBTTagCompound4, dataOutputStream9);
						dataOutputStream9.Dispose();
					}
				}
				catch (Exception exception8)
				{
					Console.WriteLine(exception8.ToString());
					Console.Write(exception8.StackTrace);
				}

				return short2.Value;
			}
		}
	}

}