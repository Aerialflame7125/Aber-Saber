using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Displays a <see cref="T:System.Windows.Forms.ListBox" /> in which a check box is displayed to the left of each item.</summary>
/// <filterpriority>2</filterpriority>
[LookupBindingProperties]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class CheckedListBox : ListBox
{
	/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
	public new class ObjectCollection : ListBox.ObjectCollection
	{
		private CheckedListBox owner;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.CheckedListBox" /> that owns the collection. </param>
		public ObjectCollection(CheckedListBox owner)
			: base(owner)
		{
			this.owner = owner;
		}

		/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.CheckedListBox" />, specifying the object to add and whether it is checked.</summary>
		/// <returns>The index of the newly added item.</returns>
		/// <param name="item">An object representing the item to add to the collection. </param>
		/// <param name="isChecked">true to check the item; otherwise, false. </param>
		public int Add(object item, bool isChecked)
		{
			return Add(item, isChecked ? CheckState.Checked : CheckState.Unchecked);
		}

		/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.CheckedListBox" />, specifying the object to add and the initial checked value.</summary>
		/// <returns>The index of the newly added item.</returns>
		/// <param name="item">An object representing the item to add to the collection. </param>
		/// <param name="check">The initial <see cref="T:System.Windows.Forms.CheckState" /> for the checked portion of the item. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="check" /> parameter is not one of the valid <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
		public int Add(object item, CheckState check)
		{
			int num = Add(item);
			ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(num, check, CheckState.Unchecked);
			if (check == CheckState.Checked)
			{
				owner.OnItemCheck(itemCheckEventArgs);
			}
			if (itemCheckEventArgs.NewValue != 0)
			{
				owner.check_states[item] = itemCheckEventArgs.NewValue;
			}
			owner.UpdateCollections();
			return num;
		}
	}

	/// <summary>Encapsulates the collection of indexes of checked items (including items in an indeterminate state) in a <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
	public class CheckedIndexCollection : ICollection, IEnumerable, IList
	{
		private CheckedListBox owner;

		private ArrayList indices = new ArrayList();

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		bool ICollection.IsSynchronized => false;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true in all cases.</returns>
		bool IList.IsFixedSize => true;

		/// <summary>Gets an object that can be used to synchronize access to the collection of controls. For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> used to synchronize to the collection.</returns>
		object ICollection.SyncRoot => this;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The index value from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> that is stored at the specified location.</returns>
		/// <param name="index">The zero-based index of the item to get.</param>
		object IList.this[int index]
		{
			get
			{
				return indices[index];
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of checked items.</summary>
		/// <returns>The number of indexes in the collection.</returns>
		public int Count => indices.Count;

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true in all cases.</returns>
		public bool IsReadOnly => true;

		/// <summary>Gets the index of a checked item in the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
		/// <returns>The index of the checked item. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> class overview.</returns>
		/// <param name="index">An index into the checked indexes collection. This index specifies the index of the checked item you want to retrieve. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> is less than zero.-or- The <paramref name="index" /> is not in the collection. </exception>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				return (int)indices[index];
			}
		}

		internal CheckedIndexCollection(CheckedListBox owner)
		{
			this.owner = owner;
		}

		/// <summary>Adds an item to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Removes all items from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the specified index is located within the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> for the <see cref="T:System.Windows.Forms.CheckedListBox" /> is an item in this collection; otherwise, false.</returns>
		/// <param name="index">The index to locate in the collection.</param>
		bool IList.Contains(object index)
		{
			return Contains((int)index);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>This member is an explicit interface member implementation. It can be used only when the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> instance is cast to an <see cref="T:System.Collections.IList" /> interface.</returns>
		/// <param name="index">The zero-based index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> to locate in this collection.</param>
		int IList.IndexOf(object index)
		{
			return IndexOf((int)index);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The index at which value should be inserted.</param>
		/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>or a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the specified index is located in the collection.</summary>
		/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> is an item in this collection; otherwise, false.</returns>
		/// <param name="index">The index to locate in the collection. </param>
		public bool Contains(int index)
		{
			return indices.Contains(index);
		}

		/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
		/// <param name="dest">The destination array. </param>
		/// <param name="index">The zero-based relative index in <paramref name="dest" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from index to the end of the destination <see cref="T:System.Array" />. </exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <see cref="T:System.Array" />. </exception>
		public void CopyTo(Array dest, int index)
		{
			indices.CopyTo(dest, index);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the <see cref="P:System.Windows.Forms.CheckedListBox.CheckedIndices" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
		public IEnumerator GetEnumerator()
		{
			return indices.GetEnumerator();
		}

		/// <summary>Returns an index into the collection of checked indexes.</summary>
		/// <returns>The index that specifies the index of the checked item or -1 if the <paramref name="index" /> parameter is not in the checked indexes collection. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> class overview.</returns>
		/// <param name="index">The index of the checked item. </param>
		public int IndexOf(int index)
		{
			return indices.IndexOf(index);
		}

		internal void Refresh()
		{
			indices.Clear();
			for (int i = 0; i < owner.Items.Count; i++)
			{
				if (owner.check_states.Contains(owner.Items[i]))
				{
					indices.Add(i);
				}
			}
		}
	}

	/// <summary>Encapsulates the collection of checked items, including items in an indeterminate state, in a <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
	public class CheckedItemCollection : ICollection, IEnumerable, IList
	{
		private CheckedListBox owner;

		private ArrayList list = new ArrayList();

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>false in all cases.</returns>
		bool ICollection.IsSynchronized => true;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> used to synchronize to the collection.</returns>
		object ICollection.SyncRoot => this;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => true;

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		public int Count => list.Count;

		/// <summary>Gets a value indicating if the collection is read-only.</summary>
		/// <returns>Always true.</returns>
		public bool IsReadOnly => true;

		/// <summary>Gets an object in the checked items collection.</summary>
		/// <returns>The object at the specified index. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> class overview.</returns>
		/// <param name="index">An index into the collection of checked items. This collection index corresponds to the index of the checked item. </param>
		/// <exception cref="T:System.NotSupportedException">The object cannot be set.</exception>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				return list[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		internal CheckedItemCollection(CheckedListBox owner)
		{
			this.owner = owner;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The zero-based index of the item to add.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The item to insert into the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" />.</param>
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The item to remove from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" />.</param>
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the specified item is located in the collection.</summary>
		/// <returns>true if item is in the collection; otherwise, false.</returns>
		/// <param name="item">An object from the items collection. </param>
		public bool Contains(object item)
		{
			return list.Contains(item);
		}

		/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
		/// <param name="dest">The destination array. </param>
		/// <param name="index">The zero-based relative index in <paramref name="dest" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.RankException">
		///   <paramref name="array" /> is multidimensional. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from index to the end of the destination <see cref="T:System.Array" />. </exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <see cref="T:System.Array" />. </exception>
		public void CopyTo(Array dest, int index)
		{
			list.CopyTo(dest, index);
		}

		/// <summary>Returns an index into the collection of checked items.</summary>
		/// <returns>The index of the object in the checked item collection or -1 if the object is not in the collection. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> class overview.</returns>
		/// <param name="item">The object whose index you want to retrieve. This object must belong to the checked items collection. </param>
		public int IndexOf(object item)
		{
			return list.IndexOf(item);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the <see cref="P:System.Windows.Forms.CheckedListBox.CheckedItems" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		internal void Refresh()
		{
			list.Clear();
			for (int i = 0; i < owner.Items.Count; i++)
			{
				if (owner.check_states.Contains(owner.Items[i]))
				{
					list.Add(owner.Items[i]);
				}
			}
		}
	}

	private CheckedIndexCollection checked_indices;

	private CheckedItemCollection checked_items;

	private Hashtable check_states = new Hashtable();

	private bool check_onclick;

	private bool three_dcheckboxes;

	private static object ItemCheckEvent;

	private int last_clicked_index = -1;

	/// <summary>Collection of checked indexes in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> collection for the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public CheckedIndexCollection CheckedIndices => checked_indices;

	/// <summary>Collection of checked items in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> collection for the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public CheckedItemCollection CheckedItems => checked_items;

	/// <summary>Gets or sets a value indicating whether the check box should be toggled when an item is selected.</summary>
	/// <returns>true if the check mark is applied immediately; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool CheckOnClick
	{
		get
		{
			return check_onclick;
		}
		set
		{
			check_onclick = value;
		}
	}

	/// <summary>Gets the required creation parameters when the control handle is created.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required parameters.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets or sets the data source for the control. This property is not relevant for this class.</summary>
	/// <returns>An object representing the source of the data.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new object DataSource
	{
		get
		{
			return base.DataSource;
		}
		set
		{
			base.DataSource = value;
		}
	}

	/// <summary>Gets or sets a string that specifies a property of the objects contained in the list box whose contents you want to display.</summary>
	/// <returns>A string that specifies the name of a property of the objects contained in the list box. The default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new string DisplayMember
	{
		get
		{
			return base.DisplayMember;
		}
		set
		{
			base.DisplayMember = value;
		}
	}

	/// <summary>Gets a value indicating the mode for drawing elements of the <see cref="T:System.Windows.Forms.CheckedListBox" />. This property is not relevant to this class.</summary>
	/// <returns>Always a <see cref="T:System.Windows.Forms.DrawMode" /> of Normal.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override DrawMode DrawMode
	{
		get
		{
			return DrawMode.Normal;
		}
		set
		{
		}
	}

	/// <summary>Gets the height of the item area.</summary>
	/// <returns>The height, in pixels, of the item area.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override int ItemHeight
	{
		get
		{
			return base.ItemHeight;
		}
		set
		{
		}
	}

	/// <summary>Gets the collection of items in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> collection representing the items in the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public new ObjectCollection Items => (ObjectCollection)base.Items;

	/// <summary>Gets or sets a value specifying the selection mode.</summary>
	/// <returns>Either the One or None value of <see cref="T:System.Windows.Forms.SelectionMode" />.</returns>
	/// <exception cref="T:System.ArgumentException">An attempt was made to assign a value that is not a <see cref="T:System.Windows.Forms.SelectionMode" /> value of One or None. </exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An attempt was made to assign the MultiExtended value of <see cref="T:System.Windows.Forms.SelectionMode" /> to the control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override SelectionMode SelectionMode
	{
		get
		{
			return base.SelectionMode;
		}
		set
		{
			if (!Enum.IsDefined(typeof(SelectionMode), value))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(SelectionMode));
			}
			if (value == SelectionMode.MultiSimple || value == SelectionMode.MultiExtended)
			{
				throw new ArgumentException("Multi selection not supported on CheckedListBox");
			}
			base.SelectionMode = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the check boxes have a <see cref="T:System.Windows.Forms.ButtonState" /> of Flat or Normal.</summary>
	/// <returns>true if the check box has a flat appearance; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool ThreeDCheckBoxes
	{
		get
		{
			return three_dcheckboxes;
		}
		set
		{
			if (three_dcheckboxes != value)
			{
				three_dcheckboxes = value;
				Refresh();
			}
		}
	}

	/// <summary>Gets or sets a string that specifies the property of the data source from which to draw the value. This property is not relevant to this class.</summary>
	/// <returns>A string that specifies the property of the data source from which to draw the value.</returns>
	/// <exception cref="T:System.ArgumentException">The specified property cannot be found on the object specified by the <see cref="P:System.Windows.Forms.CheckedListBox.DataSource" /> property.</exception>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new string ValueMember
	{
		get
		{
			return base.ValueMember;
		}
		set
		{
			base.ValueMember = value;
		}
	}

	/// <summary>Gets or sets padding within the <see cref="T:System.Windows.Forms.CheckedListBox" />. This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			base.Padding = value;
		}
	}

	/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
	/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool UseCompatibleTextRendering
	{
		get
		{
			return use_compatible_text_rendering;
		}
		set
		{
			use_compatible_text_rendering = value;
		}
	}

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public new event EventHandler Click
	{
		add
		{
			base.Click += value;
		}
		remove
		{
			base.Click -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.DataSource" /> property changes. This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DataSourceChanged
	{
		add
		{
			base.DataSourceChanged += value;
		}
		remove
		{
			base.DataSourceChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.DisplayMember" /> property changes. This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DisplayMemberChanged
	{
		add
		{
			base.DisplayMemberChanged += value;
		}
		remove
		{
			base.DisplayMemberChanged -= value;
		}
	}

	/// <summary>Occurs when a visual aspect of an owner-drawn <see cref="T:System.Windows.Forms.CheckedListBox" /> changes. This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event DrawItemEventHandler DrawItem
	{
		add
		{
			base.DrawItem += value;
		}
		remove
		{
			base.DrawItem -= value;
		}
	}

	/// <summary>Occurs when an owner-drawn <see cref="T:System.Windows.Forms.ListBox" /> is created and the sizes of the list items are determined. This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MeasureItemEventHandler MeasureItem
	{
		add
		{
			base.MeasureItem += value;
		}
		remove
		{
			base.MeasureItem -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.ValueMember" /> property changes. This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler ValueMemberChanged
	{
		add
		{
			base.ValueMemberChanged += value;
		}
		remove
		{
			base.ValueMemberChanged -= value;
		}
	}

	/// <summary>Occurs when the checked state of an item changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event ItemCheckEventHandler ItemCheck
	{
		add
		{
			base.Events.AddHandler(ItemCheckEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemCheckEvent, value);
		}
	}

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.CheckedListBox" /> control with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public new event MouseEventHandler MouseClick
	{
		add
		{
			base.MouseClick += value;
		}
		remove
		{
			base.MouseClick -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckedListBox" /> class.</summary>
	public CheckedListBox()
	{
		checked_indices = new CheckedIndexCollection(this);
		checked_items = new CheckedItemCollection(this);
		SetStyle(ControlStyles.ResizeRedraw, value: true);
	}

	static CheckedListBox()
	{
		ItemCheck = new object();
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return base.CreateAccessibilityInstance();
	}

	/// <returns>A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that represents the new item collection.</returns>
	protected override ListBox.ObjectCollection CreateItemCollection()
	{
		return new ObjectCollection(this);
	}

	/// <summary>Returns a value indicating whether the specified item is checked.</summary>
	/// <returns>true if the item is checked; otherwise, false.</returns>
	/// <param name="index">The index of the item. </param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> specified is less than zero.-or- The <paramref name="index" /> specified is greater than or equal to the count of items in the list. </exception>
	/// <filterpriority>1</filterpriority>
	public bool GetItemChecked(int index)
	{
		return check_states.Contains(Items[index]);
	}

	/// <summary>Returns a value indicating the check state of the current item.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
	/// <param name="index">The index of the item to get the checked value of. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is less than zero.-or- The <paramref name="index" /> specified is greater than or equal to the count of items in the list. </exception>
	/// <filterpriority>1</filterpriority>
	public CheckState GetItemCheckState(int index)
	{
		if (index < 0 || index >= Items.Count)
		{
			throw new ArgumentOutOfRangeException("Index of out range");
		}
		object key = Items[index];
		if (check_states.Contains(key))
		{
			return (CheckState)(int)check_states[key];
		}
		return CheckState.Unchecked;
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		base.OnBackColorChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnClick(EventArgs e)
	{
		base.OnClick(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.DrawItem" /> event.</summary>
	/// <param name="e">The <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> object with the details </param>
	protected override void OnDrawItem(DrawItemEventArgs e)
	{
		if (check_states.Contains(Items[e.Index]))
		{
			DrawItemState drawItemState = e.State | DrawItemState.Checked;
			if ((int)check_states[Items[e.Index]] == 2)
			{
				drawItemState |= DrawItemState.Inactive;
			}
			e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, drawItemState, e.ForeColor, e.BackColor);
		}
		ThemeEngine.Current.DrawCheckedListBoxItem(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.ItemCheck" /> event.</summary>
	/// <param name="ice">An <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> that contains the event data.</param>
	protected virtual void OnItemCheck(ItemCheckEventArgs ice)
	{
		((ItemCheckEventHandler)base.Events[ItemCheck])?.Invoke(this, ice);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	/// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that was raised. </param>
	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
		if (e.KeyChar == ' ' && base.FocusedItem != -1)
		{
			SetItemChecked(base.FocusedItem, !GetItemChecked(base.FocusedItem));
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.MeasureItem" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data. </param>
	protected override void OnMeasureItem(MeasureItemEventArgs e)
	{
		base.OnMeasureItem(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListBox.SelectedIndexChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnSelectedIndexChanged(EventArgs e)
	{
		base.OnSelectedIndexChanged(e);
	}

	/// <summary>Parses all <see cref="T:System.Windows.Forms.CheckedListBox" /> items again and gets new text strings for the items.</summary>
	protected override void RefreshItems()
	{
		base.RefreshItems();
	}

	/// <summary>Sets <see cref="T:System.Windows.Forms.CheckState" /> for the item at the specified index to Checked.</summary>
	/// <param name="index">The index of the item to set the check state for. </param>
	/// <param name="value">true to set the item as checked; otherwise, false. </param>
	/// <exception cref="T:System.ArgumentException">The index specified is less than zero.-or- The index is greater than the count of items in the list. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetItemChecked(int index, bool value)
	{
		SetItemCheckState(index, value ? CheckState.Checked : CheckState.Unchecked);
	}

	/// <summary>Sets the check state of the item at the specified index.</summary>
	/// <param name="index">The index of the item to set the state for. </param>
	/// <param name="value">One of the <see cref="T:System.Windows.Forms.CheckState" /> values. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is less than zero.-or- The <paramref name="index" /> is greater than or equal to the count of items in the list. </exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="value" /> is not one of the <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetItemCheckState(int index, CheckState value)
	{
		if (index < 0 || index >= Items.Count)
		{
			throw new ArgumentOutOfRangeException("Index of out range");
		}
		if (!Enum.IsDefined(typeof(CheckState), value))
		{
			throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for CheckState");
		}
		CheckState itemCheckState = GetItemCheckState(index);
		if (itemCheckState != value)
		{
			ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(index, value, itemCheckState);
			OnItemCheck(itemCheckEventArgs);
			switch (itemCheckEventArgs.NewValue)
			{
			case CheckState.Checked:
			case CheckState.Indeterminate:
				check_states[Items[index]] = itemCheckEventArgs.NewValue;
				break;
			case CheckState.Unchecked:
				check_states.Remove(Items[index]);
				break;
			}
			UpdateCollections();
			InvalidateCheckbox(index);
		}
	}

	/// <summary>Processes the command message the <see cref="T:System.Windows.Forms.CheckedListBox" /> control receives from the top-level window.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> the top-level window sent to the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</param>
	protected override void WmReflectCommand(ref Message m)
	{
		base.WmReflectCommand(ref m);
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal override void OnItemClick(int index)
	{
		if ((CheckOnClick || last_clicked_index == index) && index > -1)
		{
			if (GetItemChecked(index))
			{
				SetItemCheckState(index, CheckState.Unchecked);
			}
			else
			{
				SetItemCheckState(index, CheckState.Checked);
			}
		}
		last_clicked_index = index;
		base.OnItemClick(index);
	}

	internal override void CollectionChanged()
	{
		base.CollectionChanged();
		UpdateCollections();
	}

	private void InvalidateCheckbox(int index)
	{
		Rectangle itemDisplayRectangle = GetItemDisplayRectangle(index, base.TopIndex);
		itemDisplayRectangle.X += 2;
		itemDisplayRectangle.Y += (itemDisplayRectangle.Height - 11) / 2;
		itemDisplayRectangle.Width = 11;
		itemDisplayRectangle.Height = 11;
		Invalidate(itemDisplayRectangle);
	}

	private void UpdateCollections()
	{
		CheckedItems.Refresh();
		CheckedIndices.Refresh();
	}
}
