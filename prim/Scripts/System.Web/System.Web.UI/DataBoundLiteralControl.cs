using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI;

/// <summary>Retains data-binding expressions and static literal text. This class cannot be inherited.</summary>
[ToolboxItem(false)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataBoundLiteralControl : Control, ITextControl
{
	private int staticLiteralsCount;

	private string[] staticLiterals;

	private string[] dataBoundLiterals;

	/// <summary>Gets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> object. </summary>
	/// <returns>A <see cref="T:System.String" /> that represents the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</returns>
	public string Text
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = ((staticLiterals != null) ? staticLiterals.Length : 0);
			int num2 = dataBoundLiterals.Length;
			int num3 = ((num > num2) ? num : num2);
			for (int i = 0; i < num3; i++)
			{
				if (i < num)
				{
					stringBuilder.Append(staticLiterals[i]);
				}
				if (i < num2)
				{
					stringBuilder.Append(dataBoundLiterals[i]);
				}
			}
			return stringBuilder.ToString();
		}
	}

	/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> object.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt to set the value is made.</exception>
	string ITextControl.Text
	{
		get
		{
			return Text;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> class. </summary>
	/// <param name="staticLiteralsCount">Defines the size of the array to create for storing static literal strings.</param>
	/// <param name="dataBoundLiteralCount">Defines the size of the array to create for storing data-bound literal strings.</param>
	public DataBoundLiteralControl(int staticLiteralsCount, int dataBoundLiteralCount)
	{
		this.staticLiteralsCount = staticLiteralsCount;
		dataBoundLiterals = new string[dataBoundLiteralCount];
		base.AutoID = false;
	}

	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	protected override void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			Array array = (Array)savedState;
			if (array.Length == dataBoundLiterals.Length)
			{
				array.CopyTo(dataBoundLiterals, 0);
			}
		}
	}

	protected internal override void Render(HtmlTextWriter output)
	{
		int num = ((staticLiterals != null) ? staticLiterals.Length : 0);
		int num2 = dataBoundLiterals.Length;
		int num3 = ((num > num2) ? num : num2);
		for (int i = 0; i < num3; i++)
		{
			if (i < num)
			{
				output.Write(staticLiterals[i]);
			}
			if (i < num2)
			{
				output.Write(dataBoundLiterals[i]);
			}
		}
	}

	protected override object SaveViewState()
	{
		if (dataBoundLiterals.Length == 0)
		{
			return null;
		}
		return dataBoundLiterals;
	}

	/// <summary>Assigns a string value to an array containing data-bound values.</summary>
	/// <param name="index">The position in an array at which to retain the <paramref name="s" /> parameter value. </param>
	/// <param name="s">A <see cref="T:System.String" /> containing the value for the data-bound expression.</param>
	public void SetDataBoundString(int index, string s)
	{
		dataBoundLiterals[index] = s;
	}

	/// <summary>Assigns a string value to an array containing static values.</summary>
	/// <param name="index">The position in an array at which to retain the <paramref name="s" /> parameter value.</param>
	/// <param name="s">A <see cref="T:System.String" /> containing the value for the data-bound expression.</param>
	public void SetStaticString(int index, string s)
	{
		if (staticLiterals == null)
		{
			staticLiterals = new string[staticLiteralsCount];
		}
		staticLiterals[index] = s;
	}
}
