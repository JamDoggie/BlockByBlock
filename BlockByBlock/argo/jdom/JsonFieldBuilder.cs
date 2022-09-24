namespace argo.jdom
{
	internal sealed class JsonFieldBuilder
	{
		private JsonNodeBuilder key;
		private JsonNodeBuilder valueBuilder;

		internal static JsonFieldBuilder aJsonFieldBuilder()
		{
			return new JsonFieldBuilder();
		}

		internal JsonFieldBuilder withKey(JsonNodeBuilder jsonNodeBuilder1)
		{
			this.key = jsonNodeBuilder1;
			return this;
		}

		internal JsonFieldBuilder withValue(JsonNodeBuilder jsonNodeBuilder1)
		{
			this.valueBuilder = jsonNodeBuilder1;
			return this;
		}

		internal JsonStringNode func_27303_b()
		{
			return (JsonStringNode)this.key.buildNode();
		}

		internal JsonNode buildValue()
		{
			return this.valueBuilder.buildNode();
		}
	}

}