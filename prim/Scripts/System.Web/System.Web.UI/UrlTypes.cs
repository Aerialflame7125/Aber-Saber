namespace System.Web.UI;

[Serializable]
[Flags]
public enum UrlTypes
{
	Absolute = 1,
	AppRelative = 2,
	DocRelative = 4,
	RootRelative = 8
}
