namespace System.Web.UI.Design;

/// <summary>Represents the kind of event that has occurred on a view of a control at design time. This class cannot be inherited.</summary>
public sealed class ViewEvent
{
	/// <summary>Indicates that a view event was raised for a click on a designer region.</summary>
	public static readonly ViewEvent Click = new ViewEvent();

	/// <summary>Indicates that a view event was raised for drawing a control on the design surface.</summary>
	public static readonly ViewEvent Paint = new ViewEvent();

	/// <summary>Indicates that a view event was raised for changing the template mode of a control designer.</summary>
	public static readonly ViewEvent TemplateModeChanged = new ViewEvent();

	private ViewEvent()
	{
	}
}
