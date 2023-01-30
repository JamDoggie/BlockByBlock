using BlockByBlock.helpers;
using BlockByBlock.java_extensions;

namespace net.minecraft.src
{

	public abstract class NBTBase
	{
		private string name;

		internal abstract void write(BinaryWriter dataOutput1);
        
		internal abstract void load(BinaryReader dataInput1);

		public abstract sbyte Id {get;}
        
		protected internal NBTBase(string string1)
		{
			if (string.ReferenceEquals(string1, null))
			{
				this.name = "";
			}
			else
			{
				this.name = string1;
			}

		}

		public virtual NBTBase setName(string string1)
		{
			if (string.ReferenceEquals(string1, null))
			{
				this.name = "";
			}
			else
			{
				this.name = string1;
			}

			return this;
		}

		public virtual string Name
		{
			get
			{
				return string.ReferenceEquals(this.name, null) ? "" : this.name;
			}
		}

		public static NBTBase readNamedTag(BinaryReader dataInput0)
		{
            sbyte b1 = dataInput0.ReadSByte();
			if (b1 == 0)
			{
				return new NBTTagEnd();
			}
			else
			{
				string string2 = dataInput0.ReadUTF();
				NBTBase nBTBase3 = newTag(b1, string2);
				nBTBase3.load(dataInput0);
				return nBTBase3;
			}
		}
        
		public static void writeNamedTag(NBTBase nBTBase0, BinaryWriter dataOutput1)
		{
			dataOutput1.Write(nBTBase0.Id);
			if (nBTBase0.Id != 0)
			{
				dataOutput1.WriteUTF(nBTBase0.Name);
				nBTBase0.write(dataOutput1);
			}
		}

		public static NBTBase newTag(sbyte b0, string string1)
		{
            switch (b0)
			{
			case 0:
				return new NBTTagEnd();
			case 1:
				return new NBTTagByte(string1);
			case 2:
				return new NBTTagShort(string1);
			case 3:
				return new NBTTagInt(string1);
			case 4:
				return new NBTTagLong(string1);
			case 5:
				return new NBTTagFloat(string1);
			case 6:
				return new NBTTagDouble(string1);
			case 7:
				return new NBTTagByteArray(string1);
			case 8:
				return new NBTTagString(string1);
			case 9:
				return new NBTTagList(string1);
			case 10:
				return new NBTTagCompound(string1);
			case 11:
				return new NBTTagIntArray(string1);
			default:
				return null;
			}
		}

		public static string getTagName(sbyte b0)
		{
			switch (b0)
			{
			case 0:
				return "TAG_End";
			case 1:
				return "TAG_Byte";
			case 2:
				return "TAG_Short";
			case 3:
				return "TAG_Int";
			case 4:
				return "TAG_Long";
			case 5:
				return "TAG_Float";
			case 6:
				return "TAG_Double";
			case 7:
				return "TAG_Byte_Array";
			case 8:
				return "TAG_String";
			case 9:
				return "TAG_List";
			case 10:
				return "TAG_Compound";
			case 11:
				return "TAG_Int_Array";
			default:
				return "UNKNOWN";
			}
		}

		public abstract NBTBase copy();

		public override bool Equals(object object1)
		{
			if (object1 != null && object1 is NBTBase)
			{
				NBTBase nBTBase2 = (NBTBase)object1;
				return this.Id != nBTBase2.Id ? false : (string.ReferenceEquals(this.name, null) && !string.ReferenceEquals(nBTBase2.name, null) || !string.ReferenceEquals(this.name, null) && string.ReferenceEquals(nBTBase2.name, null) ? false : string.ReferenceEquals(this.name, null) || this.name.Equals(nBTBase2.name));
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ this.Id;
		}
	}

}