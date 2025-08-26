using System.Collections;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design;

/// <summary>Enables interaction with the user interface of the development environment object that is hosting the designer.</summary>
[Guid("06a9c74b-5e32-4561-be73-381b37869f4f")]
public interface IUIService
{
	/// <summary>Gets the collection of styles that are specific to the host's environment.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing style settings.</returns>
	IDictionary Styles { get; }

	/// <summary>Indicates whether the component can display a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" />.</summary>
	/// <returns>true if the specified component can display a component editor form; otherwise, false.</returns>
	/// <param name="component">The component to check for support for displaying a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" />. </param>
	bool CanShowComponentEditor(object component);

	/// <summary>Gets the window that should be used as the owner when showing dialog boxes.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.IWin32Window" /> that indicates the window to own any child dialog boxes.</returns>
	IWin32Window GetDialogOwnerWindow();

	/// <summary>Sets a flag indicating the UI has changed.</summary>
	void SetUIDirty();

	/// <summary>Attempts to display a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" /> for a component.</summary>
	/// <returns>true if the attempt is successful; otherwise, false.</returns>
	/// <param name="component">The component for which to display a <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" />. </param>
	/// <param name="parent">The <see cref="T:System.Windows.Forms.IWin32Window" /> to parent any dialog boxes to. </param>
	/// <exception cref="T:System.ArgumentException">The component does not support component editors. </exception>
	bool ShowComponentEditor(object component, IWin32Window parent);

	/// <summary>Displays the specified exception and information about the exception in a message box.</summary>
	/// <param name="ex">The <see cref="T:System.Exception" /> to display. </param>
	void ShowError(Exception ex);

	/// <summary>Displays the specified error message in a message box.</summary>
	/// <param name="message">The error message to display. </param>
	void ShowError(string message);

	/// <summary>Displays the specified exception and information about the exception in a message box.</summary>
	/// <param name="ex">The <see cref="T:System.Exception" /> to display. </param>
	/// <param name="message">A message to display that provides information about the exception. </param>
	void ShowError(Exception ex, string message);

	/// <summary>Attempts to display the specified form in a dialog box.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned by the dialog box.</returns>
	/// <param name="form">The <see cref="T:System.Windows.Forms.Form" /> to display. </param>
	DialogResult ShowDialog(Form form);

	/// <summary>Displays the specified message in a message box.</summary>
	/// <param name="message">The message to display </param>
	void ShowMessage(string message);

	/// <summary>Displays the specified message in a message box with the specified caption.</summary>
	/// <param name="message">The message to display. </param>
	/// <param name="caption">The caption for the message box. </param>
	void ShowMessage(string message, string caption);

	/// <summary>Displays the specified message in a message box with the specified caption and buttons to place on the dialog box.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned by the dialog box.</returns>
	/// <param name="message">The message to display. </param>
	/// <param name="caption">The caption for the dialog box. </param>
	/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values: <see cref="F:System.Windows.Forms.MessageBoxButtons.OK" />, <see cref="F:System.Windows.Forms.MessageBoxButtons.OKCancel" />, <see cref="F:System.Windows.Forms.MessageBoxButtons.YesNo" />, or <see cref="F:System.Windows.Forms.MessageBoxButtons.YesNoCancel" />. </param>
	DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons);

	/// <summary>Displays the specified tool window.</summary>
	/// <returns>true if the tool window was successfully shown; false if it could not be shown or found.</returns>
	/// <param name="toolWindow">A <see cref="T:System.Guid" /> identifier for the tool window. This can be a custom <see cref="T:System.Guid" /> or one of the predefined values from <see cref="T:System.ComponentModel.Design.StandardToolWindows" />. </param>
	bool ShowToolWindow(Guid toolWindow);
}
