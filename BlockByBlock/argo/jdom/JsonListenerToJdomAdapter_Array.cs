using System;

namespace argo.jdom
{
	internal class JsonListenerToJdomAdapter_Array : JsonListenerToJdomAdapter_NodeContainer
	{
		internal readonly JsonArrayNodeBuilder nodeBuilder;
		internal readonly JsonListenerToJdomAdapter listenerToJdomAdapter;

		internal JsonListenerToJdomAdapter_Array(JsonListenerToJdomAdapter jsonListenerToJdomAdapter1, JsonArrayNodeBuilder jsonArrayNodeBuilder2)
		{
			this.listenerToJdomAdapter = jsonListenerToJdomAdapter1;
			this.nodeBuilder = jsonArrayNodeBuilder2;
		}

		public virtual void addNode(JsonNodeBuilder jsonNodeBuilder1)
		{
			this.nodeBuilder.withElement(jsonNodeBuilder1);
		}

		public virtual void addField(JsonFieldBuilder jsonFieldBuilder1)
		{
			throw new Exception("Coding failure in Argo:  Attempt to add a field to an array.");
		}
	}

}