using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common.GameUI;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EdgeInteraction))]
public class LabelHandle : Editor
{
    private static GUIStyle labelStyle;

    private void OnEnable()
    {
        labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.white;
        labelStyle.fontSize = 12;

    }

    private void OnSceneGUI() {
        EdgeInteraction edge = (EdgeInteraction)target;

        Handles.BeginGUI();
        Handles.Label(edge.transform.position + new Vector3(0, 4f, 0), edge.CurrentEdgePosition.ToString(), labelStyle);
        Handles.EndGUI();
    }
}