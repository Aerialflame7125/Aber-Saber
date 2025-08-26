using System.Reflection;

namespace System.Web.Services.Protocols;

internal class SoapReflectedHeader
{
	internal Type headerType;

	internal MemberInfo memberInfo;

	internal SoapHeaderDirection direction;

	internal bool repeats;

	internal bool custom;
}
