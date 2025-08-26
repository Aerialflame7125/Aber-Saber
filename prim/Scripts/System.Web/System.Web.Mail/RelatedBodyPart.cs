using System.IO;

namespace System.Web.Mail;

public class RelatedBodyPart
{
	private string id;

	private string fileName;

	public string Name
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	public string Path
	{
		get
		{
			return fileName;
		}
		set
		{
			fileName = value;
		}
	}

	public RelatedBodyPart(string id, string fileName)
	{
		this.id = id;
		if (FileExists(fileName))
		{
			this.fileName = fileName;
			return;
		}
		throw new HttpException(500, "Invalid related body part");
	}

	private bool FileExists(string fileName)
	{
		try
		{
			File.OpenRead(fileName).Close();
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
