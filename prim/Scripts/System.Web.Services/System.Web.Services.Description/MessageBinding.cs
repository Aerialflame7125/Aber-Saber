namespace System.Web.Services.Description;

/// <summary>Describes how abstract content is mapped into a concrete format.</summary>
public abstract class MessageBinding : NamedItem
{
	private OperationBinding parent;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.OperationBinding" /> of which the current <see cref="T:System.Web.Services.Description.MessageBinding" /> is a member.</summary>
	/// <returns>An <see cref="T:System.Web.Services.Description.OperationBinding" /> of which the current <see cref="T:System.Web.Services.Description.MessageBinding" /> is a member.</returns>
	public OperationBinding OperationBinding => parent;

	internal void SetParent(OperationBinding parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MessageBinding" /> class.</summary>
	protected MessageBinding()
	{
	}
}
