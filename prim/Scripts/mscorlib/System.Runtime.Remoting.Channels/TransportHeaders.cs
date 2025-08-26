using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels;

/// <summary>Stores a collection of headers used in the channel sinks.</summary>
[Serializable]
[ComVisible(true)]
[MonoTODO("Serialization format not compatible with .NET")]
public class TransportHeaders : ITransportHeaders
{
	private Hashtable hash_table;

	/// <summary>Gets or sets a transport header that is associated with the given key.</summary>
	/// <param name="key">The <see cref="T:System.String" /> that the requested header is associated with.</param>
	/// <returns>A transport header that is associated with the given key, or <see langword="null" /> if the key was not found.</returns>
	public object this[object key]
	{
		get
		{
			return hash_table[key];
		}
		set
		{
			hash_table[key] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.TransportHeaders" /> class.</summary>
	public TransportHeaders()
	{
		hash_table = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
	}

	/// <summary>Returns an enumerator of the stored transport headers.</summary>
	/// <returns>An enumerator of the stored transport headers.</returns>
	public IEnumerator GetEnumerator()
	{
		return hash_table.GetEnumerator();
	}
}
