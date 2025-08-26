using System.ComponentModel;
using System.Text;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides an abstract definition of an action supported by the XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class Operation : NamedItem
{
	private string[] parameters;

	private OperationMessageCollection messages;

	private OperationFaultCollection faults;

	private PortType parent;

	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Operation" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Operation" />.</returns>
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

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.PortType" /> of which the <see cref="T:System.Web.Services.Description.Operation" /> is a member.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.PortType" /> object.</returns>
	public PortType PortType => parent;

	/// <summary>Gets or sets an optional Remote Procedure Call (RPC) signature that orders specification for request-response or solicit-response operations.</summary>
	/// <returns>A list of names of the <see cref="T:System.Web.Services.Description.MessagePart" /> instances separated by a single space.</returns>
	[XmlAttribute("parameterOrder")]
	[DefaultValue("")]
	public string ParameterOrderString
	{
		get
		{
			if (parameters == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(parameters[i]);
			}
			return stringBuilder.ToString();
		}
		set
		{
			if (value == null)
			{
				parameters = null;
				return;
			}
			parameters = value.Split(' ');
		}
	}

	/// <summary>Gets or sets an array of the elements contained in the <see cref="P:System.Web.Services.Description.Operation.ParameterOrderString" />.</summary>
	/// <returns>An array of names of <see cref="T:System.Web.Services.Description.MessagePart" /> instances.</returns>
	[XmlIgnore]
	public string[] ParameterOrder
	{
		get
		{
			return parameters;
		}
		set
		{
			parameters = value;
		}
	}

	/// <summary>Gets the collection of instances of the <see cref="T:System.Web.Services.Description.Message" /> class defined by the current <see cref="T:System.Web.Services.Description.Operation" />.</summary>
	/// <returns>The collection of instances of the <see cref="T:System.Web.Services.Description.Message" /> class defined by the current <see cref="T:System.Web.Services.Description.Operation" />.</returns>
	[XmlElement("input", typeof(OperationInput))]
	[XmlElement("output", typeof(OperationOutput))]
	public OperationMessageCollection Messages
	{
		get
		{
			if (messages == null)
			{
				messages = new OperationMessageCollection(this);
			}
			return messages;
		}
	}

	/// <summary>Gets the collection of faults, or error messages, defined by the current <see cref="T:System.Web.Services.Description.Operation" />.</summary>
	/// <returns>A collection of faults, or error messages, defined by the current operation.</returns>
	[XmlElement("fault")]
	public OperationFaultCollection Faults
	{
		get
		{
			if (faults == null)
			{
				faults = new OperationFaultCollection(this);
			}
			return faults;
		}
	}

	internal void SetParent(PortType parent)
	{
		this.parent = parent;
	}

	/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Web.Services.Description.OperationBinding" /> matches with the <see cref="T:System.Web.Services.Description.Operation" />.</summary>
	/// <param name="operationBinding">An <see cref="T:System.Web.Services.Description.OperationBinding" /> to be checked to determine whether it matches with the <see cref="T:System.Web.Services.Description.Operation" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Description.Operation" /> instance matches the <paramref name="operationBinding" /> parameter; otherwise, <see langword="false" />.</returns>
	public bool IsBoundBy(OperationBinding operationBinding)
	{
		if (operationBinding.Name != base.Name)
		{
			return false;
		}
		OperationMessage input = Messages.Input;
		if (input != null)
		{
			if (operationBinding.Input == null)
			{
				return false;
			}
			string messageName = GetMessageName(base.Name, input.Name, isInput: true);
			if (GetMessageName(operationBinding.Name, operationBinding.Input.Name, isInput: true) != messageName)
			{
				return false;
			}
		}
		else if (operationBinding.Input != null)
		{
			return false;
		}
		OperationMessage output = Messages.Output;
		if (output != null)
		{
			if (operationBinding.Output == null)
			{
				return false;
			}
			string messageName2 = GetMessageName(base.Name, output.Name, isInput: false);
			if (GetMessageName(operationBinding.Name, operationBinding.Output.Name, isInput: false) != messageName2)
			{
				return false;
			}
		}
		else if (operationBinding.Output != null)
		{
			return false;
		}
		return true;
	}

	private string GetMessageName(string operationName, string messageName, bool isInput)
	{
		if (messageName != null && messageName.Length > 0)
		{
			return messageName;
		}
		switch (Messages.Flow)
		{
		case OperationFlow.RequestResponse:
			if (isInput)
			{
				return operationName + "Request";
			}
			return operationName + "Response";
		case OperationFlow.OneWay:
			if (isInput)
			{
				return operationName;
			}
			return null;
		case OperationFlow.SolicitResponse:
			return null;
		case OperationFlow.Notification:
			return null;
		default:
			return null;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Operation" /> class.</summary>
	public Operation()
	{
	}
}
