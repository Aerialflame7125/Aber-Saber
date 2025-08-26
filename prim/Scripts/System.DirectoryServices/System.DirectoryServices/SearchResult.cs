using System.Collections;
using System.Collections.Specialized;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.SearchResult" /> class encapsulates a node in the Active Directory Domain Services hierarchy that is returned during a search through <see cref="T:System.DirectoryServices.DirectorySearcher" />.</summary>
public class SearchResult
{
	private string _Path;

	private ResultPropertyCollection _Properties;

	private DirectoryEntry _Entry;

	private StringCollection _PropsToLoad;

	private bool ispropnull = true;

	private PropertyCollection _Rproperties;

	internal PropertyCollection Rproperties => _Rproperties;

	internal StringCollection PropsToLoad
	{
		get
		{
			if (_PropsToLoad != null)
			{
				return _PropsToLoad;
			}
			return null;
		}
	}

	/// <summary>Gets a <see cref="T:System.DirectoryServices.ResultPropertyCollection" /> collection of properties for this object.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ResultPropertyCollection" /> of properties set on this object.</returns>
	public ResultPropertyCollection Properties
	{
		get
		{
			if (ispropnull)
			{
				_Properties = new ResultPropertyCollection();
				IDictionaryEnumerator enumerator = Rproperties.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					ResultPropertyValueCollection resultPropertyValueCollection = new ResultPropertyValueCollection();
					if (Rproperties[text].Count == 1)
					{
						string component = (string)Rproperties[text].Value;
						resultPropertyValueCollection.Add(component);
					}
					else if (Rproperties[text].Count > 1)
					{
						object[] components = (object[])Rproperties[text].Value;
						resultPropertyValueCollection.AddRange(components);
					}
					_Properties.Add(text, resultPropertyValueCollection);
				}
				ispropnull = false;
			}
			return _Properties;
		}
	}

	/// <summary>Gets the path for this <see cref="T:System.DirectoryServices.SearchResult" />.</summary>
	/// <returns>The path of this <see cref="T:System.DirectoryServices.SearchResult" />.</returns>
	public string Path => _Path;

	private void InitBlock()
	{
		_Properties = null;
		_Entry = null;
		_PropsToLoad = null;
		ispropnull = true;
		_Rproperties = null;
	}

	internal SearchResult(DirectoryEntry entry)
	{
		InitBlock();
		_Entry = entry;
		_Path = entry.Path;
	}

	internal SearchResult(DirectoryEntry entry, PropertyCollection props)
	{
		InitBlock();
		_Entry = entry;
		_Path = entry.Path;
		_Rproperties = props;
	}

	/// <summary>Retrieves the <see cref="T:System.DirectoryServices.DirectoryEntry" /> that corresponds to the <see cref="T:System.DirectoryServices.SearchResult" /> from the Active Directory Domain Services hierarchy.</summary>
	/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> that corresponds to the <see cref="T:System.DirectoryServices.SearchResult" />.</returns>
	public DirectoryEntry GetDirectoryEntry()
	{
		return _Entry;
	}
}
