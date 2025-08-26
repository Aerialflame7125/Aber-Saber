using System.Runtime.InteropServices;

namespace System.Threading.Tasks;

/// <summary>Provides a set of static (Shared in Visual Basic) methods for working with specific kinds of <see cref="T:System.Threading.Tasks.Task" /> instances.</summary>
public static class TaskExtensions
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	private struct VoidResult
	{
	}

	/// <summary>Creates a proxy <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of a <see cref="M:System.Threading.Tasks.TaskScheduler.TryExecuteTaskInline(System.Threading.Tasks.Task,System.Boolean)" />.</summary>
	/// <param name="task">The <see langword="Task&lt;Task&gt;" /> (C#) or <see langword="Task (Of Task)" /> (Visual Basic) to unwrap.</param>
	/// <returns>A Task that represents the asynchronous operation of the provided <see langword="System.Threading.Tasks.Task(Of Task)" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The exception that is thrown if the <paramref name="task" /> argument is null.</exception>
	public static Task Unwrap(this Task<Task> task)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		return Task.CreateUnwrapPromise<VoidResult>(task, lookForOce: false);
	}

	/// <summary>Creates a proxy <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of a <see langword="Task&lt;Task&lt;T&gt;&gt;" /> (C#) or <see langword="Task (Of Task(Of T))" /> (Visual Basic).</summary>
	/// <param name="task">The <see langword="Task&lt;Task&lt;T&gt;&gt;" /> (C#) or <see langword="Task (Of Task(Of T))" /> (Visual Basic) to unwrap.</param>
	/// <typeparam name="TResult">The type of the task's result.</typeparam>
	/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation of the provided <see langword="Task&lt;Task&lt;T&gt;&gt;" /> (C#) or <see langword="Task (Of Task(Of T))" /> (Visual Basic).</returns>
	/// <exception cref="T:System.ArgumentNullException">The exception that is thrown if the <paramref name="task" /> argument is null.</exception>
	public static Task<TResult> Unwrap<TResult>(this Task<Task<TResult>> task)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		return Task.CreateUnwrapPromise<TResult>(task, lookForOce: false);
	}
}
