using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources;

internal abstract class ResXDataNodeHandler
{
	public abstract object GetValue(ITypeResolutionService typeResolver);

	public abstract object GetValue(AssemblyName[] assemblyNames);

	public abstract string GetValueTypeName(ITypeResolutionService typeResolver);

	public abstract string GetValueTypeName(AssemblyName[] assemblyNames);

	public virtual object GetValueForResX()
	{
		return GetValue((AssemblyName[])null);
	}

	protected Type ResolveType(string typeString)
	{
		return Type.GetType(typeString);
	}

	protected Type ResolveType(string typeString, AssemblyName[] assemblyNames)
	{
		Type type = null;
		if (assemblyNames != null)
		{
			for (int i = 0; i < assemblyNames.Length; i++)
			{
				type = Assembly.Load(assemblyNames[i]).GetType(typeString, throwOnError: false);
				if (type != null)
				{
					return type;
				}
			}
		}
		if (type == null)
		{
			type = ResolveType(typeString);
		}
		return type;
	}

	protected Type ResolveType(string typeString, ITypeResolutionService typeResolver)
	{
		Type type = null;
		if (typeResolver != null)
		{
			type = typeResolver.GetType(typeString);
		}
		if (type == null)
		{
			type = ResolveType(typeString);
		}
		return type;
	}
}
