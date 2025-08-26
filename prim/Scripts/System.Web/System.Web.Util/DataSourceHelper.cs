using System.Collections;

namespace System.Web.Util;

internal class DataSourceHelper
{
	private DataSourceHelper()
	{
	}

	[Obsolete("Use DataSourceResolver")]
	public static IEnumerable GetResolvedDataSource(object o, string data_member)
	{
		return DataSourceResolver.ResolveDataSource(o, data_member);
	}
}
