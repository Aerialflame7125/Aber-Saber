namespace System.Security.Authentication.ExtendedProtection;

/// <summary>Contains APIs used for token binding.</summary>
public class TokenBinding
{
	private byte[] _rawTokenBindingId;

	/// <summary>Gets the token binding type.</summary>
	/// <returns>The token binding type.</returns>
	public TokenBindingType BindingType { get; private set; }

	internal TokenBinding(TokenBindingType bindingType, byte[] rawData)
	{
		BindingType = bindingType;
		_rawTokenBindingId = rawData;
	}

	/// <summary>Gets the raw token binding Id.</summary>
	/// <returns>The raw token binding Id. The first byte of the raw Id, which represents the binding type, is removed.</returns>
	public byte[] GetRawTokenBindingId()
	{
		if (_rawTokenBindingId == null)
		{
			return null;
		}
		return (byte[])_rawTokenBindingId.Clone();
	}
}
