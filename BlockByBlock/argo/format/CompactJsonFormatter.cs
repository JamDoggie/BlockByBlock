using System;
using System.Collections.Generic;

namespace argo.format
{
	using JsonNode = argo.jdom.JsonNode;
	using JsonRootNode = argo.jdom.JsonRootNode;
	using JsonStringNode = argo.jdom.JsonStringNode;


	public sealed class CompactJsonFormatter : JsonFormatter
	{
		public string format(JsonRootNode jsonRootNode1)
		{
			StringWriter stringWriter2 = new StringWriter();

			try
			{
				this.format(jsonRootNode1, stringWriter2);
			}
			catch (IOException iOException4)
			{
				throw new Exception("Coding failure in Argo:  StringWriter gave an IOException", iOException4);
			}

			return stringWriter2.ToString();
		}
        
		public void format(JsonRootNode jsonRootNode1, StringWriter writer2)
		{
			this.formatJsonNode(jsonRootNode1, writer2);
		}
        
		private void formatJsonNode(JsonNode jsonNode1, StringWriter writer2)
		{
			bool z3 = true;
			System.Collections.IEnumerator iterator4;
			switch (CompactJsonFormatter_JsonNodeType.enumJsonNodeTypeMappingArray[(int)jsonNode1.Type])
			{
			case 1:
				writer2.Write('[');
				iterator4 = jsonNode1.Elements.GetEnumerator();

				while (iterator4.MoveNext())
				{
					JsonNode jsonNode6 = (JsonNode)iterator4.Current;
					if (!z3)
					{
						writer2.Write(',');
					}

					z3 = false;
					this.formatJsonNode(jsonNode6, writer2);
				}

				writer2.Write(']');
				break;
			case 2:
				writer2.Write('{');
				iterator4 = (new SortedSet<JsonStringNode>(jsonNode1.Fields.Keys)).GetEnumerator();

				while (iterator4.MoveNext())
				{
					JsonStringNode jsonStringNode5 = (JsonStringNode)iterator4.Current;
					if (!z3)
					{
						writer2.Write(',');
					}

					z3 = false;
					this.formatJsonNode(jsonStringNode5, writer2);
					writer2.Write(':');
					this.formatJsonNode((JsonNode)jsonNode1.Fields[jsonStringNode5], writer2);
				}

				writer2.Write('}');
				break;
			case 3:
				writer2.Write('\"');
				writer2.Write((new JsonEscapedString(jsonNode1.Text)).ToString());
				writer2.Write('\"');
				break;
			case 4:
				writer2.Write(jsonNode1.Text);
				break;
			case 5:
				writer2.Write("false");
				break;
			case 6:
				writer2.Write("true");
				break;
			case 7:
				writer2.Write("null");
				break;
			default:
				throw new Exception("Coding failure in Argo:  Attempt to format a JsonNode of unknown type [" + jsonNode1.Type + "];");
			}

		}
	}

}