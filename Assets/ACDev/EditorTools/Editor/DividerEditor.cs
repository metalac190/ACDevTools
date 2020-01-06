using UnityEditor;
using UnityEngine;

/// <summary>
/// This script is responsible for enforcing the Editor layout of the Divider GameObject.
/// created by: Adam Chandler
/// This script gives additional functionality to the Divider class. It also allows you
/// to create a Divider within the GameObject context menu.
/// </summary>

[CustomEditor(typeof(Divider))]
public class DividerEditor : Editor
{
    Divider _divider;
    Tool _lastTool = Tool.None;

    private void OnEnable()
    {
        _divider = target as Divider;
        _lastTool = Tools.current;
        // remove transform manipulation
        _divider.transform.hideFlags = UnityEngine.HideFlags.HideInInspector;
        Tools.current = Tool.None;
    }

    private void OnDisable()
    {
        Tools.current = _lastTool;
    }

    private void OnSceneGUI()
    {
        // enforce the name
        _divider.transform.name = "- - - - - - - - - - - - - "
            + _divider.Name + " - - - - - - - - - - - - - ";
        // ensure the tool is disabled
        Tools.current = Tool.None;
    }

    /// <summary>
    /// This function allows you to create a Divider through the context menu
    /// </summary>
    /// <param name="menuCommand"></param>
    [MenuItem("GameObject/ACDev/Utility/Divider", false, 0)]
    static void CreateNewDivider(MenuCommand menuCommand)
    {
        //Instantiate(this.gameObject);
        GameObject go = new GameObject("Divider");
        // ensure it gets put into the Editors system
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
        go.AddComponent<Divider>();
    }
}

