using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Provides a collection container that enables ASP.NET server controls to maintain a list of their child controls.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ControlCollection : ICollection, IEnumerable
{
	private sealed class SimpleEnumerator : IEnumerator
	{
		private ControlCollection coll;

		private int index;

		private int version;

		private object currentElement;

		public object Current
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException((index == -1) ? "Enumerator not started" : "Enumerator ended");
				}
				return currentElement;
			}
		}

		public SimpleEnumerator(ControlCollection coll)
		{
			this.coll = coll;
			index = -1;
			version = coll.version;
		}

		public bool MoveNext()
		{
			if (version != coll.version)
			{
				throw new InvalidOperationException("List has changed.");
			}
			if (index >= -1 && ++index < coll.Count)
			{
				currentElement = coll[index];
				return true;
			}
			index = -2;
			return false;
		}

		public void Reset()
		{
			if (version != coll.version)
			{
				throw new InvalidOperationException("List has changed.");
			}
			index = -1;
		}
	}

	private Control owner;

	private Control[] controls;

	private int version;

	private int count;

	private bool readOnly;

	/// <summary>Gets the number of server controls in the <see cref="T:System.Web.UI.ControlCollection" /> object for the specified ASP.NET server control.</summary>
	/// <returns>The number of server controls in the <see cref="T:System.Web.UI.ControlCollection" />.</returns>
	public virtual int Count => count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ControlCollection" /> object is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool IsReadOnly => readOnly;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ControlCollection" /> object is synchronized.</summary>
	/// <returns>This property is always <see langword="false" />.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets a reference to the server control at the specified index location in the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <param name="index">The location of the server control in the <see cref="T:System.Web.UI.ControlCollection" />. </param>
	/// <returns>The reference to the control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to <see cref="P:System.Web.UI.ControlCollection.Count" />. </exception>
	public virtual Control this[int index]
	{
		get
		{
			if (index < 0 || index >= count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return controls[index];
		}
	}

	/// <summary>Gets the ASP.NET server control to which the <see cref="T:System.Web.UI.ControlCollection" /> object belongs.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Control" /> to which the <see cref="T:System.Web.UI.ControlCollection" /> belongs.</returns>
	protected Control Owner => owner;

	/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
	/// <returns>The <see cref="T:System.Object" /> used to synchronize the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlCollection" /> class for the specified parent server control.</summary>
	/// <param name="owner">The ASP.NET server control that the control collection is created for. </param>
	/// <exception cref="T:System.ArgumentNullException">Occurs if the <paramref name="owner" /> parameter is <see langword="null" />. </exception>
	public ControlCollection(Control owner)
	{
		if (owner == null)
		{
			throw new ArgumentException("owner");
		}
		this.owner = owner;
	}

	private void EnsureControls()
	{
		if (controls == null)
		{
			controls = new Control[5];
		}
		else if (controls.Length < count + 1)
		{
			int num = ((controls.Length == 5) ? 3 : 2);
			Control[] destinationArray = new Control[controls.Length * num];
			Array.Copy(controls, 0, destinationArray, 0, controls.Length);
			controls = destinationArray;
		}
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
	/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">Thrown if the <paramref name="child" /> parameter does not specify a control. </exception>
	/// <exception cref="T:System.Web.HttpException">Thrown if the <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
	public virtual void Add(Control child)
	{
		if (child == null)
		{
			throw new ArgumentNullException("child");
		}
		if (readOnly)
		{
			throw new HttpException(Locale.GetText("Collection is read-only."));
		}
		if (owner == child)
		{
			throw new HttpException(Locale.GetText("Cannot add collection's owner."));
		}
		EnsureControls();
		version++;
		controls[count++] = child;
		owner.AddedControl(child, count - 1);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection at the specified index location.</summary>
	/// <param name="index">The location in the array at which to add the child control. </param>
	/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter does not specify a control. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the <see cref="P:System.Web.UI.ControlCollection.Count" /> property. </exception>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
	public virtual void AddAt(int index, Control child)
	{
		if (child == null)
		{
			throw new ArgumentNullException();
		}
		if (index < -1 || index > count)
		{
			throw new ArgumentOutOfRangeException();
		}
		if (readOnly)
		{
			throw new HttpException(Locale.GetText("Collection is read-only."));
		}
		if (owner == child)
		{
			throw new HttpException(Locale.GetText("Cannot add collection's owner."));
		}
		if (index == -1)
		{
			Add(child);
			return;
		}
		EnsureControls();
		version++;
		Array.Copy(controls, index, controls, index + 1, count - index);
		count++;
		controls[index] = child;
		owner.AddedControl(child, index);
	}

	/// <summary>Removes all controls from the current server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	public virtual void Clear()
	{
		if (controls != null)
		{
			version++;
			for (int i = 0; i < count; i++)
			{
				owner.RemovedControl(controls[i]);
			}
			count = 0;
			if (owner != null)
			{
				owner.ResetChildNames();
			}
		}
	}

	/// <summary>Determines whether the specified server control is in the parent server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <param name="c">The server control to search for in the collection. </param>
	/// <returns>
	///     <see langword="true" /> if the specified server control exists in the collection; otherwise, <see langword="false" />.</returns>
	public virtual bool Contains(Control c)
	{
		if (controls != null)
		{
			return Array.IndexOf(controls, c) != -1;
		}
		return false;
	}

	/// <summary>Copies the child controls stored in the <see cref="T:System.Web.UI.ControlCollection" /> object to an <see cref="T:System.Array" /> object, beginning at the specified index location in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> to copy the child controls to. </param>
	/// <param name="index">The zero-based relative index in <paramref name="array" /> where copying begins. </param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="array" /> is not <see langword="null" /> and not one-dimensional. </exception>
	public virtual void CopyTo(Array array, int index)
	{
		if (controls != null)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index + count > array.GetLowerBound(0) + array.GetLength(0))
			{
				throw new ArgumentException();
			}
			if (array.Rank > 1)
			{
				throw new RankException(Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Locale.GetText("Value has to be >= 0."));
			}
			for (int i = 0; i < count; i++)
			{
				array.SetValue(controls[i], i + index);
			}
		}
	}

	/// <summary>Retrieves an enumerator that can iterate through the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <returns>The enumerator to iterate through the collection.</returns>
	public virtual IEnumerator GetEnumerator()
	{
		return new SimpleEnumerator(this);
	}

	/// <summary>Retrieves the index of a specified <see cref="T:System.Web.UI.Control" /> object in the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.Control" /> for which the index is returned. </param>
	/// <returns>The index of the specified server control. If the server control is not currently a member of the collection, it returns -1.</returns>
	public virtual int IndexOf(Control value)
	{
		if (controls == null || value == null)
		{
			return -1;
		}
		return Array.IndexOf(controls, value);
	}

	/// <summary>Removes the specified server control from the parent server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <param name="value">The server control to be removed. </param>
	public virtual void Remove(Control value)
	{
		int num = IndexOf(value);
		if (num != -1)
		{
			RemoveAt(num);
		}
	}

	/// <summary>Removes a child control, at the specified index location, from the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <param name="index">The ordinal index of the server control to be removed from the collection. </param>
	/// <exception cref="T:System.Web.HttpException">Thrown if the <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
	public virtual void RemoveAt(int index)
	{
		if (readOnly)
		{
			throw new HttpException();
		}
		version++;
		Control control = controls[index];
		count--;
		if (count - index > 0)
		{
			Array.Copy(controls, index + 1, controls, index, count - index);
		}
		controls[count] = null;
		owner.RemovedControl(control);
	}

	internal void SetReadonly(bool readOnly)
	{
		this.readOnly = readOnly;
	}
}
