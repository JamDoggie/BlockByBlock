namespace argo.jdom
{

	internal sealed class JsonNumberNode : JsonNode
	{
		private static readonly Pattern PATTERN = Pattern.compile("(-?)(0|([1-9]([0-9]*)))(\\.[0-9]+)?((e|E)(\\+|-)?[0-9]+)?");
		private readonly string value;

		internal JsonNumberNode(string string1)
		{
			if (string.ReferenceEquals(string1, null))
			{
				throw new System.NullReferenceException("Attempt to construct a JsonNumber with a null value.");
			}
			else if (!PATTERN.matcher(string1).matches())
			{
				throw new System.ArgumentException("Attempt to construct a JsonNumber with a String [" + string1 + "] that does not match the JSON number specification.");
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
				return JsonNodeType.NUMBER;
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
				JsonNumberNode jsonNumberNode2 = (JsonNumberNode)object1;
				return this.value.Equals(jsonNumberNode2.value);
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
			return "JsonNumberNode value:[" + this.value + "]";
		}
	}

}