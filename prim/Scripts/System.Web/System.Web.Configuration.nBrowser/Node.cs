using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Web.Configuration.nBrowser;

internal class Node
{
	private NodeType pName;

	private string pId = string.Empty;

	private string pParentID = string.Empty;

	private string pRefID = string.Empty;

	private string pMarkupTextWriterType = string.Empty;

	private string pFileName = string.Empty;

	private XmlNode xmlNode;

	private Identification[] Identification;

	private Identification[] Capture;

	private NameValueCollection Capabilities;

	private NameValueCollection Adapter;

	private Type[] AdapterControlTypes;

	private Type[] AdapterTypes;

	private List<string> ChildrenKeys;

	private List<string> DefaultChildrenKeys;

	private SortedList<string, Node> Children;

	private SortedList<string, Node> DefaultChildren;

	private NameValueCollection sampleHeaders;

	private bool HaveAdapterTypes;

	private object LookupAdapterTypesLock = new object();

	public NodeType NameType
	{
		get
		{
			return pName;
		}
		set
		{
			pName = value;
		}
	}

	public string Id
	{
		get
		{
			return pId;
		}
		set
		{
			pId = value;
		}
	}

	public string ParentId
	{
		get
		{
			return pParentID;
		}
		set
		{
			pParentID = value;
		}
	}

	public string RefId
	{
		get
		{
			return pRefID;
		}
		set
		{
			pRefID = value;
		}
	}

	public string MarkupTextWriterType
	{
		get
		{
			return pMarkupTextWriterType;
		}
		set
		{
			pMarkupTextWriterType = value;
		}
	}

	public string FileName
	{
		get
		{
			return pFileName;
		}
		set
		{
			pFileName = value;
		}
	}

	public bool HasChildren
	{
		get
		{
			if (Children.Count > -1)
			{
				return true;
			}
			return false;
		}
	}

	public NameValueCollection SampleHeader => sampleHeaders;

	public Node(XmlNode xmlNode)
	{
		this.xmlNode = xmlNode;
		ResetChildern();
		Reset();
	}

	internal Node()
	{
		ResetChildern();
		Identification = new Identification[1];
		Identification[0] = new Identification(matchType: true, "header", "User-Agent", ".");
		Id = "[Base Node]";
		NameType = NodeType.Browser;
	}

	private void ProcessIdentification(XmlNode node)
	{
		Identification = new Identification[node.ChildNodes.Count];
		int num = -1;
		for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
		{
			XmlNodeType nodeType = node.ChildNodes[i].NodeType;
			if (nodeType == XmlNodeType.Text || nodeType == XmlNodeType.Comment)
			{
				continue;
			}
			string empty = string.Empty;
			string empty2 = string.Empty;
			if (string.Compare(node.ChildNodes[i].Name, "userAgent", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				empty = "header";
				empty2 = "User-Agent";
			}
			else if (string.Compare(node.ChildNodes[i].Name, "header", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				empty = node.ChildNodes[i].Name;
				empty2 = node.ChildNodes[i].Attributes["name"].Value;
			}
			else
			{
				if (string.Compare(node.ChildNodes[i].Name, "capability", ignoreCase: true, CultureInfo.CurrentCulture) != 0)
				{
					throw new Exception("Invalid Node found in Identification");
				}
				empty = node.ChildNodes[i].Name;
				empty2 = node.ChildNodes[i].Attributes["name"].Value;
			}
			if (node.ChildNodes[i].Attributes["match"] != null)
			{
				num++;
				Identification[num] = new Identification(matchType: true, empty, empty2, node.ChildNodes[i].Attributes["match"].Value);
			}
			else if (node.ChildNodes[i].Attributes["nonMatch"] != null)
			{
				num++;
				Identification[num] = new Identification(matchType: false, empty, empty2, node.ChildNodes[i].Attributes["nonMatch"].Value);
			}
		}
	}

	private void ProcessCapture(XmlNode node)
	{
		Capture = new Identification[node.ChildNodes.Count];
		int num = -1;
		for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
		{
			XmlNodeType nodeType = node.ChildNodes[i].NodeType;
			if (nodeType != XmlNodeType.Text && nodeType != XmlNodeType.Comment)
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				if (node.ChildNodes[i].Name == "userAgent")
				{
					empty2 = "header";
					empty3 = "User-Agent";
				}
				else
				{
					empty2 = node.ChildNodes[i].Name;
					empty3 = node.ChildNodes[i].Attributes["name"].Value;
				}
				empty = node.ChildNodes[i].Attributes["match"].Value;
				num++;
				Capture[num] = new Identification(matchType: true, empty2, empty3, empty);
			}
		}
	}

	private void ProcessCapabilities(XmlNode node)
	{
		Capabilities = new NameValueCollection(node.ChildNodes.Count, StringComparer.OrdinalIgnoreCase);
		for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
		{
			if (node.ChildNodes[i].NodeType == XmlNodeType.Comment)
			{
				continue;
			}
			string text = string.Empty;
			string value = string.Empty;
			for (int j = 0; j <= node.ChildNodes[i].Attributes.Count - 1; j++)
			{
				string name = node.ChildNodes[i].Attributes[j].Name;
				if (!(name == "name"))
				{
					if (name == "value")
					{
						value = node.ChildNodes[i].Attributes[j].Value;
					}
				}
				else
				{
					text = node.ChildNodes[i].Attributes[j].Value;
				}
			}
			if (text.Length > 0)
			{
				Capabilities[text] = value;
			}
		}
	}

	private void ProcessControlAdapters(XmlNode node)
	{
		Adapter = new NameValueCollection();
		for (int i = 0; i <= node.Attributes.Count - 1; i++)
		{
			string name = node.Attributes[i].Name;
			if (name == "markupTextWriterType")
			{
				MarkupTextWriterType = node.Attributes[i].Value;
			}
		}
		for (int j = 0; j <= node.ChildNodes.Count - 1; j++)
		{
			if (node.ChildNodes[j].NodeType == XmlNodeType.Comment || node.ChildNodes[j].NodeType == XmlNodeType.Text)
			{
				continue;
			}
			XmlNode xmlNode = node.ChildNodes[j];
			string text = string.Empty;
			string text2 = string.Empty;
			for (int k = 0; k <= xmlNode.Attributes.Count - 1; k++)
			{
				if (string.Compare(xmlNode.Attributes[k].Name, "controlType", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
				{
					text = xmlNode.Attributes[k].Value;
				}
				else if (string.Compare(xmlNode.Attributes[k].Name, "adapterType", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
				{
					text2 = xmlNode.Attributes[k].Value;
				}
			}
			if (text.Length > 0 && text2.Length > 0)
			{
				Adapter[text] = text2;
			}
		}
		AdapterControlTypes = null;
		AdapterTypes = null;
	}

	private void ProcessSampleHeaders(XmlNode node)
	{
		sampleHeaders = new NameValueCollection(node.ChildNodes.Count);
		for (int i = 0; i <= node.ChildNodes.Count - 1; i++)
		{
			if (node.ChildNodes[i].NodeType == XmlNodeType.Comment)
			{
				continue;
			}
			string text = string.Empty;
			string value = string.Empty;
			for (int j = 0; j <= node.ChildNodes[i].Attributes.Count - 1; j++)
			{
				string name = node.ChildNodes[i].Attributes[j].Name;
				if (!(name == "name"))
				{
					if (name == "value")
					{
						value = node.ChildNodes[i].Attributes[j].Value;
					}
				}
				else
				{
					text = node.ChildNodes[i].Attributes[j].Value;
				}
			}
			if (text.Length > 0)
			{
				sampleHeaders[text] = value;
			}
		}
	}

	internal void ResetChildern()
	{
		Children = new SortedList<string, Node>();
		DefaultChildren = new SortedList<string, Node>();
		ChildrenKeys = new List<string>();
		DefaultChildrenKeys = new List<string>();
	}

	public void Reset()
	{
		Capture = null;
		Capabilities = null;
		Adapter = null;
		AdapterControlTypes = null;
		AdapterTypes = null;
		if (string.Compare(xmlNode.Name, "browser", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
		{
			NameType = NodeType.Browser;
		}
		else if (string.Compare(xmlNode.Name, "defaultBrowser", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
		{
			NameType = NodeType.DefaultBrowser;
		}
		else if (string.Compare(xmlNode.Name, "gateway", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
		{
			NameType = NodeType.Gateway;
		}
		for (int i = 0; i <= xmlNode.Attributes.Count - 1; i++)
		{
			if (string.Compare(xmlNode.Attributes[i].Name, "id", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				Id = xmlNode.Attributes[i].Value.ToLower(CultureInfo.CurrentCulture);
			}
			else if (string.Compare(xmlNode.Attributes[i].Name, "parentID", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				ParentId = xmlNode.Attributes[i].Value.ToLower(CultureInfo.CurrentCulture);
			}
			else if (string.Compare(xmlNode.Attributes[i].Name, "refID", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				RefId = xmlNode.Attributes[i].Value.ToLower(CultureInfo.CurrentCulture);
			}
		}
		for (int j = 0; j <= xmlNode.ChildNodes.Count - 1; j++)
		{
			if (string.Compare(xmlNode.ChildNodes[j].Name, "identification", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				ProcessIdentification(xmlNode.ChildNodes[j]);
			}
			else if (string.Compare(xmlNode.ChildNodes[j].Name, "capture", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				ProcessCapture(xmlNode.ChildNodes[j]);
			}
			else if (string.Compare(xmlNode.ChildNodes[j].Name, "capabilities", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				ProcessCapabilities(xmlNode.ChildNodes[j]);
			}
			else if (string.Compare(xmlNode.ChildNodes[j].Name, "controlAdapters", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				ProcessControlAdapters(xmlNode.ChildNodes[j]);
			}
			else if (string.Compare(xmlNode.ChildNodes[j].Name, "sampleHeaders", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
			{
				ProcessSampleHeaders(xmlNode.ChildNodes[j]);
			}
			if (Id == "default" && (Identification == null || Identification.Length == 0))
			{
				Identification = new Identification[1];
				Identification[0] = new Identification(matchType: true, "header", "User-Agent", ".");
			}
		}
	}

	public void AddChild(Node child)
	{
		if (child != null)
		{
			if (child.NameType == NodeType.Browser || child.NameType == NodeType.Gateway)
			{
				Children.Add(child.Id, child);
				ChildrenKeys.Add(child.Id);
			}
			else if (child.NameType == NodeType.DefaultBrowser)
			{
				DefaultChildren.Add(child.Id, child);
				DefaultChildrenKeys.Add(child.Id);
			}
		}
	}

	public void RemoveChild(Node child)
	{
		if (child != null)
		{
			if (child.NameType == NodeType.Browser || child.NameType == NodeType.Gateway)
			{
				Children.Remove(child.Id);
				ChildrenKeys.Remove(child.Id);
			}
			else if (child.NameType == NodeType.DefaultBrowser)
			{
				DefaultChildren.Remove(child.Id);
				DefaultChildrenKeys.Remove(child.Id);
			}
		}
	}

	private Type FindType(string typeName)
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			string typeName2 = typeName + "," + assembly.FullName;
			Type type = Type.GetType(typeName2);
			if (type != null)
			{
				return type;
			}
			type = Type.GetType(typeName2, throwOnError: false, ignoreCase: true);
			if (type != null)
			{
				return type;
			}
		}
		throw new TypeLoadException(typeName);
	}

	internal bool Process(NameValueCollection header, Result result, List<Match> matchList)
	{
		int count = matchList.Count;
		bool result2 = ProcessSubtree(header, result, matchList);
		if (matchList.Count > count)
		{
			matchList.RemoveRange(count, matchList.Count - count);
		}
		return result2;
	}

	private bool ProcessSubtree(NameValueCollection header, Result result, List<Match> matchList)
	{
		result.AddCapabilities("", header["User-Agent"]);
		if (RefId.Length == 0 && NameType != NodeType.DefaultBrowser && !BrowserIdentification(header, result, matchList))
		{
			return false;
		}
		result.AddMatchingBrowserId(Id);
		result.AddTrack(string.Concat("[", NameType, "]\t", Id));
		if (Adapter != null)
		{
			LookupAdapterTypes();
			for (int i = 0; i <= Adapter.Count - 1; i++)
			{
				result.AddAdapter(AdapterControlTypes[i], AdapterTypes[i]);
			}
		}
		if (MarkupTextWriterType != null && MarkupTextWriterType.Length > 0)
		{
			result.MarkupTextWriter = Type.GetType(MarkupTextWriterType);
			if (result.MarkupTextWriter == null)
			{
				result.MarkupTextWriter = Type.GetType(MarkupTextWriterType, throwOnError: true, ignoreCase: true);
			}
		}
		if (Capture != null)
		{
			for (int j = 0; j <= Capture.Length - 1; j++)
			{
				if (Capture[j] != null)
				{
					Match match = null;
					if (Capture[j].Group == "header")
					{
						match = Capture[j].GetMatch(header[Capture[j].Name]);
					}
					else if (Capture[j].Group == "capability")
					{
						match = Capture[j].GetMatch(result[Capture[j].Name]);
					}
					if (Capture[j].IsMatchSuccessful(match) && match.Groups.Count > 0)
					{
						matchList.Add(match);
					}
				}
			}
		}
		if (Capabilities != null)
		{
			for (int k = 0; k <= Capabilities.Count - 1; k++)
			{
				string text = Capabilities[k];
				int num = matchList.Count - 1;
				while (num >= 0 && text != null && text.Length > 0 && text.IndexOf('$') > -1)
				{
					if (matchList[num].Groups.Count != 0 && matchList[num].Success)
					{
						text = matchList[num].Result(text);
					}
					num--;
				}
				if (text.IndexOf('$') > -1 || text.IndexOf('%') > -1)
				{
					text = result.Replace(text);
				}
				result.AddCapabilities(Capabilities.Keys[k], text);
			}
		}
		for (int l = 0; l <= DefaultChildren.Count - 1; l++)
		{
			string key = DefaultChildrenKeys[l];
			Node node = DefaultChildren[key];
			if (node.NameType == NodeType.DefaultBrowser)
			{
				node.Process(header, result, matchList);
			}
		}
		for (int m = 0; m <= Children.Count - 1; m++)
		{
			string key2 = ChildrenKeys[m];
			Node node2 = Children[key2];
			if (node2.NameType == NodeType.Gateway)
			{
				node2.Process(header, result, matchList);
			}
		}
		for (int n = 0; n <= Children.Count - 1; n++)
		{
			string key3 = ChildrenKeys[n];
			Node node3 = Children[key3];
			if (node3.NameType == NodeType.Browser && node3.Process(header, result, matchList))
			{
				break;
			}
		}
		return true;
	}

	private bool BrowserIdentification(NameValueCollection header, CapabilitiesResult result, List<Match> matchList)
	{
		if (Id.Length > 0 && RefId.Length > 0)
		{
			throw new Exception("Id and refID Attributes givin when there should only be one set not both");
		}
		if (Identification == null || Identification.Length == 0)
		{
			throw new Exception($"Missing Identification Section where one is required (Id={Id}, RefID={RefId})");
		}
		if (header == null)
		{
			throw new Exception("Null Value where NameValueCollection expected ");
		}
		if (result == null)
		{
			throw new Exception("Null Value where Result expected ");
		}
		for (int i = 0; i <= Identification.Length - 1; i++)
		{
			if (Identification[i] != null)
			{
				string text = string.Empty;
				if (string.Compare(Identification[i].Group, "header", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
				{
					text = header[Identification[i].Name];
				}
				else if (string.Compare(Identification[i].Group, "capability", ignoreCase: true, CultureInfo.CurrentCulture) == 0)
				{
					text = result[Identification[i].Name];
				}
				if (text == null)
				{
					text = string.Empty;
				}
				Match match = Identification[i].GetMatch(text);
				if (!Identification[i].IsMatchSuccessful(match))
				{
					return false;
				}
				if (match.Groups.Count > 0)
				{
					matchList.Add(match);
				}
			}
		}
		return true;
	}

	private void LookupAdapterTypes()
	{
		if (Adapter == null || HaveAdapterTypes)
		{
			return;
		}
		lock (LookupAdapterTypesLock)
		{
			if (HaveAdapterTypes)
			{
				return;
			}
			if (AdapterControlTypes == null)
			{
				AdapterControlTypes = new Type[Adapter.Count];
			}
			if (AdapterTypes == null)
			{
				AdapterTypes = new Type[Adapter.Count];
			}
			for (int i = 0; i <= Adapter.Count - 1; i++)
			{
				if (AdapterControlTypes[i] == null)
				{
					AdapterControlTypes[i] = FindType(Adapter.GetKey(i));
				}
				if (AdapterTypes[i] == null)
				{
					AdapterTypes[i] = FindType(Adapter[i]);
				}
			}
			HaveAdapterTypes = true;
		}
	}

	public void Tree(XmlTextWriter xmlwriter, int position)
	{
		if (position == 0)
		{
			xmlwriter.WriteStartDocument();
			xmlwriter.WriteStartElement(NameType.ToString());
			xmlwriter.WriteRaw(Environment.NewLine);
		}
		string fileName = FileName;
		xmlwriter.WriteStartElement(NameType.ToString());
		xmlwriter.WriteAttributeString("FileName", fileName);
		xmlwriter.WriteAttributeString("ID", Id);
		xmlwriter.WriteRaw(Environment.NewLine);
		if (position != int.MaxValue)
		{
			position++;
		}
		for (int i = 0; i <= DefaultChildren.Count - 1; i++)
		{
			string key = DefaultChildrenKeys[i];
			Node node = DefaultChildren[key];
			if (node.NameType == NodeType.DefaultBrowser)
			{
				node.Tree(xmlwriter, position);
			}
		}
		for (int j = 0; j <= Children.Count - 1; j++)
		{
			string key2 = ChildrenKeys[j];
			Node node2 = Children[key2];
			if (node2.NameType == NodeType.Gateway)
			{
				node2.Tree(xmlwriter, position);
			}
		}
		for (int k = 0; k <= Children.Count - 1; k++)
		{
			string key3 = ChildrenKeys[k];
			Node node3 = Children[key3];
			if (node3.NameType == NodeType.Browser)
			{
				node3.Tree(xmlwriter, position);
			}
		}
		if (position != int.MinValue)
		{
			position--;
		}
		xmlwriter.WriteEndElement();
		xmlwriter.WriteRaw(Environment.NewLine);
		if (position == 0)
		{
			xmlwriter.WriteEndDocument();
			xmlwriter.Flush();
		}
	}

	public Collection<string> HeaderNames(Collection<string> list)
	{
		if (Identification != null)
		{
			for (int i = 0; i <= Identification.Length - 1; i++)
			{
				if (Identification[i] != null && Identification[i].Group == "header" && !list.Contains(Identification[i].Name))
				{
					list.Add(Identification[i].Name);
				}
			}
		}
		if (Capture != null)
		{
			for (int j = 0; j <= Capture.Length - 1; j++)
			{
				if (Capture[j] != null && Capture[j].Group == "header" && !list.Contains(Capture[j].Name))
				{
					list.Add(Capture[j].Name);
				}
			}
		}
		for (int k = 0; k <= DefaultChildren.Count - 1; k++)
		{
			string key = DefaultChildrenKeys[k];
			Node node = DefaultChildren[key];
			if (node.NameType == NodeType.DefaultBrowser)
			{
				list = node.HeaderNames(list);
			}
		}
		for (int l = 0; l <= Children.Count - 1; l++)
		{
			string key2 = ChildrenKeys[l];
			Node node2 = Children[key2];
			if (node2.NameType == NodeType.Gateway)
			{
				list = node2.HeaderNames(list);
			}
		}
		for (int m = 0; m <= Children.Count - 1; m++)
		{
			string key3 = ChildrenKeys[m];
			Node node3 = Children[key3];
			if (node3.NameType == NodeType.Browser)
			{
				list = node3.HeaderNames(list);
			}
		}
		return list;
	}

	public void MergeFrom(Node n)
	{
		if (n.Capabilities != null)
		{
			if (Capabilities == null)
			{
				Capabilities = new NameValueCollection(n.Capabilities.Count, StringComparer.OrdinalIgnoreCase);
			}
			foreach (string capability in n.Capabilities)
			{
				Capabilities[capability] = n.Capabilities[capability];
			}
		}
		int num = 0;
		if (Capture != null)
		{
			num += Capture.Length;
		}
		if (n.Capture != null)
		{
			num += n.Capture.Length;
		}
		Identification[] array = new Identification[num];
		if (Capture != null)
		{
			Array.Copy(Capture, 0, array, 0, Capture.Length);
		}
		if (n.Capture != null)
		{
			Array.Copy(n.Capture, 0, array, (Capture != null) ? Capture.Length : 0, n.Capture.Length);
		}
		Capture = array;
		if (n.MarkupTextWriterType != null && n.MarkupTextWriterType.Length > 0)
		{
			MarkupTextWriterType = n.MarkupTextWriterType;
		}
		if (n.Adapter == null)
		{
			return;
		}
		if (Adapter == null)
		{
			Adapter = new NameValueCollection();
		}
		foreach (string item in n.Adapter)
		{
			Adapter[item] = n.Adapter[item];
		}
	}
}
