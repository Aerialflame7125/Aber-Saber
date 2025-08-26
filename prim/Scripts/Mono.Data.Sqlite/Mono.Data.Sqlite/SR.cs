using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace Mono.Data.Sqlite;

[DebuggerNonUserCode]
internal class SR
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				resourceMan = new ResourceManager("SR", typeof(SR).Assembly);
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static string DataTypes => ResourceManager.GetString("DataTypes", resourceCulture);

	internal static string Keywords => ResourceManager.GetString("Keywords", resourceCulture);

	internal static string MetaDataCollections => ResourceManager.GetString("MetaDataCollections", resourceCulture);

	internal SR()
	{
	}
}
