using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides a type converter that retrieves a list of property names for the current control.</summary>
public class ControlPropertyNameConverter : StringConverter
{
	/// <summary>Returns a collection of property names for the control within a designer that implements <see cref="T:System.ComponentModel.Design.IDesignerHost" /> when provided with a format context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains a set of strings representing property names for the current control. If the current control is <see langword="null" />, an empty collection is returned. If the <paramref name="context" /> parameter is <see langword="null" />, <see langword="null" /> is returned.</returns>
	[MonoLimitation("This implementation always returns null")]
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		return null;
	}

	/// <summary>Returns a value that indicates whether this object supports a standard set of values that can be chosen from a list, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="context" /> parameter is not <see langword="null" />; otherwise, <see langword="false" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		if (context != null)
		{
			return true;
		}
		return false;
	}

	/// <summary>Returns a value that indicates whether the collection of standard values returned by the <see cref="Overload:System.Web.UI.WebControls.ControlPropertyNameConverter.GetStandardValues" /> method is an exclusive list of possible values, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
	/// <returns>
	///     <see langword="false" /> in all cases, which indicates that the list is not exclusive.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ControlPropertyNameConverter" /> class.</summary>
	public ControlPropertyNameConverter()
	{
	}
}
