using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides a component editor base class for Web server controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class.</summary>
public abstract class BaseDataListComponentEditor : WindowsFormsComponentEditor
{
	private int initial_page;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.BaseDataListComponentEditor" /> class.</summary>
	/// <param name="initialPage">The index in the array of page control types, of the initial page to display.</param>
	public BaseDataListComponentEditor(int initialPage)
	{
		initial_page = initialPage;
	}

	/// <summary>Edits the specified component by using the specified context descriptor and parent window.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
	/// <param name="obj">An <see cref="T:System.Object" /> implementing the <see cref="T:System.ComponentModel.IComponent" />, which represents the component to edit.</param>
	/// <param name="parent">The <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the parent window.</param>
	/// <returns>
	///   <see langword="true" /> the component was successfully edited; otherwise, <see langword="false" />.</returns>
	public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the index of the initial page to display in the component editor.</summary>
	/// <returns>The index of the initial page in the array.</returns>
	protected override int GetInitialComponentEditorPageIndex()
	{
		return initial_page;
	}
}
