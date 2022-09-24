namespace net.minecraft.src
{
	internal class IntHashMapEntry
	{
		internal readonly int hashEntry;
		internal object valueEntry;
		internal IntHashMapEntry nextEntry;
		internal readonly int slotHash;

		internal IntHashMapEntry(int i1, int i2, object object3, IntHashMapEntry intHashMapEntry4)
		{
			this.valueEntry = object3;
			this.nextEntry = intHashMapEntry4;
			this.hashEntry = i2;
			this.slotHash = i1;
		}

		public int Hash
		{
			get
			{
				return this.hashEntry;
			}
		}

		public object Value
		{
			get
			{
				return this.valueEntry;
			}
		}

		public sealed override bool Equals(object object1)
		{
			if (!(object1 is IntHashMapEntry))
			{
				return false;
			}
			else
			{
				IntHashMapEntry intHashMapEntry2 = (IntHashMapEntry)object1;
				int? integer3 = this.Hash;
				int? integer4 = intHashMapEntry2.Hash;
				if (integer3 == integer4 || integer3 != null && integer3.Equals(integer4))
				{
					object object5 = this.Value;
					object object6 = intHashMapEntry2.Value;
					if (object5 == object6 || object5 != null && object5.Equals(object6))
					{
						return true;
					}
				}

				return false;
			}
		}

		public sealed override int GetHashCode()
		{
			return IntHashMap.getHash(this.hashEntry);
		}

		public sealed override string ToString()
		{
			return this.Hash + "=" + this.Value;
		}
	}

}