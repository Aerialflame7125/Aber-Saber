using System.ComponentModel.Design;

namespace System.Windows.Forms.Design;

/// <summary>Provides access to get and set option values for a Windows Forms designer.</summary>
public class WindowsFormsDesignerOptionService : DesignerOptionService
{
	/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.DesignerOptions" /> exposed by the <see cref="T:System.Windows.Forms.Design.WindowsFormsDesignerOptionService" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.DesignerOptions" /> exposed by the <see cref="T:System.Windows.Forms.Design.WindowsFormsDesignerOptionService" />.</returns>
	public virtual DesignerOptions CompatibilityOptions
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.WindowsFormsDesignerOptionService" /> class.</summary>
	public WindowsFormsDesignerOptionService()
	{
	}

	/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
	/// <param name="options">The collection to populate.</param>
	[System.MonoTODO]
	protected override void PopulateOptionCollection(DesignerOptionCollection options)
	{
		throw new NotImplementedException();
	}
}
