using System.Collections;

namespace argo.jdom
{

	internal sealed class JsonArray_NodeList : ArrayList
	{
		internal readonly System.Collections.IEnumerable field_27405_a;

		internal JsonArray_NodeList(System.Collections.IEnumerable iterable1)
		{
			this.field_27405_a = iterable1;
			System.Collections.IEnumerator iterator2 = this.field_27405_a.GetEnumerator();

			while (iterator2.MoveNext())
			{
				JsonNode jsonNode3 = (JsonNode)iterator2.Current;
				this.Add(jsonNode3);
			}

		}
	}

}