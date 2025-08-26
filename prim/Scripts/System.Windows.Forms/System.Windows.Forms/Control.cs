using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Defines the base class for controls, which are components with visual representation.</summary>
/// <filterpriority>1</filterpriority>
[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ToolboxItemFilter("System.Windows.Forms")]
[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultEvent("Click")]
[DefaultProperty("Text")]
[ComVisible(true)]
public class Control : Component, IDisposable, IComponent, ISynchronizeInvoke, IBindableComponent, IBounds, IDropTarget, IWin32Window
{
	internal enum LayoutType
	{
		Anchor,
		Dock
	}

	internal class ControlNativeWindow : NativeWindow
	{
		private Control owner;

		public Control Owner => owner;

		public ControlNativeWindow(Control control)
		{
			owner = control;
		}

		protected override void OnHandleChange()
		{
			owner.WindowTarget.OnHandleChange(owner.Handle);
		}

		internal static Control ControlFromHandle(IntPtr hWnd)
		{
			return ((ControlNativeWindow)NativeWindow.FromHandle(hWnd))?.owner;
		}

		internal static Control ControlFromChildHandle(IntPtr handle)
		{
			for (Hwnd hwnd = Hwnd.ObjectFromHandle(handle); hwnd != null; hwnd = hwnd.Parent)
			{
				ControlNativeWindow controlNativeWindow = (ControlNativeWindow)NativeWindow.FromHandle(handle);
				if (controlNativeWindow != null)
				{
					return controlNativeWindow.owner;
				}
			}
			return null;
		}

		protected override void WndProc(ref Message m)
		{
			owner.WindowTarget.OnMessage(ref m);
		}
	}

	private class ControlWindowTarget : IWindowTarget
	{
		private Control control;

		public ControlWindowTarget(Control control)
		{
			this.control = control;
		}

		public void OnHandleChange(IntPtr newHandle)
		{
		}

		public void OnMessage(ref Message m)
		{
			control.WndProc(ref m);
		}
	}

	/// <summary>Provides information about a control that can be used by an accessibility application.</summary>
	[ComVisible(true)]
	public class ControlAccessibleObject : AccessibleObject
	{
		private IntPtr handle;

		/// <returns>A description of the default action for an object, or null if this object has no default action.</returns>
		public override string DefaultAction => base.DefaultAction;

		/// <summary>Gets the description of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</summary>
		/// <returns>A string describing the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</returns>
		public override string Description => base.Description;

		/// <summary>Gets or sets the handle of the accessible object.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the handle of the control.</returns>
		public IntPtr Handle
		{
			get
			{
				return handle;
			}
			set
			{
			}
		}

		/// <summary>Gets the description of what the object does or how the object is used.</summary>
		/// <returns>The description of what the object does or how the object is used.</returns>
		public override string Help => base.Help;

		/// <summary>Gets the object shortcut key or access key for an accessible object.</summary>
		/// <returns>The object shortcut key or access key for an accessible object, or null if there is no shortcut key associated with the object.</returns>
		public override string KeyboardShortcut => base.KeyboardShortcut;

		/// <summary>Gets or sets the accessible object name.</summary>
		/// <returns>The accessible object name.</returns>
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

		/// <summary>Gets the owner of the accessible object.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that owns the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</returns>
		public Control Owner => owner;

		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or null if there is no parent object.</returns>
		public override AccessibleObject Parent => base.Parent;

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
		public override AccessibleRole Role => base.Role;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" /> class.</summary>
		/// <param name="ownerControl">The <see cref="T:System.Windows.Forms.Control" /> that owns the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ownerControl" /> parameter value is null. </exception>
		public ControlAccessibleObject(Control ownerControl)
			: base(ownerControl)
		{
			if (ownerControl == null)
			{
				throw new ArgumentNullException("owner");
			}
			handle = ownerControl.Handle;
		}

		/// <summary>Gets an identifier for a Help topic and the path to the Help file associated with this accessible object.</summary>
		/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter will contain the path to the Help file associated with this accessible object, or null if there is no IAccessible interface specified.</returns>
		/// <param name="fileName">When this method returns, contains a string that represents the path to the Help file associated with this accessible object. This parameter is passed uninitialized. </param>
		public override int GetHelpTopic(out string fileName)
		{
			return base.GetHelpTopic(out fileName);
		}

		/// <summary>Notifies accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" />.</summary>
		/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of. </param>
		[System.MonoTODO("Stub, does nothing")]
		public void NotifyClients(AccessibleEvents accEvent)
		{
		}

		/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control.</summary>
		/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of. </param>
		/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event. </param>
		[System.MonoTODO("Stub, does nothing")]
		public void NotifyClients(AccessibleEvents accEvent, int childID)
		{
		}

		/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control, giving the identification of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
		/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
		/// <param name="objectID">The identifier of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
		/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
		[System.MonoTODO("Stub, does nothing")]
		public void NotifyClients(AccessibleEvents accEvent, int objectID, int childID)
		{
		}

		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return "ControlAccessibleObject: Owner = " + owner.ToString() + ", Text: " + owner.text;
		}
	}

	private class DoubleBuffer : IDisposable
	{
		public Region InvalidRegion;

		private Stack real_graphics;

		private object back_buffer;

		private Control parent;

		private bool pending_disposal;

		public DoubleBuffer(Control parent)
		{
			this.parent = parent;
			real_graphics = new Stack();
			int num = parent.Width;
			int num2 = parent.Height;
			if (num < 1)
			{
				num = 1;
			}
			if (num2 < 1)
			{
				num2 = 1;
			}
			XplatUI.CreateOffscreenDrawable(parent.Handle, num, num2, out back_buffer);
			Invalidate();
		}

		void IDisposable.Dispose()
		{
			Dispose();
		}

		public void Blit(PaintEventArgs pe)
		{
			Graphics offscreenGraphics = XplatUI.GetOffscreenGraphics(back_buffer);
			XplatUI.BlitFromOffscreen(parent.Handle, pe.Graphics, back_buffer, offscreenGraphics, pe.ClipRectangle);
			offscreenGraphics.Dispose();
		}

		public void Start(PaintEventArgs pe)
		{
			real_graphics.Push(pe.SetGraphics(XplatUI.GetOffscreenGraphics(back_buffer)));
		}

		public void End(PaintEventArgs pe)
		{
			Graphics graphics = pe.SetGraphics((Graphics)real_graphics.Pop());
			if (pending_disposal)
			{
				Dispose();
			}
			else
			{
				XplatUI.BlitFromOffscreen(parent.Handle, pe.Graphics, back_buffer, graphics, pe.ClipRectangle);
				InvalidRegion.Exclude(pe.ClipRectangle);
			}
			graphics.Dispose();
		}

		public void Invalidate()
		{
			if (InvalidRegion != null)
			{
				InvalidRegion.Dispose();
			}
			InvalidRegion = new Region(parent.ClientRectangle);
		}

		public void Dispose()
		{
			if (real_graphics.Count > 0)
			{
				pending_disposal = true;
				return;
			}
			XplatUI.DestroyOffscreenDrawable(back_buffer);
			if (InvalidRegion != null)
			{
				InvalidRegion.Dispose();
			}
			InvalidRegion = null;
			back_buffer = null;
			GC.SuppressFinalize(this);
		}

		~DoubleBuffer()
		{
			Dispose();
		}
	}

	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Control" /> objects.</summary>
	[ListBindable(false)]
	[ComVisible(false)]
	public class ControlCollection : ArrangedElementCollection, ICollection, IEnumerable, IList, ICloneable
	{
		internal class ControlCollectionEnumerator : IEnumerator
		{
			private ArrayList list;

			private int position = -1;

			public object Current
			{
				get
				{
					try
					{
						return list[position];
					}
					catch (IndexOutOfRangeException)
					{
						throw new InvalidOperationException();
					}
				}
			}

			public ControlCollectionEnumerator(ArrayList collection)
			{
				list = collection;
			}

			public bool MoveNext()
			{
				position++;
				return position < list.Count;
			}

			public void Reset()
			{
				position = -1;
			}
		}

		private ArrayList impl_list;

		private Control[] all_controls;

		private Control owner;

		/// <summary>Gets the control that owns this <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that owns this <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
		public Control Owner => owner;

		/// <summary>Indicates a <see cref="T:System.Windows.Forms.Control" /> with the specified key in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> with the specified key within the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
		/// <param name="key">The name of the control to retrieve from the control collection.</param>
		public virtual Control this[string key]
		{
			get
			{
				int num = IndexOfKey(key);
				if (num >= 0)
				{
					return this[num];
				}
				return null;
			}
		}

		/// <summary>Indicates the <see cref="T:System.Windows.Forms.Control" /> at the specified indexed location in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> located at the specified index location within the control collection.</returns>
		/// <param name="index">The index of the control to retrieve from the control collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero or is greater than or equal to the number of controls in the collection. </exception>
		public new virtual Control this[int index]
		{
			get
			{
				if (index < 0 || index >= list.Count)
				{
					throw new ArgumentOutOfRangeException("index", index, "ControlCollection does not have that many controls");
				}
				return (Control)list[index];
			}
		}

		internal ArrayList ImplicitControls => impl_list;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> class.</summary>
		/// <param name="owner">A <see cref="T:System.Windows.Forms.Control" /> representing the control that owns the control collection. </param>
		public ControlCollection(Control owner)
		{
			this.owner = owner;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		int IList.Add(object control)
		{
			if (!(control is Control))
			{
				throw new ArgumentException("Object of type Control required", "control");
			}
			if (control == null)
			{
				throw new ArgumentException("control", "Cannot add null controls");
			}
			bool flag = owner is MdiClient || (owner is Form && ((Form)owner).IsMdiContainer);
			bool topLevel = ((Control)control).GetTopLevel();
			bool flag2 = control is Form && ((Form)control).IsMdiChild;
			if (topLevel && (!flag || !flag2))
			{
				throw new ArgumentException("Cannot add a top level control to a control.", "control");
			}
			return list.Add(control);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		void IList.Remove(object control)
		{
			if (!(control is Control))
			{
				throw new ArgumentException("Object of type Control required", "control");
			}
			all_controls = null;
			list.Remove(control);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		object ICloneable.Clone()
		{
			ControlCollection controlCollection = new ControlCollection(owner);
			controlCollection.list = (ArrayList)list.Clone();
			return controlCollection;
		}

		/// <summary>Adds the specified control to the control collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add to the control collection. </param>
		/// <exception cref="T:System.Exception">The specified control is a top-level control, or a circular control reference would result if this control were added to the control collection. </exception>
		/// <exception cref="T:System.ArgumentException">The object assigned to the <paramref name="value" /> parameter is not a <see cref="T:System.Windows.Forms.Control" />. </exception>
		public virtual void Add(Control value)
		{
			if (value == null)
			{
				return;
			}
			Form form = value as Form;
			Form form2 = owner as Form;
			bool flag = owner is MdiClient || (form2?.IsMdiContainer ?? false);
			bool topLevel = value.GetTopLevel();
			bool flag2 = form?.IsMdiChild ?? false;
			if (topLevel && (!flag || !flag2))
			{
				throw new ArgumentException("Cannot add a top level control to a control.", "value");
			}
			if (flag2 && form.MdiParent != null && form.MdiParent != owner && form.MdiParent != owner.Parent)
			{
				throw new ArgumentException("Form cannot be added to the Controls collection that has a valid MDI parent.", "value");
			}
			value.recalculate_distances = true;
			if (Contains(value))
			{
				owner.PerformLayout();
				return;
			}
			if (value.tab_index == -1)
			{
				int num = 0;
				int count = owner.child_controls.Count;
				for (int i = 0; i < count; i++)
				{
					int tab_index = owner.child_controls[i].tab_index;
					if (tab_index >= num)
					{
						num = tab_index + 1;
					}
				}
				value.tab_index = num;
			}
			if (value.parent != null)
			{
				value.parent.Controls.Remove(value);
			}
			all_controls = null;
			list.Add(value);
			value.ChangeParent(owner);
			value.InitLayout();
			if (owner.Visible)
			{
				owner.UpdateChildrenZOrder();
			}
			owner.PerformLayout(value, "Parent");
			owner.OnControlAdded(new ControlEventArgs(value));
		}

		internal void AddToList(Control c)
		{
			all_controls = null;
			list.Add(c);
		}

		internal virtual void AddImplicit(Control control)
		{
			if (impl_list == null)
			{
				impl_list = new ArrayList();
			}
			if (AllContains(control))
			{
				owner.PerformLayout();
				return;
			}
			if (control.parent != null)
			{
				control.parent.Controls.Remove(control);
			}
			all_controls = null;
			impl_list.Add(control);
			control.ChangeParent(owner);
			control.InitLayout();
			if (owner.Visible)
			{
				owner.UpdateChildrenZOrder();
			}
			if (control.VisibleInternal)
			{
				owner.PerformLayout(control, "Parent");
			}
		}

		/// <summary>Adds an array of control objects to the collection.</summary>
		/// <param name="controls">An array of <see cref="T:System.Windows.Forms.Control" /> objects to add to the collection. </param>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual void AddRange(Control[] controls)
		{
			if (controls == null)
			{
				throw new ArgumentNullException("controls");
			}
			owner.SuspendLayout();
			try
			{
				for (int i = 0; i < controls.Length; i++)
				{
					Add(controls[i]);
				}
			}
			finally
			{
				owner.ResumeLayout();
			}
		}

		internal virtual void AddRangeImplicit(Control[] controls)
		{
			if (controls == null)
			{
				throw new ArgumentNullException("controls");
			}
			owner.SuspendLayout();
			try
			{
				for (int i = 0; i < controls.Length; i++)
				{
					AddImplicit(controls[i]);
				}
			}
			finally
			{
				owner.ResumeLayout(performLayout: false);
			}
		}

		/// <summary>Removes all controls from the collection.</summary>
		public new virtual void Clear()
		{
			all_controls = null;
			while (list.Count > 0)
			{
				Remove((Control)list[list.Count - 1]);
			}
		}

		internal virtual void ClearImplicit()
		{
			if (impl_list != null)
			{
				all_controls = null;
				impl_list.Clear();
			}
		}

		/// <summary>Determines whether the specified control is a member of the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.Control" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection. </param>
		public bool Contains(Control control)
		{
			return list.Contains(control);
		}

		internal bool ImplicitContains(Control value)
		{
			if (impl_list == null)
			{
				return false;
			}
			return impl_list.Contains(value);
		}

		internal bool AllContains(Control value)
		{
			return Contains(value) || ImplicitContains(value);
		}

		/// <summary>Determines whether the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> contains an item with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> contains an item with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </param>
		public virtual bool ContainsKey(string key)
		{
			return IndexOfKey(key) >= 0;
		}

		/// <summary>Searches for controls by their <see cref="P:System.Windows.Forms.Control.Name" /> property and builds an array of all the controls that match.</summary>
		/// <returns>An array of type <see cref="T:System.Windows.Forms.Control" /> containing the matching controls.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </param>
		/// <param name="searchAllChildren">true to search all child controls; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="key" /> parameter is null or the empty string (""). </exception>
		public Control[] Find(string key, bool searchAllChildren)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			ArrayList arrayList = new ArrayList();
			foreach (Control item in list)
			{
				if (item.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase))
				{
					arrayList.Add(item);
				}
				if (searchAllChildren)
				{
					arrayList.AddRange(item.Controls.Find(key, searchAllChildren: true));
				}
			}
			return (Control[])arrayList.ToArray(typeof(Control));
		}

		/// <summary>Retrieves the index of the specified child control within the control collection.</summary>
		/// <returns>A zero-based index value that represents the location of the specified child control within the control collection.</returns>
		/// <param name="child">The <see cref="T:System.Windows.Forms.Control" /> to search for in the control collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </exception>
		public int GetChildIndex(Control child)
		{
			return GetChildIndex(child, throwException: false);
		}

		/// <summary>Retrieves the index of the specified child control within the control collection, and optionally raises an exception if the specified control is not within the control collection.</summary>
		/// <returns>A zero-based index value that represents the location of the specified child control within the control collection; otherwise -1 if the specified <see cref="T:System.Windows.Forms.Control" /> is not found in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
		/// <param name="child">The <see cref="T:System.Windows.Forms.Control" /> to search for in the control collection. </param>
		/// <param name="throwException">true to throw an exception if the <see cref="T:System.Windows.Forms.Control" /> specified in the <paramref name="child" /> parameter is not a control in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />, and the <paramref name="throwException" /> parameter value is true. </exception>
		public virtual int GetChildIndex(Control child, bool throwException)
		{
			int num = list.IndexOf(child);
			if (num == -1 && throwException)
			{
				throw new ArgumentException("Not a child control", "child");
			}
			return num;
		}

		/// <summary>Retrieves a reference to an enumerator object that is used to iterate over a <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />.</returns>
		public override IEnumerator GetEnumerator()
		{
			return new ControlCollectionEnumerator(list);
		}

		internal IEnumerator GetAllEnumerator()
		{
			Control[] allControls = GetAllControls();
			return allControls.GetEnumerator();
		}

		internal Control[] GetAllControls()
		{
			if (all_controls != null)
			{
				return all_controls;
			}
			if (impl_list == null)
			{
				all_controls = (Control[])list.ToArray(typeof(Control));
				return all_controls;
			}
			all_controls = new Control[list.Count + impl_list.Count];
			impl_list.CopyTo(all_controls);
			list.CopyTo(all_controls, impl_list.Count);
			return all_controls;
		}

		/// <summary>Retrieves the index of the specified control in the control collection.</summary>
		/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.Control" /> in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection. </param>
		public int IndexOf(Control control)
		{
			return list.IndexOf(control);
		}

		/// <summary>Retrieves the index of the first occurrence of the specified item within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of the control with the specified name in the collection.</returns>
		/// <param name="key">The name of the control to search for. </param>
		public virtual int IndexOfKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return -1;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (((Control)list[i]).Name.Equals(key, StringComparison.CurrentCultureIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Removes the specified control from the control collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to remove from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </param>
		public virtual void Remove(Control value)
		{
			if (value != null)
			{
				all_controls = null;
				list.Remove(value);
				owner.PerformLayout(value, "Parent");
				owner.OnControlRemoved(new ControlEventArgs(value));
				owner.InternalGetContainerControl()?.ChildControlRemoved(value);
				value.ChangeParent(null);
				owner.UpdateChildrenZOrder();
			}
		}

		internal virtual void RemoveImplicit(Control control)
		{
			if (impl_list != null)
			{
				all_controls = null;
				impl_list.Remove(control);
				owner.PerformLayout(control, "Parent");
				owner.OnControlRemoved(new ControlEventArgs(control));
			}
			control.ChangeParent(null);
			owner.UpdateChildrenZOrder();
		}

		/// <summary>Removes a control from the control collection at the specified indexed location.</summary>
		/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.Control" /> to remove. </param>
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= list.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, "ControlCollection does not have that many controls");
			}
			Remove((Control)list[index]);
		}

		/// <summary>Removes the child control with the specified key.</summary>
		/// <param name="key">The name of the child control to remove. </param>
		public virtual void RemoveByKey(string key)
		{
			int num = IndexOfKey(key);
			if (num >= 0)
			{
				RemoveAt(num);
			}
		}

		/// <summary>Sets the index of the specified child control in the collection to the specified index value.</summary>
		/// <param name="child">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> to search for. </param>
		/// <param name="newIndex">The new index value of the control. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="child" /> control is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </exception>
		public virtual void SetChildIndex(Control child, int newIndex)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			int num = list.IndexOf(child);
			if (num == -1)
			{
				throw new ArgumentException("Not a child control", "child");
			}
			if (num != newIndex)
			{
				all_controls = null;
				list.RemoveAt(num);
				if (newIndex > list.Count)
				{
					list.Add(child);
				}
				else
				{
					list.Insert(newIndex, child);
				}
				child.UpdateZOrder();
				owner.PerformLayout();
			}
		}
	}

	private delegate void RemoveDelegate(object c);

	internal Rectangle bounds;

	private Rectangle explicit_bounds;

	internal object creator_thread;

	internal ControlNativeWindow window;

	private IWindowTarget window_target;

	private string name;

	private bool is_created;

	internal bool has_focus;

	internal bool is_visible;

	internal bool is_entered;

	internal bool is_enabled;

	private bool is_accessible;

	private bool is_captured;

	internal bool is_toplevel;

	private bool is_recreating;

	private bool causes_validation;

	private bool is_focusing;

	private int tab_index;

	private bool tab_stop;

	private bool is_disposed;

	private bool is_disposing;

	private Size client_size;

	private Rectangle client_rect;

	private ControlStyles control_style;

	private ImeMode ime_mode;

	private object control_tag;

	internal int mouse_clicks;

	private Cursor cursor;

	internal bool allow_drop;

	private Region clip_region;

	internal Color foreground_color;

	internal Color background_color;

	private Image background_image;

	internal Font font;

	private string text;

	internal BorderStyle border_style;

	private bool show_keyboard_cues;

	internal bool show_focus_cues;

	internal bool force_double_buffer;

	private LayoutEngine layout_engine;

	internal int layout_suspended;

	private bool layout_pending;

	internal AnchorStyles anchor_style;

	internal DockStyle dock_style;

	private LayoutType layout_type;

	private bool recalculate_distances = true;

	internal int dist_right;

	internal int dist_bottom;

	private ControlCollection child_controls;

	private Control parent;

	private BindingContext binding_context;

	private RightToLeft right_to_left;

	private ContextMenu context_menu;

	internal bool use_compatible_text_rendering;

	private bool use_wait_cursor;

	private string accessible_name;

	private string accessible_description;

	private string accessible_default_action;

	private AccessibleRole accessible_role = AccessibleRole.Default;

	private AccessibleObject accessibility_object;

	private DoubleBuffer backbuffer;

	private ControlBindingsCollection data_bindings;

	private static bool verify_thread_handle;

	private Padding padding;

	private ImageLayout backgroundimage_layout;

	private Size maximum_size;

	private Size minimum_size;

	private Padding margin;

	private ContextMenuStrip context_menu_strip;

	private bool nested_layout;

	private Point auto_scroll_offset;

	private AutoSizeMode auto_size_mode;

	private bool suppressing_key_press;

	private MenuTracker active_tracker;

	private bool auto_size;

	private static object AutoSizeChangedEvent;

	private static object BackColorChangedEvent;

	private static object BackgroundImageChangedEvent;

	private static object BackgroundImageLayoutChangedEvent;

	private static object BindingContextChangedEvent;

	private static object CausesValidationChangedEvent;

	private static object ChangeUICuesEvent;

	private static object ClickEvent;

	private static object ClientSizeChangedEvent;

	private static object ContextMenuChangedEvent;

	private static object ContextMenuStripChangedEvent;

	private static object ControlAddedEvent;

	private static object ControlRemovedEvent;

	private static object CursorChangedEvent;

	private static object DockChangedEvent;

	private static object DoubleClickEvent;

	private static object DragDropEvent;

	private static object DragEnterEvent;

	private static object DragLeaveEvent;

	private static object DragOverEvent;

	private static object EnabledChangedEvent;

	private static object EnterEvent;

	private static object FontChangedEvent;

	private static object ForeColorChangedEvent;

	private static object GiveFeedbackEvent;

	private static object GotFocusEvent;

	private static object HandleCreatedEvent;

	private static object HandleDestroyedEvent;

	private static object HelpRequestedEvent;

	private static object ImeModeChangedEvent;

	private static object InvalidatedEvent;

	private static object KeyDownEvent;

	private static object KeyPressEvent;

	private static object KeyUpEvent;

	private static object LayoutEvent;

	private static object LeaveEvent;

	private static object LocationChangedEvent;

	private static object LostFocusEvent;

	private static object MarginChangedEvent;

	private static object MouseCaptureChangedEvent;

	private static object MouseClickEvent;

	private static object MouseDoubleClickEvent;

	private static object MouseDownEvent;

	private static object MouseEnterEvent;

	private static object MouseHoverEvent;

	private static object MouseLeaveEvent;

	private static object MouseMoveEvent;

	private static object MouseUpEvent;

	private static object MouseWheelEvent;

	private static object MoveEvent;

	private static object PaddingChangedEvent;

	private static object PaintEvent;

	private static object ParentChangedEvent;

	private static object PreviewKeyDownEvent;

	private static object QueryAccessibilityHelpEvent;

	private static object QueryContinueDragEvent;

	private static object RegionChangedEvent;

	private static object ResizeEvent;

	private static object RightToLeftChangedEvent;

	private static object SizeChangedEvent;

	private static object StyleChangedEvent;

	private static object SystemColorsChangedEvent;

	private static object TabIndexChangedEvent;

	private static object TabStopChangedEvent;

	private static object TextChangedEvent;

	private static object ValidatedEvent;

	private static object ValidatingEvent;

	private static object VisibleChangedEvent;

	internal Rectangle PaddingClientRectangle => new Rectangle(ClientRectangle.Left + padding.Left, ClientRectangle.Top + padding.Top, ClientRectangle.Width - padding.Horizontal, ClientRectangle.Height - padding.Vertical);

	internal MenuTracker ActiveTracker
	{
		get
		{
			return active_tracker;
		}
		set
		{
			if (value != active_tracker)
			{
				Capture = value != null;
				active_tracker = value;
			}
		}
	}

	internal bool InternalSelected
	{
		get
		{
			IContainerControl containerControl = GetContainerControl();
			if (containerControl != null && containerControl.ActiveControl == this)
			{
				return true;
			}
			return false;
		}
	}

	internal bool InternalContainsFocus
	{
		get
		{
			IntPtr focus = XplatUI.GetFocus();
			if (IsHandleCreated)
			{
				if (focus == Handle)
				{
					return true;
				}
				Control[] allControls = child_controls.GetAllControls();
				foreach (Control control in allControls)
				{
					if (control.InternalContainsFocus)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	internal bool Entered => is_entered;

	internal bool VisibleInternal => is_visible;

	internal LayoutType ControlLayoutType => layout_type;

	internal BorderStyle InternalBorderStyle
	{
		get
		{
			return border_style;
		}
		set
		{
			if (!Enum.IsDefined(typeof(BorderStyle), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for BorderStyle");
			}
			if (border_style != value)
			{
				border_style = value;
				if (IsHandleCreated)
				{
					XplatUI.SetBorderStyle(window.Handle, (FormBorderStyle)border_style);
					RecreateHandle();
					Refresh();
				}
				else
				{
					client_size = ClientSizeFromSize(bounds.Size);
				}
			}
		}
	}

	internal Size InternalClientSize
	{
		set
		{
			client_size = value;
		}
	}

	internal virtual bool ActivateOnShow => true;

	internal Rectangle ExplicitBounds
	{
		get
		{
			return explicit_bounds;
		}
		set
		{
			explicit_bounds = value;
		}
	}

	internal bool ValidationFailed
	{
		get
		{
			return InternalGetContainerControl()?.validation_failed ?? false;
		}
		set
		{
			ContainerControl containerControl = InternalGetContainerControl();
			if (containerControl != null)
			{
				containerControl.validation_failed = value;
			}
		}
	}

	internal bool IsRecreating => is_recreating;

	internal Graphics DeviceContext => Hwnd.GraphicsContext;

	private bool UseDoubleBuffering
	{
		get
		{
			if (!ThemeEngine.Current.DoubleBufferingSupported)
			{
				return false;
			}
			if (force_double_buffer)
			{
				return true;
			}
			if (DoubleBuffered)
			{
				return true;
			}
			return (control_style & ControlStyles.DoubleBuffer) != 0;
		}
	}

	/// <summary>Gets the default background color of the control.</summary>
	/// <returns>The default background <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.Control" />.</returns>
	/// <filterpriority>1</filterpriority>
	public static Color DefaultBackColor => ThemeEngine.Current.DefaultControlBackColor;

	/// <summary>Gets the default font of the control.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Font" /> of the control. The value returned will vary depending on the user's operating system the local culture setting of their system.</returns>
	/// <exception cref="T:System.ArgumentException">The default font or the regional alternative fonts are not installed on the client computer. </exception>
	/// <filterpriority>1</filterpriority>
	public static Font DefaultFont => ThemeEngine.Current.DefaultFont;

	/// <summary>Gets the default foreground color of the control.</summary>
	/// <returns>The default foreground <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.ControlText" />.</returns>
	/// <filterpriority>1</filterpriority>
	public static Color DefaultForeColor => ThemeEngine.Current.DefaultControlForeColor;

	/// <summary>Gets a value indicating which of the modifier keys (SHIFT, CTRL, and ALT) is in a pressed state.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
	/// <filterpriority>2</filterpriority>
	public static Keys ModifierKeys => XplatUI.State.ModifierKeys;

	/// <summary>Gets a value indicating which of the mouse buttons is in a pressed state.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.MouseButtons" /> enumeration values. The default is <see cref="F:System.Windows.Forms.MouseButtons.None" />.</returns>
	/// <filterpriority>2</filterpriority>
	public static MouseButtons MouseButtons => XplatUI.State.MouseButtons;

	/// <summary>Gets the position of the mouse cursor in screen coordinates.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the coordinates of the mouse cursor relative to the upper-left corner of the screen.</returns>
	/// <filterpriority>2</filterpriority>
	public static Point MousePosition => Cursor.Position;

	/// <summary>Gets or sets a value indicating whether to catch calls on the wrong thread that access a control's <see cref="P:System.Windows.Forms.Control.Handle" /> property when an application is being debugged.</summary>
	/// <returns>true if calls on the wrong thread are caught; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	[System.MonoTODO("Stub, value is not used")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static bool CheckForIllegalCrossThreadCalls
	{
		get
		{
			return verify_thread_handle;
		}
		set
		{
			verify_thread_handle = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public AccessibleObject AccessibilityObject
	{
		get
		{
			if (accessibility_object == null)
			{
				accessibility_object = CreateAccessibilityInstance();
			}
			return accessibility_object;
		}
	}

	/// <summary>Gets or sets the default action description of the control for use by accessibility client applications.</summary>
	/// <returns>The default action description of the control for use by accessibility client applications.</returns>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string AccessibleDefaultActionDescription
	{
		get
		{
			return accessible_default_action;
		}
		set
		{
			accessible_default_action = value;
		}
	}

	/// <summary>Gets or sets the description of the control used by accessibility client applications.</summary>
	/// <returns>The description of the control used by accessibility client applications. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Localizable(true)]
	[MWFCategory("Accessibility")]
	public string AccessibleDescription
	{
		get
		{
			return accessible_description;
		}
		set
		{
			accessible_description = value;
		}
	}

	/// <summary>Gets or sets the name of the control used by accessibility client applications.</summary>
	/// <returns>The name of the control used by accessibility client applications. The default is null.</returns>
	/// <filterpriority>2</filterpriority>
	[Localizable(true)]
	[MWFCategory("Accessibility")]
	[DefaultValue(null)]
	public string AccessibleName
	{
		get
		{
			return accessible_name;
		}
		set
		{
			accessible_name = value;
		}
	}

	/// <summary>Gets or sets the accessible role of the control </summary>
	/// <returns>One of the values of <see cref="T:System.Windows.Forms.AccessibleRole" />. The default is Default.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. </exception>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(AccessibleRole.Default)]
	[MWFCategory("Accessibility")]
	[MWFDescription("Role of the control")]
	public AccessibleRole AccessibleRole
	{
		get
		{
			return accessible_role;
		}
		set
		{
			accessible_role = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control can accept data that the user drags onto it.</summary>
	/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[MWFCategory("Behavior")]
	public virtual bool AllowDrop
	{
		get
		{
			return allow_drop;
		}
		set
		{
			if (allow_drop != value)
			{
				allow_drop = value;
				if (IsHandleCreated)
				{
					UpdateStyles();
					XplatUI.SetAllowDrop(Handle, value);
				}
			}
		}
	}

	/// <summary>Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent. </summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values. The default is Top and Left.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.Repaint)]
	[MWFCategory("Layout")]
	[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
	[Localizable(true)]
	public virtual AnchorStyles Anchor
	{
		get
		{
			return anchor_style;
		}
		set
		{
			layout_type = LayoutType.Anchor;
			if (anchor_style != value)
			{
				anchor_style = value;
				dock_style = DockStyle.None;
				UpdateDistances();
				if (parent != null)
				{
					parent.PerformLayout(this, "Anchor");
				}
			}
		}
	}

	/// <summary>Gets or sets where this control is scrolled to in <see cref="M:System.Windows.Forms.ScrollableControl.ScrollControlIntoView(System.Windows.Forms.Control)" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> specifying the scroll location. The default is the upper-left corner of the control.</returns>
	[DefaultValue(typeof(Point), "0, 0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public virtual Point AutoScrollOffset
	{
		get
		{
			return auto_scroll_offset;
		}
		set
		{
			auto_scroll_offset = value;
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DefaultValue(false)]
	[RefreshProperties(RefreshProperties.All)]
	[Localizable(true)]
	public virtual bool AutoSize
	{
		get
		{
			return auto_size;
		}
		set
		{
			if (auto_size != value)
			{
				auto_size = value;
				if (!value)
				{
					Size = explicit_bounds.Size;
				}
				else if (Parent != null)
				{
					Parent.PerformLayout(this, "AutoSize");
				}
				OnAutoSizeChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Layout")]
	[AmbientValue("{Width=0, Height=0}")]
	public virtual Size MaximumSize
	{
		get
		{
			return maximum_size;
		}
		set
		{
			if (maximum_size != value)
			{
				maximum_size = value;
				Size = PreferredSize;
			}
		}
	}

	/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Layout")]
	public virtual Size MinimumSize
	{
		get
		{
			return minimum_size;
		}
		set
		{
			if (minimum_size != value)
			{
				minimum_size = value;
				Size = PreferredSize;
			}
		}
	}

	/// <summary>Gets or sets the background color for the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DispId(-501)]
	[MWFCategory("Appearance")]
	public virtual Color BackColor
	{
		get
		{
			if (background_color.IsEmpty)
			{
				if (parent != null)
				{
					Color backColor = parent.BackColor;
					if (backColor.A == byte.MaxValue || GetStyle(ControlStyles.SupportsTransparentBackColor))
					{
						return backColor;
					}
				}
				return DefaultBackColor;
			}
			return background_color;
		}
		set
		{
			if (!value.IsEmpty && value.A != byte.MaxValue && !GetStyle(ControlStyles.SupportsTransparentBackColor))
			{
				throw new ArgumentException("Transparent background colors are not supported on this control");
			}
			if (background_color != value)
			{
				background_color = value;
				SetChildColor(this);
				OnBackColorChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the background image displayed in the control.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[Localizable(true)]
	[DefaultValue(null)]
	public virtual Image BackgroundImage
	{
		get
		{
			return background_image;
		}
		set
		{
			if (background_image != value)
			{
				background_image = value;
				OnBackgroundImageChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
	/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (<see cref="F:System.Windows.Forms.ImageLayout.Center" /> , <see cref="F:System.Windows.Forms.ImageLayout.None" />, <see cref="F:System.Windows.Forms.ImageLayout.Stretch" />, <see cref="F:System.Windows.Forms.ImageLayout.Tile" />, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom" />). <see cref="F:System.Windows.Forms.ImageLayout.Tile" /> is the default value.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[MWFCategory("Appearance")]
	[DefaultValue(ImageLayout.Tile)]
	public virtual ImageLayout BackgroundImageLayout
	{
		get
		{
			return backgroundimage_layout;
		}
		set
		{
			if (Array.IndexOf(Enum.GetValues(typeof(ImageLayout)), value) == -1)
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(ImageLayout));
			}
			if (value != backgroundimage_layout)
			{
				backgroundimage_layout = value;
				Invalidate();
				OnBackgroundImageLayoutChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public virtual BindingContext BindingContext
	{
		get
		{
			if (binding_context != null)
			{
				return binding_context;
			}
			if (Parent == null)
			{
				return null;
			}
			binding_context = Parent.BindingContext;
			return binding_context;
		}
		set
		{
			if (binding_context != value)
			{
				binding_context = value;
				OnBindingContextChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Bottom => bounds.Y + bounds.Height;

	/// <summary>Gets or sets the size and location of the control including its nonclient elements, in pixels, relative to the parent control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> in pixels relative to the parent control that represents the size and location of the control including its nonclient elements.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public Rectangle Bounds
	{
		get
		{
			return bounds;
		}
		set
		{
			SetBounds(value.Left, value.Top, value.Width, value.Height, BoundsSpecified.All);
		}
	}

	/// <summary>Gets a value indicating whether the control can receive focus.</summary>
	/// <returns>true if the control can receive focus; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanFocus
	{
		get
		{
			if (IsHandleCreated && Visible && Enabled)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the control can be selected.</summary>
	/// <returns>true if the control can be selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanSelect
	{
		get
		{
			if (!GetStyle(ControlStyles.Selectable))
			{
				return false;
			}
			for (Control control = this; control != null; control = control.parent)
			{
				if (!control.is_visible || !control.is_enabled)
				{
					return false;
				}
			}
			return true;
		}
	}

	internal virtual bool InternalCapture
	{
		get
		{
			return Capture;
		}
		set
		{
			Capture = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control has captured the mouse.</summary>
	/// <returns>true if the control has captured the mouse; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool Capture
	{
		get
		{
			return is_captured;
		}
		set
		{
			if (value == is_captured)
			{
				return;
			}
			if (value)
			{
				is_captured = true;
				XplatUI.GrabWindow(Handle, IntPtr.Zero);
				return;
			}
			if (IsHandleCreated)
			{
				XplatUI.UngrabWindow(Handle);
			}
			is_captured = false;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.</summary>
	/// <returns>true if the control causes validation to be performed on any controls requiring validation when it receives focus; otherwise, false. The default is true.</returns>
	/// <filterpriority>2</filterpriority>
	[MWFCategory("Focus")]
	[DefaultValue(true)]
	public bool CausesValidation
	{
		get
		{
			return causes_validation;
		}
		set
		{
			if (causes_validation != value)
			{
				causes_validation = value;
				OnCausesValidationChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the rectangle that represents the client area of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the client area of the control.</returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Rectangle ClientRectangle
	{
		get
		{
			client_rect.Width = client_size.Width;
			client_rect.Height = client_size.Height;
			return client_rect;
		}
	}

	/// <summary>Gets or sets the height and width of the client area of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the dimensions of the client area of the control.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public Size ClientSize
	{
		get
		{
			return client_size;
		}
		set
		{
			SetClientSizeCore(value.Width, value.Height);
			OnClientSizeChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets the name of the company or creator of the application containing the control.</summary>
	/// <returns>The company name or creator of the application containing the control.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Description("ControlCompanyNameDescr")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string CompanyName => "Mono Project, Novell, Inc.";

	/// <summary>Gets a value indicating whether the control, or one of its child controls, currently has the input focus.</summary>
	/// <returns>true if the control or one of its child controls currently has the input focus; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool ContainsFocus
	{
		get
		{
			IntPtr focus = XplatUI.GetFocus();
			if (IsHandleCreated)
			{
				if (focus == Handle)
				{
					return true;
				}
				for (int i = 0; i < child_controls.Count; i++)
				{
					if (child_controls[i].ContainsFocus)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	/// <summary>Gets or sets the shortcut menu associated with the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> that represents the shortcut menu associated with the control.</returns>
	/// <filterpriority>2</filterpriority>
	[MWFCategory("Behavior")]
	[Browsable(false)]
	[DefaultValue(null)]
	public virtual ContextMenu ContextMenu
	{
		get
		{
			return ContextMenuInternal;
		}
		set
		{
			ContextMenuInternal = value;
		}
	}

	internal virtual ContextMenu ContextMenuInternal
	{
		get
		{
			return context_menu;
		}
		set
		{
			if (context_menu != value)
			{
				context_menu = value;
				OnContextMenuChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or null if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is null.</returns>
	[MWFCategory("Behavior")]
	[DefaultValue(null)]
	public virtual ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return context_menu_strip;
		}
		set
		{
			if (context_menu_strip != value)
			{
				context_menu_strip = value;
				if (value != null)
				{
					value.container = this;
				}
				OnContextMenuStripChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the collection of controls contained within the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" /> representing the collection of controls contained within the control.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ControlCollection Controls => child_controls;

	/// <summary>Gets a value indicating whether the control has been created.</summary>
	/// <returns>true if the control has been created; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public bool Created => !is_disposed && is_created;

	/// <summary>Gets or sets the cursor that is displayed when the mouse pointer is over the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[AmbientValue(null)]
	[MWFCategory("Appearance")]
	public virtual Cursor Cursor
	{
		get
		{
			if (use_wait_cursor)
			{
				return Cursors.WaitCursor;
			}
			if (cursor != null)
			{
				return cursor;
			}
			if (parent != null)
			{
				return parent.Cursor;
			}
			return Cursors.Default;
		}
		set
		{
			if (!(cursor == value))
			{
				cursor = value;
				UpdateCursor();
				OnCursorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the data bindings for the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects for the control.</returns>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Data")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[ParenthesizePropertyName(true)]
	[RefreshProperties(RefreshProperties.All)]
	public ControlBindingsCollection DataBindings
	{
		get
		{
			if (data_bindings == null)
			{
				data_bindings = new ControlBindingsCollection(this);
			}
			return data_bindings;
		}
	}

	/// <summary>Gets the rectangle that represents the display area of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual Rectangle DisplayRectangle => ClientRectangle;

	/// <summary>Gets a value indicating whether the base <see cref="T:System.Windows.Forms.Control" /> class is in the process of disposing.</summary>
	/// <returns>true if the base <see cref="T:System.Windows.Forms.Control" /> class is in the process of disposing; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool Disposing => is_disposed;

	/// <summary>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[MWFCategory("Layout")]
	[DefaultValue(DockStyle.None)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public virtual DockStyle Dock
	{
		get
		{
			return dock_style;
		}
		set
		{
			if (value != 0)
			{
				layout_type = LayoutType.Dock;
			}
			if (dock_style != value)
			{
				if (!Enum.IsDefined(typeof(DockStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				dock_style = value;
				anchor_style = AnchorStyles.Top | AnchorStyles.Left;
				if (dock_style == DockStyle.None)
				{
					bounds = explicit_bounds;
					layout_type = LayoutType.Anchor;
				}
				if (parent != null)
				{
					parent.PerformLayout(this, "Dock");
				}
				else if (Controls.Count > 0)
				{
					PerformLayout();
				}
				OnDockChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.</summary>
	/// <returns>true if the surface of the control should be drawn using double buffering; otherwise, false.</returns>
	protected virtual bool DoubleBuffered
	{
		get
		{
			return (control_style & ControlStyles.OptimizedDoubleBuffer) != 0;
		}
		set
		{
			if (value != DoubleBuffered)
			{
				if (value)
				{
					SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
				}
				else
				{
					SetStyle(ControlStyles.OptimizedDoubleBuffer, value: false);
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
	/// <returns>true if the control can respond to user interaction; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[MWFCategory("Behavior")]
	[DispId(-514)]
	public bool Enabled
	{
		get
		{
			if (!is_enabled)
			{
				return false;
			}
			if (parent != null)
			{
				return parent.Enabled;
			}
			return true;
		}
		set
		{
			if (is_enabled != value)
			{
				bool flag = is_enabled;
				is_enabled = value;
				if (!value)
				{
					UpdateCursor();
				}
				if (flag != value && !value && has_focus)
				{
					SelectNextControl(this, forward: true, tabStopOnly: true, nested: true, wrap: true);
				}
				OnEnabledChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets a value indicating whether the control has input focus.</summary>
	/// <returns>true if the control has focus; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual bool Focused => has_focus;

	/// <summary>Gets or sets the font of the text displayed by the control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[Localizable(true)]
	[DispId(-512)]
	[AmbientValue(null)]
	public virtual Font Font
	{
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Drawing.Font, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		get
		{
			if (this.font != null)
			{
				return this.font;
			}
			if (parent != null)
			{
				Font font = parent.Font;
				if (font != null)
				{
					return font;
				}
			}
			return DefaultFont;
		}
		[param: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Drawing.Font, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		set
		{
			if (font == null || font != value)
			{
				font = value;
				Invalidate();
				OnFontChanged(EventArgs.Empty);
				PerformLayout();
			}
		}
	}

	/// <summary>Gets or sets the foreground color of the control.</summary>
	/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DispId(-513)]
	[MWFCategory("Appearance")]
	public virtual Color ForeColor
	{
		get
		{
			if (foreground_color.IsEmpty)
			{
				if (parent != null)
				{
					return parent.ForeColor;
				}
				return DefaultForeColor;
			}
			return foreground_color;
		}
		set
		{
			if (foreground_color != value)
			{
				foreground_color = value;
				Invalidate();
				OnForeColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the window handle that the control is bound to.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle (HWND) of the control.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DispId(-515)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IntPtr Handle
	{
		get
		{
			if (verify_thread_handle && InvokeRequired)
			{
				throw new InvalidOperationException("Cross-thread access of handle detected. Handle access only valid on thread that created the control");
			}
			if (!IsHandleCreated)
			{
				CreateHandle();
			}
			return window.Handle;
		}
	}

	/// <summary>Gets a value indicating whether the control contains one or more child controls.</summary>
	/// <returns>true if the control contains one or more child controls; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool HasChildren
	{
		get
		{
			if (child_controls.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets or sets the height of the control.</summary>
	/// <returns>The height of the control in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public int Height
	{
		get
		{
			return bounds.Height;
		}
		set
		{
			SetBounds(bounds.X, bounds.Y, bounds.Width, value, BoundsSpecified.Height);
		}
	}

	/// <summary>Gets or sets the Input Method Editor (IME) mode of the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode" /> enumeration values. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[MWFCategory("Behavior")]
	[AmbientValue(ImeMode.Inherit)]
	public ImeMode ImeMode
	{
		get
		{
			if (ime_mode == ImeMode.Inherit)
			{
				if (parent != null)
				{
					return parent.ImeMode;
				}
				return ImeMode.NoControl;
			}
			return ime_mode;
		}
		set
		{
			if (ime_mode != value)
			{
				ime_mode = value;
				OnImeModeChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets a value indicating whether the caller must call an invoke method when making method calls to the control because the caller is on a different thread than the one the control was created on.</summary>
	/// <returns>true if the control's <see cref="P:System.Windows.Forms.Control.Handle" /> was created on a different thread than the calling thread (indicating that you must make calls to the control through an invoke method); otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool InvokeRequired
	{
		get
		{
			if (creator_thread != null && creator_thread != Thread.CurrentThread)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is visible to accessibility applications.</summary>
	/// <returns>true if the control is visible to accessibility applications; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool IsAccessible
	{
		get
		{
			return is_accessible;
		}
		set
		{
			is_accessible = value;
		}
	}

	/// <summary>Gets a value indicating whether the control has been disposed of.</summary>
	/// <returns>true if the control has been disposed of; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool IsDisposed => is_disposed;

	/// <summary>Gets a value indicating whether the control has a handle associated with it.</summary>
	/// <returns>true if a handle has been assigned to the control; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool IsHandleCreated
	{
		get
		{
			if (window == null || window.Handle == IntPtr.Zero)
			{
				return false;
			}
			Hwnd hwnd = Hwnd.ObjectFromHandle(window.Handle);
			if (hwnd != null && hwnd.zombie)
			{
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets a value indicating whether the control is mirrored.</summary>
	/// <returns>true if the control is mirrored; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoNotSupported("RTL is not supported")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool IsMirrored => false;

	/// <summary>Gets a cached instance of the control's layout engine.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual LayoutEngine LayoutEngine
	{
		get
		{
			if (layout_engine == null)
			{
				layout_engine = new DefaultLayout();
			}
			return layout_engine;
		}
	}

	/// <summary>Gets or sets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public int Left
	{
		get
		{
			return bounds.X;
		}
		set
		{
			SetBounds(value, bounds.Y, bounds.Width, bounds.Height, BoundsSpecified.X);
		}
	}

	/// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
	/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[MWFCategory("Layout")]
	public Point Location
	{
		get
		{
			return new Point(bounds.X, bounds.Y);
		}
		set
		{
			SetBounds(value.X, value.Y, bounds.Width, bounds.Height, BoundsSpecified.Location);
		}
	}

	/// <summary>Gets or sets the space between controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
	/// <filterpriority>2</filterpriority>
	[Localizable(true)]
	[MWFCategory("Layout")]
	public Padding Margin
	{
		get
		{
			return margin;
		}
		set
		{
			if (margin != value)
			{
				margin = value;
				if (Parent != null)
				{
					Parent.PerformLayout(this, "Margin");
				}
				OnMarginChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the name of the control.</summary>
	/// <returns>The name of the control. The default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
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

	/// <summary>Gets or sets padding within the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Layout")]
	[Localizable(true)]
	public Padding Padding
	{
		get
		{
			return padding;
		}
		set
		{
			if (padding != value)
			{
				padding = value;
				OnPaddingChanged(EventArgs.Empty);
				if (AutoSize && Parent != null)
				{
					parent.PerformLayout(this, "Padding");
				}
				else
				{
					PerformLayout(this, "Padding");
				}
			}
		}
	}

	/// <summary>Gets or sets the parent container of the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the parent or container control of the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control Parent
	{
		get
		{
			return parent;
		}
		set
		{
			if (value == this)
			{
				throw new ArgumentException("A circular control reference has been made. A control cannot be owned or parented to itself.");
			}
			if (parent != value)
			{
				if (value == null)
				{
					parent.Controls.Remove(this);
					parent = null;
				}
				else
				{
					value.Controls.Add(this);
				}
			}
		}
	}

	/// <summary>Gets the size of a rectangular area into which the control can fit.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> containing the height and width, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Size PreferredSize => GetPreferredSize(Size.Empty);

	/// <summary>Gets the product name of the assembly containing the control.</summary>
	/// <returns>The product name of the assembly containing the control.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string ProductName
	{
		get
		{
			Type typeFromHandle = typeof(AssemblyProductAttribute);
			Assembly assembly = GetType().Module.Assembly;
			object[] customAttributes = assembly.GetCustomAttributes(typeFromHandle, inherit: false);
			AssemblyProductAttribute assemblyProductAttribute = null;
			if (customAttributes != null && customAttributes.Length > 0)
			{
				assemblyProductAttribute = (AssemblyProductAttribute)customAttributes[0];
			}
			if (assemblyProductAttribute == null)
			{
				return GetType().Namespace;
			}
			return assemblyProductAttribute.Product;
		}
	}

	/// <summary>Gets the version of the assembly containing the control.</summary>
	/// <returns>The file version of the assembly containing the control.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public string ProductVersion
	{
		get
		{
			Type typeFromHandle = typeof(AssemblyVersionAttribute);
			Assembly assembly = GetType().Module.Assembly;
			object[] customAttributes = assembly.GetCustomAttributes(typeFromHandle, inherit: false);
			if (customAttributes == null || customAttributes.Length < 1)
			{
				return "1.0.0.0";
			}
			return ((AssemblyVersionAttribute)customAttributes[0]).Version;
		}
	}

	/// <summary>Gets a value indicating whether the control is currently re-creating its handle.</summary>
	/// <returns>true if the control is currently re-creating its handle; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool RecreatingHandle => is_recreating;

	/// <summary>Gets or sets the window region associated with the control.</summary>
	/// <returns>The window <see cref="T:System.Drawing.Region" /> associated with the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Region Region
	{
		get
		{
			return clip_region;
		}
		set
		{
			if (clip_region != value)
			{
				if (IsHandleCreated)
				{
					XplatUI.SetClipRegion(Handle, value);
				}
				clip_region = value;
				OnRegionChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int Right => bounds.X + bounds.Width;

	/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[Localizable(true)]
	[AmbientValue(RightToLeft.Inherit)]
	public virtual RightToLeft RightToLeft
	{
		get
		{
			if (right_to_left == RightToLeft.Inherit)
			{
				if (parent != null)
				{
					return parent.RightToLeft;
				}
				return RightToLeft.No;
			}
			return right_to_left;
		}
		set
		{
			if (value != right_to_left)
			{
				right_to_left = value;
				OnRightToLeftChanged(EventArgs.Empty);
				PerformLayout();
			}
		}
	}

	/// <summary>Gets or sets the site of the control.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public override ISite Site
	{
		get
		{
			return base.Site;
		}
		set
		{
			base.Site = value;
			if (value != null)
			{
				AmbientProperties ambientProperties = (AmbientProperties)value.GetService(typeof(AmbientProperties));
				if (ambientProperties != null)
				{
					BackColor = ambientProperties.BackColor;
					ForeColor = ambientProperties.ForeColor;
					Cursor = ambientProperties.Cursor;
					Font = ambientProperties.Font;
				}
			}
		}
	}

	/// <summary>Gets or sets the height and width of the control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" /> that represents the height and width of the control in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[MWFCategory("Layout")]
	public Size Size
	{
		get
		{
			return new Size(Width, Height);
		}
		set
		{
			SetBounds(bounds.X, bounds.Y, value.Width, value.Height, BoundsSpecified.Size);
		}
	}

	/// <summary>Gets or sets the tab order of the control within its container.</summary>
	/// <returns>The index value of the control within the set of controls within its container. The controls in the container are included in the tab order.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[MergableProperty(false)]
	[MWFCategory("Behavior")]
	public int TabIndex
	{
		get
		{
			if (tab_index != -1)
			{
				return tab_index;
			}
			return 0;
		}
		set
		{
			if (tab_index != value)
			{
				tab_index = value;
				OnTabIndexChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
	/// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.Note:This property will always return true for an instance of the <see cref="T:System.Windows.Forms.Form" /> class.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DispId(-516)]
	[MWFCategory("Behavior")]
	[DefaultValue(true)]
	public bool TabStop
	{
		get
		{
			return tab_stop;
		}
		set
		{
			if (tab_stop != value)
			{
				tab_stop = value;
				OnTabStopChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the object that contains data about the control.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
	/// <filterpriority>2</filterpriority>
	[Bindable(true)]
	[MWFCategory("Data")]
	[DefaultValue(null)]
	[TypeConverter(typeof(StringConverter))]
	[Localizable(false)]
	public object Tag
	{
		get
		{
			return control_tag;
		}
		set
		{
			control_tag = value;
		}
	}

	/// <summary>Gets or sets the text associated with this control.</summary>
	/// <returns>The text associated with this control.</returns>
	/// <filterpriority>1</filterpriority>
	[DispId(-517)]
	[Localizable(true)]
	[Bindable(true)]
	[MWFCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (text != value)
			{
				text = value;
				UpdateWindowText();
				OnTextChanged(EventArgs.Empty);
				if (AutoSize && Parent != null && !(this is Label))
				{
					Parent.PerformLayout(this, "Text");
				}
			}
		}
	}

	/// <summary>Gets or sets the distance, in pixels, between the top edge of the control and the top edge of its container's client area.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Top
	{
		get
		{
			return bounds.Y;
		}
		set
		{
			SetBounds(bounds.X, value, bounds.Width, bounds.Height, BoundsSpecified.Y);
		}
	}

	/// <summary>Gets the parent control that is not parented by another Windows Forms control. Typically, this is the outermost <see cref="T:System.Windows.Forms.Form" /> that the control is contained in.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the top-level control that contains the current control.</returns>
	/// <filterpriority>2</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public Control TopLevelControl
	{
		get
		{
			Control control = this;
			while (control.parent != null)
			{
				control = control.parent;
			}
			return (!(control is Form)) ? null : control;
		}
	}

	/// <summary>Gets or sets a value indicating whether to use the wait cursor for the current control and all child controls.</summary>
	/// <returns>true to use the wait cursor for the current control and all child controls; otherwise, false. The default is false.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(false)]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[MWFCategory("Appearance")]
	public bool UseWaitCursor
	{
		get
		{
			return use_wait_cursor;
		}
		set
		{
			if (use_wait_cursor != value)
			{
				use_wait_cursor = value;
				UpdateCursor();
				OnCursorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control and all its child controls are displayed.</summary>
	/// <returns>true if the control and all its child controls are displayed; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[MWFCategory("Behavior")]
	public bool Visible
	{
		get
		{
			if (!is_visible)
			{
				return false;
			}
			if (parent != null)
			{
				return parent.Visible;
			}
			return true;
		}
		set
		{
			if (is_visible != value)
			{
				SetVisibleCore(value);
				if (parent != null)
				{
					parent.PerformLayout(this, "Visible");
				}
			}
		}
	}

	/// <summary>Gets or sets the width of the control.</summary>
	/// <returns>The width of the control in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Width
	{
		get
		{
			return bounds.Width;
		}
		set
		{
			SetBounds(bounds.X, bounds.Y, value, bounds.Height, BoundsSpecified.Width);
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.IWindowTarget" />.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public IWindowTarget WindowTarget
	{
		get
		{
			return window_target;
		}
		set
		{
			window_target = value;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
	/// <returns>true in all cases.</returns>
	protected virtual bool CanEnableIme => false;

	/// <summary>Determines if events can be raised on the control.</summary>
	/// <returns>true if the control is hosted as an ActiveX control whose events are not frozen; otherwise, false.</returns>
	protected override bool CanRaiseEvents => true;

	/// <summary>Gets the required creation parameters when the control handle is created.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected virtual CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = new CreateParams();
			try
			{
				createParams.Caption = Text;
			}
			catch
			{
				createParams.Caption = text;
			}
			try
			{
				createParams.X = Left;
			}
			catch
			{
				createParams.X = bounds.X;
			}
			try
			{
				createParams.Y = Top;
			}
			catch
			{
				createParams.Y = bounds.Y;
			}
			try
			{
				createParams.Width = Width;
			}
			catch
			{
				createParams.Width = bounds.Width;
			}
			try
			{
				createParams.Height = Height;
			}
			catch
			{
				createParams.Height = bounds.Height;
			}
			createParams.ClassName = XplatUI.DefaultClassName;
			createParams.ClassStyle = 40;
			createParams.ExStyle = 0;
			createParams.Param = 0;
			if (allow_drop)
			{
				createParams.ExStyle |= 16;
			}
			if (parent != null && parent.IsHandleCreated)
			{
				createParams.Parent = parent.Handle;
			}
			createParams.Style = 1174405120;
			if (is_visible)
			{
				createParams.Style |= 268435456;
			}
			if (!is_enabled)
			{
				createParams.Style |= 134217728;
			}
			switch (border_style)
			{
			case BorderStyle.FixedSingle:
				createParams.Style |= 8388608;
				break;
			case BorderStyle.Fixed3D:
				createParams.ExStyle |= 512;
				break;
			}
			createParams.control = this;
			return createParams;
		}
	}

	/// <summary>Gets or sets the default cursor for the control.</summary>
	/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
	protected virtual Cursor DefaultCursor => Cursors.Default;

	/// <summary>Gets the default Input Method Editor (IME) mode supported by the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	protected virtual ImeMode DefaultImeMode => ImeMode.Inherit;

	/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
	protected virtual Padding DefaultMargin => new Padding(3);

	/// <summary>Gets the length and height, in pixels, that is specified as the default maximum size of a control.</summary>
	/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> representing the size of the control.</returns>
	protected virtual Size DefaultMaximumSize => default(Size);

	/// <summary>Gets the length and height, in pixels, that is specified as the default minimum size of a control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the size of the control.</returns>
	protected virtual Size DefaultMinimumSize => default(Size);

	/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
	protected virtual Padding DefaultPadding => default(Padding);

	/// <summary>Gets the default size of the control.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected virtual Size DefaultSize => new Size(0, 0);

	/// <summary>Gets or sets the height of the font of the control.</summary>
	/// <returns>The height of the <see cref="T:System.Drawing.Font" /> of the control in pixels.</returns>
	protected int FontHeight
	{
		get
		{
			return Font.Height;
		}
		set
		{
		}
	}

	/// <summary>This property is now obsolete.</summary>
	/// <returns>true if the control is rendered from right to left; otherwise, false. The default is false.</returns>
	[Obsolete]
	protected bool RenderRightToLeft => right_to_left == RightToLeft.Yes;

	/// <summary>Gets or sets a value indicating whether the control redraws itself when resized.</summary>
	/// <returns>true if the control redraws itself when resized; otherwise, false.</returns>
	protected bool ResizeRedraw
	{
		get
		{
			return GetStyle(ControlStyles.ResizeRedraw);
		}
		set
		{
			SetStyle(ControlStyles.ResizeRedraw, value);
		}
	}

	/// <summary>Gets a value that determines the scaling of child controls. </summary>
	/// <returns>true if child controls will be scaled when the <see cref="M:System.Windows.Forms.Control.Scale(System.Single)" /> method on this control is called; otherwise, false. The default is true.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual bool ScaleChildren => ScaleChildrenInternal;

	internal virtual bool ScaleChildrenInternal => true;

	/// <summary>Gets a value indicating whether the control should display focus rectangles.</summary>
	/// <returns>true if the control should display focus rectangles; otherwise, false.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected internal virtual bool ShowFocusCues
	{
		get
		{
			if (this is Form)
			{
				return show_focus_cues;
			}
			if (parent == null)
			{
				return false;
			}
			return FindForm()?.show_focus_cues ?? false;
		}
	}

	/// <summary>Gets a value indicating whether the user interface is in the appropriate state to show or hide keyboard accelerators.</summary>
	/// <returns>true if the keyboard accelerators are visible; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal virtual bool ShowKeyboardCues => ShowKeyboardCuesInternal;

	internal bool ShowKeyboardCuesInternal
	{
		get
		{
			if (SystemInformation.MenuAccessKeysUnderlined || base.DesignMode)
			{
				return true;
			}
			return show_keyboard_cues;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public event EventHandler AutoSizeChanged
	{
		add
		{
			base.Events.AddHandler(AutoSizeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AutoSizeChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler BackColorChanged
	{
		add
		{
			base.Events.AddHandler(BackColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BackColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler BackgroundImageChanged
	{
		add
		{
			base.Events.AddHandler(BackgroundImageChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BackgroundImageChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.BackgroundImageLayout" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			base.Events.AddHandler(BackgroundImageLayoutChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BackgroundImageLayoutChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="T:System.Windows.Forms.BindingContext" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler BindingContextChanged
	{
		add
		{
			base.Events.AddHandler(BindingContextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BindingContextChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.CausesValidation" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler CausesValidationChanged
	{
		add
		{
			base.Events.AddHandler(CausesValidationChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CausesValidationChangedEvent, value);
		}
	}

	/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
	/// <filterpriority>1</filterpriority>
	public event UICuesEventHandler ChangeUICues
	{
		add
		{
			base.Events.AddHandler(ChangeUICuesEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ChangeUICuesEvent, value);
		}
	}

	/// <summary>Occurs when the control is clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Click
	{
		add
		{
			base.Events.AddHandler(ClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClickEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ClientSize" /> property changes. </summary>
	public event EventHandler ClientSizeChanged
	{
		add
		{
			base.Events.AddHandler(ClientSizeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClientSizeChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenu" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public event EventHandler ContextMenuChanged
	{
		add
		{
			base.Events.AddHandler(ContextMenuChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ContextMenuChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenuStrip" /> property changes. </summary>
	public event EventHandler ContextMenuStripChanged
	{
		add
		{
			base.Events.AddHandler(ContextMenuStripChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ContextMenuStripChangedEvent, value);
		}
	}

	/// <summary>Occurs when a new control is added to the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event ControlEventHandler ControlAdded
	{
		add
		{
			base.Events.AddHandler(ControlAddedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ControlAddedEvent, value);
		}
	}

	/// <summary>Occurs when a control is removed from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(true)]
	public event ControlEventHandler ControlRemoved
	{
		add
		{
			base.Events.AddHandler(ControlRemovedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ControlRemovedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Cursor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[MWFDescription("Fired when the cursor for the control has been changed")]
	[MWFCategory("PropertyChanged")]
	public event EventHandler CursorChanged
	{
		add
		{
			base.Events.AddHandler(CursorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CursorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Dock" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DockChanged
	{
		add
		{
			base.Events.AddHandler(DockChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DockChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control is double-clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DoubleClick
	{
		add
		{
			base.Events.AddHandler(DoubleClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DoubleClickEvent, value);
		}
	}

	/// <summary>Occurs when a drag-and-drop operation is completed.</summary>
	/// <filterpriority>1</filterpriority>
	public event DragEventHandler DragDrop
	{
		add
		{
			base.Events.AddHandler(DragDropEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragDropEvent, value);
		}
	}

	/// <summary>Occurs when an object is dragged into the control's bounds.</summary>
	/// <filterpriority>1</filterpriority>
	public event DragEventHandler DragEnter
	{
		add
		{
			base.Events.AddHandler(DragEnterEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragEnterEvent, value);
		}
	}

	/// <summary>Occurs when an object is dragged out of the control's bounds.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DragLeave
	{
		add
		{
			base.Events.AddHandler(DragLeaveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragLeaveEvent, value);
		}
	}

	/// <summary>Occurs when an object is dragged over the control's bounds.</summary>
	/// <filterpriority>1</filterpriority>
	public event DragEventHandler DragOver
	{
		add
		{
			base.Events.AddHandler(DragOverEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragOverEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler EnabledChanged
	{
		add
		{
			base.Events.AddHandler(EnabledChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(EnabledChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control is entered.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Enter
	{
		add
		{
			base.Events.AddHandler(EnterEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(EnterEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Font" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler FontChanged
	{
		add
		{
			base.Events.AddHandler(FontChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FontChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ForeColorChanged
	{
		add
		{
			base.Events.AddHandler(ForeColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ForeColorChangedEvent, value);
		}
	}

	/// <summary>Occurs during a drag operation.</summary>
	/// <filterpriority>1</filterpriority>
	public event GiveFeedbackEventHandler GiveFeedback
	{
		add
		{
			base.Events.AddHandler(GiveFeedbackEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(GiveFeedbackEvent, value);
		}
	}

	/// <summary>Occurs when the control receives focus.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler GotFocus
	{
		add
		{
			base.Events.AddHandler(GotFocusEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(GotFocusEvent, value);
		}
	}

	/// <summary>Occurs when a handle is created for the control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler HandleCreated
	{
		add
		{
			base.Events.AddHandler(HandleCreatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HandleCreatedEvent, value);
		}
	}

	/// <summary>Occurs when the control's handle is in the process of being destroyed.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public event EventHandler HandleDestroyed
	{
		add
		{
			base.Events.AddHandler(HandleDestroyedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HandleDestroyedEvent, value);
		}
	}

	/// <summary>Occurs when the user requests help for a control.</summary>
	/// <filterpriority>1</filterpriority>
	public event HelpEventHandler HelpRequested
	{
		add
		{
			base.Events.AddHandler(HelpRequestedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HelpRequestedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ImeModeChanged
	{
		add
		{
			base.Events.AddHandler(ImeModeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ImeModeChangedEvent, value);
		}
	}

	/// <summary>Occurs when a control's display requires redrawing.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event InvalidateEventHandler Invalidated
	{
		add
		{
			base.Events.AddHandler(InvalidatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(InvalidatedEvent, value);
		}
	}

	/// <summary>Occurs when a key is pressed while the control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	public event KeyEventHandler KeyDown
	{
		add
		{
			base.Events.AddHandler(KeyDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(KeyDownEvent, value);
		}
	}

	/// <summary>Occurs when a key is pressed while the control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	public event KeyPressEventHandler KeyPress
	{
		add
		{
			base.Events.AddHandler(KeyPressEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(KeyPressEvent, value);
		}
	}

	/// <summary>Occurs when a key is released while the control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	public event KeyEventHandler KeyUp
	{
		add
		{
			base.Events.AddHandler(KeyUpEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(KeyUpEvent, value);
		}
	}

	/// <summary>Occurs when a control should reposition its child controls.</summary>
	/// <filterpriority>1</filterpriority>
	public event LayoutEventHandler Layout
	{
		add
		{
			base.Events.AddHandler(LayoutEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LayoutEvent, value);
		}
	}

	/// <summary>Occurs when the input focus leaves the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Leave
	{
		add
		{
			base.Events.AddHandler(LeaveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LeaveEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Location" /> property value has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler LocationChanged
	{
		add
		{
			base.Events.AddHandler(LocationChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LocationChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control loses focus.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public event EventHandler LostFocus
	{
		add
		{
			base.Events.AddHandler(LostFocusEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LostFocusEvent, value);
		}
	}

	/// <summary>Occurs when the control's margin changes.</summary>
	public event EventHandler MarginChanged
	{
		add
		{
			base.Events.AddHandler(MarginChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MarginChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control loses mouse capture.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MouseCaptureChanged
	{
		add
		{
			base.Events.AddHandler(MouseCaptureChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseCaptureChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control is clicked by the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseClick
	{
		add
		{
			base.Events.AddHandler(MouseClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseClickEvent, value);
		}
	}

	/// <summary>Occurs when the control is double clicked by the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseDoubleClick
	{
		add
		{
			base.Events.AddHandler(MouseDoubleClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseDoubleClickEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer is over the control and a mouse button is pressed.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseDown
	{
		add
		{
			base.Events.AddHandler(MouseDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseDownEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer enters the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MouseEnter
	{
		add
		{
			base.Events.AddHandler(MouseEnterEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseEnterEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer rests on the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MouseHover
	{
		add
		{
			base.Events.AddHandler(MouseHoverEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseHoverEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer leaves the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MouseLeave
	{
		add
		{
			base.Events.AddHandler(MouseLeaveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseLeaveEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer is moved over the control.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseMove
	{
		add
		{
			base.Events.AddHandler(MouseMoveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseMoveEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer is over the control and a mouse button is released.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseUp
	{
		add
		{
			base.Events.AddHandler(MouseUpEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseUpEvent, value);
		}
	}

	/// <summary>Occurs when the mouse wheel moves while the control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event MouseEventHandler MouseWheel
	{
		add
		{
			base.Events.AddHandler(MouseWheelEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseWheelEvent, value);
		}
	}

	/// <summary>Occurs when the control is moved.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Move
	{
		add
		{
			base.Events.AddHandler(MoveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MoveEvent, value);
		}
	}

	/// <summary>Occurs when the control's padding changes.</summary>
	public event EventHandler PaddingChanged
	{
		add
		{
			base.Events.AddHandler(PaddingChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PaddingChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control is redrawn.</summary>
	/// <filterpriority>1</filterpriority>
	public event PaintEventHandler Paint
	{
		add
		{
			base.Events.AddHandler(PaintEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PaintEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Parent" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ParentChanged
	{
		add
		{
			base.Events.AddHandler(ParentChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ParentChangedEvent, value);
		}
	}

	/// <summary>Occurs before the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event when a key is pressed while focus is on this control.</summary>
	public event PreviewKeyDownEventHandler PreviewKeyDown
	{
		add
		{
			base.Events.AddHandler(PreviewKeyDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PreviewKeyDownEvent, value);
		}
	}

	/// <summary>Occurs when <see cref="T:System.Windows.Forms.AccessibleObject" /> is providing help to accessibility applications.</summary>
	/// <filterpriority>1</filterpriority>
	public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
	{
		add
		{
			base.Events.AddHandler(QueryAccessibilityHelpEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(QueryAccessibilityHelpEvent, value);
		}
	}

	/// <summary>Occurs during a drag-and-drop operation and enables the drag source to determine whether the drag-and-drop operation should be canceled.</summary>
	/// <filterpriority>1</filterpriority>
	public event QueryContinueDragEventHandler QueryContinueDrag
	{
		add
		{
			base.Events.AddHandler(QueryContinueDragEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(QueryContinueDragEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Region" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler RegionChanged
	{
		add
		{
			base.Events.AddHandler(RegionChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RegionChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control is resized.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler Resize
	{
		add
		{
			base.Events.AddHandler(ResizeEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ResizeEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler RightToLeftChanged
	{
		add
		{
			base.Events.AddHandler(RightToLeftChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RightToLeftChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Size" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SizeChanged
	{
		add
		{
			base.Events.AddHandler(SizeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SizeChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control style changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler StyleChanged
	{
		add
		{
			base.Events.AddHandler(StyleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(StyleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the system colors change.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SystemColorsChanged
	{
		add
		{
			base.Events.AddHandler(SystemColorsChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SystemColorsChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.TabIndex" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler TabIndexChanged
	{
		add
		{
			base.Events.AddHandler(TabIndexChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TabIndexChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.TabStop" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler TabStopChanged
	{
		add
		{
			base.Events.AddHandler(TabStopChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TabStopChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Text" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler TextChanged
	{
		add
		{
			base.Events.AddHandler(TextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control is finished validating.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Validated
	{
		add
		{
			base.Events.AddHandler(ValidatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValidatedEvent, value);
		}
	}

	/// <summary>Occurs when the control is validating.</summary>
	/// <filterpriority>1</filterpriority>
	public event CancelEventHandler Validating
	{
		add
		{
			base.Events.AddHandler(ValidatingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValidatingEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler VisibleChanged
	{
		add
		{
			base.Events.AddHandler(VisibleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(VisibleChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with default settings.</summary>
	public Control()
	{
		if (WindowsFormsSynchronizationContext.AutoInstall && !(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
		{
			SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
		}
		layout_type = LayoutType.Anchor;
		anchor_style = AnchorStyles.Top | AnchorStyles.Left;
		is_created = false;
		is_visible = true;
		is_captured = false;
		is_disposed = false;
		is_enabled = true;
		is_entered = false;
		layout_pending = false;
		is_toplevel = false;
		causes_validation = true;
		has_focus = false;
		layout_suspended = 0;
		mouse_clicks = 1;
		tab_index = -1;
		cursor = null;
		right_to_left = RightToLeft.Inherit;
		border_style = BorderStyle.None;
		background_color = Color.Empty;
		dist_right = 0;
		dist_bottom = 0;
		tab_stop = true;
		ime_mode = ImeMode.Inherit;
		use_compatible_text_rendering = true;
		show_keyboard_cues = false;
		show_focus_cues = SystemInformation.MenuAccessKeysUnderlined;
		use_wait_cursor = false;
		backgroundimage_layout = ImageLayout.Tile;
		use_compatible_text_rendering = Application.use_compatible_text_rendering;
		padding = DefaultPadding;
		maximum_size = default(Size);
		minimum_size = default(Size);
		margin = DefaultMargin;
		auto_size_mode = AutoSizeMode.GrowOnly;
		control_style = ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.StandardDoubleClick | ControlStyles.AllPaintingInWmPaint;
		control_style |= ControlStyles.UseTextForAccessibility;
		parent = null;
		background_image = null;
		text = string.Empty;
		name = string.Empty;
		window_target = new ControlWindowTarget(this);
		window = new ControlNativeWindow(this);
		child_controls = CreateControlsInstance();
		bounds.Size = DefaultSize;
		client_size = ClientSizeFromSize(bounds.Size);
		client_rect = new Rectangle(Point.Empty, client_size);
		explicit_bounds = bounds;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class as a child control, with specific text.</summary>
	/// <param name="parent">The <see cref="T:System.Windows.Forms.Control" /> to be the parent of the control. </param>
	/// <param name="text">The text displayed by the control. </param>
	public Control(Control parent, string text)
		: this()
	{
		Text = text;
		Parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class as a child control, with specific text, size, and location.</summary>
	/// <param name="parent">The <see cref="T:System.Windows.Forms.Control" /> to be the parent of the control. </param>
	/// <param name="text">The text displayed by the control. </param>
	/// <param name="left">The <see cref="P:System.Drawing.Point.X" /> position of the control, in pixels, from the left edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Left" /> property. </param>
	/// <param name="top">The <see cref="P:System.Drawing.Point.Y" /> position of the control, in pixels, from the top edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Top" /> property. </param>
	/// <param name="width">The width of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Width" /> property. </param>
	/// <param name="height">The height of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Height" /> property. </param>
	public Control(Control parent, string text, int left, int top, int width, int height)
		: this()
	{
		Parent = parent;
		SetBounds(left, top, width, height, BoundsSpecified.All);
		Text = text;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with specific text.</summary>
	/// <param name="text">The text displayed by the control. </param>
	public Control(string text)
		: this()
	{
		Text = text;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with specific text, size, and location.</summary>
	/// <param name="text">The text displayed by the control. </param>
	/// <param name="left">The <see cref="P:System.Drawing.Point.X" /> position of the control, in pixels, from the left edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Left" /> property. </param>
	/// <param name="top">The <see cref="P:System.Drawing.Point.Y" /> position of the control, in pixels, from the top edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Top" /> property. </param>
	/// <param name="width">The width of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Width" /> property. </param>
	/// <param name="height">The height of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Height" /> property. </param>
	public Control(string text, int left, int top, int width, int height)
		: this()
	{
		SetBounds(left, top, width, height, BoundsSpecified.All);
		Text = text;
	}

	static Control()
	{
		AutoSizeChanged = new object();
		BackColorChanged = new object();
		BackgroundImageChanged = new object();
		BackgroundImageLayoutChanged = new object();
		BindingContextChanged = new object();
		CausesValidationChanged = new object();
		ChangeUICues = new object();
		Click = new object();
		ClientSizeChanged = new object();
		ContextMenuChanged = new object();
		ContextMenuStripChanged = new object();
		ControlAdded = new object();
		ControlRemoved = new object();
		CursorChanged = new object();
		DockChanged = new object();
		DoubleClick = new object();
		DragDrop = new object();
		DragEnter = new object();
		DragLeave = new object();
		DragOver = new object();
		EnabledChanged = new object();
		Enter = new object();
		FontChanged = new object();
		ForeColorChanged = new object();
		GiveFeedback = new object();
		GotFocus = new object();
		HandleCreated = new object();
		HandleDestroyed = new object();
		HelpRequested = new object();
		ImeModeChanged = new object();
		Invalidated = new object();
		KeyDown = new object();
		KeyPress = new object();
		KeyUp = new object();
		Layout = new object();
		Leave = new object();
		LocationChanged = new object();
		LostFocus = new object();
		MarginChanged = new object();
		MouseCaptureChanged = new object();
		MouseClick = new object();
		MouseDoubleClick = new object();
		MouseDown = new object();
		MouseEnter = new object();
		MouseHover = new object();
		MouseLeave = new object();
		MouseMove = new object();
		MouseUp = new object();
		MouseWheel = new object();
		Move = new object();
		PaddingChanged = new object();
		Paint = new object();
		ParentChanged = new object();
		PreviewKeyDown = new object();
		QueryAccessibilityHelp = new object();
		QueryContinueDrag = new object();
		RegionChanged = new object();
		Resize = new object();
		RightToLeftChanged = new object();
		SizeChanged = new object();
		StyleChanged = new object();
		SystemColorsChanged = new object();
		TabIndexChanged = new object();
		TabStopChanged = new object();
		TextChanged = new object();
		Validated = new object();
		Validating = new object();
		VisibleChanged = new object();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
	/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	void IDropTarget.OnDragDrop(DragEventArgs drgEvent)
	{
		OnDragDrop(drgEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
	/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	void IDropTarget.OnDragEnter(DragEventArgs drgEvent)
	{
		OnDragEnter(drgEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	void IDropTarget.OnDragLeave(EventArgs e)
	{
		OnDragLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
	/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	void IDropTarget.OnDragOver(DragEventArgs drgEvent)
	{
		OnDragOver(drgEvent);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		if (!is_disposed && disposing)
		{
			is_disposing = true;
			Capture = false;
			DisposeBackBuffer();
			if (InvokeRequired)
			{
				if (Application.MessageLoop && IsHandleCreated)
				{
					BeginInvokeInternal(new MethodInvoker(DestroyHandle), null);
				}
			}
			else
			{
				DestroyHandle();
			}
			if (parent != null)
			{
				parent.Controls.Remove(this);
			}
			Control[] allControls = child_controls.GetAllControls();
			for (int i = 0; i < allControls.Length; i++)
			{
				allControls[i].parent = null;
				allControls[i].Dispose();
			}
		}
		is_disposed = true;
		base.Dispose(disposing);
	}

	internal IAsyncResult BeginInvokeInternal(Delegate method, object[] args)
	{
		return BeginInvokeInternal(method, args, FindControlToInvokeOn());
	}

	internal IAsyncResult BeginInvokeInternal(Delegate method, object[] args, Control control)
	{
		AsyncMethodResult result = new AsyncMethodResult();
		AsyncMethodData asyncMethodData = new AsyncMethodData();
		asyncMethodData.Handle = control.GetInvokableHandle();
		asyncMethodData.Method = method;
		asyncMethodData.Args = args;
		asyncMethodData.Result = result;
		if (!ExecutionContext.IsFlowSuppressed())
		{
			asyncMethodData.Context = ExecutionContext.Capture();
		}
		XplatUI.SendAsyncMethod(asyncMethodData);
		return result;
	}

	private IntPtr GetInvokableHandle()
	{
		if (!IsHandleCreated)
		{
			CreateHandle();
		}
		return window.Handle;
	}

	internal void PointToClient(ref int x, ref int y)
	{
		XplatUI.ScreenToClient(Handle, ref x, ref y);
	}

	internal void PointToScreen(ref int x, ref int y)
	{
		XplatUI.ClientToScreen(Handle, ref x, ref y);
	}

	internal virtual int OverrideHeight(int height)
	{
		return height;
	}

	private void ProcessActiveTracker(ref Message m)
	{
		bool flag = m.Msg == 514 || m.Msg == 517;
		MouseButtons mouseButtons = FromParamToMouseButtons(m.WParam.ToInt32());
		if (flag)
		{
			switch ((Msg)m.Msg)
			{
			case Msg.WM_LBUTTONUP:
				mouseButtons |= MouseButtons.Left;
				break;
			case Msg.WM_RBUTTONUP:
				mouseButtons |= MouseButtons.Right;
				break;
			}
		}
		MouseEventArgs args = new MouseEventArgs(mouseButtons, mouse_clicks, MousePosition.X, MousePosition.Y, 0);
		if (flag)
		{
			active_tracker.OnMouseUp(args);
			mouse_clicks = 1;
		}
		else if (!active_tracker.OnMouseDown(args))
		{
			Control realChildAtPoint = GetRealChildAtPoint(Cursor.Position);
			if (realChildAtPoint != null)
			{
				Point point = realChildAtPoint.PointToClient(Cursor.Position);
				XplatUI.SendMessage(realChildAtPoint.Handle, (Msg)m.Msg, m.WParam, MakeParam(point.X, point.Y));
			}
		}
	}

	private Control FindControlToInvokeOn()
	{
		Control control = this;
		while (!control.IsHandleCreated)
		{
			control = control.parent;
			if (control == null)
			{
				break;
			}
		}
		if (control == null || !control.IsHandleCreated)
		{
			throw new InvalidOperationException("Cannot call Invoke or BeginInvoke on a control until the window handle is created");
		}
		return control;
	}

	private void InvalidateBackBuffer()
	{
		if (backbuffer != null)
		{
			backbuffer.Invalidate();
		}
	}

	private DoubleBuffer GetBackBuffer()
	{
		if (backbuffer == null)
		{
			backbuffer = new DoubleBuffer(this);
		}
		return backbuffer;
	}

	private void DisposeBackBuffer()
	{
		if (backbuffer != null)
		{
			backbuffer.Dispose();
			backbuffer = null;
		}
	}

	internal static void SetChildColor(Control parent)
	{
		for (int i = 0; i < parent.child_controls.Count; i++)
		{
			Control control = parent.child_controls[i];
			if (control.child_controls.Count > 0)
			{
				SetChildColor(control);
			}
		}
	}

	internal bool Select(Control control)
	{
		if (control == null)
		{
			return false;
		}
		IContainerControl containerControl = GetContainerControl();
		if (containerControl != null && (Control)containerControl != control)
		{
			containerControl.ActiveControl = control;
			if (containerControl.ActiveControl == control && !control.has_focus && control.IsHandleCreated)
			{
				XplatUI.SetFocus(control.window.Handle);
			}
		}
		else if (control.IsHandleCreated)
		{
			XplatUI.SetFocus(control.window.Handle);
		}
		return true;
	}

	internal virtual void DoDefaultAction()
	{
	}

	internal static IntPtr MakeParam(int low, int high)
	{
		return new IntPtr((high << 16) | (low & 0xFFFF));
	}

	internal static int LowOrder(int param)
	{
		return (short)(param & 0xFFFF);
	}

	internal static int HighOrder(long param)
	{
		return (short)(param >> 16);
	}

	internal virtual void PaintControlBackground(PaintEventArgs pevent)
	{
		bool flag = (CreateParams.Style & 0x800) != 0;
		if (((BackColor.A != byte.MaxValue && GetStyle(ControlStyles.SupportsTransparentBackColor)) || flag) && parent != null)
		{
			PaintEventArgs paintEventArgs = new PaintEventArgs(pevent.Graphics, new Rectangle(pevent.ClipRectangle.X + Left, pevent.ClipRectangle.Y + Top, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height));
			GraphicsState gstate = paintEventArgs.Graphics.Save();
			paintEventArgs.Graphics.TranslateTransform(-Left, -Top);
			parent.OnPaintBackground(paintEventArgs);
			paintEventArgs.Graphics.Restore(gstate);
			gstate = paintEventArgs.Graphics.Save();
			paintEventArgs.Graphics.TranslateTransform(-Left, -Top);
			parent.OnPaint(paintEventArgs);
			paintEventArgs.Graphics.Restore(gstate);
			paintEventArgs.SetGraphics(null);
		}
		if (clip_region != null && XplatUI.UserClipWontExposeParent && parent != null)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(Handle);
			if (hwnd != null)
			{
				PaintEventArgs paintEventArgs2 = new PaintEventArgs(pevent.Graphics, new Rectangle(pevent.ClipRectangle.X + Left, pevent.ClipRectangle.Y + Top, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height));
				Region region = new Region();
				region.MakeEmpty();
				region.Union(ClientRectangle);
				Rectangle[] clipRectangles = hwnd.ClipRectangles;
				foreach (Rectangle rect in clipRectangles)
				{
					region.Union(rect);
				}
				GraphicsState gstate2 = paintEventArgs2.Graphics.Save();
				paintEventArgs2.Graphics.Clip = region;
				paintEventArgs2.Graphics.TranslateTransform(-Left, -Top);
				parent.OnPaintBackground(paintEventArgs2);
				paintEventArgs2.Graphics.Restore(gstate2);
				gstate2 = paintEventArgs2.Graphics.Save();
				paintEventArgs2.Graphics.Clip = region;
				paintEventArgs2.Graphics.TranslateTransform(-Left, -Top);
				parent.OnPaint(paintEventArgs2);
				paintEventArgs2.Graphics.Restore(gstate2);
				paintEventArgs2.SetGraphics(null);
				region.Intersect(clip_region);
				pevent.Graphics.Clip = region;
			}
		}
		if (background_image == null)
		{
			if (!flag)
			{
				Rectangle rect2 = new Rectangle(pevent.ClipRectangle.X, pevent.ClipRectangle.Y, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height);
				Brush solidBrush = ThemeEngine.Current.ResPool.GetSolidBrush(BackColor);
				pevent.Graphics.FillRectangle(solidBrush, rect2);
			}
		}
		else
		{
			DrawBackgroundImage(pevent.Graphics);
		}
	}

	private void DrawBackgroundImage(Graphics g)
	{
		Rectangle rect = default(Rectangle);
		g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(BackColor), ClientRectangle);
		switch (backgroundimage_layout)
		{
		default:
			return;
		case ImageLayout.Tile:
		{
			using TextureBrush brush = new TextureBrush(background_image, WrapMode.Tile);
			g.FillRectangle(brush, ClientRectangle);
			return;
		}
		case ImageLayout.Center:
			rect.Location = new Point(ClientSize.Width / 2 - background_image.Width / 2, ClientSize.Height / 2 - background_image.Height / 2);
			rect.Size = background_image.Size;
			break;
		case ImageLayout.None:
			rect.Location = Point.Empty;
			rect.Size = background_image.Size;
			break;
		case ImageLayout.Stretch:
			rect = ClientRectangle;
			break;
		case ImageLayout.Zoom:
			rect = ClientRectangle;
			if ((float)background_image.Width / (float)background_image.Height < (float)rect.Width / (float)rect.Height)
			{
				rect.Width = (int)((float)background_image.Width * ((float)rect.Height / (float)background_image.Height));
				rect.X = (ClientRectangle.Width - rect.Width) / 2;
			}
			else
			{
				rect.Height = (int)((float)background_image.Height * ((float)rect.Width / (float)background_image.Width));
				rect.Y = (ClientRectangle.Height - rect.Height) / 2;
			}
			break;
		}
		g.DrawImage(background_image, rect);
	}

	internal virtual void DndEnter(DragEventArgs e)
	{
		try
		{
			OnDragEnter(e);
		}
		catch
		{
		}
	}

	internal virtual void DndOver(DragEventArgs e)
	{
		try
		{
			OnDragOver(e);
		}
		catch
		{
		}
	}

	internal virtual void DndDrop(DragEventArgs e)
	{
		try
		{
			OnDragDrop(e);
		}
		catch (Exception value)
		{
			Console.Error.WriteLine("MWF: Exception while dropping:");
			Console.Error.WriteLine(value);
		}
	}

	internal virtual void DndLeave(EventArgs e)
	{
		try
		{
			OnDragLeave(e);
		}
		catch
		{
		}
	}

	internal virtual void DndFeedback(GiveFeedbackEventArgs e)
	{
		try
		{
			OnGiveFeedback(e);
		}
		catch
		{
		}
	}

	internal virtual void DndContinueDrag(QueryContinueDragEventArgs e)
	{
		try
		{
			OnQueryContinueDrag(e);
		}
		catch
		{
		}
	}

	internal static MouseButtons FromParamToMouseButtons(long param)
	{
		MouseButtons mouseButtons = MouseButtons.None;
		if ((param & 1) != 0L)
		{
			mouseButtons |= MouseButtons.Left;
		}
		if ((param & 0x10) != 0L)
		{
			mouseButtons |= MouseButtons.Middle;
		}
		if ((param & 2) != 0L)
		{
			mouseButtons |= MouseButtons.Right;
		}
		return mouseButtons;
	}

	internal virtual void FireEnter()
	{
		OnEnter(EventArgs.Empty);
	}

	internal virtual void FireLeave()
	{
		OnLeave(EventArgs.Empty);
	}

	internal virtual void FireValidating(CancelEventArgs ce)
	{
		OnValidating(ce);
	}

	internal virtual void FireValidated()
	{
		OnValidated(EventArgs.Empty);
	}

	internal virtual bool ProcessControlMnemonic(char charCode)
	{
		return ProcessMnemonic(charCode);
	}

	private static Control FindFlatForward(Control container, Control start)
	{
		Control control = null;
		int count = container.child_controls.Count;
		bool flag = false;
		int num = start?.tab_index ?? (-1);
		for (int i = 0; i < count; i++)
		{
			if (start == container.child_controls[i])
			{
				flag = true;
			}
			else if ((control == null || control.tab_index > container.child_controls[i].tab_index) && (container.child_controls[i].tab_index > num || (flag && container.child_controls[i].tab_index == num)))
			{
				control = container.child_controls[i];
			}
		}
		return control;
	}

	private static Control FindControlForward(Control container, Control start)
	{
		Control control = null;
		if (start == null)
		{
			return FindFlatForward(container, start);
		}
		if (start.child_controls != null && start.child_controls.Count > 0 && (start == container || !(start is IContainerControl) || !start.GetStyle(ControlStyles.ContainerControl)))
		{
			return FindControlForward(start, null);
		}
		while (start != container)
		{
			control = FindFlatForward(start.parent, start);
			if (control != null)
			{
				return control;
			}
			start = start.parent;
		}
		return null;
	}

	private static Control FindFlatBackward(Control container, Control start)
	{
		Control control = null;
		int count = container.child_controls.Count;
		bool flag = false;
		int num = start?.tab_index ?? int.MaxValue;
		for (int num2 = count - 1; num2 >= 0; num2--)
		{
			if (start == container.child_controls[num2])
			{
				flag = true;
			}
			else if ((control == null || control.tab_index < container.child_controls[num2].tab_index) && (container.child_controls[num2].tab_index < num || (flag && container.child_controls[num2].tab_index == num)))
			{
				control = container.child_controls[num2];
			}
		}
		return control;
	}

	private static Control FindControlBackward(Control container, Control start)
	{
		Control control = null;
		if (start == null)
		{
			control = FindFlatBackward(container, start);
		}
		else if (start != container && start.parent != null)
		{
			control = FindFlatBackward(start.parent, start);
			if (control == null)
			{
				if (start.parent != container)
				{
					return start.parent;
				}
				return null;
			}
		}
		if (control == null || start.parent == null)
		{
			control = start;
		}
		while (control != null && (control == container || ((!(control is IContainerControl) || !control.GetStyle(ControlStyles.ContainerControl)) && control.child_controls != null && control.child_controls.Count > 0)))
		{
			control = FindFlatBackward(control, null);
		}
		return control;
	}

	internal virtual void HandleClick(int clicks, MouseEventArgs me)
	{
		bool style = GetStyle(ControlStyles.StandardClick);
		bool style2 = GetStyle(ControlStyles.StandardDoubleClick);
		if (clicks > 1 && style && style2)
		{
			OnDoubleClick(me);
			OnMouseDoubleClick(me);
		}
		else if (clicks == 1 && style && !ValidationFailed)
		{
			OnClick(me);
			OnMouseClick(me);
		}
	}

	internal void CaptureWithConfine(Control ConfineWindow)
	{
		if (IsHandleCreated && !is_captured)
		{
			is_captured = true;
			XplatUI.GrabWindow(window.Handle, ConfineWindow.Handle);
		}
	}

	private void CheckDataBindings()
	{
		if (data_bindings == null)
		{
			return;
		}
		foreach (Binding data_binding in data_bindings)
		{
			data_binding.Check();
		}
	}

	private void ChangeParent(Control new_parent)
	{
		bool enabled = Enabled;
		bool visible = Visible;
		Font font = Font;
		Color foreColor = ForeColor;
		Color backColor = BackColor;
		RightToLeft rightToLeft = RightToLeft;
		parent = new_parent;
		if (this is Form form)
		{
			form.ChangingParent(new_parent);
		}
		else if (IsHandleCreated)
		{
			IntPtr hParent = IntPtr.Zero;
			if (new_parent != null && new_parent.IsHandleCreated)
			{
				hParent = new_parent.Handle;
			}
			XplatUI.SetParent(Handle, hParent);
		}
		OnParentChanged(EventArgs.Empty);
		if (enabled != Enabled)
		{
			OnEnabledChanged(EventArgs.Empty);
		}
		if (visible != Visible)
		{
			OnVisibleChanged(EventArgs.Empty);
		}
		if (font != Font)
		{
			OnFontChanged(EventArgs.Empty);
		}
		if (foreColor != ForeColor)
		{
			OnForeColorChanged(EventArgs.Empty);
		}
		if (backColor != BackColor)
		{
			OnBackColorChanged(EventArgs.Empty);
		}
		if (rightToLeft != RightToLeft)
		{
			OnRightToLeftChanged(EventArgs.Empty);
		}
		if (new_parent != null && new_parent.Created && is_visible && !Created)
		{
			CreateControl();
		}
		if (binding_context == null && Created)
		{
			OnBindingContextChanged(EventArgs.Empty);
		}
	}

	internal Size InternalSizeFromClientSize(Size clientSize)
	{
		Rectangle ClientRect = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
		CreateParams createParams = CreateParams;
		if (XplatUI.CalculateWindowRect(ref ClientRect, createParams, null, out var WindowRect))
		{
			return new Size(WindowRect.Width, WindowRect.Height);
		}
		return Size.Empty;
	}

	internal Size ClientSizeFromSize(Size size)
	{
		Size size2 = InternalSizeFromClientSize(size);
		if (size2 == Size.Empty)
		{
			return Size.Empty;
		}
		return new Size(size.Width - (size2.Width - size.Width), size.Height - (size2.Height - size.Height));
	}

	internal CreateParams GetCreateParams()
	{
		return CreateParams;
	}

	internal virtual Size GetPreferredSizeCore(Size proposedSize)
	{
		return explicit_bounds.Size;
	}

	private void UpdateDistances()
	{
		if (parent != null)
		{
			if (bounds.Width >= 0)
			{
				dist_right = parent.ClientSize.Width - bounds.X - bounds.Width;
			}
			if (bounds.Height >= 0)
			{
				dist_bottom = parent.ClientSize.Height - bounds.Y - bounds.Height;
			}
			recalculate_distances = false;
		}
	}

	private Cursor GetAvailableCursor()
	{
		if (Cursor != null && Enabled)
		{
			return Cursor;
		}
		if (Parent != null)
		{
			return Parent.GetAvailableCursor();
		}
		return Cursors.Default;
	}

	private void UpdateCursor()
	{
		if (!IsHandleCreated)
		{
			return;
		}
		if (!Enabled)
		{
			XplatUI.SetCursor(window.Handle, GetAvailableCursor().handle);
			return;
		}
		Point pt = PointToClient(Cursor.Position);
		if ((bounds.Contains(pt) || Capture) && GetChildAtPoint(pt) == null)
		{
			if (cursor != null || use_wait_cursor)
			{
				XplatUI.SetCursor(window.Handle, Cursor.handle);
			}
			else
			{
				XplatUI.SetCursor(window.Handle, GetAvailableCursor().handle);
			}
		}
	}

	internal void OnSizeInitializedOrChanged()
	{
		if (this is Form { WindowManager: not null } form)
		{
			ThemeEngine.Current.ManagedWindowOnSizeInitializedOrChanged(form);
		}
	}

	internal bool ShouldSerializeMaximumSize()
	{
		return MaximumSize != DefaultMaximumSize;
	}

	internal bool ShouldSerializeMinimumSize()
	{
		return MinimumSize != DefaultMinimumSize;
	}

	internal bool ShouldSerializeBackColor()
	{
		return BackColor != DefaultBackColor;
	}

	internal bool ShouldSerializeCursor()
	{
		return Cursor != Cursors.Default;
	}

	/// <summary>Supports rendering to the specified bitmap.</summary>
	/// <param name="bitmap">The bitmap to be drawn to.</param>
	/// <param name="targetBounds">The bounds within which the control is rendered.</param>
	public void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
	{
		Graphics graphics = Graphics.FromImage(bitmap);
		graphics.IntersectClip(targetBounds);
		graphics.IntersectClip(Bounds);
		PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, targetBounds);
		if (!GetStyle(ControlStyles.Opaque))
		{
			OnPaintBackground(paintEventArgs);
		}
		OnPaintBackgroundInternal(paintEventArgs);
		OnPaintInternal(paintEventArgs);
		if (!paintEventArgs.Handled)
		{
			OnPaint(paintEventArgs);
		}
		graphics.Dispose();
	}

	internal bool ShouldSerializeEnabled()
	{
		return !Enabled;
	}

	internal bool ShouldSerializeFont()
	{
		return !Font.Equals(DefaultFont);
	}

	internal bool ShouldSerializeForeColor()
	{
		return ForeColor != DefaultForeColor;
	}

	internal bool ShouldSerializeImeMode()
	{
		return ImeMode != ImeMode.NoControl;
	}

	internal bool ShouldSerializeLocation()
	{
		return Location != new Point(0, 0);
	}

	internal bool ShouldSerializeMargin()
	{
		return Margin != DefaultMargin;
	}

	internal bool ShouldSerializePadding()
	{
		return Padding != DefaultPadding;
	}

	internal bool ShouldSerializeRightToLeft()
	{
		return RightToLeft != RightToLeft.No;
	}

	internal bool ShouldSerializeSite()
	{
		return false;
	}

	internal virtual bool ShouldSerializeSize()
	{
		return Size != DefaultSize;
	}

	internal virtual void UpdateWindowText()
	{
		if (IsHandleCreated)
		{
			XplatUI.Text(Handle, text);
		}
	}

	internal bool ShouldSerializeVisible()
	{
		return !Visible;
	}

	/// <summary>Retrieves the control that contains the specified handle.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the control associated with the specified handle; returns null if no control with the specified handle is found.</returns>
	/// <param name="handle">The window handle (HWND) to search for. </param>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static Control FromChildHandle(IntPtr handle)
	{
		return ControlNativeWindow.ControlFromChildHandle(handle);
	}

	/// <summary>Returns the control that is currently associated with the specified handle.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control associated with the specified handle; returns null if no control with the specified handle is found.</returns>
	/// <param name="handle">The window handle (HWND) to search for. </param>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static Control FromHandle(IntPtr handle)
	{
		return ControlNativeWindow.ControlFromHandle(handle);
	}

	/// <summary>Determines whether the CAPS LOCK, NUM LOCK, or SCROLL LOCK key is in effect.</summary>
	/// <returns>true if the specified key or keys are in effect; otherwise, false.</returns>
	/// <param name="keyVal">The CAPS LOCK, NUM LOCK, or SCROLL LOCK member of the <see cref="T:System.Windows.Forms.Keys" /> enumeration. </param>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="keyVal" /> parameter refers to a key other than the CAPS LOCK, NUM LOCK, or SCROLL LOCK key. </exception>
	/// <filterpriority>2</filterpriority>
	[System.MonoTODO("Only implemented for Win32, others always return false")]
	public static bool IsKeyLocked(Keys keyVal)
	{
		if (keyVal == Keys.NumLock || keyVal == Keys.Scroll || keyVal == Keys.CapsLock)
		{
			return XplatUI.IsKeyLocked((VirtualKeys)keyVal);
		}
		throw new NotSupportedException("keyVal must be CapsLock, NumLock, or ScrollLock");
	}

	/// <summary>Determines if the specified character is the mnemonic character assigned to the control in the specified string.</summary>
	/// <returns>true if the <paramref name="charCode" /> character is the mnemonic character assigned to the control; otherwise, false.</returns>
	/// <param name="charCode">The character to test. </param>
	/// <param name="text">The string to search. </param>
	/// <filterpriority>2</filterpriority>
	public static bool IsMnemonic(char charCode, string text)
	{
		int num = text.IndexOf('&');
		if (num != -1 && num + 1 < text.Length && text[num + 1] != '&' && char.ToUpper(charCode) == char.ToUpper(text.ToCharArray(num + 1, 1)[0]))
		{
			return true;
		}
		return false;
	}

	/// <summary>Reflects the specified message to the control that is bound to the specified handle.</summary>
	/// <returns>true if the message was reflected; otherwise, false.</returns>
	/// <param name="hWnd">An <see cref="T:System.IntPtr" /> representing the handle of the control to reflect the message to. </param>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> representing the Windows message to reflect. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static bool ReflectMessage(IntPtr hWnd, ref Message m)
	{
		Control control = FromHandle(hWnd);
		if (control != null)
		{
			control.WndProc(ref m);
			return true;
		}
		return false;
	}

	/// <summary>Executes the specified delegate asynchronously on the thread that the control's underlying handle was created on.</summary>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> operation.</returns>
	/// <param name="method">A delegate to a method that takes no parameters. </param>
	/// <exception cref="T:System.InvalidOperationException">No appropriate window handle can be found.</exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IAsyncResult BeginInvoke(Delegate method)
	{
		object[] args = null;
		if (method is EventHandler)
		{
			args = new object[2]
			{
				this,
				EventArgs.Empty
			};
		}
		return BeginInvokeInternal(method, args);
	}

	/// <summary>Executes the specified delegate asynchronously with the specified arguments, on the thread that the control's underlying handle was created on.</summary>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> operation.</returns>
	/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter. </param>
	/// <param name="args">An array of objects to pass as arguments to the given method. This can be null if no arguments are needed. </param>
	/// <exception cref="T:System.InvalidOperationException">No appropriate window handle can be found.</exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IAsyncResult BeginInvoke(Delegate method, params object[] args)
	{
		return BeginInvokeInternal(method, args);
	}

	/// <summary>Brings the control to the front of the z-order.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void BringToFront()
	{
		if (parent != null)
		{
			parent.child_controls.SetChildIndex(this, 0);
		}
		else if (IsHandleCreated)
		{
			XplatUI.SetZOrder(Handle, IntPtr.Zero, Top: false, Bottom: false);
		}
	}

	/// <summary>Retrieves a value indicating whether the specified control is a child of the control.</summary>
	/// <returns>true if the specified control is a child of the control; otherwise, false.</returns>
	/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to evaluate. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(Control ctl)
	{
		while (ctl != null)
		{
			ctl = ctl.parent;
			if (ctl == this)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Forces the creation of the visible control, including the creation of the handle and any visible child controls.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void CreateControl()
	{
		if (is_created || is_disposing || !is_visible || (parent != null && !parent.Created))
		{
			return;
		}
		if (!IsHandleCreated)
		{
			CreateHandle();
		}
		if (is_created)
		{
			return;
		}
		is_created = true;
		Control[] allControls = Controls.GetAllControls();
		foreach (Control control in allControls)
		{
			if (!control.Created && !control.IsDisposed)
			{
				control.CreateControl();
			}
		}
		OnCreateControl();
	}

	/// <summary>Creates the <see cref="T:System.Drawing.Graphics" /> for the control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> for the control.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Graphics CreateGraphics()
	{
		if (!IsHandleCreated)
		{
			CreateHandle();
		}
		return Graphics.FromHwnd(window.Handle);
	}

	/// <summary>Begins a drag-and-drop operation.</summary>
	/// <returns>A value from the <see cref="T:System.Windows.Forms.DragDropEffects" /> enumeration that represents the final effect that was performed during the drag-and-drop operation.</returns>
	/// <param name="data">The data to drag. </param>
	/// <param name="allowedEffects">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
	/// <filterpriority>1</filterpriority>
	public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
	{
		DragDropEffects dragDropEffects = DragDropEffects.None;
		if (IsHandleCreated)
		{
			dragDropEffects = XplatUI.StartDrag(Handle, data, allowedEffects);
		}
		OnDragDropEnd(dragDropEffects);
		return dragDropEffects;
	}

	internal virtual void OnDragDropEnd(DragDropEffects effects)
	{
	}

	/// <summary>Retrieves the return value of the asynchronous operation represented by the <see cref="T:System.IAsyncResult" /> passed.</summary>
	/// <returns>The <see cref="T:System.Object" /> generated by the asynchronous operation.</returns>
	/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents a specific invoke asynchronous operation, returned when calling <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" />. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter value is null. </exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> object was not created by a preceding call of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> method from the same control. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public object EndInvoke(IAsyncResult asyncResult)
	{
		AsyncMethodResult asyncMethodResult = (AsyncMethodResult)asyncResult;
		return asyncMethodResult.EndInvoke();
	}

	internal Control FindRootParent()
	{
		Control control = this;
		while (control.Parent != null)
		{
			control = control.Parent;
		}
		return control;
	}

	/// <summary>Retrieves the form that the control is on.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the control is on.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
	/// </PermissionSet>
	public Form FindForm()
	{
		for (Control control = this; control != null; control = control.Parent)
		{
			if (control is Form)
			{
				return (Form)control;
			}
		}
		return null;
	}

	/// <summary>Sets input focus to the control.</summary>
	/// <returns>true if the input focus request was successful; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool Focus()
	{
		return FocusInternal(skip_check: false);
	}

	internal virtual bool FocusInternal(bool skip_check)
	{
		if (skip_check || (CanFocus && IsHandleCreated && !has_focus && !is_focusing))
		{
			is_focusing = true;
			Select(this);
			is_focusing = false;
		}
		return has_focus;
	}

	internal Control GetRealChildAtPoint(Point pt)
	{
		if (!IsHandleCreated)
		{
			CreateHandle();
		}
		Control[] allControls = child_controls.GetAllControls();
		foreach (Control control in allControls)
		{
			if (control.Bounds.Contains(PointToClient(pt)))
			{
				Control realChildAtPoint = control.GetRealChildAtPoint(pt);
				if (realChildAtPoint == null)
				{
					return control;
				}
				return realChildAtPoint;
			}
		}
		return null;
	}

	/// <summary>Retrieves the child control that is located at the specified coordinates.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control that is located at the specified point.</returns>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that contains the coordinates where you want to look for a control. Coordinates are expressed relative to the upper-left corner of the control's client area. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Control GetChildAtPoint(Point pt)
	{
		return GetChildAtPoint(pt, GetChildAtPointSkip.None);
	}

	/// <summary>Retrieves the child control that is located at the specified coordinates, specifying whether to ignore child controls of a certain type.</summary>
	/// <returns>The child <see cref="T:System.Windows.Forms.Control" /> at the specified coordinates.</returns>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that contains the coordinates where you want to look for a control. Coordinates are expressed relative to the upper-left corner of the control's client area.</param>
	/// <param name="skipValue">One of the values of <see cref="T:System.Windows.Forms.GetChildAtPointSkip" />, determining whether to ignore child controls of a certain type.</param>
	public Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
	{
		if (!IsHandleCreated)
		{
			CreateHandle();
		}
		foreach (Control control in Controls)
		{
			if (((skipValue & GetChildAtPointSkip.Disabled) != GetChildAtPointSkip.Disabled || control.Enabled) && ((skipValue & GetChildAtPointSkip.Invisible) != GetChildAtPointSkip.Invisible || control.Visible) && ((skipValue & GetChildAtPointSkip.Transparent) != GetChildAtPointSkip.Transparent || control.BackColor.A != 0) && control.Bounds.Contains(pt))
			{
				return control;
			}
		}
		return null;
	}

	/// <summary>Returns the next <see cref="T:System.Windows.Forms.ContainerControl" /> up the control's chain of parent controls.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.IContainerControl" />, that represents the parent of the <see cref="T:System.Windows.Forms.Control" />.</returns>
	/// <filterpriority>1</filterpriority>
	public IContainerControl GetContainerControl()
	{
		for (Control control = this; control != null; control = control.parent)
		{
			if (control is IContainerControl && (control.control_style & ControlStyles.ContainerControl) != 0)
			{
				return (IContainerControl)control;
			}
		}
		return null;
	}

	internal ContainerControl InternalGetContainerControl()
	{
		for (Control control = this; control != null; control = control.parent)
		{
			if (control is ContainerControl && (control.control_style & ControlStyles.ContainerControl) != 0)
			{
				return control as ContainerControl;
			}
		}
		return null;
	}

	/// <summary>Retrieves the next control forward or back in the tab order of child controls.</summary>
	/// <returns>The next <see cref="T:System.Windows.Forms.Control" /> in the tab order.</returns>
	/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to start the search with. </param>
	/// <param name="forward">true to search forward in the tab order; false to search backward. </param>
	/// <filterpriority>2</filterpriority>
	public Control GetNextControl(Control ctl, bool forward)
	{
		if (!Contains(ctl))
		{
			ctl = this;
		}
		ctl = ((!forward) ? FindControlBackward(this, ctl) : FindControlForward(this, ctl));
		if (ctl != this)
		{
			return ctl;
		}
		return null;
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="proposedSize">The custom-sized area for a control. </param>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual Size GetPreferredSize(Size proposedSize)
	{
		Size preferredSizeCore = GetPreferredSizeCore(proposedSize);
		if (maximum_size.Width != 0 && preferredSizeCore.Width > maximum_size.Width)
		{
			preferredSizeCore.Width = maximum_size.Width;
		}
		if (maximum_size.Height != 0 && preferredSizeCore.Height > maximum_size.Height)
		{
			preferredSizeCore.Height = maximum_size.Height;
		}
		if (minimum_size.Width != 0 && preferredSizeCore.Width < minimum_size.Width)
		{
			preferredSizeCore.Width = minimum_size.Width;
		}
		if (minimum_size.Height != 0 && preferredSizeCore.Height < minimum_size.Height)
		{
			preferredSizeCore.Height = minimum_size.Height;
		}
		return preferredSizeCore;
	}

	/// <summary>Conceals the control from the user.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Hide()
	{
		Visible = false;
	}

	/// <summary>Invalidates the entire surface of the control and causes the control to be redrawn.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate()
	{
		Invalidate(ClientRectangle, invalidateChildren: false);
	}

	/// <summary>Invalidates a specific region of the control and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
	/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate(bool invalidateChildren)
	{
		Invalidate(ClientRectangle, invalidateChildren);
	}

	/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
	/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate(Rectangle rc)
	{
		Invalidate(rc, invalidateChildren: false);
	}

	/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
	/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
	/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate(Rectangle rc, bool invalidateChildren)
	{
		if (!IsHandleCreated)
		{
			return;
		}
		if (rc == Rectangle.Empty)
		{
			rc = ClientRectangle;
		}
		if (rc.Width > 0 && rc.Height > 0)
		{
			NotifyInvalidate(rc);
			XplatUI.Invalidate(Handle, rc, clear: false);
			if (invalidateChildren)
			{
				Control[] allControls = child_controls.GetAllControls();
				for (int i = 0; i < allControls.Length; i++)
				{
					allControls[i].Invalidate();
				}
			}
			else
			{
				foreach (Control control in Controls)
				{
					if (control.BackColor.A != byte.MaxValue)
					{
						control.Invalidate();
					}
				}
			}
		}
		OnInvalidated(new InvalidateEventArgs(rc));
	}

	/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
	/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate(Region region)
	{
		Invalidate(region, invalidateChildren: false);
	}

	/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
	/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate. </param>
	/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate(Region region, bool invalidateChildren)
	{
		RectangleF rectangleF = region.GetBounds(CreateGraphics());
		Invalidate(new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), invalidateChildren);
	}

	/// <summary>Executes the specified delegate on the thread that owns the control's underlying window handle.</summary>
	/// <returns>The return value from the delegate being invoked, or null if the delegate has no return value.</returns>
	/// <param name="method">A delegate that contains a method to be called in the control's thread context. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public object Invoke(Delegate method)
	{
		object[] args = null;
		if (method is EventHandler)
		{
			args = new object[2]
			{
				this,
				EventArgs.Empty
			};
		}
		return Invoke(method, args);
	}

	/// <summary>Executes the specified delegate, on the thread that owns the control's underlying window handle, with the specified list of arguments.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the return value from the delegate being invoked, or null if the delegate has no return value.</returns>
	/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter. </param>
	/// <param name="args">An array of objects to pass as arguments to the specified method. This parameter can be null if the method takes no arguments. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public object Invoke(Delegate method, params object[] args)
	{
		Control control = FindControlToInvokeOn();
		if (!InvokeRequired)
		{
			return method.DynamicInvoke(args);
		}
		IAsyncResult asyncResult = BeginInvokeInternal(method, args, control);
		return EndInvoke(asyncResult);
	}

	/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void PerformLayout()
	{
		PerformLayout(null, null);
	}

	/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
	/// <param name="affectedControl">A <see cref="T:System.Windows.Forms.Control" /> that represents the most recently changed control. </param>
	/// <param name="affectedProperty">The name of the most recently changed property on the control. </param>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void PerformLayout(Control affectedControl, string affectedProperty)
	{
		LayoutEventArgs levent = new LayoutEventArgs(affectedControl, affectedProperty);
		Control[] allControls = Controls.GetAllControls();
		foreach (Control control in allControls)
		{
			if (control.recalculate_distances)
			{
				control.UpdateDistances();
			}
		}
		if (layout_suspended > 0)
		{
			layout_pending = true;
			return;
		}
		layout_pending = false;
		layout_suspended++;
		try
		{
			OnLayout(levent);
		}
		finally
		{
			layout_suspended--;
		}
	}

	/// <summary>Computes the location of the specified screen point into client coordinates.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in client coordinates.</returns>
	/// <param name="p">The screen coordinate <see cref="T:System.Drawing.Point" /> to convert. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Point PointToClient(Point p)
	{
		int x = p.X;
		int y = p.Y;
		XplatUI.ScreenToClient(Handle, ref x, ref y);
		return new Point(x, y);
	}

	/// <summary>Computes the location of the specified client point into screen coordinates.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in screen coordinates.</returns>
	/// <param name="p">The client coordinate <see cref="T:System.Drawing.Point" /> to convert. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Point PointToScreen(Point p)
	{
		int x = p.X;
		int y = p.Y;
		XplatUI.ClientToScreen(Handle, ref x, ref y);
		return new Point(x, y);
	}

	/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.PreProcessControlState" /> values, depending on whether <see cref="M:System.Windows.Forms.Control.PreProcessMessage(System.Windows.Forms.Message@)" /> is true or false and whether <see cref="M:System.Windows.Forms.Control.IsInputKey(System.Windows.Forms.Keys)" /> or <see cref="M:System.Windows.Forms.Control.IsInputChar(System.Char)" /> are true or false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" /> that represents the message to process.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public PreProcessControlState PreProcessControlMessage(ref Message msg)
	{
		return PreProcessControlMessageInternal(ref msg);
	}

	internal PreProcessControlState PreProcessControlMessageInternal(ref Message msg)
	{
		switch ((Msg)msg.Msg)
		{
		case Msg.WM_KEYDOWN:
		case Msg.WM_SYSKEYDOWN:
		{
			PreviewKeyDownEventArgs previewKeyDownEventArgs = new PreviewKeyDownEventArgs((Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys));
			OnPreviewKeyDown(previewKeyDownEventArgs);
			if (previewKeyDownEventArgs.IsInputKey)
			{
				return PreProcessControlState.MessageNeeded;
			}
			if (PreProcessMessage(ref msg))
			{
				return PreProcessControlState.MessageProcessed;
			}
			if (IsInputKey((Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys)))
			{
				return PreProcessControlState.MessageNeeded;
			}
			break;
		}
		case Msg.WM_CHAR:
		case Msg.WM_SYSCHAR:
			if (PreProcessMessage(ref msg))
			{
				return PreProcessControlState.MessageProcessed;
			}
			if (IsInputChar((char)(int)msg.WParam))
			{
				return PreProcessControlState.MessageNeeded;
			}
			break;
		}
		return PreProcessControlState.MessageNotNeeded;
	}

	/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
	/// <returns>true if the message was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual bool PreProcessMessage(ref Message msg)
	{
		return InternalPreProcessMessage(ref msg);
	}

	internal virtual bool InternalPreProcessMessage(ref Message msg)
	{
		if (msg.Msg == 256 || msg.Msg == 260)
		{
			Keys keyData = (Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys);
			if (!ProcessCmdKey(ref msg, keyData))
			{
				if (IsInputKey(keyData))
				{
					return false;
				}
				return ProcessDialogKey(keyData);
			}
			return true;
		}
		if (msg.Msg == 258)
		{
			if (IsInputChar((char)(int)msg.WParam))
			{
				return false;
			}
			return ProcessDialogChar((char)(int)msg.WParam);
		}
		if (msg.Msg == 262)
		{
			if (ProcessDialogChar((char)(int)msg.WParam))
			{
				return true;
			}
			return ToolStripManager.ProcessMenuKey(ref msg);
		}
		return false;
	}

	/// <summary>Computes the size and location of the specified screen rectangle in client coordinates.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Rectangle" />, <paramref name="r" />, in client coordinates.</returns>
	/// <param name="r">The screen coordinate <see cref="T:System.Drawing.Rectangle" /> to convert. </param>
	/// <filterpriority>2</filterpriority>
	public Rectangle RectangleToClient(Rectangle r)
	{
		return new Rectangle(PointToClient(r.Location), r.Size);
	}

	/// <summary>Computes the size and location of the specified client rectangle in screen coordinates.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Rectangle" />, <paramref name="p" />, in screen coordinates.</returns>
	/// <param name="r">The client coordinate <see cref="T:System.Drawing.Rectangle" /> to convert. </param>
	/// <filterpriority>2</filterpriority>
	public Rectangle RectangleToScreen(Rectangle r)
	{
		return new Rectangle(PointToScreen(r.Location), r.Size);
	}

	/// <summary>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Refresh()
	{
		if (IsHandleCreated && Visible)
		{
			Invalidate(invalidateChildren: true);
			Update();
		}
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.BackColor" /> property to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetBackColor()
	{
		BackColor = Color.Empty;
	}

	/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread all the items in the list and refresh their displayed values.</summary>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void ResetBindings()
	{
		if (data_bindings != null)
		{
			data_bindings.Clear();
		}
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Cursor" /> property to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetCursor()
	{
		Cursor = null;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Font" /> property to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetFont()
	{
		font = null;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetForeColor()
	{
		foreground_color = Color.Empty;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void ResetImeMode()
	{
		ime_mode = DefaultImeMode;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetRightToLeft()
	{
		right_to_left = RightToLeft.Inherit;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Text" /> property to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	public virtual void ResetText()
	{
		Text = string.Empty;
	}

	/// <summary>Resumes usual layout logic.</summary>
	/// <filterpriority>1</filterpriority>
	public void ResumeLayout()
	{
		ResumeLayout(performLayout: true);
	}

	/// <summary>Resumes usual layout logic, optionally forcing an immediate layout of pending layout requests.</summary>
	/// <param name="performLayout">true to execute pending layout requests; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	public void ResumeLayout(bool performLayout)
	{
		if (layout_suspended > 0)
		{
			layout_suspended--;
		}
		if (layout_suspended != 0)
		{
			return;
		}
		if (this is ContainerControl)
		{
			(this as ContainerControl).PerformDelayedAutoScale();
		}
		if (!performLayout)
		{
			Control[] allControls = Controls.GetAllControls();
			foreach (Control control in allControls)
			{
				control.UpdateDistances();
			}
		}
		if (performLayout && layout_pending)
		{
			PerformLayout();
		}
	}

	/// <summary>Scales the control and any child controls.</summary>
	/// <param name="ratio">The ratio to use for scaling.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete]
	public void Scale(float ratio)
	{
		ScaleCore(ratio, ratio);
	}

	/// <summary>Scales the entire control and any child controls.</summary>
	/// <param name="dx">The horizontal scaling factor.</param>
	/// <param name="dy">The vertical scaling factor.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Obsolete]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Scale(float dx, float dy)
	{
		ScaleCore(dx, dy);
	}

	/// <summary>Scales the control and all child controls by the specified scaling factor.</summary>
	/// <param name="factor">A <see cref="T:System.Drawing.SizeF" /> containing the horizontal and vertical scaling factors.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void Scale(SizeF factor)
	{
		BoundsSpecified boundsSpecified = BoundsSpecified.All;
		SuspendLayout();
		if (this is ContainerControl)
		{
			if ((this as ContainerControl).IsAutoScaling)
			{
				boundsSpecified = BoundsSpecified.Size;
			}
			else if (IsContainerAutoScaling(Parent))
			{
				boundsSpecified = BoundsSpecified.Location;
			}
		}
		ScaleControl(factor, boundsSpecified);
		if (boundsSpecified != BoundsSpecified.Location && ScaleChildren)
		{
			Control[] allControls = Controls.GetAllControls();
			foreach (Control control in allControls)
			{
				control.Scale(factor);
				if (control is ContainerControl)
				{
					ContainerControl containerControl = control as ContainerControl;
					if (containerControl.AutoScaleMode == AutoScaleMode.Inherit && IsContainerAutoScaling(this))
					{
						containerControl.PerformAutoScale(called_by_scale: true);
					}
				}
			}
		}
		ResumeLayout();
	}

	internal ContainerControl FindContainer(Control c)
	{
		while (c != null && !(c is ContainerControl))
		{
			c = c.Parent;
		}
		return c as ContainerControl;
	}

	private bool IsContainerAutoScaling(Control c)
	{
		return FindContainer(c)?.IsAutoScaling ?? false;
	}

	/// <summary>Activates the control.</summary>
	/// <filterpriority>1</filterpriority>
	public void Select()
	{
		Select(directed: false, forward: false);
	}

	/// <summary>Activates the next control.</summary>
	/// <returns>true if a control was activated; otherwise, false.</returns>
	/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> at which to start the search. </param>
	/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
	/// <param name="tabStopOnly">true to ignore the controls with the <see cref="P:System.Windows.Forms.Control.TabStop" /> property set to false; otherwise, false. </param>
	/// <param name="nested">true to include nested (children of child controls) child controls; otherwise, false. </param>
	/// <param name="wrap">true to continue searching from the first control in the tab order after the last control has been reached; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	public bool SelectNextControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
	{
		if (!Contains(ctl) || (!nested && ctl.parent != this))
		{
			ctl = null;
		}
		Control control = ctl;
		do
		{
			control = GetNextControl(control, forward);
			if (control == null)
			{
				if (!wrap)
				{
					break;
				}
				wrap = false;
			}
			else if (control.CanSelect && (control.parent == this || nested) && (control.tab_stop || !tabStopOnly))
			{
				control.Select(directed: true, forward: true);
				return true;
			}
		}
		while (control != ctl);
		return false;
	}

	/// <summary>Sends the control to the back of the z-order.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SendToBack()
	{
		if (parent != null)
		{
			parent.child_controls.SetChildIndex(this, parent.child_controls.Count);
		}
	}

	/// <summary>Sets the bounds of the control to the specified location and size.</summary>
	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetBounds(int x, int y, int width, int height)
	{
		SetBounds(x, y, width, height, BoundsSpecified.All);
	}

	/// <summary>Sets the specified bounds of the control to the specified location and size.</summary>
	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
	{
		if ((specified & BoundsSpecified.X) == 0)
		{
			x = Left;
		}
		if ((specified & BoundsSpecified.Y) == 0)
		{
			y = Top;
		}
		if ((specified & BoundsSpecified.Width) == 0)
		{
			width = Width;
		}
		if ((specified & BoundsSpecified.Height) == 0)
		{
			height = Height;
		}
		SetBoundsInternal(x, y, width, height, specified);
	}

	internal void SetBoundsInternal(int x, int y, int width, int height, BoundsSpecified specified)
	{
		if (bounds.X != x || (explicit_bounds.X != x && (specified & BoundsSpecified.X) == BoundsSpecified.X))
		{
			SetBoundsCore(x, y, width, height, specified);
		}
		else if (bounds.Y != y || (explicit_bounds.Y != y && (specified & BoundsSpecified.Y) == BoundsSpecified.Y))
		{
			SetBoundsCore(x, y, width, height, specified);
		}
		else if (bounds.Width != width || (explicit_bounds.Width != width && (specified & BoundsSpecified.Width) == BoundsSpecified.Width))
		{
			SetBoundsCore(x, y, width, height, specified);
		}
		else
		{
			if (bounds.Height == height && (explicit_bounds.Height == height || (specified & BoundsSpecified.Height) != BoundsSpecified.Height))
			{
				return;
			}
			SetBoundsCore(x, y, width, height, specified);
		}
		if (specified != 0)
		{
			UpdateDistances();
		}
		if (parent != null)
		{
			parent.PerformLayout(this, "Bounds");
		}
	}

	/// <summary>Displays the control to the user.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Show()
	{
		Visible = true;
	}

	/// <summary>Temporarily suspends the layout logic for the control.</summary>
	/// <filterpriority>1</filterpriority>
	public void SuspendLayout()
	{
		layout_suspended++;
	}

	/// <summary>Causes the control to redraw the invalidated regions within its client area.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Update()
	{
		if (IsHandleCreated)
		{
			XplatUI.UpdateWindow(window.Handle);
		}
	}

	/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control.</summary>
	/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of. </param>
	/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void AccessibilityNotifyClients(AccessibleEvents accEvent, int childID)
	{
		if (accessibility_object != null && accessibility_object is ControlAccessibleObject)
		{
			((ControlAccessibleObject)accessibility_object).NotifyClients(accEvent, childID);
		}
	}

	/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control .</summary>
	/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
	/// <param name="objectID">The identifier of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
	/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void AccessibilityNotifyClients(AccessibleEvents accEvent, int objectID, int childID)
	{
		if (accessibility_object != null && accessibility_object is ControlAccessibleObject)
		{
			((ControlAccessibleObject)accessibility_object).NotifyClients(accEvent, objectID, childID);
		}
	}

	/// <summary>Creates a new accessibility object for the control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual AccessibleObject CreateAccessibilityInstance()
	{
		CreateControl();
		return new ControlAccessibleObject(this);
	}

	/// <summary>Creates a new instance of the control collection for the control.</summary>
	/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual ControlCollection CreateControlsInstance()
	{
		return new ControlCollection(this);
	}

	/// <summary>Creates a handle for the control.</summary>
	/// <exception cref="T:System.ObjectDisposedException">The object is in a disposed state. </exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void CreateHandle()
	{
		if (IsDisposed)
		{
			throw new ObjectDisposedException(GetType().FullName);
		}
		if (IsHandleCreated && !is_recreating)
		{
			return;
		}
		CreateParams createParams = CreateParams;
		window.CreateHandle(createParams);
		if (window.Handle != IntPtr.Zero)
		{
			creator_thread = Thread.CurrentThread;
			XplatUI.EnableWindow(window.Handle, is_enabled);
			if (clip_region != null)
			{
				XplatUI.SetClipRegion(window.Handle, clip_region);
			}
			if (parent != null && parent.IsHandleCreated)
			{
				XplatUI.SetParent(window.Handle, parent.Handle);
			}
			UpdateStyles();
			XplatUI.SetAllowDrop(window.Handle, allow_drop);
			if ((CreateParams.Style & 0x40000000) != 0)
			{
				XplatUI.SetBorderStyle(window.Handle, (FormBorderStyle)border_style);
			}
			Rectangle rectangle = explicit_bounds;
			UpdateBounds();
			explicit_bounds = rectangle;
		}
	}

	/// <summary>Sends the specified message to the default window procedure.</summary>
	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void DefWndProc(ref Message m)
	{
		window.DefWndProc(ref m);
	}

	/// <summary>Destroys the handle associated with the control.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void DestroyHandle()
	{
		if (IsHandleCreated && window != null)
		{
			window.DestroyHandle();
		}
	}

	/// <summary>Retrieves the specified <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" />.</returns>
	/// <param name="objectId">An Int32 that identifies the <see cref="T:System.Windows.Forms.AccessibleObject" /> to retrieve.</param>
	protected virtual AccessibleObject GetAccessibilityObjectById(int objectId)
	{
		return null;
	}

	/// <summary>Retrieves a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. </returns>
	protected internal AutoSizeMode GetAutoSizeMode()
	{
		return auto_size_mode;
	}

	/// <summary>Retrieves the bounds within which the control is scaled.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
	/// <param name="factor">The height and width of the control's bounds.</param>
	/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
	{
		if (!is_toplevel)
		{
			if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
			{
				bounds.X = (int)Math.Round((float)bounds.X * factor.Width);
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
			{
				bounds.Y = (int)Math.Round((float)bounds.Y * factor.Height);
			}
		}
		if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width && !GetStyle(ControlStyles.FixedWidth))
		{
			int num = ((!(this is ComboBox)) ? (this.bounds.Width - client_size.Width) : (ThemeEngine.Current.Border3DSize.Width * 2));
			bounds.Width = (int)Math.Round((float)(bounds.Width - num) * factor.Width + (float)num);
		}
		if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height && !GetStyle(ControlStyles.FixedHeight))
		{
			int num2 = ((!(this is ComboBox)) ? (this.bounds.Height - client_size.Height) : (ThemeEngine.Current.Border3DSize.Height * 2));
			bounds.Height = (int)Math.Round((float)(bounds.Height - num2) * factor.Height + (float)num2);
		}
		return bounds;
	}

	private Rectangle GetScaledBoundsOld(Rectangle bounds, SizeF factor, BoundsSpecified specified)
	{
		RectangleF rectangleF = new RectangleF(bounds.Location, bounds.Size);
		if (!is_toplevel)
		{
			if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
			{
				rectangleF.X *= factor.Width;
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
			{
				rectangleF.Y *= factor.Height;
			}
		}
		if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width && !GetStyle(ControlStyles.FixedWidth))
		{
			int num = ((this is Form) ? (this.bounds.Width - client_size.Width) : 0);
			rectangleF.Width = (rectangleF.Width - (float)num) * factor.Width + (float)num;
		}
		if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height && !GetStyle(ControlStyles.FixedHeight))
		{
			int num2 = ((this is Form) ? (this.bounds.Height - client_size.Height) : 0);
			rectangleF.Height = (rectangleF.Height - (float)num2) * factor.Height + (float)num2;
		}
		bounds.X = (int)Math.Round(rectangleF.X);
		bounds.Y = (int)Math.Round(rectangleF.Y);
		bounds.Width = (int)Math.Round(rectangleF.Right) - bounds.X;
		bounds.Height = (int)Math.Round(rectangleF.Bottom) - bounds.Y;
		return bounds;
	}

	/// <summary>Retrieves the value of the specified control style bit for the control.</summary>
	/// <returns>true if the specified control style bit is set to true; otherwise, false.</returns>
	/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to return the value from. </param>
	protected internal bool GetStyle(ControlStyles flag)
	{
		return (control_style & flag) != 0;
	}

	/// <summary>Determines if the control is a top-level control.</summary>
	/// <returns>true if the control is a top-level control; otherwise, false.</returns>
	protected bool GetTopLevel()
	{
		return is_toplevel;
	}

	/// <summary>Called after the control has been added to another container.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void InitLayout()
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event for the specified control.</summary>
	/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the event to. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void InvokeGotFocus(Control toInvoke, EventArgs e)
	{
		toInvoke.OnGotFocus(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event for the specified control.</summary>
	/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the event to. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void InvokeLostFocus(Control toInvoke, EventArgs e)
	{
		toInvoke.OnLostFocus(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event for the specified control.</summary>
	/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Click" /> event to. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void InvokeOnClick(Control toInvoke, EventArgs e)
	{
		toInvoke.OnClick(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event for the specified control.</summary>
	/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Paint" /> event to. </param>
	/// <param name="e">An <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected void InvokePaint(Control c, PaintEventArgs e)
	{
		c.OnPaint(e);
	}

	/// <summary>Raises the PaintBackground event for the specified control.</summary>
	/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Paint" /> event to. </param>
	/// <param name="e">An <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected void InvokePaintBackground(Control c, PaintEventArgs e)
	{
		c.OnPaintBackground(e);
	}

	/// <summary>Determines if a character is an input character that the control recognizes.</summary>
	/// <returns>true if the character should be sent directly to the control and not preprocessed; otherwise, false.</returns>
	/// <param name="charCode">The character to test. </param>
	protected virtual bool IsInputChar(char charCode)
	{
		if (!IsHandleCreated)
		{
			CreateHandle();
		}
		return IsInputCharInternal(charCode);
	}

	internal virtual bool IsInputCharInternal(char charCode)
	{
		return false;
	}

	/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
	/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
	protected virtual bool IsInputKey(Keys keyData)
	{
		return false;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event with a specified region of the control to invalidate.</summary>
	/// <param name="invalidatedArea">A <see cref="T:System.Drawing.Rectangle" /> representing the area to invalidate. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void NotifyInvalidate(Rectangle invalidatedArea)
	{
	}

	/// <summary>Processes a command key.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (context_menu != null && context_menu.ProcessCmdKey(ref msg, keyData))
		{
			return true;
		}
		if (parent != null)
		{
			return parent.ProcessCmdKey(ref msg, keyData);
		}
		return false;
	}

	/// <summary>Processes a dialog character.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected virtual bool ProcessDialogChar(char charCode)
	{
		if (parent != null)
		{
			return parent.ProcessDialogChar(charCode);
		}
		return false;
	}

	/// <summary>Processes a dialog key.</summary>
	/// <returns>true if the key was processed by the control; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected virtual bool ProcessDialogKey(Keys keyData)
	{
		if (parent != null)
		{
			return parent.ProcessDialogKey(keyData);
		}
		return false;
	}

	/// <summary>Processes a key message and generates the appropriate control events.</summary>
	/// <returns>true if the message was processed by the control; otherwise, false.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	protected virtual bool ProcessKeyEventArgs(ref Message m)
	{
		switch (m.Msg)
		{
		case 256:
		case 260:
		{
			KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)m.WParam.ToInt32());
			OnKeyDown(keyEventArgs);
			suppressing_key_press = keyEventArgs.SuppressKeyPress;
			return keyEventArgs.Handled;
		}
		case 257:
		case 261:
		{
			KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)m.WParam.ToInt32());
			OnKeyUp(keyEventArgs);
			return keyEventArgs.Handled;
		}
		case 258:
		case 262:
		{
			if (suppressing_key_press)
			{
				return true;
			}
			KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs((char)(int)m.WParam);
			OnKeyPress(keyPressEventArgs);
			m.WParam = (IntPtr)keyPressEventArgs.KeyChar;
			return keyPressEventArgs.Handled;
		}
		default:
			return false;
		}
	}

	/// <summary>Processes a keyboard message.</summary>
	/// <returns>true if the message was processed by the control; otherwise, false.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	protected internal virtual bool ProcessKeyMessage(ref Message m)
	{
		if (parent != null && parent.ProcessKeyPreview(ref m))
		{
			return true;
		}
		return ProcessKeyEventArgs(ref m);
	}

	/// <summary>Previews a keyboard message.</summary>
	/// <returns>true if the message was processed by the control; otherwise, false.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	protected virtual bool ProcessKeyPreview(ref Message m)
	{
		if (parent != null)
		{
			return parent.ProcessKeyPreview(ref m);
		}
		return false;
	}

	/// <summary>Processes a mnemonic character.</summary>
	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected virtual bool ProcessMnemonic(char charCode)
	{
		return false;
	}

	/// <summary>Raises the appropriate drag event.</summary>
	/// <param name="key">The event to raise. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseDragEvent(object key, DragEventArgs e)
	{
	}

	/// <summary>Raises the appropriate key event.</summary>
	/// <param name="key">The event to raise. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseKeyEvent(object key, KeyEventArgs e)
	{
	}

	/// <summary>Raises the appropriate mouse event.</summary>
	/// <param name="key">The event to raise. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseMouseEvent(object key, MouseEventArgs e)
	{
	}

	/// <summary>Raises the appropriate paint event.</summary>
	/// <param name="key">The event to raise. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaisePaintEvent(object key, PaintEventArgs e)
	{
	}

	private void SetIsRecreating()
	{
		is_recreating = true;
		Control[] allControls = Controls.GetAllControls();
		foreach (Control control in allControls)
		{
			control.SetIsRecreating();
		}
	}

	/// <summary>Forces the re-creation of the handle for the control.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RecreateHandle()
	{
		if (!IsHandleCreated)
		{
			return;
		}
		SetIsRecreating();
		if (IsHandleCreated)
		{
			DestroyHandle();
			return;
		}
		if (!is_created)
		{
			CreateControl();
		}
		else
		{
			CreateHandle();
		}
		is_recreating = false;
	}

	/// <summary>Resets the control to handle the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void ResetMouseEventArgs()
	{
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.ContentAlignment" /> to the appropriate <see cref="T:System.Drawing.ContentAlignment" /> to support right-to-left text.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</returns>
	/// <param name="align">One of the <see cref="T:System.Drawing.ContentAlignment" /> values. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected ContentAlignment RtlTranslateAlignment(ContentAlignment align)
	{
		if (right_to_left == RightToLeft.No)
		{
			return align;
		}
		return align switch
		{
			ContentAlignment.TopLeft => ContentAlignment.TopRight, 
			ContentAlignment.TopRight => ContentAlignment.TopLeft, 
			ContentAlignment.MiddleLeft => ContentAlignment.MiddleRight, 
			ContentAlignment.MiddleRight => ContentAlignment.MiddleLeft, 
			ContentAlignment.BottomLeft => ContentAlignment.BottomRight, 
			ContentAlignment.BottomRight => ContentAlignment.BottomLeft, 
			_ => align, 
		};
	}

	/// <summary>Converts the specified <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to support right-to-left text.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
	/// <param name="align">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected HorizontalAlignment RtlTranslateAlignment(HorizontalAlignment align)
	{
		if (right_to_left != 0)
		{
			switch (align)
			{
			case HorizontalAlignment.Center:
				break;
			case HorizontalAlignment.Left:
				return HorizontalAlignment.Right;
			default:
				return HorizontalAlignment.Left;
			}
		}
		return align;
	}

	/// <summary>Converts the specified <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to support right-to-left text.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</returns>
	/// <param name="align">One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected LeftRightAlignment RtlTranslateAlignment(LeftRightAlignment align)
	{
		if (right_to_left == RightToLeft.No)
		{
			return align;
		}
		if (align == LeftRightAlignment.Left)
		{
			return LeftRightAlignment.Right;
		}
		return LeftRightAlignment.Left;
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.ContentAlignment" /> to the appropriate <see cref="T:System.Drawing.ContentAlignment" /> to support right-to-left text.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</returns>
	/// <param name="align">One of the <see cref="T:System.Drawing.ContentAlignment" /> values. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected ContentAlignment RtlTranslateContent(ContentAlignment align)
	{
		return RtlTranslateAlignment(align);
	}

	/// <summary>Converts the specified <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to support right-to-left text.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
	/// <param name="align">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected HorizontalAlignment RtlTranslateHorizontal(HorizontalAlignment align)
	{
		return RtlTranslateAlignment(align);
	}

	/// <summary>Converts the specified <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to support right-to-left text.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</returns>
	/// <param name="align">One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected LeftRightAlignment RtlTranslateLeftRight(LeftRightAlignment align)
	{
		return RtlTranslateAlignment(align);
	}

	/// <summary>Scales a control's location, size, padding and margin.</summary>
	/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
	/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void ScaleControl(SizeF factor, BoundsSpecified specified)
	{
		Rectangle scaledBounds = GetScaledBounds(bounds, factor, specified);
		SetBounds(scaledBounds.X, scaledBounds.Y, scaledBounds.Width, scaledBounds.Height, specified);
	}

	/// <summary>This method is not relevant for this class.</summary>
	/// <param name="dx">The horizontal scaling factor.</param>
	/// <param name="dy">The vertical scaling factor.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected virtual void ScaleCore(float dx, float dy)
	{
		Rectangle scaledBoundsOld = GetScaledBoundsOld(bounds, new SizeF(dx, dy), BoundsSpecified.All);
		SuspendLayout();
		SetBounds(scaledBoundsOld.X, scaledBoundsOld.Y, scaledBoundsOld.Width, scaledBoundsOld.Height, BoundsSpecified.All);
		if (ScaleChildrenInternal)
		{
			Control[] allControls = Controls.GetAllControls();
			foreach (Control control in allControls)
			{
				control.Scale(dx, dy);
			}
		}
		ResumeLayout();
	}

	/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
	/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
	/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
	protected virtual void Select(bool directed, bool forward)
	{
		IContainerControl containerControl = GetContainerControl();
		if (containerControl != null && (Control)containerControl != this)
		{
			containerControl.ActiveControl = this;
		}
	}

	/// <summary>Sets a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
	/// <param name="mode">One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</param>
	protected void SetAutoSizeMode(AutoSizeMode mode)
	{
		if (auto_size_mode != mode)
		{
			auto_size_mode = mode;
			PerformLayout(this, "AutoSizeMode");
		}
	}

	/// <summary>Performs the work of setting the specified bounds of this control.</summary>
	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		SetBoundsCoreInternal(x, y, width, height, specified);
	}

	internal virtual void SetBoundsCoreInternal(int x, int y, int width, int height, BoundsSpecified specified)
	{
		height = OverrideHeight(height);
		Rectangle rectangle = explicit_bounds;
		Rectangle rectangle2 = new Rectangle(x, y, width, height);
		if (IsHandleCreated)
		{
			XplatUI.SetWindowPos(Handle, x, y, width, height);
			XplatUI.GetWindowPos(Handle, this is Form, out var _, out var _, out width, out height, out var _, out var _);
		}
		if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
		{
			explicit_bounds.X = rectangle2.X;
		}
		else
		{
			explicit_bounds.X = rectangle.X;
		}
		if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
		{
			explicit_bounds.Y = rectangle2.Y;
		}
		else
		{
			explicit_bounds.Y = rectangle.Y;
		}
		if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
		{
			explicit_bounds.Width = rectangle2.Width;
		}
		else
		{
			explicit_bounds.Width = rectangle.Width;
		}
		if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
		{
			explicit_bounds.Height = rectangle2.Height;
		}
		else
		{
			explicit_bounds.Height = rectangle.Height;
		}
		Rectangle rectangle3 = explicit_bounds;
		UpdateBounds(x, y, width, height);
		if (explicit_bounds.X == x)
		{
			explicit_bounds.X = rectangle3.X;
		}
		if (explicit_bounds.Y == y)
		{
			explicit_bounds.Y = rectangle3.Y;
		}
		if (explicit_bounds.Width == width)
		{
			explicit_bounds.Width = rectangle3.Width;
		}
		if (explicit_bounds.Height == height)
		{
			explicit_bounds.Height = rectangle3.Height;
		}
	}

	/// <summary>Sets the size of the client area of the control.</summary>
	/// <param name="x">The client area width, in pixels. </param>
	/// <param name="y">The client area height, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void SetClientSizeCore(int x, int y)
	{
		Size size = InternalSizeFromClientSize(new Size(x, y));
		if (size != Size.Empty)
		{
			SetBounds(bounds.X, bounds.Y, size.Width, size.Height, BoundsSpecified.Size);
		}
	}

	/// <summary>Sets a specified <see cref="T:System.Windows.Forms.ControlStyles" /> flag to either true or false.</summary>
	/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to set. </param>
	/// <param name="value">true to apply the specified style to the control; otherwise, false. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal void SetStyle(ControlStyles flag, bool value)
	{
		if (value)
		{
			control_style |= flag;
		}
		else
		{
			control_style &= ~flag;
		}
	}

	/// <summary>Sets the control as the top-level control.</summary>
	/// <param name="value">true to set the control as the top-level control; otherwise, false. </param>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="value" /> parameter is set to true and the control is an ActiveX control. </exception>
	/// <exception cref="T:System.Exception">The <see cref="M:System.Windows.Forms.Control.GetTopLevel" /> return value is not equal to the <paramref name="value" /> parameter and the <see cref="P:System.Windows.Forms.Control.Parent" /> property is not null. </exception>
	protected void SetTopLevel(bool value)
	{
		if (GetTopLevel() != value && parent != null)
		{
			throw new ArgumentException("Cannot change toplevel style of a parented control.");
		}
		if (this is Form)
		{
			if (IsHandleCreated && value != Visible)
			{
				Visible = value;
			}
		}
		else if (!IsHandleCreated)
		{
			CreateHandle();
		}
		is_toplevel = value;
	}

	/// <summary>Sets the control to the specified visible state.</summary>
	/// <param name="value">true to make the control visible; otherwise, false. </param>
	protected virtual void SetVisibleCore(bool value)
	{
		if (value == is_visible)
		{
			return;
		}
		is_visible = value;
		if (is_visible && (window.Handle == IntPtr.Zero || !is_created))
		{
			CreateControl();
			if (!(this is Form))
			{
				UpdateZOrder();
			}
		}
		if (IsHandleCreated)
		{
			XplatUI.SetVisible(Handle, is_visible, activate: true);
			if (!is_visible)
			{
				if (parent != null && parent.IsHandleCreated)
				{
					parent.Invalidate(bounds);
					parent.Update();
				}
				else
				{
					Refresh();
				}
			}
			else if (is_visible && this is Form)
			{
				if ((this as Form).WindowState != 0)
				{
					OnVisibleChanged(EventArgs.Empty);
				}
				else
				{
					XplatUI.SetWindowPos(window.Handle, bounds.X, bounds.Y, bounds.Width, bounds.Height);
				}
			}
			else if (parent != null)
			{
				parent.UpdateZOrderOfChild(this);
			}
			if (!(this is Form))
			{
				OnVisibleChanged(EventArgs.Empty);
			}
		}
		else
		{
			OnVisibleChanged(EventArgs.Empty);
		}
	}

	/// <summary>Determines the size of the entire control from the height and width of its client area.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value representing the height and width of the entire control.</returns>
	/// <param name="clientSize">A <see cref="T:System.Drawing.Size" /> value representing the height and width of the control's client area.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual Size SizeFromClientSize(Size clientSize)
	{
		return InternalSizeFromClientSize(clientSize);
	}

	/// <summary>Updates the bounds of the control with the current size and location.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void UpdateBounds()
	{
		if (IsHandleCreated)
		{
			XplatUI.GetWindowPos(Handle, this is Form, out var x, out var y, out var width, out var height, out var client_width, out var client_height);
			UpdateBounds(x, y, width, height, client_width, client_height);
		}
	}

	/// <summary>Updates the bounds of the control with the specified size and location.</summary>
	/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control. </param>
	/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control. </param>
	/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
	/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void UpdateBounds(int x, int y, int width, int height)
	{
		Rectangle ClientRect = new Rectangle(0, 0, 0, 0);
		CreateParams createParams = CreateParams;
		XplatUI.CalculateWindowRect(ref ClientRect, createParams, createParams.menu, out ClientRect);
		UpdateBounds(x, y, width, height, width - (ClientRect.Right - ClientRect.Left), height - (ClientRect.Bottom - ClientRect.Top));
	}

	/// <summary>Updates the bounds of the control with the specified size, location, and client size.</summary>
	/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control. </param>
	/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control. </param>
	/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
	/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
	/// <param name="clientWidth">The client <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
	/// <param name="clientHeight">The client <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void UpdateBounds(int x, int y, int width, int height, int clientWidth, int clientHeight)
	{
		bool flag = false;
		bool flag2 = false;
		if (bounds.X != x || bounds.Y != y)
		{
			flag = true;
		}
		if (Bounds.Width != width || Bounds.Height != height)
		{
			flag2 = true;
		}
		bounds.X = x;
		bounds.Y = y;
		bounds.Width = width;
		bounds.Height = height;
		explicit_bounds = bounds;
		client_size.Width = clientWidth;
		client_size.Height = clientHeight;
		if (flag)
		{
			OnLocationChanged(EventArgs.Empty);
			if (!background_color.IsEmpty && background_color.A < byte.MaxValue)
			{
				Invalidate();
			}
		}
		if (flag2)
		{
			OnSizeInitializedOrChanged();
			OnSizeChanged(EventArgs.Empty);
			OnClientSizeChanged(EventArgs.Empty);
		}
	}

	/// <summary>Forces the assigned styles to be reapplied to the control.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void UpdateStyles()
	{
		if (IsHandleCreated)
		{
			XplatUI.SetWindowStyle(window.Handle, CreateParams);
			OnStyleChanged(EventArgs.Empty);
		}
	}

	private void UpdateZOrderOfChild(Control child)
	{
		if (!IsHandleCreated || !child.IsHandleCreated || child.parent != this || !Hwnd.ObjectFromHandle(child.Handle).Mapped)
		{
			return;
		}
		Control[] allControls = child_controls.GetAllControls();
		int num = Array.IndexOf(allControls, child);
		while (num > 0 && (!allControls[num - 1].IsHandleCreated || !allControls[num - 1].VisibleInternal || !Hwnd.ObjectFromHandle(allControls[num - 1].Handle).Mapped))
		{
			num--;
		}
		if (num > 0)
		{
			XplatUI.SetZOrder(child.Handle, allControls[num - 1].Handle, Top: false, Bottom: false);
			return;
		}
		IntPtr intPtr = AfterTopMostControl();
		if (intPtr != IntPtr.Zero && intPtr != child.Handle)
		{
			XplatUI.SetZOrder(child.Handle, intPtr, Top: false, Bottom: false);
		}
		else
		{
			XplatUI.SetZOrder(child.Handle, IntPtr.Zero, Top: true, Bottom: false);
		}
	}

	internal virtual IntPtr AfterTopMostControl()
	{
		return IntPtr.Zero;
	}

	internal void UpdateChildrenZOrder()
	{
		if (!IsHandleCreated)
		{
			return;
		}
		Control[] array;
		if (child_controls.ImplicitControls == null)
		{
			array = new Control[child_controls.Count];
			child_controls.CopyTo(array, 0);
		}
		else
		{
			array = new Control[child_controls.Count + child_controls.ImplicitControls.Count];
			child_controls.CopyTo(array, 0);
			child_controls.ImplicitControls.CopyTo(array, child_controls.Count);
		}
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsHandleCreated && array[i].VisibleInternal)
			{
				Hwnd hwnd = Hwnd.ObjectFromHandle(array[i].Handle);
				if (!hwnd.zero_sized)
				{
					arrayList.Add(array[i]);
				}
			}
		}
		for (int j = 1; j < arrayList.Count; j++)
		{
			Control control = (Control)arrayList[j - 1];
			Control control2 = (Control)arrayList[j];
			XplatUI.SetZOrder(control2.Handle, control.Handle, Top: false, Bottom: false);
		}
	}

	/// <summary>Updates the control in its parent's z-order.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void UpdateZOrder()
	{
		if (parent != null)
		{
			parent.UpdateZOrderOfChild(this);
		}
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected virtual void WndProc(ref Message m)
	{
		if ((control_style & ControlStyles.EnableNotifyMessage) != 0)
		{
			OnNotifyMessage(m);
		}
		switch ((Msg)m.Msg)
		{
		case Msg.WM_DESTROY:
			WmDestroy(ref m);
			break;
		case Msg.WM_WINDOWPOSCHANGED:
			WmWindowPosChanged(ref m);
			break;
		case Msg.WM_PAINT:
			WmPaint(ref m);
			break;
		case Msg.WM_ERASEBKGND:
			WmEraseBackground(ref m);
			break;
		case Msg.WM_LBUTTONUP:
			WmLButtonUp(ref m);
			break;
		case Msg.WM_LBUTTONDOWN:
			WmLButtonDown(ref m);
			break;
		case Msg.WM_LBUTTONDBLCLK:
			WmLButtonDblClick(ref m);
			break;
		case Msg.WM_MBUTTONUP:
			WmMButtonUp(ref m);
			break;
		case Msg.WM_MBUTTONDOWN:
			WmMButtonDown(ref m);
			break;
		case Msg.WM_MBUTTONDBLCLK:
			WmMButtonDblClick(ref m);
			break;
		case Msg.WM_RBUTTONUP:
			WmRButtonUp(ref m);
			break;
		case Msg.WM_RBUTTONDOWN:
			WmRButtonDown(ref m);
			break;
		case Msg.WM_RBUTTONDBLCLK:
			WmRButtonDblClick(ref m);
			break;
		case Msg.WM_CONTEXTMENU:
			WmContextMenu(ref m);
			break;
		case Msg.WM_MOUSEWHEEL:
			WmMouseWheel(ref m);
			break;
		case Msg.WM_MOUSEMOVE:
			WmMouseMove(ref m);
			break;
		case Msg.WM_SHOWWINDOW:
			WmShowWindow(ref m);
			break;
		case Msg.WM_CREATE:
			WmCreate(ref m);
			break;
		case Msg.WM_MOUSE_ENTER:
			WmMouseEnter(ref m);
			break;
		case Msg.WM_MOUSELEAVE:
			WmMouseLeave(ref m);
			break;
		case Msg.WM_MOUSEHOVER:
			WmMouseHover(ref m);
			break;
		case Msg.WM_SYSKEYUP:
			WmSysKeyUp(ref m);
			break;
		case Msg.WM_KEYDOWN:
		case Msg.WM_KEYUP:
		case Msg.WM_CHAR:
		case Msg.WM_SYSKEYDOWN:
		case Msg.WM_SYSCHAR:
			WmKeys(ref m);
			break;
		case Msg.WM_HELP:
			WmHelp(ref m);
			break;
		case Msg.WM_KILLFOCUS:
			WmKillFocus(ref m);
			break;
		case Msg.WM_SETFOCUS:
			WmSetFocus(ref m);
			break;
		case Msg.WM_SYSCOLORCHANGE:
			WmSysColorChange(ref m);
			break;
		case Msg.WM_SETCURSOR:
			WmSetCursor(ref m);
			break;
		case Msg.WM_CAPTURECHANGED:
			WmCaptureChanged(ref m);
			break;
		case Msg.WM_CHANGEUISTATE:
			WmChangeUIState(ref m);
			break;
		case Msg.WM_UPDATEUISTATE:
			WmUpdateUIState(ref m);
			break;
		default:
			DefWndProc(ref m);
			break;
		}
	}

	private void WmDestroy(ref Message m)
	{
		OnHandleDestroyed(EventArgs.Empty);
		window.InvalidateHandle();
		is_created = false;
		if (is_recreating)
		{
			CreateHandle();
			is_recreating = false;
		}
		if (is_disposing)
		{
			is_disposing = false;
			is_visible = false;
		}
	}

	private void WmWindowPosChanged(ref Message m)
	{
		if (Visible)
		{
			Rectangle rectangle = explicit_bounds;
			UpdateBounds();
			explicit_bounds = rectangle;
			if (GetStyle(ControlStyles.ResizeRedraw))
			{
				Invalidate();
			}
		}
	}

	private void WmPaint(ref Message m)
	{
		IntPtr handle = Handle;
		PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, handle, client: true);
		if (paintEventArgs != null)
		{
			DoubleBuffer doubleBuffer = null;
			if (UseDoubleBuffering)
			{
				doubleBuffer = GetBackBuffer();
				doubleBuffer.Start(paintEventArgs);
			}
			if (GetStyle(ControlStyles.OptimizedDoubleBuffer))
			{
				paintEventArgs.Graphics.SetClip(Rectangle.Intersect(paintEventArgs.ClipRectangle, ClientRectangle));
			}
			if (!GetStyle(ControlStyles.Opaque))
			{
				OnPaintBackground(paintEventArgs);
			}
			OnPaintBackgroundInternal(paintEventArgs);
			OnPaintInternal(paintEventArgs);
			if (!paintEventArgs.Handled)
			{
				OnPaint(paintEventArgs);
			}
			doubleBuffer?.End(paintEventArgs);
			XplatUI.PaintEventEnd(ref m, handle, client: true);
		}
	}

	private void WmEraseBackground(ref Message m)
	{
		m.Result = (IntPtr)1;
	}

	private void WmLButtonUp(ref Message m)
	{
		if (XplatUI.IsEnabled(Handle) && active_tracker != null)
		{
			ProcessActiveTracker(ref m);
			return;
		}
		MouseEventArgs mouseEventArgs = new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()) | MouseButtons.Left, mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0);
		HandleClick(mouse_clicks, mouseEventArgs);
		OnMouseUp(mouseEventArgs);
		if (InternalCapture)
		{
			InternalCapture = false;
		}
		if (mouse_clicks > 1)
		{
			mouse_clicks = 1;
		}
	}

	private void WmLButtonDown(ref Message m)
	{
		if (XplatUI.IsEnabled(Handle) && active_tracker != null)
		{
			ProcessActiveTracker(ref m);
			return;
		}
		ValidationFailed = false;
		if (CanSelect)
		{
			Select(directed: true, forward: true);
		}
		if (!ValidationFailed)
		{
			InternalCapture = true;
			OnMouseDown(new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0));
		}
	}

	private void WmLButtonDblClick(ref Message m)
	{
		InternalCapture = true;
		mouse_clicks++;
		OnMouseDown(new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0));
	}

	private void WmMButtonUp(ref Message m)
	{
		MouseEventArgs mouseEventArgs = new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()) | MouseButtons.Middle, mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0);
		HandleClick(mouse_clicks, mouseEventArgs);
		OnMouseUp(mouseEventArgs);
		if (InternalCapture)
		{
			InternalCapture = false;
		}
		if (mouse_clicks > 1)
		{
			mouse_clicks = 1;
		}
	}

	private void WmMButtonDown(ref Message m)
	{
		InternalCapture = true;
		OnMouseDown(new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0));
	}

	private void WmMButtonDblClick(ref Message m)
	{
		InternalCapture = true;
		mouse_clicks++;
		OnMouseDown(new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0));
	}

	private void WmRButtonUp(ref Message m)
	{
		if (XplatUI.IsEnabled(Handle) && active_tracker != null)
		{
			ProcessActiveTracker(ref m);
			return;
		}
		Point p = new Point(LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()));
		p = PointToScreen(p);
		MouseEventArgs mouseEventArgs = new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()) | MouseButtons.Right, mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0);
		HandleClick(mouse_clicks, mouseEventArgs);
		XplatUI.SendMessage(m.HWnd, Msg.WM_CONTEXTMENU, m.HWnd, (IntPtr)(p.X + (p.Y << 16)));
		OnMouseUp(mouseEventArgs);
		if (InternalCapture)
		{
			InternalCapture = false;
		}
		if (mouse_clicks > 1)
		{
			mouse_clicks = 1;
		}
	}

	private void WmRButtonDown(ref Message m)
	{
		if (XplatUI.IsEnabled(Handle) && active_tracker != null)
		{
			ProcessActiveTracker(ref m);
			return;
		}
		InternalCapture = true;
		OnMouseDown(new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0));
	}

	private void WmRButtonDblClick(ref Message m)
	{
		InternalCapture = true;
		mouse_clicks++;
		OnMouseDown(new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0));
	}

	private void WmContextMenu(ref Message m)
	{
		if (context_menu != null)
		{
			Point p = new Point(LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()));
			if (p.X == -1 || p.Y == -1)
			{
				p.X = Width / 2 + Left;
				p.Y = Height / 2 + Top;
				p = PointToScreen(p);
			}
			context_menu.Show(this, PointToClient(p));
		}
		else if (context_menu == null && context_menu_strip != null)
		{
			Point p2 = new Point(LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()));
			if (p2.X == -1 || p2.Y == -1)
			{
				p2.X = Width / 2 + Left;
				p2.Y = Height / 2 + Top;
				p2 = PointToScreen(p2);
			}
			context_menu_strip.SetSourceControl(this);
			context_menu_strip.Show(this, PointToClient(p2));
		}
		else
		{
			DefWndProc(ref m);
		}
	}

	private void WmCreate(ref Message m)
	{
		OnHandleCreated(EventArgs.Empty);
	}

	private void WmMouseWheel(ref Message m)
	{
		DefWndProc(ref m);
		OnMouseWheel(new MouseEventArgs(FromParamToMouseButtons((long)m.WParam), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), HighOrder((long)m.WParam)));
	}

	private void WmMouseMove(ref Message m)
	{
		if (XplatUI.IsEnabled(Handle) && active_tracker != null)
		{
			MouseEventArgs args = new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, MousePosition.X, MousePosition.Y, 0);
			active_tracker.OnMotion(args);
		}
		else
		{
			OnMouseMove(new MouseEventArgs(FromParamToMouseButtons(m.WParam.ToInt32()), mouse_clicks, LowOrder(m.LParam.ToInt32()), HighOrder(m.LParam.ToInt32()), 0));
		}
	}

	private void WmMouseEnter(ref Message m)
	{
		if (!is_entered)
		{
			is_entered = true;
			OnMouseEnter(EventArgs.Empty);
		}
	}

	private void WmMouseLeave(ref Message m)
	{
		is_entered = false;
		OnMouseLeave(EventArgs.Empty);
	}

	private void WmMouseHover(ref Message m)
	{
		OnMouseHover(EventArgs.Empty);
	}

	private void WmShowWindow(ref Message m)
	{
		if (IsDisposed)
		{
			return;
		}
		Form form = this as Form;
		if (m.WParam.ToInt32() != 0)
		{
			if (m.LParam.ToInt32() == 0)
			{
				CreateControl();
				Control[] allControls = child_controls.GetAllControls();
				for (int i = 0; i < allControls.Length; i++)
				{
					if (allControls[i].is_visible && allControls[i].IsHandleCreated && XplatUI.GetParent(allControls[i].Handle) != window.Handle)
					{
						XplatUI.SetParent(allControls[i].Handle, window.Handle);
					}
				}
				UpdateChildrenZOrder();
			}
		}
		else if (parent != null && Focused)
		{
			Control control = (Control)parent.GetContainerControl();
			if (control != null && (form == null || !form.IsMdiChild))
			{
				control.SelectNextControl(this, forward: true, tabStopOnly: true, nested: true, wrap: true);
			}
		}
		if (form != null)
		{
			form.waiting_showwindow = false;
		}
		if (form != null)
		{
			if (!IsRecreating && (form.IsMdiChild || form.WindowState == FormWindowState.Normal))
			{
				OnVisibleChanged(EventArgs.Empty);
			}
		}
		else if (is_toplevel)
		{
			OnVisibleChanged(EventArgs.Empty);
		}
	}

	private void WmSysKeyUp(ref Message m)
	{
		if (ProcessKeyMessage(ref m))
		{
			m.Result = IntPtr.Zero;
			return;
		}
		if ((m.WParam.ToInt32() & 0xFFFF) == 18)
		{
			Form form = FindForm();
			if (form != null && form.ActiveMenu != null)
			{
				form.ActiveMenu.ProcessCmdKey(ref m, (Keys)m.WParam.ToInt32());
			}
			else if (ToolStripManager.ProcessMenuKey(ref m))
			{
				return;
			}
		}
		DefWndProc(ref m);
	}

	private void WmKeys(ref Message m)
	{
		if (ProcessKeyMessage(ref m))
		{
			m.Result = IntPtr.Zero;
		}
		else
		{
			DefWndProc(ref m);
		}
	}

	private void WmHelp(ref Message m)
	{
		Point mousePos;
		if (m.LParam != IntPtr.Zero)
		{
			HELPINFO hELPINFO = default(HELPINFO);
			hELPINFO = (HELPINFO)Marshal.PtrToStructure(m.LParam, typeof(HELPINFO));
			mousePos = new Point(hELPINFO.MousePos.x, hELPINFO.MousePos.y);
		}
		else
		{
			mousePos = MousePosition;
		}
		OnHelpRequested(new HelpEventArgs(mousePos));
		m.Result = (IntPtr)1;
	}

	private void WmKillFocus(ref Message m)
	{
		has_focus = false;
		OnLostFocus(EventArgs.Empty);
	}

	private void WmSetFocus(ref Message m)
	{
		if (!has_focus)
		{
			has_focus = true;
			OnGotFocus(EventArgs.Empty);
		}
	}

	private void WmSysColorChange(ref Message m)
	{
		ThemeEngine.Current.ResetDefaults();
		OnSystemColorsChanged(EventArgs.Empty);
	}

	private void WmSetCursor(ref Message m)
	{
		if ((cursor == null && !use_wait_cursor) || (m.LParam.ToInt32() & 0xFFFF) != 1)
		{
			DefWndProc(ref m);
			return;
		}
		XplatUI.SetCursor(window.Handle, Cursor.handle);
		m.Result = (IntPtr)1;
	}

	private void WmCaptureChanged(ref Message m)
	{
		is_captured = false;
		OnMouseCaptureChanged(EventArgs.Empty);
		m.Result = (IntPtr)0;
	}

	private void WmChangeUIState(ref Message m)
	{
		foreach (Control control in Controls)
		{
			XplatUI.SendMessage(control.Handle, Msg.WM_UPDATEUISTATE, m.WParam, m.LParam);
		}
	}

	private void WmUpdateUIState(ref Message m)
	{
		int num = LowOrder(m.WParam.ToInt32());
		int num2 = HighOrder(m.WParam.ToInt32());
		if (num != 3)
		{
			UICues uICues = UICues.None;
			if ((num2 & 2) != 0 && num == 2 != show_keyboard_cues)
			{
				uICues |= UICues.ChangeKeyboard;
				show_keyboard_cues = num == 2;
			}
			if ((num2 & 1) != 0 && num == 2 != show_focus_cues)
			{
				uICues |= UICues.ChangeFocus;
				show_focus_cues = num == 2;
			}
			if ((uICues & UICues.Changed) != 0)
			{
				OnChangeUICues(new UICuesEventArgs(uICues));
				Invalidate();
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.AutoSizeChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnAutoSizeChanged(EventArgs e)
	{
		((EventHandler)base.Events[AutoSizeChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnBackColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[BackColorChanged])?.Invoke(this, e);
		for (int i = 0; i < child_controls.Count; i++)
		{
			child_controls[i].OnParentBackColorChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnBackgroundImageChanged(EventArgs e)
	{
		((EventHandler)base.Events[BackgroundImageChanged])?.Invoke(this, e);
		for (int i = 0; i < child_controls.Count; i++)
		{
			child_controls[i].OnParentBackgroundImageChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
	{
		((EventHandler)base.Events[BackgroundImageLayoutChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnBindingContextChanged(EventArgs e)
	{
		CheckDataBindings();
		((EventHandler)base.Events[BindingContextChanged])?.Invoke(this, e);
		for (int i = 0; i < child_controls.Count; i++)
		{
			child_controls[i].OnParentBindingContextChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CausesValidationChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnCausesValidationChanged(EventArgs e)
	{
		((EventHandler)base.Events[CausesValidationChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.UICuesEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnChangeUICues(UICuesEventArgs e)
	{
		((UICuesEventHandler)base.Events[ChangeUICues])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnClick(EventArgs e)
	{
		((EventHandler)base.Events[Click])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ClientSizeChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnClientSizeChanged(EventArgs e)
	{
		((EventHandler)base.Events[ClientSizeChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnContextMenuChanged(EventArgs e)
	{
		((EventHandler)base.Events[ContextMenuChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuStripChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnContextMenuStripChanged(EventArgs e)
	{
		((EventHandler)base.Events[ContextMenuStripChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnControlAdded(ControlEventArgs e)
	{
		((ControlEventHandler)base.Events[ControlAdded])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnControlRemoved(ControlEventArgs e)
	{
		((ControlEventHandler)base.Events[ControlRemoved])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnCreateControl()
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnCursorChanged(EventArgs e)
	{
		((EventHandler)base.Events[CursorChanged])?.Invoke(this, e);
		for (int i = 0; i < child_controls.Count; i++)
		{
			child_controls[i].OnParentCursorChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDockChanged(EventArgs e)
	{
		((EventHandler)base.Events[DockChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDoubleClick(EventArgs e)
	{
		((EventHandler)base.Events[DoubleClick])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
	/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragDrop(DragEventArgs drgevent)
	{
		((DragEventHandler)base.Events[DragDrop])?.Invoke(this, drgevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
	/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragEnter(DragEventArgs drgevent)
	{
		((DragEventHandler)base.Events[DragEnter])?.Invoke(this, drgevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragLeave(EventArgs e)
	{
		((EventHandler)base.Events[DragLeave])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
	/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragOver(DragEventArgs drgevent)
	{
		((DragEventHandler)base.Events[DragOver])?.Invoke(this, drgevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnEnabledChanged(EventArgs e)
	{
		if (IsHandleCreated)
		{
			if (this is Form)
			{
				if (((Form)this).context == null)
				{
					XplatUI.EnableWindow(window.Handle, Enabled);
				}
			}
			else
			{
				XplatUI.EnableWindow(window.Handle, Enabled);
			}
			Refresh();
		}
		((EventHandler)base.Events[EnabledChanged])?.Invoke(this, e);
		Control[] allControls = Controls.GetAllControls();
		foreach (Control control in allControls)
		{
			control.OnParentEnabledChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnEnter(EventArgs e)
	{
		((EventHandler)base.Events[Enter])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnFontChanged(EventArgs e)
	{
		((EventHandler)base.Events[FontChanged])?.Invoke(this, e);
		for (int i = 0; i < child_controls.Count; i++)
		{
			child_controls[i].OnParentFontChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnForeColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[ForeColorChanged])?.Invoke(this, e);
		for (int i = 0; i < child_controls.Count; i++)
		{
			child_controls[i].OnParentForeColorChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GiveFeedback" /> event.</summary>
	/// <param name="gfbevent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
	{
		((GiveFeedbackEventHandler)base.Events[GiveFeedback])?.Invoke(this, gfbevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnGotFocus(EventArgs e)
	{
		((EventHandler)base.Events[GotFocus])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnHandleCreated(EventArgs e)
	{
		((EventHandler)base.Events[HandleCreated])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnHandleDestroyed(EventArgs e)
	{
		((EventHandler)base.Events[HandleDestroyed])?.Invoke(this, e);
	}

	internal void RaiseHelpRequested(HelpEventArgs hevent)
	{
		OnHelpRequested(hevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
	/// <param name="hevent">A <see cref="T:System.Windows.Forms.HelpEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnHelpRequested(HelpEventArgs hevent)
	{
		((HelpEventHandler)base.Events[HelpRequested])?.Invoke(this, hevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ImeModeChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnImeModeChanged(EventArgs e)
	{
		((EventHandler)base.Events[ImeModeChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnInvalidated(InvalidateEventArgs e)
	{
		if (UseDoubleBuffering)
		{
			if (e.InvalidRect == ClientRectangle)
			{
				InvalidateBackBuffer();
			}
			else if (backbuffer != null)
			{
				Rectangle rect = Rectangle.Inflate(e.InvalidRect, 1, 1);
				backbuffer.InvalidRegion.Union(rect);
			}
		}
		((InvalidateEventHandler)base.Events[Invalidated])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnKeyDown(KeyEventArgs e)
	{
		((KeyEventHandler)base.Events[KeyDown])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnKeyPress(KeyPressEventArgs e)
	{
		((KeyPressEventHandler)base.Events[KeyPress])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnKeyUp(KeyEventArgs e)
	{
		((KeyEventHandler)base.Events[KeyUp])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnLayout(LayoutEventArgs levent)
	{
		((LayoutEventHandler)base.Events[Layout])?.Invoke(this, levent);
		Size size = Size;
		if (Parent != null && AutoSize && !nested_layout && PreferredSize != size)
		{
			nested_layout = true;
			Parent.PerformLayout();
			nested_layout = false;
		}
		LayoutEngine.Layout(this, levent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnLeave(EventArgs e)
	{
		((EventHandler)base.Events[Leave])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnLocationChanged(EventArgs e)
	{
		OnMove(e);
		((EventHandler)base.Events[LocationChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnLostFocus(EventArgs e)
	{
		((EventHandler)base.Events[LostFocus])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MarginChanged" /> event. </summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnMarginChanged(EventArgs e)
	{
		((EventHandler)base.Events[MarginChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseCaptureChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseCaptureChanged(EventArgs e)
	{
		((EventHandler)base.Events[MouseCaptureChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseClick(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseClick])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseDoubleClick(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseDoubleClick])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseDown])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseEnter(EventArgs e)
	{
		((EventHandler)base.Events[MouseEnter])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseHover(EventArgs e)
	{
		((EventHandler)base.Events[MouseHover])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseLeave(EventArgs e)
	{
		((EventHandler)base.Events[MouseLeave])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseMove])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseUp])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMouseWheel(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseWheel])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Move" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnMove(EventArgs e)
	{
		((EventHandler)base.Events[Move])?.Invoke(this, e);
	}

	/// <summary>Notifies the control of Windows messages.</summary>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that represents the Windows message. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnNotifyMessage(Message m)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnPaddingChanged(EventArgs e)
	{
		((EventHandler)base.Events[PaddingChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnPaint(PaintEventArgs e)
	{
		((PaintEventHandler)base.Events[Paint])?.Invoke(this, e);
	}

	internal virtual void OnPaintBackgroundInternal(PaintEventArgs e)
	{
	}

	internal virtual void OnPaintInternal(PaintEventArgs e)
	{
	}

	/// <summary>Paints the background of the control.</summary>
	/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnPaintBackground(PaintEventArgs pevent)
	{
		PaintControlBackground(pevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BackColor" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentBackColorChanged(EventArgs e)
	{
		if (background_color.IsEmpty && background_image == null)
		{
			Invalidate();
			OnBackColorChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentBackgroundImageChanged(EventArgs e)
	{
		Invalidate();
		OnBackgroundImageChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BindingContext" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentBindingContextChanged(EventArgs e)
	{
		if (binding_context == null && Parent != null)
		{
			binding_context = Parent.binding_context;
			OnBindingContextChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentChanged(EventArgs e)
	{
		((EventHandler)base.Events[ParentChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentCursorChanged(EventArgs e)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentEnabledChanged(EventArgs e)
	{
		if (is_enabled)
		{
			OnEnabledChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Font" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentFontChanged(EventArgs e)
	{
		if (font == null)
		{
			Invalidate();
			OnFontChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentForeColorChanged(EventArgs e)
	{
		if (foreground_color.IsEmpty)
		{
			Invalidate();
			OnForeColorChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentRightToLeftChanged(EventArgs e)
	{
		if (right_to_left == RightToLeft.Inherit)
		{
			Invalidate();
			OnRightToLeftChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value of the control's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentVisibleChanged(EventArgs e)
	{
		if (is_visible)
		{
			OnVisibleChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
	/// <param name="qcdevent">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
	{
		((QueryContinueDragEventHandler)base.Events[QueryContinueDrag])?.Invoke(this, qcdevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
	{
		((PreviewKeyDownEventHandler)base.Events[PreviewKeyDown])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnPrint(PaintEventArgs e)
	{
		((PaintEventHandler)base.Events[Paint])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RegionChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnRegionChanged(EventArgs e)
	{
		((EventHandler)base.Events[RegionChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnResize(EventArgs e)
	{
		OnResizeInternal(e);
	}

	internal virtual void OnResizeInternal(EventArgs e)
	{
		PerformLayout(this, "Bounds");
		((EventHandler)base.Events[Resize])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnRightToLeftChanged(EventArgs e)
	{
		((EventHandler)base.Events[RightToLeftChanged])?.Invoke(this, e);
		for (int i = 0; i < child_controls.Count; i++)
		{
			child_controls[i].OnParentRightToLeftChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnSizeChanged(EventArgs e)
	{
		DisposeBackBuffer();
		OnResize(e);
		((EventHandler)base.Events[SizeChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.StyleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnStyleChanged(EventArgs e)
	{
		((EventHandler)base.Events[StyleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnSystemColorsChanged(EventArgs e)
	{
		((EventHandler)base.Events[SystemColorsChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabIndexChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnTabIndexChanged(EventArgs e)
	{
		((EventHandler)base.Events[TabIndexChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnTabStopChanged(EventArgs e)
	{
		((EventHandler)base.Events[TabStopChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnTextChanged(EventArgs e)
	{
		((EventHandler)base.Events[TextChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnValidated(EventArgs e)
	{
		((EventHandler)base.Events[Validated])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnValidating(CancelEventArgs e)
	{
		((CancelEventHandler)base.Events[Validating])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnVisibleChanged(EventArgs e)
	{
		if (Visible)
		{
			CreateControl();
		}
		((EventHandler)base.Events[VisibleChanged])?.Invoke(this, e);
		Control[] allControls = Controls.GetAllControls();
		foreach (Control control in allControls)
		{
			if (control.Visible)
			{
				control.OnParentVisibleChanged(e);
			}
		}
	}
}
