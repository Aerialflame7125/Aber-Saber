using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Text;

namespace System.Windows.Forms;

/// <summary>Represents a node of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
/// <filterpriority>1</filterpriority>
[Serializable]
[TypeConverter(typeof(TreeNodeConverter))]
[DefaultProperty("Text")]
public class TreeNode : MarshalByRefObject, ISerializable, ICloneable
{
	private TreeView tree_view;

	internal TreeNode parent;

	private string text;

	private int image_index = -1;

	private int selected_image_index = -1;

	private ContextMenu context_menu;

	private ContextMenuStrip context_menu_strip;

	private string image_key = string.Empty;

	private string selected_image_key = string.Empty;

	private int state_image_index = -1;

	private string state_image_key = string.Empty;

	private string tool_tip_text = string.Empty;

	internal TreeNodeCollection nodes;

	internal TreeViewAction check_reason;

	internal int visible_order;

	internal int width = -1;

	internal bool is_expanded;

	private bool check;

	internal OwnerDrawPropertyBag prop_bag;

	private object tag;

	internal IntPtr handle;

	private string name = string.Empty;

	/// <summary>Gets or sets the background color of the tree node.</summary>
	/// <returns>The background <see cref="T:System.Drawing.Color" /> of the tree node. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color BackColor
	{
		get
		{
			if (prop_bag != null)
			{
				return prop_bag.BackColor;
			}
			return Color.Empty;
		}
		set
		{
			if (prop_bag == null)
			{
				prop_bag = new OwnerDrawPropertyBag();
			}
			prop_bag.BackColor = value;
			TreeView?.UpdateNode(this);
		}
	}

	/// <summary>Gets the bounds of the tree node.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the tree node.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public Rectangle Bounds
	{
		get
		{
			if (TreeView == null)
			{
				return Rectangle.Empty;
			}
			int x = GetX();
			int y = GetY();
			if (width == -1)
			{
				width = TreeView.GetNodeWidth(this);
			}
			return new Rectangle(x, y, width, TreeView.ActualItemHeight);
		}
	}

	internal int IndentLevel
	{
		get
		{
			TreeNode treeNode = this;
			int num = 0;
			while (treeNode.Parent != null)
			{
				treeNode = treeNode.Parent;
				num++;
			}
			return num;
		}
	}

	/// <summary>Gets or sets a value indicating whether the tree node is in a checked state.</summary>
	/// <returns>true if the tree node is in a checked state; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Checked
	{
		get
		{
			return check;
		}
		set
		{
			if (check == value)
			{
				return;
			}
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, cancel: false, check_reason);
			if (TreeView != null)
			{
				TreeView.OnBeforeCheck(treeViewCancelEventArgs);
			}
			if (!treeViewCancelEventArgs.Cancel)
			{
				check = value;
				if (TreeView != null)
				{
					TreeView.OnAfterCheck(new TreeViewEventArgs(this, check_reason));
				}
				if (TreeView != null)
				{
					TreeView.UpdateNode(this);
				}
			}
			check_reason = TreeViewAction.Unknown;
		}
	}

	/// <summary>Gets the shortcut menu associated with this tree node.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> associated with the tree node.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(null)]
	public virtual ContextMenu ContextMenu
	{
		get
		{
			return context_menu;
		}
		set
		{
			context_menu = value;
		}
	}

	/// <summary>Gets or sets the shortcut menu associated with this tree node.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the tree node.</returns>
	[DefaultValue(null)]
	public virtual ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return context_menu_strip;
		}
		set
		{
			context_menu_strip = value;
		}
	}

	/// <summary>Gets the first child tree node in the tree node collection.</summary>
	/// <returns>The first child <see cref="T:System.Windows.Forms.TreeNode" /> in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public TreeNode FirstNode
	{
		get
		{
			if (nodes.Count > 0)
			{
				return nodes[0];
			}
			return null;
		}
	}

	/// <summary>Gets or sets the foreground color of the tree node.</summary>
	/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the tree node.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color ForeColor
	{
		get
		{
			if (prop_bag != null)
			{
				return prop_bag.ForeColor;
			}
			if (TreeView != null)
			{
				return TreeView.ForeColor;
			}
			return Color.Empty;
		}
		set
		{
			if (prop_bag == null)
			{
				prop_bag = new OwnerDrawPropertyBag();
			}
			prop_bag.ForeColor = value;
			TreeView?.UpdateNode(this);
		}
	}

	/// <summary>Gets the path from the root tree node to the current tree node.</summary>
	/// <returns>The path from the root tree node to the current tree node.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is not contained in a <see cref="T:System.Windows.Forms.TreeView" />.</exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public string FullPath
	{
		get
		{
			if (TreeView == null)
			{
				throw new InvalidOperationException("No TreeView associated");
			}
			StringBuilder stringBuilder = new StringBuilder();
			BuildFullPath(stringBuilder);
			return stringBuilder.ToString();
		}
	}

	/// <summary>Gets or sets the image list index value of the image displayed when the tree node is in the unselected state.</summary>
	/// <returns>A zero-based index value that represents the image position in the assigned <see cref="T:System.Windows.Forms.ImageList" />.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[TypeConverter(typeof(TreeViewImageIndexConverter))]
	[RelatedImageList("TreeView.ImageList")]
	[DefaultValue(-1)]
	public int ImageIndex
	{
		get
		{
			return image_index;
		}
		set
		{
			if (image_index != value)
			{
				image_index = value;
				image_key = string.Empty;
				TreeView?.UpdateNode(this);
			}
		}
	}

	/// <summary>Gets or sets the key for the image associated with this tree node when the node is in an unselected state.</summary>
	/// <returns>The key for the image associated with this tree node when the node is in an unselected state.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RelatedImageList("TreeView.ImageList")]
	[TypeConverter(typeof(TreeViewImageKeyConverter))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue("")]
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string ImageKey
	{
		get
		{
			return image_key;
		}
		set
		{
			if (!(image_key == value))
			{
				image_key = value;
				image_index = -1;
				TreeView?.UpdateNode(this);
			}
		}
	}

	/// <summary>Gets a value indicating whether the tree node is in an editable state.</summary>
	/// <returns>true if the tree node is in editable state; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool IsEditing
	{
		get
		{
			TreeView treeView = TreeView;
			if (treeView == null)
			{
				return false;
			}
			return treeView.edit_node == this;
		}
	}

	/// <summary>Gets a value indicating whether the tree node is in the expanded state.</summary>
	/// <returns>true if the tree node is in the expanded state; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public bool IsExpanded
	{
		get
		{
			TreeView treeView = TreeView;
			if (treeView != null && treeView.IsHandleCreated)
			{
				bool flag = false;
				foreach (TreeNode node in TreeView.Nodes)
				{
					if (node.Nodes.Count > 0)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return is_expanded;
		}
	}

	/// <summary>Gets a value indicating whether the tree node is in the selected state.</summary>
	/// <returns>true if the tree node is in the selected state; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public bool IsSelected
	{
		get
		{
			if (TreeView == null || !TreeView.IsHandleCreated)
			{
				return false;
			}
			return TreeView.SelectedNode == this;
		}
	}

	/// <summary>Gets a value indicating whether the tree node is visible or partially visible.</summary>
	/// <returns>true if the tree node is visible or partially visible; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public bool IsVisible
	{
		get
		{
			if (TreeView == null || !TreeView.IsHandleCreated || !TreeView.Visible)
			{
				return false;
			}
			if (visible_order <= TreeView.skipped_nodes || visible_order - TreeView.skipped_nodes > TreeView.VisibleCount)
			{
				return false;
			}
			return ArePreviousNodesExpanded;
		}
	}

	/// <summary>Gets the last child tree node.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the last child tree node.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public TreeNode LastNode => (nodes != null && nodes.Count != 0) ? nodes[nodes.Count - 1] : null;

	/// <summary>Gets the zero-based depth of the tree node in the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <returns>The zero-based depth of the tree node in the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public int Level => IndentLevel;

	/// <summary>Gets or sets the name of the tree node.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the name of the tree node.</returns>
	/// <filterpriority>1</filterpriority>
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = ((value != null) ? value : string.Empty);
		}
	}

	/// <summary>Gets the next sibling tree node.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the next sibling tree node.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public TreeNode NextNode
	{
		get
		{
			if (parent == null)
			{
				return null;
			}
			int index = Index;
			if (parent.Nodes.Count > index + 1)
			{
				return parent.Nodes[index + 1];
			}
			return null;
		}
	}

	/// <summary>Gets the next visible tree node.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the next visible tree node.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public TreeNode NextVisibleNode
	{
		get
		{
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this);
			openTreeNodeEnumerator.MoveNext();
			if (!openTreeNodeEnumerator.MoveNext())
			{
				return null;
			}
			TreeNode currentNode = openTreeNodeEnumerator.CurrentNode;
			if (!currentNode.IsInClippingRect)
			{
				return null;
			}
			return currentNode;
		}
	}

	/// <summary>Gets or sets the font used to display the text on the tree node's label.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> used to display the text on the tree node's label.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	[Localizable(true)]
	public Font NodeFont
	{
		get
		{
			if (prop_bag != null)
			{
				return prop_bag.Font;
			}
			if (TreeView != null)
			{
				return TreeView.Font;
			}
			return null;
		}
		set
		{
			if (prop_bag == null)
			{
				prop_bag = new OwnerDrawPropertyBag();
			}
			prop_bag.Font = value;
			Invalidate();
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects assigned to the current tree node.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNodeCollection" /> that represents the tree nodes assigned to the current tree node.</returns>
	/// <filterpriority>1</filterpriority>
	[ListBindable(false)]
	[Browsable(false)]
	public TreeNodeCollection Nodes
	{
		get
		{
			if (nodes == null)
			{
				nodes = new TreeNodeCollection(this);
			}
			return nodes;
		}
	}

	/// <summary>Gets the parent tree node of the current tree node.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the parent of the current tree node.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public TreeNode Parent
	{
		get
		{
			TreeView treeView = TreeView;
			if (treeView != null && treeView.root_node == parent)
			{
				return null;
			}
			return parent;
		}
	}

	/// <summary>Gets the previous sibling tree node.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the previous sibling tree node.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public TreeNode PrevNode
	{
		get
		{
			if (parent == null)
			{
				return null;
			}
			int index = Index;
			if (index <= 0 || index > parent.Nodes.Count)
			{
				return null;
			}
			return parent.Nodes[index - 1];
		}
	}

	/// <summary>Gets the previous visible tree node.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the previous visible tree node.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public TreeNode PrevVisibleNode
	{
		get
		{
			OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this);
			openTreeNodeEnumerator.MovePrevious();
			if (!openTreeNodeEnumerator.MovePrevious())
			{
				return null;
			}
			TreeNode currentNode = openTreeNodeEnumerator.CurrentNode;
			if (!currentNode.IsInClippingRect)
			{
				return null;
			}
			return currentNode;
		}
	}

	/// <summary>Gets or sets the image list index value of the image that is displayed when the tree node is in the selected state.</summary>
	/// <returns>A zero-based index value that represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue(-1)]
	[RelatedImageList("TreeView.ImageList")]
	[TypeConverter(typeof(TreeViewImageIndexConverter))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Localizable(true)]
	public int SelectedImageIndex
	{
		get
		{
			return selected_image_index;
		}
		set
		{
			selected_image_index = value;
		}
	}

	/// <summary>Gets or sets the key of the image displayed in the tree node when it is in a selected state.</summary>
	/// <returns>The key of the image displayed when the tree node is in a selected state.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[TypeConverter(typeof(TreeViewImageKeyConverter))]
	[RelatedImageList("TreeView.ImageList")]
	[DefaultValue("")]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	public string SelectedImageKey
	{
		get
		{
			return selected_image_key;
		}
		set
		{
			selected_image_key = value;
		}
	}

	/// <summary>Gets or sets the index of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" /> when the parent <see cref="T:System.Windows.Forms.TreeView" /> has its <see cref="P:System.Windows.Forms.TreeView.CheckBoxes" /> property set to false.</summary>
	/// <returns>The index of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1 or greater than 14.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RelatedImageList("TreeView.StateImageList")]
	[DefaultValue(-1)]
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
	public int StateImageIndex
	{
		get
		{
			return state_image_index;
		}
		set
		{
			if (state_image_index != value)
			{
				state_image_index = value;
				state_image_key = string.Empty;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the key of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" /> when the parent <see cref="T:System.Windows.Forms.TreeView" /> has its <see cref="P:System.Windows.Forms.TreeView.CheckBoxes" /> property set to false.</summary>
	/// <returns>The key of the image used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RelatedImageList("TreeView.StateImageList")]
	[DefaultValue("")]
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[TypeConverter(typeof(ImageKeyConverter))]
	public string StateImageKey
	{
		get
		{
			return state_image_key;
		}
		set
		{
			if (state_image_key != value)
			{
				state_image_key = value;
				state_image_index = -1;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the object that contains data about the tree node.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the tree node. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Localizable(false)]
	[Bindable(true)]
	[TypeConverter(typeof(StringConverter))]
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

	/// <summary>Gets or sets the text displayed in the label of the tree node.</summary>
	/// <returns>The text displayed in the label of the tree node.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public string Text
	{
		get
		{
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (!(text == value))
			{
				text = value;
				Invalidate();
				TreeView?.OnUIANodeTextChanged(new TreeViewEventArgs(this));
			}
		}
	}

	/// <summary>Gets or sets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <returns>Gets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(false)]
	[DefaultValue("")]
	public string ToolTipText
	{
		get
		{
			return tool_tip_text;
		}
		set
		{
			tool_tip_text = value;
		}
	}

	/// <summary>Gets the parent tree view that the tree node is assigned to.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeView" /> that represents the parent tree view that the tree node is assigned to, or null if the node has not been assigned to a tree view.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public TreeView TreeView
	{
		get
		{
			if (tree_view != null)
			{
				return tree_view;
			}
			TreeNode treeNode = parent;
			while (treeNode != null && treeNode.TreeView == null)
			{
				treeNode = treeNode.parent;
			}
			return treeNode?.TreeView;
		}
	}

	/// <summary>Gets the handle of the tree node.</summary>
	/// <returns>The tree node handle.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public IntPtr Handle
	{
		get
		{
			if (handle == IntPtr.Zero && TreeView != null)
			{
				handle = TreeView.CreateNodeHandle();
			}
			return handle;
		}
	}

	internal bool ArePreviousNodesExpanded
	{
		get
		{
			for (TreeNode treeNode = Parent; treeNode != null; treeNode = treeNode.Parent)
			{
				if (!treeNode.is_expanded)
				{
					return false;
				}
			}
			return true;
		}
	}

	internal bool IsRoot
	{
		get
		{
			TreeView treeView = TreeView;
			if (treeView == null)
			{
				return false;
			}
			if (treeView.root_node == this)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets the position of the tree node in the tree node collection.</summary>
	/// <returns>A zero-based index value that represents the position of the tree node in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int Index
	{
		get
		{
			if (parent == null)
			{
				return 0;
			}
			return parent.Nodes.IndexOf(this);
		}
	}

	internal bool NeedsWidth => width == -1;

	private bool IsInClippingRect
	{
		get
		{
			if (TreeView == null)
			{
				return false;
			}
			Rectangle bounds = Bounds;
			if (bounds.Y < 0 && bounds.Y > TreeView.ClientRectangle.Height)
			{
				return false;
			}
			return true;
		}
	}

	internal Image StateImage
	{
		get
		{
			if (TreeView != null)
			{
				if (TreeView.StateImageList == null)
				{
					return null;
				}
				if (state_image_index >= 0)
				{
					return TreeView.StateImageList.Images[state_image_index];
				}
				if (state_image_key != string.Empty)
				{
					return TreeView.StateImageList.Images[state_image_key];
				}
			}
			return null;
		}
	}

	internal int Image
	{
		get
		{
			if (TreeView == null || TreeView.ImageList == null)
			{
				return -1;
			}
			if (IsSelected)
			{
				if (selected_image_index >= 0)
				{
					return selected_image_index;
				}
				if (!string.IsNullOrEmpty(selected_image_key))
				{
					return TreeView.ImageList.Images.IndexOfKey(selected_image_key);
				}
				if (!string.IsNullOrEmpty(TreeView.SelectedImageKey))
				{
					return TreeView.ImageList.Images.IndexOfKey(TreeView.SelectedImageKey);
				}
				if (TreeView.SelectedImageIndex >= 0)
				{
					return TreeView.SelectedImageIndex;
				}
			}
			else
			{
				if (image_index >= 0)
				{
					return image_index;
				}
				if (!string.IsNullOrEmpty(image_key))
				{
					return TreeView.ImageList.Images.IndexOfKey(image_key);
				}
				if (!string.IsNullOrEmpty(TreeView.ImageKey))
				{
					return TreeView.ImageList.Images.IndexOfKey(TreeView.ImageKey);
				}
				if (TreeView.ImageIndex >= 0)
				{
					return TreeView.ImageIndex;
				}
			}
			if (TreeView.ImageList.Images.Count > 0)
			{
				return 0;
			}
			return -1;
		}
	}

	internal TreeNode(TreeView tree_view)
		: this()
	{
		this.tree_view = tree_view;
		is_expanded = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class using the specified serialization information and context.</summary>
	/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the data to deserialize the class.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source and destination of the serialized stream.</param>
	protected TreeNode(SerializationInfo serializationInfo, StreamingContext context)
		: this()
	{
		SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
		int num = 0;
		while (enumerator.MoveNext())
		{
			SerializationEntry current = enumerator.Current;
			switch (current.Name)
			{
			case "Text":
				Text = (string)current.Value;
				break;
			case "PropBag":
				prop_bag = (OwnerDrawPropertyBag)current.Value;
				break;
			case "ImageIndex":
				image_index = (int)current.Value;
				break;
			case "SelectedImageIndex":
				selected_image_index = (int)current.Value;
				break;
			case "Tag":
				tag = current.Value;
				break;
			case "IsChecked":
				check = (bool)current.Value;
				break;
			case "ChildCount":
				num = (int)current.Value;
				break;
			}
		}
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				TreeNode node = (TreeNode)serializationInfo.GetValue("children" + i, typeof(TreeNode));
				Nodes.Add(node);
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class.</summary>
	public TreeNode()
	{
		nodes = new TreeNodeCollection(this);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text.</summary>
	/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
	public TreeNode(string text)
		: this()
	{
		Text = text;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text and child tree nodes.</summary>
	/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
	/// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects. </param>
	public TreeNode(string text, TreeNode[] children)
		: this(text)
	{
		Nodes.AddRange(children);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text and images to display when the tree node is in a selected and unselected state.</summary>
	/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
	/// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected. </param>
	/// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected. </param>
	public TreeNode(string text, int imageIndex, int selectedImageIndex)
		: this(text)
	{
		image_index = imageIndex;
		selected_image_index = selectedImageIndex;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text, child tree nodes, and images to display when the tree node is in a selected and unselected state.</summary>
	/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node. </param>
	/// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected. </param>
	/// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected. </param>
	/// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects. </param>
	public TreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
		: this(text, imageIndex, selectedImageIndex)
	{
		Nodes.AddRange(children);
	}

	/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the data to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination information for this serialization.</param>
	void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
	{
		si.AddValue("Text", Text);
		si.AddValue("prop_bag", prop_bag, typeof(OwnerDrawPropertyBag));
		si.AddValue("ImageIndex", ImageIndex);
		si.AddValue("SelectedImageIndex", SelectedImageIndex);
		si.AddValue("Tag", Tag);
		si.AddValue("Checked", Checked);
		si.AddValue("NumberOfChildren", Nodes.Count);
		for (int i = 0; i < Nodes.Count; i++)
		{
			si.AddValue("Child-" + i, Nodes[i], typeof(TreeNode));
		}
	}

	/// <summary>Copies the tree node and the entire subtree rooted at this tree node.</summary>
	/// <returns>The <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual object Clone()
	{
		TreeNode treeNode = new TreeNode(text, image_index, selected_image_index);
		if (nodes != null)
		{
			foreach (TreeNode node in nodes)
			{
				treeNode.Nodes.Add((TreeNode)node.Clone());
			}
		}
		treeNode.Tag = tag;
		treeNode.Checked = Checked;
		if (prop_bag != null)
		{
			treeNode.prop_bag = OwnerDrawPropertyBag.Copy(prop_bag);
		}
		return treeNode;
	}

	/// <summary>Loads the state of the <see cref="T:System.Windows.Forms.TreeNode" /> from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
	/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the state of the stream during deserialization.</param>
	protected virtual void Deserialize(SerializationInfo serializationInfo, StreamingContext context)
	{
		Text = serializationInfo.GetString("Text");
		prop_bag = (OwnerDrawPropertyBag)serializationInfo.GetValue("prop_bag", typeof(OwnerDrawPropertyBag));
		ImageIndex = serializationInfo.GetInt32("ImageIndex");
		SelectedImageIndex = serializationInfo.GetInt32("SelectedImageIndex");
		Tag = serializationInfo.GetValue("Tag", typeof(object));
		Checked = serializationInfo.GetBoolean("Checked");
		int @int = serializationInfo.GetInt32("NumberOfChildren");
		for (int i = 0; i < @int; i++)
		{
			Nodes.Add((TreeNode)serializationInfo.GetValue("Child-" + i, typeof(TreeNode)));
		}
	}

	/// <summary>Saves the state of the <see cref="T:System.Windows.Forms.TreeNode" /> to the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" />. </summary>
	/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
	/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the state of the stream during serialization</param>
	protected virtual void Serialize(SerializationInfo si, StreamingContext context)
	{
		si.AddValue("Text", Text);
		si.AddValue("prop_bag", prop_bag, typeof(OwnerDrawPropertyBag));
		si.AddValue("ImageIndex", ImageIndex);
		si.AddValue("SelectedImageIndex", SelectedImageIndex);
		si.AddValue("Tag", Tag);
		si.AddValue("Checked", Checked);
		si.AddValue("NumberOfChildren", Nodes.Count);
		for (int i = 0; i < Nodes.Count; i++)
		{
			si.AddValue("Child-" + i, Nodes[i], typeof(TreeNode));
		}
	}

	internal int GetY()
	{
		if (TreeView == null)
		{
			return 0;
		}
		return (visible_order - 1) * TreeView.ActualItemHeight - TreeView.skipped_nodes * TreeView.ActualItemHeight;
	}

	internal int GetX()
	{
		if (TreeView == null)
		{
			return 0;
		}
		int indentLevel = IndentLevel;
		int num = (TreeView.ShowRootLines ? 1 : 0);
		int num2 = (TreeView.CheckBoxes ? 19 : 0);
		if (!TreeView.CheckBoxes && StateImage != null)
		{
			num2 = 19;
		}
		int num3 = ((TreeView.ImageList != null) ? (TreeView.ImageList.ImageSize.Width + 3) : 0);
		return (indentLevel + num) * TreeView.Indent + num2 + num3 - TreeView.hbar_offset;
	}

	internal int GetLinesX()
	{
		int num = (TreeView.ShowRootLines ? 1 : 0);
		return (IndentLevel + num) * TreeView.Indent - TreeView.hbar_offset;
	}

	internal int GetImageX()
	{
		return GetLinesX() + ((TreeView.CheckBoxes || StateImage != null) ? 19 : 0);
	}

	/// <summary>Returns the tree node with the specified handle and assigned to the specified tree view control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node assigned to the specified <see cref="T:System.Windows.Forms.TreeView" /> control with the specified handle.</returns>
	/// <param name="tree">The <see cref="T:System.Windows.Forms.TreeView" /> that contains the tree node. </param>
	/// <param name="handle">The handle of the tree node. </param>
	/// <filterpriority>2</filterpriority>
	public static TreeNode FromHandle(TreeView tree, IntPtr handle)
	{
		if (handle == IntPtr.Zero)
		{
			return null;
		}
		return tree.NodeFromHandle(handle);
	}

	/// <summary>Initiates the editing of the tree node label.</summary>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.TreeView.LabelEdit" /> is set to false. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void BeginEdit()
	{
		TreeView?.BeginEdit(this);
	}

	/// <summary>Collapses the tree node.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Collapse()
	{
		CollapseInternal(byInternal: false);
	}

	/// <summary>Collapses the <see cref="T:System.Windows.Forms.TreeNode" /> and optionally collapses its children.</summary>
	/// <param name="ignoreChildren">true to leave the child nodes in their current state; false to collapse the child nodes.</param>
	public void Collapse(bool ignoreChildren)
	{
		if (ignoreChildren)
		{
			Collapse();
		}
		else
		{
			CollapseRecursive(this);
		}
	}

	/// <summary>Ends the editing of the tree node label.</summary>
	/// <param name="cancel">true if the editing of the tree node label text was canceled without being saved; otherwise, false. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void EndEdit(bool cancel)
	{
		TreeView treeView = TreeView;
		if (!cancel && treeView != null)
		{
			treeView.EndEdit(this);
		}
		else if (cancel)
		{
			treeView?.CancelEdit(this);
		}
	}

	/// <summary>Expands the tree node.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Expand()
	{
		Expand(byInternal: false);
	}

	/// <summary>Expands all the child tree nodes.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ExpandAll()
	{
		ExpandRecursive(this);
		if (TreeView != null)
		{
			TreeView.UpdateNode(TreeView.root_node);
		}
	}

	/// <summary>Ensures that the tree node is visible, expanding tree nodes and scrolling the tree view control as necessary.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void EnsureVisible()
	{
		if (TreeView != null)
		{
			if (Parent != null)
			{
				ExpandParentRecursive(Parent);
			}
			Rectangle bounds = Bounds;
			if (bounds.Y < 0)
			{
				TreeView.SetTop(this);
			}
			else if (bounds.Bottom > TreeView.ViewportRectangle.Bottom)
			{
				TreeView.SetBottom(this);
			}
		}
	}

	/// <summary>Returns the number of child tree nodes.</summary>
	/// <returns>The number of child tree nodes assigned to the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
	/// <param name="includeSubTrees">true if the resulting count includes all tree nodes indirectly rooted at this tree node; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	public int GetNodeCount(bool includeSubTrees)
	{
		if (!includeSubTrees)
		{
			return Nodes.Count;
		}
		int count = 0;
		GetNodeCountRecursive(this, ref count);
		return count;
	}

	/// <summary>Removes the current tree node from the tree view control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Remove()
	{
		if (parent != null)
		{
			int index = Index;
			parent.Nodes.RemoveAt(index);
		}
	}

	/// <summary>Toggles the tree node to either the expanded or collapsed state.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Toggle()
	{
		if (is_expanded)
		{
			Collapse();
		}
		else
		{
			Expand();
		}
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return "TreeNode: " + Text;
	}

	private bool BuildFullPath(StringBuilder path)
	{
		if (parent == null)
		{
			return false;
		}
		if (parent.BuildFullPath(path))
		{
			path.Append(TreeView.PathSeparator);
		}
		path.Append(text);
		return true;
	}

	private void Expand(bool byInternal)
	{
		if (is_expanded || nodes.Count < 1)
		{
			is_expanded = true;
			return;
		}
		bool flag = false;
		TreeView treeView = TreeView;
		if (treeView != null)
		{
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, cancel: false, TreeViewAction.Expand);
			treeView.OnBeforeExpand(treeViewCancelEventArgs);
			flag = treeViewCancelEventArgs.Cancel;
		}
		if (flag)
		{
			return;
		}
		is_expanded = true;
		int count_to_next = CountToNext();
		if (treeView != null)
		{
			treeView.OnAfterExpand(new TreeViewEventArgs(this));
			treeView.RecalculateVisibleOrder(this);
			treeView.UpdateScrollBars(force: false);
			if (visible_order < treeView.skipped_nodes + treeView.VisibleCount + 1 && ArePreviousNodesExpanded)
			{
				treeView.ExpandBelow(this, count_to_next);
			}
		}
	}

	private void CollapseInternal(bool byInternal)
	{
		if (!is_expanded || nodes.Count < 1 || IsRoot)
		{
			return;
		}
		bool flag = false;
		TreeView treeView = TreeView;
		if (treeView != null)
		{
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, cancel: false, TreeViewAction.Collapse);
			treeView.OnBeforeCollapse(treeViewCancelEventArgs);
			flag = treeViewCancelEventArgs.Cancel;
		}
		if (flag)
		{
			return;
		}
		int count_to_next = CountToNext();
		is_expanded = false;
		if (treeView != null)
		{
			treeView.OnAfterCollapse(new TreeViewEventArgs(this));
			bool visible = treeView.hbar.Visible;
			bool visible2 = treeView.vbar.Visible;
			treeView.RecalculateVisibleOrder(this);
			treeView.UpdateScrollBars(force: false);
			if (visible_order < treeView.skipped_nodes + treeView.VisibleCount + 1 && ArePreviousNodesExpanded)
			{
				treeView.CollapseBelow(this, count_to_next);
			}
			if (!byInternal && HasFocusInChildren())
			{
				treeView.SelectedNode = this;
			}
			if ((visible & !treeView.hbar.Visible) || (visible2 & !treeView.vbar.Visible))
			{
				treeView.Invalidate();
			}
		}
	}

	private int CountToNext()
	{
		bool flag = is_expanded;
		is_expanded = false;
		OpenTreeNodeEnumerator openTreeNodeEnumerator = new OpenTreeNodeEnumerator(this);
		TreeNode treeNode = null;
		if (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.MoveNext())
		{
			treeNode = openTreeNodeEnumerator.CurrentNode;
		}
		is_expanded = flag;
		openTreeNodeEnumerator.Reset();
		openTreeNodeEnumerator.MoveNext();
		int num = 0;
		while (openTreeNodeEnumerator.MoveNext() && openTreeNodeEnumerator.CurrentNode != treeNode)
		{
			num++;
		}
		return num;
	}

	private bool HasFocusInChildren()
	{
		if (TreeView == null)
		{
			return false;
		}
		foreach (TreeNode node in nodes)
		{
			if (node == TreeView.SelectedNode)
			{
				return true;
			}
			if (node.HasFocusInChildren())
			{
				return true;
			}
		}
		return false;
	}

	private void ExpandRecursive(TreeNode node)
	{
		node.Expand(byInternal: true);
		foreach (TreeNode node2 in node.Nodes)
		{
			ExpandRecursive(node2);
		}
	}

	private void ExpandParentRecursive(TreeNode node)
	{
		node.Expand(byInternal: true);
		if (node.Parent != null)
		{
			ExpandParentRecursive(node.Parent);
		}
	}

	internal void CollapseAll()
	{
		CollapseRecursive(this);
	}

	internal void CollapseAllUncheck()
	{
		CollapseUncheckRecursive(this);
	}

	private void CollapseRecursive(TreeNode node)
	{
		node.Collapse();
		foreach (TreeNode node2 in node.Nodes)
		{
			CollapseRecursive(node2);
		}
	}

	private void CollapseUncheckRecursive(TreeNode node)
	{
		node.Collapse();
		node.Checked = false;
		foreach (TreeNode node2 in node.Nodes)
		{
			CollapseUncheckRecursive(node2);
		}
	}

	internal void SetNodes(TreeNodeCollection nodes)
	{
		this.nodes = nodes;
	}

	private void GetNodeCountRecursive(TreeNode node, ref int count)
	{
		count += node.Nodes.Count;
		foreach (TreeNode node2 in node.Nodes)
		{
			GetNodeCountRecursive(node2, ref count);
		}
	}

	internal void Invalidate()
	{
		width = -1;
		TreeView?.UpdateNode(this);
	}

	internal void InvalidateWidth()
	{
		width = -1;
	}

	internal void SetWidth(int width)
	{
		this.width = width;
	}

	internal void SetParent(TreeNode parent)
	{
		this.parent = parent;
	}
}
