using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace System.Web.Services.Protocols;

internal class MatchMember
{
	private MemberInfo memberInfo;

	private Regex regex;

	private int group;

	private int capture;

	private int maxRepeats;

	private MatchType matchType;

	internal void Match(object target, string text)
	{
		if (memberInfo is FieldInfo)
		{
			((FieldInfo)memberInfo).SetValue(target, (matchType == null) ? MatchString(text) : MatchClass(text));
		}
		else if (memberInfo is PropertyInfo)
		{
			((PropertyInfo)memberInfo).SetValue(target, (matchType == null) ? MatchString(text) : MatchClass(text), new object[0]);
		}
	}

	private object MatchString(string text)
	{
		Match match = regex.Match(text);
		if (((memberInfo is FieldInfo) ? ((FieldInfo)memberInfo).FieldType : ((PropertyInfo)memberInfo).PropertyType).IsArray)
		{
			ArrayList arrayList = new ArrayList();
			int num = 0;
			while (match.Success && num < maxRepeats)
			{
				if (match.Groups.Count <= this.group)
				{
					throw BadGroupIndexException(this.group, memberInfo.Name, match.Groups.Count - 1);
				}
				foreach (Capture capture3 in match.Groups[this.group].Captures)
				{
					arrayList.Add(text.Substring(capture3.Index, capture3.Length));
				}
				match = match.NextMatch();
				num++;
			}
			return arrayList.ToArray(typeof(string));
		}
		if (match.Success)
		{
			if (match.Groups.Count <= this.group)
			{
				throw BadGroupIndexException(this.group, memberInfo.Name, match.Groups.Count - 1);
			}
			Group group = match.Groups[this.group];
			if (group.Captures.Count > 0)
			{
				if (group.Captures.Count <= capture)
				{
					throw BadCaptureIndexException(capture, memberInfo.Name, group.Captures.Count - 1);
				}
				Capture capture2 = group.Captures[capture];
				return text.Substring(capture2.Index, capture2.Length);
			}
		}
		return null;
	}

	private object MatchClass(string text)
	{
		Match match = regex.Match(text);
		if (((memberInfo is FieldInfo) ? ((FieldInfo)memberInfo).FieldType : ((PropertyInfo)memberInfo).PropertyType).IsArray)
		{
			ArrayList arrayList = new ArrayList();
			int num = 0;
			while (match.Success && num < maxRepeats)
			{
				if (match.Groups.Count <= this.group)
				{
					throw BadGroupIndexException(this.group, memberInfo.Name, match.Groups.Count - 1);
				}
				foreach (Capture capture3 in match.Groups[this.group].Captures)
				{
					arrayList.Add(matchType.Match(text.Substring(capture3.Index, capture3.Length)));
				}
				match = match.NextMatch();
				num++;
			}
			return arrayList.ToArray(matchType.Type);
		}
		if (match.Success)
		{
			if (match.Groups.Count <= this.group)
			{
				throw BadGroupIndexException(this.group, memberInfo.Name, match.Groups.Count - 1);
			}
			Group group = match.Groups[this.group];
			if (group.Captures.Count > 0)
			{
				if (group.Captures.Count <= capture)
				{
					throw BadCaptureIndexException(capture, memberInfo.Name, group.Captures.Count - 1);
				}
				Capture capture2 = group.Captures[capture];
				return matchType.Match(text.Substring(capture2.Index, capture2.Length));
			}
		}
		return null;
	}

	private static Exception BadCaptureIndexException(int index, string matchName, int highestIndex)
	{
		return new Exception(Res.GetString("WebTextMatchBadCaptureIndex", index, matchName, highestIndex));
	}

	private static Exception BadGroupIndexException(int index, string matchName, int highestIndex)
	{
		return new Exception(Res.GetString("WebTextMatchBadGroupIndex", index, matchName, highestIndex));
	}

	internal static MatchMember Reflect(MemberInfo memberInfo)
	{
		Type type = null;
		if (memberInfo is PropertyInfo)
		{
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			if (!propertyInfo.CanRead)
			{
				return null;
			}
			if (!propertyInfo.CanWrite)
			{
				return null;
			}
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			if (getMethod.IsStatic)
			{
				return null;
			}
			if (getMethod.GetParameters().Length != 0)
			{
				return null;
			}
			type = propertyInfo.PropertyType;
		}
		if (memberInfo is FieldInfo)
		{
			FieldInfo fieldInfo = (FieldInfo)memberInfo;
			if (!fieldInfo.IsPublic)
			{
				return null;
			}
			if (fieldInfo.IsStatic)
			{
				return null;
			}
			if (fieldInfo.IsSpecialName)
			{
				return null;
			}
			type = fieldInfo.FieldType;
		}
		object[] customAttributes = memberInfo.GetCustomAttributes(typeof(MatchAttribute), inherit: false);
		if (customAttributes.Length == 0)
		{
			return null;
		}
		MatchAttribute matchAttribute = (MatchAttribute)customAttributes[0];
		MatchMember matchMember = new MatchMember();
		matchMember.regex = new Regex(matchAttribute.Pattern, (RegexOptions)(0x10 | (matchAttribute.IgnoreCase ? 513 : 0)));
		matchMember.group = matchAttribute.Group;
		matchMember.capture = matchAttribute.Capture;
		matchMember.maxRepeats = matchAttribute.MaxRepeats;
		matchMember.memberInfo = memberInfo;
		if (matchMember.maxRepeats < 0)
		{
			matchMember.maxRepeats = ((!type.IsArray) ? 1 : int.MaxValue);
		}
		if (type.IsArray)
		{
			type = type.GetElementType();
		}
		if (type != typeof(string))
		{
			matchMember.matchType = MatchType.Reflect(type);
		}
		return matchMember;
	}
}
