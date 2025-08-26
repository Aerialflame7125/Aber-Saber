using System.Collections.Generic;
using System.Text;

namespace System.Web.Caching;

[Serializable]
internal sealed class CachedVaryBy
{
	private string[] prms;

	private string[] headers;

	private string custom;

	private string key;

	private List<string> item_list;

	private bool wildCardParams;

	internal List<string> ItemList => item_list;

	internal string Key => key;

	internal CachedVaryBy(HttpCachePolicy policy, string key)
	{
		prms = policy.VaryByParams.GetParamNames();
		headers = policy.VaryByHeaders.GetHeaderNames(policy.OmitVaryStar);
		custom = policy.GetVaryByCustom();
		this.key = key;
		item_list = new List<string>();
		wildCardParams = policy.VaryByParams["*"];
	}

	internal string CreateKey(string file_path, HttpContext context)
	{
		if (string.IsNullOrEmpty(file_path))
		{
			throw new ArgumentNullException("file_path");
		}
		StringBuilder stringBuilder = new StringBuilder("vbk");
		HttpRequest httpRequest = context?.Request;
		stringBuilder.Append(file_path);
		if (httpRequest == null)
		{
			return stringBuilder.ToString();
		}
		stringBuilder.Append(httpRequest.HttpMethod);
		if (wildCardParams)
		{
			stringBuilder.Append("WQ");
			foreach (string item in httpRequest.QueryString)
			{
				if (item != null)
				{
					stringBuilder.Append('N');
					stringBuilder.Append(item.ToLowerInvariant());
					string value = httpRequest.QueryString[item];
					if (!string.IsNullOrEmpty(value))
					{
						stringBuilder.Append('V');
						stringBuilder.Append(value);
					}
				}
			}
			stringBuilder.Append('F');
			foreach (string item2 in httpRequest.Form)
			{
				if (item2 != null)
				{
					stringBuilder.Append('N');
					stringBuilder.Append(item2.ToLowerInvariant());
					string value = httpRequest.Form[item2];
					if (!string.IsNullOrEmpty(value))
					{
						stringBuilder.Append('V');
						stringBuilder.Append(value);
					}
				}
			}
		}
		else if (prms != null)
		{
			StringBuilder stringBuilder2 = null;
			stringBuilder.Append("SQ");
			for (int i = 0; i < prms.Length; i++)
			{
				string text3 = prms[i];
				if (string.IsNullOrEmpty(text3))
				{
					continue;
				}
				string value = httpRequest.QueryString[text3];
				if (value != null)
				{
					stringBuilder.Append('N');
					stringBuilder.Append(text3.ToLowerInvariant());
					if (value.Length > 0)
					{
						stringBuilder.Append('V');
						stringBuilder.Append(value);
					}
				}
				value = httpRequest.Form[text3];
				if (value != null)
				{
					if (stringBuilder2 == null)
					{
						stringBuilder2 = new StringBuilder(70);
					}
					stringBuilder.Append('N');
					stringBuilder.Append(text3.ToLowerInvariant());
					if (value.Length > 0)
					{
						stringBuilder.Append('V');
						stringBuilder.Append(value);
					}
				}
			}
			if (stringBuilder2 != null)
			{
				stringBuilder.Append(stringBuilder2.ToString());
			}
		}
		if (headers != null)
		{
			stringBuilder.Append('H');
			for (int j = 0; j < headers.Length; j++)
			{
				stringBuilder.Append('N');
				string text3 = headers[j];
				stringBuilder.Append(text3.ToLowerInvariant());
				string value = httpRequest.Headers[text3];
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.Append('V');
					stringBuilder.Append(value);
				}
			}
		}
		if (custom != null)
		{
			stringBuilder.Append('C');
			string varyByCustomString = context.ApplicationInstance.GetVaryByCustomString(context, custom);
			stringBuilder.Append('N');
			stringBuilder.Append(custom);
			stringBuilder.Append('V');
			stringBuilder.Append((varyByCustomString != null) ? varyByCustomString : "__null__");
		}
		return stringBuilder.ToString();
	}
}
