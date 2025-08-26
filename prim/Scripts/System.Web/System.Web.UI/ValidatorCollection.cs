using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Exposes an array of <see cref="T:System.Web.UI.IValidator" /> references. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ValidatorCollection : ICollection, IEnumerable
{
	private ArrayList _validators;

	/// <summary>Gets the number of references in the collection.</summary>
	/// <returns>The number of validation controls in the page's <see cref="T:System.Web.UI.ValidatorCollection" />.</returns>
	public int Count => _validators.Count;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.ValidatorCollection" /> collection is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
	public bool IsReadOnly => _validators.IsReadOnly;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.ValidatorCollection" /> collection is synchronized.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection is synchronized; otherwise, <see langword="false" />.</returns>
	public bool IsSynchronized => _validators.IsSynchronized;

	/// <summary>Gets the validation server control at the specified index location in the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
	/// <param name="index">The index of the validator to return. </param>
	/// <returns>The value of the specified validator.</returns>
	public IValidator this[int index] => (IValidator)_validators[index];

	/// <summary>Gets an object that can be used to synchronize the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
	/// <returns>The <see cref="T:System.Object" /> to synchronize the collection with.</returns>
	public object SyncRoot => this;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ValidatorCollection" /> class.</summary>
	public ValidatorCollection()
	{
		_validators = new ArrayList();
	}

	/// <summary>Adds the specified validation server control to the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
	/// <param name="validator">The validation server control to add. </param>
	public void Add(IValidator validator)
	{
		_validators.Add(validator);
	}

	/// <summary>Determines whether the specified validation server control is contained within the page's <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
	/// <param name="validator">The validation server control to check for. </param>
	/// <returns>
	///     <see langword="true" /> if the validation server control is in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(IValidator validator)
	{
		return _validators.Contains(validator);
	}

	/// <summary>Copies the validator collection to the specified array, beginning at the specified location.</summary>
	/// <param name="array">The collection to which the validation server control is added. </param>
	/// <param name="index">The index where the validation server control is copied. </param>
	public void CopyTo(Array array, int index)
	{
		_validators.CopyTo(array, index);
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> instance for the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
	/// <returns>The <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
	public IEnumerator GetEnumerator()
	{
		return _validators.GetEnumerator();
	}

	/// <summary>Removes the specified validation server control from the page's <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
	/// <param name="validator">The validation server control to remove from the collection. </param>
	public void Remove(IValidator validator)
	{
		_validators.Remove(validator);
	}
}
