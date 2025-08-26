namespace System.Web.Util;

internal static class ExceptionUtil
{
	internal static ArgumentException ParameterInvalid(string parameter)
	{
		return new ArgumentException(global::SR.GetString("The parameter '{0}' is invalid.", parameter), parameter);
	}

	internal static ArgumentException ParameterNullOrEmpty(string parameter)
	{
		return new ArgumentException(global::SR.GetString("The string parameter '{0}' cannot be null or empty.", parameter), parameter);
	}

	internal static ArgumentException PropertyInvalid(string property)
	{
		return new ArgumentException(global::SR.GetString("The value assigned to property '{0}' is invalid.", property), property);
	}

	internal static ArgumentException PropertyNullOrEmpty(string property)
	{
		return new ArgumentException(global::SR.GetString("The value assigned to property '{0}' cannot be null or empty.", property), property);
	}

	internal static InvalidOperationException UnexpectedError(string methodName)
	{
		return new InvalidOperationException(global::SR.GetString("An unexpected error occurred in '{0}'.", methodName));
	}
}
