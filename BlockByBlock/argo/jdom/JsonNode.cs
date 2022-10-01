namespace argo.jdom
{

	public abstract class JsonNode
	{
		public abstract JsonNodeType Type {get;}

		public abstract string Text {get;}

		public abstract IDictionary<JsonStringNode,JsonNode> Fields {get;}

		public abstract System.Collections.IList Elements {get;}

		public string getStringValue(params object[] object1)
		{
			return (string)this.wrapExceptionsFor(JsonNodeSelectors.func_27349_a(object1), this, object1);
		}

		public System.Collections.IList getArrayNode(params object[] object1)
		{
			return (System.Collections.IList)this.wrapExceptionsFor(JsonNodeSelectors.func_27346_b(object1), this, object1);
		}

		private object wrapExceptionsFor(JsonNodeSelector jsonNodeSelector1, JsonNode jsonNode2, object[] object3)
		{
			try
			{
				return jsonNodeSelector1.getValue(jsonNode2);
			}
			catch (JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException5)
			{
				throw JsonNodeDoesNotMatchPathElementsException.jsonNodeDoesNotMatchPathElementsException(jsonNodeDoesNotMatchChainedJsonNodeSelectorException5, object3, JsonNodeFactories.aJsonArray(new JsonNode[]{jsonNode2}));
			}
		}
	}

}