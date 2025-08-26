using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Resources;

/// <summary>Represents an element in a resource file.</summary>
[Serializable]
public sealed class ResXDataNode : ISerializable
{
	private string name;

	private object value;

	private Type type;

	private ResXFileRef fileRef;

	private string comment;

	private Point pos;

	/// <summary>Gets or sets an arbitrary comment regarding this resource. </summary>
	/// <returns>A <see cref="T:System.String" /> representing the comment.</returns>
	public string Comment
	{
		get
		{
			return comment;
		}
		set
		{
			comment = value;
		}
	}

	/// <summary>Gets the file reference for this resource.</summary>
	/// <returns>The <see cref="T:System.Resources.ResXFileRef" /> corresponding to the file reference, if this resource uses one. If this resource stores its value as an <see cref="T:System.Object" />, this property will return null.</returns>
	public ResXFileRef FileRef => fileRef;

	/// <summary>Gets or sets the name of this resource.</summary>
	/// <returns>A <see cref="T:System.String" /> corresponding to the resource name. If no name is assigned, returns a zero-length string.</returns>
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	internal object Value => value;

	/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXDataNode" /> class. </summary>
	/// <param name="name">The name of the resource.</param>
	/// <param name="value">The resource to store. </param>
	/// <exception cref="T:System.InvalidOperationException">The resource named in <paramref name="value" /> does not support serialization. </exception>
	public ResXDataNode(string name, object value)
		: this(name, value, Point.Empty)
	{
	}

	/// <summary>This overload of the <see cref="T:System.Resources.ResXDataNode" /> constructor allows you to create a reference to a file, and store that file as a resource for your application.</summary>
	/// <param name="name">The name of the resource.</param>
	/// <param name="fileRef">The file reference to use as the resource.</param>
	public ResXDataNode(string name, ResXFileRef fileRef)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (fileRef == null)
		{
			throw new ArgumentNullException("fileRef");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("name");
		}
		this.name = name;
		this.fileRef = fileRef;
		pos = Point.Empty;
	}

	internal ResXDataNode(string name, object value, Point position)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		if (name.Length == 0)
		{
			throw new ArgumentException("name");
		}
		Type type = ((value != null) ? value.GetType() : typeof(object));
		if (value != null && !type.IsSerializable)
		{
			throw new InvalidOperationException($"'{name}' of type '{type}' cannot be added because it is not serializable");
		}
		this.type = type;
		this.name = name;
		this.value = value;
		pos = position;
	}

	/// <summary>Retrieves the object's data.</summary>
	/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
	void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
	{
		si.AddValue("Name", Name);
		si.AddValue("Comment", Comment);
	}

	/// <summary>Gets the position of the resource in the resource file. </summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> structure specifying the location of this resource in the resource file as a line position (<see cref="P:System.Drawing.Point.X" />) and a column position (<see cref="P:System.Drawing.Point.Y" />). If this resource is not part of a resource file, returns a <see cref="T:System.Drawing.Point" /> structure with an <see cref="P:System.Drawing.Point.X" /> of 0 and a <see cref="P:System.Drawing.Point.Y" /> of 0. </returns>
	public Point GetNodePosition()
	{
		return pos;
	}

	/// <summary>Gets the name of the type of the value.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the fully qualified name of the type.</returns>
	/// <param name="names">The assemblies to examine for the type. </param>
	/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found. </exception>
	[System.MonoInternalNote("Move the type parsing process from ResxResourceReader")]
	public string GetValueTypeName(AssemblyName[] names)
	{
		return type.AssemblyQualifiedName;
	}

	/// <summary>A <see cref="T:System.String" /> representing the fully qualified name of the type.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the fully qualified name of the type.</returns>
	/// <param name="typeResolver">The type resolution service to use to locate a converter for this type. </param>
	/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found. </exception>
	[System.MonoInternalNote("Move the type parsing process from ResxResourceReader")]
	public string GetValueTypeName(ITypeResolutionService typeResolver)
	{
		return type.AssemblyQualifiedName;
	}

	/// <summary>Gets the object stored by this node.</summary>
	/// <returns>The <see cref="T:System.Object" /> corresponding to the stored value. </returns>
	/// <param name="names">The list of assemblies in which to look for the type of the object.</param>
	/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found, or an appropriate type converter is not available.</exception>
	[System.MonoInternalNote("Move the value parsing process from ResxResourceReader")]
	public object GetValue(AssemblyName[] names)
	{
		return value;
	}

	/// <summary>Gets the object stored by this node.</summary>
	/// <returns>The <see cref="T:System.Object" /> corresponding to the stored value. </returns>
	/// <param name="typeResolver">The type resolution service to use when looking for a type converter.</param>
	/// <exception cref="T:System.TypeLoadException">The corresponding type could not be found, or an appropriate type converter is not available.</exception>
	[System.MonoInternalNote("Move the value parsing process from ResxResourceReader")]
	public object GetValue(ITypeResolutionService typeResolver)
	{
		return value;
	}
}
