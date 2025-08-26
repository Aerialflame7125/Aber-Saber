namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Binding.Format" /> and <see cref="E:System.Windows.Forms.Binding.Parse" /> events.</summary>
/// <filterpriority>2</filterpriority>
public class ConvertEventArgs : EventArgs
{
	private object object_value;

	private Type desired_type;

	/// <summary>Gets the data type of the desired value.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the desired value.</returns>
	/// <filterpriority>1</filterpriority>
	public Type DesiredType => desired_type;

	/// <summary>Gets or sets the value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</summary>
	/// <returns>The value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</returns>
	/// <filterpriority>1</filterpriority>
	public object Value
	{
		get
		{
			return object_value;
		}
		set
		{
			object_value = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ConvertEventArgs" /> class.</summary>
	/// <param name="value">An <see cref="T:System.Object" /> that contains the value of the current property. </param>
	/// <param name="desiredType">The <see cref="T:System.Type" /> of the value. </param>
	public ConvertEventArgs(object value, Type desiredType)
	{
		object_value = value;
		desired_type = desiredType;
	}
}
