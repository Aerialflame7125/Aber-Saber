using System.EnterpriseServices;

namespace System.Web.Services;

/// <summary>Adding this attribute to a method within an XML Web service created using ASP.NET makes the method callable from remote Web clients. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class WebMethodAttribute : Attribute
{
	private int transactionOption;

	private bool enableSession;

	private int cacheDuration;

	private bool bufferResponse;

	private string description;

	private string messageName;

	private bool transactionOptionSpecified;

	private bool enableSessionSpecified;

	private bool cacheDurationSpecified;

	private bool bufferResponseSpecified;

	private bool descriptionSpecified;

	private bool messageNameSpecified;

	/// <summary>A descriptive message describing the XML Web service method.</summary>
	/// <returns>A descriptive message describing the XML Web service method. The default value is <see cref="F:System.String.Empty" />.</returns>
	public string Description
	{
		get
		{
			if (description != null)
			{
				return description;
			}
			return string.Empty;
		}
		set
		{
			description = value;
			descriptionSpecified = true;
		}
	}

	internal bool DescriptionSpecified => descriptionSpecified;

	/// <summary>Indicates whether session state is enabled for an XML Web service method.</summary>
	/// <returns>
	///     <see langword="true" /> if session state is enabled for an XML Web service method. The default is <see langword="false" />.</returns>
	public bool EnableSession
	{
		get
		{
			return enableSession;
		}
		set
		{
			enableSession = value;
			enableSessionSpecified = true;
		}
	}

	internal bool EnableSessionSpecified => enableSessionSpecified;

	/// <summary>Gets or sets the number of seconds the response should be held in the cache.</summary>
	/// <returns>The number of seconds the response should be held in the cache. The default is 0, which means the response is not cached.</returns>
	public int CacheDuration
	{
		get
		{
			return cacheDuration;
		}
		set
		{
			cacheDuration = value;
			cacheDurationSpecified = true;
		}
	}

	internal bool CacheDurationSpecified => cacheDurationSpecified;

	/// <summary>Gets or sets whether the response for this request is buffered.</summary>
	/// <returns>
	///     <see langword="true" /> if the response for this request is buffered; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool BufferResponse
	{
		get
		{
			return bufferResponse;
		}
		set
		{
			bufferResponse = value;
			bufferResponseSpecified = true;
		}
	}

	internal bool BufferResponseSpecified => bufferResponseSpecified;

	/// <summary>Indicates the transaction support of an XML Web service method.</summary>
	/// <returns>The transaction support of an XML Web service method. The default is <see cref="F:System.EnterpriseServices.TransactionOption.Disabled" />.</returns>
	public TransactionOption TransactionOption
	{
		get
		{
			return (TransactionOption)transactionOption;
		}
		set
		{
			transactionOption = (int)value;
			transactionOptionSpecified = true;
		}
	}

	internal bool TransactionOptionSpecified => transactionOptionSpecified;

	internal bool TransactionEnabled => transactionOption != 0;

	/// <summary>The name used for the XML Web service method in the data passed to and returned from an XML Web service method.</summary>
	/// <returns>The name used for the XML Web service method in the data passed to and from an XML Web service method. The default is the name of the XML Web service method.</returns>
	public string MessageName
	{
		get
		{
			if (messageName != null)
			{
				return messageName;
			}
			return string.Empty;
		}
		set
		{
			messageName = value;
			messageNameSpecified = true;
		}
	}

	internal bool MessageNameSpecified => messageNameSpecified;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
	public WebMethodAttribute()
	{
		enableSession = false;
		transactionOption = 0;
		cacheDuration = 0;
		bufferResponse = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
	/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
	public WebMethodAttribute(bool enableSession)
		: this()
	{
		EnableSession = enableSession;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
	/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
	/// <param name="transactionOption">Initializes the transaction support of an XML Web service method. </param>
	public WebMethodAttribute(bool enableSession, TransactionOption transactionOption)
		: this()
	{
		EnableSession = enableSession;
		this.transactionOption = (int)transactionOption;
		transactionOptionSpecified = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
	/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
	/// <param name="transactionOption">Initializes the transaction support of an XML Web service method. </param>
	/// <param name="cacheDuration">Initializes the number of seconds the response is cached. </param>
	public WebMethodAttribute(bool enableSession, TransactionOption transactionOption, int cacheDuration)
	{
		EnableSession = enableSession;
		this.transactionOption = (int)transactionOption;
		transactionOptionSpecified = true;
		CacheDuration = cacheDuration;
		BufferResponse = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebMethodAttribute" /> class.</summary>
	/// <param name="enableSession">Initializes whether session state is enabled for the XML Web service method. </param>
	/// <param name="transactionOption">Initializes the transaction support of an XML Web service method. </param>
	/// <param name="cacheDuration">Initializes the number of seconds the response is cached. </param>
	/// <param name="bufferResponse">Initializes whether the response for this request is buffered. </param>
	public WebMethodAttribute(bool enableSession, TransactionOption transactionOption, int cacheDuration, bool bufferResponse)
	{
		EnableSession = enableSession;
		this.transactionOption = (int)transactionOption;
		transactionOptionSpecified = true;
		CacheDuration = cacheDuration;
		BufferResponse = bufferResponse;
	}
}
