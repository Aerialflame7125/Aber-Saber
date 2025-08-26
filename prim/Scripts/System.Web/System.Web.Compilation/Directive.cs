using System.Collections;

namespace System.Web.Compilation;

internal sealed class Directive
{
	private static Hashtable directivesHash;

	private static string[] page_atts;

	private static string[] control_atts;

	private static string[] import_atts;

	private static string[] implements_atts;

	private static string[] assembly_atts;

	private static string[] register_atts;

	private static string[] outputcache_atts;

	private static string[] reference_atts;

	private static string[] webservice_atts;

	private static string[] application_atts;

	private static string[] mastertype_atts;

	private static string[] previouspagetype_atts;

	static Directive()
	{
		page_atts = new string[32]
		{
			"AspCompat", "AutoEventWireup", "Buffer", "ClassName", "ClientTarget", "CodePage", "CompilerOptions", "ContentType", "Culture", "Debug",
			"Description", "EnableEventValidation", "MaintainScrollPositionOnPostBack", "EnableSessionState", "EnableViewState", "EnableViewStateMac", "ErrorPage", "Explicit", "Inherits", "Language",
			"LCID", "ResponseEncoding", "Src", "SmartNavigation", "Strict", "Trace", "TraceMode", "Transaction", "UICulture", "WarningLevel",
			"CodeBehind", "ValidateRequest"
		};
		control_atts = new string[15]
		{
			"AutoEventWireup", "ClassName", "CompilerOptions", "Debug", "Description", "EnableViewState", "Explicit", "Inherits", "Language", "Strict",
			"Src", "WarningLevel", "CodeBehind", "TargetSchema", "LinePragmas"
		};
		import_atts = new string[1] { "namespace" };
		implements_atts = new string[1] { "interface" };
		assembly_atts = new string[2] { "name", "src" };
		register_atts = new string[5] { "tagprefix", "tagname", "Namespace", "Src", "Assembly" };
		outputcache_atts = new string[6] { "Duration", "Location", "VaryByControl", "VaryByCustom", "VaryByHeader", "VaryByParam" };
		reference_atts = new string[2] { "page", "control" };
		webservice_atts = new string[4] { "class", "codebehind", "debug", "language" };
		application_atts = new string[3] { "description", "inherits", "codebehind" };
		mastertype_atts = new string[2] { "virtualpath", "typename" };
		previouspagetype_atts = new string[2] { "virtualpath", "typename" };
		InitHash();
	}

	private static void InitHash()
	{
		StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
		directivesHash = new Hashtable(ordinalIgnoreCase);
		Hashtable hashtable = new Hashtable(ordinalIgnoreCase);
		string[] array = page_atts;
		foreach (string key in array)
		{
			hashtable.Add(key, null);
		}
		directivesHash.Add("PAGE", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = control_atts;
		foreach (string key2 in array)
		{
			hashtable.Add(key2, null);
		}
		directivesHash.Add("CONTROL", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = import_atts;
		foreach (string key3 in array)
		{
			hashtable.Add(key3, null);
		}
		directivesHash.Add("IMPORT", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = implements_atts;
		foreach (string key4 in array)
		{
			hashtable.Add(key4, null);
		}
		directivesHash.Add("IMPLEMENTS", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = register_atts;
		foreach (string key5 in array)
		{
			hashtable.Add(key5, null);
		}
		directivesHash.Add("REGISTER", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = assembly_atts;
		foreach (string key6 in array)
		{
			hashtable.Add(key6, null);
		}
		directivesHash.Add("ASSEMBLY", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = outputcache_atts;
		foreach (string key7 in array)
		{
			hashtable.Add(key7, null);
		}
		directivesHash.Add("OUTPUTCACHE", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = reference_atts;
		foreach (string key8 in array)
		{
			hashtable.Add(key8, null);
		}
		directivesHash.Add("REFERENCE", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = webservice_atts;
		foreach (string key9 in array)
		{
			hashtable.Add(key9, null);
		}
		directivesHash.Add("WEBSERVICE", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = webservice_atts;
		foreach (string key10 in array)
		{
			hashtable.Add(key10, null);
		}
		directivesHash.Add("WEBHANDLER", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = application_atts;
		foreach (string key11 in array)
		{
			hashtable.Add(key11, null);
		}
		directivesHash.Add("APPLICATION", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = mastertype_atts;
		foreach (string key12 in array)
		{
			hashtable.Add(key12, null);
		}
		directivesHash.Add("MASTERTYPE", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = control_atts;
		foreach (string key13 in array)
		{
			hashtable.Add(key13, null);
		}
		directivesHash.Add("MASTER", hashtable);
		hashtable = new Hashtable(ordinalIgnoreCase);
		array = previouspagetype_atts;
		foreach (string key14 in array)
		{
			hashtable.Add(key14, null);
		}
		directivesHash.Add("PREVIOUSPAGETYPE", hashtable);
	}

	private Directive()
	{
	}

	public static bool IsDirective(string id)
	{
		return directivesHash.Contains(id);
	}
}
