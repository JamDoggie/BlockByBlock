namespace argo.jdom
{
	using JsonListener = argo.saj.JsonListener;

	internal sealed class JsonListenerToJdomAdapter : JsonListener
	{
		private readonly System.Collections.Stack stack = new System.Collections.Stack();
		private JsonNodeBuilder root;

		internal JsonRootNode Document
		{
			get
			{
				return (JsonRootNode)this.root.buildNode();
			}
		}

		public void startDocument()
		{
		}

		public void endDocument()
		{
		}

		public void startArray()
		{
			JsonArrayNodeBuilder jsonArrayNodeBuilder1 = JsonNodeBuilders.anArrayBuilder();
			this.addRootNode(jsonArrayNodeBuilder1);
			this.stack.Push(new JsonListenerToJdomAdapter_Array(this, jsonArrayNodeBuilder1));
		}

		public void endArray()
		{
			this.stack.Pop();
		}

		public void startObject()
		{
			JsonObjectNodeBuilder jsonObjectNodeBuilder1 = JsonNodeBuilders.anObjectBuilder();
			this.addRootNode(jsonObjectNodeBuilder1);
			this.stack.Push(new JsonListenerToJdomAdapter_Object(this, jsonObjectNodeBuilder1));
		}

		public void endObject()
		{
			this.stack.Pop();
		}

		public void startField(string string1)
		{
			JsonFieldBuilder jsonFieldBuilder2 = JsonFieldBuilder.aJsonFieldBuilder().withKey(JsonNodeBuilders.func_27254_b(string1));
			((JsonListenerToJdomAdapter_NodeContainer)this.stack.Peek()).addField(jsonFieldBuilder2);
			this.stack.Push(new JsonListenerToJdomAdapter_Field(this, jsonFieldBuilder2));
		}

		public void endField()
		{
			this.stack.Pop();
		}

		public void numberValue(string string1)
		{
			this.addValue(JsonNodeBuilders.func_27250_a(string1));
		}

		public void trueValue()
		{
			this.addValue(JsonNodeBuilders.func_27251_b());
		}

		public void stringValue(string string1)
		{
			this.addValue(JsonNodeBuilders.func_27254_b(string1));
		}

		public void falseValue()
		{
			this.addValue(JsonNodeBuilders.func_27252_c());
		}

		public void nullValue()
		{
			this.addValue(JsonNodeBuilders.func_27248_a());
		}

		private void addRootNode(JsonNodeBuilder jsonNodeBuilder1)
		{
			if (this.root == null)
			{
				this.root = jsonNodeBuilder1;
			}
			else
			{
				this.addValue(jsonNodeBuilder1);
			}

		}

		private void addValue(JsonNodeBuilder jsonNodeBuilder1)
		{
			((JsonListenerToJdomAdapter_NodeContainer)this.stack.Peek()).addNode(jsonNodeBuilder1);
		}
	}

}