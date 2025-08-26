using System.Collections.Generic;

namespace System;

/// <summary>Provides members for setting and retrieving data about an application's context.</summary>
public static class AppContext
{
	[Flags]
	private enum SwitchValueState
	{
		HasFalseValue = 1,
		HasTrueValue = 2,
		HasLookedForOverride = 4,
		UnknownValue = 8
	}

	private static readonly Dictionary<string, SwitchValueState> s_switchMap = new Dictionary<string, SwitchValueState>();

	private static volatile bool s_defaultsInitialized = false;

	/// <summary>Gets the pathname of the base directory that the assembly resolver uses to probe for assemblies.</summary>
	/// <returns>the pathname of the base directory that the assembly resolver uses to probe for assemblies.</returns>
	public static string BaseDirectory => ((string)AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY")) ?? AppDomain.CurrentDomain.BaseDirectory;

	/// <summary>Gets the name of the framework version targeted by the current application.</summary>
	/// <returns>The name of the framework version targeted by the current application.</returns>
	public static string TargetFrameworkName => AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;

	/// <summary>Returns the value of the named data element assigned to the current application domain.</summary>
	/// <param name="name">The name of the data element.</param>
	/// <returns>The value of <paramref name="name" />, if <paramref name="name" /> identifies a named value; otherwise, <see langword="null" />.</returns>
	public static object GetData(string name)
	{
		return AppDomain.CurrentDomain.GetData(name);
	}

	private static void InitializeDefaultSwitchValues()
	{
		lock (s_switchMap)
		{
			if (!s_defaultsInitialized)
			{
				AppContextDefaultValues.PopulateDefaultValues();
				s_defaultsInitialized = true;
			}
		}
	}

	/// <summary>Tries to get the value of a switch.</summary>
	/// <param name="switchName">The name of the switch.</param>
	/// <param name="isEnabled">When this method returns, contains the value of <paramref name="switchName" /> if <paramref name="switchName" /> was found, or <see langword="false" /> if <paramref name="switchName" /> was not found. This parameter is passed uninitialized.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="switchName" /> was set and the <paramref name="isEnabled" /> argument contains the value of the switch; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="switchName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="switchName" /> is <see cref="F:System.String.Empty" />.</exception>
	public static bool TryGetSwitch(string switchName, out bool isEnabled)
	{
		if (switchName == null)
		{
			throw new ArgumentNullException("switchName");
		}
		if (switchName.Length == 0)
		{
			throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "switchName");
		}
		if (!s_defaultsInitialized)
		{
			InitializeDefaultSwitchValues();
		}
		isEnabled = false;
		lock (s_switchMap)
		{
			if (s_switchMap.TryGetValue(switchName, out var value))
			{
				if (value == SwitchValueState.UnknownValue)
				{
					isEnabled = false;
					return false;
				}
				isEnabled = (value & SwitchValueState.HasTrueValue) == SwitchValueState.HasTrueValue;
				if ((value & SwitchValueState.HasLookedForOverride) == SwitchValueState.HasLookedForOverride)
				{
					return true;
				}
				if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out var overrideValue))
				{
					isEnabled = overrideValue;
				}
				s_switchMap[switchName] = (SwitchValueState)(((!isEnabled) ? 1 : 2) | 4);
				return true;
			}
			if (AppContextDefaultValues.TryGetSwitchOverride(switchName, out var overrideValue2))
			{
				isEnabled = overrideValue2;
				s_switchMap[switchName] = (SwitchValueState)(((!isEnabled) ? 1 : 2) | 4);
				return true;
			}
			s_switchMap[switchName] = SwitchValueState.UnknownValue;
		}
		return false;
	}

	/// <summary>Sets the value of a switch.</summary>
	/// <param name="switchName">The name of the switch.</param>
	/// <param name="isEnabled">The value of the switch.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="switchName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="switchName" /> is <see cref="F:System.String.Empty" />.</exception>
	public static void SetSwitch(string switchName, bool isEnabled)
	{
		if (switchName == null)
		{
			throw new ArgumentNullException("switchName");
		}
		if (switchName.Length == 0)
		{
			throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "switchName");
		}
		if (!s_defaultsInitialized)
		{
			InitializeDefaultSwitchValues();
		}
		SwitchValueState value = (SwitchValueState)(((!isEnabled) ? 1 : 2) | 4);
		lock (s_switchMap)
		{
			s_switchMap[switchName] = value;
		}
	}

	internal static void DefineSwitchDefault(string switchName, bool isEnabled)
	{
		s_switchMap[switchName] = ((!isEnabled) ? SwitchValueState.HasFalseValue : SwitchValueState.HasTrueValue);
	}

	internal static void DefineSwitchOverride(string switchName, bool isEnabled)
	{
		s_switchMap[switchName] = (SwitchValueState)(((!isEnabled) ? 1 : 2) | 4);
	}
}
