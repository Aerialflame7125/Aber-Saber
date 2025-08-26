using System.Collections;

namespace System.Web;

internal class HttpRawResponse
{
	private int _statusCode;

	private string _statusDescr;

	private ArrayList _headers;

	private ArrayList _buffers;

	private bool _hasSubstBlocks;

	internal int StatusCode => _statusCode;

	internal string StatusDescription => _statusDescr;

	internal ArrayList Headers => _headers;

	internal ArrayList Buffers => _buffers;

	internal bool HasSubstBlocks => _hasSubstBlocks;

	internal HttpRawResponse(int statusCode, string statusDescription, ArrayList headers, ArrayList buffers, bool hasSubstBlocks)
	{
		_statusCode = statusCode;
		_statusDescr = statusDescription;
		_headers = headers;
		_buffers = buffers;
		_hasSubstBlocks = hasSubstBlocks;
	}
}
