using System.Collections;
using System.IO;
using System.Web.Util;

namespace System.Web;

internal sealed class CapabilitiesLoader : MarshalByRefObject
{
	private const int userAgentsCacheSize = 3000;

	private static Hashtable defaultCaps;

	private static readonly object lockobj;

	private static volatile bool loaded;

	private static ICollection alldata;

	private static Hashtable userAgentsCache;

	private static char[] eq;

	private CapabilitiesLoader()
	{
	}

	static CapabilitiesLoader()
	{
		lockobj = new object();
		userAgentsCache = Hashtable.Synchronized(new Hashtable(3010));
		eq = new char[1] { '=' };
		defaultCaps = new Hashtable(StringComparer.OrdinalIgnoreCase);
		defaultCaps.Add("activexcontrols", "False");
		defaultCaps.Add("alpha", "False");
		defaultCaps.Add("aol", "False");
		defaultCaps.Add("aolversion", "0");
		defaultCaps.Add("authenticodeupdate", "");
		defaultCaps.Add("backgroundsounds", "False");
		defaultCaps.Add("beta", "False");
		defaultCaps.Add("browser", "*");
		defaultCaps.Add("browsers", new ArrayList());
		defaultCaps.Add("cdf", "False");
		defaultCaps.Add("clrversion", "0");
		defaultCaps.Add("cookies", "False");
		defaultCaps.Add("crawler", "False");
		defaultCaps.Add("css", "0");
		defaultCaps.Add("cssversion", "0");
		defaultCaps.Add("ecmascriptversion", "0.0");
		defaultCaps.Add("frames", "False");
		defaultCaps.Add("iframes", "False");
		defaultCaps.Add("isbanned", "False");
		defaultCaps.Add("ismobiledevice", "False");
		defaultCaps.Add("issyndicationreader", "False");
		defaultCaps.Add("javaapplets", "False");
		defaultCaps.Add("javascript", "False");
		defaultCaps.Add("majorver", "0");
		defaultCaps.Add("minorver", "0");
		defaultCaps.Add("msdomversion", "0.0");
		defaultCaps.Add("netclr", "False");
		defaultCaps.Add("platform", "unknown");
		defaultCaps.Add("stripper", "False");
		defaultCaps.Add("supportscss", "False");
		defaultCaps.Add("tables", "False");
		defaultCaps.Add("vbscript", "False");
		defaultCaps.Add("version", "0");
		defaultCaps.Add("w3cdomversion", "0.0");
		defaultCaps.Add("wap", "False");
		defaultCaps.Add("win16", "False");
		defaultCaps.Add("win32", "False");
		defaultCaps.Add("win64", "False");
		defaultCaps.Add("adapters", new Hashtable());
		defaultCaps.Add("cancombineformsindeck", "False");
		defaultCaps.Add("caninitiatevoicecall", "False");
		defaultCaps.Add("canrenderafterinputorselectelement", "False");
		defaultCaps.Add("canrenderemptyselects", "False");
		defaultCaps.Add("canrenderinputandselectelementstogether", "False");
		defaultCaps.Add("canrendermixedselects", "False");
		defaultCaps.Add("canrenderoneventandprevelementstogether", "False");
		defaultCaps.Add("canrenderpostbackcards", "False");
		defaultCaps.Add("canrendersetvarzerowithmultiselectionlist", "False");
		defaultCaps.Add("cansendmail", "False");
		defaultCaps.Add("defaultsubmitbuttonlimit", "0");
		defaultCaps.Add("gatewayminorversion", "0");
		defaultCaps.Add("gatewaymajorversion", "0");
		defaultCaps.Add("gatewayversion", "None");
		defaultCaps.Add("hasbackbutton", "True");
		defaultCaps.Add("hidesrightalignedmultiselectscrollbars", "False");
		defaultCaps.Add("inputtype", "telephoneKeypad");
		defaultCaps.Add("iscolor", "False");
		defaultCaps.Add("jscriptversion", "0.0");
		defaultCaps.Add("maximumhreflength", "0");
		defaultCaps.Add("maximumrenderedpagesize", "2000");
		defaultCaps.Add("maximumsoftkeylabellength", "5");
		defaultCaps.Add("minorversionstring", "0.0");
		defaultCaps.Add("mobiledevicemanufacturer", "Unknown");
		defaultCaps.Add("mobiledevicemodel", "Unknown");
		defaultCaps.Add("numberofsoftkeys", "0");
		defaultCaps.Add("preferredimagemime", "image/gif");
		defaultCaps.Add("preferredrenderingmime", "text/html");
		defaultCaps.Add("preferredrenderingtype", "html32");
		defaultCaps.Add("preferredrequestencoding", "");
		defaultCaps.Add("preferredresponseencoding", "");
		defaultCaps.Add("rendersbreakbeforewmlselectandinput", "False");
		defaultCaps.Add("rendersbreaksafterhtmllists", "True");
		defaultCaps.Add("rendersbreaksafterwmlanchor", "False");
		defaultCaps.Add("rendersbreaksafterwmlinput", "False");
		defaultCaps.Add("renderswmldoacceptsinline", "True");
		defaultCaps.Add("renderswmlselectsasmenucards", "False");
		defaultCaps.Add("requiredmetatagnamevalue", "");
		defaultCaps.Add("requiresattributecolonsubstitution", "False");
		defaultCaps.Add("requirescontenttypemetatag", "False");
		defaultCaps.Add("requirescontrolstateinsession", "False");
		defaultCaps.Add("requiresdbcscharacter", "False");
		defaultCaps.Add("requireshtmladaptiveerrorreporting", "False");
		defaultCaps.Add("requiresleadingpagebreak", "False");
		defaultCaps.Add("requiresnobreakinformatting", "False");
		defaultCaps.Add("requiresoutputoptimization", "False");
		defaultCaps.Add("requiresphonenumbersasplaintext", "False");
		defaultCaps.Add("requiresspecialviewstateencoding", "False");
		defaultCaps.Add("requiresuniquefilepathsuffix", "False");
		defaultCaps.Add("requiresuniquehtmlcheckboxnames", "False");
		defaultCaps.Add("requiresuniquehtmlinputnames", "False");
		defaultCaps.Add("requiresurlencodedpostfieldvalues", "False");
		defaultCaps.Add("screenbitdepth", "1");
		defaultCaps.Add("screencharactersheight", "6");
		defaultCaps.Add("screencharacterswidth", "12");
		defaultCaps.Add("screenpixelsheight", "72");
		defaultCaps.Add("screenpixelswidth", "96");
		defaultCaps.Add("supportsaccesskeyattribute", "False");
		defaultCaps.Add("supportsbodycolor", "True");
		defaultCaps.Add("supportsbold", "False");
		defaultCaps.Add("supportscachecontrolmetatag", "True");
		defaultCaps.Add("supportscallback", "False");
		defaultCaps.Add("supportsdivalign", "True");
		defaultCaps.Add("supportsdivnowrap", "False");
		defaultCaps.Add("supportsemptystringincookievalue", "False");
		defaultCaps.Add("supportsfontcolor", "True");
		defaultCaps.Add("supportsfontname", "False");
		defaultCaps.Add("supportsfontsize", "False");
		defaultCaps.Add("supportsimagesubmit", "False");
		defaultCaps.Add("supportsimodesymbols", "False");
		defaultCaps.Add("supportsinputistyle", "False");
		defaultCaps.Add("supportsinputmode", "False");
		defaultCaps.Add("supportsitalic", "False");
		defaultCaps.Add("supportsjphonemultimediaattributes", "False");
		defaultCaps.Add("supportsjphonesymbols", "False");
		defaultCaps.Add("supportsquerystringinformaction", "True");
		defaultCaps.Add("supportsredirectwithcookie", "True");
		defaultCaps.Add("supportsselectmultiple", "True");
		defaultCaps.Add("supportsuncheck", "True");
		defaultCaps.Add("supportsxmlhttp", "False");
		defaultCaps.Add("type", "Unknown");
	}

	public static Hashtable GetCapabilities(string userAgent)
	{
		Init();
		if (userAgent != null)
		{
			userAgent = userAgent.Trim();
		}
		if (alldata == null || userAgent == null || userAgent.Length == 0)
		{
			return defaultCaps;
		}
		Hashtable hashtable = (Hashtable)(userAgentsCache.Contains(userAgent) ? userAgentsCache[userAgent] : null);
		if (hashtable == null)
		{
			foreach (BrowserData alldatum in alldata)
			{
				if (alldatum.IsMatch(userAgent))
				{
					Hashtable tbl = new Hashtable(defaultCaps, StringComparer.OrdinalIgnoreCase);
					hashtable = alldatum.GetProperties(tbl);
					break;
				}
			}
			if (hashtable == null)
			{
				hashtable = defaultCaps;
			}
			lock (lockobj)
			{
				if (userAgentsCache.Count >= 3000)
				{
					userAgentsCache.Clear();
				}
			}
			userAgentsCache[userAgent] = hashtable;
		}
		return hashtable;
	}

	private static void Init()
	{
		if (loaded)
		{
			return;
		}
		lock (lockobj)
		{
			if (!loaded)
			{
				string machineConfigurationDirectory = HttpRuntime.MachineConfigurationDirectory;
				string text = Path.Combine(machineConfigurationDirectory, "browscap.ini");
				if (!File.Exists(text))
				{
					machineConfigurationDirectory = Path.GetDirectoryName(machineConfigurationDirectory);
					text = Path.Combine(machineConfigurationDirectory, "browscap.ini");
				}
				try
				{
					LoadFile(text);
				}
				catch (Exception)
				{
				}
				loaded = true;
			}
		}
	}

	private static void LoadFile(string filename)
	{
		if (!File.Exists(filename))
		{
			return;
		}
		TextReader textReader = new StreamReader(File.OpenRead(filename));
		using (textReader)
		{
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			int num = 0;
			ArrayList arrayList = new ArrayList();
			string text;
			while ((text = textReader.ReadLine()) != null)
			{
				if (text.Length == 0 || text[0] == ';')
				{
					continue;
				}
				string text2 = text.Substring(1, text.Length - 2);
				BrowserData browserData = new BrowserData(text2);
				ReadCapabilities(textReader, browserData);
				if (!(text2 == "*") && !(text2 == "GJK_Browscap_Version"))
				{
					string browser = browserData.GetBrowser();
					if (browser == null || hashtable.ContainsKey(browser))
					{
						hashtable.Add(num++, browserData);
						arrayList.Add(browserData);
					}
					else
					{
						hashtable.Add(browser, browserData);
						arrayList.Add(browserData);
					}
				}
			}
			alldata = arrayList;
			foreach (BrowserData alldatum in alldata)
			{
				string parentName = alldatum.GetParentName();
				if (parentName != null)
				{
					alldatum.Parent = (BrowserData)hashtable[parentName];
				}
			}
		}
	}

	private static void ReadCapabilities(TextReader input, BrowserData data)
	{
		string text;
		while ((text = input.ReadLine()) != null && text.Length != 0)
		{
			string[] array = text.Split(eq, 2);
			string text2 = array[0].ToLower(Helpers.InvariantCulture).Trim();
			if (text2.Length != 0)
			{
				data.Add(text2, array[1]);
			}
		}
	}
}
