using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Represents an item that is saved in the <see cref="T:System.Web.UI.StateBag" /> class when view state information is persisted between Web requests. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class StateItem
{
	private bool _isDirty;

	private object _value;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.StateItem" /> object has been modified.</summary>
	/// <returns>
	///     <see langword="true" /> if the stored <see cref="T:System.Web.UI.StateItem" /> object has been modified; otherwise, <see langword="false" />.</returns>
	public bool IsDirty
	{
		get
		{
			return _isDirty;
		}
		set
		{
			_isDirty = value;
		}
	}

	/// <summary>Gets or sets the value of the <see cref="T:System.Web.UI.StateItem" /> object that is stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	/// <returns>The value of the <see cref="T:System.Web.UI.StateItem" /> stored in the <see cref="T:System.Web.UI.StateBag" />.</returns>
	public object Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
		}
	}

	private StateItem()
	{
	}

	internal StateItem(object value)
	{
		_value = value;
	}
}
