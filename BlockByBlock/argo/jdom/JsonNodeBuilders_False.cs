namespace argo.jdom
{
	internal sealed class JsonNodeBuilders_False : JsonNodeBuilder
	{
		public JsonNode buildNode()
		{
			return JsonNodeFactories.aJsonFalse();
		}
	}

}