namespace net.minecraft.src
{
	internal class EntityAITaskEntry
	{
		public EntityAIBase action;
		public int priority;
		internal readonly EntityAITasks tasks;

		public EntityAITaskEntry(EntityAITasks entityAITasks1, int i2, EntityAIBase entityAIBase3)
		{
			this.tasks = entityAITasks1;
			this.priority = i2;
			this.action = entityAIBase3;
		}
	}

}