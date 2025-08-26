namespace System.Web.UI;

internal static class Util
{
	internal static string GetUrlWithApplicationPath(HttpContextBase context, string url)
	{
		string text = context.Request.ApplicationPath ?? string.Empty;
		if (!text.EndsWith("/", StringComparison.OrdinalIgnoreCase))
		{
			text += "/";
		}
		return context.Response.ApplyAppPathModifier(text + url);
	}
}
