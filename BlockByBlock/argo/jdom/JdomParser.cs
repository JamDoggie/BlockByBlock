using System;

namespace argo.jdom
{
	using InvalidSyntaxException = argo.saj.InvalidSyntaxException;
	using SajParser = argo.saj.SajParser;


	public sealed class JdomParser
	{
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public JsonRootNode parse(java.io.Reader reader1) throws InvalidSyntaxException, java.io.IOException
		public JsonRootNode parse(Reader reader1)
		{
			JsonListenerToJdomAdapter jsonListenerToJdomAdapter2 = new JsonListenerToJdomAdapter();
			(new SajParser()).parse(reader1, jsonListenerToJdomAdapter2);
			return jsonListenerToJdomAdapter2.Document;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public JsonRootNode parse(String string1) throws argo.saj.InvalidSyntaxException
		public JsonRootNode parse(string string1)
		{
			try
			{
				JsonRootNode jsonRootNode2 = this.parse((Reader)(new StringReader(string1)));
				return jsonRootNode2;
			}
			catch (IOException iOException4)
			{
				throw new Exception("Coding failure in Argo:  StringWriter gave an IOException", iOException4);
			}
		}
	}

}