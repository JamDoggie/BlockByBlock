using System.Collections;

namespace argo.jdom
{

	internal sealed class JsonObject : JsonRootNode
	{
		private readonly System.Collections.IDictionary fields;

		internal JsonObject(System.Collections.IDictionary map1)
		{
			this.fields = new Hashtable(map1);
		}

		public override System.Collections.IDictionary Fields
		{
			get
			{
				return new Hashtable(this.fields);
			}
		}

		public override JsonNodeType Type
		{
			get
			{
				return JsonNodeType.OBJECT;
			}
		}

		public override string Text
		{
			get
			{
				throw new System.InvalidOperationException("Attempt to get text on a JsonNode without text.");
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
				JsonObject jsonObject2 = (JsonObject)object1;
				return this.fields.Equals(jsonObject2.fields);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.fields.GetHashCode();
		}

		public override string ToString()
		{
			return "JsonObject fields:[" + this.fields + "]";
		}
	}

}