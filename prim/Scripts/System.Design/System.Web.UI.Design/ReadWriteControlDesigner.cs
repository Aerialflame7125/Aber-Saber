using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Extends design-time behavior for read/write server controls.</summary>
[Obsolete("Use ContainerControlDesigner instead")]
[System.MonoTODO]
public class ReadWriteControlDesigner : ControlDesigner
{
	/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.Design.ReadWriteControlDesigner" /> class.</summary>
	[System.MonoTODO]
	public ReadWriteControlDesigner()
	{
		throw new NotImplementedException();
	}

	/// <summary>Maps a property, including description and value, to an intrinsic HTML style.</summary>
	/// <param name="propName">The name of the property to map.</param>
	/// <param name="varPropValue">The value of the property.</param>
	[System.MonoTODO]
	protected virtual void MapPropertyToStyle(string propName, object varPropValue)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides notification that is raised when a behavior is attached to the designer.</summary>
	[Obsolete("Use ControlDesigner.Tag instead")]
	[System.MonoTODO]
	protected override void OnBehaviorAttached()
	{
		throw new NotImplementedException();
	}

	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event of the <see cref="T:System.ComponentModel.Design.IComponentChangeService" /> class.</summary>
	/// <param name="sender">The object sending the event.</param>
	/// <param name="ce">The <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> object that provides data for the event.</param>
	[System.MonoTODO]
	public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
	/// <returns>The HTML that is used to represent the control at design time.</returns>
	[System.MonoTODO]
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Refreshes the display of the control.</summary>
	[System.MonoTODO]
	public override void UpdateDesignTimeHtml()
	{
		throw new NotImplementedException();
	}
}
