using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Compilation;
using System.Web.Hosting;

namespace System.Web.UI;

/// <summary>Acts as a template and merging container for pages that are composed only of <see cref="T:System.Web.UI.WebControls.Content" /> controls and their respective child controls.</summary>
[ParseChildren(false)]
[ControlBuilder(typeof(MasterPageControlBuilder))]
public class MasterPage : UserControl
{
	private Hashtable definedContentTemplates = new Hashtable();

	private Hashtable templates = new Hashtable();

	private List<string> placeholders;

	private string parentMasterPageFile;

	private MasterPage parentMasterPage;

	/// <summary>Gets a list of <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> controls that the master page uses to define different content regions.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> controls that the master page uses as placeholders for <see cref="T:System.Web.UI.WebControls.Content" /> controls found in content pages.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal IList ContentPlaceHolders
	{
		get
		{
			if (placeholders == null)
			{
				placeholders = new List<string>();
			}
			return placeholders;
		}
	}

	/// <summary>Gets a list of content controls that are associated with the master page. </summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> of content controls associated with the master page. </returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal IDictionary ContentTemplates => templates;

	/// <summary>Gets or sets the name of the master page that contains the current content.</summary>
	/// <returns>The name of the master page that is the parent of the current master page; otherwise, <see langword="null" />, if the current master page has no parent.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.MasterPage.MasterPageFile" /> property can only be set in or before the <see cref="E:System.Web.UI.Page.PreInit" /> event.</exception>
	[DefaultValue("")]
	public string MasterPageFile
	{
		get
		{
			return parentMasterPageFile;
		}
		set
		{
			parentMasterPageFile = value;
			parentMasterPage = null;
		}
	}

	/// <summary>Gets the parent master page of the current master in nested master pages scenarios.</summary>
	/// <returns>The master page that is the parent of the current master page; otherwise, <see langword="null" />, if the current master page has no parent.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public MasterPage Master
	{
		get
		{
			if (parentMasterPage == null && parentMasterPageFile != null)
			{
				parentMasterPage = CreateMasterPage(this, Context, parentMasterPageFile, definedContentTemplates);
			}
			return parentMasterPage;
		}
	}

	/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.Content" /> control to the <see cref="P:System.Web.UI.MasterPage.ContentTemplates" /> dictionary.</summary>
	/// <param name="templateName">A unique name for the <see cref="T:System.Web.UI.WebControls.Content" />.</param>
	/// <param name="template">The <see cref="T:System.Web.UI.WebControls.Content" />.</param>
	/// <exception cref="T:System.Web.HttpException">A <see cref="T:System.Web.UI.WebControls.Content" /> control with the same name already exists in the <see cref="P:System.Web.UI.MasterPage.ContentTemplates" /> dictionary.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal void AddContentTemplate(string templateName, ITemplate template)
	{
		if (definedContentTemplates.ContainsKey(templateName))
		{
			throw new HttpException("Multiple contents applied to " + templateName);
		}
		definedContentTemplates[templateName] = template;
	}

	/// <summary>Provides a method to set the current template control to a page that owns the master page.</summary>
	/// <param name="contentPlaceHolder">The control that represents the container of the content.</param>
	/// <param name="template">The <see cref="T:System.Web.UI.WebControls.Content" /> instance to use.</param>
	public void InstantiateInContentPlaceHolder(Control contentPlaceHolder, ITemplate template)
	{
		if (contentPlaceHolder == null || template == null)
		{
			throw new NullReferenceException();
		}
		if (contentPlaceHolder != null)
		{
			template?.InstantiateIn(contentPlaceHolder);
		}
	}

	internal static MasterPage CreateMasterPage(TemplateControl owner, HttpContext context, string masterPageFile, IDictionary contentTemplateCollection)
	{
		HttpRequest request = context.Request;
		if (request != null)
		{
			masterPageFile = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(request.CurrentExecutionFilePath, masterPageFile);
		}
		if (!(BuildManager.CreateInstanceFromVirtualPath(masterPageFile, typeof(MasterPage)) is MasterPage masterPage))
		{
			throw new HttpException("Failed to create MasterPage instance for '" + masterPageFile + "'.");
		}
		if (contentTemplateCollection != null)
		{
			foreach (string key in contentTemplateCollection.Keys)
			{
				if (masterPage.ContentTemplates[key] == null)
				{
					masterPage.ContentTemplates[key] = contentTemplateCollection[key];
				}
			}
		}
		masterPage.Page = owner.Page;
		masterPage.InitializeAsUserControlInternal();
		List<string> list = masterPage.placeholders;
		if (contentTemplateCollection != null && list != null && list.Count > 0)
		{
			foreach (string key2 in contentTemplateCollection.Keys)
			{
				if (!list.Contains(key2.ToLowerInvariant()))
				{
					throw new HttpException($"Cannot find ContentPlaceHolder '{key2}' in the master page '{masterPageFile}'");
				}
			}
		}
		return masterPage;
	}

	internal static void ApplyMasterPageRecursive(string currentFilePath, VirtualPathProvider vpp, MasterPage master, Dictionary<string, bool> appliedMasterPageFiles)
	{
		string masterPageFile = master.MasterPageFile;
		if (!string.IsNullOrEmpty(masterPageFile))
		{
			masterPageFile = vpp.CombineVirtualPaths(currentFilePath, masterPageFile);
			if (appliedMasterPageFiles.ContainsKey(masterPageFile))
			{
				throw new HttpException("circular dependency in master page files detected");
			}
			MasterPage master2 = master.Master;
			if (master2 != null)
			{
				master.Controls.Clear();
				master.Controls.Add(master2);
				appliedMasterPageFiles.Add(masterPageFile, value: true);
				ApplyMasterPageRecursive(currentFilePath, vpp, master2, appliedMasterPageFiles);
			}
		}
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.MasterPage" /> class.</summary>
	public MasterPage()
	{
	}
}
