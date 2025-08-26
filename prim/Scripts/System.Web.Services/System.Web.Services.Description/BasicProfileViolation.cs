using System.Collections.Specialized;
using System.Text;

namespace System.Web.Services.Description;

/// <summary>Represents a WSDL violation of the WSI Basic Profile version 1.1.</summary>
public class BasicProfileViolation
{
	private WsiProfiles claims = WsiProfiles.BasicProfile1_1;

	private string normativeStatement;

	private string details;

	private string recommendation;

	private StringCollection elements;

	/// <summary>Gets a <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service declares that it conforms to the WSI Basic Profile version 1.1.</summary>
	/// <returns>A <see cref="T:System.Web.Services.WsiProfiles" /> object that specifies whether the Web service declares that it conforms to the WSI Basic Profile version 1.1.</returns>
	public WsiProfiles Claims => claims;

	/// <summary>Gets a <see cref="T:System.String" /> that provides a detailed description of the WSDL violation of the Basic Profile.</summary>
	/// <returns>A <see cref="T:System.String" /> that provides a detailed description of the WSDL violation of the Basic Profile.</returns>
	public string Details
	{
		get
		{
			if (details == null)
			{
				return string.Empty;
			}
			return details;
		}
	}

	/// <summary>Represents WSDL elements that do not comply with the WSI Basic Profile version 1.1 specification.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the WSDL elements that do not comply with the WSI Basic Profile version 1.1 specification.</returns>
	public StringCollection Elements
	{
		get
		{
			if (elements == null)
			{
				elements = new StringCollection();
			}
			return elements;
		}
	}

	/// <summary>Gets the identifier for the WSDL violation of the Basic Profile version 1.1 specification. </summary>
	/// <returns>A <see cref="T:System.String" /> that contains the identifier (For example, R2038) for the WSDL violation of the Basic Profile version 1.1 specification. </returns>
	public string NormativeStatement => normativeStatement;

	/// <summary>Gets a <see cref="T:System.String" /> object that describes the WSDL violation of the Basic Profile version 1.1 specification.</summary>
	/// <returns>The <see cref="T:System.String" /> object that describes the WSDL violation of the Basic Profile version 1.1 specification.</returns>
	public string Recommendation => recommendation;

	internal BasicProfileViolation(string normativeStatement)
		: this(normativeStatement, null)
	{
	}

	internal BasicProfileViolation(string normativeStatement, string element)
	{
		this.normativeStatement = normativeStatement;
		int num = normativeStatement.IndexOf(',');
		if (num >= 0)
		{
			normativeStatement = normativeStatement.Substring(0, num);
		}
		details = Res.GetString("HelpGeneratorServiceConformance" + normativeStatement);
		recommendation = Res.GetString("HelpGeneratorServiceConformance" + normativeStatement + "_r");
		if (element != null)
		{
			Elements.Add(element);
		}
		if (this.normativeStatement == "Rxxxx")
		{
			this.normativeStatement = Res.GetString("Rxxxx");
		}
	}

	/// <summary>Returns a <see cref="T:System.String" /> that comprises information from <see cref="P:System.Web.Services.Description.BasicProfileViolation.NormativeStatement" />, <see cref="P:System.Web.Services.Description.BasicProfileViolation.Details" />, and <see cref="P:System.Web.Services.Description.BasicProfileViolation.Elements" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that comprises information from <see cref="P:System.Web.Services.Description.BasicProfileViolation.NormativeStatement" />, <see cref="P:System.Web.Services.Description.BasicProfileViolation.Details" />, and <see cref="P:System.Web.Services.Description.BasicProfileViolation.Elements" />.</returns>
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(normativeStatement);
		stringBuilder.Append(": ");
		stringBuilder.Append(Details);
		StringEnumerator enumerator = Elements.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("  -  ");
				stringBuilder.Append(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
		return stringBuilder.ToString();
	}
}
