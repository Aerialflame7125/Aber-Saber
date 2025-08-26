using System.Security.Permissions;

namespace System.ComponentModel;

/// <summary>Provides data for a cancelable event.</summary>
[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
public class CancelEventArgs : EventArgs
{
	private bool cancel;

	/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
	/// <returns>
	///   <see langword="true" /> if the event should be canceled; otherwise, <see langword="false" />.</returns>
	public bool Cancel
	{
		get
		{
			return cancel;
		}
		set
		{
			cancel = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to <see langword="false" />.</summary>
	public CancelEventArgs()
		: this(cancel: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
	/// <param name="cancel">
	///   <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
	public CancelEventArgs(bool cancel)
	{
		this.cancel = cancel;
	}
}
