using System.ComponentModel;

namespace System.Web;

[AttributeUsage(AttributeTargets.All)]
internal class WebCategoryAttribute : CategoryAttribute
{
	public WebCategoryAttribute(string category)
		: base(category)
	{
	}
}
