using System.Globalization;
using System.Text;

namespace System.Web.Services.Diagnostics;

internal class TraceMethod
{
	private object target;

	private string name;

	private object[] args;

	private string call;

	internal TraceMethod(object target, string name, params object[] args)
	{
		this.target = target;
		this.name = name;
		this.args = args;
	}

	public override string ToString()
	{
		if (call == null)
		{
			call = CallString(target, name, args);
		}
		return call;
	}

	internal static string CallString(object target, string method, params object[] args)
	{
		StringBuilder stringBuilder = new StringBuilder();
		WriteObjectId(stringBuilder, target);
		stringBuilder.Append(':');
		stringBuilder.Append(':');
		stringBuilder.Append(method);
		stringBuilder.Append('(');
		for (int i = 0; i < args.Length; i++)
		{
			object obj = args[i];
			WriteObjectId(stringBuilder, obj);
			if (obj != null)
			{
				stringBuilder.Append('=');
				WriteValue(stringBuilder, obj);
			}
			if (i + 1 < args.Length)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(' ');
			}
		}
		stringBuilder.Append(')');
		return stringBuilder.ToString();
	}

	internal static string MethodId(object target, string method)
	{
		StringBuilder stringBuilder = new StringBuilder();
		WriteObjectId(stringBuilder, target);
		stringBuilder.Append(':');
		stringBuilder.Append(':');
		stringBuilder.Append(method);
		return stringBuilder.ToString();
	}

	private static void WriteObjectId(StringBuilder sb, object o)
	{
		if (o == null)
		{
			sb.Append("(null)");
		}
		else if (o is Type)
		{
			Type type = (Type)o;
			sb.Append(type.FullName);
			if (!type.IsAbstract || !type.IsSealed)
			{
				sb.Append('#');
				sb.Append(HashString(o));
			}
		}
		else
		{
			sb.Append(o.GetType().FullName);
			sb.Append('#');
			sb.Append(HashString(o));
		}
	}

	private static void WriteValue(StringBuilder sb, object o)
	{
		if (o == null)
		{
			return;
		}
		if (o is string)
		{
			sb.Append('"');
			sb.Append(o);
			sb.Append('"');
			return;
		}
		Type type = o.GetType();
		if (type.IsArray)
		{
			sb.Append('[');
			sb.Append(((Array)o).Length);
			sb.Append(']');
			return;
		}
		string text = o.ToString();
		if (type.FullName == text)
		{
			sb.Append('.');
			sb.Append('.');
		}
		else
		{
			sb.Append(text);
		}
	}

	private static string HashString(object objectValue)
	{
		if (objectValue == null)
		{
			return "(null)";
		}
		return objectValue.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
	}
}
