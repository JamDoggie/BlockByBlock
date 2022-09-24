using System;

namespace argo.jdom
{

	internal sealed class JsonNodeSelectors_Element : LeafFunctor
	{
		internal readonly int index;

		internal JsonNodeSelectors_Element(int i1)
		{
			this.index = i1;
		}

		public bool matchesNode_(System.Collections.IList list1)
		{
			return list1.Count > this.index;
		}

		public override string shortForm()
		{
			return Convert.ToString(this.index);
		}

		public JsonNode typeSafeApplyTo_(System.Collections.IList list1)
		{
			return (JsonNode)list1[this.index];
		}

		public override string ToString()
		{
			return "an element at index [" + this.index + "]";
		}

		public override object typeSafeApplyTo(object object1)
		{
			return this.typeSafeApplyTo_((System.Collections.IList)object1);
		}

		public override bool matchesNode(object object1)
		{
			return this.matchesNode_((System.Collections.IList)object1);
		}
	}

}