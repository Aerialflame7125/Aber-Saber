using System.ComponentModel;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides a component editor for a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
public class DataListComponentEditor : BaseDataListComponentEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataListComponentEditor" /> class.</summary>
	public DataListComponentEditor()
		: base(0)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataListComponentEditor" /> class, and sets its initial page to the specified index.</summary>
	/// <param name="initialPage">The index of the initial page.</param>
	public DataListComponentEditor(int initialPage)
		: base(initialPage)
	{
	}

	public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array of <see cref="T:System.Type" /> objects corresponding to the pages that can be edited using this editor.</summary>
	/// <returns>An array of <see cref="T:System.Type" /> objects corresponding to the pages that can be edited using this editor.</returns>
	protected override Type[] GetComponentEditorPages()
	{
		throw new NotImplementedException();
	}
}
