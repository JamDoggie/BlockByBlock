using System.Collections;
using System.Linq;

namespace argo.jdom
{

	internal sealed class JsonArray : JsonRootNode
	{
		private readonly System.Collections.IList elements;

		internal JsonArray(System.Collections.IEnumerable iterable1)
		{
			this.elements = asList(iterable1);
		}

		public override JsonNodeType Type
		{
			get
			{
				return JsonNodeType.ARRAY;
			}
		}

		public override System.Collections.IList Elements
		{
			get
			{
				return new ArrayList(this.elements);
			}
		}

		public override string Text
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get text on a JsonNode without text.");
			}
		}

		public override System.Collections.IDictionary Fields
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get fields on a JsonNode without fields.");
			}
		}

		public override bool Equals(object object1)
		{
			if (this == object1)
			{
				return true;
			}
			else if (object1 != null && this.GetType() == object1.GetType())
			{
				JsonArray jsonArray2 = (JsonArray)object1;
//JAVA TO C# CONVERTER WARNING: LINQ 'SequenceEqual' is not always identical to Java AbstractList 'equals':
//ORIGINAL LINE: return this.elements.equals(jsonArray2.elements);
				return this.elements.SequenceEqual(jsonArray2.elements);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.elements.GetHashCode();
		}

		public override string ToString()
		{
			return "JsonArray elements:[" + this.elements + "]";
		}

		private static System.Collections.IList asList(System.Collections.IEnumerable iterable0)
		{
			return new JsonArray_NodeList(iterable0);
		}
	}

}