using System.Collections;

namespace System.Security.Cryptography.Pkcs;

/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface.</summary>
public sealed class CmsRecipientEnumerator : IEnumerator
{
	private IEnumerator enumerator;

	/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
	public CmsRecipient Current => (CmsRecipient)enumerator.Current;

	/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
	/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
	object IEnumerator.Current => enumerator.Current;

	internal CmsRecipientEnumerator(IEnumerable enumerable)
	{
		enumerator = enumerable.GetEnumerator();
	}

	/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.MoveNext" /> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
	/// <returns>
	///   <see langword="true" /> if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object; <see langword="false" /> if the enumeration moved past the last item in the enumeration.</returns>
	public bool MoveNext()
	{
		return enumerator.MoveNext();
	}

	/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
	public void Reset()
	{
		enumerator.Reset();
	}
}
