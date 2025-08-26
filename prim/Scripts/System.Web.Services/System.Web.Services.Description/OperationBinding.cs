using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides specifications for protocols and data formats for the messages used in the action supported by the XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class OperationBinding : NamedItem
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	private FaultBindingCollection faults;

	private InputBinding input;

	private OutputBinding output;

	private Binding parent;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.Binding" /> of which the current <see cref="T:System.Web.Services.Description.OperationBinding" /> is a member.</summary>
	/// <returns>A binding of which the current <see cref="T:System.Web.Services.Description.OperationBinding" /> is a member.</returns>
	public Binding Binding => parent;

	/// <summary>Gets the collection of extensibility elements specific to the current <see cref="T:System.Web.Services.Description.OperationBinding" />.</summary>
	/// <returns>A collection of extensibility elements.</returns>
	[XmlIgnore]
	public override ServiceDescriptionFormatExtensionCollection Extensions
	{
		get
		{
			if (extensions == null)
			{
				extensions = new ServiceDescriptionFormatExtensionCollection(this);
			}
			return extensions;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.InputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</summary>
	/// <returns>An <see cref="T:System.Web.Services.Description.InputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</returns>
	[XmlElement("input")]
	public InputBinding Input
	{
		get
		{
			return input;
		}
		set
		{
			if (input != null)
			{
				input.SetParent(null);
			}
			input = value;
			if (input != null)
			{
				input.SetParent(this);
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.OutputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</summary>
	/// <returns>An <see cref="T:System.Web.Services.Description.OutputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</returns>
	[XmlElement("output")]
	public OutputBinding Output
	{
		get
		{
			return output;
		}
		set
		{
			if (output != null)
			{
				output.SetParent(null);
			}
			output = value;
			if (output != null)
			{
				output.SetParent(this);
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.FaultBindingCollection" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" /> instance.</summary>
	/// <returns>A fault binding collection.</returns>
	[XmlElement("fault")]
	public FaultBindingCollection Faults
	{
		get
		{
			if (faults == null)
			{
				faults = new FaultBindingCollection(this);
			}
			return faults;
		}
	}

	internal void SetParent(Binding parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.OperationBinding" /> class.</summary>
	public OperationBinding()
	{
	}
}
