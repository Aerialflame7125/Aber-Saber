using System.Web.Compilation;

namespace System.Web.Profile;

internal sealed class ProfileParser
{
	internal ProfileParser(HttpContext context)
	{
	}

	public static Type GetProfileCommonType(HttpContext context)
	{
		string typeName = ((AppCodeCompiler.DefaultAppCodeAssemblyName == null) ? "ProfileCommon" : ("ProfileCommon, " + AppCodeCompiler.DefaultAppCodeAssemblyName));
		Type type = Type.GetType(typeName);
		_ = type == null;
		return type;
	}

	public static Type GetProfileGroupType(HttpContext context, string groupName)
	{
		string typeName = ((AppCodeCompiler.DefaultAppCodeAssemblyName == null) ? ("ProfileGroup" + groupName) : ("ProfileGroup" + groupName + ", " + AppCodeCompiler.DefaultAppCodeAssemblyName));
		Type type = Type.GetType(typeName);
		_ = type == null;
		return type;
	}
}
