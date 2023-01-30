using System.Collections;

namespace net.minecraft.src
{

	public class NBTTagCompound : NBTBase
	{
		private System.Collections.IDictionary tagMap = new Hashtable();

		public NBTTagCompound() : base("")
		{
		}

		public NBTTagCompound(string string1) : base(string1)
		{
		}
        
		internal override void write(BinaryWriter dataOutput1)
		{
			System.Collections.IEnumerator iterator2 = this.tagMap.Values.GetEnumerator();

			while (iterator2.MoveNext())
			{
				NBTBase nBTBase3 = (NBTBase)iterator2.Current;
				NBTBase.writeNamedTag(nBTBase3, dataOutput1);
			}

			dataOutput1.Write((sbyte)0);
		}
        
		internal override void load(BinaryReader dataInput1)
		{
			this.tagMap.Clear();

			NBTBase nBTBase2;
			while ((nBTBase2 = NBTBase.readNamedTag(dataInput1)).Id != 0)
			{
				this.tagMap[nBTBase2.Name] = nBTBase2;
			}

		}

		public virtual System.Collections.ICollection Tags
		{
			get
			{
				return this.tagMap.Values;
			}
		}

		public override sbyte Id
		{
			get
			{
				return 10;
			}
		}

		public virtual void setTag(string string1, NBTBase nBTBase2)
		{
			this.tagMap[string1] = nBTBase2.setName(string1);
		}

		public virtual void setByte(string string1, sbyte b2)
		{
			this.tagMap[string1] = new NBTTagByte(string1, b2);
		}

		public virtual void setShort(string string1, short s2)
		{
			this.tagMap[string1] = new NBTTagShort(string1, s2);
		}

		public virtual void setInteger(string string1, int i2)
		{
			this.tagMap[string1] = new NBTTagInt(string1, i2);
		}

		public virtual void setLong(string string1, long j2)
		{
			this.tagMap[string1] = new NBTTagLong(string1, j2);
		}

		public virtual void setFloat(string string1, float f2)
		{
			this.tagMap[string1] = new NBTTagFloat(string1, f2);
		}

		public virtual void setDouble(string string1, double d2)
		{
			this.tagMap[string1] = new NBTTagDouble(string1, d2);
		}

		public virtual void setString(string string1, string string2)
		{
			this.tagMap[string1] = new NBTTagString(string1, string2);
		}

		public virtual void setByteArray(string string1, sbyte[] b2)
		{
			this.tagMap[string1] = new NBTTagByteArray(string1, b2);
		}

		public virtual void setIntArray(string string1, int[] i2)
		{
			this.tagMap[string1] = new NBTTagIntArray(string1, i2);
		}

		public virtual void setCompoundTag(string string1, NBTTagCompound nBTTagCompound2)
		{
			this.tagMap[string1] = nBTTagCompound2.setName(string1);
		}

		public virtual void setBoolean(string string1, bool z2)
		{
			this.setByte(string1, (sbyte)(z2 ? 1 : 0));
		}

		public virtual NBTBase getTag(string string1)
		{
			return (NBTBase)this.tagMap[string1];
		}

		public virtual bool hasKey(string string1)
		{
			return this.tagMap.Contains(string1);
		}

		public virtual sbyte getByte(string string1)
		{
			return !this.tagMap.Contains(string1) ? (sbyte)0 : ((NBTTagByte)this.tagMap[string1]).data;
		}

		public virtual short getShort(string string1)
		{
			return !this.tagMap.Contains(string1) ? (short)0 : ((NBTTagShort)this.tagMap[string1]).data;
		}

		public virtual int getInteger(string string1)
		{
			return !this.tagMap.Contains(string1) ? 0 : ((NBTTagInt)this.tagMap[string1]).data;
		}

		public virtual long getLong(string string1)
		{
			return !this.tagMap.Contains(string1) ? 0L : ((NBTTagLong)this.tagMap[string1]).data;
		}

		public virtual float getFloat(string string1)
		{
			return !this.tagMap.Contains(string1) ? 0.0F : ((NBTTagFloat)this.tagMap[string1]).data;
		}

		public virtual double getDouble(string string1)
		{
			return !this.tagMap.Contains(string1) ? 0.0D : ((NBTTagDouble)this.tagMap[string1]).data;
		}

		public virtual string getString(string string1)
		{
			return !this.tagMap.Contains(string1) ? "" : ((NBTTagString)this.tagMap[string1]).data;
		}

		public virtual sbyte[] getByteArray(string string1)
		{
			return !this.tagMap.Contains(string1) ? new sbyte[0] : ((NBTTagByteArray)this.tagMap[string1]).byteArray;
		}

		public virtual int[] getIntArray(string string1)
		{
			return !this.tagMap.Contains(string1) ? new int[0] : ((NBTTagIntArray)this.tagMap[string1]).data;
		}

		public virtual NBTTagCompound getCompoundTag(string string1)
		{
			return !this.tagMap.Contains(string1) ? new NBTTagCompound(string1) : (NBTTagCompound)this.tagMap[string1];
		}

		public virtual NBTTagList getTagList(string string1)
		{
			return !this.tagMap.Contains(string1) ? new NBTTagList(string1) : (NBTTagList)this.tagMap[string1];
		}

		public virtual bool getBoolean(string string1)
		{
			return this.getByte(string1) != 0;
		}

		public override string ToString()
		{
			return "" + this.tagMap.Count + " entries";
		}

		public override NBTBase copy()
		{
			NBTTagCompound nBTTagCompound1 = new NBTTagCompound(this.Name);
			System.Collections.IEnumerator iterator2 = this.tagMap.Keys.GetEnumerator();

			while (iterator2.MoveNext())
			{
				string string3 = (string)iterator2.Current;
				nBTTagCompound1.setTag(string3, ((NBTBase)this.tagMap[string3]).copy());
			}

			return nBTTagCompound1;
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagCompound nBTTagCompound2 = (NBTTagCompound)object1;
				return tagMap.Keys.Equals(nBTTagCompound2.tagMap.Keys) && this.tagMap.Values.Equals(nBTTagCompound2.tagMap.Values);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.tagMap.GetHashCode();
		}
	}

}