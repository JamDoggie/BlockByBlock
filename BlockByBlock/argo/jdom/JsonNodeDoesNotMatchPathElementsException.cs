using System.Text;

namespace argo.jdom
{
	using CompactJsonFormatter = argo.format.CompactJsonFormatter;
	using JsonFormatter = argo.format.JsonFormatter;

	public sealed class JsonNodeDoesNotMatchPathElementsException : JsonNodeDoesNotMatchJsonNodeSelectorException
	{
		private static readonly JsonFormatter JSON_FORMATTER = new CompactJsonFormatter();

		internal static JsonNodeDoesNotMatchPathElementsException jsonNodeDoesNotMatchPathElementsException(JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException0, object[] object1, JsonRootNode jsonRootNode2)
		{
			return new JsonNodeDoesNotMatchPathElementsException(jsonNodeDoesNotMatchChainedJsonNodeSelectorException0, object1, jsonRootNode2);
		}

		private JsonNodeDoesNotMatchPathElementsException(JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException1, object[] object2, JsonRootNode jsonRootNode3) : base(formatMessage(jsonNodeDoesNotMatchChainedJsonNodeSelectorException1, object2, jsonRootNode3))
		{
		}

		private static string formatMessage(JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException0, object[] object1, JsonRootNode jsonRootNode2)
		{
			return "Failed to find " + jsonNodeDoesNotMatchChainedJsonNodeSelectorException0.failedNode.ToString() + " at [" + JsonNodeDoesNotMatchChainedJsonNodeSelectorException.getShortFormFailPath(jsonNodeDoesNotMatchChainedJsonNodeSelectorException0.failPath) + "] while resolving [" + commaSeparate(object1) + "] in " + JSON_FORMATTER.format(jsonRootNode2) + ".";
		}

		private static string commaSeparate(object[] object0)
		{
			StringBuilder stringBuilder1 = new StringBuilder();
			bool z2 = true;
			object[] object3 = object0;
			int i4 = object0.Length;

			for (int i5 = 0; i5 < i4; ++i5)
			{
				object object6 = object3[i5];
				if (!z2)
				{
					stringBuilder1.Append(".");
				}

				z2 = false;
				if (object6 is string)
				{
					stringBuilder1.Append("\"").Append(object6).Append("\"");
				}
				else
				{
					stringBuilder1.Append(object6);
				}
			}

			return stringBuilder1.ToString();
		}
	}

}