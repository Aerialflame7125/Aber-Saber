using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace System.Web.Configuration.nBrowser;

internal class File
{
	private XmlDocument BrowserFile;

	internal Node[] Nodes;

	private ListDictionary Lookup;

	private ListDictionary DefaultLookup;

	internal List<Node> RefNodes;

	private string pFileName = string.Empty;

	public string FileName => pFileName;

	public string[] Keys
	{
		get
		{
			string[] array = new string[Lookup.Keys.Count];
			int num = 0;
			for (int i = 0; i <= Nodes.Length - 1; i++)
			{
				if (Nodes[i] != null && Nodes[i].NameType != NodeType.DefaultBrowser && Nodes[i].RefId.Length == 0)
				{
					array[num] = Nodes[i].Id;
					num++;
				}
			}
			return array;
		}
	}

	public string[] DefaultKeys
	{
		get
		{
			string[] array = new string[DefaultLookup.Keys.Count];
			int num = 0;
			for (int i = 0; i <= Nodes.Length - 1; i++)
			{
				if (Nodes[i] != null && Nodes[i].NameType == NodeType.DefaultBrowser)
				{
					array[num] = Nodes[i].Id;
					num++;
				}
			}
			return array;
		}
	}

	public File(string file)
	{
		pFileName = file;
		BrowserFile = new XmlDocument();
		BrowserFile.Load(file);
		Load(BrowserFile);
	}

	public File(XmlDocument BrowserFile, string filename)
	{
		pFileName = filename;
		Load(BrowserFile);
	}

	private void Load(XmlDocument BrowserFile)
	{
		Lookup = new ListDictionary();
		DefaultLookup = new ListDictionary();
		RefNodes = new List<Node>();
		Nodes = new Node[BrowserFile.DocumentElement.ChildNodes.Count];
		for (int i = 0; i <= BrowserFile.DocumentElement.ChildNodes.Count - 1; i++)
		{
			XmlNode xmlNode = BrowserFile.DocumentElement.ChildNodes[i];
			if (xmlNode.NodeType == XmlNodeType.Comment)
			{
				continue;
			}
			Nodes[i] = new Node(xmlNode);
			Nodes[i].FileName = FileName;
			if (Nodes[i].NameType != NodeType.DefaultBrowser)
			{
				if (Nodes[i].RefId.Length > 0)
				{
					RefNodes.Add(Nodes[i]);
					continue;
				}
				if (Lookup.Contains(Nodes[i].Id))
				{
					throw new Exception("Duplicate ID found \"" + Nodes[i].Id + "\"");
				}
				Lookup.Add(Nodes[i].Id, i);
			}
			else if (Nodes[i].RefId.Length > 0)
			{
				RefNodes.Add(Nodes[i]);
			}
			else
			{
				if (DefaultLookup.Contains(Nodes[i].Id))
				{
					throw new Exception("Duplicate ID found \"" + Nodes[i].Id + "\"");
				}
				DefaultLookup.Add(Nodes[i].Id, i);
			}
		}
	}

	internal Node GetNode(string Key)
	{
		object obj = Lookup[Key];
		if (obj == null)
		{
			return GetDefaultNode(Key);
		}
		return Nodes[(int)obj];
	}

	internal Node GetDefaultNode(string Key)
	{
		object obj = DefaultLookup[Key];
		if (obj == null)
		{
			return null;
		}
		return Nodes[(int)obj];
	}
}
