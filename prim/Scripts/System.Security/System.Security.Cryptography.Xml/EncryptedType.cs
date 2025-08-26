using System.Xml;

namespace System.Security.Cryptography.Xml;

/// <summary>Represents the abstract base class from which the classes <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> and <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> derive.</summary>
public abstract class EncryptedType
{
	private string _id;

	private string _type;

	private string _mimeType;

	private string _encoding;

	private EncryptionMethod _encryptionMethod;

	private CipherData _cipherData;

	private EncryptionPropertyCollection _props;

	private KeyInfo _keyInfo;

	internal XmlElement _cachedXml;

	internal bool CacheValid => _cachedXml != null;

	/// <summary>Gets or sets the <see langword="Id" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
	/// <returns>A string of the <see langword="Id" /> attribute of the <see langword="&lt;EncryptedType&gt;" /> element.</returns>
	public virtual string Id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
			_cachedXml = null;
		}
	}

	/// <summary>Gets or sets the <see langword="Type" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
	/// <returns>A string that describes the text form of the encrypted data.</returns>
	public virtual string Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
			_cachedXml = null;
		}
	}

	/// <summary>Gets or sets the <see langword="MimeType" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
	/// <returns>A string that describes the media type of the encrypted data.</returns>
	public virtual string MimeType
	{
		get
		{
			return _mimeType;
		}
		set
		{
			_mimeType = value;
			_cachedXml = null;
		}
	}

	/// <summary>Gets or sets the <see langword="Encoding" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
	/// <returns>A string that describes the encoding of the encrypted data.</returns>
	public virtual string Encoding
	{
		get
		{
			return _encoding;
		}
		set
		{
			_encoding = value;
			_cachedXml = null;
		}
	}

	/// <summary>Gets of sets the <see langword="&lt;KeyInfo&gt;" /> element in XML encryption.</summary>
	/// <returns>A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
	public KeyInfo KeyInfo
	{
		get
		{
			if (_keyInfo == null)
			{
				_keyInfo = new KeyInfo();
			}
			return _keyInfo;
		}
		set
		{
			_keyInfo = value;
		}
	}

	/// <summary>Gets or sets the <see langword="&lt;EncryptionMethod&gt;" /> element for XML encryption.</summary>
	/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that represents the <see langword="&lt;EncryptionMethod&gt;" /> element.</returns>
	public virtual EncryptionMethod EncryptionMethod
	{
		get
		{
			return _encryptionMethod;
		}
		set
		{
			_encryptionMethod = value;
			_cachedXml = null;
		}
	}

	/// <summary>Gets or sets the <see langword="&lt;EncryptionProperties&gt;" /> element in XML encryption.</summary>
	/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
	public virtual EncryptionPropertyCollection EncryptionProperties
	{
		get
		{
			if (_props == null)
			{
				_props = new EncryptionPropertyCollection();
			}
			return _props;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> value for an instance of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
	/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedType.CipherData" /> property was set to <see langword="null" />.</exception>
	public virtual CipherData CipherData
	{
		get
		{
			if (_cipherData == null)
			{
				_cipherData = new CipherData();
			}
			return _cipherData;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			_cipherData = value;
			_cachedXml = null;
		}
	}

	/// <summary>Adds an <see langword="&lt;EncryptionProperty&gt;" /> child element to the <see langword="&lt;EncryptedProperties&gt;" /> element in the current <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object in XML encryption.</summary>
	/// <param name="ep">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</param>
	public void AddProperty(EncryptionProperty ep)
	{
		EncryptionProperties.Add(ep);
	}

	/// <summary>Loads XML information into the <see langword="&lt;EncryptedType&gt;" /> element in XML encryption.</summary>
	/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use in the <see langword="&lt;EncryptedType&gt;" /> element.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is <see langword="null" />.</exception>
	public abstract void LoadXml(XmlElement value);

	/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object.</summary>
	/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the <see langword="&lt;EncryptedType&gt;" /> element in XML encryption.</returns>
	public abstract XmlElement GetXml();

	/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
	protected EncryptedType()
	{
	}
}
