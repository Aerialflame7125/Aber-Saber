using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Accessibility;

namespace System.Windows.Forms;

/// <summary>Provides information that accessibility applications use to adjust an application's user interface (UI) for users with impairments.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class AccessibleObject : StandardOleMarshalObject, IReflect, IAccessible
{
	internal string name;

	internal string value;

	internal Control owner;

	internal AccessibleRole role;

	internal AccessibleStates state;

	internal string default_action;

	internal string description;

	internal string help;

	internal string keyboard_shortcut;

	/// <summary>Gets the underlying type that represents the <see cref="T:System.Reflection.IReflect" /> object. For a description of this member, see <see cref="P:System.Reflection.IReflect.UnderlyingSystemType" />.</summary>
	/// <returns>The underlying type that represents the <see cref="T:System.Reflection.IReflect" /> object.</returns>
	Type IReflect.UnderlyingSystemType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of child interfaces that belong to this object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accChildCount" />.</summary>
	/// <returns>The number of child accessible objects that belong to this object. If the object has no child objects, this value is 0.</returns>
	int IAccessible.accChildCount
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the object that has the keyboard focus. For a description of this member, see <see cref="P:Accessibility.IAccessible.accFocus" />.</summary>
	/// <returns>The object that has keyboard focus. </returns>
	object IAccessible.accFocus
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the parent accessible object of this object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accParent" />.</summary>
	/// <returns>An <see cref="T:Accessibility.IAccessible" /> that represents the parent of the accessible object, or null if there is no parent object.</returns>
	object IAccessible.accParent
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the selected child objects of an accessible object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accSelection" />.</summary>
	/// <returns>The selected child objects of an accessible object. </returns>
	object IAccessible.accSelection
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the location and size of the accessible object.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The bounds of control cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual Rectangle Bounds => owner.Bounds;

	/// <summary>Gets a string that describes the default action of the object. Not all objects have a default action.</summary>
	/// <returns>A description of the default action for an object, or null if this object has no default action.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual string DefaultAction => default_action;

	/// <summary>Gets a string that describes the visual appearance of the specified object. Not all objects have a description.</summary>
	/// <returns>A description of the object's visual appearance to the user, or null if the object does not have a description.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The description for the control cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual string Description => description;

	/// <summary>Gets a description of what the object does or how the object is used.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the description of what the object does or how the object is used. Returns null if no help is defined.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The help string for the control cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual string Help => help;

	/// <summary>Gets the shortcut key or access key for the accessible object.</summary>
	/// <returns>The shortcut key or access key for the accessible object, or null if there is no shortcut key associated with the object.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The shortcut for the control cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual string KeyboardShortcut => keyboard_shortcut;

	/// <summary>Gets or sets the object name.</summary>
	/// <returns>The object name, or null if the property has not been set.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The name of the control cannot be retrieved or set. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual string Name
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

	/// <summary>Gets the parent of an accessible object.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or null if there is no parent object.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public virtual AccessibleObject Parent
	{
		get
		{
			if (owner != null && owner.Parent != null)
			{
				return owner.Parent.AccessibilityObject;
			}
			return null;
		}
	}

	/// <summary>Gets the role of this accessible object.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values, or <see cref="F:System.Windows.Forms.AccessibleRole.None" /> if no role has been specified.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual AccessibleRole Role => role;

	/// <summary>Gets the state of this accessible object.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" />, if no state has been set.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual AccessibleStates State => state;

	/// <summary>Gets or sets the value of an accessible object.</summary>
	/// <returns>The value of an accessible object, or null if the object has no value set.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The value cannot be set or retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public virtual string Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AccessibleObject" /> class.</summary>
	public AccessibleObject()
	{
		owner = null;
		value = null;
		name = null;
		role = AccessibleRole.Default;
		default_action = null;
		description = null;
		help = null;
		keyboard_shortcut = null;
		state = AccessibleStates.None;
	}

	internal AccessibleObject(Control owner)
		: this()
	{
		this.owner = owner;
	}

	/// <summary>Gets the <see cref="T:System.Reflection.FieldInfo" /> object corresponding to the specified field and binding flag. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetField(System.String,System.Reflection.BindingFlags)" />.</summary>
	/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object containing the field information for the named object that meets the search constraints specified in <paramref name="bindingAttr" />.</returns>
	/// <param name="name">The name of the field to find.</param>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple fields with the same name.</exception>
	FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array of <see cref="T:System.Reflection.FieldInfo" /> objects corresponding to all fields of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetFields(System.Reflection.BindingFlags)" />.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects containing all the field information for this reflection object that meets the search constraints specified in <paramref name="bindingAttr" />.</returns>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array of <see cref="T:System.Reflection.MemberInfo" /> objects corresponding to all public members or to all members that match a specified name. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMember(System.String,System.Reflection.BindingFlags)" />.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects matching the name parameter.</returns>
	/// <param name="name">The name of the member to find.</param>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array of <see cref="T:System.Reflection.MemberInfo" /> objects corresponding either to all public members or to all members of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMembers(System.Reflection.BindingFlags)" />.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects containing all the member information for this reflection object.</returns>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> object corresponding to a specified method under specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethod(System.String,System.Reflection.BindingFlags)" />.</summary>
	/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object containing the method information, with the match being based on the method name and search constraints specified in <paramref name="bindingAttr" />.</returns>
	/// <param name="name">The name of the member to find.</param>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
	MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> object corresponding to a specified method, using a Type array to choose from among overloaded methods. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" />.</summary>
	/// <returns>The requested method that matches all the specified parameters.</returns>
	/// <param name="name">The name of the member to find.</param>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	/// <param name="binder">An object that implements <see cref="T:System.Reflection.Binder" />, containing properties related to this method.</param>
	/// <param name="types">An array used to choose among overloaded methods.</param>
	/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified.</param>
	/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
	MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array of <see cref="T:System.Reflection.MethodInfo" /> objects with all public methods or all methods of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethods(System.Reflection.BindingFlags)" />.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects containing all the methods defined for this reflection object that meet the search constraints specified in bindingAttr.</returns>
	/// <param name="bindingAttr">The binding attributes used to control the search. </param>
	MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.Reflection.PropertyInfo" /> object corresponding to a specified property under specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperty(System.String,System.Reflection.BindingFlags)" />.</summary>
	/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the located property that meets the search constraints specified in <paramref name="bindingAttr" />, or null if the property was not located.</returns>
	/// <param name="name">The name of the property to find.</param>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
	PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.Reflection.PropertyInfo" /> object corresponding to a specified property with specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperty(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type,System.Type[],System.Reflection.ParameterModifier[])" />.</summary>
	/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the located property, if a property with the specified name was located in this reflection object, or null if the property was not located.</returns>
	/// <param name="name">The name of the member to find.</param>
	/// <param name="bindingAttr">The binding attributes used to control the search.</param>
	/// <param name="binder">An object that implements Binder, containing properties related to this method.</param>
	/// <param name="returnType">An array used to choose among overloaded methods.</param>
	/// <param name="types">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified.</param>
	/// <param name="modifiers">An array used to choose the parameter modifiers.</param>
	PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array of <see cref="T:System.Reflection.PropertyInfo" /> objects corresponding to all public properties or to all properties of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperties(System.Reflection.BindingFlags)" />.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects for all the properties defined on the reflection object.</returns>
	/// <param name="bindingAttr">The binding attribute used to control the search.</param>
	PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	/// <summary>Invokes a specified member. For a description of this member, see <see cref="M:System.Reflection.IReflect.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
	/// <returns>The specified member.</returns>
	/// <param name="name">The name of the member to find.</param>
	/// <param name="invokeAttr">One of the <see cref="T:System.Reflection.BindingFlags" /> invocation attributes. </param>
	/// <param name="binder">One of the <see cref="T:System.Reflection.BindingFlags" /> bit flags. Implements Binder, containing properties related to this method.</param>
	/// <param name="target">The object on which to invoke the specified member. This parameter is ignored for static members.</param>
	/// <param name="args">An array of objects that contains the number, order, and type of the parameters of the member to be invoked. This is an empty array if there are no parameters.</param>
	/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects. </param>
	/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. </param>
	/// <param name="namedParameters">A String array of parameters.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="invokeAttr" /> is <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> and another bit flag is also set.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="invokeAttr" /> is not <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> and name is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="invokeAttr" /> is not an invocation attribute from <see cref="T:System.Reflection.BindingFlags" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="invokeAttr" /> specifies both get and set for a property or field.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="invokeAttr" /> specifies both a field set and an Invoke method. <paramref name="args" /> is provided for a field get operation.</exception>
	/// <exception cref="T:System.ArgumentException">More than one argument is specified for a field set operation.</exception>
	/// <exception cref="T:System.MissingFieldException">The field or property cannot be found.</exception>
	/// <exception cref="T:System.MissingMethodException">The method cannot be found.</exception>
	/// <exception cref="T:System.Security.SecurityException">A private member is invoked without the necessary <see cref="T:System.Security.Permissions.ReflectionPermission" />.</exception>
	object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs the specified object's default action. Not all objects have a default action. For a description of this member, see <see cref="M:Accessibility.IAccessible.accDoDefaultAction(System.Object)" />.</summary>
	/// <param name="childID">The child ID in the <see cref="T:Accessibility.IAccessible" /> interface/child ID pair that represents the accessible object.</param>
	void IAccessible.accDoDefaultAction(object childID)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the child object at the specified screen coordinates. For a description of this member, see <see cref="M:Accessibility.IAccessible.accHitTest(System.Int32,System.Int32)" />.</summary>
	/// <returns>The accessible object at the point specified by <paramref name="xLeft" /> and <paramref name="yTop" />. </returns>
	/// <param name="xLeft">The horizontal coordinate.</param>
	/// <param name="yTop">The vertical coordinate.</param>
	object IAccessible.accHitTest(int xLeft, int yTop)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the object's current screen location. For a description of this member, see <see cref="M:Accessibility.IAccessible.accLocation(System.Int32@,System.Int32@,System.Int32@,System.Int32@,System.Object)" />.</summary>
	/// <param name="pxLeft">When this method returns, contains the x-coordinate of the object’s left edge. This parameter is passed uninitialized.</param>
	/// <param name="pyTop">When this method returns, contains the y-coordinate of the object’s top edge. This parameter is passed uninitialized.</param>
	/// <param name="pcxWidth">When this method returns, contains the width of the object. This parameter is passed uninitialized.</param>
	/// <param name="pcyHeight">When this method returns, contains the height of the object. This parameter is passed uninitialized.</param>
	/// <param name="childID">The ID number of the accessible object. This parameter is 0 to get the location of the object, or a child ID to get the location of one of the object's child objects.</param>
	void IAccessible.accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight, object childID)
	{
		throw new NotImplementedException();
	}

	/// <summary>Navigates to an accessible object relative to the current object. For a description of this member, see <see cref="M:Accessibility.IAccessible.accNavigate(System.Int32,System.Object)" />.</summary>
	/// <returns>The accessible object positioned at the value specified by <paramref name="navDir" />. </returns>
	/// <param name="navDir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> enumerations that specifies the direction to navigate. </param>
	/// <param name="childID">The ID number of the accessible object. This parameter is 0 to start from the object, or a child ID to start from one of the object's child objects.</param>
	object IAccessible.accNavigate(int navDir, object childID)
	{
		throw new NotImplementedException();
	}

	/// <summary>Modifies the selection or moves the keyboard focus of the accessible object. For a description of this member, see <see cref="M:Accessibility.IAccessible.accSelect(System.Int32,System.Object)" />.</summary>
	/// <param name="flagsSelect">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
	/// <param name="childID">The ID number of the accessible object on which to perform the selection. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
	void IAccessible.accSelect(int flagsSelect, object childID)
	{
		throw new NotImplementedException();
	}

	object IAccessible.get_accChild(object childID)
	{
		throw new NotImplementedException();
	}

	string IAccessible.get_accDefaultAction(object childID)
	{
		throw new NotImplementedException();
	}

	string IAccessible.get_accDescription(object childID)
	{
		throw new NotImplementedException();
	}

	string IAccessible.get_accHelp(object childID)
	{
		throw new NotImplementedException();
	}

	int IAccessible.get_accHelpTopic(out string pszHelpFile, object childID)
	{
		throw new NotImplementedException();
	}

	string IAccessible.get_accKeyboardShortcut(object childID)
	{
		throw new NotImplementedException();
	}

	string IAccessible.get_accName(object childID)
	{
		throw new NotImplementedException();
	}

	object IAccessible.get_accRole(object childID)
	{
		throw new NotImplementedException();
	}

	object IAccessible.get_accState(object childID)
	{
		throw new NotImplementedException();
	}

	string IAccessible.get_accValue(object childID)
	{
		throw new NotImplementedException();
	}

	void IAccessible.set_accName(object childID, string newName)
	{
		throw new NotImplementedException();
	}

	void IAccessible.set_accValue(object childID, string newValue)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs the default action associated with this accessible object.</summary>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be performed. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public virtual void DoDefaultAction()
	{
		if (owner != null)
		{
			owner.DoDefaultAction();
		}
	}

	/// <summary>Retrieves the accessible child corresponding to the specified index.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
	/// <param name="index">The zero-based index of the accessible child. </param>
	/// <filterpriority>1</filterpriority>
	public virtual AccessibleObject GetChild(int index)
	{
		if (owner != null && index < owner.Controls.Count)
		{
			return owner.Controls[index].AccessibilityObject;
		}
		return null;
	}

	/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
	/// <returns>The number of children belonging to an accessible object.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual int GetChildCount()
	{
		if (owner != null)
		{
			return owner.Controls.Count;
		}
		return -1;
	}

	/// <summary>Retrieves the object that has the keyboard focus.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that specifies the currently focused child. This method returns the calling object if the object itself is focused. Returns null if no object has focus.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual AccessibleObject GetFocused()
	{
		if (owner.has_focus)
		{
			return owner.AccessibilityObject;
		}
		return FindFocusControl(owner);
	}

	/// <summary>Gets an identifier for a Help topic identifier and the path to the Help file associated with this accessible object.</summary>
	/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter contains the path to the Help file associated with this accessible object.</returns>
	/// <param name="fileName">On return, this property contains the path to the Help file associated with this accessible object. </param>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The Help topic for the control cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual int GetHelpTopic(out string fileName)
	{
		fileName = null;
		return -1;
	}

	/// <summary>Retrieves the currently selected child.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the currently selected child. This method returns the calling object if the object itself is selected. Returns null if is no child is currently selected and the object itself does not have focus.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The selected child cannot be retrieved. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual AccessibleObject GetSelected()
	{
		if ((state & AccessibleStates.Selected) != 0)
		{
			return this;
		}
		return FindSelectedControl(owner);
	}

	/// <summary>Retrieves the child object at the specified screen coordinates.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns null if no object is at the tested location.</returns>
	/// <param name="x">The horizontal screen coordinate. </param>
	/// <param name="y">The vertical screen coordinate. </param>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be hit tested. </exception>
	/// <filterpriority>1</filterpriority>
	public virtual AccessibleObject HitTest(int x, int y)
	{
		return FindHittestControl(owner, x, y)?.AccessibilityObject;
	}

	/// <summary>Navigates to another accessible object.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</returns>
	/// <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values. </param>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The navigation attempt fails. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public virtual AccessibleObject Navigate(AccessibleNavigation navdir)
	{
		int num = ((owner.Parent == null) ? (-1) : owner.Parent.Controls.IndexOf(owner));
		switch (navdir)
		{
		case AccessibleNavigation.Up:
			if (owner.Parent != null)
			{
				for (int k = 0; k < owner.Parent.Controls.Count; k++)
				{
					if (owner != owner.Parent.Controls[k] && owner.Parent.Controls[k].Top < owner.Top)
					{
						return owner.Parent.Controls[k].AccessibilityObject;
					}
				}
			}
			return owner.AccessibilityObject;
		case AccessibleNavigation.Down:
			if (owner.Parent != null)
			{
				for (int j = 0; j < owner.Parent.Controls.Count; j++)
				{
					if (owner != owner.Parent.Controls[j] && owner.Parent.Controls[j].Top > owner.Bottom)
					{
						return owner.Parent.Controls[j].AccessibilityObject;
					}
				}
			}
			return owner.AccessibilityObject;
		case AccessibleNavigation.Left:
			if (owner.Parent != null)
			{
				for (int i = 0; i < owner.Parent.Controls.Count; i++)
				{
					if (owner != owner.Parent.Controls[i] && owner.Parent.Controls[i].Left < owner.Left)
					{
						return owner.Parent.Controls[i].AccessibilityObject;
					}
				}
			}
			return owner.AccessibilityObject;
		case AccessibleNavigation.Right:
			if (owner.Parent != null)
			{
				for (int l = 0; l < owner.Parent.Controls.Count; l++)
				{
					if (owner != owner.Parent.Controls[l] && owner.Parent.Controls[l].Left > owner.Right)
					{
						return owner.Parent.Controls[l].AccessibilityObject;
					}
				}
			}
			return owner.AccessibilityObject;
		case AccessibleNavigation.Next:
			if (owner.Parent != null)
			{
				if (num + 1 < owner.Parent.Controls.Count)
				{
					return owner.Parent.Controls[num + 1].AccessibilityObject;
				}
				return owner.Parent.Controls[0].AccessibilityObject;
			}
			return owner.AccessibilityObject;
		case AccessibleNavigation.Previous:
			if (owner.Parent != null)
			{
				if (num > 0)
				{
					return owner.Parent.Controls[num - 1].AccessibilityObject;
				}
				return owner.Parent.Controls[owner.Parent.Controls.Count - 1].AccessibilityObject;
			}
			return owner.AccessibilityObject;
		case AccessibleNavigation.FirstChild:
			if (owner.Controls.Count > 0)
			{
				return owner.Controls[0].AccessibilityObject;
			}
			return owner.AccessibilityObject;
		case AccessibleNavigation.LastChild:
			if (owner.Controls.Count > 0)
			{
				return owner.Controls[owner.Controls.Count - 1].AccessibilityObject;
			}
			return owner.AccessibilityObject;
		default:
			return owner.AccessibilityObject;
		}
	}

	/// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
	/// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values. </param>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">The selection cannot be performed. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public virtual void Select(AccessibleSelection flags)
	{
		if ((flags & AccessibleSelection.TakeFocus) != 0)
		{
			owner.Focus();
		}
	}

	/// <summary>Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject" /> based on the handle of the object.</summary>
	/// <param name="handle">An <see cref="T:System.IntPtr" /> that contains the handle of the object. </param>
	protected void UseStdAccessibleObjects(IntPtr handle)
	{
	}

	/// <summary>Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject" /> based on the handle and the object id of the object.</summary>
	/// <param name="handle">An <see cref="T:System.IntPtr" /> that contains the handle of the object. </param>
	/// <param name="objid">An Int that defines the type of object that the <paramref name="handle" /> parameter refers to. </param>
	protected void UseStdAccessibleObjects(IntPtr handle, int objid)
	{
		UseStdAccessibleObjects(handle, 0);
	}

	internal static AccessibleObject FindFocusControl(Control parent)
	{
		if (parent != null)
		{
			for (int i = 0; i < parent.Controls.Count; i++)
			{
				Control control = parent.Controls[i];
				if ((control.AccessibilityObject.state & AccessibleStates.Focused) != 0)
				{
					return control.AccessibilityObject;
				}
				if (control.Controls.Count > 0)
				{
					AccessibleObject accessibleObject = FindFocusControl(control);
					if (accessibleObject != null)
					{
						return accessibleObject;
					}
				}
			}
		}
		return null;
	}

	internal static AccessibleObject FindSelectedControl(Control parent)
	{
		if (parent != null)
		{
			for (int i = 0; i < parent.Controls.Count; i++)
			{
				Control control = parent.Controls[i];
				if ((control.AccessibilityObject.state & AccessibleStates.Selected) != 0)
				{
					return control.AccessibilityObject;
				}
				if (control.Controls.Count > 0)
				{
					AccessibleObject accessibleObject = FindSelectedControl(control);
					if (accessibleObject != null)
					{
						return accessibleObject;
					}
				}
			}
		}
		return null;
	}

	internal static Control FindHittestControl(Control parent, int x, int y)
	{
		Point p = new Point(x, y);
		Point pt = parent.PointToClient(p);
		if (parent.ClientRectangle.Contains(pt))
		{
			return parent;
		}
		for (int i = 0; i < parent.Controls.Count; i++)
		{
			Control control = parent.Controls[i];
			pt = control.PointToClient(p);
			if (control.ClientRectangle.Contains(pt))
			{
				return control;
			}
			if (control.Controls.Count > 0)
			{
				Control control2 = FindHittestControl(control, x, y);
				if (control2 != null)
				{
					return control2;
				}
			}
		}
		return null;
	}
}
