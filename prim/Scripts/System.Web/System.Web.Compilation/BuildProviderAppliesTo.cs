namespace System.Web.Compilation;

/// <summary>Specifies the locations where the <see cref="T:System.Web.Compilation.BuildProviderAppliesToAttribute" /> attribute is respected during code generation for a resource by a <see cref="T:System.Web.Compilation.BuildProvider" /> object.</summary>
[Flags]
public enum BuildProviderAppliesTo
{
	/// <summary>Specifies that the build provider generates code for only those resources in Web content directories, which are directories other than the reserved ASP.NET directories \App_Code, \App_GlobalResources, and \App_LocalResources.</summary>
	Web = 1,
	/// <summary>Specifies that the build provider generates code for only those resources in the \App_Code directory.</summary>
	Code = 2,
	/// <summary>Specifies that the build provider generates code for resources in the \App_GlobalResources and \App_LocalResources directories.</summary>
	Resources = 4,
	/// <summary>Specifies that the build provider generates code for resources wherever the resources are found. This is the default value for the <see cref="T:System.Web.Compilation.BuildProviderAppliesToAttribute" /> attribute.</summary>
	All = 7
}
