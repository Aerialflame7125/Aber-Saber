using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

public class UniversalUIInteractionTool : EditorWindow
{
    private Vector2 scrollPosition;
    private string searchFilter = "";
    private List<Button> allButtons = new List<Button>();
    private Dictionary<Button, string> buttonPaths = new Dictionary<Button, string>();
    private bool showActiveOnly = true;
    private bool groupByCanvas = true;
    private Dictionary<Canvas, List<Button>> buttonsByCanvas = new Dictionary<Canvas, List<Button>>();

    // Table cell related fields
    private List<MonoBehaviour> allTableCells = new List<MonoBehaviour>();
    private Dictionary<MonoBehaviour, string> tableCellPaths = new Dictionary<MonoBehaviour, string>();
    private Dictionary<Canvas, List<MonoBehaviour>> tableCellsByCanvas = new Dictionary<Canvas, List<MonoBehaviour>>();

    // Tab selection
    private enum TabType { Buttons, TableCells }
    private TabType selectedTab = TabType.Buttons;

    [MenuItem("Tools/UI Interaction Tool")]
    public static void ShowWindow()
    {
        GetWindow<UniversalUIInteractionTool>("UI Interaction Tool");
    }

    private void OnEnable()
    {
        RefreshLists();
    }

    private void RefreshLists()
    {
        RefreshButtonList();
        RefreshTableCellList();
    }

    private void RefreshButtonList()
    {
        allButtons.Clear();
        buttonPaths.Clear();
        buttonsByCanvas.Clear();

        // Find all buttons in the scene
        Button[] sceneButtons = Resources.FindObjectsOfTypeAll<Button>();

        foreach (Button button in sceneButtons)
        {
            // Skip buttons that are part of prefabs in the project (not in the scene)
            if (PrefabUtility.GetPrefabAssetType(button.gameObject) != PrefabAssetType.NotAPrefab &&
                PrefabUtility.GetPrefabInstanceStatus(button.gameObject) == PrefabInstanceStatus.NotAPrefab)
                continue;

            // Get the full path for this button
            string path = GetGameObjectPath(button.gameObject);

            allButtons.Add(button);
            buttonPaths[button] = path;

            // Group by canvas
            Canvas parentCanvas = button.GetComponentInParent<Canvas>();
            if (parentCanvas != null)
            {
                if (!buttonsByCanvas.ContainsKey(parentCanvas))
                    buttonsByCanvas[parentCanvas] = new List<Button>();

                buttonsByCanvas[parentCanvas].Add(button);
            }
        }
    }

    private void RefreshTableCellList()
    {
        allTableCells.Clear();
        tableCellPaths.Clear();
        tableCellsByCanvas.Clear();

        // Find all MonoBehaviours in the scene
        MonoBehaviour[] allMonoBehaviours = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

        foreach (MonoBehaviour mb in allMonoBehaviours)
        {
            // Skip components that are part of prefabs in the project (not in the scene)
            if (PrefabUtility.GetPrefabAssetType(mb.gameObject) != PrefabAssetType.NotAPrefab &&
                PrefabUtility.GetPrefabInstanceStatus(mb.gameObject) == PrefabInstanceStatus.NotAPrefab)
                continue;

            Type type = mb.GetType();
            // Check if the type name ends with "TableCell"
            if (type.Name.EndsWith("TableCell"))
            {
                // Get the full path for this table cell
                string path = GetGameObjectPath(mb.gameObject);

                allTableCells.Add(mb);
                tableCellPaths[mb] = path;

                // Group by canvas
                Canvas parentCanvas = mb.GetComponentInParent<Canvas>();
                if (parentCanvas != null)
                {
                    if (!tableCellsByCanvas.ContainsKey(parentCanvas))
                        tableCellsByCanvas[parentCanvas] = new List<MonoBehaviour>();

                    tableCellsByCanvas[parentCanvas].Add(mb);
                }
            }
        }
    }

    private string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;
        Transform parent = obj.transform.parent;

        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }

        return path;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        // Tab selection
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Toggle(selectedTab == TabType.Buttons, "UI Buttons", EditorStyles.toolbarButton))
            selectedTab = TabType.Buttons;

        if (GUILayout.Toggle(selectedTab == TabType.TableCells, "Table Cells", EditorStyles.toolbarButton))
            selectedTab = TabType.TableCells;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Refresh Lists", GUILayout.Width(150)))
        {
            RefreshLists();
        }

        EditorGUILayout.Space();

        showActiveOnly = EditorGUILayout.ToggleLeft("Show Active Only", showActiveOnly, GUILayout.Width(120));
        groupByCanvas = EditorGUILayout.ToggleLeft("Group By Canvas", groupByCanvas, GUILayout.Width(120));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Search filter
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search:", GUILayout.Width(50));
        searchFilter = EditorGUILayout.TextField(searchFilter);
        if (GUILayout.Button("Clear", GUILayout.Width(60)))
        {
            searchFilter = "";
            GUI.FocusControl(null);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Show appropriate content based on selected tab
        if (selectedTab == TabType.Buttons)
        {
            DrawButtonsTab();
        }
        else if (selectedTab == TabType.TableCells)
        {
            DrawTableCellsTab();
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawButtonsTab()
    {
        EditorGUILayout.LabelField($"Found {allButtons.Count} buttons in scene", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        if (groupByCanvas)
        {
            // Display buttons grouped by canvas
            foreach (var canvasGroup in buttonsByCanvas)
            {
                Canvas canvas = canvasGroup.Key;
                List<Button> canvasButtons = canvasGroup.Value;

                // Filter buttons based on active state and search filter
                List<Button> filteredButtons = canvasButtons
                    .Where(b => (!showActiveOnly || b.gameObject.activeInHierarchy) &&
                               (string.IsNullOrEmpty(searchFilter) ||
                                buttonPaths[b].ToLower().Contains(searchFilter.ToLower()) ||
                                b.gameObject.name.ToLower().Contains(searchFilter.ToLower())))
                    .ToList();

                if (filteredButtons.Count == 0)
                    continue;

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                // Canvas header
                string canvasName = canvas != null ? canvas.name : "No Canvas";
                EditorGUILayout.LabelField($"Canvas: {canvasName} ({filteredButtons.Count} buttons)", EditorStyles.boldLabel);

                // Display all buttons in this canvas
                foreach (Button button in filteredButtons)
                {
                    DrawButtonControl(button);
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
        else
        {
            // Display all buttons without grouping
            List<Button> filteredButtons = allButtons
                .Where(b => (!showActiveOnly || b.gameObject.activeInHierarchy) &&
                           (string.IsNullOrEmpty(searchFilter) ||
                            buttonPaths[b].ToLower().Contains(searchFilter.ToLower()) ||
                            b.gameObject.name.ToLower().Contains(searchFilter.ToLower())))
                .ToList();

            foreach (Button button in filteredButtons)
            {
                DrawButtonControl(button);
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawTableCellsTab()
    {
        EditorGUILayout.LabelField($"Found {allTableCells.Count} table cells in scene", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        if (groupByCanvas)
        {
            // Display table cells grouped by canvas
            foreach (var canvasGroup in tableCellsByCanvas)
            {
                Canvas canvas = canvasGroup.Key;
                List<MonoBehaviour> canvasTableCells = canvasGroup.Value;

                // Filter table cells based on active state and search filter
                List<MonoBehaviour> filteredTableCells = canvasTableCells
                    .Where(tc => (!showActiveOnly || tc.gameObject.activeInHierarchy) &&
                                (string.IsNullOrEmpty(searchFilter) ||
                                 tableCellPaths[tc].ToLower().Contains(searchFilter.ToLower()) ||
                                 tc.gameObject.name.ToLower().Contains(searchFilter.ToLower())))
                    .ToList();

                if (filteredTableCells.Count == 0)
                    continue;

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                // Canvas header
                string canvasName = canvas != null ? canvas.name : "No Canvas";
                EditorGUILayout.LabelField($"Canvas: {canvasName} ({filteredTableCells.Count} table cells)", EditorStyles.boldLabel);

                // Display all table cells in this canvas
                foreach (MonoBehaviour tableCell in filteredTableCells)
                {
                    DrawTableCellControl(tableCell);
                }

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
        else
        {
            // Display all table cells without grouping
            List<MonoBehaviour> filteredTableCells = allTableCells
                .Where(tc => (!showActiveOnly || tc.gameObject.activeInHierarchy) &&
                            (string.IsNullOrEmpty(searchFilter) ||
                             tableCellPaths[tc].ToLower().Contains(searchFilter.ToLower()) ||
                             tc.gameObject.name.ToLower().Contains(searchFilter.ToLower())))
                .ToList();

            foreach (MonoBehaviour tableCell in filteredTableCells)
            {
                DrawTableCellControl(tableCell);
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawButtonControl(Button button)
    {
        if (button == null)
            return;

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        // Show button interactable status
        GUI.enabled = button.interactable;

        // Button path and name
        EditorGUILayout.LabelField(buttonPaths[button], EditorStyles.wordWrappedLabel);

        // Selection and click buttons
        if (GUILayout.Button("Select", GUILayout.Width(60)))
        {
            Selection.activeGameObject = button.gameObject;
            EditorGUIUtility.PingObject(button.gameObject);
        }

        if (GUILayout.Button("Click", GUILayout.Width(60)))
        {
            // Invoke the button's onClick event
            button.onClick.Invoke();
            Debug.Log($"Clicked button: {buttonPaths[button]}");
        }

        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
    }

    private void DrawTableCellControl(MonoBehaviour tableCell)
    {
        if (tableCell == null)
            return;

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        // Show tableCell status
        GUI.enabled = tableCell.isActiveAndEnabled;

        // TableCell path and name with type
        string typeName = tableCell.GetType().Name;
        EditorGUILayout.LabelField($"{tableCellPaths[tableCell]} ({typeName})", EditorStyles.wordWrappedLabel);

        // Selection button
        if (GUILayout.Button("Select", GUILayout.Width(60)))
        {
            Selection.activeGameObject = tableCell.gameObject;
            EditorGUIUtility.PingObject(tableCell.gameObject);
        }

        // Simulate click button
        if (GUILayout.Button("Interact", GUILayout.Width(60)))
        {
            SimulateTableCellInteraction(tableCell);
        }

        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
    }

    private void SimulateTableCellInteraction(MonoBehaviour tableCell)
    {
        // Try several common methods that might be found on TableCell scripts
        bool methodFound = false;

        // Methods to try, in order of preference
        string[] methodsToTry = new string[] {
            "OnClick",
            "OnCellClicked",
            "OnSelect",
            "OnSelected",
            "OnPress",
            "OnPressed",
            "Interact",
            "HandleClick"
        };

        Type type = tableCell.GetType();

        // First, look for methods with no parameters
        foreach (string methodName in methodsToTry)
        {
            MethodInfo method = type.GetMethod(methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                Type.EmptyTypes,
                null);

            if (method != null)
            {
                method.Invoke(tableCell, null);
                Debug.Log($"Invoked {methodName}() on {tableCellPaths[tableCell]}");
                methodFound = true;
                break;
            }
        }

        // If no method was found, try to find a Button component and click that
        if (!methodFound)
        {
            Button button = tableCell.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
                Debug.Log($"Clicked Button component on {tableCellPaths[tableCell]}");
                methodFound = true;
            }
        }

        // Check if there's a public property or field called "IsSelected" or similar and toggle it
        if (!methodFound)
        {
            string[] propertiesToTry = new string[] {
                "IsSelected",
                "Selected",
                "isSelected",
                "selected"
            };

            foreach (string propertyName in propertiesToTry)
            {
                PropertyInfo property = type.GetProperty(propertyName);
                if (property != null && property.PropertyType == typeof(bool) && property.CanWrite)
                {
                    bool currentValue = (bool)property.GetValue(tableCell);
                    property.SetValue(tableCell, !currentValue);
                    Debug.Log($"Toggled {propertyName} from {currentValue} to {!currentValue} on {tableCellPaths[tableCell]}");
                    methodFound = true;
                    break;
                }

                FieldInfo field = type.GetField(propertyName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null && field.FieldType == typeof(bool))
                {
                    bool currentValue = (bool)field.GetValue(tableCell);
                    field.SetValue(tableCell, !currentValue);
                    Debug.Log($"Toggled {propertyName} field from {currentValue} to {!currentValue} on {tableCellPaths[tableCell]}");
                    methodFound = true;
                    break;
                }
            }
        }

        if (!methodFound)
        {
            Debug.LogWarning($"Could not find a way to interact with {tableCellPaths[tableCell]}. You may need to implement a custom interaction method.");
        }
    }
}