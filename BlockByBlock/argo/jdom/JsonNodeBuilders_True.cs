namespace argo.jdom
{
	internal sealed class JsonNodeBuilders_True : JsonNodeBuilder
	{
		public JsonNode buildNode()
		{
			return JsonNodeFactories.aJsonTrue();
		}
	}

}