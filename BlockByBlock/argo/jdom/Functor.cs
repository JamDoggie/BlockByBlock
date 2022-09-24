namespace argo.jdom
{
	internal interface Functor
	{
		bool matchesNode(object object1);

		object applyTo(object object1);

		string shortForm();
	}

}