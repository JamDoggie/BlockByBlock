namespace argo.format
{
	using JsonRootNode = argo.jdom.JsonRootNode;

	public interface JsonFormatter
	{
		string format(JsonRootNode jsonRootNode1);
	}

}