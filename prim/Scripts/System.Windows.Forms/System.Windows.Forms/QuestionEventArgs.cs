namespace System.Windows.Forms;

/// <summary>Provides data for events that need a true or false answer to a question.</summary>
/// <filterpriority>2</filterpriority>
public class QuestionEventArgs : EventArgs
{
	private bool response;

	/// <summary>Gets or sets a value indicating the response to a question represented by the event.</summary>
	/// <returns>true for an affirmative response; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	public bool Response
	{
		get
		{
			return response;
		}
		set
		{
			response = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QuestionEventArgs" /> class using a default <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property value of false.</summary>
	public QuestionEventArgs()
	{
		response = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QuestionEventArgs" /> class using the specified default value for the <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property.</summary>
	/// <param name="response">The default value of the <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property.</param>
	public QuestionEventArgs(bool response)
	{
		this.response = response;
	}
}
