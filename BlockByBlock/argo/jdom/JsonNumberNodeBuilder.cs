namespace argo.jdom
{
	internal sealed class JsonNumberNodeBuilder : JsonNodeBuilder
	{
		private readonly JsonNode field_27239_a;

		internal JsonNumberNodeBuilder(string string1)
		{
			this.field_27239_a = JsonNodeFactories.aJsonNumber(string1);
		}

		public JsonNode buildNode()
		{
			return this.field_27239_a;
		}
	}

}