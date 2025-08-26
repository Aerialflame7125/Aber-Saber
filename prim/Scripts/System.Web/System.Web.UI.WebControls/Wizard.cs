using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides navigation and a user interface (UI) to collect related data across multiple steps.</summary>
[DefaultEvent("FinishButtonClick")]
[Bindable(false)]
[Designer("System.Web.UI.Design.WebControls.WizardDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxData("<{0}:Wizard runat=\"server\"> <WizardSteps> <asp:WizardStep title=\"Step 1\" runat=\"server\"></asp:WizardStep> <asp:WizardStep title=\"Step 2\" runat=\"server\"></asp:WizardStep> </WizardSteps> </{0}:Wizard>")]
public class Wizard : CompositeControl
{
	private sealed class TableCellNamingContainer : TableCell, INamingContainer, INonBindingContainer
	{
		private string skipLinkText;

		private string clientId;

		private bool haveSkipLink;

		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			if (haveSkipLink)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + clientId + "_SkipLink");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, skipLinkText);
				writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
				Page page = Page;
				ClientScriptManager clientScriptManager = ((page == null) ? new ClientScriptManager(null) : page.ClientScript);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			base.RenderChildren(writer);
			if (haveSkipLink)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, "SkipLink");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderEndTag();
			}
		}

		public TableCellNamingContainer(string skipLinkText, string clientId)
		{
			this.skipLinkText = skipLinkText;
			this.clientId = clientId;
			haveSkipLink = !string.IsNullOrEmpty(skipLinkText);
		}
	}

	private sealed class SideBarButtonTemplate : ITemplate
	{
		private Wizard wizard;

		public SideBarButtonTemplate(Wizard wizard)
		{
			this.wizard = wizard;
		}

		public void InstantiateIn(Control control)
		{
			LinkButton linkButton = new LinkButton();
			wizard.RegisterApplyStyle(linkButton, wizard.SideBarButtonStyle);
			control.Controls.Add(linkButton);
			control.DataBinding += Bound;
		}

		private void Bound(object s, EventArgs args)
		{
			if (DataBinder.GetDataItem(s) is WizardStepBase wizardStepBase)
			{
				LinkButton linkButton = (LinkButton)((DataListItem)s).Controls[0];
				linkButton.ID = SideBarButtonID;
				linkButton.CommandName = MoveToCommandName;
				linkButton.CommandArgument = wizard.WizardSteps.IndexOf(wizardStepBase).ToString();
				linkButton.Text = wizardStepBase.Name;
				if (wizardStepBase.StepType == WizardStepType.Complete)
				{
					linkButton.Enabled = false;
				}
			}
		}
	}

	private class WizardHeaderCell : TableCell, INamingContainer, INonBindingContainer
	{
		private bool _initialized;

		public bool Initialized => _initialized;

		public void ConfirmInitState()
		{
			_initialized = true;
		}
	}

	internal abstract class DefaultNavigationContainer : BaseWizardNavigationContainer
	{
		private bool _isDefault;

		private Wizard _wizard;

		protected Wizard Wizard => _wizard;

		protected DefaultNavigationContainer(Wizard wizard)
		{
			_wizard = wizard;
		}

		public sealed override void PrepareControlHierarchy()
		{
			if (_isDefault)
			{
				UpdateState();
			}
		}

		protected abstract void UpdateState();

		public void ConfirmDefaultTemplate()
		{
			_isDefault = true;
		}

		protected void UpdateNavButtonState(string id, string text, string image, Style style)
		{
			WebControl webControl = (WebControl)FindControl(id);
			foreach (Control control in webControl.Parent.Controls)
			{
				control.Visible = webControl == control;
			}
			((IButtonControl)webControl).Text = text;
			if (webControl is ImageButton imageButton)
			{
				imageButton.ImageUrl = image;
			}
			webControl.ApplyStyle(style);
		}
	}

	private sealed class StartNavigationContainer : DefaultNavigationContainer
	{
		public StartNavigationContainer(Wizard wizard)
			: base(wizard)
		{
		}

		protected override void UpdateState()
		{
			bool visible = false;
			if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex + 1))
			{
				visible = true;
				UpdateNavButtonState(StartNextButtonIDShort + base.Wizard.StartNextButtonType, base.Wizard.StartNextButtonText, base.Wizard.StartNextButtonImageUrl, base.Wizard.StartNextButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[0].Visible = false;
			}
			if (base.Wizard.DisplayCancelButton)
			{
				visible = true;
				UpdateNavButtonState(CancelButtonIDShort + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[1].Visible = false;
			}
			Visible = visible;
		}
	}

	private sealed class StepNavigationContainer : DefaultNavigationContainer
	{
		public StepNavigationContainer(Wizard wizard)
			: base(wizard)
		{
		}

		protected override void UpdateState()
		{
			bool visible = false;
			if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex - 1))
			{
				visible = true;
				UpdateNavButtonState(StepPreviousButtonIDShort + base.Wizard.StepPreviousButtonType, base.Wizard.StepPreviousButtonText, base.Wizard.StepPreviousButtonImageUrl, base.Wizard.StepPreviousButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[0].Visible = false;
			}
			if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex + 1))
			{
				visible = true;
				UpdateNavButtonState(StepNextButtonIDShort + base.Wizard.StepNextButtonType, base.Wizard.StepNextButtonText, base.Wizard.StepNextButtonImageUrl, base.Wizard.StepNextButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[1].Visible = false;
			}
			if (base.Wizard.DisplayCancelButton)
			{
				visible = true;
				UpdateNavButtonState(CancelButtonIDShort + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[2].Visible = false;
			}
			Visible = visible;
		}
	}

	private sealed class FinishNavigationContainer : DefaultNavigationContainer
	{
		public FinishNavigationContainer(Wizard wizard)
			: base(wizard)
		{
		}

		protected override void UpdateState()
		{
			int num = base.Wizard.ActiveStepIndex - 1;
			if (num >= 0 && base.Wizard.AllowNavigationToStep(num))
			{
				UpdateNavButtonState(FinishPreviousButtonIDShort + base.Wizard.FinishPreviousButtonType, base.Wizard.FinishPreviousButtonText, base.Wizard.FinishPreviousButtonImageUrl, base.Wizard.FinishPreviousButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[0].Visible = false;
			}
			UpdateNavButtonState(FinishButtonIDShort + base.Wizard.FinishCompleteButtonType, base.Wizard.FinishCompleteButtonText, base.Wizard.FinishCompleteButtonImageUrl, base.Wizard.FinishCompleteButtonStyle);
			if (base.Wizard.DisplayCancelButton)
			{
				UpdateNavButtonState(CancelButtonIDShort + base.Wizard.CancelButtonType, base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
			}
			else
			{
				((Table)Controls[0]).Rows[0].Cells[2].Visible = false;
			}
		}
	}

	internal class BaseWizardContainer : Table, INamingContainer, INonBindingContainer
	{
		public TableCell InnerCell => Rows[0].Cells[0];

		internal BaseWizardContainer()
		{
			InitTable();
		}

		private void InitTable()
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.ControlStyle.Width = Unit.Percentage(100.0);
			tableCell.ControlStyle.Height = Unit.Percentage(100.0);
			tableRow.Cells.Add(tableCell);
			base.ControlStyle.Width = Unit.Percentage(100.0);
			base.ControlStyle.Height = Unit.Percentage(100.0);
			CellPadding = 0;
			CellSpacing = 0;
			Rows.Add(tableRow);
		}

		public virtual void PrepareControlHierarchy()
		{
		}
	}

	internal class BaseWizardNavigationContainer : Control, INamingContainer, INonBindingContainer
	{
		internal BaseWizardNavigationContainer()
		{
		}

		public virtual void PrepareControlHierarchy()
		{
		}
	}

	internal abstract class DefaultContentContainer : BaseWizardContainer
	{
		private bool _isDefault;

		private Wizard _wizard;

		protected bool IsDefaultTemplate => _isDefault;

		protected Wizard Wizard => _wizard;

		protected DefaultContentContainer(Wizard wizard)
		{
			_wizard = wizard;
		}

		public sealed override void PrepareControlHierarchy()
		{
			if (_isDefault)
			{
				UpdateState();
			}
		}

		protected abstract void UpdateState();

		public void ConfirmDefaultTemplate()
		{
			_isDefault = true;
		}
	}

	/// <summary>Retrieves the command name for the Cancel button. This field is static and read-only.</summary>
	public static readonly string CancelCommandName = "Cancel";

	/// <summary>Retrieves the command name that is associated with the Finish button. This field is static and read-only.</summary>
	public static readonly string MoveCompleteCommandName = "MoveComplete";

	/// <summary>Retrieves the command name that is associated with the Next button. This field is static and read-only.</summary>
	public static readonly string MoveNextCommandName = "MoveNext";

	/// <summary>Retrieves the command name that is associated with the Previous button. This field is static and read-only. </summary>
	public static readonly string MovePreviousCommandName = "MovePrevious";

	/// <summary>Retrieves the command name that is associated with each of the sidebar buttons. This field is static and read-only. </summary>
	public static readonly string MoveToCommandName = "Move";

	/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.HeaderTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	public static readonly string HeaderPlaceholderId = "headerPlaceholder";

	/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.StartNavigationTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	public static readonly string NavigationPlaceholderId = "navigationPlaceholder";

	/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.SideBarTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	public static readonly string SideBarPlaceholderId = "sideBarPlaceholder";

	/// <summary>Gets the ID of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	public static readonly string WizardStepPlaceholderId = "wizardStepPlaceholder";

	/// <summary>Retrieves the identifier for the sidebar <see cref="T:System.Web.UI.WebControls.DataList" /> collection. This field is static and read-only.</summary>
	protected static readonly string DataListID = "SideBarList";

	private static readonly string CancelButtonIDShort = "Cancel";

	/// <summary>Specifies the identifier for the Cancel button. This field is static and read-only.</summary>
	protected static readonly string CancelButtonID = CancelButtonIDShort + "Button";

	private static readonly string CustomFinishButtonIDShort = "CustomFinish";

	/// <summary>Retrieves the identifier for a custom Finish button. This field is static and read-only.</summary>
	protected static readonly string CustomFinishButtonID = CustomFinishButtonIDShort + "Button";

	private static readonly string CustomNextButtonIDShort = "CustomNext";

	/// <summary>Retrieves the identifier for a custom Next button. This field is static and read-only.</summary>
	protected static readonly string CustomNextButtonID = CustomNextButtonIDShort + "Button";

	private static readonly string CustomPreviousButtonIDShort = "CustomPrevious";

	/// <summary>Retrieves the identifier for a custom Previous button. This field is static and read-only.</summary>
	protected static readonly string CustomPreviousButtonID = CustomPreviousButtonIDShort + "Button";

	private static readonly string FinishButtonIDShort = "Finish";

	/// <summary>Retrieves the identifier for the Finish button. This field is static and read-only.</summary>
	protected static readonly string FinishButtonID = FinishButtonIDShort + "Button";

	private static readonly string FinishPreviousButtonIDShort = "FinishPrevious";

	/// <summary>Retrieves the identifier for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step. This field is static and read-only.</summary>
	protected static readonly string FinishPreviousButtonID = FinishPreviousButtonIDShort + "Button";

	private static readonly string SideBarButtonIDShort = "SideBar";

	/// <summary>Retrieves the identifier that is associated with each of the sidebar buttons. This field is static and read-only. </summary>
	protected static readonly string SideBarButtonID = SideBarButtonIDShort + "Button";

	private static readonly string StartNextButtonIDShort = "StartNext";

	/// <summary>Retrieves the identifier that is associated with the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step. This field is static and read-only. </summary>
	protected static readonly string StartNextButtonID = StartNextButtonIDShort + "Button";

	private static readonly string StepNextButtonIDShort = "StepNext";

	/// <summary>Retrieves the identifier that is associated with the Next button. This field is static and read-only. </summary>
	protected static readonly string StepNextButtonID = StepNextButtonIDShort + "Button";

	private static readonly string StepPreviousButtonIDShort = "StepPrevious";

	/// <summary>Retrieves the identifier that is associated with the Previous button. This field is static and read-only. </summary>
	protected static readonly string StepPreviousButtonID = StepPreviousButtonIDShort + "Button";

	private WizardStepCollection steps;

	private TableItemStyle stepStyle;

	private TableItemStyle sideBarStyle;

	private TableItemStyle headerStyle;

	private TableItemStyle navigationStyle;

	private Style sideBarButtonStyle;

	private Style cancelButtonStyle;

	private Style finishCompleteButtonStyle;

	private Style finishPreviousButtonStyle;

	private Style startNextButtonStyle;

	private Style stepNextButtonStyle;

	private Style stepPreviousButtonStyle;

	private Style navigationButtonStyle;

	private ITemplate finishNavigationTemplate;

	private ITemplate startNavigationTemplate;

	private ITemplate stepNavigationTemplate;

	private ITemplate headerTemplate;

	private ITemplate sideBarTemplate;

	private int activeStepIndex = -1;

	private bool inited;

	private ArrayList history;

	private Table wizardTable;

	private WizardHeaderCell _headerCell;

	private TableCell _navigationCell;

	private StartNavigationContainer _startNavContainer;

	private StepNavigationContainer _stepNavContainer;

	private FinishNavigationContainer _finishNavContainer;

	private MultiView multiView;

	private DataList stepDatalist;

	private ArrayList styles = new ArrayList();

	private Hashtable customNavigation;

	private static readonly object ActiveStepChangedEvent;

	private static readonly object CancelButtonClickEvent;

	private static readonly object FinishButtonClickEvent;

	private static readonly object NextButtonClickEvent;

	private static readonly object PreviousButtonClickEvent;

	private static readonly object SideBarButtonClickEvent;

	/// <summary>Gets the step in the <see cref="P:System.Web.UI.WebControls.Wizard.WizardSteps" /> collection that is currently displayed to the user.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that is currently displayed to the user.</returns>
	/// <exception cref="T:System.InvalidOperationException">The corresponding <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStepIndex" /> is less than -1 or greater than the number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects in the <see cref="T:System.Web.UI.WebControls.Wizard" />.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public WizardStepBase ActiveStep
	{
		get
		{
			int num = ActiveStepIndex;
			if (num < -1 || num >= WizardSteps.Count)
			{
				throw new InvalidOperationException("ActiveStepIndex has an invalid value.");
			}
			if (num == -1)
			{
				return null;
			}
			return WizardSteps[num];
		}
	}

	/// <summary>Gets or sets the index of the current <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is higher than the number of wizard steps defined in the <see cref="P:System.Web.UI.WebControls.Wizard.WizardSteps" /> collection.</exception>
	[DefaultValue(-1)]
	[Themeable(false)]
	public virtual int ActiveStepIndex
	{
		get
		{
			return activeStepIndex;
		}
		set
		{
			if (value < -1 || (value > WizardSteps.Count && (inited || WizardSteps.Count > 0)))
			{
				throw new ArgumentOutOfRangeException("The ActiveStepIndex must be less than WizardSteps.Count and at least -1");
			}
			if ((inited && !AllowNavigationToStep(value)) || activeStepIndex == value)
			{
				return;
			}
			activeStepIndex = value;
			if (inited)
			{
				multiView.ActiveViewIndex = value;
				if (stepDatalist != null)
				{
					stepDatalist.SelectedIndex = value;
					stepDatalist.DataBind();
				}
				OnActiveStepChanged(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the URL of the image displayed for the Cancel button.</summary>
	/// <returns>The URL of the image displayed for the Cancel button on the <see cref="T:System.Web.UI.WebControls.Wizard" /> control. The default value is an empty string ("").</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string CancelButtonImageUrl
	{
		get
		{
			object obj = ViewState["CancelButtonImageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["CancelButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a collection of style properties that define the appearance of the Cancel button.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style CancelButtonStyle
	{
		get
		{
			if (cancelButtonStyle == null)
			{
				cancelButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)cancelButtonStyle).TrackViewState();
				}
			}
			return cancelButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption that is displayed for the Cancel button.</summary>
	/// <returns>The text caption displayed for Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Cancel". The default text for the control is localized based on the current locale for the server.</returns>
	[Localizable(true)]
	public virtual string CancelButtonText
	{
		get
		{
			object obj = ViewState["CancelButtonText"];
			if (obj == null)
			{
				return "Cancel";
			}
			return (string)obj;
		}
		set
		{
			ViewState["CancelButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button that is rendered as the Cancel button.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType CancelButtonType
	{
		get
		{
			object obj = ViewState["CancelButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["CancelButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the URL that the user is directed to when they click the Cancel button.</summary>
	/// <returns>The URL that the user is redirected to when they click Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[Themeable(false)]
	public virtual string CancelDestinationPageUrl
	{
		get
		{
			object obj = ViewState["CancelDestinationPageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["CancelDestinationPageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the amount of space between the contents of the cell and the cell border.</summary>
	/// <returns>The amount of space, in pixels, between the contents of a cell and the cell border. The default is 0.</returns>
	[DefaultValue(0)]
	public virtual int CellPadding
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			return 0;
		}
		set
		{
			((TableStyle)base.ControlStyle).CellPadding = value;
		}
	}

	/// <summary>Gets or sets the amount of space between cells.</summary>
	/// <returns>The amount of space, in pixels, between cells. The default is 0.</returns>
	[DefaultValue(0)]
	public virtual int CellSpacing
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			return 0;
		}
		set
		{
			((TableStyle)base.ControlStyle).CellSpacing = value;
		}
	}

	/// <summary>Gets or sets a Boolean value indicating whether to display a Cancel button.</summary>
	/// <returns>
	///     <see langword="true" /> to display Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />; otherwise, <see langword="false" />. The default is <see langword="false" />.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	public virtual bool DisplayCancelButton
	{
		get
		{
			object obj = ViewState["DisplayCancelButton"];
			if (obj == null)
			{
				return false;
			}
			return (bool)obj;
		}
		set
		{
			ViewState["DisplayCancelButton"] = value;
		}
	}

	/// <summary>Gets or sets a Boolean value indicating whether to display the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> to display the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />; otherwise, <see langword="false" />. The default is <see langword="true" />.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	public virtual bool DisplaySideBar
	{
		get
		{
			object obj = ViewState["DisplaySideBar"];
			if (obj == null)
			{
				return true;
			}
			return (bool)obj;
		}
		set
		{
			ViewState["DisplaySideBar"] = value;
			UpdateViews();
		}
	}

	/// <summary>Gets or sets the URL of the image that is displayed for the Finish button.</summary>
	/// <returns>The URL of the image displayed for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string FinishCompleteButtonImageUrl
	{
		get
		{
			object obj = ViewState["FinishCompleteButtonImageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["FinishCompleteButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Finish button.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style FinishCompleteButtonStyle
	{
		get
		{
			if (finishCompleteButtonStyle == null)
			{
				finishCompleteButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)finishCompleteButtonStyle).TrackViewState();
				}
			}
			return finishCompleteButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption that is displayed for the Finish button.</summary>
	/// <returns>The text caption displayed for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Finish". The default text for the control is localized based on the current locale for the server.</returns>
	[Localizable(true)]
	public virtual string FinishCompleteButtonText
	{
		get
		{
			object obj = ViewState["FinishCompleteButtonText"];
			if (obj == null)
			{
				return "Finish";
			}
			return (string)obj;
		}
		set
		{
			ViewState["FinishCompleteButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button that is rendered as the Finish button.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType FinishCompleteButtonType
	{
		get
		{
			object obj = ViewState["FinishCompleteButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["FinishCompleteButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the URL that the user is redirected to when they click the Finish button.</summary>
	/// <returns>The URL that the user is redirected to when they click Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[Themeable(false)]
	public virtual string FinishDestinationPageUrl
	{
		get
		{
			object obj = ViewState["FinishDestinationPageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["FinishDestinationPageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the template that is used to display the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
	/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> that defines the content for the navigation area for the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate FinishNavigationTemplate
	{
		get
		{
			return finishNavigationTemplate;
		}
		set
		{
			finishNavigationTemplate = value;
			UpdateViews();
		}
	}

	/// <summary>Gets or sets the URL of the image that is displayed for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
	/// <returns>The URL of the image displayed for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string FinishPreviousButtonImageUrl
	{
		get
		{
			object obj = ViewState["FinishPreviousButtonImageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["FinishPreviousButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style FinishPreviousButtonStyle
	{
		get
		{
			if (finishPreviousButtonStyle == null)
			{
				finishPreviousButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)finishPreviousButtonStyle).TrackViewState();
				}
			}
			return finishPreviousButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption that is displayed for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
	/// <returns>The text caption displayed for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Previous". The default text for the control is localized based on the current locale for the server.</returns>
	[Localizable(true)]
	public virtual string FinishPreviousButtonText
	{
		get
		{
			object obj = ViewState["FinishPreviousButtonText"];
			if (obj == null)
			{
				return "Previous";
			}
			return (string)obj;
		}
		set
		{
			ViewState["FinishPreviousButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button that is rendered as the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType FinishPreviousButtonType
	{
		get
		{
			object obj = ViewState["FinishPreviousButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["FinishPreviousButtonType"] = value;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the header area on the control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle HeaderStyle
	{
		get
		{
			if (headerStyle == null)
			{
				headerStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)headerStyle).TrackViewState();
				}
			}
			return headerStyle;
		}
	}

	/// <summary>Gets or sets the template that is used to display the header area on the control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate HeaderTemplate
	{
		get
		{
			return headerTemplate;
		}
		set
		{
			headerTemplate = value;
			UpdateViews();
		}
	}

	/// <summary>Gets or sets the text caption that is displayed for the header area on the control.</summary>
	/// <returns>The text caption displayed for the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string HeaderText
	{
		get
		{
			object obj = ViewState["HeaderText"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["HeaderText"] = value;
		}
	}

	/// <summary>Gets or sets the custom content of the root container in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>An object that contains the custom content for the root container in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control. The default is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(Wizard))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate LayoutTemplate { get; set; }

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the buttons in the navigation area on the control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the buttons in the navigation area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style NavigationButtonStyle
	{
		get
		{
			if (navigationButtonStyle == null)
			{
				navigationButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)navigationButtonStyle).TrackViewState();
				}
			}
			return navigationButtonStyle;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the navigation area on the control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the navigation area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public TableItemStyle NavigationStyle
	{
		get
		{
			if (navigationStyle == null)
			{
				navigationStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)navigationStyle).TrackViewState();
				}
			}
			return navigationStyle;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the sidebar area on the control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	public TableItemStyle SideBarStyle
	{
		get
		{
			if (sideBarStyle == null)
			{
				sideBarStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)sideBarStyle).TrackViewState();
				}
			}
			return sideBarStyle;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the buttons on the sidebar.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the buttons on the sidebar of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	public Style SideBarButtonStyle
	{
		get
		{
			if (sideBarButtonStyle == null)
			{
				sideBarButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)sideBarButtonStyle).TrackViewState();
				}
			}
			return sideBarButtonStyle;
		}
	}

	/// <summary>Gets or sets the template that is used to display the sidebar area on the control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate SideBarTemplate
	{
		get
		{
			return sideBarTemplate;
		}
		set
		{
			sideBarTemplate = value;
			UpdateViews();
		}
	}

	/// <summary>Gets or sets a value that is used to render alternate text that notifies screen readers to skip the content in the sidebar area.</summary>
	/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.Wizard" /> renders as alternate text with an invisible image, as a hint to screen readers. The default is "Skip Navigation Links". The default text for the control is localized based on the current locale for the server.</returns>
	[Localizable(true)]
	public virtual string SkipLinkText
	{
		get
		{
			object obj = ViewState["SkipLinkText"];
			if (obj == null)
			{
				return "Skip Navigation Links.";
			}
			return (string)obj;
		}
		set
		{
			ViewState["SkipLinkText"] = value;
		}
	}

	/// <summary>Gets or sets the template that is used to display the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> for the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate StartNavigationTemplate
	{
		get
		{
			return startNavigationTemplate;
		}
		set
		{
			startNavigationTemplate = value;
			UpdateViews();
		}
	}

	/// <summary>Gets or sets the URL of the image that is displayed for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
	/// <returns>The URL of the image displayed for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string StartNextButtonImageUrl
	{
		get
		{
			object obj = ViewState["StartNextButtonImageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["StartNextButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style StartNextButtonStyle
	{
		get
		{
			if (startNextButtonStyle == null)
			{
				startNextButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)startNextButtonStyle).TrackViewState();
				}
			}
			return startNextButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption that is displayed for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
	/// <returns>The text caption displayed for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Next". The default text for the control is localized based on the current locale for the server.</returns>
	[Localizable(true)]
	public virtual string StartNextButtonText
	{
		get
		{
			object obj = ViewState["StartNextButtonText"];
			if (obj == null)
			{
				return "Next";
			}
			return (string)obj;
		}
		set
		{
			ViewState["StartNextButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button that is rendered as the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType StartNextButtonType
	{
		get
		{
			object obj = ViewState["StartNextButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["StartNextButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the template that is used to display the navigation area on any <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects other than the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" />, the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" />, or <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" /> step.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the navigation area on any <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control other than the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" />, <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" />, or <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" />. The default is <see langword="null" />.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate StepNavigationTemplate
	{
		get
		{
			return stepNavigationTemplate;
		}
		set
		{
			stepNavigationTemplate = value;
			UpdateViews();
		}
	}

	/// <summary>Gets or sets the URL of the image that is displayed for the Next button.</summary>
	/// <returns>The URL of the image displayed for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string StepNextButtonImageUrl
	{
		get
		{
			object obj = ViewState["StepNextButtonImageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["StepNextButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Next button.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style StepNextButtonStyle
	{
		get
		{
			if (stepNextButtonStyle == null)
			{
				stepNextButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)stepNextButtonStyle).TrackViewState();
				}
			}
			return stepNextButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption that is displayed for the Next button.</summary>
	/// <returns>The text caption displayed for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Next". The default text for the control is localized based on the current locale for the server.</returns>
	[Localizable(true)]
	public virtual string StepNextButtonText
	{
		get
		{
			object obj = ViewState["StepNextButtonText"];
			if (obj == null)
			{
				return "Next";
			}
			return (string)obj;
		}
		set
		{
			ViewState["StepNextButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button that is rendered as the Next button.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType StepNextButtonType
	{
		get
		{
			object obj = ViewState["StepNextButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["StepNextButtonType"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the image that is displayed for the Previous button.</summary>
	/// <returns>The URL of the image displayed for Previous on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string StepPreviousButtonImageUrl
	{
		get
		{
			object obj = ViewState["StepPreviousButtonImageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			ViewState["StepPreviousButtonImageUrl"] = value;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Previous button.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Previous on a <see cref="F:System.Web.UI.WebControls.WizardStepType.Step" /> for the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public Style StepPreviousButtonStyle
	{
		get
		{
			if (stepPreviousButtonStyle == null)
			{
				stepPreviousButtonStyle = new Style();
				if (base.IsTrackingViewState)
				{
					((IStateManager)stepPreviousButtonStyle).TrackViewState();
				}
			}
			return stepPreviousButtonStyle;
		}
	}

	/// <summary>Gets or sets the text caption that is displayed for the Previous button.</summary>
	/// <returns>The text caption displayed for Previous on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Previous". The default text for the control is localized based on the current locale for the server.</returns>
	[Localizable(true)]
	public virtual string StepPreviousButtonText
	{
		get
		{
			object obj = ViewState["StepPreviousButtonText"];
			if (obj == null)
			{
				return "Previous";
			}
			return (string)obj;
		}
		set
		{
			ViewState["StepPreviousButtonText"] = value;
		}
	}

	/// <summary>Gets or sets the type of button that is rendered as the Previous button.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
	[DefaultValue(ButtonType.Button)]
	public virtual ButtonType StepPreviousButtonType
	{
		get
		{
			object obj = ViewState["StepPreviousButtonType"];
			if (obj == null)
			{
				return ButtonType.Button;
			}
			return (ButtonType)obj;
		}
		set
		{
			ViewState["StepPreviousButtonType"] = value;
		}
	}

	/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the <see cref="T:System.Web.UI.WebControls.WizardStep" /> objects.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the <see cref="T:System.Web.UI.WebControls.WizardStep" /> objects on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	[NotifyParentProperty(true)]
	public TableItemStyle StepStyle
	{
		get
		{
			if (stepStyle == null)
			{
				stepStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					((IStateManager)stepStyle).TrackViewState();
				}
			}
			return stepStyle;
		}
	}

	/// <summary>Gets a collection containing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that are defined for the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> representing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Editor("System.Web.UI.Design.WebControls.WizardStepCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Themeable(false)]
	public virtual WizardStepCollection WizardSteps
	{
		get
		{
			if (steps == null)
			{
				steps = new WizardStepCollection(this);
			}
			return steps;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
	protected new virtual HtmlTextWriterTag TagKey => HtmlTextWriterTag.Table;

	internal virtual ITemplate SideBarItemTemplate => new SideBarButtonTemplate(this);

	/// <summary>Occurs when the user switches to a new step in the control.</summary>
	public event EventHandler ActiveStepChanged
	{
		add
		{
			base.Events.AddHandler(ActiveStepChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ActiveStepChangedEvent, value);
		}
	}

	/// <summary>Occurs when the Cancel button is clicked.</summary>
	public event EventHandler CancelButtonClick
	{
		add
		{
			base.Events.AddHandler(CancelButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CancelButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when the Finish button is clicked.</summary>
	public event WizardNavigationEventHandler FinishButtonClick
	{
		add
		{
			base.Events.AddHandler(FinishButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FinishButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when the Next button is clicked.</summary>
	public event WizardNavigationEventHandler NextButtonClick
	{
		add
		{
			base.Events.AddHandler(NextButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(NextButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when the Previous button is clicked.</summary>
	public event WizardNavigationEventHandler PreviousButtonClick
	{
		add
		{
			base.Events.AddHandler(PreviousButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PreviousButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when a button in the sidebar area is clicked.</summary>
	public event WizardNavigationEventHandler SideBarButtonClick
	{
		add
		{
			base.Events.AddHandler(SideBarButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SideBarButtonClickEvent, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.ActiveStepChanged" /> event.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnActiveStepChanged(object source, EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[ActiveStepChanged])?.Invoke(source, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.CancelButtonClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data.</param>
	protected virtual void OnCancelButtonClick(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[CancelButtonClick])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.FinishButtonClick" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing the event data.</param>
	protected virtual void OnFinishButtonClick(WizardNavigationEventArgs e)
	{
		if (base.Events != null)
		{
			((WizardNavigationEventHandler)base.Events[FinishButtonClick])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.NextButtonClick" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing the event data.</param>
	protected virtual void OnNextButtonClick(WizardNavigationEventArgs e)
	{
		if (base.Events != null)
		{
			((WizardNavigationEventHandler)base.Events[NextButtonClick])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.PreviousButtonClick" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing event data.</param>
	protected virtual void OnPreviousButtonClick(WizardNavigationEventArgs e)
	{
		if (base.Events != null)
		{
			((WizardNavigationEventHandler)base.Events[PreviousButtonClick])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.SideBarButtonClick" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing event data.</param>
	protected virtual void OnSideBarButtonClick(WizardNavigationEventArgs e)
	{
		if (base.Events != null)
		{
			((WizardNavigationEventHandler)base.Events[SideBarButtonClick])?.Invoke(this, e);
		}
	}

	/// <summary>Returns a collection of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that have been accessed.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that have been accessed.</returns>
	public ICollection GetHistory()
	{
		if (history == null)
		{
			history = new ArrayList();
		}
		return history;
	}

	/// <summary>Sets the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object as the value for the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" /> property of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to set as the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The value of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStepIndex" /> of the associated <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is equal to -1.</exception>
	public void MoveTo(WizardStepBase wizardStep)
	{
		if (wizardStep == null)
		{
			throw new ArgumentNullException("wizardStep");
		}
		int num = WizardSteps.IndexOf(wizardStep);
		if (num == -1)
		{
			throw new ArgumentException("The provided wizard step does not belong to this wizard.");
		}
		ActiveStepIndex = num;
	}

	/// <summary>Returns the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> value for the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object.</summary>
	/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> for which the associated <see cref="T:System.Web.UI.WebControls.WizardStepType" />  is returned.</param>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> for which the associated <see cref="T:System.Web.UI.WebControls.WizardStepType" />  is returned.</param>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> values.</returns>
	public WizardStepType GetStepType(WizardStepBase wizardStep, int index)
	{
		if (wizardStep.StepType == WizardStepType.Auto)
		{
			if (index == WizardSteps.Count - 1 || (WizardSteps.Count > 1 && WizardSteps[WizardSteps.Count - 1].StepType == WizardStepType.Complete && index == WizardSteps.Count - 2))
			{
				return WizardStepType.Finish;
			}
			if (index == 0)
			{
				return WizardStepType.Start;
			}
			return WizardStepType.Step;
		}
		return wizardStep.StepType;
	}

	/// <summary>Uses a Boolean value to determine whether the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" /> property can be set to the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object that corresponds to the index that is passed in.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object being checked.</param>
	/// <returns>
	///     <see langword="false" /> if the index passed in refers to a <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that has already been accessed and its <see cref="P:System.Web.UI.WebControls.WizardStepBase.AllowReturn" /> property is set to <see langword="false" />; otherwise, <see langword="true" />.</returns>
	protected virtual bool AllowNavigationToStep(int index)
	{
		if (index < 0 || index >= WizardSteps.Count || history == null || !history.Contains(index))
		{
			return true;
		}
		return WizardSteps[index].AllowReturn;
	}

	/// <summary>Raises the Init event.</summary>
	/// <param name="e">The raised event.</param>
	protected internal override void OnInit(EventArgs e)
	{
		Page.RegisterRequiresControlState(this);
		base.OnInit(e);
		if (ActiveStepIndex == -1)
		{
			ActiveStepIndex = 0;
		}
		EnsureChildControls();
		inited = true;
	}

	/// <summary>Creates control collection.</summary>
	protected override ControlCollection CreateControlCollection()
	{
		ControlCollection controlCollection = new ControlCollection(this);
		controlCollection.SetReadonly(readOnly: true);
		return controlCollection;
	}

	/// <summary>Creates child controls.</summary>
	protected internal override void CreateChildControls()
	{
		CreateControlHierarchy();
	}

	private InvalidOperationException MakeLayoutException(string phName, string phID, string condition = null)
	{
		return new InvalidOperationException($"A {phName} placeholder must be specified on Wizard '{ID}'{condition}. Specify a placeholder by setting a control's ID property to \"{phID}\". The placeholder control must also specify runat=\"server\"");
	}

	private void CreateControlHierarchy_LayoutTemplate(ITemplate layoutTemplate)
	{
		WizardLayoutContainer wizardLayoutContainer = new WizardLayoutContainer();
		ControlCollection controls = Controls;
		controls.SetReadonly(readOnly: false);
		controls.Add(wizardLayoutContainer);
		controls.SetReadonly(readOnly: true);
		layoutTemplate.InstantiateIn(wizardLayoutContainer);
		WizardStepCollection wizardSteps = WizardSteps;
		bool visible = wizardSteps != null && wizardSteps.Count > 0;
		Control control;
		Control control2;
		if (DisplaySideBar)
		{
			control = wizardLayoutContainer.FindControl(SideBarPlaceholderId);
			if (control == null)
			{
				throw MakeLayoutException("sidebar", SideBarPlaceholderId, " when DisplaySideBar is set to true");
			}
			control2 = new Control();
			CreateSideBar(control2);
			ReplacePlaceHolder(wizardLayoutContainer, control, control2);
		}
		ITemplate template = HeaderTemplate;
		if (template != null)
		{
			control = wizardLayoutContainer.FindControl(HeaderPlaceholderId);
			if (control == null)
			{
				throw MakeLayoutException("header", HeaderPlaceholderId, " when HeaderTemplate is set");
			}
			control2 = new Control();
			template.InstantiateIn(control2);
			ReplacePlaceHolder(wizardLayoutContainer, control, control2);
		}
		control = wizardLayoutContainer.FindControl(WizardStepPlaceholderId);
		if (control == null)
		{
			throw MakeLayoutException("step", WizardStepPlaceholderId);
		}
		customNavigation = null;
		multiView = new MultiView();
		foreach (View item in wizardSteps)
		{
			if (item is TemplatedWizardStep)
			{
				InstantiateTemplateStep((TemplatedWizardStep)item);
			}
			multiView.Views.Add(item);
		}
		multiView.ActiveViewIndex = ActiveStepIndex;
		ReplacePlaceHolder(wizardLayoutContainer, control, multiView);
		control = wizardLayoutContainer.FindControl(NavigationPlaceholderId);
		if (control == null)
		{
			throw MakeLayoutException("navigation", NavigationPlaceholderId);
		}
		Table obj = new Table
		{
			CellSpacing = 5,
			CellPadding = 5
		};
		TableRow tableRow = new TableRow();
		TableCell cell = new TableCell
		{
			HorizontalAlign = HorizontalAlign.Right
		};
		control2 = new Control();
		CreateButtonBar(control2);
		tableRow.Cells.Add(cell);
		obj.Rows.Add(tableRow);
		ReplacePlaceHolder(wizardLayoutContainer, control, control2);
		wizardLayoutContainer.Visible = visible;
	}

	private void ReplacePlaceHolder(WebControl container, Control placeHolder, Control replacement)
	{
		ControlCollection controls = container.Controls;
		int index = controls.IndexOf(placeHolder);
		controls.Remove(placeHolder);
		controls.AddAt(index, replacement);
	}

	/// <summary>Creates the hierarchy of child controls that make up the control.</summary>
	/// <exception cref="T:System.InvalidOperationException">The sidebar template does not contain a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</exception>
	protected virtual void CreateControlHierarchy()
	{
		ITemplate layoutTemplate = LayoutTemplate;
		if (layoutTemplate != null)
		{
			CreateControlHierarchy_LayoutTemplate(layoutTemplate);
			return;
		}
		styles.Clear();
		wizardTable = new ContainedTable(this);
		Table table = wizardTable;
		if (DisplaySideBar)
		{
			table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.Height = new Unit("100%");
			table.Width = new Unit("100%");
			TableRow tableRow = new TableRow();
			TableCellNamingContainer tableCellNamingContainer = new TableCellNamingContainer(SkipLinkText, ClientID);
			tableCellNamingContainer.ID = "SideBarContainer";
			tableCellNamingContainer.ControlStyle.Height = Unit.Percentage(100.0);
			CreateSideBar(tableCellNamingContainer);
			tableRow.Cells.Add(tableCellNamingContainer);
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(table);
			tableCell.Height = new Unit("100%");
			tableRow.Cells.Add(tableCell);
			wizardTable.Rows.Add(tableRow);
		}
		AddHeaderRow(table);
		TableRow tableRow2 = new TableRow();
		TableCell tableCell2 = new TableCell();
		customNavigation = null;
		multiView = new MultiView();
		foreach (View wizardStep in WizardSteps)
		{
			if (wizardStep is TemplatedWizardStep)
			{
				InstantiateTemplateStep((TemplatedWizardStep)wizardStep);
			}
			multiView.Views.Add(wizardStep);
		}
		multiView.ActiveViewIndex = ActiveStepIndex;
		RegisterApplyStyle(tableCell2, StepStyle);
		tableCell2.Controls.Add(multiView);
		tableRow2.Cells.Add(tableCell2);
		tableRow2.Height = new Unit("100%");
		table.Rows.Add(tableRow2);
		TableRow tableRow3 = new TableRow();
		_navigationCell = new TableCell();
		_navigationCell.HorizontalAlign = HorizontalAlign.Right;
		RegisterApplyStyle(_navigationCell, NavigationStyle);
		CreateButtonBar(_navigationCell);
		tableRow3.Cells.Add(_navigationCell);
		table.Rows.Add(tableRow3);
		Controls.SetReadonly(readOnly: false);
		Controls.Add(wizardTable);
		Controls.SetReadonly(readOnly: true);
	}

	internal virtual void InstantiateTemplateStep(TemplatedWizardStep step)
	{
		BaseWizardContainer baseWizardContainer = new BaseWizardContainer();
		if (step.ContentTemplate != null)
		{
			step.ContentTemplate.InstantiateIn(baseWizardContainer.InnerCell);
		}
		step.ContentTemplateContainer = baseWizardContainer;
		step.Controls.Clear();
		step.Controls.Add(baseWizardContainer);
		BaseWizardNavigationContainer baseWizardNavigationContainer = new BaseWizardNavigationContainer();
		if (step.CustomNavigationTemplate != null)
		{
			step.CustomNavigationTemplate.InstantiateIn(baseWizardNavigationContainer);
			RegisterCustomNavigation(step, baseWizardNavigationContainer);
		}
		step.CustomNavigationTemplateContainer = baseWizardNavigationContainer;
	}

	internal void RegisterCustomNavigation(TemplatedWizardStep step, BaseWizardNavigationContainer customNavigationTemplateContainer)
	{
		if (customNavigation == null)
		{
			customNavigation = new Hashtable();
		}
		customNavigation[step] = customNavigationTemplateContainer;
	}

	private void CreateButtonBar(Control container)
	{
		if (customNavigation != null && customNavigation.Values.Count > 0)
		{
			int num = 0;
			foreach (Control value in customNavigation.Values)
			{
				value.ID = "CustomNavigationTemplateContainerID" + num++;
				container.Controls.Add(value);
			}
		}
		_startNavContainer = new StartNavigationContainer(this);
		_startNavContainer.ID = "StartNavigationTemplateContainerID";
		if (startNavigationTemplate != null)
		{
			startNavigationTemplate.InstantiateIn(_startNavContainer);
		}
		else
		{
			AddNavButtonsTable(_startNavContainer, out var row);
			AddButtonCell(row, CreateButtonSet(StartNextButtonIDShort, MoveNextCommandName));
			AddButtonCell(row, CreateButtonSet(CancelButtonIDShort, CancelCommandName, causesValidation: false));
			_startNavContainer.ConfirmDefaultTemplate();
		}
		container.Controls.Add(_startNavContainer);
		_stepNavContainer = new StepNavigationContainer(this);
		_stepNavContainer.ID = "StepNavigationTemplateContainerID";
		if (stepNavigationTemplate != null)
		{
			stepNavigationTemplate.InstantiateIn(_stepNavContainer);
		}
		else
		{
			AddNavButtonsTable(_stepNavContainer, out var row2);
			AddButtonCell(row2, CreateButtonSet(StepPreviousButtonIDShort, MovePreviousCommandName, causesValidation: false));
			AddButtonCell(row2, CreateButtonSet(StepNextButtonIDShort, MoveNextCommandName));
			AddButtonCell(row2, CreateButtonSet(CancelButtonIDShort, CancelCommandName, causesValidation: false));
			_stepNavContainer.ConfirmDefaultTemplate();
		}
		container.Controls.Add(_stepNavContainer);
		_finishNavContainer = new FinishNavigationContainer(this);
		_finishNavContainer.ID = "FinishNavigationTemplateContainerID";
		if (finishNavigationTemplate != null)
		{
			finishNavigationTemplate.InstantiateIn(_finishNavContainer);
		}
		else
		{
			AddNavButtonsTable(_finishNavContainer, out var row3);
			AddButtonCell(row3, CreateButtonSet(FinishPreviousButtonIDShort, MovePreviousCommandName, causesValidation: false));
			AddButtonCell(row3, CreateButtonSet(FinishButtonIDShort, MoveCompleteCommandName));
			AddButtonCell(row3, CreateButtonSet(CancelButtonIDShort, CancelCommandName, causesValidation: false));
			_finishNavContainer.ConfirmDefaultTemplate();
		}
		container.Controls.Add(_finishNavContainer);
	}

	private static void AddNavButtonsTable(BaseWizardNavigationContainer container, out TableRow row)
	{
		Table table = new Table();
		table.CellPadding = 5;
		table.CellSpacing = 5;
		row = new TableRow();
		table.Rows.Add(row);
		container.Controls.Add(table);
	}

	private Control[] CreateButtonSet(string id, string command)
	{
		return CreateButtonSet(id, command, causesValidation: true, null);
	}

	private Control[] CreateButtonSet(string id, string command, bool causesValidation)
	{
		return CreateButtonSet(id, command, causesValidation, null);
	}

	internal Control[] CreateButtonSet(string id, string command, bool causesValidation, string validationGroup)
	{
		return new Control[3]
		{
			CreateButton(id + ButtonType.Button, command, ButtonType.Button, causesValidation, validationGroup),
			CreateButton(id + ButtonType.Image, command, ButtonType.Image, causesValidation, validationGroup),
			CreateButton(id + ButtonType.Link, command, ButtonType.Link, causesValidation, validationGroup)
		};
	}

	private Control CreateButton(string id, string command, ButtonType type, bool causesValidation, string validationGroup)
	{
		WebControl webControl = type switch
		{
			ButtonType.Button => CreateStandardButton(), 
			ButtonType.Image => CreateImageButton(null), 
			ButtonType.Link => CreateLinkButton(), 
			_ => throw new ArgumentOutOfRangeException("type"), 
		};
		webControl.ID = id;
		webControl.EnableTheming = false;
		((IButtonControl)webControl).CommandName = command;
		((IButtonControl)webControl).CausesValidation = causesValidation;
		if (!string.IsNullOrEmpty(validationGroup))
		{
			((IButtonControl)webControl).ValidationGroup = validationGroup;
		}
		RegisterApplyStyle(webControl, NavigationButtonStyle);
		return webControl;
	}

	private WebControl CreateStandardButton()
	{
		return new Button();
	}

	private WebControl CreateImageButton(string imageUrl)
	{
		return new ImageButton
		{
			ImageUrl = imageUrl
		};
	}

	private WebControl CreateLinkButton()
	{
		return new LinkButton();
	}

	private void AddButtonCell(TableRow row, params Control[] controls)
	{
		TableCell tableCell = new TableCell();
		tableCell.HorizontalAlign = HorizontalAlign.Right;
		for (int i = 0; i < controls.Length; i++)
		{
			tableCell.Controls.Add(controls[i]);
		}
		row.Cells.Add(tableCell);
	}

	private void CreateSideBar(Control container)
	{
		if (container is WebControl control)
		{
			RegisterApplyStyle(control, SideBarStyle);
		}
		if (sideBarTemplate != null)
		{
			sideBarTemplate.InstantiateIn(container);
			stepDatalist = container.FindControl(DataListID) as DataList;
			if (stepDatalist == null)
			{
				throw new InvalidOperationException("The side bar template must contain a DataList control with id '" + DataListID + "'.");
			}
			stepDatalist.ItemDataBound += StepDatalistItemDataBound;
		}
		else
		{
			stepDatalist = new DataList();
			stepDatalist.ID = DataListID;
			stepDatalist.SelectedItemStyle.Font.Bold = true;
			stepDatalist.ItemTemplate = SideBarItemTemplate;
			container.Controls.Add(stepDatalist);
		}
		stepDatalist.ItemCommand += StepDatalistItemCommand;
		stepDatalist.CellSpacing = 0;
		stepDatalist.DataSource = WizardSteps;
		stepDatalist.SelectedIndex = ActiveStepIndex;
		stepDatalist.DataBind();
	}

	private void StepDatalistItemCommand(object sender, DataListCommandEventArgs e)
	{
		WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(ActiveStepIndex, Convert.ToInt32(e.CommandArgument));
		OnSideBarButtonClick(wizardNavigationEventArgs);
		if (!wizardNavigationEventArgs.Cancel)
		{
			ActiveStepIndex = wizardNavigationEventArgs.NextStepIndex;
		}
	}

	private void StepDatalistItemDataBound(object sender, DataListItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.SelectedItem)
		{
			IButtonControl buttonControl = (IButtonControl)e.Item.FindControl(SideBarButtonID);
			if (buttonControl == null)
			{
				throw new InvalidOperationException("SideBarList control must contain an IButtonControl with ID " + SideBarButtonID + " in every item template, this maybe include ItemTemplate, EditItemTemplate, SelectedItemTemplate or AlternatingItemTemplate if they exist.");
			}
			WizardStepBase wizardStepBase = (WizardStepBase)e.Item.DataItem;
			if (buttonControl is Button)
			{
				((Button)buttonControl).UseSubmitBehavior = false;
			}
			buttonControl.CommandName = MoveToCommandName;
			buttonControl.CommandArgument = WizardSteps.IndexOf(wizardStepBase).ToString();
			buttonControl.Text = wizardStepBase.Name;
			if (wizardStepBase.StepType == WizardStepType.Complete && buttonControl is WebControl)
			{
				((WebControl)buttonControl).Enabled = false;
			}
		}
	}

	private void AddHeaderRow(Table table)
	{
		TableRow tableRow = new TableRow();
		_headerCell = new WizardHeaderCell();
		_headerCell.ID = "HeaderContainer";
		RegisterApplyStyle(_headerCell, HeaderStyle);
		if (headerTemplate != null)
		{
			headerTemplate.InstantiateIn(_headerCell);
			_headerCell.ConfirmInitState();
		}
		tableRow.Cells.Add(_headerCell);
		table.Rows.Add(tableRow);
	}

	internal void RegisterApplyStyle(WebControl control, Style style)
	{
		styles.Add(new object[2] { control, style });
	}

	/// <summary>Creates control style.</summary>
	protected override Style CreateControlStyle()
	{
		return new TableStyle
		{
			CellPadding = 0,
			CellSpacing = 0
		};
	}

	/// <summary>Gets the design mode state.</summary>
	protected override IDictionary GetDesignModeState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Restores control state information.</summary>
	/// <param name="state">The control state to be restored.</param>
	protected internal override void LoadControlState(object state)
	{
		if (state != null)
		{
			object[] array = (object[])state;
			base.LoadControlState(array[0]);
			activeStepIndex = (int)array[1];
			history = (ArrayList)array[2];
		}
	}

	/// <summary>Save the control state.</summary>
	/// <returns>The control state.</returns>
	protected internal override object SaveControlState()
	{
		if (GetHistory().Count == 0 || (int)history[0] != ActiveStepIndex)
		{
			history.Insert(0, ActiveStepIndex);
		}
		object obj = base.SaveControlState();
		return new object[3] { obj, activeStepIndex, history };
	}

	/// <summary>Loads view-state information.</summary>
	/// <param name="savedState">The control state to be restored.</param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> value.</exception>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			base.LoadViewState((object)null);
			return;
		}
		object[] array = (object[])savedState;
		base.LoadViewState(array[0]);
		if (array[1] != null)
		{
			((IStateManager)StepStyle).LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			((IStateManager)SideBarStyle).LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			((IStateManager)HeaderStyle).LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			((IStateManager)NavigationStyle).LoadViewState(array[4]);
		}
		if (array[5] != null)
		{
			((IStateManager)SideBarButtonStyle).LoadViewState(array[5]);
		}
		if (array[6] != null)
		{
			((IStateManager)CancelButtonStyle).LoadViewState(array[6]);
		}
		if (array[7] != null)
		{
			((IStateManager)FinishCompleteButtonStyle).LoadViewState(array[7]);
		}
		if (array[8] != null)
		{
			((IStateManager)FinishPreviousButtonStyle).LoadViewState(array[8]);
		}
		if (array[9] != null)
		{
			((IStateManager)StartNextButtonStyle).LoadViewState(array[9]);
		}
		if (array[10] != null)
		{
			((IStateManager)StepNextButtonStyle).LoadViewState(array[10]);
		}
		if (array[11] != null)
		{
			((IStateManager)StepPreviousButtonStyle).LoadViewState(array[11]);
		}
		if (array[12] != null)
		{
			((IStateManager)NavigationButtonStyle).LoadViewState(array[12]);
		}
		if (array[13] != null)
		{
			base.ControlStyle.LoadViewState(array[13]);
		}
	}

	/// <summary>Saves the view state.</summary>
	/// <returns>The view state.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[14]
		{
			base.SaveViewState(),
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null
		};
		if (stepStyle != null)
		{
			array[1] = ((IStateManager)stepStyle).SaveViewState();
		}
		if (sideBarStyle != null)
		{
			array[2] = ((IStateManager)sideBarStyle).SaveViewState();
		}
		if (headerStyle != null)
		{
			array[3] = ((IStateManager)headerStyle).SaveViewState();
		}
		if (navigationStyle != null)
		{
			array[4] = ((IStateManager)navigationStyle).SaveViewState();
		}
		if (sideBarButtonStyle != null)
		{
			array[5] = ((IStateManager)sideBarButtonStyle).SaveViewState();
		}
		if (cancelButtonStyle != null)
		{
			array[6] = ((IStateManager)cancelButtonStyle).SaveViewState();
		}
		if (finishCompleteButtonStyle != null)
		{
			array[7] = ((IStateManager)finishCompleteButtonStyle).SaveViewState();
		}
		if (finishPreviousButtonStyle != null)
		{
			array[8] = ((IStateManager)finishPreviousButtonStyle).SaveViewState();
		}
		if (startNextButtonStyle != null)
		{
			array[9] = ((IStateManager)startNextButtonStyle).SaveViewState();
		}
		if (stepNextButtonStyle != null)
		{
			array[10] = ((IStateManager)stepNextButtonStyle).SaveViewState();
		}
		if (stepPreviousButtonStyle != null)
		{
			array[11] = ((IStateManager)stepPreviousButtonStyle).SaveViewState();
		}
		if (navigationButtonStyle != null)
		{
			array[12] = ((IStateManager)navigationButtonStyle).SaveViewState();
		}
		if (base.ControlStyleCreated)
		{
			array[13] = base.ControlStyle.SaveViewState();
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Tracks view state.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (stepStyle != null)
		{
			((IStateManager)stepStyle).TrackViewState();
		}
		if (sideBarStyle != null)
		{
			((IStateManager)sideBarStyle).TrackViewState();
		}
		if (headerStyle != null)
		{
			((IStateManager)headerStyle).TrackViewState();
		}
		if (navigationStyle != null)
		{
			((IStateManager)navigationStyle).TrackViewState();
		}
		if (sideBarButtonStyle != null)
		{
			((IStateManager)sideBarButtonStyle).TrackViewState();
		}
		if (cancelButtonStyle != null)
		{
			((IStateManager)cancelButtonStyle).TrackViewState();
		}
		if (finishCompleteButtonStyle != null)
		{
			((IStateManager)finishCompleteButtonStyle).TrackViewState();
		}
		if (finishPreviousButtonStyle != null)
		{
			((IStateManager)finishPreviousButtonStyle).TrackViewState();
		}
		if (startNextButtonStyle != null)
		{
			((IStateManager)startNextButtonStyle).TrackViewState();
		}
		if (stepNextButtonStyle != null)
		{
			((IStateManager)stepNextButtonStyle).TrackViewState();
		}
		if (stepPreviousButtonStyle != null)
		{
			((IStateManager)stepPreviousButtonStyle).TrackViewState();
		}
		if (navigationButtonStyle != null)
		{
			((IStateManager)navigationButtonStyle).TrackViewState();
		}
		if (base.ControlStyleCreated)
		{
			base.ControlStyle.TrackViewState();
		}
	}

	/// <summary>Registers a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventHandler" /> class for the specified <see cref="T:System.Web.UI.WebControls.IButtonControl" /> object.</summary>
	/// <param name="button">The <see cref="T:System.Web.UI.WebControls.IButtonControl" /> for which the new instance of <see cref="T:System.Web.UI.WebControls.CommandEventHandler" /> is registered.</param>
	protected internal void RegisterCommandEvents(IButtonControl button)
	{
		button.Command += ProcessCommand;
	}

	private void ProcessCommand(object sender, CommandEventArgs args)
	{
		if (sender is Control control)
		{
			switch (control.ID)
			{
			case "CancelButton":
				ProcessEvent("Cancel", null);
				return;
			case "FinishButton":
				ProcessEvent("MoveComplete", null);
				return;
			case "StepPreviousButton":
			case "FinishPreviousButton":
				ProcessEvent("MovePrevious", null);
				return;
			case "StartNextButton":
			case "StepNextButton":
				ProcessEvent("MoveNext", null);
				return;
			}
		}
		ProcessEvent(args.CommandName, args.CommandArgument as string);
	}

	/// <summary>Determines whether the event for the server control is passed up the page’s user interface server control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">Contains event data.</param>
	/// <returns>
	///     <see langword="true" /> if the event for the server control is passed up the page’s user interface server control hierarchy; otherwise, <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (e is CommandEventArgs commandEventArgs)
		{
			ProcessEvent(commandEventArgs.CommandName, commandEventArgs.CommandArgument as string);
			return true;
		}
		return base.OnBubbleEvent(source, e);
	}

	private void ProcessEvent(string commandName, string commandArg)
	{
		switch (commandName)
		{
		case "Cancel":
			if (CancelDestinationPageUrl.Length > 0)
			{
				Context.Response.Redirect(CancelDestinationPageUrl);
			}
			else
			{
				OnCancelButtonClick(EventArgs.Empty);
			}
			break;
		case "MoveComplete":
		{
			int num3 = -1;
			for (int i = 0; i < WizardSteps.Count; i++)
			{
				if (WizardSteps[i].StepType == WizardStepType.Complete)
				{
					num3 = i;
					break;
				}
			}
			if (num3 == -1 && ActiveStepIndex == WizardSteps.Count - 1)
			{
				num3 = ActiveStepIndex;
			}
			WizardNavigationEventArgs wizardNavigationEventArgs2 = new WizardNavigationEventArgs(ActiveStepIndex, num3);
			OnFinishButtonClick(wizardNavigationEventArgs2);
			if (FinishDestinationPageUrl.Length > 0)
			{
				Context.Response.Redirect(FinishDestinationPageUrl);
			}
			else if (num3 != -1 && !wizardNavigationEventArgs2.Cancel)
			{
				ActiveStepIndex = num3;
			}
			break;
		}
		case "MoveNext":
			if (ActiveStepIndex < WizardSteps.Count - 1)
			{
				WizardNavigationEventArgs wizardNavigationEventArgs3 = new WizardNavigationEventArgs(ActiveStepIndex, ActiveStepIndex + 1);
				int num4 = ActiveStepIndex;
				OnNextButtonClick(wizardNavigationEventArgs3);
				if (!wizardNavigationEventArgs3.Cancel && num4 == activeStepIndex)
				{
					ActiveStepIndex++;
				}
			}
			break;
		case "MovePrevious":
		{
			if (ActiveStepIndex <= 0)
			{
				break;
			}
			WizardNavigationEventArgs wizardNavigationEventArgs = new WizardNavigationEventArgs(ActiveStepIndex, ActiveStepIndex - 1);
			int num2 = ActiveStepIndex;
			OnPreviousButtonClick(wizardNavigationEventArgs);
			if (!wizardNavigationEventArgs.Cancel)
			{
				if (num2 == activeStepIndex)
				{
					ActiveStepIndex--;
				}
				if (history != null && activeStepIndex < num2)
				{
					history.Remove(num2);
				}
			}
			break;
		}
		case "Move":
		{
			int num = int.Parse(commandArg);
			ActiveStepIndex = num;
			break;
		}
		}
	}

	internal void UpdateViews()
	{
		base.ChildControlsCreated = false;
	}

	/// <summary>Renders the control to the specified writer.</summary>
	/// <param name="writer">The HTML writer.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		PrepareControlHierarchy();
		if (LayoutTemplate == null)
		{
			wizardTable.Render(writer);
		}
		else
		{
			RenderChildren(writer);
		}
	}

	private void PrepareControlHierarchy()
	{
		if (LayoutTemplate == null)
		{
			if (!_headerCell.Initialized)
			{
				if (string.IsNullOrEmpty(HeaderText))
				{
					_headerCell.Parent.Visible = false;
				}
				else
				{
					_headerCell.Text = HeaderText;
				}
			}
			if (ActiveStep.StepType == WizardStepType.Complete)
			{
				_headerCell.Parent.Visible = false;
			}
		}
		else
		{
			WizardStepCollection wizardSteps = WizardSteps;
			if (wizardSteps == null || wizardSteps.Count == 0)
			{
				return;
			}
		}
		if (stepDatalist != null)
		{
			stepDatalist.SelectedIndex = ActiveStepIndex;
			stepDatalist.DataBind();
			if (ActiveStep.StepType == WizardStepType.Complete)
			{
				stepDatalist.NamingContainer.Visible = false;
			}
		}
		if (ActiveStep is TemplatedWizardStep { ContentTemplateContainer: BaseWizardContainer contentTemplateContainer })
		{
			contentTemplateContainer.PrepareControlHierarchy();
		}
		if (customNavigation != null)
		{
			foreach (Control value in customNavigation.Values)
			{
				value.Visible = false;
			}
		}
		_startNavContainer.Visible = false;
		_stepNavContainer.Visible = false;
		_finishNavContainer.Visible = false;
		BaseWizardNavigationContainer currentNavContainer = GetCurrentNavContainer();
		if (currentNavContainer == null)
		{
			if (_navigationCell != null)
			{
				_navigationCell.Parent.Visible = false;
			}
		}
		else
		{
			currentNavContainer.Visible = true;
			currentNavContainer.PrepareControlHierarchy();
			if (_navigationCell != null && !currentNavContainer.Visible)
			{
				_navigationCell.Parent.Visible = false;
			}
		}
		foreach (object[] style in styles)
		{
			((WebControl)style[0]).ApplyStyle((Style)style[1]);
		}
	}

	private BaseWizardNavigationContainer GetCurrentNavContainer()
	{
		if (customNavigation != null && customNavigation[ActiveStep] != null)
		{
			return (BaseWizardNavigationContainer)customNavigation[ActiveStep];
		}
		return GetStepType(ActiveStep, ActiveStepIndex) switch
		{
			WizardStepType.Start => _startNavContainer, 
			WizardStepType.Step => _stepNavContainer, 
			WizardStepType.Finish => _finishNavContainer, 
			_ => null, 
		};
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Wizard" /> class.</summary>
	public Wizard()
	{
	}

	static Wizard()
	{
		ActiveStepChanged = new object();
		CancelButtonClick = new object();
		FinishButtonClick = new object();
		NextButtonClick = new object();
		PreviousButtonClick = new object();
		SideBarButtonClick = new object();
	}
}
