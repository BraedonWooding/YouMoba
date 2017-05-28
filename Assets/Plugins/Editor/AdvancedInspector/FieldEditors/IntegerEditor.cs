using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

using UniToolsEditor;

namespace AdvancedInspector
{
    public class IntegerEditor : FieldEditor
    {
        private static int clickedValue = 0;
        private static Vector2 clickedPosition;

        public override Type[] EditedTypes
        {
            get { return new Type[] { typeof(int) }; }
        }

        public override void OnLabelDraw(InspectorField field, Rect rect)
        {
            if (Event.current != null && Event.current.shift)
                EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
        }

        public override void OnLabelClick(InspectorField field)
        {
            int[] values = field.GetValues<int>();
            clickedValue = values[0];
            clickedPosition = Event.current.mousePosition;
        }

        public override void OnLabelDragged(InspectorField field)
        {
            if (Event.current.shift)
            {
                int result = Mathf.RoundToInt(clickedValue + (CalculateIntDragSensitivity(clickedValue) * (Event.current.mousePosition - clickedPosition).x * 0.1f));

                RangeValueAttribute range = field.GetAttribute<RangeValueAttribute>();
                if (range != null)
                    result = (int)Mathf.Clamp(result, range.Min, range.Max);

                field.SetValue(result);
            }
        }

        private static float CalculateIntDragSensitivity(int value)
        {
            return Mathf.Max(1f, Mathf.Pow(Mathf.Abs((float)value), 0.5f) * 0.03f);
        }

        public override void Draw(InspectorField field, GUIStyle style)
        {
            RangeValueAttribute range = field.GetAttribute<RangeValueAttribute>();
            AngleAttribute angle = field.GetAttribute<AngleAttribute>();

            int result;
            if (DrawInt(GUIContent.none, field.GetValues<int>(), range, angle, style, out result))
                field.SetValue(result);
        }

        public static bool DrawInt(string label, int[] values, out int result, params GUILayoutOption[] options)
        {
            return DrawInt(new GUIContent(label), values, null, null, null, out result, options);
        }

        public static bool DrawInt(string label, int[] values, GUIStyle style, out int result, params GUILayoutOption[] options)
        {
            return DrawInt(new GUIContent(label), values, null, null, style, out result, options);
        }

        public static bool DrawInt(GUIContent label, int[] values, RangeValueAttribute range, AngleAttribute angle,GUIStyle style, out int result, params GUILayoutOption[] options)
        {
            EditorGUI.showMixedValue = false;
            result = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] == result)
                    continue;

                EditorGUI.showMixedValue = true;
                break;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();

            if (angle != null)
            {
                if (range != null)
                    result = ExtraGUILayout.IntAngle(result, (int)angle.Snap, (int)range.Min, (int)range.Max);
                else
                    result = ExtraGUILayout.IntAngle(result, (int)angle.Snap);
            }
            else
            {
                if (range != null)
                    result = EditorGUILayout.IntSlider(label, result, (int)range.Min, (int)range.Max, options);
                else
                {
                    if (style != null)
                        result = EditorGUILayout.IntField(label, result, style, options);
                    else
                        result = EditorGUILayout.IntField(label, result, options);
                }
            }

            EditorGUILayout.EndHorizontal();

            return EditorGUI.EndChangeCheck();
        }
    }
}