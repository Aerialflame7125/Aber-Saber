using System.Configuration;

namespace System.Web.Configuration;

internal static class PropertyHelper
{
	internal static WhiteSpaceTrimStringConverter WhiteSpaceTrimStringConverter = new WhiteSpaceTrimStringConverter();

	internal static InfiniteTimeSpanConverter InfiniteTimeSpanConverter = new InfiniteTimeSpanConverter();

	internal static InfiniteIntConverter InfiniteIntConverter = new InfiniteIntConverter();

	internal static TimeSpanMinutesConverter TimeSpanMinutesConverter = new TimeSpanMinutesConverter();

	internal static TimeSpanSecondsOrInfiniteConverter TimeSpanSecondsOrInfiniteConverter = new TimeSpanSecondsOrInfiniteConverter();

	internal static TimeSpanSecondsConverter TimeSpanSecondsConverter = new TimeSpanSecondsConverter();

	internal static CommaDelimitedStringCollectionConverter CommaDelimitedStringCollectionConverter = new CommaDelimitedStringCollectionConverter();

	internal static DefaultValidator DefaultValidator = new DefaultValidator();

	internal static NullableStringValidator NonEmptyStringValidator = new NullableStringValidator(1);

	internal static PositiveTimeSpanValidator PositiveTimeSpanValidator = new PositiveTimeSpanValidator();

	internal static TimeSpanMinutesOrInfiniteConverter TimeSpanMinutesOrInfiniteConverter = new TimeSpanMinutesOrInfiniteConverter();

	internal static IntegerValidator IntFromZeroToMaxValidator = new IntegerValidator(0, int.MaxValue);

	internal static IntegerValidator IntFromOneToMax_1Validator = new IntegerValidator(1, 2147483646);

	internal static VersionConverter VersionConverter = new VersionConverter();
}
