using System.Collections;

namespace argo.jdom
{

	internal class JsonObjectNodeBuilder_List : Hashtable
	{
		internal readonly JsonObjectNodeBuilder nodeBuilder;

		internal JsonObjectNodeBuilder_List(JsonObjectNodeBuilder jsonObjectNodeBuilder1)
		{
			this.nodeBuilder = jsonObjectNodeBuilder1;
			System.Collections.IEnumerator iterator2 = JsonObjectNodeBuilder.func_27236_a(this.nodeBuilder).GetEnumerator();

			while (iterator2.MoveNext())
			{
				JsonFieldBuilder jsonFieldBuilder3 = (JsonFieldBuilder)iterator2.Current;
				this[jsonFieldBuilder3.func_27303_b()] = jsonFieldBuilder3.buildValue();
			}

		}
	}

}