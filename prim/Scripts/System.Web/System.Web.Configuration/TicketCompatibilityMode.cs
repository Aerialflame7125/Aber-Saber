namespace System.Web.Configuration;

/// <summary>Defines whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date for forms authentication.</summary>
public enum TicketCompatibilityMode
{
	/// <summary>Specifies that the ticket expiration date is stored as local time. This is the default value.</summary>
	Framework20,
	/// <summary>Specifies that the ticket expiration date is stored as UTC.</summary>
	Framework40
}
