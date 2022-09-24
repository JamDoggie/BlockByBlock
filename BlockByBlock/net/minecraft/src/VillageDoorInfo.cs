namespace net.minecraft.src
{
	public class VillageDoorInfo
	{
		public readonly int posX;
		public readonly int posY;
		public readonly int posZ;
		public readonly int insideDirectionX;
		public readonly int insideDirectionZ;
		public int lastActivityTimestamp;
		public bool isDetachedFromVillageFlag = false;
		private int doorOpeningRestrictionCounter = 0;

		public VillageDoorInfo(int i1, int i2, int i3, int i4, int i5, int i6)
		{
			this.posX = i1;
			this.posY = i2;
			this.posZ = i3;
			this.insideDirectionX = i4;
			this.insideDirectionZ = i5;
			this.lastActivityTimestamp = i6;
		}

		public virtual int getDistanceSquared(int i1, int i2, int i3)
		{
			int i4 = i1 - this.posX;
			int i5 = i2 - this.posY;
			int i6 = i3 - this.posZ;
			return i4 * i4 + i5 * i5 + i6 * i6;
		}

		public virtual int getInsideDistanceSquare(int i1, int i2, int i3)
		{
			int i4 = i1 - this.posX - this.insideDirectionX;
			int i5 = i2 - this.posY;
			int i6 = i3 - this.posZ - this.insideDirectionZ;
			return i4 * i4 + i5 * i5 + i6 * i6;
		}

		public virtual int InsidePosX
		{
			get
			{
				return this.posX + this.insideDirectionX;
			}
		}

		public virtual int InsidePosY
		{
			get
			{
				return this.posY;
			}
		}

		public virtual int InsidePosZ
		{
			get
			{
				return this.posZ + this.insideDirectionZ;
			}
		}

		public virtual bool isInside(int i1, int i2)
		{
			int i3 = i1 - this.posX;
			int i4 = i2 - this.posZ;
			return i3 * this.insideDirectionX + i4 * this.insideDirectionZ >= 0;
		}

		public virtual void resetDoorOpeningRestrictionCounter()
		{
			this.doorOpeningRestrictionCounter = 0;
		}

		public virtual void incrementDoorOpeningRestrictionCounter()
		{
			++this.doorOpeningRestrictionCounter;
		}

		public virtual int DoorOpeningRestrictionCounter
		{
			get
			{
				return this.doorOpeningRestrictionCounter;
			}
		}
	}

}