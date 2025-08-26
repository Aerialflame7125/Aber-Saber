using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design;

internal class StringCollectionEditor : CollectionEditor
{
	private class StringCollectionEditForm : CollectionForm
	{
		private TextBox txtItems;

		private Label label1;

		private Button butOk;

		private Button butCancel;

		public StringCollectionEditForm(CollectionEditor editor)
			: base(editor)
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.txtItems = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butOk = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			base.SuspendLayout();
			this.txtItems.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtItems.Location = new System.Drawing.Point(12, 25);
			this.txtItems.Multiline = true;
			this.txtItems.AcceptsTab = true;
			this.txtItems.Name = "txtItems";
			this.txtItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtItems.Size = new System.Drawing.Size(378, 168);
			this.txtItems.TabIndex = 1;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(227, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Enter the strings in the collection (one per line):";
			this.butOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.butOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.butOk.Location = new System.Drawing.Point(234, 199);
			this.butOk.Name = "butOk";
			this.butOk.Size = new System.Drawing.Size(75, 23);
			this.butOk.TabIndex = 3;
			this.butOk.Text = "OK";
			this.butOk.Click += new System.EventHandler(butOk_Click);
			this.butCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(315, 199);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 23);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(butCancel_Click);
			base.ClientSize = new System.Drawing.Size(402, 228);
			base.Controls.Add(this.butCancel);
			base.Controls.Add(this.butOk);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtItems);
			base.CancelButton = this.butCancel;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "StringEditorForm";
			this.Text = "String Collection Editor";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		protected override void OnEditValueChanged()
		{
			object[] items = base.Items;
			string text = string.Empty;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] is string)
				{
					text += (string)items[i];
					if (i != items.Length - 1)
					{
						text += Environment.NewLine;
					}
				}
			}
			txtItems.Text = text;
		}

		private void butOk_Click(object sender, EventArgs e)
		{
			if (txtItems.Text == string.Empty)
			{
				base.Items = new string[0];
				return;
			}
			string[] lines = txtItems.Lines;
			object[] array = new object[(lines[lines.Length - 1].Trim().Length == 0) ? (lines.Length - 1) : lines.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = lines[i];
			}
			base.Items = array;
		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}

	public StringCollectionEditor(Type type)
		: base(type)
	{
	}

	protected override CollectionForm CreateCollectionForm()
	{
		return new StringCollectionEditForm(this);
	}
}
