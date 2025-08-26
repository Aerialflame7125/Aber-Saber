using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides the base class for implementing a CodeDOM-based designer loader.</summary>
public abstract class CodeDomDesignerLoader : BasicDesignerLoader, INameCreationService, IDesignerSerializationService
{
	private CodeDomSerializer _rootSerializer;

	/// <summary>Gets the <see cref="P:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.CodeDomProvider" /> this designer loader will use.</summary>
	/// <returns>The <see cref="P:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.CodeDomProvider" /> this designer loader will use</returns>
	protected abstract CodeDomProvider CodeDomProvider { get; }

	/// <summary>Gets the type resolution service to be used with this designer loader.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.Design.ITypeResolutionService" /> that the CodeDOM serializers will use when resolving types.</returns>
	protected abstract ITypeResolutionService TypeResolutionService { get; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader" /> class.</summary>
	protected CodeDomDesignerLoader()
	{
	}

	/// <summary>Initializes services.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerLoaderHost" /> has not been initialized, or the designer loader did not supply a type resolution service, which is required for CodeDom serialization.</exception>
	protected override void Initialize()
	{
		base.Initialize();
		base.LoaderHost.AddService(typeof(IDesignerSerializationService), this);
		base.LoaderHost.AddService(typeof(INameCreationService), this);
		base.LoaderHost.AddService(typeof(ComponentSerializationService), new CodeDomComponentSerializationService(base.LoaderHost));
		if (TypeResolutionService != null && base.LoaderHost.GetService(typeof(ITypeResolutionService)) == null)
		{
			base.LoaderHost.AddService(typeof(ITypeResolutionService), TypeResolutionService);
		}
		if (base.LoaderHost.GetService(typeof(IDesignerSerializationManager)) is IDesignerSerializationManager designerSerializationManager)
		{
			designerSerializationManager.AddSerializationProvider(CodeDomSerializationProvider.Instance);
		}
	}

	/// <summary>Returns a value indicating whether a reload is required.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="P:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.CodeDomProvider" /> decides a reload is required; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The language did not provide a code parser for this file; this file type may not support a designer.</exception>
	/// <exception cref="T:System.InvalidOperationException">The class can be designed, but it is not the first class in the file, or the designer could not be shown for this file because none of the classes within it can be designed.</exception>
	protected override bool IsReloadNeeded()
	{
		if (CodeDomProvider is ICodeDomDesignerReload)
		{
			return ((ICodeDomDesignerReload)CodeDomProvider).ShouldReloadDesigner(Parse());
		}
		return base.IsReloadNeeded();
	}

	/// <summary>Parses code from a CodeDOM provider.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> from which to request the serializer.</param>
	/// <exception cref="T:System.NotSupportedException">The language did not provide a code parser for this file; this file type may not support a designer.</exception>
	/// <exception cref="T:System.InvalidOperationException">The class can be designed, but it is not the first class in the file, or the designer could not be shown for this file because none of the classes within it can be designed.</exception>
	protected override void PerformLoad(IDesignerSerializationManager manager)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		CodeCompileUnit codeCompileUnit = Parse();
		if (codeCompileUnit == null)
		{
			throw new NotSupportedException("The language did not provide a code parser for this file");
		}
		string namespaceName = null;
		CodeTypeDeclaration firstCodeTypeDecl = GetFirstCodeTypeDecl(codeCompileUnit, out namespaceName);
		if (firstCodeTypeDecl == null)
		{
			throw new InvalidOperationException("Cannot find a declaration in a namespace to load.");
		}
		_rootSerializer = manager.GetSerializer(manager.GetType(firstCodeTypeDecl.BaseTypes[0].BaseType), typeof(RootCodeDomSerializer)) as CodeDomSerializer;
		if (_rootSerializer == null)
		{
			throw new InvalidOperationException("Serialization not supported for this class");
		}
		_rootSerializer.Deserialize(manager, firstCodeTypeDecl);
		SetBaseComponentClassName(namespaceName + "." + firstCodeTypeDecl.Name);
	}

	private CodeTypeDeclaration GetFirstCodeTypeDecl(CodeCompileUnit document, out string namespaceName)
	{
		namespaceName = null;
		foreach (CodeNamespace @namespace in document.Namespaces)
		{
			foreach (CodeTypeDeclaration type in @namespace.Types)
			{
				if (type.IsClass)
				{
					namespaceName = @namespace.Name;
					return type;
				}
			}
		}
		return null;
	}

	/// <summary>Requests serialization of the root component of the designer.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> from which to request the serializer.</param>
	/// <exception cref="T:System.NotSupportedException">The language did not provide a code parser for this file; this file type may not support a designer.</exception>
	/// <exception cref="T:System.InvalidOperationException">The class can be designed, but it is not the first class in the file, or the designer could not be shown for this file because none of the classes within it can be designed.</exception>
	protected override void PerformFlush(IDesignerSerializationManager manager)
	{
		if (_rootSerializer != null)
		{
			CodeTypeDeclaration typeDecl = (CodeTypeDeclaration)_rootSerializer.Serialize(manager, base.LoaderHost.RootComponent);
			Write(MergeTypeDeclWithCompileUnit(typeDecl, Parse()));
		}
	}

	private CodeCompileUnit MergeTypeDeclWithCompileUnit(CodeTypeDeclaration typeDecl, CodeCompileUnit unit)
	{
		CodeNamespace codeNamespace = null;
		int num = -1;
		foreach (CodeNamespace @namespace in unit.Namespaces)
		{
			for (int i = 0; i < @namespace.Types.Count; i++)
			{
				if (@namespace.Types[i].IsClass)
				{
					num = i;
					codeNamespace = @namespace;
				}
			}
		}
		if (num != -1)
		{
			codeNamespace.Types.RemoveAt(num);
		}
		codeNamespace.Types.Add(typeDecl);
		return unit;
	}

	/// <summary>Notifies the designer loader that loading is about to begin.</summary>
	protected override void OnBeginLoad()
	{
		base.OnBeginLoad();
		if (GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.ComponentRename += OnComponentRename_EventHandler;
		}
	}

	/// <summary>Notifies the designer loader that unloading is about to begin.</summary>
	protected override void OnBeginUnload()
	{
		base.OnBeginUnload();
		if (GetService(typeof(IComponentChangeService)) is IComponentChangeService componentChangeService)
		{
			componentChangeService.ComponentRename -= OnComponentRename_EventHandler;
		}
	}

	/// <summary>Notifies the designer loader that loading is complete.</summary>
	/// <param name="successful">
	///   <see langword="true" /> to indicate that the load completed successfully; otherwise, <see langword="false" />.</param>
	/// <param name="errors">An <see cref="T:System.Collections.ICollection" /> of objects (usually exceptions) that were reported as errors.</param>
	protected override void OnEndLoad(bool successful, ICollection errors)
	{
		base.OnEndLoad(successful, errors);
	}

	private void OnComponentRename_EventHandler(object sender, ComponentRenameEventArgs args)
	{
		OnComponentRename(args.Component, args.OldName, args.NewName);
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	/// <param name="component">The component to rename.</param>
	/// <param name="oldName">The original name of the component.</param>
	/// <param name="newName">The new name of the component.</param>
	protected virtual void OnComponentRename(object component, string oldName, string newName)
	{
	}

	/// <summary>Parses the text or other persistent storage and returns a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
	/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> resulting from a parse operation.</returns>
	protected abstract CodeCompileUnit Parse();

	/// <summary>Writes compile-unit changes to persistent storage.</summary>
	/// <param name="unit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> to be persisted.</param>
	protected abstract void Write(CodeCompileUnit unit);

	/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomDesignerLoader" /> class.</summary>
	public override void Dispose()
	{
		base.Dispose();
	}

	/// <summary>Creates a new name that is unique to all components in the specified container.</summary>
	/// <param name="container">The container where the new object is added.</param>
	/// <param name="dataType">The data type of the object that receives the name.</param>
	/// <returns>A unique name for the data type.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataType" /> is <see langword="null" />.</exception>
	string INameCreationService.CreateName(IContainer container, Type dataType)
	{
		if (dataType == null)
		{
			throw new ArgumentNullException("dataType");
		}
		string name = dataType.Name;
		char c = char.ToLower(name[0]);
		name = name.Remove(0, 1);
		name = name.Insert(0, char.ToString(c));
		int num = 1;
		bool flag = false;
		while (!flag)
		{
			if (container != null && container.Components[name + num] != null)
			{
				num++;
				continue;
			}
			flag = true;
			name += num;
		}
		if (CodeDomProvider != null)
		{
			name = CodeDomProvider.CreateValidIdentifier(name);
		}
		return name;
	}

	/// <summary>Gets a value indicating whether the specified name is valid.</summary>
	/// <param name="name">The name to validate.</param>
	/// <returns>
	///   <see langword="true" /> if the name is valid; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="name" /> is <see langword="null" />.</exception>
	bool INameCreationService.IsValidName(string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		bool result = true;
		if (base.LoaderHost != null && base.LoaderHost.Container.Components[name] != null)
		{
			result = false;
		}
		else if (CodeDomProvider != null)
		{
			result = CodeDomProvider.IsValidIdentifier(name);
		}
		else
		{
			if (name.Trim().Length == 0)
			{
				result = false;
			}
			for (int i = 0; i < name.Length; i++)
			{
				if (!char.IsLetterOrDigit(name[i]))
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	/// <summary>Gets a value indicating whether the specified name is valid.</summary>
	/// <param name="name">The name to validate.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="name" /> is not a valid identifier, or there is already a component with the same name.</exception>
	void INameCreationService.ValidateName(string name)
	{
		if (!((INameCreationService)this).IsValidName(name))
		{
			throw new ArgumentException("Invalid name '" + name + "'");
		}
	}

	/// <summary>Deserializes the specified serialization data object and returns a collection of objects represented by that data.</summary>
	/// <param name="serializationData">An object consisting of serialized data.</param>
	/// <returns>A collection of objects represented by <paramref name="serializationData" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="serializationData" /> is not a <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</exception>
	ICollection IDesignerSerializationService.Deserialize(object serializationData)
	{
		if (serializationData == null)
		{
			throw new ArgumentNullException("serializationData");
		}
		ComponentSerializationService componentSerializationService = base.LoaderHost.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
		SerializationStore store = serializationData as SerializationStore;
		if (componentSerializationService != null && serializationData != null)
		{
			return componentSerializationService.Deserialize(store, base.LoaderHost.Container);
		}
		return new object[0];
	}

	/// <summary>Serializes the specified collection of objects and stores them in a serialization data object.</summary>
	/// <param name="objects">A collection of objects to serialize.</param>
	/// <returns>An object that contains the serialized state of the specified collection of objects.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" /> was not found.</exception>
	object IDesignerSerializationService.Serialize(ICollection objects)
	{
		if (objects == null)
		{
			throw new ArgumentNullException("objects");
		}
		if (base.LoaderHost.GetService(typeof(ComponentSerializationService)) is ComponentSerializationService componentSerializationService)
		{
			SerializationStore serializationStore = componentSerializationService.CreateStore();
			foreach (object @object in objects)
			{
				componentSerializationService.Serialize(serializationStore, @object);
			}
			serializationStore.Close();
			return serializationStore;
		}
		return null;
	}
}
