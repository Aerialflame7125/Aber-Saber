namespace System.Web.UI;

/// <summary>Defines an attribute that is used by ASP.NET server controls to indicate at design time whether nested content that is contained within a server control corresponds to controls or to properties of the server control. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class PersistChildrenAttribute : Attribute
{
	/// <summary>Indicates that nested content should persist as controls at design time. The <see cref="F:System.Web.UI.PersistChildrenAttribute.Yes" /> field is read-only.</summary>
	public static readonly PersistChildrenAttribute Yes = new PersistChildrenAttribute(persist: true);

	/// <summary>Indicates that nested content should not persist as nested controls at design time. This field is read-only.</summary>
	public static readonly PersistChildrenAttribute No = new PersistChildrenAttribute(persist: false);

	/// <summary>Indicates the default attribute state. The <see cref="F:System.Web.UI.PersistChildrenAttribute.Default" /> field is read-only.</summary>
	public static readonly PersistChildrenAttribute Default = Yes;

	private bool _persist;

	private bool _usesCustomPersistence;

	/// <summary>Gets a value that indicates whether the nested content is persisted as nested controls at design time.</summary>
	/// <returns>
	///     <see langword="true" /> to persist nested content as nested controls; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool Persist => _persist;

	/// <summary>Gets a value indicating whether the server control provides custom persistence of nested controls at design time. </summary>
	/// <returns>
	///     <see langword="true" /> to provide custom persistence of nested content; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool UsesCustomPersistence
	{
		get
		{
			if (!_persist)
			{
				return _usesCustomPersistence;
			}
			return false;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class using a Boolean value indicating whether to persist nested content as nested controls. </summary>
	/// <param name="persist">
	///       <see langword="true" /> to persist the nested content as nested controls; otherwise, <see langword="false" />. </param>
	public PersistChildrenAttribute(bool persist)
	{
		_persist = persist;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class using two Boolean values. One indicating whether to persist nested content as nested controls and the other indicating whether to use a custom persistence method.</summary>
	/// <param name="persist">
	///       <see langword="true" /> to persist nested content as nested controls; otherwise, <see langword="false" />.</param>
	/// <param name="usesCustomPersistence">
	///       <see langword="true" /> to use customized persistence; otherwise, <see langword="false" />.</param>
	public PersistChildrenAttribute(bool persist, bool usesCustomPersistence)
		: this(persist)
	{
		_usesCustomPersistence = usesCustomPersistence;
	}

	/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class.</summary>
	/// <returns>A hash code for the <see cref="T:System.Web.UI.PersistChildrenAttribute" />.</returns>
	public override int GetHashCode()
	{
		return Persist.GetHashCode();
	}

	/// <summary>Determines whether the specified object is equal to the current object.</summary>
	/// <param name="obj">The object to compare with the current object.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj == this)
		{
			return true;
		}
		if (obj != null && obj is PersistChildrenAttribute)
		{
			return ((PersistChildrenAttribute)obj).Persist == _persist;
		}
		return false;
	}

	/// <summary>Returns a value indicating whether the value of the current instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> class is the default value of the derived clss.</summary>
	/// <returns>
	///     <see langword="true" /> if the value of the current instance of the <see cref="T:System.Web.UI.PersistChildrenAttribute" /> is the default instance; otherwise, <see langword="false" />. </returns>
	public override bool IsDefaultAttribute()
	{
		return Equals(Default);
	}
}
