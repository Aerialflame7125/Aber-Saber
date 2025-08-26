using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web.Routing;

internal static class RouteParser
{
	private static string GetLiteral(string segmentLiteral)
	{
		string text = segmentLiteral.Replace("{{", "").Replace("}}", "");
		if (text.Contains("{") || text.Contains("}"))
		{
			return null;
		}
		return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
	}

	private static int IndexOfFirstOpenParameter(string segment, int startIndex)
	{
		while (true)
		{
			startIndex = segment.IndexOf('{', startIndex);
			if (startIndex == -1)
			{
				return -1;
			}
			if (startIndex + 1 == segment.Length || (startIndex + 1 < segment.Length && segment[startIndex + 1] != '{'))
			{
				break;
			}
			startIndex += 2;
		}
		return startIndex;
	}

	internal static bool IsSeparator(string s)
	{
		return string.Equals(s, "/", StringComparison.Ordinal);
	}

	private static bool IsValidParameterName(string parameterName)
	{
		if (parameterName.Length == 0)
		{
			return false;
		}
		foreach (char c in parameterName)
		{
			if (c == '/' || c == '{' || c == '}')
			{
				return false;
			}
		}
		return true;
	}

	internal static bool IsInvalidRouteUrl(string routeUrl)
	{
		if (!routeUrl.StartsWith("~", StringComparison.Ordinal) && !routeUrl.StartsWith("/", StringComparison.Ordinal))
		{
			return routeUrl.IndexOf('?') != -1;
		}
		return true;
	}

	public static ParsedRoute Parse(string routeUrl)
	{
		if (routeUrl == null)
		{
			routeUrl = string.Empty;
		}
		if (IsInvalidRouteUrl(routeUrl))
		{
			throw new ArgumentException(global::SR.GetString("The route URL cannot start with a '/' or '~' character and it cannot contain a '?' character."), "routeUrl");
		}
		IList<string> list = SplitUrlToPathSegmentStrings(routeUrl);
		Exception ex = ValidateUrlParts(list);
		if (ex != null)
		{
			throw ex;
		}
		return new ParsedRoute(SplitUrlToPathSegments(list));
	}

	private static IList<PathSubsegment> ParseUrlSegment(string segment, out Exception exception)
	{
		int num = 0;
		List<PathSubsegment> list = new List<PathSubsegment>();
		while (num < segment.Length)
		{
			int num2 = IndexOfFirstOpenParameter(segment, num);
			if (num2 == -1)
			{
				string literal = GetLiteral(segment.Substring(num));
				if (literal == null)
				{
					exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
					return null;
				}
				if (literal.Length > 0)
				{
					list.Add(new LiteralSubsegment(literal));
				}
				break;
			}
			int num3 = segment.IndexOf('}', num2 + 1);
			if (num3 == -1)
			{
				exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
				return null;
			}
			string literal2 = GetLiteral(segment.Substring(num, num2 - num));
			if (literal2 == null)
			{
				exception = new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("There is an incomplete parameter in this path segment: '{0}'. Check that each '{{' character has a matching '}}' character."), segment), "routeUrl");
				return null;
			}
			if (literal2.Length > 0)
			{
				list.Add(new LiteralSubsegment(literal2));
			}
			string parameterName = segment.Substring(num2 + 1, num3 - num2 - 1);
			list.Add(new ParameterSubsegment(parameterName));
			num = num3 + 1;
		}
		exception = null;
		return list;
	}

	private static IList<PathSegment> SplitUrlToPathSegments(IList<string> urlParts)
	{
		List<PathSegment> list = new List<PathSegment>();
		foreach (string urlPart in urlParts)
		{
			if (IsSeparator(urlPart))
			{
				list.Add(new SeparatorPathSegment());
				continue;
			}
			Exception exception;
			IList<PathSubsegment> subsegments = ParseUrlSegment(urlPart, out exception);
			list.Add(new ContentPathSegment(subsegments));
		}
		return list;
	}

	internal static IList<string> SplitUrlToPathSegmentStrings(string url)
	{
		List<string> list = new List<string>();
		if (string.IsNullOrEmpty(url))
		{
			return list;
		}
		int num = 0;
		while (num < url.Length)
		{
			int num2 = url.IndexOf('/', num);
			if (num2 == -1)
			{
				string text = url.Substring(num);
				if (text.Length > 0)
				{
					list.Add(text);
				}
				break;
			}
			string text2 = url.Substring(num, num2 - num);
			if (text2.Length > 0)
			{
				list.Add(text2);
			}
			list.Add("/");
			num = num2 + 1;
		}
		return list;
	}

	private static Exception ValidateUrlParts(IList<string> pathSegments)
	{
		HashSet<string> usedParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		bool? flag = null;
		bool flag2 = false;
		foreach (string pathSegment in pathSegments)
		{
			if (flag2)
			{
				return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A catch-all parameter can only appear as the last segment of the route URL.")), "routeUrl");
			}
			bool flag3;
			if (!flag.HasValue)
			{
				flag = IsSeparator(pathSegment);
				flag3 = flag.Value;
			}
			else
			{
				flag3 = IsSeparator(pathSegment);
				if (flag3 && flag.Value)
				{
					return new ArgumentException(global::SR.GetString("The route URL separator character '/' cannot appear consecutively. It must be separated by either a parameter or a literal value."), "routeUrl");
				}
				flag = flag3;
			}
			if (!flag3)
			{
				Exception exception;
				IList<PathSubsegment> list = ParseUrlSegment(pathSegment, out exception);
				if (exception != null)
				{
					return exception;
				}
				exception = ValidateUrlSegment(list, usedParameterNames, pathSegment);
				if (exception != null)
				{
					return exception;
				}
				flag2 = list.Any((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);
			}
		}
		return null;
	}

	private static Exception ValidateUrlSegment(IList<PathSubsegment> pathSubsegments, HashSet<string> usedParameterNames, string pathSegment)
	{
		bool flag = false;
		Type type = null;
		foreach (PathSubsegment pathSubsegment in pathSubsegments)
		{
			if (type != null && type == pathSubsegment.GetType())
			{
				return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A path segment cannot contain two consecutive parameters. They must be separated by a '/' or by a literal string.")), "routeUrl");
			}
			type = pathSubsegment.GetType();
			if (!(pathSubsegment is LiteralSubsegment) && pathSubsegment is ParameterSubsegment { ParameterName: var parameterName } parameterSubsegment)
			{
				if (parameterSubsegment.IsCatchAll)
				{
					flag = true;
				}
				if (!IsValidParameterName(parameterName))
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route parameter name '{0}' is invalid. Route parameter names must be non-empty and cannot contain these characters: \"{{\", \"}}\", \"/\", \"?\""), parameterName), "routeUrl");
				}
				if (usedParameterNames.Contains(parameterName))
				{
					return new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route parameter name '{0}' appears more than one time in the URL."), parameterName), "routeUrl");
				}
				usedParameterNames.Add(parameterName);
			}
		}
		if (flag && pathSubsegments.Count != 1)
		{
			return new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A path segment that contains more than one section, such as a literal section or a parameter, cannot contain a catch-all parameter.")), "routeUrl");
		}
		return null;
	}
}
