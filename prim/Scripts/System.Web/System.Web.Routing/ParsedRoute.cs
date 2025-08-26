using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Routing;

internal sealed class ParsedRoute
{
	private IList<PathSegment> PathSegments { get; set; }

	public ParsedRoute(IList<PathSegment> pathSegments)
	{
		PathSegments = pathSegments;
	}

	public BoundUrl Bind(RouteValueDictionary currentValues, RouteValueDictionary values, RouteValueDictionary defaultValues, RouteValueDictionary constraints)
	{
		if (currentValues == null)
		{
			currentValues = new RouteValueDictionary();
		}
		if (values == null)
		{
			values = new RouteValueDictionary();
		}
		if (defaultValues == null)
		{
			defaultValues = new RouteValueDictionary();
		}
		RouteValueDictionary acceptedValues = new RouteValueDictionary();
		HashSet<string> unusedNewValues = new HashSet<string>(values.Keys, StringComparer.OrdinalIgnoreCase);
		ForEachParameter(PathSegments, delegate(ParameterSubsegment parameterSubsegment)
		{
			string parameterName = parameterSubsegment.ParameterName;
			object value;
			bool flag = values.TryGetValue(parameterName, out value);
			if (flag)
			{
				unusedNewValues.Remove(parameterName);
			}
			object value2;
			bool flag2 = currentValues.TryGetValue(parameterName, out value2);
			if (flag && flag2 && !RoutePartsEqual(value2, value))
			{
				return false;
			}
			if (flag)
			{
				if (IsRoutePartNonEmpty(value))
				{
					acceptedValues.Add(parameterName, value);
				}
			}
			else if (flag2)
			{
				acceptedValues.Add(parameterName, value2);
			}
			return true;
		});
		foreach (KeyValuePair<string, object> value7 in values)
		{
			if (IsRoutePartNonEmpty(value7.Value) && !acceptedValues.ContainsKey(value7.Key))
			{
				acceptedValues.Add(value7.Key, value7.Value);
			}
		}
		foreach (KeyValuePair<string, object> currentValue in currentValues)
		{
			string key = currentValue.Key;
			if (!acceptedValues.ContainsKey(key) && GetParameterSubsegment(PathSegments, key) == null)
			{
				acceptedValues.Add(key, currentValue.Value);
			}
		}
		ForEachParameter(PathSegments, delegate(ParameterSubsegment parameterSubsegment)
		{
			if (!acceptedValues.ContainsKey(parameterSubsegment.ParameterName) && !IsParameterRequired(parameterSubsegment, defaultValues, out var defaultValue))
			{
				acceptedValues.Add(parameterSubsegment.ParameterName, defaultValue);
			}
			return true;
		});
		if (!ForEachParameter(PathSegments, (ParameterSubsegment parameterSubsegment) => (!IsParameterRequired(parameterSubsegment, defaultValues, out var _) || acceptedValues.ContainsKey(parameterSubsegment.ParameterName)) ? true : false))
		{
			return null;
		}
		RouteValueDictionary otherDefaultValues = new RouteValueDictionary(defaultValues);
		ForEachParameter(PathSegments, delegate(ParameterSubsegment parameterSubsegment)
		{
			otherDefaultValues.Remove(parameterSubsegment.ParameterName);
			return true;
		});
		foreach (KeyValuePair<string, object> item in otherDefaultValues)
		{
			if (values.TryGetValue(item.Key, out var value3))
			{
				unusedNewValues.Remove(item.Key);
				if (!RoutePartsEqual(value3, item.Value))
				{
					return null;
				}
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		bool flag3 = false;
		bool flag4 = false;
		for (int i = 0; i < PathSegments.Count; i++)
		{
			PathSegment pathSegment = PathSegments[i];
			if (pathSegment is SeparatorPathSegment)
			{
				if (flag3 && stringBuilder2.Length > 0)
				{
					if (flag4)
					{
						return null;
					}
					stringBuilder.Append(stringBuilder2.ToString());
					stringBuilder2.Length = 0;
				}
				flag3 = false;
				if (stringBuilder2.Length > 0 && stringBuilder2[stringBuilder2.Length - 1] == '/')
				{
					if (flag4)
					{
						return null;
					}
					stringBuilder.Append(stringBuilder2.ToString(0, stringBuilder2.Length - 1));
					stringBuilder2.Length = 0;
					flag4 = true;
				}
				else
				{
					stringBuilder2.Append("/");
				}
			}
			else
			{
				if (!(pathSegment is ContentPathSegment contentPathSegment))
				{
					continue;
				}
				bool flag5 = false;
				foreach (PathSubsegment subsegment in contentPathSegment.Subsegments)
				{
					if (subsegment is LiteralSubsegment literalSubsegment)
					{
						flag3 = true;
						stringBuilder2.Append(UrlEncode(literalSubsegment.Literal));
					}
					else
					{
						if (!(subsegment is ParameterSubsegment parameterSubsegment2))
						{
							continue;
						}
						if (flag3 && stringBuilder2.Length > 0)
						{
							if (flag4)
							{
								return null;
							}
							stringBuilder.Append(stringBuilder2.ToString());
							stringBuilder2.Length = 0;
							flag5 = true;
						}
						flag3 = false;
						if (acceptedValues.TryGetValue(parameterSubsegment2.ParameterName, out var value4))
						{
							unusedNewValues.Remove(parameterSubsegment2.ParameterName);
						}
						defaultValues.TryGetValue(parameterSubsegment2.ParameterName, out var value5);
						if (RoutePartsEqual(value4, value5))
						{
							stringBuilder2.Append(UrlEncode(Convert.ToString(value4, CultureInfo.InvariantCulture)));
							continue;
						}
						if (flag4)
						{
							return null;
						}
						if (stringBuilder2.Length > 0)
						{
							stringBuilder.Append(stringBuilder2.ToString());
							stringBuilder2.Length = 0;
						}
						stringBuilder.Append(UrlEncode(Convert.ToString(value4, CultureInfo.InvariantCulture)));
						flag5 = true;
					}
				}
				if (flag5 && stringBuilder2.Length > 0)
				{
					if (flag4)
					{
						return null;
					}
					stringBuilder.Append(stringBuilder2.ToString());
					stringBuilder2.Length = 0;
				}
			}
		}
		if (flag3 && stringBuilder2.Length > 0)
		{
			if (flag4)
			{
				return null;
			}
			stringBuilder.Append(stringBuilder2.ToString());
		}
		if (constraints != null)
		{
			foreach (KeyValuePair<string, object> constraint in constraints)
			{
				unusedNewValues.Remove(constraint.Key);
			}
		}
		if (unusedNewValues.Count > 0)
		{
			bool flag6 = true;
			foreach (string item2 in unusedNewValues)
			{
				if (acceptedValues.TryGetValue(item2, out var value6))
				{
					stringBuilder.Append(flag6 ? '?' : '&');
					flag6 = false;
					stringBuilder.Append(Uri.EscapeDataString(item2));
					stringBuilder.Append('=');
					stringBuilder.Append(Uri.EscapeDataString(Convert.ToString(value6, CultureInfo.InvariantCulture)));
				}
			}
		}
		return new BoundUrl
		{
			Url = stringBuilder.ToString(),
			Values = acceptedValues
		};
	}

	private static string EscapeReservedCharacters(Match m)
	{
		return "%" + Convert.ToUInt16(m.Value[0]).ToString("x2", CultureInfo.InvariantCulture);
	}

	private static bool ForEachParameter(IList<PathSegment> pathSegments, Func<ParameterSubsegment, bool> action)
	{
		for (int i = 0; i < pathSegments.Count; i++)
		{
			PathSegment pathSegment = pathSegments[i];
			if (pathSegment is SeparatorPathSegment || !(pathSegment is ContentPathSegment contentPathSegment))
			{
				continue;
			}
			foreach (PathSubsegment subsegment in contentPathSegment.Subsegments)
			{
				if (!(subsegment is LiteralSubsegment) && subsegment is ParameterSubsegment arg && !action(arg))
				{
					return false;
				}
			}
		}
		return true;
	}

	private static ParameterSubsegment GetParameterSubsegment(IList<PathSegment> pathSegments, string parameterName)
	{
		ParameterSubsegment foundParameterSubsegment = null;
		ForEachParameter(pathSegments, delegate(ParameterSubsegment parameterSubsegment)
		{
			if (string.Equals(parameterName, parameterSubsegment.ParameterName, StringComparison.OrdinalIgnoreCase))
			{
				foundParameterSubsegment = parameterSubsegment;
				return false;
			}
			return true;
		});
		return foundParameterSubsegment;
	}

	private static bool IsParameterRequired(ParameterSubsegment parameterSubsegment, RouteValueDictionary defaultValues, out object defaultValue)
	{
		if (parameterSubsegment.IsCatchAll)
		{
			defaultValue = null;
			return false;
		}
		return !defaultValues.TryGetValue(parameterSubsegment.ParameterName, out defaultValue);
	}

	private static bool IsRoutePartNonEmpty(object routePart)
	{
		if (routePart is string text)
		{
			return text.Length > 0;
		}
		return routePart != null;
	}

	public RouteValueDictionary Match(string virtualPath, RouteValueDictionary defaultValues)
	{
		IList<string> list = RouteParser.SplitUrlToPathSegmentStrings(virtualPath);
		if (defaultValues == null)
		{
			defaultValues = new RouteValueDictionary();
		}
		RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < PathSegments.Count; i++)
		{
			PathSegment pathSegment = PathSegments[i];
			if (list.Count <= i)
			{
				flag = true;
			}
			string text = (flag ? null : list[i]);
			if (pathSegment is SeparatorPathSegment)
			{
				if (!flag && !string.Equals(text, "/", StringComparison.Ordinal))
				{
					return null;
				}
			}
			else if (pathSegment is ContentPathSegment contentPathSegment)
			{
				if (contentPathSegment.IsCatchAll)
				{
					MatchCatchAll(contentPathSegment, list.Skip(i), defaultValues, routeValueDictionary);
					flag2 = true;
				}
				else if (!MatchContentPathSegment(contentPathSegment, text, defaultValues, routeValueDictionary))
				{
					return null;
				}
			}
		}
		if (!flag2 && PathSegments.Count < list.Count)
		{
			for (int j = PathSegments.Count; j < list.Count; j++)
			{
				if (!RouteParser.IsSeparator(list[j]))
				{
					return null;
				}
			}
		}
		if (defaultValues != null)
		{
			foreach (KeyValuePair<string, object> defaultValue in defaultValues)
			{
				if (!routeValueDictionary.ContainsKey(defaultValue.Key))
				{
					routeValueDictionary.Add(defaultValue.Key, defaultValue.Value);
				}
			}
		}
		return routeValueDictionary;
	}

	private void MatchCatchAll(ContentPathSegment contentPathSegment, IEnumerable<string> remainingRequestSegments, RouteValueDictionary defaultValues, RouteValueDictionary matchedValues)
	{
		string text = string.Join(string.Empty, remainingRequestSegments.ToArray());
		ParameterSubsegment parameterSubsegment = contentPathSegment.Subsegments[0] as ParameterSubsegment;
		object value;
		if (text.Length > 0)
		{
			value = text;
		}
		else
		{
			defaultValues.TryGetValue(parameterSubsegment.ParameterName, out value);
		}
		matchedValues.Add(parameterSubsegment.ParameterName, value);
	}

	private bool MatchContentPathSegment(ContentPathSegment routeSegment, string requestPathSegment, RouteValueDictionary defaultValues, RouteValueDictionary matchedValues)
	{
		if (string.IsNullOrEmpty(requestPathSegment))
		{
			if (routeSegment.Subsegments.Count > 1)
			{
				return false;
			}
			if (!(routeSegment.Subsegments[0] is ParameterSubsegment parameterSubsegment))
			{
				return false;
			}
			if (defaultValues.TryGetValue(parameterSubsegment.ParameterName, out var value))
			{
				matchedValues.Add(parameterSubsegment.ParameterName, value);
				return true;
			}
			return false;
		}
		int num = requestPathSegment.Length;
		int num2 = routeSegment.Subsegments.Count - 1;
		ParameterSubsegment parameterSubsegment2 = null;
		LiteralSubsegment literalSubsegment = null;
		while (num2 >= 0)
		{
			int num3 = num;
			ParameterSubsegment parameterSubsegment3 = routeSegment.Subsegments[num2] as ParameterSubsegment;
			if (parameterSubsegment3 != null)
			{
				parameterSubsegment2 = parameterSubsegment3;
			}
			else if (routeSegment.Subsegments[num2] is LiteralSubsegment literalSubsegment2)
			{
				literalSubsegment = literalSubsegment2;
				int num4 = num - 1;
				if (parameterSubsegment2 != null)
				{
					num4--;
				}
				if (num4 < 0)
				{
					return false;
				}
				int num5 = requestPathSegment.LastIndexOf(literalSubsegment2.Literal, num4, StringComparison.OrdinalIgnoreCase);
				if (num5 == -1)
				{
					return false;
				}
				if (num2 == routeSegment.Subsegments.Count - 1 && num5 + literalSubsegment2.Literal.Length != requestPathSegment.Length)
				{
					return false;
				}
				num3 = num5;
			}
			if (parameterSubsegment2 != null && ((literalSubsegment != null && parameterSubsegment3 == null) || num2 == 0))
			{
				int num6;
				int length;
				if (literalSubsegment == null)
				{
					num6 = ((num2 != 0) ? num3 : 0);
					length = num;
				}
				else if (num2 == 0 && parameterSubsegment3 != null)
				{
					num6 = 0;
					length = num;
				}
				else
				{
					num6 = num3 + literalSubsegment.Literal.Length;
					length = num - num6;
				}
				string value2 = requestPathSegment.Substring(num6, length);
				if (string.IsNullOrEmpty(value2))
				{
					return false;
				}
				matchedValues.Add(parameterSubsegment2.ParameterName, value2);
				parameterSubsegment2 = null;
				literalSubsegment = null;
			}
			num = num3;
			num2--;
		}
		if (num != 0)
		{
			return routeSegment.Subsegments[0] is ParameterSubsegment;
		}
		return true;
	}

	private static bool RoutePartsEqual(object a, object b)
	{
		string text = a as string;
		string text2 = b as string;
		if (text != null && text2 != null)
		{
			return string.Equals(text, text2, StringComparison.OrdinalIgnoreCase);
		}
		if (a != null && b != null)
		{
			return a.Equals(b);
		}
		return a == b;
	}

	private static string UrlEncode(string str)
	{
		return Regex.Replace(Uri.EscapeUriString(str), "([#;?:@&=+$,])", EscapeReservedCharacters);
	}
}
