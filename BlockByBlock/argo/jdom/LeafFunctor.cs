namespace argo.jdom
{
	internal abstract class LeafFunctor : Functor
	{
		public abstract string shortForm();
		public abstract bool matchesNode(object object1);
		public object applyTo(object object1)
		{
			if (!this.matchesNode(object1))
			{
				throw JsonNodeDoesNotMatchChainedJsonNodeSelectorException.func_27322_a(this);
			}
			else
			{
				return this.typeSafeApplyTo(object1);
			}
		}

		protected internal abstract object typeSafeApplyTo(object object1);
	}

}