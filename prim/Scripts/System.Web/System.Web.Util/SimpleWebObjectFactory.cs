namespace System.Web.Util;

internal sealed class SimpleWebObjectFactory : IWebObjectFactory
{
	private Type type;

	public SimpleWebObjectFactory(Type type)
	{
		this.type = type;
	}

	public object CreateInstance()
	{
		if (type == null)
		{
			return null;
		}
		return Activator.CreateInstance(type);
	}
}
