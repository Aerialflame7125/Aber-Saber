using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.Caching;

/// <summary>Provides programmatic access to the output-cache providers that are specified in the configuration file for a Web site. </summary>
public static class OutputCache
{
	internal const string DEFAULT_PROVIDER_NAME = "AspNetInternalProvider";

	private static readonly object initLock = new object();

	private static readonly object defaultProviderInitLock = new object();

	private static bool initialized;

	private static string defaultProviderName;

	private static OutputCacheProviderCollection providers;

	private static OutputCacheProvider defaultProvider;

	/// <summary>Gets the name of the default provider that is configured for the output cache.</summary>
	/// <returns>The name of the default provider.</returns>
	public static string DefaultProviderName
	{
		get
		{
			Init();
			if (string.IsNullOrEmpty(defaultProviderName))
			{
				return "AspNetInternalProvider";
			}
			return defaultProviderName;
		}
	}

	internal static OutputCacheProvider DefaultProvider
	{
		get
		{
			if (defaultProvider == null)
			{
				lock (defaultProviderInitLock)
				{
					if (defaultProvider == null)
					{
						defaultProvider = new InMemoryOutputCacheProvider();
					}
				}
			}
			return defaultProvider;
		}
	}

	/// <summary>Gets a collection of the output-cache providers that are specified in the configuration file for a Web site. </summary>
	/// <returns>The collection of configured providers.</returns>
	public static OutputCacheProviderCollection Providers
	{
		get
		{
			Init();
			return providers;
		}
	}

	/// <summary>Deserializes a binary object into output-cache data.</summary>
	/// <param name="stream">The data to deserialize.</param>
	/// <returns>An object that contains the deserialized data.</returns>
	/// <exception cref="T:System.ArgumentException">The deserialized object that is returned by the method is not a valid output-cache type. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="stream" /> is <see langword="null" />. </exception>
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public static object Deserialize(Stream stream)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		object obj = new BinaryFormatter().Deserialize(stream);
		if (obj == null || IsInvalidType(obj))
		{
			throw new ArgumentException("The provided parameter is not of a supported type for serialization and/or deserialization.");
		}
		return obj;
	}

	/// <summary>Serializes output-cache data into binary data.</summary>
	/// <param name="stream">The object to contain the serialized binary data.</param>
	/// <param name="data">The output-cache data to serialize.</param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="data" /> is not one of the specified output-cache types. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="data" /> is <see langword="null" /> or <paramref name="stream" /> is <see langword="null" />. </exception>
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
	public static void Serialize(Stream stream, object data)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (data == null || IsInvalidType(data))
		{
			throw new ArgumentException("The provided parameter is not of a supported type for serialization and/or deserialization.");
		}
		new BinaryFormatter().Serialize(stream, data);
	}

	internal static OutputCacheProvider GetProvider(string providerName)
	{
		if (string.IsNullOrEmpty(providerName))
		{
			return null;
		}
		if (string.Compare(providerName, "AspNetInternalProvider", StringComparison.Ordinal) == 0)
		{
			return DefaultProvider;
		}
		return Providers?[providerName];
	}

	private static bool IsInvalidType(object data)
	{
		if (!(data is MemoryResponseElement) && !(data is FileResponseElement))
		{
			return !(data is SubstitutionResponseElement);
		}
		return false;
	}

	private static void Init()
	{
		if (initialized)
		{
			return;
		}
		lock (initLock)
		{
			if (initialized)
			{
				return;
			}
			OutputCacheSection obj = WebConfigurationManager.GetWebApplicationSection("system.web/caching/outputCache") as OutputCacheSection;
			ProviderSettingsCollection providerSettingsCollection = obj.Providers;
			defaultProviderName = obj.DefaultProviderName;
			if (providerSettingsCollection != null && providerSettingsCollection.Count > 0)
			{
				OutputCacheProviderCollection outputCacheProviderCollection = new OutputCacheProviderCollection();
				foreach (ProviderSettings item in providerSettingsCollection)
				{
					outputCacheProviderCollection.Add(LoadProvider(item));
				}
				outputCacheProviderCollection.SetReadOnly();
				providers = outputCacheProviderCollection;
			}
			initialized = true;
		}
	}

	private static OutputCacheProvider LoadProvider(ProviderSettings ps)
	{
		Type type = HttpApplication.LoadType(ps.Type, throwOnMissing: false);
		if (type == null)
		{
			throw new ConfigurationErrorsException($"Could not load type '{ps.Type}'.");
		}
		OutputCacheProvider obj = Activator.CreateInstance(type) as OutputCacheProvider;
		obj.Initialize(ps.Name, ps.Parameters);
		return obj;
	}

	internal static void RemoveFromProvider(string key, string providerName)
	{
		if (providerName != null)
		{
			OutputCacheProviderCollection outputCacheProviderCollection = Providers;
			OutputCacheProvider outputCacheProvider = ((outputCacheProviderCollection != null && outputCacheProviderCollection.Count != 0) ? outputCacheProviderCollection[providerName] : null);
			if (outputCacheProvider == null)
			{
				throw new ProviderException("Provider '" + providerName + "' was not found.");
			}
			outputCacheProvider.Remove(key);
		}
	}
}
