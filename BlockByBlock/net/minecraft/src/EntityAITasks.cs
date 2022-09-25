using System;
using System.Collections;

namespace net.minecraft.src
{

	public class EntityAITasks
	{
		private ArrayList tasksToDo = new ArrayList();
		private ArrayList executingTasks = new ArrayList();

		public virtual void addTask(int i1, EntityAIBase entityAIBase2)
		{
			this.tasksToDo.Add(new EntityAITaskEntry(this, i1, entityAIBase2));
		}

		public virtual void onUpdateTasks()
		{
			ArrayList arrayList1 = new ArrayList();
			System.Collections.IEnumerator iterator2 = this.tasksToDo.GetEnumerator();

			while (true)
			{
				EntityAITaskEntry entityAITaskEntry3;
				while (true)
				{
					// JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					if (!iterator2.MoveNext())
					{
						bool z5 = false;
						if (z5 && arrayList1.Count > 0)
						{
							Console.WriteLine("Starting: ");
						}

						System.Collections.IEnumerator iterator6;
						EntityAITaskEntry entityAITaskEntry7;
						for (iterator6 = arrayList1.GetEnumerator(); iterator6.MoveNext(); entityAITaskEntry7.action.startExecuting())
						{
							entityAITaskEntry7 = (EntityAITaskEntry)iterator6.Current;
							if (z5)
							{
								Console.WriteLine(entityAITaskEntry7.action.ToString() + ", ");
							}
						}

						if (z5 && this.executingTasks.Count > 0)
						{
							Console.WriteLine("Running: ");
						}

						for (iterator6 = this.executingTasks.GetEnumerator(); iterator6.MoveNext(); entityAITaskEntry7.action.updateTask())
						{
							entityAITaskEntry7 = (EntityAITaskEntry)iterator6.Current;
							if (z5)
							{
								Console.WriteLine(entityAITaskEntry7.action.ToString());
							}
						}

						return;
					}

					// JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
					entityAITaskEntry3 = (EntityAITaskEntry)iterator2.Current;
					bool z4 = this.executingTasks.Contains(entityAITaskEntry3);
					if (!z4)
					{
						break;
					}

					if (!this.func_46116_a(entityAITaskEntry3) || !entityAITaskEntry3.action.continueExecuting())
					{
						entityAITaskEntry3.action.resetTask();
						this.executingTasks.Remove(entityAITaskEntry3);
						break;
					}
				}

				if (this.func_46116_a(entityAITaskEntry3) && entityAITaskEntry3.action.shouldExecute())
				{
					arrayList1.Add(entityAITaskEntry3);
					this.executingTasks.Add(entityAITaskEntry3);
				}
			}
		}

		private bool func_46116_a(EntityAITaskEntry entityAITaskEntry1)
		{
			System.Collections.IEnumerator iterator2 = this.tasksToDo.GetEnumerator();

			while (iterator2.MoveNext())
			{
				EntityAITaskEntry entityAITaskEntry3 = (EntityAITaskEntry)iterator2.Current;
				if (entityAITaskEntry3 != entityAITaskEntry1)
				{
					if (entityAITaskEntry1.priority >= entityAITaskEntry3.priority)
					{
						if (this.executingTasks.Contains(entityAITaskEntry3) && !this.areTasksCompatible(entityAITaskEntry1, entityAITaskEntry3))
						{
							return false;
						}
					}
					else if (this.executingTasks.Contains(entityAITaskEntry3) && !entityAITaskEntry3.action.Continuous)
					{
						return false;
					}
				}
			}

			return true;
		}

		private bool areTasksCompatible(EntityAITaskEntry entityAITaskEntry1, EntityAITaskEntry entityAITaskEntry2)
		{
			return (entityAITaskEntry1.action.MutexBits & entityAITaskEntry2.action.MutexBits) == 0;
		}
	}

}