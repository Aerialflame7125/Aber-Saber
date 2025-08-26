using System.Collections;
using System.Reflection;

namespace System.Web.Services.Protocols;

internal class MatchType
{
	private Type type;

	private MatchMember[] fields;

	internal Type Type => type;

	internal static MatchType Reflect(Type type)
	{
		MatchType matchType = new MatchType();
		matchType.type = type;
		MemberInfo[] members = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < members.Length; i++)
		{
			MatchMember matchMember = MatchMember.Reflect(members[i]);
			if (matchMember != null)
			{
				arrayList.Add(matchMember);
			}
		}
		matchType.fields = (MatchMember[])arrayList.ToArray(typeof(MatchMember));
		return matchType;
	}

	internal object Match(string text)
	{
		object obj = Activator.CreateInstance(type);
		for (int i = 0; i < fields.Length; i++)
		{
			fields[i].Match(obj, text);
		}
		return obj;
	}
}
