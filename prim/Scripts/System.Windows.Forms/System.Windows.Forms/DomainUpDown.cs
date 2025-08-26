using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows spin box (also known as an up-down control) that displays string values.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultProperty("Items")]
[DefaultEvent("SelectedItemChanged")]
[ComVisible(true)]
[DefaultBindingProperty("SelectedItem")]
public class DomainUpDown : UpDownBase
{
	/// <summary>Provides information about the items in the <see cref="T:System.Windows.Forms.DomainUpDown" /> control to accessibility client applications.</summary>
	[ComVisible(true)]
	public class DomainItemAccessibleObject : AccessibleObject
	{
		private AccessibleObject parent;

		/// <summary>Gets or sets the object name.</summary>
		/// <returns>The object name, or null if the property has not been set.</returns>
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		/// <summary>Gets the parent of an accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or null if there is no parent object.</returns>
		public override AccessibleObject Parent => parent;

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.ListItem" /> value.</returns>
		public override AccessibleRole Role => base.Role;

		/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <returns>If the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property is set to true, returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />.</returns>
		public override AccessibleStates State => base.State;

		/// <summary>Gets the value of an accessible object.</summary>
		/// <returns>The Name property of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" />.</returns>
		public override string Value => base.Value;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" /> class.</summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" />.</param>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.AccessibleObject" /> that contains the items in the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</param>
		public DomainItemAccessibleObject(string name, AccessibleObject parent)
		{
			base.name = name;
			this.parent = parent;
		}
	}

	/// <summary>Provides information about the <see cref="T:System.Windows.Forms.DomainUpDown" /> control to accessibility client applications.</summary>
	[ComVisible(true)]
	public class DomainUpDownAccessibleObject : ControlAccessibleObject
	{
		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.ComboBox" /> value.</returns>
		public override AccessibleRole Role => base.Role;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject" /> class. </summary>
		public DomainUpDownAccessibleObject(Control owner)
			: base(owner)
		{
		}

		/// <summary>Gets the accessible child corresponding to the specified index.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
		/// <param name="index">The zero-based index of the accessible child.</param>
		public override AccessibleObject GetChild(int index)
		{
			return base.GetChild(index);
		}

		/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
		/// <returns>Returns 3 in all cases.</returns>
		public override int GetChildCount()
		{
			return base.GetChildCount();
		}
	}

	/// <summary>Encapsulates a collection of objects for use by the <see cref="T:System.Windows.Forms.DomainUpDown" /> class.</summary>
	public class DomainUpDownItemCollection : ArrayList
	{
		private class ToStringSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				return string.Compare(x.ToString(), y.ToString());
			}
		}

		/// <summary>Gets or sets the item at the specified indexed location in the collection.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the item at the specified indexed location.</returns>
		/// <param name="index">The indexed location of the item in the collection. </param>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override object this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", "Cannot add null values to a DomainUpDownItemCollection");
				}
				base[index] = value;
				OnCollectionChanged(index, 0);
			}
		}

		internal event CollectionChangedEventHandler CollectionChanged;

		internal DomainUpDownItemCollection()
		{
		}

		/// <summary>Adds the specified object to the end of the collection.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Object" /> added to the collection.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to be added to the end of the collection. </param>
		public override int Add(object item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("value", "Cannot add null values to a DomainUpDownItemCollection");
			}
			int result = base.Add(item);
			OnCollectionChanged(Count - 1, 1);
			return result;
		}

		/// <summary>Inserts the specified object into the collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the <see cref="T:System.Object" />. </param>
		/// <param name="item">The <see cref="T:System.Object" /> to insert. </param>
		public override void Insert(int index, object item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("value", "Cannot add null values to a DomainUpDownItemCollection");
			}
			base.Insert(index, item);
			OnCollectionChanged(index, 1);
		}

		/// <summary>Removes the specified item from the collection.</summary>
		/// <param name="item">The <see cref="T:System.Object" /> to remove from the collection. </param>
		public override void Remove(object item)
		{
			int num = IndexOf(item);
			if (num >= 0)
			{
				RemoveAt(num);
			}
		}

		/// <summary>Removes the item from the specified location in the collection.</summary>
		/// <param name="item">The indexed location of the <see cref="T:System.Object" /> in the collection. </param>
		public override void RemoveAt(int item)
		{
			base.RemoveAt(item);
			OnCollectionChanged(item, -1);
		}

		internal void OnCollectionChanged(int index, int size_delta)
		{
			this.CollectionChanged?.Invoke(index, size_delta);
		}

		internal void PrivSort()
		{
			base.Sort(new ToStringSorter());
		}
	}

	internal delegate void CollectionChangedEventHandler(int index, int size_delta);

	private DomainUpDownItemCollection items;

	private int selected_index = -1;

	private bool sorted;

	private bool wrap;

	private int typed_to_index = -1;

	private static object SelectedItemChangedEvent;

	/// <summary>A collection of objects assigned to the spin box (also known as an up-down control).</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownItemCollection" /> that contains an <see cref="T:System.Object" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Localizable(true)]
	public DomainUpDownItemCollection Items => items;

	/// <summary>Gets or sets the spacing between the <see cref="T:System.Windows.Forms.DomainUpDown" /> control's contents and its edges.</summary>
	/// <returns>
	///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Padding Padding
	{
		get
		{
			return Padding.Empty;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the index value of the selected item.</summary>
	/// <returns>The zero-based index value of the selected item. The default value is -1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the default, -1.-or- The assigned value is greater than the <see cref="P:System.Windows.Forms.DomainUpDown.Items" /> count. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue(-1)]
	public int SelectedIndex
	{
		get
		{
			return selected_index;
		}
		set
		{
			object objA = ((selected_index < 0) ? null : items[selected_index]);
			selected_index = value;
			UpdateEditText();
			object objB = ((selected_index < 0) ? null : items[selected_index]);
			if (!object.ReferenceEquals(objA, objB))
			{
				OnSelectedItemChanged(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the selected item based on the index value of the selected item in the collection.</summary>
	/// <returns>The selected item based on the <see cref="P:System.Windows.Forms.DomainUpDown.SelectedIndex" /> value. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public object SelectedItem
	{
		get
		{
			if (selected_index >= 0)
			{
				return items[selected_index];
			}
			return null;
		}
		set
		{
			SelectedIndex = items.IndexOf(value);
		}
	}

	/// <summary>Gets or sets a value indicating whether the item collection is sorted.</summary>
	/// <returns>true if the item collection is sorted; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Sorted
	{
		get
		{
			return sorted;
		}
		set
		{
			sorted = value;
			if (sorted)
			{
				items.PrivSort();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the collection of items continues to the first or last item if the user continues past the end of the list.</summary>
	/// <returns>true if the list starts again when the user reaches the beginning or end of the collection; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(false)]
	public bool Wrap
	{
		get
		{
			return wrap;
		}
		set
		{
			wrap = value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DomainUpDown.Padding" /> property changes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler PaddingChanged
	{
		add
		{
			base.PaddingChanged += value;
		}
		remove
		{
			base.PaddingChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DomainUpDown.SelectedItem" /> property has been changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SelectedItemChanged
	{
		add
		{
			base.Events.AddHandler(SelectedItemChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectedItemChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown" /> class.</summary>
	public DomainUpDown()
	{
		selected_index = -1;
		sorted = false;
		wrap = false;
		typed_to_index = -1;
		items = new DomainUpDownItemCollection();
		items.CollectionChanged += items_CollectionChanged;
		txtView.LostFocus += TextBoxLostFocus;
		txtView.KeyPress += TextBoxKeyDown;
		UpdateEditText();
	}

	static DomainUpDown()
	{
		SelectedItemChanged = new object();
	}

	internal void items_CollectionChanged(int index, int size_delta)
	{
		bool flag = false;
		if (index == selected_index && size_delta <= 0)
		{
			flag = true;
		}
		else if (index <= selected_index)
		{
			selected_index += size_delta;
		}
		if (sorted && index >= 0)
		{
			items.PrivSort();
		}
		UpdateEditText();
		if (flag)
		{
			OnSelectedItemChanged(this, EventArgs.Empty);
		}
	}

	private void go_to_user_input()
	{
		base.UserEdit = false;
		if (typed_to_index >= 0)
		{
			selected_index = typed_to_index;
			OnSelectedItemChanged(this, EventArgs.Empty);
		}
	}

	private void TextBoxLostFocus(object source, EventArgs e)
	{
		Select(txtView.SelectionStart + txtView.SelectionLength, 0);
	}

	private int SearchTextWithPrefix(char key_char)
	{
		string strA = key_char.ToString();
		int num = ((selected_index != -1) ? selected_index : 0);
		int num2 = ((selected_index != -1 && selected_index + 1 < items.Count) ? (num + 1) : 0);
		do
		{
			string strB = items[num2].ToString();
			if (string.Compare(strA, 0, strB, 0, 1, ignoreCase: true) == 0)
			{
				return num2;
			}
			num2 = ((num2 + 1 < items.Count) ? (num2 + 1) : 0);
		}
		while (num2 != num);
		return -1;
	}

	private bool IsValidInput(char key_char)
	{
		return char.IsLetterOrDigit(key_char) || char.IsNumber(key_char) || char.IsPunctuation(key_char) || char.IsSymbol(key_char) || char.IsWhiteSpace(key_char);
	}

	private void TextBoxKeyDown(object source, KeyPressEventArgs e)
	{
		if (base.ReadOnly)
		{
			char keyChar = e.KeyChar;
			if (IsValidInput(keyChar) && items.Count > 0)
			{
				int num = SearchTextWithPrefix(keyChar);
				if (num > -1)
				{
					SelectedIndex = num;
					e.Handled = true;
				}
			}
			return;
		}
		if (!base.UserEdit)
		{
			txtView.SelectionLength = 0;
			typed_to_index = -1;
		}
		if (txtView.SelectionLength == 0)
		{
			txtView.SelectionStart = 0;
		}
		if (txtView.SelectionStart != 0)
		{
			return;
		}
		if (e.KeyChar == '\b')
		{
			if (txtView.SelectionLength <= 0)
			{
				return;
			}
			string text = txtView.SelectedText.Substring(0, txtView.SelectionLength - 1);
			bool flag = false;
			if (typed_to_index < 0)
			{
				typed_to_index = 0;
			}
			if (sorted)
			{
				for (int num2 = typed_to_index; num2 >= 0; num2--)
				{
					int num3 = string.Compare(text, 0, items[num2].ToString(), 0, text.Length, ignoreCase: true);
					if (num3 == 0)
					{
						flag = true;
						typed_to_index = num2;
					}
					if (num3 > 0)
					{
						break;
					}
				}
			}
			else
			{
				for (int i = 0; i < items.Count; i++)
				{
					if (string.Compare(text, 0, items[i].ToString(), 0, text.Length, ignoreCase: true) == 0)
					{
						flag = true;
						typed_to_index = i;
						break;
					}
				}
			}
			base.ChangingText = true;
			if (flag)
			{
				Text = items[typed_to_index].ToString();
			}
			else
			{
				Text = text;
			}
			Select(0, text.Length);
			base.UserEdit = true;
			e.Handled = true;
			return;
		}
		char keyChar2 = e.KeyChar;
		if (!IsValidInput(keyChar2))
		{
			return;
		}
		string text2 = txtView.SelectedText + keyChar2;
		bool flag2 = false;
		if (typed_to_index < 0)
		{
			typed_to_index = 0;
		}
		if (sorted)
		{
			for (int j = typed_to_index; j < items.Count; j++)
			{
				int num4 = string.Compare(text2, 0, items[j].ToString(), 0, text2.Length, ignoreCase: true);
				if (num4 == 0)
				{
					flag2 = true;
					typed_to_index = j;
				}
				if (num4 <= 0)
				{
					break;
				}
			}
		}
		else
		{
			for (int k = 0; k < items.Count; k++)
			{
				if (string.Compare(text2, 0, items[k].ToString(), 0, text2.Length, ignoreCase: true) == 0)
				{
					flag2 = true;
					typed_to_index = k;
					break;
				}
			}
		}
		base.ChangingText = true;
		if (flag2)
		{
			Text = items[typed_to_index].ToString();
		}
		else
		{
			Text = text2;
		}
		Select(0, text2.Length);
		base.UserEdit = true;
		e.Handled = true;
	}

	/// <summary>Displays the next item in the object collection.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void DownButton()
	{
		if (base.UserEdit)
		{
			go_to_user_input();
		}
		int num = selected_index + 1;
		if (num >= items.Count)
		{
			if (!wrap)
			{
				return;
			}
			num = 0;
		}
		SelectedIndex = num;
		OnUIADownButtonClick(EventArgs.Empty);
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.DomainUpDown" />. </returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Items.Count: " + items.Count + ", SelectedIndex: " + selected_index;
	}

	/// <summary>Displays the previous item in the collection.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void UpButton()
	{
		if (base.UserEdit)
		{
			go_to_user_input();
		}
		int num = selected_index - 1;
		if (num < 0)
		{
			if (!wrap)
			{
				return;
			}
			num = items.Count - 1;
		}
		SelectedIndex = num;
		OnUIAUpButtonClick(EventArgs.Empty);
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		AccessibleObject accessibleObject = new AccessibleObject(this);
		accessibleObject.role = AccessibleRole.SpinButton;
		return accessibleObject;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnChanged(object source, EventArgs e)
	{
		base.OnChanged(source, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected void OnSelectedItemChanged(object source, EventArgs e)
	{
		((EventHandler)base.Events[SelectedItemChanged])?.Invoke(this, e);
	}

	/// <summary>Updates the text in the spin box (also known as an up-down control) to display the selected item.</summary>
	protected override void UpdateEditText()
	{
		if (selected_index >= 0 && selected_index < items.Count)
		{
			base.ChangingText = true;
			Text = items[selected_index].ToString();
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event. </summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
	protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
	{
		base.OnTextBoxKeyPress(source, e);
	}
}
