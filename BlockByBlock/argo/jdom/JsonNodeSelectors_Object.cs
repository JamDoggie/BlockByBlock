namespace argo.jdom
{

	internal sealed class JsonNodeSelectors_Object : LeafFunctor
	{
		public bool func_27070_a(JsonNode jsonNode1)
		{
			return JsonNodeType.OBJECT == jsonNode1.Type;
		}

		public override string shortForm()
		{
			return "A short form object";
		}

		public System.Collections.IDictionary func_27071_b(JsonNode jsonNode1)
		{
			return jsonNode1.Fields;
		}

		public override string ToString()
		{
			return "an object";
		}

		public override object typeSafeApplyTo(object object1)
		{
			return this.func_27071_b((JsonNode)object1);
		}

		public override bool matchesNode(object object1)
		{
			return this.func_27070_a((JsonNode)object1);
		}
	}

}