using System;

namespace Microsoft.SqlServer.Server;

/// <summary>Indicates the determinism and data access properties of a method or property on a user-defined type (UDT). The properties on the attribute reflect the physical characteristics that are used when the type is registered with SQL Server.</summary>
[Serializable]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class SqlMethodAttribute : SqlFunctionAttribute
{
	private bool m_fCallOnNullInputs;

	private bool m_fMutator;

	private bool m_fInvokeIfReceiverIsNull;

	/// <summary>Indicates whether the method on a user-defined type (UDT) is called when <see langword="null" /> input arguments are specified in the method invocation.</summary>
	/// <returns>
	///   <see langword="true" /> if the method is called when <see langword="null" /> input arguments are specified in the method invocation; <see langword="false" /> if the method returns a <see langword="null" /> value when any of its input parameters are <see langword="null" />. If the method cannot be invoked (because of an attribute on the method), the SQL Server <see langword="DbNull" /> is returned.</returns>
	public bool OnNullCall
	{
		get
		{
			return m_fCallOnNullInputs;
		}
		set
		{
			m_fCallOnNullInputs = value;
		}
	}

	/// <summary>Indicates whether a method on a user-defined type (UDT) is a mutator.</summary>
	/// <returns>
	///   <see langword="true" /> if the method is a mutator; otherwise <see langword="false" />.</returns>
	public bool IsMutator
	{
		get
		{
			return m_fMutator;
		}
		set
		{
			m_fMutator = value;
		}
	}

	/// <summary>Indicates whether SQL Server should invoke the method on null instances.</summary>
	/// <returns>
	///   <see langword="true" /> if SQL Server should invoke the method on null instances; otherwise, <see langword="false" />. If the method cannot be invoked (because of an attribute on the method), the SQL Server <see langword="DbNull" /> is returned.</returns>
	public bool InvokeIfReceiverIsNull
	{
		get
		{
			return m_fInvokeIfReceiverIsNull;
		}
		set
		{
			m_fInvokeIfReceiverIsNull = value;
		}
	}

	/// <summary>An attribute on a user-defined type (UDT), used to indicate the determinism and data access properties of a method or a property on a UDT.</summary>
	public SqlMethodAttribute()
	{
		m_fCallOnNullInputs = true;
		m_fMutator = false;
		m_fInvokeIfReceiverIsNull = false;
	}
}
