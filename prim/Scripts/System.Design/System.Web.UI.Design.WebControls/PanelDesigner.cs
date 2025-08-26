namespace System.Web.UI.Design.WebControls;

/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
public class PanelDesigner : ReadWriteControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.PanelDesigner" /> class.</summary>
	public PanelDesigner()
	{
	}

	/// <summary>Maps a specified property and value to a specified markup style.</summary>
	/// <param name="propName">A string containing the property name.</param>
	/// <param name="varPropValue">An object that is the property value.</param>
	protected override void MapPropertyToStyle(string propName, object varPropValue)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides notification when a behavior is attached to the designer.</summary>
	protected override void OnBehaviorAttached()
	{
		throw new NotImplementedException();
	}
}
