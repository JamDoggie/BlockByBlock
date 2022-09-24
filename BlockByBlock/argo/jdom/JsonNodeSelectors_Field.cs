namespace argo.jdom
{

	internal sealed class JsonNodeSelectors_Field : LeafFunctor
	{
		internal readonly JsonStringNode field_27066_a;

		internal JsonNodeSelectors_Field(JsonStringNode jsonStringNode1)
		{
			this.field_27066_a = jsonStringNode1;
		}

		public bool func_27065_a(System.Collections.IDictionary map1)
		{
			return map1.Contains(this.field_27066_a);
		}

		public override string shortForm()
		{
			return "\"" + this.field_27066_a.Text + "\"";
		}

		public JsonNode func_27064_b(System.Collections.IDictionary map1)
		{
			return (JsonNode)map1[this.field_27066_a];
		}

		public override string ToString()
		{
			return "a field called [\"" + this.field_27066_a.Text + "\"]";
		}

		public override object typeSafeApplyTo(object object1)
		{
			return this.func_27064_b((System.Collections.IDictionary)object1);
		}

		public override bool matchesNode(object object1)
		{
			return this.func_27065_a((System.Collections.IDictionary)object1);
		}
	}

}