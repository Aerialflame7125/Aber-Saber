using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Hosting;

[Serializable]
internal class HostingEnvironmentException : Exception
{
	private string _details;

	internal string Details
	{
		get
		{
			if (_details == null)
			{
				return string.Empty;
			}
			return _details;
		}
	}

	protected HostingEnvironmentException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		_details = info.GetString("_details");
	}

	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue("_details", _details);
	}

	internal HostingEnvironmentException(string message, string details)
		: base(message)
	{
		_details = details;
	}
}
