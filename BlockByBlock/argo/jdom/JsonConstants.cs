namespace argo.jdom
{

	internal sealed class JsonConstants : JsonNode
	{
		internal static readonly JsonConstants NULL = new JsonConstants(JsonNodeType.NULL);
		internal static readonly JsonConstants TRUE = new JsonConstants(JsonNodeType.TRUE);
		internal static readonly JsonConstants FALSE = new JsonConstants(JsonNodeType.FALSE);
		private readonly JsonNodeType jsonNodeType;

		private JsonConstants(JsonNodeType jsonNodeType1)
		{
			this.jsonNodeType = jsonNodeType1;
		}

		public override JsonNodeType Type
		{
			get
			{
				return this.jsonNodeType;
			}
		}

		public override string Text
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get text on a JsonNode without text.");
			}
		}

		public override System.Collections.IDictionary Fields
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get fields on a JsonNode without fields.");
			}
		}

		public override System.Collections.IList Elements
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get elements on a JsonNode without elements.");
			}
		}
	}

}