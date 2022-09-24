using System.Collections;

namespace net.minecraft.src
{

	public class DataWatcher
	{
		private static readonly Hashtable dataTypes = new Hashtable();
		private readonly System.Collections.IDictionary watchedObjects = new Hashtable();
		private bool objectChanged;

		public virtual void addObject(int i1, object object2)
		{
			int? integer3 = (int?)dataTypes[object2.GetType()];
			if (integer3 == null)
			{
				throw new System.ArgumentException("Unknown data type: " + object2.GetType());
			}
			else if (i1 > 31)
			{
				throw new System.ArgumentException("Data value id is too big with " + i1 + "! (Max is " + 31 + ")");
			}
			else if (this.watchedObjects.Contains(i1))
			{
				throw new System.ArgumentException("Duplicate id value for " + i1 + "!");
			}
			else
			{
				WatchableObject watchableObject4 = new WatchableObject(integer3.Value, i1, object2);
				this.watchedObjects[i1] = watchableObject4;
			}
		}

		public virtual sbyte getWatchableObjectByte(int i1)
		{
			return ((sbyte?)((WatchableObject)this.watchedObjects[i1]).Object).Value;
		}

		public virtual short getWatchableObjectShort(int i1)
		{
			return ((short?)((WatchableObject)this.watchedObjects[i1]).Object).Value;
		}

		public virtual int getWatchableObjectInt(int i1)
		{
			return ((int?)((WatchableObject)this.watchedObjects[i1]).Object).Value;
		}

		public virtual string getWatchableObjectString(int i1)
		{
			return (string)((WatchableObject)this.watchedObjects[i1]).Object;
		}

		public virtual void updateObject(int i1, object object2)
		{
			WatchableObject watchableObject3 = (WatchableObject)this.watchedObjects[i1];
			if (!object2.Equals(watchableObject3.Object))
			{
				watchableObject3.Object = object2;
				watchableObject3.Watching = true;
				this.objectChanged = true;
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public static void writeObjectsInListToStream(java.util.List list0, java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public static void writeObjectsInListToStream(System.Collections.IList list0, DataOutputStream dataOutputStream1)
		{
			if (list0 != null)
			{
				System.Collections.IEnumerator iterator2 = list0.GetEnumerator();

				while (iterator2.MoveNext())
				{
					WatchableObject watchableObject3 = (WatchableObject)iterator2.Current;
					writeWatchableObject(dataOutputStream1, watchableObject3);
				}
			}

			dataOutputStream1.writeByte(127);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public void writeWatchableObjects(java.io.DataOutputStream dataOutputStream1) throws java.io.IOException
		public virtual void writeWatchableObjects(DataOutputStream dataOutputStream1)
		{
			System.Collections.IEnumerator iterator2 = this.watchedObjects.Values.GetEnumerator();

			while (iterator2.MoveNext())
			{
				WatchableObject watchableObject3 = (WatchableObject)iterator2.Current;
				writeWatchableObject(dataOutputStream1, watchableObject3);
			}

			dataOutputStream1.writeByte(127);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: private static void writeWatchableObject(java.io.DataOutputStream dataOutputStream0, WatchableObject watchableObject1) throws java.io.IOException
		private static void writeWatchableObject(DataOutputStream dataOutputStream0, WatchableObject watchableObject1)
		{
			int i2 = (watchableObject1.ObjectType << 5 | watchableObject1.DataValueId & 31) & 255;
			dataOutputStream0.writeByte(i2);
			switch (watchableObject1.ObjectType)
			{
			case 0:
				dataOutputStream0.writeByte(((sbyte?)watchableObject1.Object).Value);
				break;
			case 1:
				dataOutputStream0.writeShort(((short?)watchableObject1.Object).Value);
				break;
			case 2:
				dataOutputStream0.writeInt(((int?)watchableObject1.Object).Value);
				break;
			case 3:
				dataOutputStream0.writeFloat(((float?)watchableObject1.Object).Value);
				break;
			case 4:
				Packet.writeString((string)watchableObject1.Object, dataOutputStream0);
				break;
			case 5:
				ItemStack itemStack4 = (ItemStack)watchableObject1.Object;
				dataOutputStream0.writeShort(itemStack4.Item.shiftedIndex);
				dataOutputStream0.writeByte(itemStack4.stackSize);
				dataOutputStream0.writeShort(itemStack4.ItemDamage);
				break;
			case 6:
				ChunkCoordinates chunkCoordinates3 = (ChunkCoordinates)watchableObject1.Object;
				dataOutputStream0.writeInt(chunkCoordinates3.posX);
				dataOutputStream0.writeInt(chunkCoordinates3.posY);
				dataOutputStream0.writeInt(chunkCoordinates3.posZ);
			break;
			}

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public static java.util.List readWatchableObjects(java.io.DataInputStream dataInputStream0) throws java.io.IOException
		public static System.Collections.IList readWatchableObjects(DataInputStream dataInputStream0)
		{
			ArrayList arrayList1 = null;

			for (sbyte b2 = dataInputStream0.readByte(); b2 != 127; b2 = dataInputStream0.readByte())
			{
				if (arrayList1 == null)
				{
					arrayList1 = new ArrayList();
				}

				int i3 = (b2 & 224) >> 5;
				int i4 = b2 & 31;
				WatchableObject watchableObject5 = null;
				switch (i3)
				{
				case 0:
					watchableObject5 = new WatchableObject(i3, i4, dataInputStream0.readByte());
					break;
				case 1:
					watchableObject5 = new WatchableObject(i3, i4, dataInputStream0.readShort());
					break;
				case 2:
					watchableObject5 = new WatchableObject(i3, i4, dataInputStream0.readInt());
					break;
				case 3:
					watchableObject5 = new WatchableObject(i3, i4, dataInputStream0.readFloat());
					break;
				case 4:
					watchableObject5 = new WatchableObject(i3, i4, Packet.readString(dataInputStream0, 64));
					break;
				case 5:
					short s9 = dataInputStream0.readShort();
					sbyte b10 = dataInputStream0.readByte();
					short s11 = dataInputStream0.readShort();
					watchableObject5 = new WatchableObject(i3, i4, new ItemStack(s9, b10, s11));
					break;
				case 6:
					int i6 = dataInputStream0.readInt();
					int i7 = dataInputStream0.readInt();
					int i8 = dataInputStream0.readInt();
					watchableObject5 = new WatchableObject(i3, i4, new ChunkCoordinates(i6, i7, i8));
				break;
				}

				arrayList1.Add(watchableObject5);
			}

			return arrayList1;
		}

		public virtual void updateWatchedObjectsFromList(System.Collections.IList list1)
		{
			System.Collections.IEnumerator iterator2 = list1.GetEnumerator();

			while (iterator2.MoveNext())
			{
				WatchableObject watchableObject3 = (WatchableObject)iterator2.Current;
				WatchableObject watchableObject4 = (WatchableObject)this.watchedObjects[watchableObject3.DataValueId];
				if (watchableObject4 != null)
				{
					watchableObject4.Object = watchableObject3.Object;
				}
			}

		}

		static DataWatcher()
		{
			dataTypes[typeof(Byte)] = 0;
			dataTypes[typeof(Short)] = 1;
			dataTypes[typeof(Integer)] = 2;
			dataTypes[typeof(Float)] = 3;
			dataTypes[typeof(string)] = 4;
			dataTypes[typeof(ItemStack)] = 5;
			dataTypes[typeof(ChunkCoordinates)] = 6;
		}
	}

}