using System;

namespace argo.saj
{
	public sealed class InvalidSyntaxException : Exception
	{
		private readonly int column;
		private readonly int row;

		internal InvalidSyntaxException(string string1, ThingWithPosition thingWithPosition2) : base("At line " + thingWithPosition2.Row + ", column " + thingWithPosition2.Column + ":  " + string1)
		{
			this.column = thingWithPosition2.Column;
			this.row = thingWithPosition2.Row;
		}

		internal InvalidSyntaxException(string string1, Exception throwable2, ThingWithPosition thingWithPosition3) : base("At line " + thingWithPosition3.Row + ", column " + thingWithPosition3.Column + ":  " + string1, throwable2)
		{
			this.column = thingWithPosition3.Column;
			this.row = thingWithPosition3.Row;
		}
	}

}