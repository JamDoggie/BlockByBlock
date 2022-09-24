namespace argo.jdom
{
	internal sealed class JsonNodeBuilders_Null : JsonNodeBuilder
	{
		public JsonNode buildNode()
		{
			return JsonNodeFactories.aJsonNull();
		}
	}

}