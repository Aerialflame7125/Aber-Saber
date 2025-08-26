namespace System.Windows.Forms;

/// <summary>Defines a method that executes a certain action on the type that implements this interface.</summary>
/// <filterpriority>2</filterpriority>
public interface ICommandExecutor
{
	/// <summary>Performs a task that is determined by the type that implements this method. </summary>
	/// <filterpriority>1</filterpriority>
	void Execute();
}
