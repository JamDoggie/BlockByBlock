namespace argo.jdom
{

	internal sealed class JsonNodeSelectors_Array : LeafFunctor
	{
		public bool matchesNode_(JsonNode jsonNode1)
		{
			return JsonNodeType.ARRAY == jsonNode1.Type;
		}

		public override string shortForm()
		{
			return "A short form array";
		}

		public System.Collections.IList typeSafeApplyTo(JsonNode jsonNode1)
		{
			return jsonNode1.Elements;
		}

		public override string ToString()
		{
			return "an array";
		}

		public override object typeSafeApplyTo(object object1)
		{
			return this.typeSafeApplyTo((JsonNode)object1);
		}

		public override bool matchesNode(object object1)
		{
			return this.matchesNode_((JsonNode)object1);
		}
	}

}