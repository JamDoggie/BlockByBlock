using System;

namespace net.minecraft.src
{
	public class UnexpectedThrowable
	{
		public readonly string description;
		public readonly Exception exception;

		public UnexpectedThrowable(string string1, Exception throwable2)
		{
			this.description = string1;
			this.exception = throwable2;
		}
	}

}