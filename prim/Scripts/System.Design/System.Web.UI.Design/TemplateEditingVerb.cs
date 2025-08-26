using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Represents a designer verb that creates a template editing frame, and that can be invoked only by a template editor.</summary>
[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
public class TemplateEditingVerb : DesignerVerb, IDisposable
{
	private int _index;

	/// <summary>Gets the index or other user data for the verb.</summary>
	/// <returns>The index or user data for the verb.</returns>
	public int Index => _index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> class with the specified verb text and index.</summary>
	/// <param name="text">The text to show for the verb on a menu.</param>
	/// <param name="index">An optional integer value that can be used by a designer, typically to indicate the index of the verb within a set of verbs.</param>
	[System.MonoTODO]
	public TemplateEditingVerb(string text, int index)
		: base(text, null)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> class.</summary>
	/// <param name="text">The text to show for the verb on a menu.</param>
	/// <param name="index">An optional integer value that can be used by a designer, typically to indicate the index of the verb within a set of verbs.</param>
	/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that can use this verb.</param>
	public TemplateEditingVerb(string text, int index, TemplatedControlDesigner designer)
		: base(text, designer.TemplateEditingVerbHandler)
	{
		_index = index;
	}

	/// <summary>Attempts to free resources before the object is reclaimed by garbage collection.</summary>
	~TemplateEditingVerb()
	{
		Dispose(disposing: false);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoTODO]
	protected virtual void Dispose(bool disposing)
	{
	}
}
