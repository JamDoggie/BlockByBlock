namespace argo.jdom
{
	internal sealed class JsonNodeSelectors_String : LeafFunctor
	{
		public bool func_27072_a(JsonNode jsonNode1)
		{
			return JsonNodeType.STRING == jsonNode1.Type;
		}

		public override string shortForm()
		{
			return "A short form string";
		}

		public string func_27073_b(JsonNode jsonNode1)
		{
			return jsonNode1.Text;
		}

		public override string ToString()
		{
			return "a value that is a string";
		}

		public override object typeSafeApplyTo(object object1)
		{
			return this.func_27073_b((JsonNode)object1);
		}

		public override bool matchesNode(object object1)
		{
			return this.func_27072_a((JsonNode)object1);
		}
	}

}