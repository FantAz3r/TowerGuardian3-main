using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DynamicJoystick))]
public class DynamicJoystickEditor : JoystickEditor
{
    private SerializedProperty moveThreshold;

    protected override void OnEnable()
    {
        base.OnEnable();
        moveThreshold = serializedObject.FindProperty("moveThreshold");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

      //if (background != null)
      //{
      //    RectTransform backgroundRect = (RectTransform)background.objectReferenceValue;
      //    backgroundRect.anchorMax = new Vector2(0.5f, 0.5f);
      //    backgroundRect.anchorMin = new Vector2(0.5f, 0.5f);
      //    backgroundRect.pivot = center;
      //}
    }

    protected override void DrawValues()
    {
        base.DrawValues();
        EditorGUILayout.PropertyField(moveThreshold, new GUIContent("Move Threshold", "The distance away from the center input has to be before the joystick begins to move."));
    }
}