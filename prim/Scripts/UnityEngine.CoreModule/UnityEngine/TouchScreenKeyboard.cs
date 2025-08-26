using UnityEngine.Internal;

namespace UnityEngine;

public sealed class TouchScreenKeyboard
{
	public enum Status
	{
		Visible,
		Done,
		Canceled,
		LostFocus
	}

	public string text
	{
		get
		{
			return string.Empty;
		}
		set
		{
		}
	}

	public static bool hideInput
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public bool active
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public bool done => true;

	public bool wasCanceled => false;

	public Status status => Status.Done;

	private static Rect area => default(Rect);

	private static bool visible => false;

	public static bool isSupported => false;

	public int characterLimit
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public bool canGetSelection => false;

	public bool canSetSelection => false;

	public RangeInt selection
	{
		get
		{
			return new RangeInt(0, 0);
		}
		set
		{
		}
	}

	[ExcludeFromDocs]
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
	{
		int num = 0;
		return Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
	}

	[ExcludeFromDocs]
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
	{
		int num = 0;
		string textPlaceholder = "";
		return Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
	}

	[ExcludeFromDocs]
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
	{
		int num = 0;
		string textPlaceholder = "";
		bool alert = false;
		return Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
	}

	[ExcludeFromDocs]
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
	{
		int num = 0;
		string textPlaceholder = "";
		bool alert = false;
		bool secure = false;
		return Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
	}

	[ExcludeFromDocs]
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
	{
		int num = 0;
		string textPlaceholder = "";
		bool alert = false;
		bool secure = false;
		bool multiline = false;
		return Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
	}

	[ExcludeFromDocs]
	public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
	{
		int num = 0;
		string textPlaceholder = "";
		bool alert = false;
		bool secure = false;
		bool multiline = false;
		bool autocorrection = true;
		return Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
	}

	[ExcludeFromDocs]
	public static TouchScreenKeyboard Open(string text)
	{
		int num = 0;
		string textPlaceholder = "";
		bool alert = false;
		bool secure = false;
		bool multiline = false;
		bool autocorrection = true;
		TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default;
		return Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
	}

	public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder, [DefaultValue("0")] int characterLimit)
	{
		return null;
	}
}
