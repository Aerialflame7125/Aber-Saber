namespace System.Web;

internal struct DynamicModuleInfo
{
	public readonly string Name;

	public readonly Type Type;

	public DynamicModuleInfo(Type type, string name)
	{
		Name = name;
		Type = type;
	}
}
