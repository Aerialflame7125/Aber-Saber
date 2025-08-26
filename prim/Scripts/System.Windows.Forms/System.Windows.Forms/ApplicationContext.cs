using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Specifies the contextual information about an application thread.</summary>
/// <filterpriority>1</filterpriority>
public class ApplicationContext : IDisposable
{
	private Form main_form;

	private object tag;

	private bool thread_exit_raised;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.Form" /> to use as context.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Form" /> to use as context.</returns>
	/// <filterpriority>1</filterpriority>
	public Form MainForm
	{
		get
		{
			return main_form;
		}
		set
		{
			if (main_form != value)
			{
				if (main_form != null)
				{
					main_form.HandleDestroyed -= OnMainFormClosed;
				}
				main_form = value;
				if (main_form != null)
				{
					main_form.HandleDestroyed += OnMainFormClosed;
				}
			}
		}
	}

	/// <summary>Gets or sets an object that contains data about the control.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
	/// <filterpriority>2</filterpriority>
	[Bindable(true)]
	[Localizable(false)]
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue(null)]
	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value;
		}
	}

	/// <summary>Occurs when the message loop of the thread should be terminated, by calling <see cref="M:System.Windows.Forms.ApplicationContext.ExitThread" />.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ThreadExit;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with no context.</summary>
	public ApplicationContext()
		: this(null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with the specified <see cref="T:System.Windows.Forms.Form" />.</summary>
	/// <param name="mainForm">The main <see cref="T:System.Windows.Forms.Form" /> of the application to use for context. </param>
	public ApplicationContext(Form mainForm)
	{
		MainForm = mainForm;
	}

	/// <summary>Attempts to free resources and perform other cleanup operations before the application context is reclaimed by garbage collection.</summary>
	~ApplicationContext()
	{
		Dispose(disposing: false);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" />.</summary>
	/// <filterpriority>2</filterpriority>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Terminates the message loop of the thread.</summary>
	/// <filterpriority>1</filterpriority>
	public void ExitThread()
	{
		ExitThreadCore();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected virtual void Dispose(bool disposing)
	{
		MainForm = null;
		tag = null;
	}

	/// <summary>Terminates the message loop of the thread.</summary>
	protected virtual void ExitThreadCore()
	{
		if (Application.MWFThread.Current.Context == this)
		{
			XplatUI.PostQuitMessage(0);
		}
		if (!thread_exit_raised && this.ThreadExit != null)
		{
			thread_exit_raised = true;
			this.ThreadExit(this, EventArgs.Empty);
		}
	}

	/// <summary>Calls <see cref="M:System.Windows.Forms.ApplicationContext.ExitThreadCore" />, which raises the <see cref="E:System.Windows.Forms.ApplicationContext.ThreadExit" /> event.</summary>
	/// <param name="sender">The object that raised the event. </param>
	/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnMainFormClosed(object sender, EventArgs e)
	{
		if (!MainForm.RecreatingHandle)
		{
			ExitThreadCore();
		}
	}
}
