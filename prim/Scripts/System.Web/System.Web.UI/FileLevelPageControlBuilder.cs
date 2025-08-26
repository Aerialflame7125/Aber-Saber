using System.Web.UI.WebControls;

namespace System.Web.UI;

/// <summary>Parses page files and is the default <see cref="T:System.Web.UI.ControlBuilder" /> class for parsing page files.</summary>
public class FileLevelPageControlBuilder : RootBuilder
{
	private bool hasContentControls;

	private bool hasLiteralControls;

	private bool hasOtherControls;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.FileLevelPageControlBuilder" /> class. </summary>
	public FileLevelPageControlBuilder()
	{
	}

	/// <summary>Adds the specified literal content to a control. </summary>
	/// <param name="text">The content to add to the control.</param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.FileLevelPageControlBuilder.AppendLiteralString(System.String)" /> method cannot append the literal string to a content page.</exception>
	public override void AppendLiteralString(string text)
	{
		bool flag = text == null || text.Trim().Length == 0;
		if (hasContentControls && !flag)
		{
			throw new HttpException("Literal strings cannot be appended to Content pages.");
		}
		if (!flag)
		{
			hasLiteralControls = true;
		}
		base.AppendLiteralString(text);
	}

	/// <summary>Adds a <see cref="T:System.Web.UI.ControlBuilder" /> object to the <see cref="T:System.Web.UI.FileLevelPageControlBuilder" /> object for any child controls that belong to the container control.</summary>
	/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> assigned to the child control. </param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.ControlBuilder" /> that was added is associated with a <see cref="T:System.Web.UI.WebControls.Content" /> control and is only allowed on pages that contain <see cref="T:System.Web.UI.WebControls.Content" /> controls.</exception>
	/// <exception cref="T:System.Web.HttpParseException">The content page contained a literal other than a <see cref="T:System.Web.UI.WebControls.Content" /> control.</exception>
	public override void AppendSubBuilder(ControlBuilder subBuilder)
	{
		if (subBuilder == null)
		{
			base.AppendSubBuilder(subBuilder);
			return;
		}
		if (typeof(ContentBuilderInternal).IsAssignableFrom(subBuilder.GetType()))
		{
			if (hasOtherControls)
			{
				throw new HttpException("Only Content controls are supported on content pages.");
			}
			hasContentControls = true;
			if (hasLiteralControls)
			{
				throw new HttpParseException("Only Content controls are supported on content pages.");
			}
		}
		else
		{
			hasOtherControls = true;
		}
		base.AppendSubBuilder(subBuilder);
	}
}
