using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources;

internal class InMemoryHandler : ResXDataNodeHandler
{
	private object value;

	public InMemoryHandler(object valueObject)
	{
		value = valueObject;
	}

	public override object GetValue(ITypeResolutionService typeResolver)
	{
		return value;
	}

	public override object GetValue(AssemblyName[] assemblyNames)
	{
		return value;
	}

	public override string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		if (value == null)
		{
			return null;
		}
		return value.GetType().AssemblyQualifiedName;
	}

	public override string GetValueTypeName(AssemblyName[] assemblyNames)
	{
		if (value == null)
		{
			return null;
		}
		return value.GetType().AssemblyQualifiedName;
	}
}
