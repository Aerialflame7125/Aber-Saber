using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides a dialog box for editing regular expressions used by the <see cref="T:System.Web.UI.WebControls.RegularExpressionValidator" />.</summary>
public class RegexEditorDialog : Form
{
	private string regular_expression;

	/// <summary>Gets or sets the name of the regular expression to edit.</summary>
	/// <returns>The name of the regular expression.</returns>
	public string RegularExpression
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			regular_expression = value;
		}
	}

	protected void CmdHelp_Click(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	protected void CmdOK_Click(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	protected void CmdTestValidate_Click(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Web.UI.Design.WebControls.RegexEditorDialog" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">A value indicating to all resources held by any managed objects that this <see cref="T:System.Web.UI.Design.WebControls.RegexEditorDialog" /> references.</param>
	protected override void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	protected void LstStandardExpressions_SelectedIndexChanged(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Represents the method that will handle the Activated event of dialog box.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that provides data for the event.</param>
	protected void RegexTypeEditor_Activated(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	protected void TxtExpression_Changed(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}
}
