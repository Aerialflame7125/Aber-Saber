using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;

namespace System.Web.Configuration.nBrowser;

internal class Result : CapabilitiesResult
{
	private Dictionary<Type, Type> AdapterTypeMap;

	private StringCollection Track;

	internal Type MarkupTextWriter;

	public StringCollection Tracks => Track;

	internal Result(IDictionary items)
		: base(items)
	{
		AdapterTypeMap = new Dictionary<Type, Type>();
		Track = new StringCollection();
		MarkupTextWriter = typeof(HtmlTextWriter);
	}

	internal void AddTrack(string track)
	{
		Track.Add(track);
	}

	internal void AddAdapter(Type controlType, Type adapterType)
	{
		AdapterTypeMap[controlType] = adapterType;
	}

	internal override Type GetTagWriter()
	{
		return MarkupTextWriter;
	}

	internal override IDictionary GetAdapters()
	{
		return AdapterTypeMap;
	}
}
