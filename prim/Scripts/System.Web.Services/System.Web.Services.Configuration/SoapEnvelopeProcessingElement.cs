using System.ComponentModel;
using System.Configuration;

namespace System.Web.Services.Configuration;

/// <summary>Configures a timeout that helps mitigate denial of service attacks by terminating any request that takes longer than the <see cref="P:System.Web.Services.Configuration.SoapEnvelopeProcessingElement.ReadTimeout" /> property value. </summary>
public sealed class SoapEnvelopeProcessingElement : ConfigurationElement
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private readonly ConfigurationProperty readTimeout = new ConfigurationProperty("readTimeout", typeof(int), int.MaxValue, new InfiniteIntConverter(), null, ConfigurationPropertyOptions.None);

	private readonly ConfigurationProperty strict = new ConfigurationProperty("strict", typeof(bool), false);

	/// <summary>Gets or sets the timeout period used to determine whether to terminate requests to mitigate against denial of service attacks.</summary>
	/// <returns>The time to wait before terminating requests to <see cref="M:System.Xml.XmlReader.Read" /> and <see cref="M:System.Xml.XmlReader.MoveToContent" />.</returns>
	[ConfigurationProperty("readTimeout", DefaultValue = int.MaxValue)]
	[TypeConverter(typeof(InfiniteIntConverter))]
	public int ReadTimeout
	{
		get
		{
			return (int)base[readTimeout];
		}
		set
		{
			base[readTimeout] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether to throw an exception if the serializer encounters unexpected elements or attributes.</summary>
	/// <returns>
	///     <see langword="true" /> if the Web services serializer tries to detect unexpected elements or attributes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("strict", DefaultValue = false)]
	public bool IsStrict
	{
		get
		{
			return (bool)base[strict];
		}
		set
		{
			base[strict] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> class. </summary>
	public SoapEnvelopeProcessingElement()
	{
		properties.Add(readTimeout);
		properties.Add(strict);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> class using the provided <see cref="T:System.Int32" /> value. </summary>
	/// <param name="readTimeout">The value of the timeout period.</param>
	public SoapEnvelopeProcessingElement(int readTimeout)
		: this()
	{
		ReadTimeout = readTimeout;
	}

	/// <summary>Gets or sets the timeout period used to determine whether to terminate requests to mitigate against denial of service attacks.</summary>
	/// <param name="readTimeout">The time to wait before terminating requests to <see cref="M:System.Xml.XmlReader.Read" /> and <see cref="M:System.Xml.XmlReader.MoveToContent" />.</param>
	/// <param name="strict">Whether to throw an exception if the serializer encounters elements or attributes that were not in the original schema. For details, see the <see cref="P:System.Web.Services.Configuration.SoapEnvelopeProcessingElement.IsStrict" /> property.</param>
	public SoapEnvelopeProcessingElement(int readTimeout, bool strict)
		: this()
	{
		ReadTimeout = readTimeout;
		IsStrict = strict;
	}
}
