using System.CodeDom;
using System.Reflection;

namespace System.Windows.Forms.Design;

/// <summary>Represents a parameter of a method of a hosted ActiveX control.</summary>
public class AxParameterData
{
	private bool isByRef;

	private bool isIn;

	private bool isOptional;

	private bool isOut;

	private string name;

	private Type type;

	/// <summary>Indicates the direction of assignment fields.</summary>
	/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> indicating the direction of assignment fields.</returns>
	public FieldDirection Direction
	{
		get
		{
			if (IsOut)
			{
				return FieldDirection.Out;
			}
			if (IsByRef)
			{
				return FieldDirection.Ref;
			}
			return FieldDirection.In;
		}
	}

	/// <summary>Indicates whether the parameter data is passed by reference.</summary>
	/// <returns>
	///   <see langword="true" /> if the parameter data is by reference; otherwise, <see langword="false" />.</returns>
	public bool IsByRef => isByRef;

	/// <summary>Indicates whether the parameter data is in.</summary>
	/// <returns>
	///   <see langword="true" /> if the parameter data is in; otherwise, <see langword="false" />.</returns>
	public bool IsIn => isIn;

	/// <summary>Indicates whether the parameter data is optional.</summary>
	/// <returns>
	///   <see langword="true" /> if the parameter data is optional; otherwise, <see langword="false" />.</returns>
	public bool IsOptional => isOptional;

	/// <summary>Indicates whether the parameter data is out.</summary>
	/// <returns>
	///   <see langword="true" /> if the parameter data is out; otherwise, <see langword="false" />.</returns>
	public bool IsOut => isOut;

	/// <summary>Gets or sets the name of the parameter.</summary>
	/// <returns>The name of the parameter.</returns>
	public string Name
	{
		get
		{
			return name;
		}
		[System.MonoTODO]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the type expected by the parameter.</summary>
	/// <returns>The type expected by the parameter.</returns>
	public Type ParameterType => type;

	/// <summary>Gets the name of the type expected by the parameter.</summary>
	/// <returns>The name of the type expected by the parameter.</returns>
	[System.MonoTODO]
	public string TypeName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified parameter information.</summary>
	/// <param name="info">A <see cref="T:System.Reflection.ParameterInfo" /> indicating the parameter information to use.</param>
	[System.MonoTODO]
	public AxParameterData(ParameterInfo info)
		: this(info, ignoreByRefs: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified parameter information and whether to ignore by reference parameters.</summary>
	/// <param name="info">A <see cref="T:System.Reflection.ParameterInfo" /> indicating the parameter information to use.</param>
	/// <param name="ignoreByRefs">A value indicating whether to ignore parameters passed by reference.</param>
	[System.MonoTODO]
	public AxParameterData(ParameterInfo info, bool ignoreByRefs)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified name and type name.</summary>
	/// <param name="inname">The name of the parameter.</param>
	/// <param name="typeName">The name of the type of the parameter.</param>
	[System.MonoTODO]
	public AxParameterData(string inname, string typeName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.AxParameterData" /> class using the specified name and type.</summary>
	/// <param name="inname">The name of the parameter.</param>
	/// <param name="type">The type of the parameter.</param>
	[System.MonoTODO]
	public AxParameterData(string inname, Type type)
	{
		throw new NotImplementedException();
	}

	/// <summary>Converts the specified parameter information to an <see cref="T:System.Windows.Forms.Design.AxParameterData" /> object.</summary>
	/// <param name="infos">An array of <see cref="T:System.Reflection.ParameterInfo" /> objects to convert.</param>
	/// <returns>An array of <see cref="T:System.Windows.Forms.Design.AxParameterData" /> objects representing the specified array of <see cref="T:System.Reflection.ParameterInfo" /> objects.</returns>
	[System.MonoTODO]
	public static AxParameterData[] Convert(ParameterInfo[] infos)
	{
		throw new NotImplementedException();
	}

	/// <summary>Converts the specified parameter information to an <see cref="T:System.Windows.Forms.Design.AxParameterData" /> object, according to the specified value indicating whether to ignore by reference parameters.</summary>
	/// <param name="infos">An array of <see cref="T:System.Reflection.ParameterInfo" /> objects to convert.</param>
	/// <param name="ignoreByRefs">A value indicating whether to ignore parameters passed by reference.</param>
	/// <returns>An array of <see cref="T:System.Windows.Forms.Design.AxParameterData" /> objects representing the specified array of <see cref="T:System.Reflection.ParameterInfo" /> objects.</returns>
	[System.MonoTODO]
	public static AxParameterData[] Convert(ParameterInfo[] infos, bool ignoreByRefs)
	{
		throw new NotImplementedException();
	}
}
