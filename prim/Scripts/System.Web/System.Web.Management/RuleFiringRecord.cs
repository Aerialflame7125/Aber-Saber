namespace System.Web.Management;

/// <summary>Represents the firing record for an event that derives from the <see cref="T:System.Web.Management.WebManagementEvent" /> class and implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface.</summary>
public sealed class RuleFiringRecord
{
	/// <summary>Gets the last time that the event was last fired.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> object representing when the event was last fired.</returns>
	public DateTime LastFired
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the total number of times that the event has been raised.</summary>
	/// <returns>The total number of times the event has been raised.</returns>
	public int TimesRaised
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	internal RuleFiringRecord()
	{
	}
}
