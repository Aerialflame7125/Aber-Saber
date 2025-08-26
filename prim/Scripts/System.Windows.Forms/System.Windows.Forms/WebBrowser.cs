using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Mono.WebBrowser;

namespace System.Windows.Forms;

/// <summary>Enables the user to navigate Web pages inside your form. </summary>
/// <filterpriority>1</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Docking(DockingBehavior.AutoDock)]
[DefaultEvent("DocumentCompleted")]
[DefaultProperty("Url")]
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.WebBrowserDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class WebBrowser : WebBrowserBase
{
	/// <summary>Represents the host window of a <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	[System.MonoTODO("Stub, not implemented")]
	[ComVisible(false)]
	protected class WebBrowserSite : WebBrowserSiteBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> class. </summary>
		/// <param name="host">The <see cref="T:System.Windows.Forms.WebBrowser" /></param>
		[System.MonoTODO("Stub, not implemented")]
		public WebBrowserSite(WebBrowser host)
		{
		}
	}

	private bool allowNavigation;

	private bool allowWebBrowserDrop = true;

	private bool isWebBrowserContextMenuEnabled;

	private object objectForScripting;

	private bool webBrowserShortcutsEnabled;

	private bool scrollbarsEnabled = true;

	private WebBrowserReadyState readyState;

	private HtmlDocument document;

	private WebBrowserEncryptionLevel securityLevel;

	private Stream data;

	private bool isStreamSet;

	private string url;

	/// <summary>Gets or sets a value indicating whether the control can navigate to another page after its initial page has been loaded.</summary>
	/// <returns>true if the control can navigate to another page; otherwise, false.</returns>
	[DefaultValue(true)]
	public bool AllowNavigation
	{
		get
		{
			return allowNavigation;
		}
		set
		{
			allowNavigation = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to documents that are dropped onto it.</summary>
	/// <returns>true if the control accepts documents that are dropped onto it; otherwise, false. The default is true.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool AllowWebBrowserDrop
	{
		get
		{
			return allowWebBrowserDrop;
		}
		set
		{
			allowWebBrowserDrop = value;
		}
	}

	/// <summary>Gets a value indicating whether a previous page in navigation history is available, which allows the <see cref="M:System.Windows.Forms.WebBrowser.GoBack" /> method to succeed.</summary>
	/// <returns>true if the control can navigate backward; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanGoBack => base.WebHost.Navigation.CanGoBack;

	/// <summary>Gets a value indicating whether a subsequent page in navigation history is available, which allows the <see cref="M:System.Windows.Forms.WebBrowser.GoForward" /> method to succeed.</summary>
	/// <returns>true if the control can navigate forward; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool CanGoForward => base.WebHost.Navigation.CanGoForward;

	/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlDocument" /> representing the Web page currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlDocument" /> representing the current page, or null if no page is loaded.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HtmlDocument Document
	{
		get
		{
			if (document == null && documentReady)
			{
				document = new HtmlDocument(this, base.WebHost);
			}
			return document;
		}
	}

	/// <summary>Gets or sets a stream containing the contents of the Web page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> containing the contents of the current Web page, or null if no page is loaded. The default is null.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public Stream DocumentStream
	{
		get
		{
			if (base.WebHost.Document == null || base.WebHost.Document.DocumentElement == null)
			{
				return null;
			}
			return null;
		}
		set
		{
			if (!allowNavigation)
			{
				Url = new Uri("about:blank");
				data = value;
				isStreamSet = true;
			}
		}
	}

	/// <summary>Gets or sets the HTML contents of the page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>The HTML text of the displayed page, or the empty string ("") if no document is loaded.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string DocumentText
	{
		get
		{
			if (base.WebHost.Document == null || base.WebHost.Document.DocumentElement == null)
			{
				return string.Empty;
			}
			return base.WebHost.Document.DocumentElement.OuterHTML;
		}
		set
		{
			if (base.WebHost.Document != null && base.WebHost.Document.DocumentElement != null)
			{
				base.WebHost.Document.DocumentElement.OuterHTML = value;
			}
		}
	}

	/// <summary>Gets the title of the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>The title of the current document, or the empty string ("") if no document is loaded.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string DocumentTitle
	{
		get
		{
			if (document != null)
			{
				return document.Title;
			}
			return string.Empty;
		}
	}

	/// <summary>Gets the type of the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>The type of the current document.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string DocumentType
	{
		get
		{
			if (document != null)
			{
				return document.DocType;
			}
			return string.Empty;
		}
	}

	/// <summary>Gets a value indicating the encryption method used by the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.WebBrowserEncryptionLevel" /> values.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public WebBrowserEncryptionLevel EncryptionLevel => securityLevel;

	/// <summary>Gets a value indicating whether the control or any of its child windows has input focus.</summary>
	/// <returns>true if the control or any of its child windows has input focus; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override bool Focused => base.Focused;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control is currently loading a new document.</summary>
	/// <returns>true if the control is busy loading a document; otherwise, false.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsBusy => !documentReady;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control is in offline mode.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.WebBrowser" /> control is in offline mode; otherwise, false.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool IsOffline => base.WebHost.Offline;

	/// <summary>Gets or a sets a value indicating whether the shortcut menu of the <see cref="T:System.Windows.Forms.WebBrowser" /> control is enabled.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.WebBrowser" /> control shortcut menu is enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	[System.MonoTODO("Stub, not implemented")]
	public bool IsWebBrowserContextMenuEnabled
	{
		get
		{
			return isWebBrowserContextMenuEnabled;
		}
		set
		{
			isWebBrowserContextMenuEnabled = value;
		}
	}

	/// <summary>Gets or sets an object that can be accessed by scripting code that is contained within a Web page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>The object being made available to the scripting code.</returns>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is an instance of a non-public type.-or-The specified value when setting this property is an instance of a type that is not COM-visible. For more information, see <see cref="M:System.Runtime.InteropServices.Marshal.IsTypeVisibleFromCom(System.Type)" />.</exception>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[System.MonoTODO("Stub, not implemented")]
	public object ObjectForScripting
	{
		get
		{
			return objectForScripting;
		}
		set
		{
			objectForScripting = value;
		}
	}

	/// <summary>Gets a value indicating the current state of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.WebBrowserReadyState" /> values.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public WebBrowserReadyState ReadyState => readyState;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> displays dialog boxes such as script error messages.</summary>
	/// <returns>true if the control does not display its dialog boxes; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool ScriptErrorsSuppressed
	{
		get
		{
			return base.SuppressDialogs;
		}
		set
		{
			base.SuppressDialogs = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether scroll bars are displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>true if scroll bars are displayed in the control; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool ScrollBarsEnabled
	{
		get
		{
			return scrollbarsEnabled;
		}
		set
		{
			scrollbarsEnabled = value;
			if (document != null)
			{
				SetScrollbars();
			}
		}
	}

	/// <summary>Gets the status text of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>The status text.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string StatusText => status;

	/// <summary>Gets or sets the URL of the current document.</summary>
	/// <returns>A <see cref="T:System.Uri" /> representing the URL of the current document.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[TypeConverter(typeof(WebBrowserUriTypeConverter))]
	[DefaultValue(null)]
	[Bindable(true)]
	public Uri Url
	{
		get
		{
			if (url != null)
			{
				return new Uri(url);
			}
			if (base.WebHost.Document != null && base.WebHost.Document.Url != null)
			{
				return new Uri(base.WebHost.Document.Url);
			}
			return null;
		}
		set
		{
			url = null;
			Navigate(value);
		}
	}

	/// <summary>Gets the version of Internet Explorer installed.</summary>
	/// <returns>A <see cref="T:System.Version" /> object representing the version of Internet Explorer installed.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public Version Version
	{
		get
		{
			Assembly assembly = base.WebHost.GetType().Assembly;
			return assembly.GetName().Version;
		}
	}

	/// <summary>Gets or sets a value indicating whether keyboard shortcuts are enabled within the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>true if keyboard shortcuts are enabled within the control; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Stub, not implemented")]
	[DefaultValue(true)]
	public bool WebBrowserShortcutsEnabled
	{
		get
		{
			return webBrowserShortcutsEnabled;
		}
		set
		{
			webBrowserShortcutsEnabled = value;
		}
	}

	/// <summary>Gets the default size of the control.</summary>
	/// <returns>Gets the default size of the control.</returns>
	protected override Size DefaultSize => base.DefaultSize;

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>
	///   <see cref="F:System.Windows.Forms.Padding.Empty" />
	/// </returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			base.Padding = value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.CanGoBack" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public event EventHandler CanGoBackChanged;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.CanGoForward" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public event EventHandler CanGoForwardChanged;

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control finishes loading a document.</summary>
	/// <filterpriority>1</filterpriority>
	public event WebBrowserDocumentCompletedEventHandler DocumentCompleted;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.DocumentTitle" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public event EventHandler DocumentTitleChanged;

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to or away from a Web site that uses encryption.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public event EventHandler EncryptionLevelChanged;

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control downloads a file.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler FileDownload;

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated to a new document and has begun loading it.</summary>
	/// <filterpriority>1</filterpriority>
	public event WebBrowserNavigatedEventHandler Navigated;

	/// <summary>Occurs before the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to a new document.</summary>
	/// <filterpriority>1</filterpriority>
	public event WebBrowserNavigatingEventHandler Navigating;

	/// <summary>Occurs before a new browser window is opened.</summary>
	/// <filterpriority>1</filterpriority>
	public event CancelEventHandler NewWindow;

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control has updated information on the download progress of a document it is navigating to.</summary>
	/// <filterpriority>1</filterpriority>
	public event WebBrowserProgressChangedEventHandler ProgressChanged;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.StatusText" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public event EventHandler StatusTextChanged;

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.WebBrowser.Padding" /> property changes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new event EventHandler PaddingChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowser" /> class.</summary>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.WebBrowser" /> control is hosted inside Internet Explorer.</exception>
	[System.MonoTODO("WebBrowser control is only supported on Linux/Windows. No support for OSX.")]
	public WebBrowser()
	{
	}

	/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the previous page in the navigation history, if one is available.</summary>
	/// <returns>true if the navigation succeeds; false if a previous page in the navigation history is not available.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool GoBack()
	{
		documentReady = false;
		document = null;
		return base.WebHost.Navigation.Back();
	}

	/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the next page in the navigation history, if one is available.</summary>
	/// <returns>true if the navigation succeeds; false if a subsequent page in the navigation history is not available.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool GoForward()
	{
		documentReady = false;
		document = null;
		return base.WebHost.Navigation.Forward();
	}

	/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the home page of the current user.</summary>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void GoHome()
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Home();
	}

	/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the previous document.</summary>
	/// <param name="urlString">The URL of the document to load.</param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Navigate(string urlString)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(urlString);
	}

	/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the previous document.</summary>
	/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load. </param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
	public void Navigate(Uri url)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(url.ToString());
	}

	/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into a new browser window or into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <param name="urlString">The URL of the document to load.</param>
	/// <param name="newWindow">true to load the document into a new browser window; false to load the document into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Navigate(string urlString, bool newWindow)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(urlString);
	}

	/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the contents of the Web page frame with the specified name.</summary>
	/// <param name="urlString">The URL of the document to load.</param>
	/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Navigate(string urlString, string targetFrameName)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(urlString);
	}

	/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into a new browser window or into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
	/// <param name="newWindow">true to load the document into a new browser window; false to load the document into the <see cref="T:System.Windows.Forms.WebBrowser" /> control. </param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
	public void Navigate(Uri url, bool newWindow)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(url.ToString());
	}

	/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the contents of the Web page frame with the specified name.</summary>
	/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
	/// <param name="targetFrameName">The name of the frame in which to load the document. </param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
	public void Navigate(Uri url, string targetFrameName)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(url.ToString());
	}

	/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, requesting it using the specified HTTP data and replacing the contents of the Web page frame with the specified name.</summary>
	/// <param name="urlString">The URL of the document to load.</param>
	/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
	/// <param name="postData">HTTP POST data such as form data.</param>
	/// <param name="additionalHeaders">HTTP headers to add to the default headers.</param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	public void Navigate(string urlString, string targetFrameName, byte[] postData, string additionalHeaders)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(urlString);
	}

	/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, requesting it using the specified HTTP data and replacing the contents of the Web page frame with the specified name.</summary>
	/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
	/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
	/// <param name="postData">HTTP POST data such as form data.</param>
	/// <param name="additionalHeaders">HTTP headers to add to the default headers.</param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
	public void Navigate(Uri url, string targetFrameName, byte[] postData, string additionalHeaders)
	{
		documentReady = false;
		document = null;
		base.WebHost.Navigation.Go(url.ToString());
	}

	/// <summary>Reloads the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control by checking the server for an updated version.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void Refresh()
	{
		Refresh(WebBrowserRefreshOption.IfExpired);
	}

	/// <summary>Reloads the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control using the specified refresh options.</summary>
	/// <param name="opt">One of the <see cref="T:System.Windows.Forms.WebBrowserRefreshOption" /> values. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Refresh(WebBrowserRefreshOption opt)
	{
		documentReady = false;
		document = null;
		switch (opt)
		{
		case WebBrowserRefreshOption.Normal:
			base.WebHost.Navigation.Reload(ReloadOption.Proxy);
			break;
		case WebBrowserRefreshOption.IfExpired:
			base.WebHost.Navigation.Reload(ReloadOption.None);
			break;
		case WebBrowserRefreshOption.Completely:
			base.WebHost.Navigation.Reload(ReloadOption.Full);
			break;
		case WebBrowserRefreshOption.Continue:
			break;
		}
	}

	/// <summary>Cancels any pending navigation and stops any dynamic page elements, such as background sounds and animations.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Stop()
	{
		base.WebHost.Navigation.Stop();
	}

	/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the default search page of the current user.</summary>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void GoSearch()
	{
		string urlString = "http://www.google.com";
		try
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\Search Page");
			if (registryKey != null)
			{
				object value = registryKey.GetValue("Default_Search_URL");
				if (value != null && value is string && Uri.TryCreate(value as string, UriKind.Absolute, out var result))
				{
					urlString = result.ToString();
				}
			}
		}
		catch
		{
		}
		Navigate(urlString);
	}

	/// <summary>Prints the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control using the current print and page settings.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Print()
	{
		throw new NotImplementedException();
	}

	/// <summary>Opens the Internet Explorer Page Setup dialog box.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowPageSetupDialog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Opens the Internet Explorer Print dialog box without setting header and footer values.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowPrintDialog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Opens the Internet Explorer Print Preview dialog box.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowPrintPreviewDialog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Opens the Internet Explorer Properties dialog box for the current document.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowPropertiesDialog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Opens the Internet Explorer Save Web Page dialog box or the Save dialog box of the hosted document if it is not an HTML page.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowSaveAsDialog()
	{
		throw new NotImplementedException();
	}

	/// <summary>Called by the control when the underlying ActiveX control is created.</summary>
	/// <param name="nativeActiveXObject">An object that represents the underlying ActiveX control.</param>
	[System.MonoTODO("Stub, not implemented")]
	protected override void AttachInterfaces(object nativeActiveXObject)
	{
		base.AttachInterfaces(nativeActiveXObject);
	}

	/// <summary>Associates the underlying ActiveX control with a client that can handle control events.</summary>
	[System.MonoTODO("Stub, not implemented")]
	protected override void CreateSink()
	{
		base.CreateSink();
	}

	/// <summary>Returns a reference to the unmanaged WebBrowser ActiveX control site, which you can extend to customize the managed <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> that represents the WebBrowser ActiveX control site.</returns>
	[System.MonoTODO("Stub, not implemented")]
	protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
	{
		return base.CreateWebBrowserSiteBase();
	}

	/// <summary>Called by the control when the underlying ActiveX control is discarded.</summary>
	[System.MonoTODO("Stub, not implemented")]
	protected override void DetachInterfaces()
	{
		base.DetachInterfaces();
	}

	/// <summary>Releases the event-handling client attached in the <see cref="M:System.Windows.Forms.WebBrowser.CreateSink" /> method from the underlying ActiveX control.</summary>
	[System.MonoTODO("Stub, not implemented")]
	protected override void DetachSink()
	{
		base.DetachSink();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.WebBrowser" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <param name="m">The windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.CanGoBackChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCanGoBackChanged(EventArgs e)
	{
		if (this.CanGoBackChanged != null)
		{
			this.CanGoBackChanged(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.CanGoForwardChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCanGoForwardChanged(EventArgs e)
	{
		if (this.CanGoForwardChanged != null)
		{
			this.CanGoForwardChanged(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.DocumentCompleted" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserDocumentCompletedEventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
	/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
	protected virtual void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
	{
		if (this.DocumentCompleted != null)
		{
			this.DocumentCompleted(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.DocumentTitleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnDocumentTitleChanged(EventArgs e)
	{
		if (this.DocumentTitleChanged != null)
		{
			this.DocumentTitleChanged(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.EncryptionLevelChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnEncryptionLevelChanged(EventArgs e)
	{
		if (this.EncryptionLevelChanged != null)
		{
			this.EncryptionLevelChanged(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.FileDownload" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
	protected virtual void OnFileDownload(EventArgs e)
	{
		if (this.FileDownload != null)
		{
			this.FileDownload(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.Navigated" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserNavigatedEventArgs" /> that contains the event data. </param>
	protected virtual void OnNavigated(WebBrowserNavigatedEventArgs e)
	{
		if (this.Navigated != null)
		{
			this.Navigated(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.Navigating" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserNavigatingEventArgs" /> that contains the event data. </param>
	protected virtual void OnNavigating(WebBrowserNavigatingEventArgs e)
	{
		if (this.Navigating != null)
		{
			this.Navigating(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.NewWindow" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
	protected virtual void OnNewWindow(CancelEventArgs e)
	{
		if (this.NewWindow != null)
		{
			this.NewWindow(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.ProgressChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserProgressChangedEventArgs" /> that contains the event data. </param>
	protected virtual void OnProgressChanged(WebBrowserProgressChangedEventArgs e)
	{
		if (this.ProgressChanged != null)
		{
			this.ProgressChanged(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.StatusTextChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnStatusTextChanged(EventArgs e)
	{
		if (this.StatusTextChanged != null)
		{
			this.StatusTextChanged(this, e);
		}
	}

	internal override bool OnNewWindowInternal()
	{
		CancelEventArgs cancelEventArgs = new CancelEventArgs();
		OnNewWindow(cancelEventArgs);
		return cancelEventArgs.Cancel;
	}

	internal override void OnWebHostLoadStarted(object sender, LoadStartedEventArgs e)
	{
		documentReady = false;
		document = null;
		readyState = WebBrowserReadyState.Loading;
		WebBrowserNavigatingEventArgs e2 = new WebBrowserNavigatingEventArgs(new Uri(e.Uri), e.FrameName);
		OnNavigating(e2);
	}

	internal override void OnWebHostLoadCommited(object sender, LoadCommitedEventArgs e)
	{
		readyState = WebBrowserReadyState.Loaded;
		url = e.Uri;
		SetScrollbars();
		WebBrowserNavigatedEventArgs e2 = new WebBrowserNavigatedEventArgs(new Uri(e.Uri));
		OnNavigated(e2);
	}

	internal override void OnWebHostProgressChanged(object sender, Mono.WebBrowser.ProgressChangedEventArgs e)
	{
		readyState = WebBrowserReadyState.Interactive;
		WebBrowserProgressChangedEventArgs e2 = new WebBrowserProgressChangedEventArgs(e.Progress, e.MaxProgress);
		OnProgressChanged(e2);
	}

	internal override void OnWebHostLoadFinished(object sender, LoadFinishedEventArgs e)
	{
		url = null;
		documentReady = true;
		readyState = WebBrowserReadyState.Complete;
		if (isStreamSet)
		{
			byte[] buffer = new byte[data.Length];
			long length = data.Length;
			int num = 0;
			data.Position = 0L;
			do
			{
				num = data.Read(buffer, (int)data.Position, (int)(length - data.Position));
			}
			while (num > 0);
			base.WebHost.Render(buffer);
			data = null;
			isStreamSet = false;
		}
		SetScrollbars();
		WebBrowserDocumentCompletedEventArgs e2 = new WebBrowserDocumentCompletedEventArgs(new Uri(e.Uri));
		OnDocumentCompleted(e2);
	}

	internal override void OnWebHostSecurityChanged(object sender, SecurityChangedEventArgs e)
	{
		switch (e.State)
		{
		case SecurityLevel.Insecure:
			securityLevel = WebBrowserEncryptionLevel.Insecure;
			break;
		case SecurityLevel.Mixed:
			securityLevel = WebBrowserEncryptionLevel.Mixed;
			break;
		case SecurityLevel.Secure:
			securityLevel = WebBrowserEncryptionLevel.Bit56;
			break;
		}
	}

	internal override void OnWebHostContextMenuShown(object sender, ContextMenuEventArgs e)
	{
		if (isWebBrowserContextMenuEnabled)
		{
			ContextMenu contextMenu = new ContextMenu();
			MenuItem menuItem = new MenuItem("Back", delegate
			{
				GoBack();
			});
			menuItem.Enabled = CanGoBack;
			contextMenu.MenuItems.Add(menuItem);
			menuItem = new MenuItem("Forward", delegate
			{
				GoForward();
			});
			menuItem.Enabled = CanGoForward;
			contextMenu.MenuItems.Add(menuItem);
			menuItem = new MenuItem("Refresh", delegate
			{
				Refresh();
			});
			contextMenu.MenuItems.Add(menuItem);
			contextMenu.MenuItems.Add(new MenuItem("-"));
			contextMenu.Show(this, PointToClient(Control.MousePosition));
		}
	}

	internal override void OnWebHostStatusChanged(object sender, StatusChangedEventArgs e)
	{
		status = e.Message;
		OnStatusTextChanged(null);
	}

	private void SetScrollbars()
	{
	}
}
