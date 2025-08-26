using System.Reflection;
using System.Security.Permissions;

namespace System.Web.Services.Protocols;

/// <summary>The <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> class represents a SOAP header mapping.</summary>
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public sealed class SoapHeaderMapping
{
	internal Type headerType;

	internal bool repeats;

	internal bool custom;

	internal SoapHeaderDirection direction;

	internal MemberInfo memberInfo;

	/// <summary>Gets a <see cref="T:System.Type" /> that represents the type of the SOAP header mapping.</summary>
	/// <returns>A <see cref="T:System.Type" /> that represents the type of the SOAP header mapping.</returns>
	public Type HeaderType => headerType;

	/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the SOAP header mapping repeats.</summary>
	/// <returns>
	///     <see langword="true" /> if the SOAP header mapping repeats; otherwise, <see langword="false" />.</returns>
	public bool Repeats => repeats;

	/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the SOAP header mapping is custom-defined.</summary>
	/// <returns>
	///     <see langword="true" /> if the SOAP header mapping is custom-defined; otherwise, <see langword="false" />.</returns>
	public bool Custom => custom;

	/// <summary>Gets a <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP header mapping.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP header mapping.</returns>
	public SoapHeaderDirection Direction => direction;

	/// <summary>Gets the <see cref="T:System.Reflection.MemberInfo" /> associated with the SOAP header mapping.</summary>
	/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> associated with the SOAP header mapping.</returns>
	public MemberInfo MemberInfo => memberInfo;

	internal SoapHeaderMapping()
	{
	}
}
