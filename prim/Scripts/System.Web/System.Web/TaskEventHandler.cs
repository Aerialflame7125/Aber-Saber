using System.Threading.Tasks;

namespace System.Web;

/// <summary>Represents the asynchronous task that is being processed by an instance of the <see cref="T:System.Web.EventHandlerTaskAsyncHelper" /> class.</summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The event data.</param>
/// <returns>The asynchronous task.</returns>
public delegate Task TaskEventHandler(object sender, EventArgs e);
