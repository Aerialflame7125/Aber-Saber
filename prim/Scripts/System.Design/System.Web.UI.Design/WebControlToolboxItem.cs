using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace System.Web.UI.Design;

/// <summary>Provides a base class for a Web server control <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
[Serializable]
[System.MonoTODO]
public class WebControlToolboxItem : ToolboxItem
{
	private int persistChildren;

	private string toolData;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" /> class.</summary>
	public WebControlToolboxItem()
	{
		toolData = null;
		persistChildren = -1;
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" /> class using the provided type.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> of the tool for this toolbox item.</param>
	[System.MonoTODO]
	public WebControlToolboxItem(Type type)
	{
		toolData = null;
		persistChildren = -1;
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" /> class using the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
	/// <param name="info">A SerializationInfo object containing information needed to instantiate the Web control through deserialization.</param>
	/// <param name="context">A StreamingContext object.</param>
	protected WebControlToolboxItem(SerializationInfo info, StreamingContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates objects from each type contained in this <see cref="T:System.Drawing.Design.ToolboxItem" />, and adds them to the specified designer.</summary>
	/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
	/// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
	/// <exception cref="T:System.Exception">The <see cref="M:System.Web.UI.Design.WebControlToolboxItem.CreateComponentsCore(System.ComponentModel.Design.IDesignerHost)" /> method is only available in Windows Forms.</exception>
	[System.MonoTODO]
	protected override IComponent[] CreateComponentsCore(IDesignerHost host)
	{
		throw new NotImplementedException();
	}

	/// <summary>Saves the state of the toolbox item to the specified serialization information object.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> used to save the state of the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" />.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the serialization stream characteristics.</param>
	[System.MonoTODO]
	protected override void Serialize(SerializationInfo info, StreamingContext context)
	{
		base.Serialize(info, context);
		if (toolData != null)
		{
			info.AddValue("ToolData", toolData);
		}
		if (persistChildren != -1)
		{
			info.AddValue("PersistChildren", persistChildren);
		}
	}

	/// <summary>Loads the state of the toolbox item from the specified serialization information object.</summary>
	/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Web.UI.Design.WebControlToolboxItem" />.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the serialization stream characteristics.</param>
	[System.MonoTODO]
	protected override void Deserialize(SerializationInfo info, StreamingContext context)
	{
		base.Deserialize(info, context);
		toolData = info.GetString("ToolData");
		persistChildren = info.GetInt32("PersistChildren");
	}

	/// <summary>Initializes this toolbox item.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> of the Web server control toolbox item.</param>
	[System.MonoTODO]
	public override void Initialize(Type type)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the value of the specified type of attribute of the toolbox item.</summary>
	/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
	/// <param name="attributeType">The type of attribute to retrieve the value of.</param>
	/// <returns>The value of the specified type of attribute.</returns>
	/// <exception cref="T:System.ArgumentException">The <paramref name="attributeType" /> parameter is not a <see cref="T:System.Web.UI.PersistChildrenAttribute" />.</exception>
	[System.MonoTODO]
	public object GetToolAttributeValue(IDesignerHost host, Type attributeType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the HTML for the Web control that the tool creates.</summary>
	/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
	/// <returns>The HTML for the Web control that the tool creates.</returns>
	[System.MonoTODO]
	public string GetToolHtml(IDesignerHost host)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the type of this toolbox item.</summary>
	/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current design document.</param>
	/// <returns>The <see cref="T:System.Type" /> of this toolbox item.</returns>
	[System.MonoTODO]
	public Type GetToolType(IDesignerHost host)
	{
		throw new NotImplementedException();
	}
}
