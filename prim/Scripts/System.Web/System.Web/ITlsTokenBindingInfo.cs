namespace System.Web;

/// <summary>Provides information about transport layer security (TLS) token binding.</summary>
public interface ITlsTokenBindingInfo
{
	/// <summary>Returns the provided token binding ID.</summary>
	/// <returns>The provided token binding ID.</returns>
	byte[] GetProvidedTokenBindingId();

	/// <summary>Returns the referred token binding ID.</summary>
	/// <returns>The referred token binding ID.</returns>
	byte[] GetReferredTokenBindingId();
}
