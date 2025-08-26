namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class UICuesEventArgs : EventArgs
{
	private UICues cues;

	/// <summary>Gets the bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values. The default is <see cref="F:System.Windows.Forms.UICues.Changed" />.</returns>
	/// <filterpriority>1</filterpriority>
	public UICues Changed => cues & UICues.Changed;

	/// <summary>Gets a value indicating whether the state of the focus cues has changed.</summary>
	/// <returns>true if the state of the focus cues has changed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool ChangeFocus
	{
		get
		{
			if ((cues & UICues.ChangeFocus) == 0)
			{
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets a value indicating whether the state of the keyboard cues has changed.</summary>
	/// <returns>true if the state of the keyboard cues has changed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool ChangeKeyboard
	{
		get
		{
			if ((cues & UICues.ChangeKeyboard) == 0)
			{
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets a value indicating whether focus rectangles are shown after the change.</summary>
	/// <returns>true if focus rectangles are shown after the change; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool ShowFocus
	{
		get
		{
			if ((cues & UICues.ShowFocus) == 0)
			{
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets a value indicating whether keyboard cues are underlined after the change.</summary>
	/// <returns>true if keyboard cues are underlined after the change; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool ShowKeyboard
	{
		get
		{
			if ((cues & UICues.ShowKeyboard) == 0)
			{
				return false;
			}
			return true;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UICuesEventArgs" /> class with the specified <see cref="T:System.Windows.Forms.UICues" />.</summary>
	/// <param name="uicues">A bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values. </param>
	public UICuesEventArgs(UICues uicues)
	{
		cues = uicues;
	}
}
