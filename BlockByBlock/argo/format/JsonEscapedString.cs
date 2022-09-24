namespace argo.format
{
	internal sealed class JsonEscapedString
	{
		private readonly string escapedString;

		internal JsonEscapedString(string string1)
		{
			this.escapedString = string1.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\b", "\\b").Replace("\f", "\\f").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
		}

		public override string ToString()
		{
			return this.escapedString;
		}
	}

}