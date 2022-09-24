namespace net.minecraft.src
{
	internal class LongHashMapEntry
	{
		internal readonly long key;
		internal object value;
		internal LongHashMapEntry nextEntry;
		internal readonly int hash;

		internal LongHashMapEntry(int i1, long j2, object object4, LongHashMapEntry longHashMapEntry5)
		{
			this.value = object4;
			this.nextEntry = longHashMapEntry5;
			this.key = j2;
			this.hash = i1;
		}

		public long Key
		{
			get
			{
				return this.key;
			}
		}

		public object Value
		{
			get
			{
				return this.value;
			}
		}

		public sealed override bool Equals(object object1)
		{
			if (!(object1 is LongHashMapEntry))
			{
				return false;
			}
			else
			{
				LongHashMapEntry longHashMapEntry2 = (LongHashMapEntry)object1;
				long? long3 = this.Key;
				long? long4 = longHashMapEntry2.Key;
				if (long3 == long4 || long3 != null && long3.Equals(long4))
				{
					object object5 = this.Value;
					object object6 = longHashMapEntry2.Value;
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
			return LongHashMap.getHashCode(this.key);
		}

		public sealed override string ToString()
		{
			return this.Key + "=" + this.Value;
		}
	}

}