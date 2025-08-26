using System.ComponentModel;

namespace System.Web;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Event)]
internal sealed class WebSysDisplayNameAttribute : DisplayNameAttribute
{
	private bool replaced;

	public override string DisplayName
	{
		get
		{
			if (!replaced)
			{
				replaced = true;
				base.DisplayNameValue = global::SR.GetString(base.DisplayName);
			}
			return base.DisplayName;
		}
	}

	public override object TypeId => typeof(DisplayNameAttribute);

	internal WebSysDisplayNameAttribute(string DisplayName)
		: base(DisplayName)
	{
	}
}
