using System.Collections;

namespace System.Web.Services.Protocols;

internal class SoapHeaderAttributeComparer : IComparer
{
	public int Compare(object x, object y)
	{
		return string.Compare(((SoapHeaderAttribute)x).MemberName, ((SoapHeaderAttribute)y).MemberName, StringComparison.Ordinal);
	}
}
