namespace argo.jdom
{

	public sealed class JsonNodeFactories
	{
		public static JsonNode aJsonNull()
		{
			return JsonConstants.NULL;
		}

		public static JsonNode aJsonTrue()
		{
			return JsonConstants.TRUE;
		}

		public static JsonNode aJsonFalse()
		{
			return JsonConstants.FALSE;
		}

		public static JsonStringNode aJsonString(string string0)
		{
			return new JsonStringNode(string0);
		}

		public static JsonNode aJsonNumber(string string0)
		{
			return new JsonNumberNode(string0);
		}

		public static JsonRootNode aJsonArray(System.Collections.IEnumerable iterable0)
		{
			return new JsonArray(iterable0);
		}

		public static JsonRootNode aJsonArray(params JsonNode[] jsonNode0)
		{
			return aJsonArray((System.Collections.IEnumerable)Arrays.asList(jsonNode0));
		}

		public static JsonRootNode aJsonObject(System.Collections.IDictionary map0)
		{
			return new JsonObject(map0);
		}
	}

}