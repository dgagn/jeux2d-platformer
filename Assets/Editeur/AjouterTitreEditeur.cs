using System;
using UnityEditor;
using UnityEngine;

namespace PlatformerTP.Editeur
{

#if UNITY_EDITOR
    [InitializeOnLoad]
    public static class HierarchyWindowGroupHeader
    {
        static HierarchyWindowGroupHeader()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject == null || !gameObject.name.StartsWith("-----", System.StringComparison.Ordinal)) return;
        
            EditorGUI.DrawRect(selectionRect, Color.black);
            EditorGUI.DropShadowLabel(selectionRect, gameObject.name.Replace("-", ""));
        }
    }
#endif
}