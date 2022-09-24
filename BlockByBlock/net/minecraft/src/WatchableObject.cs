namespace net.minecraft.src
{
	public class WatchableObject
	{
		private readonly int objectType;
		private readonly int dataValueId;
		private object watchedObject;
		private bool isWatching;

		public WatchableObject(int i1, int i2, object object3)
		{
			this.dataValueId = i2;
			this.watchedObject = object3;
			this.objectType = i1;
			this.isWatching = true;
		}

		public virtual int DataValueId
		{
			get
			{
				return this.dataValueId;
			}
		}

		public virtual object Object
		{
			set
			{
				this.watchedObject = value;
			}
			get
			{
				return this.watchedObject;
			}
		}


		public virtual int ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		public virtual bool Watching
		{
			set
			{
				this.isWatching = value;
			}
		}
	}

}