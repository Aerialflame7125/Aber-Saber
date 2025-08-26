namespace System.Web.UI;

internal class TemplateBinding
{
	public Type ControlType;

	public string ControlProperty;

	public string ControlId;

	public string FieldName;

	public TemplateBinding(Type controlType, string controlProperty, string controlId, string fieldName)
	{
		ControlType = controlType;
		ControlProperty = controlProperty;
		ControlId = controlId;
		FieldName = fieldName;
	}
}
