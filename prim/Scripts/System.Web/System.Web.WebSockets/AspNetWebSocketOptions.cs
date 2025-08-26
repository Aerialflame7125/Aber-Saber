namespace System.Web.WebSockets;

/// <summary>Specifies configuration settings for an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
public sealed class AspNetWebSocketOptions
{
	private string _subProtocol;

	/// <summary>Gets or sets whether the URL that initiatedthe WebSocket connection corresponds to the current server.</summary>
	/// <returns>
	///     <see langword="true" /> if the URL that initiatedthe WebSocket connection corresponds to the current server; otherwise, <see langword="false" />.</returns>
	public bool RequireSameOrigin { get; set; }

	/// <summary>Gets or sets the name of an application-specific protocol that a remote client and a server can use to exchange data over an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
	/// <returns>The name of the protocol.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The protocol name assigned to the property contains invalid characters.</exception>
	public string SubProtocol
	{
		get
		{
			return _subProtocol;
		}
		set
		{
			if (value != null && !SubProtocolUtil.IsValidSubProtocolName(value))
			{
				throw new ArgumentOutOfRangeException("value");
			}
			_subProtocol = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.WebSockets.AspNetWebSocketOptions" /> class.</summary>
	public AspNetWebSocketOptions()
	{
	}
}
