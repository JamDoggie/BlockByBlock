namespace argo.jdom
{
	internal sealed class ChainedFunctor : Functor
	{
		private readonly JsonNodeSelector parentJsonNodeSelector;
		private readonly JsonNodeSelector childJsonNodeSelector;

		internal ChainedFunctor(JsonNodeSelector jsonNodeSelector1, JsonNodeSelector jsonNodeSelector2)
		{
			this.parentJsonNodeSelector = jsonNodeSelector1;
			this.childJsonNodeSelector = jsonNodeSelector2;
		}

		public bool matchesNode(object object1)
		{
			return this.parentJsonNodeSelector.matches(object1) && this.childJsonNodeSelector.matches(this.parentJsonNodeSelector.getValue(object1));
		}

		public object applyTo(object object1)
		{
			object object2;
			try
			{
				object2 = this.parentJsonNodeSelector.getValue(object1);
			}
			catch (JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException6)
			{
				throw JsonNodeDoesNotMatchChainedJsonNodeSelectorException.func_27321_b(jsonNodeDoesNotMatchChainedJsonNodeSelectorException6, this.parentJsonNodeSelector);
			}

			try
			{
				object object3 = this.childJsonNodeSelector.getValue(object2);
				return object3;
			}
			catch (JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException5)
			{
				throw JsonNodeDoesNotMatchChainedJsonNodeSelectorException.func_27323_a(jsonNodeDoesNotMatchChainedJsonNodeSelectorException5, this.parentJsonNodeSelector);
			}
		}

		public string shortForm()
		{
			return this.childJsonNodeSelector.shortForm();
		}

		public override string ToString()
		{
			return this.parentJsonNodeSelector.ToString() + ", with " + this.childJsonNodeSelector.ToString();
		}
	}

}