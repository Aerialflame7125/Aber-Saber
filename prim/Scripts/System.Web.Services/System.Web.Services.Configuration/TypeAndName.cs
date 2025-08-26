namespace System.Web.Services.Configuration;

internal class TypeAndName
{
	public readonly Type type;

	public readonly string name;

	public TypeAndName(string name)
	{
		type = Type.GetType(name, throwOnError: true, ignoreCase: true);
		this.name = name;
	}

	public TypeAndName(Type type)
	{
		this.type = type;
	}

	public override int GetHashCode()
	{
		return type.GetHashCode();
	}

	public override bool Equals(object comparand)
	{
		return type.Equals(((TypeAndName)comparand).type);
	}
}
