namespace System.Web.UI;

/// <summary>Provides standard support for a <see cref="T:System.Web.UI.ControlCollection" /> collection that is always empty.</summary>
public class EmptyControlCollection : ControlCollection
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.EmptyControlCollection" /> class.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this collection as its collection of child controls. </param>
	public EmptyControlCollection(Control owner)
		: base(owner)
	{
	}

	private void ThrowNotSupportedException()
	{
		throw new HttpException(global::SR.GetString("'{0}' does not allow child controls.", base.Owner.GetType().ToString()));
	}

	/// <summary>Denies the addition of the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
	/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to be added. This parameter is always ignored. </param>
	/// <exception cref="T:System.Web.HttpException">Always issued, because the control does not allow child controls. </exception>
	public override void Add(Control child)
	{
		ThrowNotSupportedException();
	}

	/// <summary>Denies the addition of the specified <see cref="T:System.Web.UI.Control" /> object to the collection, at the specified index position.</summary>
	/// <param name="index">The index at which to add the <see cref="T:System.Web.UI.Control" />. This parameter is always ignored. </param>
	/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to be added. This parameter is always ignored. </param>
	/// <exception cref="T:System.Web.HttpException">Always issued, because the control does not allow child controls. </exception>
	public override void AddAt(int index, Control child)
	{
		ThrowNotSupportedException();
	}
}
