using System;

namespace argo.jdom
{

	public sealed class JsonStringNode : JsonNode, IComparable
	{
		private readonly string value;

		internal JsonStringNode(string string1)
		{
			if (string.ReferenceEquals(string1, null))
			{
				throw new System.NullReferenceException("Attempt to construct a JsonString with a null value.");
			}
			else
			{
				this.value = string1;
			}
		}

		public override JsonNodeType Type
		{
			get
			{
				return JsonNodeType.STRING;
			}
		}

		public override string Text
		{
			get
			{
				return this.value;
			}
		}

		public override System.Collections.IDictionary Fields
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get fields on a JsonNode without fields.");
			}
		}

		public override System.Collections.IList Elements
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get elements on a JsonNode without elements.");
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
				JsonStringNode jsonStringNode2 = (JsonStringNode)object1;
				return this.value.Equals(jsonStringNode2.value);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		public override string ToString()
		{
			return "JsonStringNode value:[" + this.value + "]";
		}

		public int func_27223_a(JsonStringNode jsonStringNode1)
		{
			return string.CompareOrdinal(this.value, jsonStringNode1.value);
		}

		public int CompareTo(object object1)
		{
			return this.func_27223_a((JsonStringNode)object1);
		}
	}

}