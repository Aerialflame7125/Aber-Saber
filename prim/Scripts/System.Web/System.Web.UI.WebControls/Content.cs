using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Holds text, markup, and server controls to render to a <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control in a master page.</summary>
[ToolboxItem(false)]
[Designer("System.Web.UI.Design.WebControls.ContentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ControlBuilder(typeof(ContentBuilderInternal))]
public class Content : Control, INamingContainer, INonBindingContainer
{
	/// <summary>Gets or sets the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control that is associated with the current content.</summary>
	/// <returns>A string containing the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> associated with the current content. The default is an empty string ("").</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to set the property at run time.</exception>
	[Themeable(false)]
	[DefaultValue("")]
	[WebCategory("Behavior")]
	[IDReferenceProperty(typeof(ContentPlaceHolder))]
	public string ContentPlaceHolderID
	{
		get
		{
			return string.Empty;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Occurs when the control binds to a data source.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new event EventHandler DataBinding
	{
		add
		{
			base.DataBinding += value;
		}
		remove
		{
			base.DataBinding -= value;
		}
	}

	/// <summary>Occurs when the control is released from memory.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new event EventHandler Disposed
	{
		add
		{
			base.Disposed += value;
		}
		remove
		{
			base.Disposed -= value;
		}
	}

	/// <summary>Occurs when the control is initialized.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new event EventHandler Init
	{
		add
		{
			base.Init += value;
		}
		remove
		{
			base.Init -= value;
		}
	}

	/// <summary>Occurs when the server control is loaded into the <see cref="T:System.Web.UI.Page" /> control. </summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new event EventHandler Load
	{
		add
		{
			base.Load += value;
		}
		remove
		{
			base.Load -= value;
		}
	}

	/// <summary>Occurs when the server control is about to render to its containing <see cref="T:System.Web.UI.Page" /> control.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new event EventHandler PreRender
	{
		add
		{
			base.PreRender += value;
		}
		remove
		{
			base.PreRender -= value;
		}
	}

	/// <summary>Occurs when the server control is unloaded from memory.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new event EventHandler Unload
	{
		add
		{
			base.Unload += value;
		}
		remove
		{
			base.Unload -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CompositeDataBoundControl" /> class.</summary>
	public Content()
	{
	}
}
