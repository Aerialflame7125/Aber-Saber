namespace System.Windows.Forms;

/// <summary>Defines how to format the text inside of a <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
public enum MaskFormat
{
	/// <summary>Return only text input by the user. </summary>
	ExcludePromptAndLiterals,
	/// <summary>Return text input by the user as well as any instances of the prompt character.</summary>
	IncludePrompt,
	/// <summary>Return text input by the user as well as any literal characters defined in the mask.</summary>
	IncludeLiterals,
	/// <summary>Return text input by the user as well as any literal characters defined in the mask and any instances of the prompt character. </summary>
	IncludePromptAndLiterals
}
