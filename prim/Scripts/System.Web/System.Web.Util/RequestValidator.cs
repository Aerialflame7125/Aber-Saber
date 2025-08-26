using System.Configuration;
using System.Web.Configuration;

namespace System.Web.Util;

/// <summary>
///     Defines base methods for custom request validation. </summary>
public class RequestValidator
{
	private static RequestValidator current;

	private static Lazy<RequestValidator> lazyLoader;

	/// <summary>Gets or sets a reference to the current <see cref="T:System.Web.Util.RequestValidator" /> instance that will be used in an application. </summary>
	/// <returns>An instance of the <see cref="T:System.Web.Util.RequestValidator" /> class.</returns>
	/// <exception cref="T:System.ArgumentNullException">The property is <see langword="null" />. </exception>
	public static RequestValidator Current
	{
		get
		{
			if (current == null)
			{
				current = lazyLoader.Value;
			}
			return current;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			current = value;
		}
	}

	static RequestValidator()
	{
		lazyLoader = new Lazy<RequestValidator>(LoadConfiguredValidator);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Util.RequestValidator" /> class. </summary>
	public RequestValidator()
	{
	}

	/// <summary>Validates a string that contains HTTP request data.</summary>
	/// <param name="context">The context of the current request.</param>
	/// <param name="value">The HTTP request data to validate.</param>
	/// <param name="requestValidationSource">An enumeration that represents the source of request data that is being validated. The following are possible values for the enumeration:
	///       <see langword="QueryString" />
	///
	///       <see langword="Form " />
	///
	///       <see langword="Cookies" />
	///
	///       <see langword="Files" />
	///
	///       <see langword="RawUrl" />
	///
	///       <see langword="Path" />
	///
	///       <see langword="PathInfo" />
	///
	///       <see langword="Headers" />
	///     </param>
	/// <param name="collectionKey">The key in the request collection of the item to validate. This parameter is optional. This parameter is used if the data to validate is obtained from a collection. If the data to validate is not from a collection, <paramref name="collectionKey" /> can be <see langword="null" />. </param>
	/// <param name="validationFailureIndex">When this method returns, indicates the zero-based starting point of the problematic or invalid text in the request collection. This parameter is passed uninitialized.</param>
	/// <returns>
	///     <see langword="true" /> if the string to be validated is valid; otherwise, <see langword="false" />.</returns>
	protected internal virtual bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
	{
		validationFailureIndex = 0;
		return !HttpRequest.IsInvalidString(value, out validationFailureIndex);
	}

	private static void ParseTypeName(string spec, out string typeName, out string assemblyName)
	{
		try
		{
			if (string.IsNullOrEmpty(spec))
			{
				typeName = null;
				assemblyName = null;
				return;
			}
			int num = spec.IndexOf(',');
			if (num == -1)
			{
				typeName = spec;
				assemblyName = null;
			}
			else
			{
				typeName = spec.Substring(0, num).Trim();
				assemblyName = spec.Substring(num + 1).Trim();
			}
		}
		catch
		{
			typeName = spec;
			assemblyName = null;
		}
	}

	private static RequestValidator LoadConfiguredValidator()
	{
		HttpRuntimeSection section = HttpRuntime.Section;
		Type type = null;
		string requestValidationType = section.RequestValidationType;
		try
		{
			type = HttpApplication.LoadType<RequestValidator>(requestValidationType, throwOnMissing: true);
		}
		catch (TypeLoadException inner)
		{
			ParseTypeName(requestValidationType, out var typeName, out var assemblyName);
			throw new ConfigurationErrorsException($"Could not load type '{typeName}' from assembly '{assemblyName}'.", inner);
		}
		return (RequestValidator)Activator.CreateInstance(type);
	}
}
