using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Serves as the base class for controls that reside in <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zones and are used to edit <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
[Bindable(false)]
[Designer("System.Web.UI.Design.WebControls.WebParts.EditorPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public abstract class EditorPart : Part
{
	private bool display = true;

	private WebPart webPartToEdit;

	private object zone;

	private string displayTitle;

	/// <summary>Gets a value that indicates whether a control should be displayed when its associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is in edit mode.</summary>
	/// <returns>A Boolean value that indicates whether the control should be displayed. The default value is <see langword="true" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual bool Display => display;

	/// <summary>Gets a string that contains the title text displayed in the title bar of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
	/// <returns>A string that represents the complete, visible title of the control. The default value is a calculated, culture-specific string supplied by the .NET Framework.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string DisplayTitle => displayTitle;

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is currently being edited. </summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is currently in edit mode.</returns>
	protected WebPart WebPartToEdit => webPartToEdit;

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can be called only by an inherited class.</summary>
	protected EditorPart()
	{
	}

	/// <summary>Saves the values in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control to the corresponding properties in the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the action of saving values from the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is successful; otherwise (if an error occurs), <see langword="false" />. </returns>
	public abstract bool ApplyChanges();

	/// <summary>Retrieves the current state of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control's parent zone.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> that has the current state of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone that contains an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</returns>
	protected override IDictionary GetDesignModeState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that contains the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control is <see langword="null" />.</exception>
	protected internal override void OnPreRender(EventArgs e)
	{
		if (zone == null)
		{
			throw new InvalidOperationException();
		}
		base.OnPreRender(e);
		if (!Display)
		{
			Visible = false;
		}
	}

	/// <summary>Retrieves the property values from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control for its associated <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
	public abstract void SyncChanges();
}
