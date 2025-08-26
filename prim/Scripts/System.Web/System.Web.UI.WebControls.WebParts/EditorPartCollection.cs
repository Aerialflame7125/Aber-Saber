using System.Collections;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Contains a collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls used for editing the properties, layout, appearance, and behavior of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. This class cannot be inherited. </summary>
public sealed class EditorPartCollection : ReadOnlyCollectionBase
{
	/// <summary>References a static, read-only, empty instance of the collection. </summary>
	public static readonly EditorPartCollection Empty = new EditorPartCollection();

	/// <summary>Returns a specific member of the collection according to a unique identifier.</summary>
	/// <param name="index">The index of a particular <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> in a collection. </param>
	/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> at the specified index in the collection. </returns>
	public EditorPart this[int index] => (EditorPart)base.InnerList[index];

	/// <summary>Initializes an empty new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class.</summary>
	public EditorPartCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class by passing in an <see cref="T:System.Collections.ICollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
	/// <param name="editorParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls. </param>
	public EditorPartCollection(ICollection editorParts)
	{
		foreach (object editorPart in editorParts)
		{
			base.InnerList.Add(editorPart);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> class by passing in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls, and an <see cref="T:System.Collections.ICollection" /> collection of additional <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
	/// <param name="existingEditorParts">An <see cref="T:System.Collections.ICollection" /> of existing <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in a zone. </param>
	/// <param name="editorParts">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls not in a zone, but created programmatically. </param>
	public EditorPartCollection(EditorPartCollection existingEditorParts, ICollection editorParts)
	{
		foreach (object existingEditorPart in existingEditorParts)
		{
			base.InnerList.Add(existingEditorPart);
		}
		foreach (object editorPart in editorParts)
		{
			base.InnerList.Add(editorPart);
		}
	}

	/// <summary>Returns a value that indicates whether a particular control is in the collection.</summary>
	/// <param name="editorPart">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> being tested for its status as a member of the collection. </param>
	/// <returns>A Boolean value that indicates whether the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> is in the collection.</returns>
	public bool Contains(EditorPart editorPart)
	{
		return base.InnerList.Contains(editorPart);
	}

	/// <summary>Copies the collection to an array of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
	/// <param name="array">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> to contain the copied collection of controls. </param>
	/// <param name="index">The starting point in the array at which to place the collection contents. </param>
	public void CopyTo(EditorPart[] array, int index)
	{
		((ICollection)this).CopyTo((Array)array, index);
	}

	/// <summary>Returns the position of a particular member of the collection.</summary>
	/// <param name="editorPart">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> that is a member of the collection. </param>
	/// <returns>An integer that corresponds to the index of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control in the collection.</returns>
	public int IndexOf(EditorPart editorPart)
	{
		return base.InnerList.IndexOf(editorPart);
	}
}
