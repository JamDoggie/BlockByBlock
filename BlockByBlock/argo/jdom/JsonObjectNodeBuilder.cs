using System.Collections.Generic;

namespace argo.jdom
{

	public sealed class JsonObjectNodeBuilder : JsonNodeBuilder
	{
		private readonly System.Collections.IList fieldBuilders = new LinkedList();

		public JsonObjectNodeBuilder withFieldBuilder(JsonFieldBuilder jsonFieldBuilder1)
		{
			this.fieldBuilders.Add(jsonFieldBuilder1);
			return this;
		}

		public JsonRootNode func_27235_a()
		{
			return JsonNodeFactories.aJsonObject(new JsonObjectNodeBuilder_List(this));
		}

		public JsonNode buildNode()
		{
			return this.func_27235_a();
		}

		internal static System.Collections.IList func_27236_a(JsonObjectNodeBuilder jsonObjectNodeBuilder0)
		{
			return jsonObjectNodeBuilder0.fieldBuilders;
		}
	}

}