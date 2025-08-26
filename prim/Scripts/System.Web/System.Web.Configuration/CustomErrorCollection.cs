using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.CustomError" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(CustomError), AddItemName = "error", CollectionType = ConfigurationElementCollectionType.BasicMap)]
public sealed class CustomErrorCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Returns an array of the keys for all of the configuration elements contained in this <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</summary>
	/// <returns>An array containing the keys for all of the <see cref="T:System.Web.Configuration.CustomError" /> objects contained in this <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</returns>
	public string[] AllKeys
	{
		get
		{
			string[] array = new string[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this[i].StatusCode.ToString();
			}
			return array;
		}
	}

	/// <summary>The type of the <see cref="T:System.Web.Configuration.CustomErrorCollection" />.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

	protected override string ElementName => "error";

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> with the specified index.</summary>
	/// <param name="index">The collection error's index. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> at the specified index.</returns>
	public CustomError this[int index]
	{
		get
		{
			return (CustomError)BaseGet(index);
		}
		set
		{
			if (BaseGet(index) != null)
			{
				RemoveAt(index);
			}
			BaseAdd(index, value);
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> with the specified status code.</summary>
	/// <param name="statusCode">The HTTP status code. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> with the specified status code.</returns>
	public new CustomError this[string statusCode] => (CustomError)BaseGet(statusCode);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static CustomErrorCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.CustomError" /> object to the collection.</summary>
	/// <param name="customError">The <see cref="T:System.Web.Configuration.CustomError" /> object to add already exists in the collection or the collection is read only.</param>
	public void Add(CustomError customError)
	{
		BaseAdd(customError);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.CustomError" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new CustomError();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((CustomError)element).StatusCode.ToString();
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomErrorCollection" /> key at the specified index.</summary>
	/// <param name="index">The collection key's index. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.CustomErrorCollection" /> key at the specified index.</returns>
	public string GetKey(int index)
	{
		return (string)BaseGetKey(index);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object with the specified status code.</summary>
	/// <param name="statusCode">The HTTP status code associated with the custom error. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> object with the specified status code.</returns>
	public CustomError Get(string statusCode)
	{
		return (CustomError)BaseGet(statusCode);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CustomError" /> object with the specified index.</summary>
	/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.CustomError" /> object. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.CustomError" /> with the specified index.</returns>
	public CustomError Get(int index)
	{
		return (CustomError)BaseGet(index);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.CustomError" /> object from the collection.</summary>
	/// <param name="statusCode">The HTTP status code associated with the custom error.  </param>
	public void Remove(string statusCode)
	{
		BaseRemove(statusCode);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.CustomError" /> object at the specified index location from the collection.</summary>
	/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.CustomError" /> object to remove. </param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.CustomError" /> to the collection.</summary>
	/// <param name="customError">The <see cref="T:System.Web.Configuration.CustomError" /> to add to the collection. </param>
	public void Set(CustomError customError)
	{
		CustomError customError2 = Get(customError.StatusCode.ToString());
		if (customError2 == null)
		{
			Add(customError);
			return;
		}
		int index = BaseIndexOf(customError2);
		RemoveAt(index);
		BaseAdd(index, customError);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CustomErrorCollection" /> class.</summary>
	public CustomErrorCollection()
	{
	}
}
