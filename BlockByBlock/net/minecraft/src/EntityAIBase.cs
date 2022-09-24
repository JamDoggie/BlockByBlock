namespace net.minecraft.src
{
	public abstract class EntityAIBase
	{
		private int mutexBits = 0;

		public abstract bool shouldExecute();

		public virtual bool continueExecuting()
		{
			return this.shouldExecute();
		}

		public virtual bool Continuous
		{
			get
			{
				return true;
			}
		}

		public virtual void startExecuting()
		{
		}

		public virtual void resetTask()
		{
		}

		public virtual void updateTask()
		{
		}

		public virtual int MutexBits
		{
			set
			{
				this.mutexBits = value;
			}
			get
			{
				return this.mutexBits;
			}
		}

	}

}