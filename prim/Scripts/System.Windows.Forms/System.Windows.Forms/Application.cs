using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms;

/// <summary>Provides static methods and properties to manage an application, such as methods to start and stop an application, to process Windows messages, and properties to get information about an application. This class cannot be inherited.</summary>
/// <filterpriority>1</filterpriority>
public sealed class Application
{
	internal class MWFThread
	{
		private ApplicationContext context;

		private bool messageloop_started;

		private bool handling_exception;

		private int thread_id;

		private static readonly Hashtable threads = new Hashtable();

		public ApplicationContext Context
		{
			get
			{
				return context;
			}
			set
			{
				context = value;
			}
		}

		public bool MessageLoop
		{
			get
			{
				return messageloop_started;
			}
			set
			{
				messageloop_started = value;
			}
		}

		public bool HandlingException
		{
			get
			{
				return handling_exception;
			}
			set
			{
				handling_exception = value;
			}
		}

		public static int LoopCount
		{
			get
			{
				lock (threads)
				{
					int num = 0;
					foreach (MWFThread value in threads.Values)
					{
						if (value.messageloop_started)
						{
							num++;
						}
					}
					return num;
				}
			}
		}

		public static MWFThread Current
		{
			get
			{
				MWFThread mWFThread = null;
				lock (threads)
				{
					mWFThread = (MWFThread)threads[Thread.CurrentThread.GetHashCode()];
					if (mWFThread == null)
					{
						mWFThread = new MWFThread();
						mWFThread.thread_id = Thread.CurrentThread.GetHashCode();
						threads[mWFThread.thread_id] = mWFThread;
					}
				}
				return mWFThread;
			}
		}

		private MWFThread()
		{
		}

		public void Exit()
		{
			if (context != null)
			{
				context.ExitThread();
			}
			context = null;
			if (Application.ThreadExit != null)
			{
				Application.ThreadExit(null, EventArgs.Empty);
			}
			if (LoopCount == 0 && Application.ApplicationExit != null)
			{
				Application.ApplicationExit(null, EventArgs.Empty);
			}
			((MWFThread)threads[thread_id]).MessageLoop = false;
		}
	}

	/// <summary>Represents a method that will check whether the hosting environment is still sending messages. </summary>
	/// <returns>true if the hosting environment is still sending messages; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public delegate bool MessageLoopCallback();

	private static bool browser_embedded;

	private static InputLanguage input_language;

	private static string safe_caption_format;

	private static readonly ArrayList message_filters;

	private static readonly FormCollection forms;

	private static bool use_wait_cursor;

	private static ToolStrip keyboard_capture;

	private static VisualStyleState visual_style_state;

	private static bool visual_styles_enabled;

	internal static bool use_compatible_text_rendering;

	/// <summary>Gets a value indicating whether the caller can quit this application.</summary>
	/// <returns>true if the caller can quit this application; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	public static bool AllowQuit => !browser_embedded;

	/// <summary>Gets the path for the application data that is shared among all users.</summary>
	/// <returns>The path for the application data that is shared among all users.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string CommonAppDataPath => CreateDataPath(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));

	/// <summary>Gets the registry key for the application data that is shared among all users.</summary>
	/// <returns>A <see cref="T:Microsoft.Win32.RegistryKey" /> representing the registry key of the application data that is shared among all users.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static RegistryKey CommonAppDataRegistry
	{
		get
		{
			string subkey = $"Software\\{CompanyName}\\{ProductName}\\{ProductVersion}";
			return Registry.LocalMachine.CreateSubKey(subkey);
		}
	}

	/// <summary>Gets the company name associated with the application.</summary>
	/// <returns>The company name.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string CompanyName
	{
		get
		{
			string text = string.Empty;
			Assembly assembly = Assembly.GetEntryAssembly();
			if ((object)assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			AssemblyCompanyAttribute[] array = (AssemblyCompanyAttribute[])assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: true);
			if (array != null && array.Length > 0)
			{
				text = array[0].Company;
			}
			if ((text == null || text.Length == 0) && (object)assembly.EntryPoint != null)
			{
				text = assembly.EntryPoint.DeclaringType.Namespace;
				if (text != null)
				{
					int num = text.IndexOf('.');
					if (num >= 0)
					{
						text = text.Substring(0, num);
					}
				}
			}
			if ((text == null || text.Length == 0) && (object)assembly.EntryPoint != null)
			{
				text = assembly.EntryPoint.DeclaringType.FullName;
			}
			return text;
		}
	}

	/// <summary>Gets or sets the culture information for the current thread.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture information for the current thread.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
	/// </PermissionSet>
	public static CultureInfo CurrentCulture
	{
		get
		{
			return Thread.CurrentThread.CurrentUICulture;
		}
		set
		{
			Thread.CurrentThread.CurrentUICulture = value;
		}
	}

	/// <summary>Gets or sets the current input language for the current thread.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> representing the current input language for the current thread.</returns>
	/// <filterpriority>2</filterpriority>
	public static InputLanguage CurrentInputLanguage
	{
		get
		{
			return input_language;
		}
		set
		{
			input_language = value;
		}
	}

	/// <summary>Gets the path for the executable file that started the application, including the executable name.</summary>
	/// <returns>The path and executable name for the executable file that started the application.This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string ExecutablePath => Path.GetFullPath(Environment.GetCommandLineArgs()[0]);

	/// <summary>Gets the path for the application data of a local, non-roaming user.</summary>
	/// <returns>The path for the application data of a local, non-roaming user.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string LocalUserAppDataPath => CreateDataPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

	/// <summary>Gets a value indicating whether a message loop exists on this thread.</summary>
	/// <returns>true if a message loop exists; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool MessageLoop => MWFThread.Current.MessageLoop;

	/// <summary>Gets the product name associated with this application.</summary>
	/// <returns>The product name.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string ProductName
	{
		get
		{
			string text = string.Empty;
			Assembly assembly = Assembly.GetEntryAssembly();
			if ((object)assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			AssemblyProductAttribute[] array = (AssemblyProductAttribute[])assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: true);
			if (array != null && array.Length > 0)
			{
				text = array[0].Product;
			}
			if ((text == null || text.Length == 0) && (object)assembly.EntryPoint != null)
			{
				text = assembly.EntryPoint.DeclaringType.Namespace;
				if (text != null)
				{
					int num = text.LastIndexOf('.');
					if (num >= 0 && num < text.Length - 1)
					{
						text = text.Substring(num + 1);
					}
				}
				if (text == null || text.Length == 0)
				{
					text = assembly.EntryPoint.DeclaringType.FullName;
				}
			}
			return text;
		}
	}

	/// <summary>Gets the product version associated with this application.</summary>
	/// <returns>The product version.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string ProductVersion
	{
		get
		{
			string text = string.Empty;
			Assembly assembly = Assembly.GetEntryAssembly();
			if ((object)assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			if (Attribute.GetCustomAttribute(assembly, typeof(AssemblyInformationalVersionAttribute)) is AssemblyInformationalVersionAttribute assemblyInformationalVersionAttribute)
			{
				text = assemblyInformationalVersionAttribute.InformationalVersion;
			}
			if ((text == null || text.Length == 0) && Attribute.GetCustomAttribute(assembly, typeof(AssemblyFileVersionAttribute)) is AssemblyFileVersionAttribute assemblyFileVersionAttribute)
			{
				text = assemblyFileVersionAttribute.Version;
			}
			if (text == null || text.Length == 0)
			{
				text = assembly.GetName().Version.ToString();
			}
			return text;
		}
	}

	/// <summary>Gets or sets the format string to apply to top-level window captions when they are displayed with a warning banner.</summary>
	/// <returns>The format string to apply to top-level window captions.</returns>
	/// <filterpriority>2</filterpriority>
	public static string SafeTopLevelCaptionFormat
	{
		get
		{
			return safe_caption_format;
		}
		set
		{
			safe_caption_format = value;
		}
	}

	/// <summary>Gets the path for the executable file that started the application, not including the executable name.</summary>
	/// <returns>The path for the executable file that started the application.This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string StartupPath => Path.GetDirectoryName(ExecutablePath);

	/// <summary>Gets the path for the application data of a user.</summary>
	/// <returns>The path for the application data of a user.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static string UserAppDataPath => CreateDataPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

	/// <summary>Gets the registry key for the application data of a user.</summary>
	/// <returns>A <see cref="T:Microsoft.Win32.RegistryKey" /> representing the registry key for the application data specific to the user.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static RegistryKey UserAppDataRegistry
	{
		get
		{
			string subkey = $"Software\\{CompanyName}\\{ProductName}\\{ProductVersion}";
			return Registry.CurrentUser.CreateSubKey(subkey);
		}
	}

	/// <summary>Gets or sets whether the wait cursor is used for all open forms of the application.</summary>
	/// <returns>true is the wait cursor is used for all open forms; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	public static bool UseWaitCursor
	{
		get
		{
			return use_wait_cursor;
		}
		set
		{
			use_wait_cursor = value;
			if (!use_wait_cursor)
			{
				return;
			}
			foreach (Form openForm in OpenForms)
			{
				openForm.Cursor = Cursors.WaitCursor;
			}
		}
	}

	/// <summary>Gets a value specifying whether the current application is drawing controls with visual styles.</summary>
	/// <returns>true if visual styles are enabled for controls in the client area of application windows; otherwise, false.</returns>
	public static bool RenderWithVisualStyles
	{
		get
		{
			if (VisualStyleInformation.IsSupportedByOS)
			{
				if (!VisualStyleInformation.IsEnabledByUser)
				{
					return false;
				}
				if (!XplatUI.ThemesEnabled)
				{
					return false;
				}
				if (VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled)
				{
					return true;
				}
				if (VisualStyleState == VisualStyleState.ClientAreaEnabled)
				{
					return true;
				}
			}
			return false;
		}
	}

	/// <summary>Gets a value that specifies how visual styles are applied to application windows.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleState" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public static VisualStyleState VisualStyleState
	{
		get
		{
			return visual_style_state;
		}
		set
		{
			visual_style_state = value;
		}
	}

	/// <summary>Gets a collection of open forms owned by the application.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FormCollection" /> containing all the currently open forms owned by this application.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
	/// </PermissionSet>
	public static FormCollection OpenForms => forms;

	internal static ToolStrip KeyboardCapture
	{
		get
		{
			return keyboard_capture;
		}
		set
		{
			keyboard_capture = value;
		}
	}

	internal static bool VisualStylesEnabled => visual_styles_enabled;

	/// <summary>Occurs when the application is about to shut down.</summary>
	/// <filterpriority>1</filterpriority>
	public static event EventHandler ApplicationExit;

	/// <summary>Occurs when the application finishes processing and is about to enter the idle state.</summary>
	/// <filterpriority>1</filterpriority>
	public static event EventHandler Idle
	{
		add
		{
			XplatUI.Idle += value;
		}
		remove
		{
			XplatUI.Idle -= value;
		}
	}

	/// <summary>Occurs when a thread is about to shut down. When the main thread for an application is about to be shut down, this event is raised first, followed by an <see cref="E:System.Windows.Forms.Application.ApplicationExit" /> event.</summary>
	/// <filterpriority>1</filterpriority>
	public static event EventHandler ThreadExit;

	/// <summary>Occurs when an untrapped thread exception is thrown.</summary>
	/// <filterpriority>1</filterpriority>
	public static event ThreadExceptionEventHandler ThreadException;

	internal static event EventHandler FormAdded;

	internal static event EventHandler PreRun;

	/// <summary>Occurs when the application is about to enter a modal state. </summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static event EventHandler EnterThreadModal;

	/// <summary>Occurs when the application is about to leave a modal state. </summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static event EventHandler LeaveThreadModal;

	private Application()
	{
		browser_embedded = false;
	}

	static Application()
	{
		input_language = InputLanguage.CurrentInputLanguage;
		safe_caption_format = "{1} - {0} - {2}";
		message_filters = new ArrayList();
		forms = new FormCollection();
		visual_style_state = VisualStyleState.ClientAndNonClientAreasEnabled;
		use_compatible_text_rendering = true;
		InitializeUIAutomation();
	}

	private static void InitializeUIAutomation()
	{
		Assembly assembly = null;
		try
		{
			assembly = Assembly.Load("UIAutomationWinforms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f4ceacb585d99812");
		}
		catch
		{
		}
		if ((object)assembly == null)
		{
			return;
		}
		try
		{
			Type type = assembly.GetType("Mono.UIAutomation.Winforms.Global", throwOnError: false);
			if ((object)type != null)
			{
				MethodInfo method = type.GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public);
				if ((object)method != null)
				{
					method.Invoke(null, new object[0]);
					return;
				}
				throw new Exception(string.Format("Method {0} not found in type {1}.", "Initialize", "Mono.UIAutomation.Winforms.Global"));
			}
			throw new Exception(string.Format("Type {0} not found in assembly {1}.", "Mono.UIAutomation.Winforms.Global", "UIAutomationWinforms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f4ceacb585d99812"));
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine("Error setting up UIA: " + ex);
		}
	}

	internal static void CloseForms(Thread thread)
	{
		ArrayList arrayList = new ArrayList();
		lock (forms)
		{
			foreach (Form form3 in forms)
			{
				if (thread == null || thread == form3.creator_thread)
				{
					arrayList.Add(form3);
				}
			}
			foreach (Form item in arrayList)
			{
				item.Dispose();
			}
		}
	}

	/// <summary>Adds a message filter to monitor Windows messages as they are routed to their destinations.</summary>
	/// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter" /> interface you want to install. </param>
	/// <filterpriority>1</filterpriority>
	public static void AddMessageFilter(IMessageFilter value)
	{
		lock (message_filters)
		{
			message_filters.Add(value);
		}
	}

	internal static void AddKeyFilter(IKeyFilter value)
	{
		XplatUI.AddKeyFilter(value);
	}

	/// <summary>Processes all Windows messages currently in the message queue.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void DoEvents()
	{
		XplatUI.DoEvents();
	}

	/// <summary>Enables visual styles for the application.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void EnableVisualStyles()
	{
		visual_styles_enabled = true;
		XplatUI.EnableThemes();
	}

	/// <summary>Runs any filters against a window message, and returns a copy of the modified message.</summary>
	/// <returns>True if the filters were processed; otherwise, false.</returns>
	/// <param name="message">The Windows event message to filter. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static bool FilterMessage(ref Message message)
	{
		lock (message_filters)
		{
			for (int i = 0; i < message_filters.Count; i++)
			{
				IMessageFilter messageFilter = (IMessageFilter)message_filters[i];
				if (messageFilter.PreFilterMessage(ref message))
				{
					return true;
				}
			}
		}
		return false;
	}

	/// <summary>Sets the application-wide default for the UseCompatibleTextRendering property defined on certain controls.</summary>
	/// <param name="defaultValue">The default value to use for new controls. If true, new controls that support UseCompatibleTextRendering use the GDI+ based <see cref="T:System.Drawing.Graphics" /> class for text rendering; if false, new controls use the GDI based <see cref="T:System.Windows.Forms.TextRenderer" /> class.</param>
	/// <exception cref="T:System.InvalidOperationException">You can only call this method before the first window is created by your Windows Forms application. </exception>
	public static void SetCompatibleTextRenderingDefault(bool defaultValue)
	{
		use_compatible_text_rendering = defaultValue;
	}

	/// <summary>Registers a callback for checking whether the message loop is running in hosted environments.</summary>
	/// <param name="callback">The method to call when Windows Forms needs to check if the hosting environment is still sending messages.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoNotSupported("Only applies when Winforms is being hosted by an unmanaged app.")]
	public static void RegisterMessageLoop(MessageLoopCallback callback)
	{
	}

	/// <summary>Suspends or hibernates the system, or requests that the system be suspended or hibernated.</summary>
	/// <returns>true if the system is being suspended, otherwise, false.</returns>
	/// <param name="state">A <see cref="T:System.Windows.Forms.PowerState" /> indicating the power activity mode to which to transition. </param>
	/// <param name="force">true to force the suspended mode immediately; false to cause Windows to send a suspend request to every application. </param>
	/// <param name="disableWakeEvent">true to disable restoring the system's power status to active on a wake event, false to enable restoring the system's power status to active on a wake event. </param>
	/// <filterpriority>1</filterpriority>
	[System.MonoNotSupported("Empty stub.")]
	public static bool SetSuspendState(PowerState state, bool force, bool disableWakeEvent)
	{
		return false;
	}

	/// <summary>Instructs the application how to respond to unhandled exceptions.</summary>
	/// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode" /> value describing how the application should behave if an exception is thrown without being caught.</param>
	/// <exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
	[System.MonoNotSupported("Empty stub.")]
	public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode)
	{
	}

	/// <summary>Instructs the application how to respond to unhandled exceptions, optionally applying thread-specific behavior.</summary>
	/// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode" /> value describing how the application should behave if an exception is thrown without being caught.</param>
	/// <param name="threadScope">true to set the thread exception mode; otherwise, false.</param>
	/// <exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
	[System.MonoNotSupported("Empty stub.")]
	public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode, bool threadScope)
	{
	}

	/// <summary>Unregisters the message loop callback made with <see cref="M:System.Windows.Forms.Application.RegisterMessageLoop(System.Windows.Forms.Application.MessageLoopCallback)" />.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoNotSupported("Only applies when Winforms is being hosted by an unmanaged app.")]
	public static void UnregisterMessageLoop()
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Application.Idle" /> event in hosted scenarios.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> objects to pass to the <see cref="E:System.Windows.Forms.Application.Idle" /> event.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static void RaiseIdle(EventArgs e)
	{
		XplatUI.RaiseIdle(e);
	}

	/// <summary>Shuts down the application and starts a new instance immediately.</summary>
	/// <exception cref="T:System.NotSupportedException">Your code is not a Windows Forms application. You cannot call this method in this context.</exception>
	public static void Restart()
	{
		if ((object)Assembly.GetEntryAssembly() == null)
		{
			throw new NotSupportedException("The method 'Restart' is not supported by this application type.");
		}
		string text = null;
		PropertyInfo property = typeof(Environment).GetProperty("GacPath", BindingFlags.Static | BindingFlags.NonPublic);
		MethodInfo methodInfo = null;
		if ((object)property != null)
		{
			methodInfo = property.GetGetMethod(nonPublic: true);
		}
		if ((object)methodInfo != null)
		{
			string directoryName = Path.GetDirectoryName((string)methodInfo.Invoke(null, null));
			string directoryName2 = Path.GetDirectoryName(Path.GetDirectoryName(directoryName));
			if (XplatUI.RunningOnUnix)
			{
				text = Path.Combine(directoryName2, "bin/mono");
				if (!File.Exists(text))
				{
					text = "mono";
				}
			}
			else
			{
				text = Path.Combine(directoryName2, "bin\\mono.bat");
				if (!File.Exists(text))
				{
					text = Path.Combine(directoryName2, "bin\\mono.exe");
				}
				if (!File.Exists(text))
				{
					text = Path.Combine(directoryName2, "mono\\mono\\mini\\mono.exe");
				}
				if (!File.Exists(text))
				{
					throw new FileNotFoundException($"Windows mono path not found: '{text}'");
				}
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			stringBuilder.Append($"\"{commandLineArgs[i]}\" ");
		}
		string text2 = stringBuilder.ToString();
		ProcessStartInfo startInfo = Process.GetCurrentProcess().StartInfo;
		if (text == null)
		{
			startInfo.FileName = commandLineArgs[0];
			startInfo.Arguments = text2.Remove(0, commandLineArgs[0].Length + 3);
		}
		else
		{
			startInfo.Arguments = text2;
			startInfo.FileName = text;
		}
		startInfo.WorkingDirectory = Environment.CurrentDirectory;
		Exit();
		Process.Start(startInfo);
	}

	/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void Exit()
	{
		Exit(new CancelEventArgs());
	}

	/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
	/// <param name="e">Returns whether any <see cref="T:System.Windows.Forms.Form" /> within the application cancelled the exit.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static void Exit(CancelEventArgs e)
	{
		lock (forms)
		{
			ArrayList arrayList = new ArrayList(forms);
			foreach (Form item in arrayList)
			{
				e.Cancel = item.FireClosingEvents(CloseReason.ApplicationExitCall, cancel: false);
				if (e.Cancel)
				{
					return;
				}
				item.suppress_closing_events = true;
				item.Close();
				item.Dispose();
			}
		}
		XplatUI.PostQuitMessage(0);
	}

	/// <summary>Exits the message loop on the current thread and closes all windows on the thread.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void ExitThread()
	{
		CloseForms(Thread.CurrentThread);
		XplatUI.PostQuitMessage(0);
	}

	/// <summary>Initializes OLE on the current thread.</summary>
	/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values.</returns>
	/// <filterpriority>2</filterpriority>
	public static ApartmentState OleRequired()
	{
		return ApartmentState.Unknown;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event. </summary>
	/// <param name="t">An <see cref="T:System.Exception" /> that represents the exception that was thrown. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void OnThreadException(Exception t)
	{
		if (MWFThread.Current.HandlingException)
		{
			Console.WriteLine(t);
			Environment.Exit(1);
		}
		try
		{
			MWFThread.Current.HandlingException = true;
			if (Application.ThreadException != null)
			{
				Application.ThreadException(null, new ThreadExceptionEventArgs(t));
			}
			else if (SystemInformation.UserInteractive)
			{
				Form form = new ThreadExceptionDialog(t);
				form.ShowDialog();
			}
			else
			{
				Console.WriteLine(t.ToString());
				Exit();
			}
		}
		finally
		{
			MWFThread.Current.HandlingException = false;
		}
	}

	/// <summary>Removes a message filter from the message pump of the application.</summary>
	/// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter" /> to remove from the application. </param>
	/// <filterpriority>1</filterpriority>
	public static void RemoveMessageFilter(IMessageFilter value)
	{
		lock (message_filters)
		{
			message_filters.Remove(value);
		}
	}

	/// <summary>Begins running a standard application message loop on the current thread, without a form.</summary>
	/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void Run()
	{
		Run(new ApplicationContext());
	}

	/// <summary>Begins running a standard application message loop on the current thread, and makes the specified form visible.</summary>
	/// <param name="mainForm">A <see cref="T:System.Windows.Forms.Form" /> that represents the form to make visible. </param>
	/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on the current thread. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void Run(Form mainForm)
	{
		Run(new ApplicationContext(mainForm));
	}

	internal static void FirePreRun()
	{
		Application.PreRun?.Invoke(null, EventArgs.Empty);
	}

	/// <summary>Begins running a standard application message loop on the current thread, with an <see cref="T:System.Windows.Forms.ApplicationContext" />.</summary>
	/// <param name="context">An <see cref="T:System.Windows.Forms.ApplicationContext" /> in which the application is run. </param>
	/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static void Run(ApplicationContext context)
	{
		if (SynchronizationContext.Current == null)
		{
			SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
		}
		RunLoop(Modal: false, context);
		if (SynchronizationContext.Current is WindowsFormsSynchronizationContext)
		{
			WindowsFormsSynchronizationContext.Uninstall();
		}
	}

	private static void DisableFormsForModalLoop(Queue toplevels, ApplicationContext context)
	{
		lock (forms)
		{
			IEnumerator enumerator = forms.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Form form = (Form)enumerator.Current;
				if (form == context.MainForm)
				{
					continue;
				}
				Control control = form;
				bool flag = false;
				do
				{
					if (control.Parent == context.MainForm)
					{
						flag = true;
						break;
					}
					control = control.Parent;
				}
				while (control != null);
				if (!flag && form.IsHandleCreated && XplatUI.IsEnabled(form.Handle))
				{
					XplatUI.EnableWindow(form.Handle, Enable: false);
					toplevels.Enqueue(form);
				}
			}
		}
	}

	private static void EnableFormsForModalLoop(Queue toplevels, ApplicationContext context)
	{
		while (toplevels.Count > 0)
		{
			Form form = (Form)toplevels.Dequeue();
			if (form.IsHandleCreated)
			{
				XplatUI.EnableWindow(form.window.Handle, Enable: true);
				context.MainForm = form;
			}
		}
	}

	internal static void RunLoop(bool Modal, ApplicationContext context)
	{
		MWFThread current = MWFThread.Current;
		MSG msg = default(MSG);
		if (context == null)
		{
			context = new ApplicationContext();
		}
		ApplicationContext context2 = current.Context;
		current.Context = context;
		if (context.MainForm != null)
		{
			context.MainForm.is_modal = Modal;
			context.MainForm.context = context;
			context.MainForm.closing = false;
			context.MainForm.Visible = true;
			if (context.MainForm != null)
			{
				context.MainForm.Activate();
			}
		}
		Queue toplevels;
		if (Modal)
		{
			toplevels = new Queue();
			DisableFormsForModalLoop(toplevels, context);
			if (context.MainForm != null)
			{
				XplatUI.EnableWindow(context.MainForm.Handle, Enable: true);
				XplatUI.SetModal(context.MainForm.Handle, Modal: true);
			}
		}
		else
		{
			toplevels = null;
		}
		object queue_id = XplatUI.StartLoop(Thread.CurrentThread);
		current.MessageLoop = true;
		bool flag = false;
		while (!flag && XplatUI.GetMessage(queue_id, ref msg, IntPtr.Zero, 0, 0))
		{
			Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
			if (FilterMessage(ref message))
			{
				continue;
			}
			switch (msg.message)
			{
			case Msg.WM_KEYDOWN:
			case Msg.WM_KEYUP:
			case Msg.WM_CHAR:
			case Msg.WM_SYSKEYDOWN:
			case Msg.WM_SYSKEYUP:
			case Msg.WM_SYSCHAR:
			{
				Control control2 = Control.FromHandle(msg.hwnd);
				if (keyboard_capture != null)
				{
					if (message.Msg == 260 && message.WParam.ToInt32() == 18)
					{
						keyboard_capture.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.Keyboard);
						continue;
					}
					message.HWnd = keyboard_capture.Handle;
					switch (keyboard_capture.PreProcessControlMessageInternal(ref message))
					{
					case PreProcessControlState.MessageNeeded:
					case PreProcessControlState.MessageNotNeeded:
						if ((message.Msg != 256 && message.Msg != 258) || keyboard_capture.ProcessControlMnemonic((char)(int)message.WParam) || control2 == null || !ControlOnToolStrip(control2))
						{
							continue;
						}
						message.HWnd = msg.hwnd;
						break;
					case PreProcessControlState.MessageProcessed:
						continue;
					}
				}
				if ((control2 != null && control2.PreProcessControlMessageInternal(ref message) != 0) || control2 == null)
				{
					goto default;
				}
				break;
			}
			case Msg.WM_LBUTTONDOWN:
			case Msg.WM_RBUTTONDOWN:
			case Msg.WM_MBUTTONDOWN:
				if (keyboard_capture != null)
				{
					Control control = Control.FromHandle(msg.hwnd);
					if (control == null)
					{
						ToolStripManager.FireAppClicked();
					}
					else if (control is ToolStrip)
					{
						if ((control as ToolStrip).GetTopLevelToolStrip() != keyboard_capture.GetTopLevelToolStrip())
						{
							ToolStripManager.FireAppClicked();
						}
					}
					else if ((control.Parent == null || !(control.Parent is ToolStripDropDownMenu) || (control.Parent as ToolStripDropDownMenu).GetTopLevelToolStrip() != keyboard_capture.GetTopLevelToolStrip()) && control.TopLevelControl != null)
					{
						ToolStripManager.FireAppClicked();
					}
				}
				goto default;
			case Msg.WM_QUIT:
				flag = true;
				break;
			default:
				XplatUI.TranslateMessage(ref msg);
				XplatUI.DispatchMessage(ref msg);
				break;
			}
			if (context.MainForm != null && (context.MainForm.closing || (Modal && !context.MainForm.Visible)))
			{
				if (Modal)
				{
					break;
				}
				XplatUI.PostQuitMessage(0);
			}
		}
		current.MessageLoop = false;
		XplatUI.EndLoop(Thread.CurrentThread);
		if (Modal)
		{
			Form mainForm = context.MainForm;
			context.MainForm = null;
			EnableFormsForModalLoop(toplevels, context);
			if (context.MainForm != null && context.MainForm.IsHandleCreated)
			{
				XplatUI.SetModal(context.MainForm.Handle, Modal: false);
			}
			mainForm.RaiseCloseEvents(last_check: true, cancel: false);
			mainForm.is_modal = false;
		}
		if (context.MainForm != null)
		{
			context.MainForm.context = null;
			context.MainForm = null;
		}
		current.Context = context2;
		if (!Modal)
		{
			current.Exit();
		}
	}

	internal static void AddForm(Form f)
	{
		lock (forms)
		{
			forms.Add(f);
		}
		if (Application.FormAdded != null)
		{
			Application.FormAdded(f, null);
		}
	}

	internal static void RemoveForm(Form f)
	{
		lock (forms)
		{
			forms.Remove(f);
		}
	}

	private static bool ControlOnToolStrip(Control c)
	{
		for (Control parent = c.Parent; parent != null; parent = parent.Parent)
		{
			if (parent is ToolStrip)
			{
				return true;
			}
		}
		return false;
	}

	private static string CreateDataPath(string basePath)
	{
		string path = Path.Combine(basePath, CompanyName);
		path = Path.Combine(path, ProductName);
		path = Path.Combine(path, ProductVersion);
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		return path;
	}
}
