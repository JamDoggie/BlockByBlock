using System.Collections.Generic;

namespace argo.jdom
{

	public sealed class JsonArrayNodeBuilder : JsonNodeBuilder
	{
		private readonly System.Collections.IList elementBuilders = new LinkedList();

		public JsonArrayNodeBuilder withElement(JsonNodeBuilder jsonNodeBuilder1)
		{
			this.elementBuilders.Add(jsonNodeBuilder1);
			return this;
		}

		public JsonRootNode build()
		{
			LinkedList linkedList1 = new LinkedList();
			System.Collections.IEnumerator iterator2 = this.elementBuilders.GetEnumerator();

			while (iterator2.MoveNext())
			{
				JsonNodeBuilder jsonNodeBuilder3 = (JsonNodeBuilder)iterator2.Current;
				linkedList1.AddLast(jsonNodeBuilder3.buildNode());
			}

			return JsonNodeFactories.aJsonArray((System.Collections.IEnumerable)linkedList1);
		}

		public JsonNode buildNode()
		{
			return this.build();
		}
	}

}