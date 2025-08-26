using System.ComponentModel;

namespace System.Web.Services;

[AttributeUsage(AttributeTargets.All)]
internal class WebServicesDescriptionAttribute : DescriptionAttribute
{
	private bool replaced;

	public override string Description
	{
		get
		{
			if (!replaced)
			{
				replaced = true;
				base.DescriptionValue = Res.GetString(base.Description);
			}
			return base.Description;
		}
	}

	internal WebServicesDescriptionAttribute(string description)
		: base(description)
	{
	}
}
