namespace argo.jdom
{

	public sealed class JsonNodeSelectors
	{
		public static JsonNodeSelector func_27349_a(params object[] object0)
		{
			return chainOn(object0, new JsonNodeSelector(new JsonNodeSelectors_String()));
		}

		public static JsonNodeSelector func_27346_b(params object[] object0)
		{
			return chainOn(object0, new JsonNodeSelector(new JsonNodeSelectors_Array()));
		}

		public static JsonNodeSelector func_27353_c(params object[] object0)
		{
			return chainOn(object0, new JsonNodeSelector(new JsonNodeSelectors_Object()));
		}

		public static JsonNodeSelector func_27348_a(string string0)
		{
			return func_27350_a(JsonNodeFactories.aJsonString(string0));
		}

		public static JsonNodeSelector func_27350_a(JsonStringNode jsonStringNode0)
		{
			return new JsonNodeSelector(new JsonNodeSelectors_Field(jsonStringNode0));
		}

		public static JsonNodeSelector func_27351_b(string string0)
		{
			return func_27353_c(new object[0]).with(func_27348_a(string0));
		}

		public static JsonNodeSelector func_27347_a(int i0)
		{
			return new JsonNodeSelector(new JsonNodeSelectors_Element(i0));
		}

		public static JsonNodeSelector func_27354_b(int i0)
		{
			return func_27346_b(new object[0]).with(func_27347_a(i0));
		}

		private static JsonNodeSelector chainOn(object[] object0, JsonNodeSelector jsonNodeSelector1)
		{
			JsonNodeSelector jsonNodeSelector2 = jsonNodeSelector1;

			for (int i3 = object0.Length - 1; i3 >= 0; --i3)
			{
				if (object0[i3] is Integer)
				{
					jsonNodeSelector2 = chainedJsonNodeSelector(func_27354_b(((int?)object0[i3]).Value), jsonNodeSelector2);
				}
				else
				{
					if (!(object0[i3] is string))
					{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getCanonicalName method:
						throw new System.ArgumentException("Element [" + object0[i3] + "] of path elements" + " [" + "[" + string.Join(", ", object0) + "]" + "] was of illegal type [" + object0[i3].GetType().FullName + "]; only Integer and String are valid.");
					}

					jsonNodeSelector2 = chainedJsonNodeSelector(func_27351_b((string)object0[i3]), jsonNodeSelector2);
				}
			}

			return jsonNodeSelector2;
		}

		private static JsonNodeSelector chainedJsonNodeSelector(JsonNodeSelector jsonNodeSelector0, JsonNodeSelector jsonNodeSelector1)
		{
			return new JsonNodeSelector(new ChainedFunctor(jsonNodeSelector0, jsonNodeSelector1));
		}
	}

}