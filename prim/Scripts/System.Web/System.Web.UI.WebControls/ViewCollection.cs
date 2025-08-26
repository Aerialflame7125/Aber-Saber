namespace System.Web.UI.WebControls;

/// <summary>Represents a collection container that enables a <see cref="T:System.Web.UI.WebControls.MultiView" /> control to maintain a list of its child controls.</summary>
public class ViewCollection : ControlCollection
{
	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.View" /> control at the specified index location in the <see cref="T:System.Web.UI.WebControls.ViewCollection" /> collection.</summary>
	/// <param name="i">The ordinal index value that specifies the location of the <see cref="T:System.Web.UI.WebControls.View" /> control in the <see cref="T:System.Web.UI.WebControls.ViewCollection" />. </param>
	/// <returns>A reference to a <see cref="T:System.Web.UI.WebControls.View" /> control in the <see cref="T:System.Web.UI.WebControls.ViewCollection" />.</returns>
	public new View this[int i] => (View)base[i];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ViewCollection" /> class.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.MultiView" /> control that owns this collection of child controls. </param>
	public ViewCollection(Control owner)
		: base(owner)
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.View" /> control to the collection.</summary>
	/// <param name="v">The <see cref="T:System.Web.UI.WebControls.View" /> control to add to the collection. </param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="v" /> parameter does not specify a <see cref="T:System.Web.UI.WebControls.View" /> control. </exception>
	public override void Add(Control v)
	{
		if (!(v is View))
		{
			throw new ArgumentException("The parameter is not a View control");
		}
		base.Add(v);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.View" /> control to the collection at the specified index location.</summary>
	/// <param name="index">The array index at which to add the <see cref="T:System.Web.UI.WebControls.View" /> control. </param>
	/// <param name="v">The <see cref="T:System.Web.UI.WebControls.View" /> control to add to the collection. </param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="v" /> parameter does not specify a <see cref="T:System.Web.UI.WebControls.View" /> control. </exception>
	public override void AddAt(int index, Control v)
	{
		if (!(v is View))
		{
			throw new ArgumentException("The parameter is not a View control");
		}
		base.AddAt(index, v);
	}
}
