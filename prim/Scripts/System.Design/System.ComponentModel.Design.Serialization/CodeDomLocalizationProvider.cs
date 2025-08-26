using System.Globalization;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides CodeDOM resource serialization services. This class cannot be inherited.</summary>
public sealed class CodeDomLocalizationProvider : IDisposable, IDesignerSerializationProvider
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationProvider" /> class.</summary>
	/// <param name="provider">An <see cref="T:System.IServiceProvider" /> used by the localization provider to add its extender properties.</param>
	/// <param name="model">A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationModel" /> value indicating the localization model to be used by the CodeDOM resource adapter</param>
	[System.MonoTODO]
	public CodeDomLocalizationProvider(IServiceProvider provider, CodeDomLocalizationModel model)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationProvider" /> class.</summary>
	/// <param name="provider">An <see cref="T:System.IServiceProvider" /> used by the localization provider to add its extender properties.</param>
	/// <param name="model">A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationModel" /> value indicating the localization model to be used by the CodeDOM resource adapter</param>
	/// <param name="supportedCultures">An array of cultures that this resource adapter should support.</param>
	[System.MonoTODO]
	public CodeDomLocalizationProvider(IServiceProvider provider, CodeDomLocalizationModel model, CultureInfo[] supportedCultures)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomLocalizationProvider" />.</summary>
	[System.MonoTODO]
	public void Dispose()
	{
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.Serialization.IDesignerSerializationProvider.GetSerializer(System.ComponentModel.Design.Serialization.IDesignerSerializationManager,System.Object,System.Type,System.Type)" />.</summary>
	/// <param name="manager">The serialization manager requesting the serializer.</param>
	/// <param name="currentSerializer">An instance of the current serializer of the specified type. This can be <see langword="null" /> if no serializer of the specified type exists.</param>
	/// <param name="objectType">The data type of the object to serialize.</param>
	/// <param name="serializerType">The data type of the serializer to create.</param>
	/// <returns>An instance of a serializer of the type requested, or <see langword="null" /> if the request cannot be satisfied.</returns>
	[System.MonoTODO]
	object IDesignerSerializationProvider.GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
	{
		throw new NotImplementedException();
	}
}
