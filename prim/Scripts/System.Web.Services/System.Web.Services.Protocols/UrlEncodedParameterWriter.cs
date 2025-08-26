using System.IO;
using System.Reflection;
using System.Text;

namespace System.Web.Services.Protocols;

/// <summary>Provides URL encoding functionality for writers of out-going request parameters for Web service clients implemented using HTTP but without SOAP.</summary>
public abstract class UrlEncodedParameterWriter : MimeParameterWriter
{
	private ParameterInfo[] paramInfos;

	private int numberEncoded;

	private Encoding encoding;

	/// <summary>Gets or sets the encoding used to write parameters to the HTTP request.</summary>
	/// <returns>The encoding used to write parameters to the HTTP request.</returns>
	public override Encoding RequestEncoding
	{
		get
		{
			return encoding;
		}
		set
		{
			encoding = value;
		}
	}

	/// <summary>Returns an initializer for the specified method.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the initializer for the specified method.</returns>
	public override object GetInitializer(LogicalMethodInfo methodInfo)
	{
		if (!ValueCollectionParameterReader.IsSupported(methodInfo))
		{
			return null;
		}
		return methodInfo.InParameters;
	}

	/// <summary>Initializes an instance.</summary>
	/// <param name="initializer">A <see cref="T:System.Reflection.ParameterInfo" /> array obtained through the <see cref="P:System.Web.Services.Protocols.LogicalMethodInfo.InParameters" /> property of the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> class.</param>
	public override void Initialize(object initializer)
	{
		paramInfos = (ParameterInfo[])initializer;
	}

	/// <summary>Encodes all the parameter values for a Web method and writes them to the specified writer.</summary>
	/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object that does the writing to the HTTP request.</param>
	/// <param name="values">The Web method parameter values.</param>
	protected void Encode(TextWriter writer, object[] values)
	{
		numberEncoded = 0;
		for (int i = 0; i < paramInfos.Length; i++)
		{
			ParameterInfo parameterInfo = paramInfos[i];
			if (parameterInfo.ParameterType.IsArray)
			{
				Array array = (Array)values[i];
				for (int j = 0; j < array.Length; j++)
				{
					Encode(writer, parameterInfo.Name, array.GetValue(j));
				}
			}
			else
			{
				Encode(writer, parameterInfo.Name, values[i]);
			}
		}
	}

	/// <summary>Encodes a specified parameter value and writes it to the specified writer.</summary>
	/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object that does the writing to the HTTP request.</param>
	/// <param name="name">The name of the parameter that will be encoded.</param>
	/// <param name="value">The value of the parameter that will be encoded.</param>
	protected void Encode(TextWriter writer, string name, object value)
	{
		if (numberEncoded > 0)
		{
			writer.Write('&');
		}
		writer.Write(UrlEncode(name));
		writer.Write('=');
		writer.Write(UrlEncode(ScalarFormatter.ToString(value)));
		numberEncoded++;
	}

	private string UrlEncode(string value)
	{
		if (encoding != null)
		{
			return UrlEncoder.UrlEscapeString(value, encoding);
		}
		return UrlEncoder.UrlEscapeStringUnicode(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.UrlEncodedParameterWriter" /> class. </summary>
	protected UrlEncodedParameterWriter()
	{
	}
}
