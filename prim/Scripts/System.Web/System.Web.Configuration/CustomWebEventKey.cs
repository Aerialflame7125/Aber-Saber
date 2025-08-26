namespace System.Web.Configuration;

internal class CustomWebEventKey
{
	internal Type _type;

	internal int _eventCode;

	internal CustomWebEventKey(Type eventType, int eventCode)
	{
		_type = eventType;
		_eventCode = eventCode;
	}
}
