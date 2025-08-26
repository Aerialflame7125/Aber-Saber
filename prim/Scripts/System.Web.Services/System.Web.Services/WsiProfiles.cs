namespace System.Web.Services;

/// <summary>Describes the Web services interoperability (WSI) specification to which a Web service claims to conform.</summary>
[Flags]
public enum WsiProfiles
{
	/// <summary>The web service makes no conformance claims.</summary>
	None = 0,
	/// <summary>The web service claims to conform to the WSI Basic Profile version 1.1.</summary>
	BasicProfile1_1 = 1
}
