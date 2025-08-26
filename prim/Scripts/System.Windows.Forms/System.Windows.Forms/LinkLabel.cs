using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Theming;

namespace System.Windows.Forms;

/// <summary>Represents a Windows label control that can display hyperlinks.</summary>
/// <filterpriority>1</filterpriority>
[DefaultEvent("LinkClicked")]
[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class LinkLabel : Label, IButtonControl
{
	internal class Piece
	{
		public string text;

		public int start;

		public int length;

		public Link link;

		public Region region;

		public Piece(int start, int length, string text, Link link)
		{
			this.start = start;
			this.length = length;
			this.text = text;
			this.link = link;
		}
	}

	/// <summary>Represents a link within a <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
	[TypeConverter(typeof(LinkConverter))]
	public class Link
	{
		private bool enabled;

		internal int length;

		private object linkData;

		private int start;

		private bool visited;

		private LinkLabel owner;

		private bool hovered;

		internal ArrayList pieces;

		private bool focused;

		private bool active;

		private string description;

		private string name;

		private object tag;

		/// <summary>Gets or sets a text description of the link.</summary>
		/// <returns>A <see cref="T:System.String" /> representing a text description of the link.</returns>
		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.LinkLabel.Link" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the <see cref="T:System.Windows.Forms.LinkLabel.Link" />. The default value is the empty string ("").</returns>
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		/// <summary>Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.LinkLabel.Link" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		[Localizable(false)]
		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the link is enabled.</summary>
		/// <returns>true if the link is enabled; otherwise, false.</returns>
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				if (enabled != value)
				{
					Invalidate();
				}
				enabled = value;
			}
		}

		/// <summary>Gets or sets the number of characters in the link text.</summary>
		/// <returns>The number of characters, including spaces, in the link text.</returns>
		public int Length
		{
			get
			{
				if (length == -1)
				{
					return owner.Text.Length;
				}
				return length;
			}
			set
			{
				if (length != value)
				{
					length = value;
					owner.CreateLinkPieces();
				}
			}
		}

		/// <summary>Gets or sets the data associated with the link.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the data associated with the link.</returns>
		[DefaultValue(null)]
		public object LinkData
		{
			get
			{
				return linkData;
			}
			set
			{
				linkData = value;
			}
		}

		/// <summary>Gets or sets the starting location of the link within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>The location within the text of the <see cref="T:System.Windows.Forms.LinkLabel" /> control where the link starts.</returns>
		public int Start
		{
			get
			{
				return start;
			}
			set
			{
				if (start != value)
				{
					start = value;
					owner.sorted_links = null;
					owner.CreateLinkPieces();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user has visited the link.</summary>
		/// <returns>true if the link has been visited; otherwise, false.</returns>
		[DefaultValue(false)]
		public bool Visited
		{
			get
			{
				return visited;
			}
			set
			{
				if (visited != value)
				{
					Invalidate();
				}
				visited = value;
			}
		}

		internal bool Hovered
		{
			get
			{
				return hovered;
			}
			set
			{
				if (hovered != value)
				{
					Invalidate();
				}
				hovered = value;
			}
		}

		internal bool Focused
		{
			get
			{
				return focused;
			}
			set
			{
				if (focused != value)
				{
					Invalidate();
				}
				focused = value;
			}
		}

		internal bool Active
		{
			get
			{
				return active;
			}
			set
			{
				if (active != value)
				{
					Invalidate();
				}
				active = value;
			}
		}

		internal LinkLabel Owner
		{
			set
			{
				owner = value;
			}
		}

		internal Link(LinkLabel owner)
		{
			focused = false;
			enabled = true;
			visited = false;
			length = (start = 0);
			linkData = null;
			this.owner = owner;
			pieces = new ArrayList();
			name = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class. </summary>
		public Link()
		{
			enabled = true;
			name = string.Empty;
			pieces = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class with the specified starting location and number of characters after the starting location within the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</param>
		/// <param name="length">The number of characters, after the starting character, to include in the link area.</param>
		public Link(int start, int length)
			: this()
		{
			this.start = start;
			this.length = length;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class with the specified starting location, number of characters after the starting location within the <see cref="T:System.Windows.Forms.LinkLabel" />, and the data associated with the link.</summary>
		/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</param>
		/// <param name="length">The number of characters, after the starting character, to include in the link area.</param>
		/// <param name="linkData">The data associated with the link.</param>
		public Link(int start, int length, object linkData)
			: this(start, length)
		{
			this.linkData = linkData;
		}

		private void Invalidate()
		{
			for (int i = 0; i < pieces.Count; i++)
			{
				owner.Invalidate(((Piece)pieces[i]).region);
			}
		}

		internal bool Contains(int x, int y)
		{
			foreach (Piece piece in pieces)
			{
				if (piece.region.IsVisible(new Point(x, y)))
				{
					return true;
				}
			}
			return false;
		}
	}

	private class LinkComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			Link link = (Link)x;
			Link link2 = (Link)y;
			return link.Start - link2.Start;
		}
	}

	/// <summary>Represents the collection of links within a <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
	public class LinkCollection : ICollection, IEnumerable, IList
	{
		private LinkLabel owner;

		private bool links_added;

		/// <summary>For a description of this member, see .<see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		bool IList.IsFixedSize => false;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		object IList.this[int index]
		{
			get
			{
				return owner.links[index];
			}
			set
			{
				owner.links[index] = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		object ICollection.SyncRoot => this;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		bool ICollection.IsSynchronized => false;

		/// <summary>Gets the number of links in the collection.</summary>
		/// <returns>The number of links in the collection.</returns>
		[Browsable(false)]
		public int Count => owner.links.Count;

		/// <summary>Gets a value indicating whether this collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		public bool IsReadOnly => false;

		/// <summary>Gets and sets the link at the specified index within the collection.</summary>
		/// <returns>An object representing the link located at the specified index within the collection.</returns>
		/// <param name="index">The index of the link in the collection to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is a negative value or greater than the number of items in the collection. </exception>
		public virtual Link this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				return (Link)owner.links[index];
			}
			set
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				owner.links[index] = value;
			}
		}

		/// <summary>Gets a link with the specified key from the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> with the specified key within the collection.</returns>
		/// <param name="key">The name of the link to retrieve from the collection.</param>
		public virtual Link this[string key]
		{
			get
			{
				if (string.IsNullOrEmpty(key))
				{
					return null;
				}
				foreach (Link link in owner.links)
				{
					if (string.Compare(link.Name, key, ignoreCase: true) == 0)
					{
						return link;
					}
				}
				return null;
			}
		}

		internal bool IsDefault => Count == 1 && this[0].Start == 0 && this[0].length == -1;

		/// <summary>Gets a value indicating whether links have been added to the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" />. </summary>
		/// <returns>true if links have been added to the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" />; otherwise, false.</returns>
		public bool LinksAdded => links_added;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.LinkLabel" /> control that owns the collection. </param>
		public LinkCollection(LinkLabel owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.owner = owner;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		void ICollection.CopyTo(Array dest, int index)
		{
			owner.links.CopyTo(dest, index);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		int IList.Add(object value)
		{
			int result = owner.links.Add(value);
			owner.sorted_links = null;
			owner.CheckLinks();
			owner.CreateLinkPieces();
			return result;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="link">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		bool IList.Contains(object link)
		{
			return Contains((Link)link);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The index of the <paramref name="link" /> parameter, if found in the list; otherwise, -1.</returns>
		/// <param name="link">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		int IList.IndexOf(object link)
		{
			return owner.links.IndexOf(link);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		void IList.Insert(int index, object value)
		{
			owner.links.Insert(index, value);
			owner.sorted_links = null;
			owner.CheckLinks();
			owner.CreateLinkPieces();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		void IList.Remove(object value)
		{
			Remove((Link)value);
		}

		/// <summary>Adds a link with the specified value to the collection.</summary>
		/// <returns>The zero-based index where the link specified by the <paramref name="value" /> parameter is located in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to add.</param>
		public int Add(Link value)
		{
			value.Owner = owner;
			if (IsDefault)
			{
				owner.links.Clear();
			}
			int result = owner.links.Add(value);
			links_added = true;
			owner.sorted_links = null;
			owner.CheckLinks();
			owner.CreateLinkPieces();
			return result;
		}

		/// <summary>Adds a link to the collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link that was created and added to the collection.</returns>
		/// <param name="start">The starting character within the text of the label where the link is created. </param>
		/// <param name="length">The number of characters after the starting character to include in the link text. </param>
		public Link Add(int start, int length)
		{
			return Add(start, length, null);
		}

		/// <summary>Adds a link to the collection with information to associate with the link.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link that was created and added to the collection.</returns>
		/// <param name="start">The starting character within the text of the label where the link is created. </param>
		/// <param name="length">The number of characters after the starting character to include in the link text. </param>
		/// <param name="linkData">The object containing the information to associate with the link. </param>
		public Link Add(int start, int length, object linkData)
		{
			Link link = new Link(owner);
			link.Length = length;
			link.Start = start;
			link.LinkData = linkData;
			int index = Add(link);
			return (Link)owner.links[index];
		}

		/// <summary>Clears all links from the collection.</summary>
		public virtual void Clear()
		{
			owner.links.Clear();
			owner.sorted_links = null;
			owner.CreateLinkPieces();
		}

		/// <summary>Determines whether the specified link is within the collection.</summary>
		/// <returns>true if the specified link is within the collection; otherwise, false.</returns>
		/// <param name="link">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to search for in the collection. </param>
		public bool Contains(Link link)
		{
			return owner.links.Contains(link);
		}

		/// <summary>Returns a value indicating whether the collection contains a link with the specified key.</summary>
		/// <returns>true if the collection contains an item with the specified key; otherwise, false.</returns>
		/// <param name="key">The link to search for in the collection.</param>
		public virtual bool ContainsKey(string key)
		{
			return this[key] != null;
		}

		/// <summary>Returns an enumerator to use to iterate through the link collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the link collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return owner.links.GetEnumerator();
		}

		/// <summary>Returns the index of the specified link within the collection.</summary>
		/// <returns>The zero-based index where the link is located within the collection; otherwise, negative one (-1).</returns>
		/// <param name="link">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to search for in the collection. </param>
		public int IndexOf(Link link)
		{
			return owner.links.IndexOf(link);
		}

		/// <summary>Retrieves the zero-based index of the first occurrence of the specified key within the entire collection.</summary>
		/// <returns>The zero-based index of the first occurrence of value within the entire collection, if found; otherwise, -1.</returns>
		/// <param name="key">The key to search the collection for.</param>
		public virtual int IndexOfKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return -1;
			}
			return IndexOf(this[key]);
		}

		/// <summary>Removes the specified link from the collection.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that represents the link to remove from the collection. </param>
		public void Remove(Link value)
		{
			owner.links.Remove(value);
			owner.sorted_links = null;
			owner.CreateLinkPieces();
		}

		/// <summary>Removes the link with the specified key. </summary>
		/// <param name="key">The key of the link to remove.</param>
		public virtual void RemoveByKey(string key)
		{
			Remove(this[key]);
		}

		/// <summary>Removes a link at a specified location within the collection.</summary>
		/// <param name="index">The zero-based index of the item to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is a negative value or greater than the number of items in the collection. </exception>
		public void RemoveAt(int index)
		{
			if (index >= Count)
			{
				throw new ArgumentOutOfRangeException("Invalid value for array index");
			}
			owner.links.Remove(owner.links[index]);
			owner.sorted_links = null;
			owner.CreateLinkPieces();
		}
	}

	private Color active_link_color;

	private Color disabled_link_color;

	private Color link_color;

	private Color visited_color;

	private LinkArea link_area;

	private LinkBehavior link_behavior;

	private LinkCollection link_collection;

	private ArrayList links = new ArrayList();

	internal Link[] sorted_links;

	private bool link_visited;

	internal Piece[] pieces;

	private Cursor override_cursor;

	private DialogResult dialog_result;

	private Link active_link;

	private Link hovered_link;

	private int focused_index;

	private static object LinkClickedEvent;

	/// <summary>For a description of this member, see <see cref="P:System.Windows.Forms.IButtonControl.DialogResult" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
	DialogResult IButtonControl.DialogResult
	{
		get
		{
			return dialog_result;
		}
		set
		{
			dialog_result = value;
		}
	}

	/// <summary>Gets or sets the color used to display an active link.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display an active link. The default color is specified by the system, typically this color is Color.Red.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color ActiveLinkColor
	{
		get
		{
			return active_link_color;
		}
		set
		{
			if (!(active_link_color == value))
			{
				active_link_color = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the color used when displaying a disabled link.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color when displaying a disabled link. The default is Empty.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color DisabledLinkColor
	{
		get
		{
			return disabled_link_color;
		}
		set
		{
			if (!(disabled_link_color == value))
			{
				disabled_link_color = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the color used when displaying a normal link.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to displaying a normal link. The default color is specified by the system, typically this color is Color.Blue.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color LinkColor
	{
		get
		{
			return link_color;
		}
		set
		{
			if (!(link_color == value))
			{
				link_color = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the color used when displaying a link that that has been previously visited.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display links that have been visited. The default color is specified by the system, typically this color is Color.Purple.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color VisitedLinkColor
	{
		get
		{
			return visited_color;
		}
		set
		{
			if (!(visited_color == value))
			{
				visited_color = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the range in the text to treat as a link.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.LinkArea" /> that represents the area treated as a link.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.LinkArea.Start" /> property of the <see cref="T:System.Windows.Forms.LinkArea" /> object is less than zero.-or- The <see cref="P:System.Windows.Forms.LinkArea.Length" /> property of the <see cref="T:System.Windows.Forms.LinkArea" /> object is less than -1. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Editor("System.Windows.Forms.Design.LinkAreaEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public LinkArea LinkArea
	{
		get
		{
			return link_area;
		}
		set
		{
			if (value.Start < 0 || value.Length < -1)
			{
				throw new ArgumentException();
			}
			Links.Clear();
			if (!value.IsEmpty)
			{
				Links.Add(value.Start, value.Length);
				link_area = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is LinkBehavior.SystemDefault.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value is assigned that is not one of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(LinkBehavior.SystemDefault)]
	public LinkBehavior LinkBehavior
	{
		get
		{
			return link_behavior;
		}
		set
		{
			if (link_behavior != value)
			{
				link_behavior = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets the collection of links contained within the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" /> that represents the links contained within the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public LinkCollection Links
	{
		get
		{
			if (link_collection == null)
			{
				link_collection = new LinkCollection(this);
			}
			return link_collection;
		}
	}

	/// <summary>Gets or sets a value indicating whether a link should be displayed as though it were visited.</summary>
	/// <returns>true if links should display as though they were visited; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool LinkVisited
	{
		get
		{
			return link_visited;
		}
		set
		{
			if (link_visited != value)
			{
				link_visited = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the mouse pointer to use when the mouse pointer is within the bounds of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> to use when the mouse pointer is within the <see cref="T:System.Windows.Forms.LinkLabel" /> bounds.</returns>
	protected Cursor OverrideCursor
	{
		get
		{
			if (override_cursor == null)
			{
				override_cursor = Cursors.Hand;
			}
			return override_cursor;
		}
		set
		{
			override_cursor = value;
		}
	}

	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.Repaint)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			if (!(base.Text == value))
			{
				base.Text = value;
				CreateLinkPieces();
			}
		}
	}

	/// <summary>Gets or sets the flat style appearance of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new FlatStyle FlatStyle
	{
		get
		{
			return base.FlatStyle;
		}
		set
		{
			if (base.FlatStyle != value)
			{
				base.FlatStyle = value;
			}
		}
	}

	/// <summary>Gets or sets the interior spacing, in pixels, between the edges of a <see cref="T:System.Windows.Forms.LinkLabel" /> and its contents.</summary>
	/// <returns>
	///   <see cref="T:System.Windows.Forms.Padding" /> values representing the interior spacing, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			if (!(base.Padding == value))
			{
				base.Padding = value;
				CreateLinkPieces();
			}
		}
	}

	/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
	/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
	[RefreshProperties(RefreshProperties.Repaint)]
	public new bool UseCompatibleTextRendering
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

	/// <summary>Occurs when a link is clicked within the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event LinkLabelLinkClickedEventHandler LinkClicked
	{
		add
		{
			base.Events.AddHandler(LinkClickedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LinkClickedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.TabStop" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public new event EventHandler TabStopChanged
	{
		add
		{
			base.TabStopChanged += value;
		}
		remove
		{
			base.TabStopChanged -= value;
		}
	}

	/// <summary>Initializes a new default instance of the <see cref="T:System.Windows.Forms.LinkLabel" /> class.</summary>
	public LinkLabel()
	{
		LinkArea = new LinkArea(0, -1);
		link_behavior = LinkBehavior.SystemDefault;
		link_visited = false;
		pieces = null;
		focused_index = -1;
		string_format.FormatFlags |= StringFormatFlags.NoClip;
		ActiveLinkColor = Color.Red;
		DisabledLinkColor = ThemeEngine.Current.ColorGrayText;
		LinkColor = Color.FromArgb(255, 0, 0, 255);
		VisitedLinkColor = Color.FromArgb(255, 128, 0, 128);
		SetStyle(ControlStyles.Selectable, value: false);
		SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		CreateLinkPieces();
	}

	static LinkLabel()
	{
		LinkClicked = new object();
	}

	/// <summary>Notifies the <see cref="T:System.Windows.Forms.LinkLabel" /> control that it is the default button.</summary>
	/// <param name="value">true if the control should behave as a default button; otherwise, false.</param>
	void IButtonControl.NotifyDefault(bool value)
	{
	}

	/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
	void IButtonControl.PerformClick()
	{
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return base.CreateAccessibilityInstance();
	}

	/// <summary>Creates a handle for this control. This method is called by the .NET Framework, this should not be called. Inheriting classes should always call base.createHandle when overriding this method.</summary>
	protected override void CreateHandle()
	{
		base.CreateHandle();
		CreateLinkPieces();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.AutoSizeChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnAutoSizeChanged(EventArgs e)
	{
		base.OnAutoSizeChanged(e);
	}

	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
		Invalidate();
	}

	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
		CreateLinkPieces();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);
		if (focused_index == -1)
		{
			if ((Control.ModifierKeys & Keys.Shift) == 0)
			{
				for (int i = 0; i < sorted_links.Length; i++)
				{
					if (sorted_links[i].Enabled)
					{
						focused_index = i;
						break;
					}
				}
			}
			else
			{
				if (focused_index == -1)
				{
					focused_index = sorted_links.Length;
				}
				for (int num = focused_index - 1; num >= 0; num--)
				{
					if (sorted_links[num].Enabled)
					{
						sorted_links[num].Focused = true;
						focused_index = num;
						return;
					}
				}
			}
		}
		if (focused_index != -1)
		{
			sorted_links[focused_index].Focused = true;
		}
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnKeyDown(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyDown(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && focused_index != -1)
		{
			OnLinkClicked(new LinkLabelLinkClickedEventArgs(sorted_links[focused_index]));
		}
		base.OnKeyDown(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.LinkLabel.LinkClicked" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> that contains the event data. </param>
	protected virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
	{
		((LinkLabelLinkClickedEventHandler)base.Events[LinkClicked])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnLostFocus(EventArgs e)
	{
		base.OnLostFocus(e);
		if (focused_index != -1)
		{
			sorted_links[focused_index].Focused = false;
		}
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (!base.Enabled)
		{
			return;
		}
		base.OnMouseDown(e);
		for (int i = 0; i < sorted_links.Length; i++)
		{
			if (sorted_links[i].Contains(e.X, e.Y) && sorted_links[i].Enabled)
			{
				sorted_links[i].Active = true;
				if (focused_index != -1)
				{
					sorted_links[focused_index].Focused = false;
				}
				active_link = sorted_links[i];
				focused_index = i;
				sorted_links[focused_index].Focused = true;
				break;
			}
		}
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs e)
	{
		if (base.Enabled)
		{
			base.OnMouseLeave(e);
			UpdateHover(null);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnPaddingChanged(EventArgs e)
	{
		base.OnPaddingChanged(e);
	}

	private void UpdateHover(Link link)
	{
		if (link != hovered_link)
		{
			if (hovered_link != null)
			{
				hovered_link.Hovered = false;
			}
			hovered_link = link;
			if (hovered_link != null)
			{
				hovered_link.Hovered = true;
			}
			Cursor = ((hovered_link == null) ? Cursors.Default : OverrideCursor);
			Invalidate();
		}
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseMove(MouseEventArgs e)
	{
		UpdateHover(PointInLink(e.X, e.Y));
		base.OnMouseMove(e);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs e)
	{
		if (!base.Enabled)
		{
			return;
		}
		base.OnMouseUp(e);
		if (active_link != null)
		{
			Link link = ((PointInLink(e.X, e.Y) != active_link) ? null : active_link);
			active_link.Active = false;
			active_link = null;
			if (link != null)
			{
				OnLinkClicked(new LinkLabelLinkClickedEventArgs(link, e.Button));
			}
		}
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		InvokePaintBackground(this, e);
		ThemeElements.LinkLabelPainter.Draw(e.Graphics, e.ClipRectangle, this);
	}

	/// <param name="e"></param>
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnTextAlignChanged(EventArgs e)
	{
		CreateLinkPieces();
		base.OnTextAlignChanged(e);
	}

	protected override void OnTextChanged(EventArgs e)
	{
		CreateLinkPieces();
		base.OnTextChanged(e);
	}

	/// <summary>Gets the link located at the specified client coordinates.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link located at the specified coordinates. If the point does not contain a link, null is returned.</returns>
	/// <param name="x">The horizontal coordinate of the point to search for a link. </param>
	/// <param name="y">The vertical coordinate of the point to search for a link. </param>
	protected Link PointInLink(int x, int y)
	{
		for (int i = 0; i < sorted_links.Length; i++)
		{
			if (sorted_links[i].Contains(x, y))
			{
				return sorted_links[i];
			}
		}
		return null;
	}

	/// <summary>Processes a dialog key. </summary>
	/// <returns>true to consume the key; false to allow further processing.</returns>
	/// <param name="keyData">Key code and modifier flags. </param>
	protected override bool ProcessDialogKey(Keys keyData)
	{
		if ((keyData & Keys.KeyCode) == Keys.Tab)
		{
			Select(directed: true, (keyData & Keys.Shift) == 0);
			return true;
		}
		return base.ProcessDialogKey(keyData);
	}

	/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
	/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
	protected override void Select(bool directed, bool forward)
	{
		if (!directed)
		{
			return;
		}
		if (focused_index != -1)
		{
			sorted_links[focused_index].Focused = false;
			focused_index = -1;
		}
		if (forward)
		{
			for (int i = focused_index + 1; i < sorted_links.Length; i++)
			{
				if (sorted_links[i].Enabled)
				{
					sorted_links[i].Focused = true;
					focused_index = i;
					base.Select(directed, forward);
					return;
				}
			}
		}
		else
		{
			if (focused_index == -1)
			{
				focused_index = sorted_links.Length;
			}
			for (int num = focused_index - 1; num >= 0; num--)
			{
				if (sorted_links[num].Enabled)
				{
					sorted_links[num].Focused = true;
					focused_index = num;
					base.Select(directed, forward);
					return;
				}
			}
		}
		focused_index = -1;
		if (base.Parent != null)
		{
			base.Parent.SelectNextControl(this, forward, tabStopOnly: false, nested: true, wrap: true);
		}
	}

	/// <summary>Performs the work of setting the bounds of this control. </summary>
	/// <param name="x">New left of the control. </param>
	/// <param name="y">New right of the control. </param>
	/// <param name="width">New width of the control. </param>
	/// <param name="height">New height of the control. </param>
	/// <param name="specified">Which values were specified. This parameter reflects user intent, not which values have changed. </param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		base.SetBoundsCore(x, y, width, height, specified);
		CreateLinkPieces();
	}

	protected override void WndProc(ref Message msg)
	{
		base.WndProc(ref msg);
	}

	private ArrayList CreatePiecesFromText(int start, int len, Link link)
	{
		ArrayList arrayList = new ArrayList();
		if (start + len > Text.Length)
		{
			len = Text.Length - start;
		}
		if (len < 0)
		{
			return arrayList;
		}
		string text = Text.Substring(start, len);
		int num = 0;
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '\n')
			{
				if (i != 0)
				{
					Piece value = new Piece(start + num, i + 1 - num, text.Substring(num, i + 1 - num), link);
					arrayList.Add(value);
				}
				num = i + 1;
			}
		}
		if (num < text.Length)
		{
			Piece value2 = new Piece(start + num, text.Length - num, text.Substring(num, text.Length - num), link);
			arrayList.Add(value2);
		}
		return arrayList;
	}

	private void CreateLinkPieces()
	{
		if (Text.Length == 0)
		{
			SetStyle(ControlStyles.Selectable, value: false);
			base.TabStop = false;
			link_area.Start = 0;
			link_area.Length = 0;
			return;
		}
		if (Links.Count == 1 && Links[0].Start == 0 && Links[0].Length == -1)
		{
			Links[0].Length = Text.Length;
		}
		SortLinks();
		if (Links.Count > 0)
		{
			link_area.Start = Links[0].Start;
			link_area.Length = Links[0].Length;
		}
		else
		{
			link_area.Start = 0;
			link_area.Length = 0;
		}
		base.TabStop = LinkArea.Length > 0;
		SetStyle(ControlStyles.Selectable, base.TabStop);
		if (!base.IsHandleCreated)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		int num = 0;
		for (int i = 0; i < sorted_links.Length; i++)
		{
			int start = sorted_links[i].Start;
			if (start > num)
			{
				ArrayList c = CreatePiecesFromText(num, start - num, null);
				arrayList.AddRange(c);
			}
			ArrayList c2 = CreatePiecesFromText(start, sorted_links[i].Length, sorted_links[i]);
			arrayList.AddRange(c2);
			sorted_links[i].pieces.AddRange(c2);
			num = sorted_links[i].Start + sorted_links[i].Length;
		}
		if (num < Text.Length)
		{
			ArrayList c3 = CreatePiecesFromText(num, Text.Length - num, null);
			arrayList.AddRange(c3);
		}
		pieces = new Piece[arrayList.Count];
		arrayList.CopyTo(pieces, 0);
		CharacterRange[] array = new CharacterRange[pieces.Length];
		for (int j = 0; j < pieces.Length; j++)
		{
			ref CharacterRange reference = ref array[j];
			reference = new CharacterRange(pieces[j].start, pieces[j].length);
		}
		string_format.SetMeasurableCharacterRanges(array);
		Region[] array2 = TextRenderer.MeasureCharacterRanges(Text, ThemeEngine.Current.GetLinkFont(this), base.PaddingClientRectangle, string_format);
		for (int k = 0; k < pieces.Length; k++)
		{
			pieces[k].region = array2[k];
			pieces[k].region.Translate(Padding.Left, Padding.Top);
		}
		Invalidate();
	}

	private void SortLinks()
	{
		if (sorted_links == null)
		{
			sorted_links = new Link[Links.Count];
			((ICollection)Links).CopyTo((Array)sorted_links, 0);
			Array.Sort(sorted_links, new LinkComparer());
		}
	}

	private void CheckLinks()
	{
		SortLinks();
		int num = 0;
		for (int i = 0; i < sorted_links.Length; i++)
		{
			if (sorted_links[i].Start < num)
			{
				throw new InvalidOperationException("Overlapping link regions.");
			}
			num = sorted_links[i].Start + sorted_links[i].Length;
		}
	}
}
