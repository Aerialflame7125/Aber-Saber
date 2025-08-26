namespace System.Diagnostics.Contracts;

/// <summary>Identifies a member that has no run-time behavior.</summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
[Conditional("CONTRACTS_FULL")]
public sealed class ContractRuntimeIgnoredAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Contracts.ContractRuntimeIgnoredAttribute" /> class.</summary>
	public ContractRuntimeIgnoredAttribute()
	{
	}
}
