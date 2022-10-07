using System.Buffers.Binary;
using System.Collections;
using System.Linq;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public class NBTTagList : NBTBase
	{
		private List<NBTBase> tagList = new();
		private sbyte tagType;

		public NBTTagList() : base("")
		{
		}

		public NBTTagList(string string1) : base(string1)
		{
		}

		internal override void write(BinaryWriter dataOutput1)
		{
			if (tagList.Count > 0)
			{
				tagType = ((NBTBase)this.tagList[0]).Id;
			}
			else
			{
				tagType = 1;
			}

			dataOutput1.Write(tagType);
			dataOutput1.WriteBigEndian(tagList.Count);

			for (int i2 = 0; i2 < tagList.Count; ++i2)
			{
				((NBTBase)tagList[i2]).write(dataOutput1);
			}

		}
        
		internal override void load(BinaryReader dataInput1)
		{
			tagType = dataInput1.ReadSByte();
			int i2 = dataInput1.ReadInt32BigEndian();
			tagList = new();

			for (int i3 = 0; i3 < i2; ++i3)
			{
				NBTBase nBTBase4 = newTag(tagType, null);
				nBTBase4.load(dataInput1);
				tagList.Add(nBTBase4);
			}

		}

		public override sbyte Id
		{
			get
			{
				return (sbyte)9;
			}
		}

		public override string ToString()
		{
			return "" + this.tagList.Count + " entries of type " + NBTBase.getTagName(this.tagType);
		}

		public virtual void appendTag(NBTBase nBTBase1)
		{
			this.tagType = nBTBase1.Id;
			this.tagList.Add(nBTBase1);
		}

		public virtual NBTBase tagAt(int i1)
		{
			return (NBTBase)this.tagList[i1];
		}

		public virtual int tagCount()
		{
			return this.tagList.Count;
		}

		public override NBTBase copy()
		{
			NBTTagList nBTTagList1 = new NBTTagList(this.Name);
			nBTTagList1.tagType = this.tagType;
			System.Collections.IEnumerator iterator2 = this.tagList.GetEnumerator();

			while (iterator2.MoveNext())
			{
				NBTBase nBTBase3 = (NBTBase)iterator2.Current;
				NBTBase nBTBase4 = nBTBase3.copy();
				nBTTagList1.tagList.Add(nBTBase4);
			}

			return nBTTagList1;
		}

		public override bool Equals(object object1)
		{
			if (base.Equals(object1))
			{
				NBTTagList nBTTagList2 = (NBTTagList)object1;
				if (this.tagType == nBTTagList2.tagType)
				{
//JAVA TO C# CONVERTER WARNING: LINQ 'SequenceEqual' is not always identical to Java AbstractList 'equals':
//ORIGINAL LINE: return this.tagList.equals(nBTTagList2.tagList);
					return this.tagList.SequenceEqual(nBTTagList2.tagList);
				}
			}

			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.tagList.GetHashCode();
		}
	}

}