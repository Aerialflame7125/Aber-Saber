using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources;

internal class ByteArrayFromResXHandler : ResXDataNodeHandler, IWritableHandler
{
	private string dataString;

	public string DataString => dataString;

	public ByteArrayFromResXHandler(string data)
	{
		dataString = data;
	}

	public override object GetValue(ITypeResolutionService typeResolver)
	{
		return Convert.FromBase64String(dataString);
	}

	public override object GetValue(AssemblyName[] assemblyNames)
	{
		return Convert.FromBase64String(dataString);
	}

	public override string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		return ResolveType(typeof(byte[]).AssemblyQualifiedName, typeResolver).AssemblyQualifiedName;
	}

	public override string GetValueTypeName(AssemblyName[] assemblyNames)
	{
		return typeof(byte[]).AssemblyQualifiedName;
	}
}
