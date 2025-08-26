using System.Collections;

namespace System.Configuration;

/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingElement" /> objects. This class cannot be inherited.</summary>
public sealed class SettingElementCollection : ConfigurationElementCollection
{
	/// <summary>Gets the type of the configuration collection.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

	protected override string ElementName => "setting";

	/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingElementCollection" /> class.</summary>
	public SettingElementCollection()
	{
	}

	/// <summary>Adds a <see cref="T:System.Configuration.SettingElement" /> object to the collection.</summary>
	/// <param name="element">The <see cref="T:System.Configuration.SettingElement" /> object to add to the collection.</param>
	public void Add(SettingElement element)
	{
		BaseAdd(element);
	}

	/// <summary>Removes all <see cref="T:System.Configuration.SettingElement" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Gets a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
	/// <param name="elementKey">A string value representing the <see cref="T:System.Configuration.SettingElement" /> object in the collection.</param>
	/// <returns>A <see cref="T:System.Configuration.SettingElement" /> object.</returns>
	public SettingElement Get(string elementKey)
	{
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				SettingElement settingElement = (SettingElement)enumerator.Current;
				if (settingElement.Name == elementKey)
				{
					return settingElement;
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	/// <summary>Removes a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
	/// <param name="element">A <see cref="T:System.Configuration.SettingElement" /> object.</param>
	public void Remove(SettingElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		BaseRemove(element.Name);
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new SettingElement();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((SettingElement)element).Name;
	}
}
