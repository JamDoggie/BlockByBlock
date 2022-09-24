namespace argo.format
{
	using JsonNodeType = argo.jdom.JsonNodeType;

	internal class CompactJsonFormatter_JsonNodeType
	{
		internal static readonly int[] enumJsonNodeTypeMappingArray = new int[(JsonNodeType[])Enum.GetValues(typeof(JsonNodeType)).Length];

		static CompactJsonFormatter_JsonNodeType()
		{
			try
			{
				enumJsonNodeTypeMappingArray[JsonNodeType.ARRAY.ordinal()] = 1;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumJsonNodeTypeMappingArray[JsonNodeType.OBJECT.ordinal()] = 2;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumJsonNodeTypeMappingArray[JsonNodeType.STRING.ordinal()] = 3;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumJsonNodeTypeMappingArray[JsonNodeType.NUMBER.ordinal()] = 4;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumJsonNodeTypeMappingArray[JsonNodeType.FALSE.ordinal()] = 5;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumJsonNodeTypeMappingArray[JsonNodeType.TRUE.ordinal()] = 6;
			}
			catch (NoSuchFieldError)
			{
			}

			try
			{
				enumJsonNodeTypeMappingArray[JsonNodeType.NULL.ordinal()] = 7;
			}
			catch (NoSuchFieldError)
			{
			}

		}
	}

}