using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Compilation;

[Serializable]
internal class CompilationException : HtmlizedException
{
	private string filename;

	private CompilerErrorCollection errors;

	private CompilerResults results;

	private string fileText;

	private string errmsg;

	private int[] errorLines;

	public override string Message => ErrorMessage;

	public override string SourceFile
	{
		get
		{
			if (errors == null || errors.Count == 0)
			{
				return filename;
			}
			return errors[0].FileName;
		}
	}

	public override string FileName => filename;

	public override string Title => "Compilation Error";

	public override string Description => "Error compiling a resource required to service this request. Review your source file and modify it to fix this error.";

	public override string ErrorMessage
	{
		get
		{
			if (errmsg == null && errors != null)
			{
				CompilerError compilerError = null;
				foreach (CompilerError error in errors)
				{
					if (!error.IsWarning)
					{
						compilerError = error;
						break;
					}
				}
				if (compilerError != null)
				{
					errmsg = compilerError.ToString();
					int num = errmsg.IndexOf(" : error ");
					if (num > -1)
					{
						errmsg = errmsg.Substring(num + 9);
					}
				}
				else
				{
					errmsg = string.Empty;
				}
			}
			return errmsg;
		}
	}

	public override string FileText => fileText;

	public override int[] ErrorLines
	{
		get
		{
			if (errorLines == null && errors != null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (CompilerError error in errors)
				{
					if (!error.IsWarning && error.Line != 0 && !arrayList.Contains(error.Line))
					{
						arrayList.Add(error.Line);
					}
				}
				errorLines = (int[])arrayList.ToArray(typeof(int));
				Array.Sort(errorLines);
			}
			return errorLines;
		}
	}

	public override bool ErrorLinesPaired => false;

	public StringCollection CompilerOutput
	{
		get
		{
			if (results == null)
			{
				return null;
			}
			return results.Output;
		}
	}

	public CompilerResults Results => results;

	private CompilationException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		filename = info.GetString("filename");
		errors = info.GetValue("errors", typeof(CompilerErrorCollection)) as CompilerErrorCollection;
		results = info.GetValue("results", typeof(CompilerResults)) as CompilerResults;
		fileText = info.GetString("fileText");
		errmsg = info.GetString("errmsg");
		errorLines = info.GetValue("errorLines", typeof(int[])) as int[];
	}

	public CompilationException(string filename, CompilerErrorCollection errors, string fileText)
	{
		this.filename = filename;
		this.errors = errors;
		this.fileText = fileText;
	}

	public CompilationException(string filename, CompilerResults results, string fileText)
		: this(filename, results?.Errors, fileText)
	{
		this.results = results;
	}

	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
	{
		base.GetObjectData(info, ctx);
		info.AddValue("filename", filename);
		info.AddValue("errors", errors);
		info.AddValue("results", results);
		info.AddValue("fileText", fileText);
		info.AddValue("errmsg", errmsg);
		info.AddValue("errorLines", errorLines);
	}
}
