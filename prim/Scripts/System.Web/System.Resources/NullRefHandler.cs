using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources;

internal class NullRefHandler : ResXDataNodeHandler, IWritableHandler
{
	private string dataString;

	public string DataString => dataString;

	public NullRefHandler(string _dataString)
	{
		dataString = _dataString;
	}

	public override object GetValue(ITypeResolutionService typeResolver)
	{
		return null;
	}

	public override object GetValue(AssemblyName[] assemblyNames)
	{
		return null;
	}

	public override string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		return typeof(object).AssemblyQualifiedName;
	}

	public override string GetValueTypeName(AssemblyName[] assemblyNames)
	{
		return typeof(object).AssemblyQualifiedName;
	}
}
