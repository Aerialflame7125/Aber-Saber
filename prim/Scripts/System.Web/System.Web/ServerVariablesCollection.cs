using System.Security.Principal;
using System.Text;
using System.Web.Util;

namespace System.Web;

internal sealed class ServerVariablesCollection : BaseParamsCollection
{
	private bool loaded;

	private string QueryString
	{
		get
		{
			string queryStringRaw = _request.QueryStringRaw;
			if (string.IsNullOrEmpty(queryStringRaw))
			{
				return queryStringRaw;
			}
			if (queryStringRaw[0] == '?')
			{
				return queryStringRaw.Substring(1);
			}
			return queryStringRaw;
		}
	}

	private IIdentity UserIdentity => (((_request != null) ? _request.Context : null)?.User)?.Identity;

	public ServerVariablesCollection(HttpRequest request)
		: base(request)
	{
		base.IsReadOnly = true;
	}

	private void AppendKeyValue(StringBuilder sb, string key, string value, bool standard)
	{
		if (standard)
		{
			sb.Append("HTTP_");
			sb.Append(key.ToUpper(Helpers.InvariantCulture).Replace('-', '_'));
			sb.Append(":");
		}
		else
		{
			sb.Append(key);
			sb.Append(": ");
		}
		sb.Append(value);
		sb.Append("\r\n");
	}

	private string Fill(HttpWorkerRequest wr, bool standard)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < 40; i++)
		{
			string knownRequestHeader = wr.GetKnownRequestHeader(i);
			if (!string.IsNullOrEmpty(knownRequestHeader))
			{
				string knownRequestHeaderName = HttpWorkerRequest.GetKnownRequestHeaderName(i);
				AppendKeyValue(stringBuilder, knownRequestHeaderName, knownRequestHeader, standard);
			}
		}
		string[][] unknownRequestHeaders = wr.GetUnknownRequestHeaders();
		if (unknownRequestHeaders == null)
		{
			return stringBuilder.ToString();
		}
		int num = unknownRequestHeaders.Length;
		while (num > 0)
		{
			num--;
			AppendKeyValue(stringBuilder, unknownRequestHeaders[num][0], unknownRequestHeaders[num][1], standard);
		}
		return stringBuilder.ToString();
	}

	private void AddHeaderVariables(HttpWorkerRequest wr)
	{
		for (int i = 0; i < 40; i++)
		{
			string knownRequestHeader = wr.GetKnownRequestHeader(i);
			if (knownRequestHeader != null && knownRequestHeader.Length > 0)
			{
				string knownRequestHeaderName = HttpWorkerRequest.GetKnownRequestHeaderName(i);
				if (knownRequestHeaderName != null && knownRequestHeaderName.Length > 0)
				{
					Add("HTTP_" + knownRequestHeaderName.ToUpper(Helpers.InvariantCulture).Replace('-', '_'), knownRequestHeader);
				}
			}
		}
		string[][] unknownRequestHeaders = wr.GetUnknownRequestHeaders();
		if (unknownRequestHeaders == null)
		{
			return;
		}
		for (int j = 0; j < unknownRequestHeaders.Length; j++)
		{
			string knownRequestHeaderName = unknownRequestHeaders[j][0];
			if (knownRequestHeaderName != null)
			{
				string knownRequestHeader = unknownRequestHeaders[j][1];
				Add("HTTP_" + knownRequestHeaderName.ToUpper(Helpers.InvariantCulture).Replace('-', '_'), knownRequestHeader);
			}
		}
	}

	private void loadServerVariablesCollection()
	{
		HttpWorkerRequest workerRequest = _request.WorkerRequest;
		if (!loaded && workerRequest != null)
		{
			base.IsReadOnly = false;
			Add("ALL_HTTP", Fill(workerRequest, standard: true));
			Add("ALL_RAW", Fill(workerRequest, standard: false));
			Add("APPL_MD_PATH", workerRequest.GetServerVariable("APPL_MD_PATH"));
			Add("APPL_PHYSICAL_PATH", workerRequest.GetServerVariable("APPL_PHYSICAL_PATH"));
			IIdentity userIdentity = UserIdentity;
			if (userIdentity != null && userIdentity.IsAuthenticated)
			{
				Add("AUTH_TYPE", userIdentity.AuthenticationType);
				Add("AUTH_USER", userIdentity.Name);
			}
			else
			{
				Add("AUTH_TYPE", string.Empty);
				Add("AUTH_USER", string.Empty);
			}
			Add("AUTH_PASSWORD", workerRequest.GetServerVariable("AUTH_PASSWORD"));
			Add("LOGON_USER", workerRequest.GetServerVariable("LOGON_USER"));
			Add("REMOTE_USER", workerRequest.GetServerVariable("REMOTE_USER"));
			Add("CERT_COOKIE", workerRequest.GetServerVariable("CERT_COOKIE"));
			Add("CERT_FLAGS", workerRequest.GetServerVariable("CERT_FLAGS"));
			Add("CERT_ISSUER", workerRequest.GetServerVariable("CERT_ISSUER"));
			Add("CERT_KEYSIZE", workerRequest.GetServerVariable("CERT_KEYSIZE"));
			Add("CERT_SECRETKEYSIZE", workerRequest.GetServerVariable("CERT_SECRETKEYSIZE"));
			Add("CERT_SERIALNUMBER", workerRequest.GetServerVariable("CERT_SERIALNUMBER"));
			Add("CERT_SERVER_ISSUER", workerRequest.GetServerVariable("CERT_SERVER_ISSUER"));
			Add("CERT_SERVER_SUBJECT", workerRequest.GetServerVariable("CERT_SERVER_SUBJECT"));
			Add("CERT_SUBJECT", workerRequest.GetServerVariable("CERT_SUBJECT"));
			string knownRequestHeader = workerRequest.GetKnownRequestHeader(11);
			if (knownRequestHeader != null)
			{
				Add("CONTENT_LENGTH", knownRequestHeader);
			}
			Add("CONTENT_TYPE", _request.ContentType);
			Add("GATEWAY_INTERFACE", workerRequest.GetServerVariable("GATEWAY_INTERFACE"));
			Add("HTTPS", workerRequest.GetServerVariable("HTTPS"));
			Add("HTTPS_KEYSIZE", workerRequest.GetServerVariable("HTTPS_KEYSIZE"));
			Add("HTTPS_SECRETKEYSIZE", workerRequest.GetServerVariable("HTTPS_SECRETKEYSIZE"));
			Add("HTTPS_SERVER_ISSUER", workerRequest.GetServerVariable("HTTPS_SERVER_ISSUER"));
			Add("HTTPS_SERVER_SUBJECT", workerRequest.GetServerVariable("HTTPS_SERVER_SUBJECT"));
			Add("INSTANCE_ID", workerRequest.GetServerVariable("INSTANCE_ID"));
			Add("INSTANCE_META_PATH", workerRequest.GetServerVariable("INSTANCE_META_PATH"));
			Add("LOCAL_ADDR", workerRequest.GetLocalAddress());
			Add("PATH_INFO", _request.PathInfo);
			Add("PATH_TRANSLATED", _request.PhysicalPath);
			Add("QUERY_STRING", QueryString);
			Add("REMOTE_ADDR", _request.UserHostAddress);
			Add("REMOTE_HOST", _request.UserHostName);
			Add("REMOTE_PORT", workerRequest.GetRemotePort().ToString());
			Add("REQUEST_METHOD", _request.HttpMethod);
			Add("SCRIPT_NAME", _request.FilePath);
			Add("SERVER_NAME", workerRequest.GetServerName());
			Add("SERVER_PORT", workerRequest.GetLocalPort().ToString());
			if (workerRequest.IsSecure())
			{
				Add("SERVER_PORT_SECURE", "1");
			}
			else
			{
				Add("SERVER_PORT_SECURE", "0");
			}
			Add("SERVER_PROTOCOL", workerRequest.GetHttpVersion());
			Add("SERVER_SOFTWARE", workerRequest.GetServerVariable("SERVER_SOFTWARE"));
			Add("URL", _request.FilePath);
			AddHeaderVariables(workerRequest);
			base.IsReadOnly = true;
			loaded = true;
		}
	}

	protected override void InsertInfo()
	{
		loadServerVariablesCollection();
	}

	protected override string InternalGet(string name)
	{
		if (name == null || _request == null)
		{
			return null;
		}
		name = name.ToUpper(Helpers.InvariantCulture);
		switch (name)
		{
		case "AUTH_TYPE":
		{
			IIdentity userIdentity = UserIdentity;
			if (userIdentity != null && userIdentity.IsAuthenticated)
			{
				return userIdentity.AuthenticationType;
			}
			return string.Empty;
		}
		case "AUTH_USER":
		{
			IIdentity userIdentity = UserIdentity;
			if (userIdentity != null && userIdentity.IsAuthenticated)
			{
				return userIdentity.Name;
			}
			return string.Empty;
		}
		case "QUERY_STRING":
			return QueryString;
		case "PATH_INFO":
			return _request.PathInfo;
		case "PATH_TRANSLATED":
			return _request.PhysicalPath;
		case "REQUEST_METHOD":
			return _request.HttpMethod;
		case "REMOTE_ADDR":
			return _request.UserHostAddress;
		case "REMOTE_HOST":
			return _request.UserHostName;
		case "REMOTE_ADDRESS":
			return _request.UserHostAddress;
		case "SCRIPT_NAME":
			return _request.FilePath;
		case "LOCAL_ADDR":
			return _request.WorkerRequest.GetLocalAddress();
		case "SERVER_PROTOCOL":
			return _request.WorkerRequest.GetHttpVersion();
		case "CONTENT_TYPE":
			return _request.ContentType;
		case "REMOTE_PORT":
			return _request.WorkerRequest.GetRemotePort().ToString();
		case "SERVER_NAME":
			return _request.WorkerRequest.GetServerName();
		case "SERVER_PORT":
			return _request.WorkerRequest.GetLocalPort().ToString();
		case "APPL_PHYSICAL_PATH":
			return _request.WorkerRequest.GetAppPathTranslated();
		case "URL":
			return _request.FilePath;
		case "SERVER_PORT_SECURE":
			if (!_request.WorkerRequest.IsSecure())
			{
				return "0";
			}
			return "1";
		case "ALL_HTTP":
			return Fill(_request.WorkerRequest, standard: true);
		case "ALL_RAW":
			return Fill(_request.WorkerRequest, standard: false);
		case "REMOTE_USER":
		case "SERVER_SOFTWARE":
		case "APPL_MD_PATH":
		case "AUTH_PASSWORD":
		case "CERT_COOKIE":
		case "CERT_FLAGS":
		case "CERT_ISSUER":
		case "CERT_KEYSIZE":
		case "CERT_SECRETKEYSIZE":
		case "CERT_SERIALNUMBER":
		case "CERT_SERVER_ISSUER":
		case "CERT_SERVER_SUBJECT":
		case "GATEWAY_INTERFACE":
		case "HTTPS":
		case "HTTPS_KEYSIZE":
		case "HTTPS_SECRETKEYSIZE":
		case "HTTPS_SERVER_ISSUER":
		case "HTTPS_SERVER_SUBJECT":
		case "INSTANCE_ID":
		case "INSTANCE_META_PATH":
		case "LOGON_USER":
		case "HTTP_ACCEPT":
		case "HTTP_REFERER":
		case "HTTP_ACCEPT_LANGUAGE":
		case "HTTP_ACCEPT_ENCODING":
		case "HTTP_CONNECTION":
		case "HTTP_HOST":
		case "HTTP_USER_AGENT":
		case "HTTP_SOAPACTION":
			return _request.WorkerRequest.GetServerVariable(name);
		default:
			return null;
		}
	}
}
