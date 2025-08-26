using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design;

/// <summary>Implements the basic functionality that can be used to design value editors. These editors can, in turn, provide a user interface for representing and editing the values of objects of the supported data types.</summary>
public abstract class ObjectSelectorEditor : UITypeEditor
{
	/// <summary>Displays a hierarchical collection of labeled items, each represented by a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	public class Selector : TreeView
	{
		/// <summary>This field is for internal use only.</summary>
		[System.MonoTODO]
		public bool clickSeen;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor.Selector" /> class.</summary>
		/// <param name="editor">The <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" />.</param>
		[System.MonoTODO]
		public Selector(ObjectSelectorEditor editor)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a new tree node to the collection.</summary>
		/// <param name="label">The label for the node.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the value for the node.</param>
		/// <param name="parent">The parent of the node.</param>
		/// <returns>A <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor.SelectorNode" /> added to the collection.</returns>
		[System.MonoTODO]
		public SelectorNode AddNode(string label, object value, SelectorNode parent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all tree nodes from the collection.</summary>
		[System.MonoTODO]
		public void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs after the tree node is selected.</summary>
		/// <param name="sender">The source of an event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
		[System.MonoTODO]
		protected void OnAfterSelect(object sender, TreeViewEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		/// <param name="e">Provides data for the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</param>
		[System.MonoTODO]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		/// <param name="e">Provides data for the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</param>
		[System.MonoTODO]
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when the mouse pointer is over the control and a mouse button is clicked.</summary>
		/// <param name="e">Provides data for the <see cref="E:System.Windows.Forms.Control.MouseUp" />, <see cref="E:System.Windows.Forms.Control.MouseDown" />, and <see cref="E:System.Windows.Forms.Control.MouseMove" /> events.</param>
		[System.MonoTODO]
		protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the collection nodes to a specific value.</summary>
		/// <param name="value">The value to be set.</param>
		/// <param name="nodes">The nodes collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection nodes were set; otherwise, <see langword="false" />.</returns>
		[System.MonoTODO]
		public bool SetSelection(object value, TreeNodeCollection nodes)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes the editor service.</summary>
		/// <param name="edSvc">The editor service.</param>
		/// <param name="value">The value to be set.</param>
		[System.MonoTODO]
		public void Start(IWindowsFormsEditorService edSvc, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the editor service.</summary>
		[System.MonoTODO]
		public void Stop()
		{
			throw new NotImplementedException();
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		[System.MonoTODO]
		protected override void WndProc(ref Message m)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Represents a node of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
	public class SelectorNode : TreeNode
	{
		/// <summary>Represents the value for the node.</summary>
		public object value;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor.SelectorNode" /> class.</summary>
		/// <param name="label">The label for the node.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the value for the node.</param>
		public SelectorNode(string label, object value)
			: base(label)
		{
			this.value = value;
		}
	}

	/// <summary>Represents the current value of <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" />.</summary>
	protected object currValue;

	/// <summary>Represents the previous value of <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" />.</summary>
	protected object prevValue;

	/// <summary>Controls whether or not the nodes within the hierarchical collection of labeled items are accessible.</summary>
	public bool SubObjectSelector;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" /> class.</summary>
	public ObjectSelectorEditor()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" /> class.</summary>
	/// <param name="subObjectSelector">The specified sub-object selector value.</param>
	public ObjectSelectorEditor(bool subObjectSelector)
	{
		SubObjectSelector = subObjectSelector;
	}

	/// <summary>Edits the value of the specified object using the editor style indicated by <see cref="Overload:System.ComponentModel.Design.ObjectSelectorEditor.GetEditStyle" />.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
	/// <param name="value">The object to edit.</param>
	/// <returns>The new value of the object. If the value of the object has not changed, the method should return the same object it was passed.</returns>
	[System.MonoTODO]
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
	/// <param name="value">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, <see langword="false" />.</returns>
	public bool EqualsToValue(object value)
	{
		return currValue == value;
	}

	/// <summary>Fills a hierarchical collection of labeled items, with each item represented by a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <param name="selector">A hierarchical collection of labeled items.</param>
	/// <param name="context">The context information for a component.</param>
	/// <param name="provider">The <see cref="M:System.IServiceProvider.GetService(System.Type)" /> method of this interface that obtains the object that provides the service.</param>
	[System.MonoTODO]
	protected virtual void FillTreeWithData(Selector selector, ITypeDescriptorContext context, IServiceProvider provider)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the editor style used by the <see cref="Overload:System.ComponentModel.Design.ObjectSelectorEditor.EditValue" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by <see cref="Overload:System.ComponentModel.Design.ObjectSelectorEditor.EditValue" />.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.DropDown;
	}

	/// <summary>Sets the current <see cref="T:System.ComponentModel.Design.ObjectSelectorEditor" /> to the specified value.</summary>
	/// <param name="value">The specified value.</param>
	public virtual void SetValue(object value)
	{
		currValue = value;
	}
}
