using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System;

/// <summary>Identifies the activation context for the current application. This class cannot be inherited.</summary>
[Serializable]
[ComVisible(false)]
public sealed class ActivationContext : IDisposable, ISerializable
{
	/// <summary>Indicates the context for a manifest-activated application.</summary>
	public enum ContextForm
	{
		/// <summary>The application is not in the ClickOnce store.</summary>
		Loose,
		/// <summary>The application is contained in the ClickOnce store.</summary>
		StoreBounded
	}

	private ApplicationIdentity _appid;

	private ContextForm _form;

	private bool _disposed;

	/// <summary>Gets the form, or store context, for the current application.</summary>
	/// <returns>One of the enumeration values.</returns>
	public ContextForm Form => _form;

	/// <summary>Gets the application identity for the current application.</summary>
	/// <returns>An <see cref="T:System.ApplicationIdentity" /> object that identifies the current application.</returns>
	public ApplicationIdentity Identity => _appid;

	private ActivationContext(ApplicationIdentity identity)
	{
		_appid = identity;
	}

	/// <summary>Enables an <see cref="T:System.ActivationContext" /> object to attempt to free resources and perform other cleanup operations before the <see cref="T:System.ActivationContext" /> is reclaimed by garbage collection.</summary>
	~ActivationContext()
	{
		Dispose(disposing: false);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity.</summary>
	/// <param name="identity">An object that identifies an application.</param>
	/// <returns>An object with the specified application identity.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="identity" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.</exception>
	[MonoTODO("Missing validation")]
	public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity)
	{
		if (identity == null)
		{
			throw new ArgumentNullException("identity");
		}
		return new ActivationContext(identity);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ActivationContext" /> class using the specified application identity and array of manifest paths.</summary>
	/// <param name="identity">An object that identifies an application.</param>
	/// <param name="manifestPaths">A string array of manifest paths for the application.</param>
	/// <returns>An object with the specified application identity and array of manifest paths.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="identity" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="manifestPaths" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">No deployment or application identity is specified in <paramref name="identity" />.  
	///  -or-  
	///  <paramref name="identity" /> does not match the identity in the manifests.  
	///  -or-  
	///  <paramref name="identity" /> does not have the same number of components as the manifest paths.</exception>
	[MonoTODO("Missing validation")]
	public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity, string[] manifestPaths)
	{
		if (identity == null)
		{
			throw new ArgumentNullException("identity");
		}
		if (manifestPaths == null)
		{
			throw new ArgumentNullException("manifestPaths");
		}
		return new ActivationContext(identity);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ActivationContext" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (_disposed)
		{
			_disposed = true;
		}
	}

	/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
	/// <param name="info">The object to populate with data.</param>
	/// <param name="context">The structure for this serialization.</param>
	[MonoTODO("Missing serialization support")]
	void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
	{
		if (info == null)
		{
			throw new ArgumentNullException("info");
		}
	}
}
