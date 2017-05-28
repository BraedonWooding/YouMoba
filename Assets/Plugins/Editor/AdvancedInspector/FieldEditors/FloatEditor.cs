using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace AdvancedInspector
{
    public class FloatEditor : FieldEditor
    {
        private static float clickedValue = 0;
        private static Vector2 clickedPosition;

        public override Type[] EditedTypes
        {
            get { return new Type[] { typeof(float) }; }
        }

        public override void OnLabelDraw(InspectorField field, Rect rect)
        {
            if (Event.current != null && Event.current.shift)
                EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeHorizontal);
        }

        public override void OnLabelClick(InspectorField field)
        {
            float[] values = field.GetValues<float>();
            clickedValue = values[0];
            clickedPosition = Event.current.mousePosition;
        }

        public override void OnLabelDragged(InspectorField field)
        {
            if (Event.current.shift)
            {
                float result = clickedValue + (CalculateFloatDragSensitivity(clickedValue) * (Event.current.mousePosition - clickedPosition).x);

                RangeValueAttribute range = field.GetAttribute<RangeValueAttribute>();
                if (range != null)
                    result = Mathf.Clamp(result, range.Min, range.Max);

                field.SetValue(result);
            }
        }

        private static float CalculateFloatDragSensitivity(float value)
        {
            if (!float.IsInfinity(value) && !float.IsNaN(value))
                return Mathf.Max(1f, Mathf.Pow(Mathf.Abs(value), 0.5f)) * 0.03f;

            return 0f;
        }

        public override void Draw(InspectorField field, GUIStyle style)
        {
            RangeValueAttribute range = field.GetAttribute<RangeValueAttribute>();
            AngleAttribute angle = field.GetAttribute<AngleAttribute>();

            float result;
            if (DrawFloat(new GUIContent(""), field.GetValues<float>(), range, angle, style, out result))
                field.SetValue(result);
        }

        public static bool DrawFloat(string label, float[] values, out float result, params GUILayoutOption[] options)
        {
            return DrawFloat(new GUIContent(label), values, null, null, null, out result, options);
        }

        public static bool DrawFloat(string label, float[] values, GUIStyle style, out float result, params GUILayoutOption[] options)
        {
            return DrawFloat(new GUIContent(label), values, null, null, style, out result, options);
        }

        public static bool DrawFloat(string label, float[] values, RangeValueAttribute range, out float result, params GUILayoutOption[] options)
        {
            return DrawFloat(new GUIContent(label), values, range, null, null, out result, options);
        }

        public static bool DrawFloat(string label, float[] values, RangeValueAttribute range, GUIStyle style, out float result, params GUILayoutOption[] options)
        {
            return DrawFloat(new GUIContent(label), values, range, null, style, out result, options);
        }

        public static bool DrawFloat(string label, float[] values, RangeValueAttribute range, AngleAttribute angle, out float result, params GUILayoutOption[] options)
        {
            return DrawFloat(new GUIContent(label), values, range, angle, null, out result, options);
        }

        public static bool DrawFloat(string label, float[] values, RangeValueAttribute range, AngleAttribute angle, GUIStyle style, out float result, params GUILayoutOption[] options)
        {
            return DrawFloat(new GUIContent(label), values, range, angle, style, out result, options);
        }

        public static bool DrawFloat(GUIContent label, float[] values, RangeValueAttribute range, AngleAttribute angle, GUIStyle style, out float result, params GUILayoutOption[] options)
        {
            result = 0;

            EditorGUI.showMixedValue = false;
            float value = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] == value)
                    continue;

                EditorGUI.showMixedValue = true;
                break;
            }

            EditorGUI.BeginChangeCheck();

            if (angle != null)
            {
                if (range != null)
                    result = ExtraGUILayout.FloatAngle(value, angle.Snap, range.Min, range.Max);
                else
                    result = ExtraGUILayout.FloatAngle(value, angle.Snap);
            }
            else
            {
                if (range != null)
                    result = EditorGUILayout.Slider(label, value, range.Min, range.Max, options);
                else
                {
                    if (style != null)
                        result = EditorGUILayout.FloatField(label, value, style, options);
                    else
                        result = EditorGUILayout.FloatField(label, value, options);
                }
            }

            return EditorGUI.EndChangeCheck();
        }
    }
}