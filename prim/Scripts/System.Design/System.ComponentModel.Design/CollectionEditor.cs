using System.Collections;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design;

/// <summary>Provides a user interface that can edit most types of collections at design time.</summary>
public class CollectionEditor : UITypeEditor
{
	/// <summary>Provides a modal dialog box for editing the contents of a collection using a <see cref="T:System.Drawing.Design.UITypeEditor" />.</summary>
	protected abstract class CollectionForm : Form
	{
		private CollectionEditor editor;

		private object editValue;

		/// <summary>Gets the data type of each item in the collection.</summary>
		/// <returns>The data type of the collection items.</returns>
		protected Type CollectionItemType => editor.CollectionItemType;

		/// <summary>Gets the data type of the collection object.</summary>
		/// <returns>The data type of the collection object.</returns>
		protected Type CollectionType => editor.CollectionType;

		/// <summary>Gets a type descriptor that indicates the current context.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context currently in use, or <see langword="null" /> if no context is available.</returns>
		protected ITypeDescriptorContext Context => editor.Context;

		/// <summary>Gets or sets the collection object to edit.</summary>
		/// <returns>The collection object to edit.</returns>
		public object EditValue
		{
			get
			{
				return editValue;
			}
			set
			{
				editValue = value;
				OnEditValueChanged();
			}
		}

		/// <summary>Gets or sets the array of items for this form to display.</summary>
		/// <returns>An array of objects for the form to display.</returns>
		protected object[] Items
		{
			get
			{
				return editor.GetItems(editValue);
			}
			set
			{
				if (editValue == null)
				{
					object obj = null;
					try
					{
						obj = ((!typeof(Array).IsAssignableFrom(CollectionType)) ? Activator.CreateInstance(CollectionType) : Array.CreateInstance(CollectionItemType, 0));
					}
					catch
					{
					}
					object obj3 = editor.SetItems(obj, value);
					if (obj3 != obj)
					{
						EditValue = obj3;
					}
				}
				else
				{
					object obj4 = editor.SetItems(editValue, value);
					if (obj4 != editValue)
					{
						EditValue = obj4;
					}
				}
			}
		}

		/// <summary>Gets the available item types that can be created for this collection.</summary>
		/// <returns>The types of items that can be created.</returns>
		protected Type[] NewItemTypes => editor.NewItemTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CollectionEditor.CollectionForm" /> class.</summary>
		/// <param name="editor">The <see cref="T:System.ComponentModel.Design.CollectionEditor" /> to use for editing the collection.</param>
		public CollectionForm(CollectionEditor editor)
		{
			this.editor = editor;
		}

		/// <summary>Indicates whether you can remove the original members of the collection.</summary>
		/// <param name="value">The value to remove.</param>
		/// <returns>
		///   <see langword="true" /> if it is permissible to remove this value from the collection; otherwise, <see langword="false" />. By default, this method returns the value from <see cref="M:System.ComponentModel.Design.CollectionEditor.CanRemoveInstance(System.Object)" /> of the <see cref="T:System.ComponentModel.Design.CollectionEditor" /> for this form.</returns>
		protected bool CanRemoveInstance(object value)
		{
			return editor.CanRemoveInstance(value);
		}

		/// <summary>Indicates whether multiple collection items can be selected at once.</summary>
		/// <returns>
		///   <see langword="true" /> if it multiple collection members can be selected at the same time; otherwise, <see langword="false" />. By default, this method returns the value from <see cref="M:System.ComponentModel.Design.CollectionEditor.CanSelectMultipleInstances" /> of the <see cref="T:System.ComponentModel.Design.CollectionEditor" /> for this form.</returns>
		protected virtual bool CanSelectMultipleInstances()
		{
			return editor.CanSelectMultipleInstances();
		}

		/// <summary>Creates a new instance of the specified collection item type.</summary>
		/// <param name="itemType">The type of item to create.</param>
		/// <returns>A new instance of the specified object, or <see langword="null" /> if the user chose to cancel the creation of this instance.</returns>
		protected object CreateInstance(Type itemType)
		{
			return editor.CreateInstance(itemType);
		}

		/// <summary>Destroys the specified instance of the object.</summary>
		/// <param name="instance">The object to destroy.</param>
		protected void DestroyInstance(object instance)
		{
			editor.DestroyInstance(instance);
		}

		/// <summary>Displays the specified exception to the user.</summary>
		/// <param name="e">The exception to display.</param>
		protected virtual void DisplayError(Exception e)
		{
			MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		/// <summary>Gets the requested service, if it is available.</summary>
		/// <param name="serviceType">The type of service to retrieve.</param>
		/// <returns>An instance of the service, or <see langword="null" /> if the service cannot be found.</returns>
		protected override object GetService(Type serviceType)
		{
			return editor.GetService(serviceType);
		}

		/// <summary>Provides an opportunity to perform processing when a collection value has changed.</summary>
		protected abstract void OnEditValueChanged();

		/// <summary>Shows the dialog box for the collection editor using the specified <see cref="T:System.Windows.Forms.Design.IWindowsFormsEditorService" /> object.</summary>
		/// <param name="edSvc">An <see cref="T:System.Windows.Forms.Design.IWindowsFormsEditorService" /> that can be used to show the dialog box.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> that indicates the result code returned from the dialog box.</returns>
		protected internal virtual DialogResult ShowEditorDialog(IWindowsFormsEditorService edSvc)
		{
			return edSvc.ShowDialog(this);
		}
	}

	private class ConcreteCollectionForm : CollectionForm
	{
		internal class ObjectContainerConverter : TypeConverter
		{
			private class ObjectContainerPropertyDescriptor : SimplePropertyDescriptor
			{
				private AttributeCollection attributes;

				public override AttributeCollection Attributes => attributes;

				public ObjectContainerPropertyDescriptor(Type componentType, Type propertyType)
					: base(componentType, "Value", propertyType)
				{
					CategoryAttribute categoryAttribute = new CategoryAttribute(propertyType.Name);
					attributes = new AttributeCollection(categoryAttribute);
				}

				public override object GetValue(object component)
				{
					return ((ObjectContainer)component).Object;
				}

				public override void SetValue(object component, object value)
				{
					((ObjectContainer)component).Object = value;
				}
			}

			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				ObjectContainer objectContainer = (ObjectContainer)value;
				ObjectContainerPropertyDescriptor objectContainerPropertyDescriptor = new ObjectContainerPropertyDescriptor(value.GetType(), objectContainer.editor.CollectionItemType);
				return new PropertyDescriptorCollection(new PropertyDescriptor[1] { objectContainerPropertyDescriptor });
			}

			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}

		[TypeConverter(typeof(ObjectContainerConverter))]
		private class ObjectContainer
		{
			internal object Object;

			internal CollectionEditor editor;

			internal string Name => editor.GetDisplayText(Object);

			public ObjectContainer(object obj, CollectionEditor editor)
			{
				Object = obj;
				this.editor = editor;
			}

			public override string ToString()
			{
				return Name;
			}
		}

		private class UpdateableListbox : ListBox
		{
			public void DoRefreshItem(int index)
			{
				RefreshItem(index);
			}
		}

		private CollectionEditor editor;

		private Label labelMember;

		private Label labelProperty;

		private UpdateableListbox itemsList;

		private PropertyGrid itemDisplay;

		private Button doClose;

		private Button moveUp;

		private Button moveDown;

		private Button doAdd;

		private Button doRemove;

		private Button doCancel;

		private ComboBox addType;

		public ConcreteCollectionForm(CollectionEditor editor)
			: base(editor)
		{
			this.editor = editor;
			labelMember = new Label();
			labelProperty = new Label();
			itemsList = new UpdateableListbox();
			itemDisplay = new PropertyGrid();
			doClose = new Button();
			moveUp = new Button();
			moveDown = new Button();
			doAdd = new Button();
			doRemove = new Button();
			doCancel = new Button();
			addType = new ComboBox();
			SuspendLayout();
			labelMember.Location = new Point(12, 9);
			labelMember.Size = new Size(55, 13);
			labelMember.Text = "Members:";
			labelProperty.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			labelProperty.Location = new Point(172, 9);
			labelProperty.Size = new Size(347, 13);
			labelProperty.Text = "Properties:";
			itemsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			itemsList.HorizontalScrollbar = true;
			itemsList.Location = new Point(12, 25);
			itemsList.SelectionMode = SelectionMode.MultiExtended;
			itemsList.Size = new Size(120, 290);
			itemsList.TabIndex = 0;
			itemsList.SelectedIndexChanged += itemsList_SelectedIndexChanged;
			itemDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			itemDisplay.HelpVisible = false;
			itemDisplay.Location = new Point(175, 25);
			itemDisplay.Size = new Size(344, 314);
			itemDisplay.TabIndex = 6;
			itemDisplay.PropertyValueChanged += itemDisplay_PropertyValueChanged;
			doClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			doClose.Location = new Point(341, 345);
			doClose.Size = new Size(86, 26);
			doClose.TabIndex = 7;
			doClose.Text = "OK";
			doClose.Click += doClose_Click;
			moveUp.Location = new Point(138, 25);
			moveUp.Size = new Size(31, 28);
			moveUp.TabIndex = 4;
			moveUp.Enabled = false;
			moveUp.Text = "Up";
			moveUp.Click += moveUp_Click;
			moveDown.Location = new Point(138, 59);
			moveDown.Size = new Size(31, 28);
			moveDown.TabIndex = 5;
			moveDown.Enabled = false;
			moveDown.Text = "Dn";
			moveDown.Click += moveDown_Click;
			doAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			doAdd.Location = new Point(12, 346);
			doAdd.Size = new Size(59, 25);
			doAdd.TabIndex = 1;
			doAdd.Text = "Add";
			doAdd.Click += doAdd_Click;
			doRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			doRemove.Location = new Point(77, 346);
			doRemove.Size = new Size(55, 25);
			doRemove.TabIndex = 2;
			doRemove.Text = "Remove";
			doRemove.Click += doRemove_Click;
			doCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			doCancel.DialogResult = DialogResult.Cancel;
			doCancel.Location = new Point(433, 345);
			doCancel.Size = new Size(86, 26);
			doCancel.TabIndex = 8;
			doCancel.Text = "Cancel";
			doCancel.Click += doCancel_Click;
			addType.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			addType.DropDownStyle = ComboBoxStyle.DropDownList;
			addType.Location = new Point(12, 319);
			addType.Size = new Size(120, 21);
			addType.TabIndex = 3;
			base.AcceptButton = doClose;
			base.CancelButton = doCancel;
			base.ClientSize = new Size(531, 381);
			base.ControlBox = false;
			base.Controls.Add(addType);
			base.Controls.Add(doCancel);
			base.Controls.Add(doRemove);
			base.Controls.Add(doAdd);
			base.Controls.Add(moveDown);
			base.Controls.Add(moveUp);
			base.Controls.Add(doClose);
			base.Controls.Add(itemDisplay);
			base.Controls.Add(itemsList);
			base.Controls.Add(labelProperty);
			base.Controls.Add(labelMember);
			base.HelpButton = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new Size(400, 300);
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			ResumeLayout(performLayout: false);
			if (editor.CollectionType.IsGenericType)
			{
				Text = editor.CollectionItemType.Name + " Collection Editor";
			}
			else
			{
				Text = editor.CollectionType.Name + " Collection Editor";
			}
			Type[] newItemTypes = editor.NewItemTypes;
			foreach (Type type in newItemTypes)
			{
				addType.Items.Add(type.Name);
			}
			if (addType.Items.Count > 0)
			{
				addType.SelectedIndex = 0;
			}
		}

		private void UpdateItems()
		{
			object[] items = editor.GetItems(base.EditValue);
			if (items != null)
			{
				itemsList.BeginUpdate();
				itemsList.Items.Clear();
				object[] array = items;
				foreach (object obj in array)
				{
					itemsList.Items.Add(new ObjectContainer(obj, editor));
				}
				if (itemsList.Items.Count > 0)
				{
					itemsList.SelectedIndex = 0;
				}
				itemsList.EndUpdate();
			}
		}

		private void doClose_Click(object sender, EventArgs e)
		{
			SetEditValue();
			Close();
		}

		private void SetEditValue()
		{
			object[] array = new object[itemsList.Items.Count];
			for (int i = 0; i < itemsList.Items.Count; i++)
			{
				array[i] = ((ObjectContainer)itemsList.Items[i]).Object;
			}
			base.Items = array;
		}

		private void doCancel_Click(object sender, EventArgs e)
		{
			editor.CancelChanges();
			Close();
		}

		private void itemsList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (itemsList.SelectedIndex == -1)
			{
				itemDisplay.SelectedObject = null;
				return;
			}
			if (itemsList.SelectedIndex <= 0 || itemsList.SelectedItems.Count > 1)
			{
				moveUp.Enabled = false;
			}
			else
			{
				moveUp.Enabled = true;
			}
			if (itemsList.SelectedIndex > itemsList.Items.Count - 2 || itemsList.SelectedItems.Count > 1)
			{
				moveDown.Enabled = false;
			}
			else
			{
				moveDown.Enabled = true;
			}
			if (itemsList.SelectedItems.Count == 1)
			{
				ObjectContainer objectContainer = (ObjectContainer)itemsList.SelectedItem;
				if (Type.GetTypeCode(objectContainer.Object.GetType()) != TypeCode.Object)
				{
					itemDisplay.SelectedObject = objectContainer;
				}
				else
				{
					itemDisplay.SelectedObject = objectContainer.Object;
				}
			}
			else
			{
				object[] array = new object[itemsList.SelectedItems.Count];
				for (int i = 0; i < itemsList.SelectedItems.Count; i++)
				{
					if (Type.GetTypeCode(((ObjectContainer)itemsList.SelectedItem).Object.GetType()) != TypeCode.Object)
					{
						array[i] = (ObjectContainer)itemsList.SelectedItems[i];
					}
					else
					{
						array[i] = ((ObjectContainer)itemsList.SelectedItems[i]).Object;
					}
				}
				itemDisplay.SelectedObjects = array;
			}
			labelProperty.Text = ((ObjectContainer)itemsList.SelectedItem).Name + " properties:";
		}

		private void itemDisplay_PropertyValueChanged(object sender, EventArgs e)
		{
			int[] array = new int[itemsList.SelectedItems.Count];
			for (int i = 0; i < itemsList.SelectedItems.Count; i++)
			{
				array[i] = itemsList.Items.IndexOf(itemsList.SelectedItems[i]);
			}
			SetEditValue();
			itemsList.BeginUpdate();
			itemsList.ClearSelected();
			int[] array2 = array;
			foreach (int index in array2)
			{
				itemsList.DoRefreshItem(index);
				itemsList.SetSelected(index, value: true);
			}
			itemsList.SelectedIndex = array[0];
			itemsList.EndUpdate();
		}

		private void moveUp_Click(object sender, EventArgs e)
		{
			if (itemsList.SelectedIndex > 0)
			{
				object selectedItem = itemsList.SelectedItem;
				int selectedIndex = itemsList.SelectedIndex;
				itemsList.Items.RemoveAt(selectedIndex);
				itemsList.Items.Insert(selectedIndex - 1, selectedItem);
				itemsList.SelectedIndex = selectedIndex - 1;
			}
		}

		private void moveDown_Click(object sender, EventArgs e)
		{
			if (itemsList.SelectedIndex <= itemsList.Items.Count - 2)
			{
				object selectedItem = itemsList.SelectedItem;
				int selectedIndex = itemsList.SelectedIndex;
				itemsList.Items.RemoveAt(selectedIndex);
				itemsList.Items.Insert(selectedIndex + 1, selectedItem);
				itemsList.SelectedIndex = selectedIndex + 1;
			}
		}

		private void doAdd_Click(object sender, EventArgs e)
		{
			object obj;
			try
			{
				obj = editor.CreateInstance(editor.NewItemTypes[addType.SelectedIndex]);
			}
			catch (Exception e2)
			{
				DisplayError(e2);
				return;
			}
			itemsList.Items.Add(new ObjectContainer(obj, editor));
			itemsList.SelectedIndex = -1;
			itemsList.SelectedIndex = itemsList.Items.Count - 1;
		}

		private void doRemove_Click(object sender, EventArgs e)
		{
			if (itemsList.SelectedIndex != -1)
			{
				int[] array = new int[itemsList.SelectedItems.Count];
				for (int i = 0; i < itemsList.SelectedItems.Count; i++)
				{
					array[i] = itemsList.Items.IndexOf(itemsList.SelectedItems[i]);
				}
				for (int num = array.Length - 1; num >= 0; num--)
				{
					itemsList.Items.RemoveAt(array[num]);
				}
				itemsList.SelectedIndex = Math.Min(array[0], itemsList.Items.Count - 1);
			}
		}

		protected override void OnEditValueChanged()
		{
			UpdateItems();
		}
	}

	private Type type;

	private Type collectionItemType;

	private Type[] newItemTypes;

	private ITypeDescriptorContext context;

	private IServiceProvider provider;

	private IWindowsFormsEditorService editorService;

	/// <summary>Gets the data type of each item in the collection.</summary>
	/// <returns>The data type of the collection items.</returns>
	protected Type CollectionItemType => collectionItemType;

	/// <summary>Gets the data type of the collection object.</summary>
	/// <returns>The data type of the collection object.</returns>
	protected Type CollectionType => type;

	/// <summary>Gets a type descriptor that indicates the current context.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context currently in use, or <see langword="null" /> if no context is available.</returns>
	protected ITypeDescriptorContext Context => context;

	/// <summary>Gets the Help keyword to display the Help topic or topic list for when the editor's dialog box Help button or the F1 key is pressed.</summary>
	/// <returns>The Help keyword to display the Help topic or topic list for when Help is requested from the editor.</returns>
	protected virtual string HelpTopic => "CollectionEditor";

	/// <summary>Gets the available types of items that can be created for this collection.</summary>
	/// <returns>The types of items that can be created.</returns>
	protected Type[] NewItemTypes => newItemTypes;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CollectionEditor" /> class using the specified collection type.</summary>
	/// <param name="type">The type of the collection for this editor to edit.</param>
	public CollectionEditor(Type type)
	{
		this.type = type;
		collectionItemType = CreateCollectionItemType();
		newItemTypes = CreateNewItemTypes();
	}

	/// <summary>Cancels changes to the collection.</summary>
	protected virtual void CancelChanges()
	{
	}

	/// <summary>Indicates whether original members of the collection can be removed.</summary>
	/// <param name="value">The value to remove.</param>
	/// <returns>
	///   <see langword="true" /> if it is permissible to remove this value from the collection; otherwise, <see langword="false" />. The default implementation always returns <see langword="true" />.</returns>
	protected virtual bool CanRemoveInstance(object value)
	{
		return true;
	}

	/// <summary>Indicates whether multiple collection items can be selected at once.</summary>
	/// <returns>
	///   <see langword="true" /> if it multiple collection members can be selected at the same time; otherwise, <see langword="false" />. By default, this returns <see langword="true" />.</returns>
	protected virtual bool CanSelectMultipleInstances()
	{
		return true;
	}

	/// <summary>Creates a new form to display and edit the current collection.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.CollectionEditor.CollectionForm" /> to provide as the user interface for editing the collection.</returns>
	protected virtual CollectionForm CreateCollectionForm()
	{
		return new ConcreteCollectionForm(this);
	}

	/// <summary>Gets the data type that this collection contains.</summary>
	/// <returns>The data type of the items in the collection, or an <see cref="T:System.Object" /> if no <see langword="Item" /> property can be located on the collection.</returns>
	protected virtual Type CreateCollectionItemType()
	{
		PropertyInfo[] properties = type.GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			if (propertyInfo.Name == "Item")
			{
				return propertyInfo.PropertyType;
			}
		}
		return typeof(object);
	}

	/// <summary>Creates a new instance of the specified collection item type.</summary>
	/// <param name="itemType">The type of item to create.</param>
	/// <returns>A new instance of the specified object.</returns>
	protected virtual object CreateInstance(Type itemType)
	{
		object obj = null;
		if (typeof(IComponent).IsAssignableFrom(itemType) && GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
		{
			obj = designerHost.CreateComponent(itemType);
		}
		if (obj == null)
		{
			obj = TypeDescriptor.CreateInstance(provider, itemType, null, null);
		}
		return obj;
	}

	/// <summary>Gets the data types that this collection editor can contain.</summary>
	/// <returns>An array of data types that this collection can contain.</returns>
	protected virtual Type[] CreateNewItemTypes()
	{
		return new Type[1] { collectionItemType };
	}

	/// <summary>Destroys the specified instance of the object.</summary>
	/// <param name="instance">The object to destroy.</param>
	protected virtual void DestroyInstance(object instance)
	{
		if (instance is IComponent component && GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
		{
			designerHost.DestroyComponent(component);
		}
	}

	/// <summary>Edits the value of the specified object using the specified service provider and context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <param name="provider">A service provider object through which editing services can be obtained.</param>
	/// <param name="value">The object to edit the value of.</param>
	/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
	/// <exception cref="T:System.ComponentModel.Design.CheckoutException">An attempt to check out a file that is checked into a source code management program did not succeed.</exception>
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		this.context = context;
		this.provider = provider;
		if (context != null && provider != null)
		{
			editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (editorService != null)
			{
				CollectionForm collectionForm = CreateCollectionForm();
				collectionForm.EditValue = value;
				collectionForm.ShowEditorDialog(editorService);
				return collectionForm.EditValue;
			}
		}
		return base.EditValue(context, provider, value);
	}

	/// <summary>Retrieves the display text for the given list item.</summary>
	/// <param name="value">The list item for which to retrieve display text.</param>
	/// <returns>The display text for <paramref name="value" />.</returns>
	protected virtual string GetDisplayText(object value)
	{
		if (value == null)
		{
			return string.Empty;
		}
		PropertyInfo property = value.GetType().GetProperty("Name");
		if (property != null && property.GetValue(value, null) is string { Length: not 0 } text)
		{
			return text;
		}
		if (Type.GetTypeCode(value.GetType()) == TypeCode.Object)
		{
			return value.GetType().Name;
		}
		return value.ToString();
	}

	/// <summary>Gets the edit style used by the <see cref="M:System.ComponentModel.Design.CollectionEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the provided editing style. If the method is not supported in the specified context, this method will return the <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" /> identifier.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.Modal;
	}

	/// <summary>Gets an array of objects containing the specified collection.</summary>
	/// <param name="editValue">The collection to edit.</param>
	/// <returns>An array containing the collection objects, or an empty object array if the specified collection does not inherit from <see cref="T:System.Collections.ICollection" />.</returns>
	protected virtual object[] GetItems(object editValue)
	{
		if (editValue == null)
		{
			return new object[0];
		}
		if (!(editValue is ICollection collection))
		{
			return new object[0];
		}
		object[] array = new object[collection.Count];
		collection.CopyTo(array, 0);
		return array;
	}

	/// <summary>Returns a list containing the given object</summary>
	/// <param name="instance">An <see cref="T:System.Collections.ArrayList" /> returned as an object.</param>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> which contains the individual objects to be created.</returns>
	protected virtual IList GetObjectsFromInstance(object instance)
	{
		return new ArrayList { instance };
	}

	/// <summary>Gets the requested service, if it is available.</summary>
	/// <param name="serviceType">The type of service to retrieve.</param>
	/// <returns>An instance of the service, or <see langword="null" /> if the service cannot be found.</returns>
	protected object GetService(Type serviceType)
	{
		return context.GetService(serviceType);
	}

	/// <summary>Sets the specified array as the items of the collection.</summary>
	/// <param name="editValue">The collection to edit.</param>
	/// <param name="value">An array of objects to set as the collection items.</param>
	/// <returns>The newly created collection object or, otherwise, the collection indicated by the <paramref name="editValue" /> parameter.</returns>
	protected virtual object SetItems(object editValue, object[] value)
	{
		IList list = (IList)editValue;
		if (list == null)
		{
			return null;
		}
		list.Clear();
		foreach (object value2 in value)
		{
			list.Add(value2);
		}
		return list;
	}

	/// <summary>Displays the default Help topic for the collection editor.</summary>
	protected virtual void ShowHelp()
	{
		Help.ShowHelp(null, "", HelpTopic);
	}
}
