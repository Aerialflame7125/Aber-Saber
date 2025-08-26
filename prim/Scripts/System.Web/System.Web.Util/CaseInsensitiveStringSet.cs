namespace System.Web.Util;

internal class CaseInsensitiveStringSet : StringSet
{
	protected override bool CaseInsensitive => true;
}
