using System.Reflection;

namespace System.Web.UI;

internal class UnknownAttributeDescriptor
{
	public MemberInfo Info;

	public object Value;

	public UnknownAttributeDescriptor(MemberInfo memberInfo, object value)
	{
		Info = memberInfo;
		Value = value;
	}
}
