using System.Web.Configuration;

namespace System.Web;

internal sealed class TraceManager
{
	private bool enabled;

	private bool local_only = true;

	private bool page_output;

	private TraceMode mode;

	private int request_limit = 10;

	private int cur_item;

	private TraceData[] data;

	private Exception initialException;

	public bool HasException => initialException != null;

	public Exception InitialException => initialException;

	public bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			enabled = value;
		}
	}

	public bool LocalOnly
	{
		get
		{
			return local_only;
		}
		set
		{
			local_only = value;
		}
	}

	public bool PageOutput
	{
		get
		{
			return page_output;
		}
		set
		{
			page_output = value;
		}
	}

	public int RequestLimit
	{
		get
		{
			return request_limit;
		}
		set
		{
			if (request_limit != value)
			{
				TraceData[] destinationArray = new TraceData[value];
				Array.Copy(data, destinationArray, (cur_item > value) ? value : cur_item);
				if (cur_item > value)
				{
					cur_item = value;
				}
				request_limit = value;
			}
		}
	}

	public TraceMode TraceMode
	{
		get
		{
			return mode;
		}
		set
		{
			mode = value;
		}
	}

	public TraceData[] TraceData => data;

	public int ItemCount => cur_item;

	public TraceManager()
	{
		try
		{
			mode = TraceMode.SortByTime;
			TraceSection traceSection = WebConfigurationManager.GetWebApplicationSection("system.web/trace") as TraceSection;
			if (traceSection == null)
			{
				traceSection = new TraceSection();
			}
			if (traceSection != null)
			{
				enabled = traceSection.Enabled;
				local_only = traceSection.LocalOnly;
				page_output = traceSection.PageOutput;
				if (traceSection.TraceMode == TraceDisplayMode.SortByTime)
				{
					mode = TraceMode.SortByTime;
				}
				else
				{
					mode = TraceMode.SortByCategory;
				}
				request_limit = traceSection.RequestLimit;
			}
		}
		catch (Exception ex)
		{
			initialException = ex;
		}
	}

	public void AddTraceData(TraceData item)
	{
		if (data == null)
		{
			data = new TraceData[request_limit];
		}
		if (cur_item != request_limit)
		{
			data[cur_item++] = item;
		}
	}

	public void Clear()
	{
		if (data != null)
		{
			Array.Clear(data, 0, data.Length);
			cur_item = 0;
		}
	}
}
