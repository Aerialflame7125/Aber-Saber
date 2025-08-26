namespace System.Runtime.CompilerServices;

/// <summary>Provides a static method to create a <see cref="T:System.FormattableString" /> object from a composite format string and its arguments.</summary>
public static class FormattableStringFactory
{
	private sealed class ConcreteFormattableString : FormattableString
	{
		private readonly string _format;

		private readonly object[] _arguments;

		public override string Format => _format;

		public override int ArgumentCount => _arguments.Length;

		internal ConcreteFormattableString(string format, object[] arguments)
		{
			_format = format;
			_arguments = arguments;
		}

		public override object[] GetArguments()
		{
			return _arguments;
		}

		public override object GetArgument(int index)
		{
			return _arguments[index];
		}

		public override string ToString(IFormatProvider formatProvider)
		{
			return string.Format(formatProvider, _format, _arguments);
		}
	}

	/// <summary>Creates a <see cref="T:System.FormattableString" /> instance from a composite format string and its arguments.</summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="arguments">The arguments whose string representations are to be inserted in the result string.</param>
	/// <returns>The object that represents the composite format string and its arguments.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="format" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="arguments" /> is <see langword="null" />.</exception>
	public static FormattableString Create(string format, params object[] arguments)
	{
		if (format == null)
		{
			throw new ArgumentNullException("format");
		}
		if (arguments == null)
		{
			throw new ArgumentNullException("arguments");
		}
		return new ConcreteFormattableString(format, arguments);
	}
}
