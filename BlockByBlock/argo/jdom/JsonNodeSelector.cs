namespace argo.jdom
{
	public sealed class JsonNodeSelector
	{
		internal readonly Functor valueGetter;

		internal JsonNodeSelector(Functor functor1)
		{
			this.valueGetter = functor1;
		}

		public bool matches(object object1)
		{
			return this.valueGetter.matchesNode(object1);
		}

		public object getValue(object object1)
		{
			return this.valueGetter.applyTo(object1);
		}

		public JsonNodeSelector with(JsonNodeSelector jsonNodeSelector1)
		{
			return new JsonNodeSelector(new ChainedFunctor(this, jsonNodeSelector1));
		}

		internal string shortForm()
		{
			return this.valueGetter.shortForm();
		}

		public override string ToString()
		{
			return this.valueGetter.ToString();
		}
	}

}