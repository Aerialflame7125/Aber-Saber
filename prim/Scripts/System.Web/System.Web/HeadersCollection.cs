namespace System.Web;

internal class HeadersCollection : BaseParamsCollection
{
	public HeadersCollection(HttpRequest request)
		: base(request)
	{
	}

	public override void Add(string name, string value)
	{
		if (base.IsReadOnly)
		{
			throw new PlatformNotSupportedException();
		}
		base.Set(name, value);
	}

	public override void Set(string name, string value)
	{
		if (base.IsReadOnly)
		{
			throw new PlatformNotSupportedException();
		}
		base.Set(name, value);
	}

	public override void Remove(string name)
	{
		if (base.IsReadOnly)
		{
			throw new PlatformNotSupportedException();
		}
		base.Remove(name);
	}

	protected override void InsertInfo()
	{
		HttpWorkerRequest workerRequest = _request.WorkerRequest;
		if (workerRequest == null)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			string knownRequestHeader = workerRequest.GetKnownRequestHeader(i);
			if (knownRequestHeader != null && !(knownRequestHeader == ""))
			{
				Add(HttpWorkerRequest.GetKnownRequestHeaderName(i), knownRequestHeader);
			}
		}
		string[][] unknownRequestHeaders = workerRequest.GetUnknownRequestHeaders();
		if (unknownRequestHeaders != null && unknownRequestHeaders.GetUpperBound(0) != -1)
		{
			int num = unknownRequestHeaders.GetUpperBound(0) + 1;
			for (int j = 0; j < num; j++)
			{
				Add(unknownRequestHeaders[j][0], unknownRequestHeaders[j][1]);
			}
		}
		Protect();
	}

	protected override string InternalGet(string name)
	{
		int knownRequestHeaderIndex = HttpWorkerRequest.GetKnownRequestHeaderIndex(name);
		string text = null;
		if (knownRequestHeaderIndex >= 0)
		{
			text = _request.WorkerRequest.GetKnownRequestHeader(knownRequestHeaderIndex);
		}
		if (text == null)
		{
			text = _request.WorkerRequest.GetUnknownRequestHeader(name);
		}
		return text;
	}
}
