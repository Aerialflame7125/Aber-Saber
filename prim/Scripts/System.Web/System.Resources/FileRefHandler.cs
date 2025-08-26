using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources;

internal class FileRefHandler : ResXDataNodeHandler
{
	private ResXFileRef resXFileRef;

	public FileRefHandler(ResXFileRef fileRef)
	{
		resXFileRef = fileRef;
	}

	public override object GetValue(ITypeResolutionService typeResolver)
	{
		return GetValue();
	}

	public override object GetValue(AssemblyName[] assemblyNames)
	{
		return GetValue();
	}

	public override string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		Type type = ResolveType(resXFileRef.TypeName, typeResolver);
		if (type == null)
		{
			return resXFileRef.TypeName;
		}
		return type.AssemblyQualifiedName;
	}

	public override string GetValueTypeName(AssemblyName[] assemblyNames)
	{
		Type type = ResolveType(resXFileRef.TypeName, assemblyNames);
		if (type == null)
		{
			return resXFileRef.TypeName;
		}
		return type.AssemblyQualifiedName;
	}

	private object GetValue()
	{
		TypeConverter converter = TypeDescriptor.GetConverter(typeof(ResXFileRef));
		try
		{
			return converter.ConvertFromInvariantString(resXFileRef.ToString());
		}
		catch (ArgumentNullException ex)
		{
			if (ex.ParamName == "type")
			{
				throw new TypeLoadException("Could not find type", ex);
			}
			throw ex;
		}
	}
}
