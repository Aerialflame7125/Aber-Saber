using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms;

/// <summary>Represents a Windows spin box (also known as an up-down control) that displays numeric values.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
[DefaultEvent("ValueChanged")]
[DefaultBindingProperty("Value")]
[DefaultProperty("Value")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class NumericUpDown : UpDownBase, ISupportInitialize
{
	private bool suppress_validation;

	private int decimal_places;

	private bool hexadecimal;

	private decimal increment;

	private decimal maximum;

	private decimal minimum;

	private bool thousands_separator;

	private decimal dvalue;

	private NumericUpDownAccelerationCollection accelerations;

	private static object UIAMinimumChangedEvent;

	private static object UIAMaximumChangedEvent;

	private static object UIASmallChangeChangedEvent;

	private static object ValueChangedEvent;

	/// <summary>Gets a collection of sorted acceleration objects for the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> containing the sorted acceleration objects for the <see cref="T:System.Windows.Forms.NumericUpDown" /> control</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public NumericUpDownAccelerationCollection Accelerations
	{
		get
		{
			if (accelerations == null)
			{
				accelerations = new NumericUpDownAccelerationCollection();
			}
			return accelerations;
		}
	}

	/// <summary>Gets or sets the number of decimal places to display in the spin box (also known as an up-down control).</summary>
	/// <returns>The number of decimal places to display in the spin box. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than 0.-or- The value assigned is greater than 99. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	public int DecimalPlaces
	{
		get
		{
			return decimal_places;
		}
		set
		{
			decimal_places = value;
			UpdateEditText();
		}
	}

	/// <summary>Gets or sets a value indicating whether the spin box (also known as an up-down control) should display the value it contains in hexadecimal format.</summary>
	/// <returns>true if the spin box should display its value in hexadecimal format; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Hexadecimal
	{
		get
		{
			return hexadecimal;
		}
		set
		{
			hexadecimal = value;
			UpdateEditText();
		}
	}

	/// <summary>Gets or sets the value to increment or decrement the spin box (also known as an up-down control) when the up or down buttons are clicked.</summary>
	/// <returns>The value to increment or decrement the <see cref="P:System.Windows.Forms.NumericUpDown.Value" /> property when the up or down buttons are clicked on the spin box. The default value is 1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is not greater than or equal to zero. </exception>
	/// <filterpriority>1</filterpriority>
	public decimal Increment
	{
		get
		{
			return increment;
		}
		set
		{
			if (value < 0m)
			{
				throw new ArgumentOutOfRangeException("value", value, "NumericUpDown increment cannot be negative");
			}
			increment = value;
			OnUIASmallChangeChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets the maximum value for the spin box (also known as an up-down control).</summary>
	/// <returns>The maximum value for the spin box. The default value is 100.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	public decimal Maximum
	{
		get
		{
			return maximum;
		}
		set
		{
			maximum = value;
			if (minimum > maximum)
			{
				minimum = maximum;
			}
			if (dvalue > maximum)
			{
				Value = maximum;
			}
			OnUIAMaximumChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets the minimum allowed value for the spin box (also known as an up-down control).</summary>
	/// <returns>The minimum allowed value for the spin box. The default value is 0.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	public decimal Minimum
	{
		get
		{
			return minimum;
		}
		set
		{
			minimum = value;
			if (maximum < minimum)
			{
				maximum = minimum;
			}
			if (dvalue < minimum)
			{
				Value = minimum;
			}
			OnUIAMinimumChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control and its contents.</summary>
	/// <returns>
	///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Padding Padding
	{
		get
		{
			return Padding.Empty;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the text to be displayed in the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a thousands separator is displayed in the spin box (also known as an up-down control) when appropriate.</summary>
	/// <returns>true if a thousands separator is displayed in the spin box when appropriate; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(false)]
	public bool ThousandsSeparator
	{
		get
		{
			return thousands_separator;
		}
		set
		{
			thousands_separator = value;
			UpdateEditText();
		}
	}

	/// <summary>Gets or sets the value assigned to the spin box (also known as an up-down control).</summary>
	/// <returns>The numeric value of the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the <see cref="P:System.Windows.Forms.NumericUpDown.Minimum" /> property value.-or- The assigned value is greater than the <see cref="P:System.Windows.Forms.NumericUpDown.Maximum" /> property value. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(true)]
	public decimal Value
	{
		get
		{
			if (base.UserEdit)
			{
				ValidateEditText();
			}
			return dvalue;
		}
		set
		{
			if (value != dvalue)
			{
				if (!suppress_validation && (value < minimum || value > maximum))
				{
					throw new ArgumentOutOfRangeException("value", "NumericUpDown.Value must be within the specified Minimum and Maximum values");
				}
				dvalue = value;
				OnValueChanged(EventArgs.Empty);
				UpdateEditText();
			}
		}
	}

	internal event EventHandler UIAMinimumChanged
	{
		add
		{
			base.Events.AddHandler(UIAMinimumChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAMinimumChangedEvent, value);
		}
	}

	internal event EventHandler UIAMaximumChanged
	{
		add
		{
			base.Events.AddHandler(UIAMaximumChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAMaximumChangedEvent, value);
		}
	}

	internal event EventHandler UIASmallChangeChanged
	{
		add
		{
			base.Events.AddHandler(UIASmallChangeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIASmallChangeChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.NumericUpDown.Padding" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler PaddingChanged
	{
		add
		{
			base.PaddingChanged += value;
		}
		remove
		{
			base.PaddingChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.NumericUpDown.Value" /> property has been changed in some way.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ValueChanged
	{
		add
		{
			base.Events.AddHandler(ValueChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValueChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.NumericUpDown.Text" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler TextChanged
	{
		add
		{
			base.TextChanged += value;
		}
		remove
		{
			base.TextChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDown" /> class.</summary>
	public NumericUpDown()
	{
		suppress_validation = false;
		decimal_places = 0;
		hexadecimal = false;
		increment = 1m;
		maximum = 100m;
		minimum = 0m;
		thousands_separator = false;
		Text = "0";
	}

	static NumericUpDown()
	{
		UIAMinimumChanged = new object();
		UIAMaximumChanged = new object();
		UIASmallChangeChanged = new object();
		ValueChanged = new object();
	}

	internal void OnUIAMinimumChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIAMinimumChanged])?.Invoke(this, e);
	}

	internal void OnUIAMaximumChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIAMaximumChanged])?.Invoke(this, e);
	}

	internal void OnUIASmallChangeChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIASmallChangeChanged])?.Invoke(this, e);
	}

	private void wide_number_multiply_by_10(int[] number)
	{
		long num = 0L;
		for (int i = 0; i < number.Length; i++)
		{
			long num2 = num + 10L * (long)(uint)number[i];
			num = num2 >> 32;
			number[i] = (int)num2;
		}
	}

	private void wide_number_multiply_by_16(int[] number)
	{
		int num = 0;
		for (int i = 0; i < number.Length; i++)
		{
			int num2 = num | (number[i] << 4);
			num = (number[i] >> 28) & 0xF;
			number[i] = num2;
		}
	}

	private void wide_number_divide_by_16(int[] number)
	{
		int num = 0;
		for (int num2 = number.Length - 1; num2 >= 0; num2--)
		{
			int num3 = num | ((number[num2] >> 4) & 0xFFFFFFF);
			num = number[num2] << 28;
			number[num2] = num3;
		}
	}

	private bool wide_number_less_than(int[] left, int[] right)
	{
		for (int num = left.Length - 1; num >= 0; num--)
		{
			uint num2 = (uint)left[num];
			uint num3 = (uint)right[num];
			if (num2 > num3)
			{
				return false;
			}
			if (num2 < num3)
			{
				return true;
			}
		}
		return false;
	}

	private void wide_number_subtract(int[] subtrahend, int[] minuend)
	{
		long num = 0L;
		for (int i = 0; i < subtrahend.Length; i++)
		{
			long num2 = (uint)subtrahend[i];
			long num3 = (uint)minuend[i];
			long num4 = num2 - num3 + num;
			if (num4 < 0)
			{
				num = -1L;
				num4 -= int.MinValue;
				num4 -= int.MinValue;
			}
			else
			{
				num = 0L;
			}
			subtrahend[i] = (int)num4;
		}
	}

	/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control that is used on a form or used by another component. The initialization occurs at run time.</summary>
	/// <filterpriority>1</filterpriority>
	public void BeginInit()
	{
		suppress_validation = true;
	}

	/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control that is used on a form or used by another component. The initialization occurs at run time.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void EndInit()
	{
		suppress_validation = false;
		Value = Check(dvalue);
		UpdateEditText();
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.NumericUpDown" />. </returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"{base.ToString()}, Minimum = {minimum}, Maximum = {maximum}";
	}

	/// <summary>Decrements the value of the spin box (also known as an up-down control).</summary>
	/// <filterpriority>1</filterpriority>
	public override void DownButton()
	{
		if (base.UserEdit)
		{
			ParseEditText();
		}
		Value = Math.Max(minimum, dvalue - increment);
		OnUIADownButtonClick(EventArgs.Empty);
	}

	/// <summary>Increments the value of the spin box (also known as an up-down control).</summary>
	/// <filterpriority>1</filterpriority>
	public override void UpButton()
	{
		if (base.UserEdit)
		{
			ParseEditText();
		}
		Value = Math.Min(maximum, dvalue + increment);
		OnUIAUpButtonClick(EventArgs.Empty);
	}

	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		AccessibleObject accessibleObject = new AccessibleObject(this);
		accessibleObject.role = AccessibleRole.SpinButton;
		return accessibleObject;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
	protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
	{
		if ((Control.ModifierKeys & ~Keys.Shift) != 0)
		{
			return;
		}
		NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
		string text = e.KeyChar.ToString();
		if (text != numberFormat.NegativeSign && text != numberFormat.NumberDecimalSeparator && text != numberFormat.NumberGroupSeparator)
		{
			string text2 = ((!hexadecimal) ? "\b0123456789" : "\b0123456789abcdefABCDEF");
			if (text2.IndexOf(e.KeyChar) == -1)
			{
				e.Handled = true;
			}
		}
		base.OnTextBoxKeyPress(source, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.NumericUpDown.ValueChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnValueChanged(EventArgs e)
	{
		((EventHandler)base.Events[ValueChanged])?.Invoke(this, e);
	}

	/// <summary>Converts the text displayed in the spin box (also known as an up-down control) to a numeric value and evaluates it.</summary>
	protected void ParseEditText()
	{
		try
		{
			if (!hexadecimal)
			{
				Value = Check(decimal.Parse(Text, CultureInfo.CurrentCulture));
			}
			else
			{
				Value = Check(Convert.ToDecimal(Convert.ToInt32(Text, 10)));
			}
		}
		catch
		{
		}
		finally
		{
			base.UserEdit = false;
		}
	}

	private decimal Check(decimal val)
	{
		decimal num = val;
		if (num < minimum)
		{
			num = minimum;
		}
		if (num > maximum)
		{
			num = maximum;
		}
		return num;
	}

	/// <summary>Displays the current value of the spin box (also known as an up-down control) in the appropriate format.</summary>
	protected override void UpdateEditText()
	{
		if (suppress_validation)
		{
			return;
		}
		if (base.UserEdit)
		{
			ParseEditText();
		}
		base.ChangingText = true;
		if (!hexadecimal)
		{
			string text = ((!thousands_separator) ? "F" : "N");
			text += decimal_places;
			Text = dvalue.ToString(text, CultureInfo.CurrentCulture);
			return;
		}
		int[] bits = decimal.GetBits(dvalue);
		bool flag = bits[3] < 0;
		int num = (bits[3] >> 16) & 0x1F;
		bits[3] = 0;
		int[] array = new int[4] { 1, 0, 0, 0 };
		for (int i = 0; i < num; i++)
		{
			wide_number_multiply_by_10(array);
		}
		int num2 = 0;
		while (!wide_number_less_than(bits, array))
		{
			num2++;
			wide_number_multiply_by_16(array);
		}
		if (num2 == 0)
		{
			Text = "0";
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (flag)
		{
			stringBuilder.Append('-');
		}
		for (int j = 0; j < num2; j++)
		{
			int num3 = 0;
			wide_number_divide_by_16(array);
			while (!wide_number_less_than(bits, array))
			{
				num3++;
				wide_number_subtract(bits, array);
			}
			if (num3 < 10)
			{
				stringBuilder.Append((char)(48 + num3));
			}
			else
			{
				stringBuilder.Append((char)(65 + num3 - 10));
			}
		}
		Text = stringBuilder.ToString();
	}

	/// <summary>Validates and updates the text displayed in the spin box (also known as an up-down control).</summary>
	protected override void ValidateEditText()
	{
		ParseEditText();
		UpdateEditText();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnLostFocus(EventArgs e)
	{
		base.OnLostFocus(e);
		if (base.UserEdit)
		{
			UpdateEditText();
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
	protected override void OnKeyUp(KeyEventArgs e)
	{
		base.OnKeyUp(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
	}
}
