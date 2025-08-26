namespace System.Web.Profile;

/// <summary>Provides untyped access to grouped ASP.NET profile property values.</summary>
public class ProfileGroupBase
{
	private string _MyName;

	private ProfileBase _Parent;

	/// <summary>Gets or sets a grouped profile property value indexed by the property name.</summary>
	/// <param name="propertyName">The name of the grouped profile property.</param>
	/// <returns>The value of the specified grouped profile property.</returns>
	public object this[string propertyName]
	{
		get
		{
			return _Parent[_MyName + propertyName];
		}
		set
		{
			_Parent[_MyName + propertyName] = value;
		}
	}

	/// <summary>Gets the value of a grouped profile property.</summary>
	/// <param name="propertyName">The name of the grouped profile property.</param>
	/// <returns>The value of the grouped profile property typed as <see langword="object" />.</returns>
	public object GetPropertyValue(string propertyName)
	{
		return _Parent[_MyName + propertyName];
	}

	/// <summary>Sets the value of a grouped profile property.</summary>
	/// <param name="propertyName">The name of the grouped property to set.</param>
	/// <param name="propertyValue">The value to assign to the grouped property.</param>
	public void SetPropertyValue(string propertyName, object propertyValue)
	{
		_Parent[_MyName + propertyName] = propertyValue;
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileGroupBase" /> class.</summary>
	public ProfileGroupBase()
	{
		_Parent = null;
		_MyName = null;
	}

	/// <summary>Used by ASP.NET to initialize the grouped profile property values and information.</summary>
	/// <param name="parent">The class that inherits <see cref="T:System.Web.Profile.ProfileBase" /> that is assigned to the <see cref="P:System.Web.HttpContext.Profile" /> property.</param>
	/// <param name="myName">The name of the profile property group.</param>
	public void Init(ProfileBase parent, string myName)
	{
		if (_Parent == null)
		{
			_Parent = parent;
			_MyName = myName + ".";
		}
	}
}
