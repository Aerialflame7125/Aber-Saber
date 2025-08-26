namespace System.Web.Compilation;

/// <summary>Contains properties for a script block being parsed.</summary>
[Serializable]
public sealed class LinePragmaCodeInfo
{
	/// <summary>Gets the length of the script block.</summary>
	/// <returns>The length of the script block.</returns>
	[MonoTODO("Not implemented")]
	public int CodeLength
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the script block is located inside &lt;% %&gt; tags.</summary>
	/// <returns>
	///     <see langword="true" /> if the script block is contained inside &lt;% %&gt; tags; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool IsCodeNugget
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the starting column of a script block in an .aspx file.</summary>
	/// <returns>The starting column of a script block in an .aspx file.</returns>
	[MonoTODO("Not implemented")]
	public int StartColumn
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the starting column of a script block in the generated source file.</summary>
	/// <returns>The starting column of a script block in the generated source file.</returns>
	[MonoTODO("Not implemented")]
	public int StartGeneratedColumn
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the starting line of a script block in an .aspx file.</summary>
	/// <returns>The starting line of a script block in an .aspx file.</returns>
	[MonoTODO("Not implemented")]
	public int StartLine
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.LinePragmaCodeInfo" /> class. </summary>
	public LinePragmaCodeInfo()
	{
	}
}
