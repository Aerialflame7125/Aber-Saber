namespace System.Web.Hosting;

internal struct HTTP_COOKED_URL
{
	internal readonly ushort FullUrlLength;

	internal readonly ushort HostLength;

	internal readonly ushort AbsPathLength;

	internal readonly ushort QueryStringLength;

	internal unsafe readonly char* pFullUrl;

	internal unsafe readonly char* pHost;

	internal unsafe readonly char* pAbsPath;

	internal unsafe readonly char* pQueryString;
}
