namespace argo.saj
{
	public interface JsonListener
	{
		void startDocument();

		void endDocument();

		void startArray();

		void endArray();

		void startObject();

		void endObject();

		void startField(string string1);

		void endField();

		void stringValue(string string1);

		void numberValue(string string1);

		void trueValue();

		void falseValue();

		void nullValue();
	}

}