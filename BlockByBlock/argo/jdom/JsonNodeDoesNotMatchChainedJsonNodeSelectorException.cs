using System.Collections.Generic;
using System.Text;

namespace argo.jdom
{

	public sealed class JsonNodeDoesNotMatchChainedJsonNodeSelectorException : JsonNodeDoesNotMatchJsonNodeSelectorException
	{
		internal readonly Functor failedNode;
		internal readonly System.Collections.IList failPath;

		internal static JsonNodeDoesNotMatchJsonNodeSelectorException func_27322_a(Functor functor0)
		{
			return new JsonNodeDoesNotMatchChainedJsonNodeSelectorException(functor0, new LinkedList());
		}

		internal static JsonNodeDoesNotMatchJsonNodeSelectorException func_27323_a(JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException0, JsonNodeSelector jsonNodeSelector1)
		{
			LinkedList linkedList2 = new LinkedList(jsonNodeDoesNotMatchChainedJsonNodeSelectorException0.failPath);
			linkedList2.AddLast(jsonNodeSelector1);
			return new JsonNodeDoesNotMatchChainedJsonNodeSelectorException(jsonNodeDoesNotMatchChainedJsonNodeSelectorException0.failedNode, linkedList2);
		}

		internal static JsonNodeDoesNotMatchJsonNodeSelectorException func_27321_b(JsonNodeDoesNotMatchChainedJsonNodeSelectorException jsonNodeDoesNotMatchChainedJsonNodeSelectorException0, JsonNodeSelector jsonNodeSelector1)
		{
			LinkedList linkedList2 = new LinkedList();
			linkedList2.AddLast(jsonNodeSelector1);
			return new JsonNodeDoesNotMatchChainedJsonNodeSelectorException(jsonNodeDoesNotMatchChainedJsonNodeSelectorException0.failedNode, linkedList2);
		}

		private JsonNodeDoesNotMatchChainedJsonNodeSelectorException(Functor functor1, System.Collections.IList list2) : base("Failed to match any JSON node at [" + getShortFormFailPath(list2) + "]")
		{
			this.failedNode = functor1;
			this.failPath = list2;
		}

		internal static string getShortFormFailPath(System.Collections.IList list0)
		{
			StringBuilder stringBuilder1 = new StringBuilder();

			for (int i2 = list0.Count - 1; i2 >= 0; --i2)
			{
				stringBuilder1.Append(((JsonNodeSelector)list0[i2]).shortForm());
				if (i2 != 0)
				{
					stringBuilder1.Append(".");
				}
			}

			return stringBuilder1.ToString();
		}

		public override string ToString()
		{
			return "JsonNodeDoesNotMatchJsonNodeSelectorException{failedNode=" + this.failedNode + ", failPath=" + this.failPath + '}';
		}
	}

}