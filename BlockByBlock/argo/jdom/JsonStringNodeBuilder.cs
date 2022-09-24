namespace argo.jdom
{
	public sealed class JsonStringNodeBuilder : JsonNodeBuilder
	{
		private readonly string field_27244_a;

		internal JsonStringNodeBuilder(string string1)
		{
			this.field_27244_a = string1;
		}

		public JsonStringNode func_27243_a()
		{
			return JsonNodeFactories.aJsonString(this.field_27244_a);
		}

		public JsonNode buildNode()
		{
			return this.func_27243_a();
		}
	}

}