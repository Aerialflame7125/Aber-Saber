namespace System.Web.UI.WebControls;

internal interface IDataControlButton : IButtonControl
{
	Control Container { get; set; }

	string ImageUrl { get; set; }

	bool AllowCallback { get; set; }

	ButtonType ButtonType { get; }
}
