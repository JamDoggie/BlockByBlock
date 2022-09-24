using System;

namespace argo.jdom
{
	internal class JsonListenerToJdomAdapter_Field : JsonListenerToJdomAdapter_NodeContainer
	{
		internal readonly JsonFieldBuilder fieldBuilder;
		internal readonly JsonListenerToJdomAdapter listenerToJdomAdapter;

		internal JsonListenerToJdomAdapter_Field(JsonListenerToJdomAdapter jsonListenerToJdomAdapter1, JsonFieldBuilder jsonFieldBuilder2)
		{
			this.listenerToJdomAdapter = jsonListenerToJdomAdapter1;
			this.fieldBuilder = jsonFieldBuilder2;
		}

		public virtual void addNode(JsonNodeBuilder jsonNodeBuilder1)
		{
			this.fieldBuilder.withValue(jsonNodeBuilder1);
		}

		public virtual void addField(JsonFieldBuilder jsonFieldBuilder1)
		{
			throw new Exception("Coding failure in Argo:  Attempt to add a field to a field.");
		}
	}

}