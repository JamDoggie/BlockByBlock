using System;

namespace argo.jdom
{
	using InvalidSyntaxException = argo.saj.InvalidSyntaxException;
	using SajParser = argo.saj.SajParser;


	public sealed class JdomParser
	{
		public JsonRootNode parse(StringReader reader1)
		{
			JsonListenerToJdomAdapter jsonListenerToJdomAdapter2 = new JsonListenerToJdomAdapter();
			(new SajParser()).parse(reader1, jsonListenerToJdomAdapter2);
			return jsonListenerToJdomAdapter2.Document;
		}

		public JsonRootNode parse(string string1)
		{
			try
			{
				JsonRootNode jsonRootNode2 = this.parse((new StringReader(string1)));
				return jsonRootNode2;
			}
			catch (IOException iOException4)
			{
				throw new Exception("Coding failure in Argo:  StringWriter gave an IOException", iOException4);
			}
		}
	}

}