using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents the navigation and manipulation user interface (UI) for controls on a form that are bound to data.</summary>
[Designer("System.Windows.Forms.Design.BindingNavigatorDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultProperty("BindingSource")]
[DefaultEvent("RefreshItems")]
[ComVisible(true)]
public class BindingNavigator : ToolStrip, ISupportInitialize
{
	private ToolStripItem addNewItem;

	private BindingSource bindingSource;

	private ToolStripItem countItem;

	private string countItemFormat = Locale.GetText("of {0}");

	private ToolStripItem deleteItem;

	private bool initFlag;

	private ToolStripItem moveFirstItem;

	private ToolStripItem moveLastItem;

	private ToolStripItem moveNextItem;

	private ToolStripItem movePreviousItem;

	private ToolStripItem positionItem;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Add New button.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Add New button for the <see cref="T:System.Windows.Forms.BindingSource" />. The default is null.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem AddNewItem
	{
		get
		{
			return addNewItem;
		}
		set
		{
			ReplaceItem(ref addNewItem, value, OnAddNew);
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingSource" /> component that is the source of data.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.BindingSource" /> component associated with this <see cref="T:System.Windows.Forms.BindingNavigator" />. The default is null.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	[DefaultValue(null)]
	public BindingSource BindingSource
	{
		get
		{
			return bindingSource;
		}
		set
		{
			AttachNewSource(value);
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the total number of items in the associated <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the total number of items in the associated <see cref="T:System.Windows.Forms.BindingSource" />. </returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem CountItem
	{
		get
		{
			return countItem;
		}
		set
		{
			countItem = value;
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets a string used to format the information displayed in the <see cref="P:System.Windows.Forms.BindingNavigator.CountItem" /> control. </summary>
	/// <returns>The format <see cref="T:System.String" /> used to format the item count. The default is the string "of {0}".</returns>
	public string CountItemFormat
	{
		get
		{
			return countItemFormat;
		}
		set
		{
			countItemFormat = value;
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Delete functionality.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Delete button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem DeleteItem
	{
		get
		{
			return deleteItem;
		}
		set
		{
			ReplaceItem(ref deleteItem, value, OnDelete);
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move First functionality.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move First button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem MoveFirstItem
	{
		get
		{
			return moveFirstItem;
		}
		set
		{
			ReplaceItem(ref moveFirstItem, value, OnMoveFirst);
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Last functionality.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Last button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem MoveLastItem
	{
		get
		{
			return moveLastItem;
		}
		set
		{
			ReplaceItem(ref moveLastItem, value, OnMoveLast);
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Next functionality.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Next button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem MoveNextItem
	{
		get
		{
			return moveNextItem;
		}
		set
		{
			ReplaceItem(ref moveNextItem, value, OnMoveNext);
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is associated with the Move Previous functionality.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that represents the Move Previous button for the <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem MovePreviousItem
	{
		get
		{
			return movePreviousItem;
		}
		set
		{
			ReplaceItem(ref movePreviousItem, value, OnMovePrevious);
			OnRefreshItems();
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the current position within the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the current position.</returns>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripItem PositionItem
	{
		get
		{
			return positionItem;
		}
		set
		{
			positionItem = value;
			OnRefreshItems();
		}
	}

	/// <summary>Occurs when the state of the navigational user interface (UI) needs to be refreshed to reflect the current state of the underlying data.</summary>
	public event EventHandler RefreshItems;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public BindingNavigator()
		: this(addStandardItems: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class with the specified <see cref="T:System.Windows.Forms.BindingSource" /> as the data source.</summary>
	/// <param name="bindingSource">The <see cref="T:System.Windows.Forms.BindingSource" /> used as a data source.</param>
	public BindingNavigator(BindingSource bindingSource)
	{
		AttachNewSource(bindingSource);
		AddStandardItems();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class, indicating whether to display the standard navigation user interface (UI).</summary>
	/// <param name="addStandardItems">true to show the standard navigational UI; otherwise, false.</param>
	public BindingNavigator(bool addStandardItems)
	{
		bindingSource = null;
		if (addStandardItems)
		{
			AddStandardItems();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingNavigator" /> class and adds this new instance to the specified container.</summary>
	/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to add the new <see cref="T:System.Windows.Forms.BindingNavigator" /> control to.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public BindingNavigator(IContainer container)
	{
		bindingSource = null;
		container.Add(this);
	}

	private void ReplaceItem(ref ToolStripItem existingItem, ToolStripItem newItem, EventHandler clickHandler)
	{
		if (existingItem != null)
		{
			existingItem.Click -= clickHandler;
		}
		if (newItem != null)
		{
			newItem.Click += clickHandler;
		}
		existingItem = newItem;
	}

	/// <summary>Adds the standard set of navigation items to the <see cref="T:System.Windows.Forms.BindingNavigator" /> control.</summary>
	public virtual void AddStandardItems()
	{
		BeginInit();
		MoveFirstItem = new ToolStripButton();
		moveFirstItem.Image = ResourceImageLoader.Get("nav_first.png");
		moveFirstItem.ToolTipText = Locale.GetText("Move first");
		Items.Add(moveFirstItem);
		MovePreviousItem = new ToolStripButton();
		movePreviousItem.Image = ResourceImageLoader.Get("nav_previous.png");
		movePreviousItem.ToolTipText = Locale.GetText("Move previous");
		Items.Add(movePreviousItem);
		Items.Add(new ToolStripSeparator());
		PositionItem = new ToolStripTextBox();
		positionItem.Width = 50;
		positionItem.Text = ((bindingSource != null) ? 1 : 0).ToString();
		positionItem.Width = 50;
		positionItem.ToolTipText = Locale.GetText("Current position");
		Items.Add(positionItem);
		CountItem = new ToolStripLabel();
		countItem.ToolTipText = Locale.GetText("Total number of items");
		countItem.Text = Locale.GetText(countItemFormat, (bindingSource != null) ? bindingSource.Count : 0);
		Items.Add(countItem);
		Items.Add(new ToolStripSeparator());
		MoveNextItem = new ToolStripButton();
		moveNextItem.Image = ResourceImageLoader.Get("nav_next.png");
		moveNextItem.ToolTipText = Locale.GetText("Move next");
		Items.Add(moveNextItem);
		MoveLastItem = new ToolStripButton();
		moveLastItem.Image = ResourceImageLoader.Get("nav_end.png");
		moveLastItem.ToolTipText = Locale.GetText("Move last");
		Items.Add(moveLastItem);
		Items.Add(new ToolStripSeparator());
		AddNewItem = new ToolStripButton();
		addNewItem.Image = ResourceImageLoader.Get("nav_plus.png");
		addNewItem.ToolTipText = Locale.GetText("Add new");
		Items.Add(addNewItem);
		DeleteItem = new ToolStripButton();
		deleteItem.Image = ResourceImageLoader.Get("nav_delete.png");
		deleteItem.ToolTipText = Locale.GetText("Delete");
		Items.Add(deleteItem);
		EndInit();
	}

	/// <summary>Disables updates to the <see cref="T:System.Windows.Forms.ToolStripItem" /> controls of the <see cref="T:System.Windows.Forms.BindingNavigator" /> during the component's initialization.</summary>
	public void BeginInit()
	{
		initFlag = true;
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.BindingNavigator" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Enables updates to the <see cref="T:System.Windows.Forms.ToolStripItem" /> controls of the <see cref="T:System.Windows.Forms.BindingNavigator" /> after the component's initialization has concluded.</summary>
	public void EndInit()
	{
		initFlag = false;
		OnRefreshItems();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingNavigator.RefreshItems" /> event.</summary>
	protected virtual void OnRefreshItems()
	{
		if (!initFlag)
		{
			if (this.RefreshItems != null)
			{
				this.RefreshItems(this, EventArgs.Empty);
			}
			RefreshItemsCore();
		}
	}

	/// <summary>Refreshes the state of the standard items to reflect the current state of the data.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void RefreshItemsCore()
	{
		try
		{
			bool flag = bindingSource != null;
			initFlag = true;
			if (addNewItem != null)
			{
				addNewItem.Enabled = flag && bindingSource.AllowNew;
			}
			if (moveFirstItem != null)
			{
				moveFirstItem.Enabled = flag && bindingSource.Position > 0;
			}
			if (moveLastItem != null)
			{
				moveLastItem.Enabled = flag && bindingSource.Position < bindingSource.Count - 1;
			}
			if (moveNextItem != null)
			{
				moveNextItem.Enabled = flag && bindingSource.Position < bindingSource.Count - 1;
			}
			if (movePreviousItem != null)
			{
				movePreviousItem.Enabled = flag && bindingSource.Position > 0;
			}
			if (deleteItem != null)
			{
				deleteItem.Enabled = flag && bindingSource.Count != 0 && bindingSource.AllowRemove;
			}
			if (countItem != null)
			{
				countItem.Text = string.Format(countItemFormat, flag ? bindingSource.Count : 0);
				countItem.Enabled = flag && bindingSource.Count > 0;
			}
			if (positionItem != null)
			{
				positionItem.Text = $"{(flag ? (bindingSource.Position + 1) : 0)}";
				positionItem.Enabled = flag && bindingSource.Count > 0;
			}
		}
		finally
		{
			initFlag = false;
		}
	}

	/// <summary>Causes form validation to occur and returns whether validation was successful.</summary>
	/// <returns>true if validation was successful and focus can shift to the <see cref="T:System.Windows.Forms.BindingNavigator" />; otherwise, false.</returns>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	public bool Validate()
	{
		throw new NotImplementedException();
	}

	private void AttachNewSource(BindingSource source)
	{
		if (bindingSource != null)
		{
			bindingSource.ListChanged -= OnListChanged;
			bindingSource.PositionChanged -= OnPositionChanged;
			bindingSource.AddingNew -= OnAddingNew;
		}
		bindingSource = source;
		if (bindingSource != null)
		{
			bindingSource.ListChanged += OnListChanged;
			bindingSource.PositionChanged += OnPositionChanged;
			bindingSource.AddingNew += OnAddingNew;
		}
	}

	private void OnAddNew(object sender, EventArgs e)
	{
		if (bindingSource != null)
		{
			bindingSource.AddNew();
		}
		OnRefreshItems();
	}

	private void OnAddingNew(object sender, AddingNewEventArgs e)
	{
		OnRefreshItems();
	}

	private void OnDelete(object sender, EventArgs e)
	{
		if (bindingSource != null)
		{
			bindingSource.RemoveCurrent();
		}
		OnRefreshItems();
	}

	private void OnListChanged(object sender, ListChangedEventArgs e)
	{
		OnRefreshItems();
	}

	private void OnMoveFirst(object sender, EventArgs e)
	{
		if (bindingSource != null)
		{
			bindingSource.MoveFirst();
		}
		OnRefreshItems();
	}

	private void OnMoveLast(object sender, EventArgs e)
	{
		if (bindingSource != null)
		{
			bindingSource.MoveLast();
		}
		OnRefreshItems();
	}

	private void OnMoveNext(object sender, EventArgs e)
	{
		if (bindingSource != null)
		{
			bindingSource.MoveNext();
		}
		OnRefreshItems();
	}

	private void OnMovePrevious(object sender, EventArgs e)
	{
		if (bindingSource != null)
		{
			bindingSource.MovePrevious();
		}
		OnRefreshItems();
	}

	private void OnPositionChanged(object sender, EventArgs e)
	{
		OnRefreshItems();
	}
}
