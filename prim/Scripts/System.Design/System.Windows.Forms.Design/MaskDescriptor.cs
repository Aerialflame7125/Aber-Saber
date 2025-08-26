using System.Globalization;

namespace System.Windows.Forms.Design;

/// <summary>Defines a set of members for derived classes to provide options for the masked text box UI type editor.</summary>
public abstract class MaskDescriptor
{
	/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> representing the locale the mask is authored for.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the locale the mask is authored for.</returns>
	[System.MonoTODO]
	public virtual CultureInfo Culture
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the mask being defined.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the mask being defined.</returns>
	public abstract string Mask { get; }

	/// <summary>Gets the user-friendly name of the mask.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the name or brief description of the <see cref="P:System.Windows.Forms.Design.MaskDescriptor.Mask" />.</returns>
	public abstract string Name { get; }

	/// <summary>Gets a sample of a formatted string for the mask.</summary>
	/// <returns>A <see cref="T:System.String" /> containing text that is formatted by using the <see cref="P:System.Windows.Forms.Design.MaskDescriptor.Mask" />.</returns>
	public abstract string Sample { get; }

	/// <summary>Gets the type providing validation associated with the mask.</summary>
	/// <returns>The <see cref="T:System.Type" /> that the formatted string is validated against.</returns>
	public abstract Type ValidatingType { get; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.MaskDescriptor" /> class.</summary>
	protected MaskDescriptor()
	{
	}

	/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.Design.MaskDescriptor" /> is equal to the current <see cref="T:System.Windows.Forms.Design.MaskDescriptor" />.</summary>
	/// <param name="maskDescriptor">The <see cref="T:System.Windows.Forms.Design.MaskDescriptor" /> to compare with the current <see cref="T:System.Windows.Forms.Design.MaskDescriptor" />.</param>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.Windows.Forms.Design.MaskDescriptor" /> is equal to the current <see cref="T:System.Windows.Forms.Design.MaskDescriptor" />; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool Equals(object maskDescriptor)
	{
		return base.Equals(maskDescriptor);
	}

	/// <summary>Serves as a hash function for a particular type.</summary>
	/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
	[System.MonoTODO]
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Returns a value indicating whether the specified mask descriptor is valid and can be added to the masks list.</summary>
	/// <param name="maskDescriptor">The mask descriptor to test for validity.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="maskDescriptor" /> is valid; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public static bool IsValidMaskDescriptor(MaskDescriptor maskDescriptor)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a value indicating whether the specified mask descriptor is valid, and provides an error description if it is not valid.</summary>
	/// <param name="maskDescriptor">The mask descriptor to test for validity.</param>
	/// <param name="validationErrorDescription">A string representing a validation error. If no validation error occurred, the <paramref name="validationErrorDescription" /> is <see cref="F:System.String.Empty" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="maskDescriptor" /> is valid; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public static bool IsValidMaskDescriptor(MaskDescriptor maskDescriptor, out string validationErrorDescription)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a string that represents the current object.</summary>
	/// <returns>A string that represents the current object.</returns>
	[System.MonoTODO]
	public override string ToString()
	{
		return base.ToString();
	}
}
