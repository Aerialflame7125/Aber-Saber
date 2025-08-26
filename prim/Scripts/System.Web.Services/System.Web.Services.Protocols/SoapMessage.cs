using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols;

/// <summary>Represents the data in a SOAP request or SOAP response at a specific <see cref="T:System.Web.Services.Protocols.SoapMessageStage" />.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public abstract class SoapMessage
{
	private SoapMessageStage stage;

	private SoapHeaderCollection headers = new SoapHeaderCollection();

	private Stream stream;

	private SoapExtensionStream extensionStream;

	private string contentType;

	private string contentEncoding;

	private object[] parameterValues;

	private SoapException exception;

	/// <summary>Gets a value indicating the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.OneWay" /> property of either the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> or the <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> attribute applied to the XML Web service method.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.OneWay" /> property of the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> or <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> applied to the XML Web service method is <see langword="true" />; otherwise, <see langword="false" />.</returns>
	public abstract bool OneWay { get; }

	/// <summary>Gets the <see cref="T:System.Web.Services.Protocols.SoapException" /> from the call to the XML Web service method.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapException" /> that occurred in the call to the XML Web service method. <see langword="null" /> if no <see cref="T:System.Web.Services.Protocols.SoapException" /> has occurred during the call to the Web Sevice method.</returns>
	public SoapException Exception
	{
		get
		{
			return exception;
		}
		set
		{
			exception = value;
		}
	}

	/// <summary>When overridden in a derived class, gets a representation of the method prototype for the XML Web service method for which the SOAP request is intended.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the XML Web service method for which the SOAP request is intended.</returns>
	public abstract LogicalMethodInfo MethodInfo { get; }

	/// <summary>A collection of the SOAP headers applied to the current SOAP request or SOAP response.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> of the SOAP headers applied to the current SOAP request or SOAP response. <see langword="null" />, if there are no SOAP headers.</returns>
	public SoapHeaderCollection Headers => headers;

	/// <summary>Gets the data representing the SOAP request or SOAP response in the form of a <see cref="T:System.IO.Stream" />.</summary>
	/// <returns>A read-only instance of the <see cref="T:System.IO.Stream" /> class.</returns>
	public Stream Stream => stream;

	/// <summary>Gets or sets the HTTP <see langword="Content-Type" /> of the SOAP request or SOAP response.</summary>
	/// <returns>The HTTP <see langword="Content-Type" /> of the SOAP request or SOAP response. The default is "text/xml".</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="P:System.Web.Services.Protocols.SoapMessage.ContentType" /> is accessed <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterSerialize" /> or <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterDeserialize" /> stages. </exception>
	public string ContentType
	{
		get
		{
			EnsureStage((SoapMessageStage)5);
			return contentType;
		}
		set
		{
			EnsureStage((SoapMessageStage)5);
			contentType = value;
		}
	}

	/// <summary>Gets or sets the contents of the <see langword="Content-Encoding" /> HTTP header.</summary>
	/// <returns>The contents of the <see langword="Content-Encoding" /> HTTP header.</returns>
	/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterSerialize" /> or <see cref="F:System.Web.Services.Protocols.SoapMessageStage.AfterDeserialize" /> stages. </exception>
	public string ContentEncoding
	{
		get
		{
			EnsureStage((SoapMessageStage)5);
			return contentEncoding;
		}
		set
		{
			EnsureStage((SoapMessageStage)5);
			contentEncoding = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> of the <see cref="T:System.Web.Services.Protocols.SoapMessage" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> of the <see cref="T:System.Web.Services.Protocols.SoapMessage" />.</returns>
	public SoapMessageStage Stage => stage;

	/// <summary>When overridden in a derived class, gets the base URL of the XML Web service.</summary>
	/// <returns>The base URL of the XML Web service.</returns>
	public abstract string Url { get; }

	/// <summary>When overridden in a derived class, gets the SOAPAction HTTP request header field for the SOAP request or SOAP response.</summary>
	/// <returns>The SOAPAction HTTP request header field for the SOAP request or SOAP response.</returns>
	public abstract string Action { get; }

	/// <summary>Gets the version of the SOAP protocol used to communicate with the XML Web service.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
	[ComVisible(false)]
	[DefaultValue(SoapProtocolVersion.Default)]
	public virtual SoapProtocolVersion SoapVersion => SoapProtocolVersion.Default;

	internal SoapMessage()
	{
	}

	internal void SetParameterValues(object[] parameterValues)
	{
		this.parameterValues = parameterValues;
	}

	internal object[] GetParameterValues()
	{
		return parameterValues;
	}

	/// <summary>Gets the parameter passed into the XML Web service method at the specified index.</summary>
	/// <param name="index">The zero-based index of the parameter in the array of parameters. </param>
	/// <returns>An <see cref="T:System.Object" /> representing the parameter at the specified index.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the length of the parameters array. </exception>
	/// <exception cref="T:System.InvalidOperationException">Accessing this property when in parameters are not available. For details see the Remarks section. </exception>
	public object GetInParameterValue(int index)
	{
		EnsureInStage();
		EnsureNoException();
		if (index < 0 || index >= parameterValues.Length)
		{
			throw new IndexOutOfRangeException(Res.GetString("indexMustBeBetweenAnd0Inclusive", parameterValues.Length));
		}
		return parameterValues[index];
	}

	/// <summary>Gets the out parameter passed into the XML Web service method at the specified index.</summary>
	/// <param name="index">The zero-based index of the parameter in the array of parameters. </param>
	/// <returns>An <see cref="T:System.Object" /> representing the parameter at the specified index.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is greater than the length of the parameters array. </exception>
	/// <exception cref="T:System.InvalidOperationException">Accessing this property when out parameters are not available. For details see the Remarks section. </exception>
	public object GetOutParameterValue(int index)
	{
		EnsureOutStage();
		EnsureNoException();
		if (!MethodInfo.IsVoid)
		{
			if (index == int.MaxValue)
			{
				throw new IndexOutOfRangeException(Res.GetString("indexMustBeBetweenAnd0Inclusive", parameterValues.Length));
			}
			index++;
		}
		if (index < 0 || index >= parameterValues.Length)
		{
			throw new IndexOutOfRangeException(Res.GetString("indexMustBeBetweenAnd0Inclusive", parameterValues.Length));
		}
		return parameterValues[index];
	}

	/// <summary>Gets the return value of an XML Web service method.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the return value of the XML Web service method.</returns>
	/// <exception cref="T:System.InvalidOperationException">The XML Web service method does not have a return value.OR The return value is not available. For details see the Remarks section </exception>
	public object GetReturnValue()
	{
		EnsureOutStage();
		EnsureNoException();
		if (MethodInfo.IsVoid)
		{
			throw new InvalidOperationException(Res.GetString("WebNoReturnValue"));
		}
		return parameterValues[0];
	}

	/// <summary>When overridden in a derived class, asserts that the current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> stage is a stage where out parameters are available.</summary>
	/// <exception cref="T:System.InvalidOperationException">Out parameters are not available. </exception>
	protected abstract void EnsureOutStage();

	/// <summary>When overridden in a derived class, asserts that the current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is a stage where in parameters are available.</summary>
	/// <exception cref="T:System.InvalidOperationException">In parameters are not available. </exception>
	protected abstract void EnsureInStage();

	private void EnsureNoException()
	{
		if (exception != null)
		{
			throw new InvalidOperationException(Res.GetString("WebCannotAccessValue"), exception);
		}
	}

	/// <summary>Ensures that the <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> of the call to the XML Web service method is the stage or stages passed in. If the current processing stage is not one of the stages passed in, an exception is thrown.</summary>
	/// <param name="stage">The <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> asserted. </param>
	/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.Services.Protocols.SoapMessageStage" /> is not the asserted stage or stages. </exception>
	protected void EnsureStage(SoapMessageStage stage)
	{
		if ((this.stage & stage) == 0)
		{
			throw new InvalidOperationException(Res.GetString("WebCannotAccessValueStage", this.stage.ToString()));
		}
	}

	internal void SetStream(Stream stream)
	{
		if (extensionStream != null)
		{
			extensionStream.SetInnerStream(stream);
			extensionStream.SetStreamReady();
			extensionStream = null;
		}
		else
		{
			this.stream = stream;
		}
	}

	internal void SetExtensionStream(SoapExtensionStream extensionStream)
	{
		this.extensionStream = extensionStream;
		stream = extensionStream;
	}

	internal void SetStage(SoapMessageStage stage)
	{
		this.stage = stage;
	}

	internal static SoapExtension[] InitializeExtensions(SoapReflectedExtension[] reflectedExtensions, object[] extensionInitializers)
	{
		if (reflectedExtensions == null)
		{
			return null;
		}
		SoapExtension[] array = new SoapExtension[reflectedExtensions.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = reflectedExtensions[i].CreateInstance(extensionInitializers[i]);
		}
		return array;
	}

	internal void InitExtensionStreamChain(SoapExtension[] extensions)
	{
		if (extensions != null)
		{
			for (int i = 0; i < extensions.Length; i++)
			{
				stream = extensions[i].ChainStream(stream);
			}
		}
	}

	internal void RunExtensions(SoapExtension[] extensions, bool throwOnException)
	{
		if (extensions == null)
		{
			return;
		}
		TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "RunExtensions", extensions, throwOnException) : null);
		if ((stage & (SoapMessageStage)12) != 0)
		{
			for (int i = 0; i < extensions.Length; i++)
			{
				if (Tracing.On)
				{
					Tracing.Enter("SoapExtension", traceMethod, new TraceMethod(extensions[i], "ProcessMessage", stage));
				}
				extensions[i].ProcessMessage(this);
				if (Tracing.On)
				{
					Tracing.Exit("SoapExtension", traceMethod);
				}
				if (Exception != null)
				{
					if (throwOnException)
					{
						throw Exception;
					}
					if (Tracing.On)
					{
						Tracing.ExceptionIgnore(TraceEventType.Warning, traceMethod, Exception);
					}
				}
			}
			return;
		}
		for (int num = extensions.Length - 1; num >= 0; num--)
		{
			if (Tracing.On)
			{
				Tracing.Enter("SoapExtension", traceMethod, new TraceMethod(extensions[num], "ProcessMessage", stage));
			}
			extensions[num].ProcessMessage(this);
			if (Tracing.On)
			{
				Tracing.Exit("SoapExtension", traceMethod);
			}
			if (Exception != null)
			{
				if (throwOnException)
				{
					throw Exception;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionIgnore(TraceEventType.Warning, traceMethod, Exception);
				}
			}
		}
	}
}
