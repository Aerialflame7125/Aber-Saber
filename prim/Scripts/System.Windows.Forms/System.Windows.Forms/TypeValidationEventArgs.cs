namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MaskedTextBox.TypeValidationCompleted" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class TypeValidationEventArgs : EventArgs
{
	private bool cancel;

	private bool is_valid_input;

	private string message;

	private object return_value;

	private Type validating_type;

	/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
	/// <returns>true if the event should be canceled and focus retained by the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control; otherwise, false to continue validation processing.</returns>
	public bool Cancel
	{
		get
		{
			return cancel;
		}
		set
		{
			cancel = value;
		}
	}

	/// <summary>Gets a value indicating whether the formatted input string was successfully converted to the validating type.</summary>
	/// <returns>true if the formatted input string can be converted into the type specified by the <see cref="P:System.Windows.Forms.TypeValidationEventArgs.ValidatingType" /> property; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	public bool IsValidInput => is_valid_input;

	/// <summary>Gets a text message describing the conversion process.</summary>
	/// <returns>A <see cref="T:System.String" /> containing a description of the conversion process.</returns>
	/// <filterpriority>1</filterpriority>
	public string Message => message;

	/// <summary>Gets the object that results from the conversion of the formatted input string.</summary>
	/// <returns>If the validation is successful, an <see cref="T:System.Object" /> that represents the converted type; otherwise, null. </returns>
	/// <filterpriority>1</filterpriority>
	public object ReturnValue => return_value;

	/// <summary>Gets the type that the formatted input string is being validated against.</summary>
	/// <returns>The target <see cref="T:System.Type" /> of the conversion process. This should never be null.</returns>
	/// <filterpriority>1</filterpriority>
	public Type ValidatingType => validating_type;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TypeValidationEventArgs" /> class.</summary>
	/// <param name="validatingType">The <see cref="T:System.Type" /> that the formatted input string was being validated against. </param>
	/// <param name="isValidInput">A <see cref="T:System.Boolean" /> value indicating whether the formatted string was successfully converted to the validating type. </param>
	/// <param name="returnValue">An <see cref="T:System.Object" /> that is the result of the formatted string being converted to the target type. </param>
	/// <param name="message">A <see cref="T:System.String" /> containing a description of the conversion process. </param>
	public TypeValidationEventArgs(Type validatingType, bool isValidInput, object returnValue, string message)
	{
		is_valid_input = isValidInput;
		this.message = message;
		return_value = returnValue;
		validating_type = validatingType;
		cancel = false;
	}
}
