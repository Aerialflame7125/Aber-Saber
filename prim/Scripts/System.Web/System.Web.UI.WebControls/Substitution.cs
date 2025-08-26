using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Specifies a section on an output-cached Web page that is exempt from caching. At this location, dynamic content is retrieved and substituted for the <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</summary>
[DefaultProperty("MethodName")]
[ParseChildren(true)]
[PersistChildren(false)]
[Designer("System.Web.UI.Design.WebControls.SubstitutionDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Substitution : Control
{
	/// <summary>Gets or sets the name of the callback method to invoke when the <see cref="T:System.Web.UI.WebControls.Substitution" /> control executes.</summary>
	/// <returns>A string that represents the name of the method to invoke when the <see cref="T:System.Web.UI.WebControls.Substitution" /> control executes.</returns>
	[DefaultValue("")]
	[WebCategory("Behavior")]
	public virtual string MethodName
	{
		get
		{
			string text = ViewState["MethodName"] as string;
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			ViewState["MethodName"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Substitution" /> class.</summary>
	public Substitution()
	{
	}

	/// <summary>Returns an <see cref="T:System.Web.UI.EmptyControlCollection" /> object, indicating that the <see cref="T:System.Web.UI.WebControls.Substitution" /> control does not support child controls.</summary>
	/// <returns>An <see cref="T:System.Web.UI.EmptyControlCollection" />, indicating that the control does not support child controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	/// <exception cref="T:System.Web.HttpException">The parent control or master page is cached.</exception>
	[MonoTODO("Why override?")]
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the clien</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		string methodName = MethodName;
		if (methodName.Length != 0)
		{
			TemplateControl templateControl = base.TemplateControl;
			if (templateControl != null)
			{
				(Context?.Response)?.WriteSubstitution(CreateCallback(methodName, templateControl));
			}
		}
	}

	private HttpResponseSubstitutionCallback CreateCallback(string method, TemplateControl tc)
	{
		try
		{
			return Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), tc.GetType(), method, ignoreCase: true, throwOnBindFailure: true) as HttpResponseSubstitutionCallback;
		}
		catch (Exception innerException)
		{
			throw new HttpException("Cannot find static method '" + method + "' matching HttpResponseSubstitutionCallback", innerException);
		}
	}
}
