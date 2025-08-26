using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Converts a control on the Web Forms page that can be validated with a validation control to a string containing the control's ID.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ValidatedControlConverter : ControlIDConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ValidatedControlConverter" /> class. </summary>
	public ValidatedControlConverter()
	{
	}

	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		if (context != null && context.Container != null && context.Container.Components != null)
		{
			ArrayList arrayList = new ArrayList();
			ComponentCollection components = context.Container.Components;
			int count = components.Count;
			for (int i = 0; i < count; i++)
			{
				if (FilterControl((Control)components[i]))
				{
					string iD = ((Control)components[i]).ID;
					if (iD != null && iD.Length > 0)
					{
						arrayList.Add(iD);
					}
				}
			}
			arrayList.Sort();
			if (arrayList.Count > 0)
			{
				return new StandardValuesCollection(arrayList);
			}
			return null;
		}
		return base.GetStandardValues(context);
	}

	/// <summary>Returns a value indicating whether the specified control should be added to the list of controls that can be validated.</summary>
	/// <param name="control">The control to check. </param>
	/// <returns>
	///     <see langword="true" /> if the control should be added to the list of controls that can be validated; otherwise, <see langword="false" />.</returns>
	protected override bool FilterControl(Control control)
	{
		return BaseValidator.GetValidationProperty(control) != null;
	}
}
