using System.Collections;

namespace System.Security.Cryptography.X509Certificates;

/// <summary>Supports a simple iteration over an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />. This class cannot be inherited.</summary>
public sealed class X509ChainElementEnumerator : IEnumerator
{
	private IEnumerator enumerator;

	/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
	/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
	public X509ChainElement Current => (X509ChainElement)enumerator.Current;

	/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
	/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
	object IEnumerator.Current => enumerator.Current;

	internal X509ChainElementEnumerator(IEnumerable enumerable)
	{
		enumerator = enumerable.GetEnumerator();
	}

	/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
	/// <returns>
	///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
	/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
	public bool MoveNext()
	{
		return enumerator.MoveNext();
	}

	/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
	/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
	public void Reset()
	{
		enumerator.Reset();
	}
}
