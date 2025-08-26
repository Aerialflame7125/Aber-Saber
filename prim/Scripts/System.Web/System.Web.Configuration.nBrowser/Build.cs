using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Web.Configuration.nBrowser;

internal class Build : CapabilitiesBuild
{
	private Dictionary<string, File> Browserfiles;

	private List<File> nbrowserfiles;

	private Dictionary<string, string> DefaultKeys;

	private Dictionary<string, string> BrowserKeys;

	private object browserSyncRoot = new object();

	private Node browser;

	public Build()
	{
		Browserfiles = new Dictionary<string, File>();
		nbrowserfiles = new List<File>();
		DefaultKeys = new Dictionary<string, string>();
		BrowserKeys = new Dictionary<string, string>();
	}

	public void AddBrowserDirectory(string path)
	{
		if (Directory.Exists(path))
		{
			FileInfo[] files = new DirectoryInfo(path).GetFiles("*.browser");
			for (int i = 0; i <= files.Length - 1; i++)
			{
				AddBrowserFile(files[i].FullName);
			}
		}
		else if (System.IO.File.Exists(path))
		{
			AddBrowserFile(path);
		}
	}

	public void AddBrowserFile(string fileName)
	{
		if (!Browserfiles.ContainsKey(fileName))
		{
			File file = new File(fileName);
			AddBrowserFile(file);
		}
	}

	private void AddBrowserFile(File file)
	{
		if (Browserfiles.ContainsKey(file.FileName))
		{
			return;
		}
		Browserfiles.Add(file.FileName, file);
		nbrowserfiles.Add(file);
		string[] keys = file.Keys;
		for (int i = 0; i <= keys.Length - 1; i++)
		{
			if (!BrowserKeys.ContainsKey(keys[i]))
			{
				BrowserKeys.Add(keys[i], file.FileName);
				continue;
			}
			throw new Exception("Duplicate Key \"" + keys[i] + "\" found in " + file.FileName + " and in file " + BrowserKeys[keys[i]]);
		}
		keys = file.DefaultKeys;
		for (int j = 0; j <= keys.Length - 1; j++)
		{
			if (!DefaultKeys.ContainsKey(keys[j]))
			{
				DefaultKeys.Add(keys[j], file.FileName);
				continue;
			}
			throw new Exception("Duplicate Key \"" + keys[j] + "\" found in " + file.FileName + " and in file " + DefaultKeys[keys[j]]);
		}
	}

	public void AddBrowserFile(XmlDocument browser, string fileName)
	{
		if (!Browserfiles.ContainsKey(fileName))
		{
			File file = new File(browser, fileName);
			AddBrowserFile(file);
		}
	}

	public Node Browser()
	{
		if (browser == null)
		{
			lock (browserSyncRoot)
			{
				if (browser == null)
				{
					browser = InitializeTree();
				}
			}
		}
		return browser;
	}

	private Node InitializeTree()
	{
		Node node = new Node();
		SortedList<string, List<File>> sortedList = new SortedList<string, List<File>>();
		for (int i = 0; i <= Browserfiles.Count - 1; i++)
		{
			if (!sortedList.ContainsKey(nbrowserfiles[i].FileName))
			{
				List<File> value = new List<File>();
				sortedList.Add(nbrowserfiles[i].FileName, value);
			}
			sortedList[nbrowserfiles[i].FileName].Add(nbrowserfiles[i]);
		}
		File[] array = new File[Browserfiles.Count];
		int num = 0;
		for (int j = 0; j <= sortedList.Count - 1; j++)
		{
			List<File> list = sortedList[sortedList.Keys[j]];
			for (int k = 0; k <= list.Count - 1; k++)
			{
				array[num] = list[k];
				num++;
			}
		}
		for (int l = 0; l <= Browserfiles.Count - 1; l++)
		{
			for (int m = 0; m <= array[l].Keys.Length - 1; m++)
			{
				Node node2 = array[l].GetNode(array[l].Keys[m]);
				Node node3 = null;
				if (node2.ParentId.Length > 0)
				{
					node3 = GetNode(node2.ParentId);
					if (node3 == null)
					{
						throw new Exception($"Parent not found with id = {node2.ParentId}");
					}
				}
				if (node3 == null)
				{
					node3 = node;
				}
				node3.AddChild(node2);
			}
		}
		for (int n = 0; n <= Browserfiles.Count - 1; n++)
		{
			for (int num2 = 0; num2 <= array[n].DefaultKeys.Length - 1; num2++)
			{
				Node defaultNode = array[n].GetDefaultNode(array[n].DefaultKeys[num2]);
				Node node4 = GetNode(defaultNode.Id);
				if (node4 != defaultNode)
				{
					Node node5 = GetNode(node4.ParentId);
					if (node5 == null)
					{
						node5 = node;
					}
					node5.RemoveChild(node4);
					defaultNode.AddChild(node4);
					node5.AddChild(defaultNode);
				}
			}
		}
		for (int num3 = 0; num3 <= Browserfiles.Count - 1; num3++)
		{
			foreach (Node refNode in array[num3].RefNodes)
			{
				GetNode(refNode.RefId).MergeFrom(refNode);
			}
		}
		return node;
	}

	private Node GetNode(string Key)
	{
		if (Key == null || Key.Length == 0)
		{
			return null;
		}
		if (!BrowserKeys.TryGetValue(Key, out var value) && !DefaultKeys.TryGetValue(Key, out value))
		{
			return null;
		}
		if (value != null && value.Length > 0)
		{
			return Browserfiles[value].GetNode(Key);
		}
		return null;
	}

	public Node[] Nodes()
	{
		File[] array = new File[Browserfiles.Count];
		Browserfiles.Values.CopyTo(array, 0);
		int num = 0;
		for (int i = 0; i <= array.Length - 1; i++)
		{
			num += array[i].Nodes.Length;
		}
		Node[] array2 = new Node[num];
		num = 0;
		for (int j = 0; j <= array.Length - 1; j++)
		{
			for (int k = 0; k <= array[j].Nodes.Length - 1; k++)
			{
				array2[num] = array[j].Nodes[k];
				num++;
			}
		}
		return array2;
	}

	public override CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities)
	{
		if (initialCapabilities == null)
		{
			initialCapabilities = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}
		Result result = new Result(initialCapabilities);
		Browser().Process(header, result, new List<Match>());
		return result;
	}

	protected override Collection<string> HeaderNames(Collection<string> list)
	{
		return Browser().HeaderNames(list);
	}
}
