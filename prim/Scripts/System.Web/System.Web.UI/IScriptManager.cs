namespace System.Web.UI;

internal interface IScriptManager
{
	void RegisterOnSubmitStatementExternal(Control control, Type type, string key, string script);

	void RegisterExpandoAttributeExternal(Control control, string controlId, string attributeName, string attributeValue, bool encode);

	void RegisterHiddenFieldExternal(Control control, string hiddenFieldName, string hiddenFieldInitialValue);

	void RegisterStartupScriptExternal(Control control, Type type, string key, string script, bool addScriptTags);

	void RegisterArrayDeclarationExternal(Control control, string arrayName, string arrayValue);

	void RegisterClientScriptBlockExternal(Control control, Type type, string key, string script, bool addScriptTags);

	void RegisterClientScriptIncludeExternal(Control control, Type type, string key, string url);

	void RegisterClientScriptResourceExternal(Control control, Type type, string resourceName);
}
