using System.Security.Authentication.ExtendedProtection;

namespace System.Web;

internal sealed class HttpChannelBindingToken : ChannelBinding
{
	private int _size;

	public override int Size => _size;

	internal HttpChannelBindingToken(IntPtr token, int tokenSize)
	{
		SetHandle(token);
		_size = tokenSize;
	}

	protected override bool ReleaseHandle()
	{
		SetHandle(IntPtr.Zero);
		_size = 0;
		return true;
	}
}
