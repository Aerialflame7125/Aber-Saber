namespace System.Web;

internal class ModuleConfigurationInfo
{
	private string _type;

	private string _name;

	private string _precondition;

	internal string Type => _type;

	internal string Name => _name;

	internal string Precondition => _precondition;

	internal ModuleConfigurationInfo(string name, string type, string condition)
	{
		_type = type;
		_name = name;
		_precondition = condition;
	}
}
