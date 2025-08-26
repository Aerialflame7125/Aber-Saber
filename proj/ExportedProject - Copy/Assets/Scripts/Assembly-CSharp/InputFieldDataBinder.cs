using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputFieldDataBinder
{
	private List<Tuple<InputField, IObservableChange, UnityAction<string>, Action>> _bindings;

	public InputFieldDataBinder()
	{
		_bindings = new List<Tuple<InputField, IObservableChange, UnityAction<string>, Action>>();
	}

	public void AddBindings<T0, T1>(List<Tuple<InputField, T0, Func<string, T1>, Func<T1, string>>> bindingData) where T0 : IObservableChange, IValue<T1>
	{
		foreach (Tuple<InputField, T0, Func<string, T1>, Func<T1, string>> bindingDatum in bindingData)
		{
			InputField inputField = bindingDatum.Item1;
			T0 valueItem = bindingDatum.Item2;
			Func<string, T1> toValueConvertor = bindingDatum.Item3;
			Func<T1, string> toStringConvertor = bindingDatum.Item4;
			UnityAction<string> unityAction = delegate(string value)
			{
				valueItem.value = toValueConvertor(value);
			};
			Action action = delegate
			{
				inputField.text = toStringConvertor(valueItem.value);
			};
			bindingDatum.Item1.onEndEdit.AddListener(unityAction);
			T0 item = bindingDatum.Item2;
			item.didChangeEvent += action;
			_bindings.Add(bindingDatum.Item1, bindingDatum.Item2, unityAction, action);
			action();
		}
	}

	public void AddStringBindings<T>(List<Tuple<InputField, T>> bindingData) where T : IObservableChange, IValue<string>
	{
		List<Tuple<InputField, T, Func<string, string>, Func<string, string>>> list = new List<Tuple<InputField, T, Func<string, string>, Func<string, string>>>();
		Func<string, string> func = (string value) => value;
		foreach (Tuple<InputField, T> bindingDatum in bindingData)
		{
			list.Add(bindingDatum.Item1, bindingDatum.Item2, func, func);
		}
		AddBindings(list);
	}

	public void ClearBindings()
	{
		if (_bindings == null)
		{
			return;
		}
		foreach (Tuple<InputField, IObservableChange, UnityAction<string>, Action> binding in _bindings)
		{
			InputField item = binding.Item1;
			IObservableChange item2 = binding.Item2;
			if (item != null)
			{
				item.onEndEdit.RemoveListener(binding.Item3);
			}
			if (item2 != null)
			{
				item2.didChangeEvent -= binding.Item4;
			}
		}
		_bindings.Clear();
	}
}
