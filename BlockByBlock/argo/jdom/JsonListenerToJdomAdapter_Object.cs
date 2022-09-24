using System;

namespace argo.jdom
{
	internal class JsonListenerToJdomAdapter_Object : JsonListenerToJdomAdapter_NodeContainer
	{
		internal readonly JsonObjectNodeBuilder nodeBuilder;
		internal readonly JsonListenerToJdomAdapter listenerToJdomAdapter;

		internal JsonListenerToJdomAdapter_Object(JsonListenerToJdomAdapter jsonListenerToJdomAdapter1, JsonObjectNodeBuilder jsonObjectNodeBuilder2)
		{
			this.listenerToJdomAdapter = jsonListenerToJdomAdapter1;
			this.nodeBuilder = jsonObjectNodeBuilder2;
		}

		public virtual void addNode(JsonNodeBuilder jsonNodeBuilder1)
		{
			throw new Exception("Coding failure in Argo:  Attempt to add a node to an object.");
		}

		public virtual void addField(JsonFieldBuilder jsonFieldBuilder1)
		{
			this.nodeBuilder.withFieldBuilder(jsonFieldBuilder1);
		}
	}

}