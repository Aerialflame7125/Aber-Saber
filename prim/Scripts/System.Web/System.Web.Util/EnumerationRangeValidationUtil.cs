using System.Web.UI.WebControls;

namespace System.Web.Util;

internal static class EnumerationRangeValidationUtil
{
	public static void ValidateRepeatLayout(RepeatLayout value)
	{
		if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
		{
			throw new ArgumentOutOfRangeException("value");
		}
	}
}
