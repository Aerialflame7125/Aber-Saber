using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides a type converter that retrieves a list of control IDs in the current container.</summary>
public class ControlIDConverter : StringConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ControlIDConverter" /> class.</summary>
	public ControlIDConverter()
	{
	}

	/// <summary>Returns a value indicating whether the control ID of the specified control is added to the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that is returned by the <see cref="M:System.Web.UI.WebControls.ControlIDConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
	/// <param name="control">The control instance to test for inclusion in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	protected virtual bool FilterControl(Control control)
	{
		return true;
	}

	/// <summary>Returns a collection of control IDs from the container within the <see cref="T:System.ComponentModel.Design.IDesignerHost" /> when provided with a format context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />. </param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a set of strings representing the control IDs of the controls in the current container. If no controls are currently contained, an empty collection is returned. If the context is <see langword="null" /> or there is no current container, then <see langword="null" /> is returned.</returns>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		if (context == null)
		{
			return null;
		}
		IContainer container = context.Container;
		if (container == null)
		{
			return null;
		}
		ComponentCollection components = container.Components;
		ArrayList arrayList = new ArrayList(0);
		foreach (Control item in components)
		{
			if (FilterControl(item))
			{
				arrayList.Add(item.ID);
			}
		}
		return new StandardValuesCollection(arrayList);
	}

	/// <summary>Returns a value indicating whether the collection of standard values returned by the <see cref="M:System.Web.UI.WebControls.ControlIDConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list of possible values, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Returns a value indicating whether this object supports a standard set of control ID values that can be picked from a list, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <returns>
	///     <see langword="true" /> if <see cref="M:System.Web.UI.WebControls.ControlIDConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> should be called to find a common set of control ID values the object supports; otherwise, <see langword="false" />. This implementation returns <see langword="true" /> if the context is not <see langword="null" />; otherwise, <see langword="false" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		if (context == null)
		{
			return false;
		}
		return true;
	}
}
