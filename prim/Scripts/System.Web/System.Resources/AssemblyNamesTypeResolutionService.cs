using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources;

internal sealed class AssemblyNamesTypeResolutionService : ITypeResolutionService
{
	public AssemblyNamesTypeResolutionService(AssemblyName[] names)
	{
	}

	public Assembly GetAssembly(AssemblyName name)
	{
		return GetAssembly(name, throwOnError: true);
	}

	public Assembly GetAssembly(AssemblyName name, bool throwOnError)
	{
		throw new NotImplementedException();
	}

	public Type GetType(string name)
	{
		return GetType(name, throwOnError: true);
	}

	public Type GetType(string name, bool throwOnError)
	{
		return GetType(name, throwOnError, ignoreCase: false);
	}

	public Type GetType(string name, bool throwOnError, bool ignoreCase)
	{
		Type type = Type.GetType(name, throwOnError: false, ignoreCase);
		if (type == null && throwOnError)
		{
			throw new ArgumentException($"Could not find a type for a name. The type name was `{name}'");
		}
		return type;
	}

	public void ReferenceAssembly(AssemblyName name)
	{
		throw new NotImplementedException();
	}

	public string GetPathOfAssembly(AssemblyName name)
	{
		throw new NotImplementedException();
	}
}
