namespace System.Web.Configuration;

/// <summary>Specifies the optimization mode for an application deployment environment.</summary>
[Flags]
public enum ProfileGuidedOptimizationsFlags
{
	/// <summary>No optimizations are performed based on the deployment environment of the application.</summary>
	None = 0,
	/// <summary>All optimizations are performed based on the deployment environment of the application.</summary>
	All = 1
}
