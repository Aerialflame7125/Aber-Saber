using System.Collections;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.Compiler" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(Compiler), AddItemName = "compiler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
public sealed class CompilerCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets all the keys of the <see cref="T:System.Web.Configuration.CompilerCollection" />.</summary>
	/// <returns>The <see langword="string" /> array containing the collection keys.</returns>
	public string[] AllKeys
	{
		get
		{
			string[] array = new string[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this[i].Language;
			}
			return array;
		}
	}

	/// <summary>Gets the type of the configuration collection.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

	protected override string ElementName => "compiler";

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> at the specified index of the collection.</summary>
	/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
	public Compiler this[int index] => (Compiler)BaseGet(index);

	/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element for the specified language.</summary>
	/// <param name="language">The language for the <see cref="T:System.Web.Configuration.Compiler" /> object within the collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
	public new Compiler this[string language]
	{
		get
		{
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Compiler compiler = (Compiler)enumerator.Current;
					if (compiler.Language.IndexOf(language, StringComparison.InvariantCultureIgnoreCase) != -1)
					{
						return compiler;
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
	}

	static CompilerCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CompilerCollection" /> class.</summary>
	public CompilerCollection()
		: base(CaseInsensitiveComparer.DefaultInvariant)
	{
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new Compiler();
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element at the specified index.</summary>
	/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
	public Compiler Get(int index)
	{
		return (Compiler)BaseGet(index);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.Compiler" /> collection element for the specified language.</summary>
	/// <param name="language">The language for the <see cref="T:System.Web.Configuration.Compiler" /> object within the collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.Compiler" /> object.</returns>
	public Compiler Get(string language)
	{
		return this[language];
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((Compiler)element).Language;
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CompilerCollection" /> key name at the specified index.</summary>
	/// <param name="index">An integer value specifying a <see cref="T:System.Web.Configuration.Compiler" /> within the <see cref="T:System.Web.Configuration.CompilerCollection" />.</param>
	/// <returns>The key name at the specified index of the <see cref="T:System.Web.Configuration.CompilerCollection" />.</returns>
	public string GetKey(int index)
	{
		return (string)BaseGetKey(index);
	}
}
