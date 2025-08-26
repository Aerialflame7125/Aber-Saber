using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Wraps ActiveX controls and exposes them as fully featured Windows Forms controls.</summary>
/// <filterpriority>2</filterpriority>
[Designer("System.Windows.Forms.Design.AxHostDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DesignTimeVisible(false)]
[DefaultEvent("Enter")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[System.MonoTODO("Possibly implement this for Win32; find a way for Linux and Mac")]
[ToolboxItem(false)]
[ComVisible(true)]
public abstract class AxHost : Control, ISupportInitialize, ICustomTypeDescriptor
{
	/// <summary>Specifies the type of member that referenced the ActiveX control while it was in an invalid state.</summary>
	public enum ActiveXInvokeKind
	{
		/// <summary>A method referenced the ActiveX control.</summary>
		MethodInvoke,
		/// <summary>The get accessor of a property referenced the ActiveX control.</summary>
		PropertyGet,
		/// <summary>The set accessor of a property referenced the ActiveX control.</summary>
		PropertySet
	}

	/// <summary>Provides an editor that uses a modal dialog box to display a property page for an ActiveX control.</summary>
	[ComVisible(false)]
	public class AxComponentEditor : WindowsFormsComponentEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.AxComponentEditor" /> class. </summary>
		public AxComponentEditor()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <returns>true if the component was changed during editing; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="obj"></param>
		/// <param name="parent"></param>
		public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>Specifies the CLSID of an ActiveX control hosted by an <see cref="T:System.Windows.Forms.AxHost" /> control.</summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class ClsidAttribute : Attribute
	{
		private string clsid;

		/// <summary>The CLSID of the ActiveX control.</summary>
		/// <returns>The CLSID of the ActiveX control.</returns>
		public string Value => clsid;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.ClsidAttribute" /> class. </summary>
		/// <param name="clsid">The CLSID of the ActiveX control.</param>
		public ClsidAttribute(string clsid)
		{
			this.clsid = clsid;
		}
	}

	/// <summary>Connects an ActiveX control to a client that handles the control’s events.</summary>
	public class ConnectionPointCookie
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.ConnectionPointCookie" /> class.</summary>
		/// <param name="source">A connectable object that contains connection points.</param>
		/// <param name="sink">The client's sink which receives outgoing calls from the connection point.</param>
		/// <param name="eventInterface">The outgoing interface whose connection point object is being requested.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> does not implement <paramref name="eventInterface" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="sink" /> does not implement <paramref name="eventInterface" />.-or-<paramref name="source" /> does not implement <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPointContainer" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The connection point has already reached its limit of connections and cannot accept any more.</exception>
		public ConnectionPointCookie(object source, object sink, Type eventInterface)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Disconnects the ActiveX control from the client.</summary>
		public void Disconnect()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.AxHost.ConnectionPointCookie" /> class.</summary>
		~ConnectionPointCookie()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>The exception that is thrown when the ActiveX control is referenced while in an invalid state.</summary>
	public class InvalidActiveXStateException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.InvalidActiveXStateException" /> class without specifying information about the member that referenced the ActiveX control.</summary>
		public InvalidActiveXStateException()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.InvalidActiveXStateException" /> class and indicates the name of the member that referenced the ActiveX control and the kind of reference it made.</summary>
		/// <param name="name">The name of the member that referenced the ActiveX control while it was in an invalid state. </param>
		/// <param name="kind">One of the <see cref="T:System.Windows.Forms.AxHost.ActiveXInvokeKind" /> values. </param>
		public InvalidActiveXStateException(string name, ActiveXInvokeKind kind)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Creates and returns a string representation of the current exception.</summary>
		/// <returns>A string representation of the current exception.</returns>
		public override string ToString()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>Encapsulates the persisted state of an ActiveX control.</summary>
	[Serializable]
	[TypeConverter("System.ComponentModel.TypeConverter, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public class State : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.State" /> class for serializing a state. </summary>
		/// <param name="ms">A <see cref="T:System.IO.Stream" /> in which the state is stored. </param>
		/// <param name="storageType">An <see cref="T:System.Int32" /> indicating the storage type.</param>
		/// <param name="manualUpdate">true for manual updates; otherwise, false.</param>
		/// <param name="licKey">The license key of the control.</param>
		public State(Stream ms, int storageType, bool manualUpdate, string licKey)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.State" /> class for deserializing a state. </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> value.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> value.</param>
		protected State(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The destination for this serialization.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
		}
	}

	/// <summary>Specifies a date and time associated with the type library of an ActiveX control hosted by an <see cref="T:System.Windows.Forms.AxHost" /> control.</summary>
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class TypeLibraryTimeStampAttribute : Attribute
	{
		/// <summary>The date and time to associate with the type library.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value representing the date and time to associate with the type library.</returns>
		public DateTime Value
		{
			get
			{
				throw new NotImplementedException("COM/ActiveX support is not implemented");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.TypeLibraryTimeStampAttribute" /> class. </summary>
		/// <param name="timestamp">A <see cref="T:System.DateTime" /> value representing the date and time to associate with the type library.</param>
		public TypeLibraryTimeStampAttribute(string timestamp)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>Converts <see cref="T:System.Windows.Forms.AxHost.State" /> objects from one data type to another. </summary>
	public class StateConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> class. </summary>
		public StateConverter()
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns whether the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can convert an object of the specified type to an <see cref="T:System.Windows.Forms.AxHost.State" />, using the specified context.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which to convert.</param>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>Returns whether the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can convert an object to the given destination type using the context.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type from which to convert.</param>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>This member overrides <see cref="M:System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}

		/// <summary>This member overrides <see cref="M:System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>Represents the method that will display an ActiveX control's About dialog box.</summary>
	protected delegate void AboutBoxDelegate();

	private static object MouseClickEvent;

	private static object MouseDoubleClickEvent;

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Color BackColor
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Image BackgroundImage
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override ImageLayout BackgroundImageLayout
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>Gets or sets the control containing the ActiveX control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContainerControl" /> that represents the control containing the ActiveX control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public ContainerControl ContainingControl
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override ContextMenu ContextMenu
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Cursor Cursor
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool EditMode
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new virtual bool Enabled
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Font Font
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Color ForeColor
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>Gets a value indicating whether the ActiveX control has an About dialog box.</summary>
	/// <returns>true if the ActiveX control has an About dialog box; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool HasAboutBox
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImeMode" /> value.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new ImeMode ImeMode
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>Gets or sets the persisted state of the ActiveX control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.AxHost.State" /> that represents the persisted state of the ActiveX control.</returns>
	/// <exception cref="T:System.Exception">The ActiveX control is already loaded. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DefaultValue(null)]
	[RefreshProperties(RefreshProperties.All)]
	public State OcxState
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new virtual bool RightToLeft
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override ISite Site
	{
		set
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.String" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected override CreateParams CreateParams
	{
		get
		{
			throw new NotImplementedException("COM/ActiveX support is not implemented");
		}
	}

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => new Size(75, 23);

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BackColorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackColorChanged
	{
		add
		{
			base.BackColorChanged += value;
		}
		remove
		{
			base.BackColorChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BackgroundImageChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackgroundImageChanged
	{
		add
		{
			base.BackgroundImageChanged += value;
		}
		remove
		{
			base.BackgroundImageChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BindingContextChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BindingContextChanged
	{
		add
		{
			base.BindingContextChanged += value;
		}
		remove
		{
			base.BindingContextChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ChangeUICues" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event UICuesEventHandler ChangeUICues
	{
		add
		{
			base.ChangeUICues += value;
		}
		remove
		{
			base.ChangeUICues -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Click" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler Click
	{
		add
		{
			base.Click += value;
		}
		remove
		{
			base.Click -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ContextMenuChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler ContextMenuChanged
	{
		add
		{
			base.ContextMenuChanged += value;
		}
		remove
		{
			base.ContextMenuChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.CursorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler CursorChanged
	{
		add
		{
			base.CursorChanged += value;
		}
		remove
		{
			base.CursorChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DoubleClick" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DoubleClick
	{
		add
		{
			base.DoubleClick += value;
		}
		remove
		{
			base.DoubleClick -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragDrop" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event DragEventHandler DragDrop
	{
		add
		{
			base.DragDrop += value;
		}
		remove
		{
			base.DragDrop -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragEnter" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event DragEventHandler DragEnter
	{
		add
		{
			base.DragEnter += value;
		}
		remove
		{
			base.DragEnter -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragLeave" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler DragLeave
	{
		add
		{
			base.DragLeave += value;
		}
		remove
		{
			base.DragLeave -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragOver" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event DragEventHandler DragOver
	{
		add
		{
			base.DragOver += value;
		}
		remove
		{
			base.DragOver -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.EnabledChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler EnabledChanged
	{
		add
		{
			base.EnabledChanged += value;
		}
		remove
		{
			base.EnabledChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.FontChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler FontChanged
	{
		add
		{
			base.FontChanged += value;
		}
		remove
		{
			base.FontChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ForeColorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ForeColorChanged
	{
		add
		{
			base.ForeColorChanged += value;
		}
		remove
		{
			base.ForeColorChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.GiveFeedback" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event GiveFeedbackEventHandler GiveFeedback
	{
		add
		{
			base.GiveFeedback += value;
		}
		remove
		{
			base.GiveFeedback -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.HelpRequested" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event HelpEventHandler HelpRequested
	{
		add
		{
			base.HelpRequested += value;
		}
		remove
		{
			base.HelpRequested -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ImeModeChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ImeModeChanged
	{
		add
		{
			base.ImeModeChanged += value;
		}
		remove
		{
			base.ImeModeChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyDown" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event KeyEventHandler KeyDown
	{
		add
		{
			base.KeyDown += value;
		}
		remove
		{
			base.KeyDown -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyPress" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event KeyPressEventHandler KeyPress
	{
		add
		{
			base.KeyPress += value;
		}
		remove
		{
			base.KeyPress -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyUp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event KeyEventHandler KeyUp
	{
		add
		{
			base.KeyUp += value;
		}
		remove
		{
			base.KeyUp -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Layout" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event LayoutEventHandler Layout
	{
		add
		{
			base.Layout += value;
		}
		remove
		{
			base.Layout -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseDown
	{
		add
		{
			base.MouseDown += value;
		}
		remove
		{
			base.MouseDown -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseEnter" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler MouseEnter
	{
		add
		{
			base.MouseEnter += value;
		}
		remove
		{
			base.MouseEnter -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseHover" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MouseHover
	{
		add
		{
			base.MouseHover += value;
		}
		remove
		{
			base.MouseHover -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseLeave" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MouseLeave
	{
		add
		{
			base.MouseLeave += value;
		}
		remove
		{
			base.MouseLeave -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event MouseEventHandler MouseMove
	{
		add
		{
			base.MouseMove += value;
		}
		remove
		{
			base.MouseMove -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event MouseEventHandler MouseUp
	{
		add
		{
			base.MouseUp += value;
		}
		remove
		{
			base.MouseUp -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseWheel" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseWheel
	{
		add
		{
			base.MouseWheel += value;
		}
		remove
		{
			base.MouseWheel -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Paint" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event PaintEventHandler Paint
	{
		add
		{
			base.Paint += value;
		}
		remove
		{
			base.Paint -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.QueryAccessibilityHelp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
	{
		add
		{
			base.QueryAccessibilityHelp += value;
		}
		remove
		{
			base.QueryAccessibilityHelp -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.QueryContinueDrag" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event QueryContinueDragEventHandler QueryContinueDrag
	{
		add
		{
			base.QueryContinueDrag += value;
		}
		remove
		{
			base.QueryContinueDrag -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.RightToLeftChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler RightToLeftChanged
	{
		add
		{
			base.RightToLeftChanged += value;
		}
		remove
		{
			base.RightToLeftChanged -= value;
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.StyleChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler StyleChanged
	{
		add
		{
			base.StyleChanged += value;
		}
		remove
		{
			base.StyleChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			base.BackgroundImageLayoutChanged += value;
		}
		remove
		{
			base.BackgroundImageLayoutChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MouseClick
	{
		add
		{
			base.Events.AddHandler(MouseClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseClickEvent, value);
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MouseDoubleClick
	{
		add
		{
			base.Events.AddHandler(MouseDoubleClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseDoubleClickEvent, value);
		}
	}

	/// <summary>The <see cref="E:System.Windows.Forms.AxHost.TextChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler TextChanged
	{
		add
		{
			base.TextChanged += value;
		}
		remove
		{
			base.TextChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost" /> class, wrapping the ActiveX control indicated by the specified CLSID. </summary>
	/// <param name="clsid">The CLSID of the ActiveX control to wrap.</param>
	protected AxHost(string clsid)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost" /> class, wrapping the ActiveX control indicated by the specified CLSID, and using the shortcut-menu behavior indicated by the specified <paramref name="flags" /> value.</summary>
	/// <param name="clsid">The CLSID of the ActiveX control to wrap.</param>
	/// <param name="flags">An <see cref="T:System.Int32" /> that modifies the shortcut-menu behavior for the control.</param>
	protected AxHost(string clsid, int flags)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	static AxHost()
	{
		MouseClick = new object();
		MouseDoubleClick = new object();
	}

	/// <summary>Returns a collection of type <see cref="T:System.Attribute" /> for the current object.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the current object.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	AttributeCollection ICustomTypeDescriptor.GetAttributes()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the class name of the current object.</summary>
	/// <returns>Returns null in all cases.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	string ICustomTypeDescriptor.GetClassName()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the name of the current object.</summary>
	/// <returns>Returns null in all cases.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	string ICustomTypeDescriptor.GetComponentName()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns a type converter for the current object.</summary>
	/// <returns>Returns null in all cases.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	TypeConverter ICustomTypeDescriptor.GetConverter()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the default event for the current object.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for the current object, or null if the object does not have events.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the default property for the current object.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for the current object, or null if the object does not have properties.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns an editor of the specified type for the current object.</summary>
	/// <returns>An object of the specified type that is the editor for the current object, or null if the editor cannot be found.</returns>
	/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for the current object.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the events for the current object.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for the current object.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the events for the current object using the specified attribute array as a filter.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for the <see cref="T:System.Windows.Forms.AxHost" /> that match the given set of attributes.</returns>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the properties for the current object.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the events for the current object.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the properties for the current object using the specified attribute array as a filter.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the events for the current <see cref="T:System.Windows.Forms.AxHost" /> that match the given set of attributes.</returns>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns the object that owns the specified value.</summary>
	/// <returns>The current object.</returns>
	/// <param name="pd">Not used.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[CLSCompliant(false)]
	protected static Color GetColorFromOleColor(uint color)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static Font GetFontFromIFont(object font)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static Font GetFontFromIFontDisp(object font)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static object GetIFontDispFromFont(Font font)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static object GetIFontFromFont(Font font)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns an OLE IPictureDisp object corresponding to the specified <see cref="T:System.Drawing.Image" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the OLE IPictureDisp object.</returns>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to convert.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static object GetIPictureDispFromPicture(Image image)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns an OLE IPicture object corresponding to the specified <see cref="T:System.Windows.Forms.Cursor" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the OLE IPicture object.</returns>
	/// <param name="cursor">
	///   <see cref="T:System.Windows.Forms.Cursor" />
	/// </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static object GetIPictureFromCursor(Cursor cursor)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns an OLE IPicture object corresponding to the specified <see cref="T:System.Drawing.Image" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the OLE IPicture object.</returns>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to convert.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static object GetIPictureFromPicture(Image image)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static double GetOADateFromTime(DateTime time)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[CLSCompliant(false)]
	protected static uint GetOleColorFromColor(Color color)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns an <see cref="T:System.Drawing.Image" /> corresponding to the specified OLE IPicture object.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> representing the IPicture. </returns>
	/// <param name="picture">The IPicture to convert.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static Image GetPictureFromIPicture(object picture)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Returns an <see cref="T:System.Drawing.Image" /> corresponding to the specified OLE IPictureDisp object.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> representing the IPictureDisp. </returns>
	/// <param name="picture">The IPictureDisp to convert.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static Image GetPictureFromIPictureDisp(object picture)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected static DateTime GetTimeFromOADate(double date)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Begins the initialization of the ActiveX control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void BeginInit()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DoVerb(int verb)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Ends the initialization of an ActiveX control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void EndInit()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Retrieves a reference to the underlying ActiveX control.</summary>
	/// <returns>An object that represents the ActiveX control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public object GetOcx()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Determines if the ActiveX control has a property page.</summary>
	/// <returns>true if the ActiveX control has a property page; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public bool HasPropertyPages()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void InvokeEditMode()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void MakeDirty()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <returns>true if the message was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override bool PreProcessMessage(ref Message msg)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Displays the ActiveX control's About dialog box.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public void ShowAboutBox()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Displays the property pages associated with the ActiveX control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowPropertyPages()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Displays the property pages associated with the ActiveX control assigned to the specified parent control.</summary>
	/// <param name="control">The parent <see cref="T:System.Windows.Forms.Control" /> of the ActiveX control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowPropertyPages(Control control)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>When overridden in a derived class, attaches interfaces to the underlying ActiveX control.</summary>
	protected virtual void AttachInterfaces()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	protected override void CreateHandle()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Called by the system to create the ActiveX control.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the ActiveX control. </returns>
	/// <param name="clsid">The CLSID of the ActiveX control.</param>
	protected virtual object CreateInstanceCore(Guid clsid)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void CreateSink()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	protected override void DestroyHandle()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void DetachSink()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>This method is not supported by this control.</summary>
	/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
	/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Called by the system to retrieve the current bounds of the ActiveX control.</summary>
	/// <returns>The unmodified <paramref name="bounds" /> value.</returns>
	/// <param name="bounds">The original bounds of the ActiveX control.</param>
	/// <param name="factor">A scaling factor. </param>
	/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected new virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Determines if a character is an input character that the ActiveX control recognizes.</summary>
	/// <returns>true if the character should be sent directly to the ActiveX control and not preprocessed; otherwise, false.</returns>
	/// <param name="charCode">The character to test. </param>
	protected override bool IsInputChar(char charCode)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnForeColorChanged(EventArgs e)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleCreated(EventArgs e)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Not used.</summary>
	protected virtual void OnInPlaceActive()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnLostFocus(EventArgs e)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <returns>true if the key was processed by the control; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected override bool ProcessDialogKey(Keys keyData)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected override bool ProcessMnemonic(char charCode)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected bool PropsValid()
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified 32-bit signed integers.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="shift">Not used.</param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseDown(short button, short shift, int x, int y)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified single-precision floating-point numbers.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="shift">Not used.</param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseDown(short button, short shift, float x, float y)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified objects.</summary>
	/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="o2">Not used.</param>
	/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="o4">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseDown(object o1, object o2, object o3, object o4)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified 32-bit signed integers.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="shift">Not used.</param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseMove(short button, short shift, int x, int y)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified single-precision floating-point numbers.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="shift">Not used.</param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseMove(short button, short shift, float x, float y)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified objects.</summary>
	/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="o2">Not used.</param>
	/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="o4">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseMove(object o1, object o2, object o3, object o4)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified 32-bit signed integers.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="shift">Not used.</param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseUp(short button, short shift, int x, int y)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified single-precision floating-point numbers.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="shift">Not used.</param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseUp(short button, short shift, float x, float y)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified objects.</summary>
	/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed. </param>
	/// <param name="o2">Not used.</param>
	/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
	/// <param name="o4">The y-coordinate of a mouse click, in pixels. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected void RaiseOnMouseUp(object o1, object o2, object o3, object o4)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <summary>Calls the <see cref="M:System.Windows.Forms.AxHost.ShowAboutBox" /> method to display the ActiveX control's About dialog box.</summary>
	/// <param name="d">The <see cref="T:System.Windows.Forms.AxHost.AboutBoxDelegate" /> to call. </param>
	protected void SetAboutBoxDelegate(AboutBoxDelegate d)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="value">true to make the control visible; otherwise, false. </param>
	protected override void SetVisibleCore(bool value)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected override void WndProc(ref Message m)
	{
		throw new NotImplementedException("COM/ActiveX support is not implemented");
	}
}
