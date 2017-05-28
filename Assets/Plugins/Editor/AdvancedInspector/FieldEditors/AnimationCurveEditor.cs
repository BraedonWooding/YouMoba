using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace AdvancedInspector
{
    public class AnimationCurveEditor : FieldEditor
    {
        public override Type[] EditedTypes
        {
            get { return new Type[] { typeof(AnimationCurve) }; }
        }

        public override void Draw(InspectorField field, GUIStyle style)
        {
            object value = GetValue(field);
            if (value == null)
                return;

            AnimationCurve curve = (AnimationCurve)value;

            EditorGUI.BeginChangeCheck();

            AnimationCurve result;
            if (field.Descriptor != null)
                result = EditorGUILayout.CurveField(curve, field.Descriptor.Color, GetCurveRect(curve));
            else
                result = EditorGUILayout.CurveField(curve, Color.red, GetCurveRect(curve));

            if (EditorGUI.EndChangeCheck())
                field.SetValue(result);
        }

        private Rect GetCurveRect(AnimationCurve curve)
        {
            Rect rect = new Rect(0, 0, 0, 0);

            foreach (Keyframe frame in curve.keys)
            {
                if (frame.time > rect.width)
                    rect.width = frame.time;
                if (frame.time < rect.x)
                    rect.x = frame.time;
                if (frame.value > rect.height)
                    rect.height = frame.value;
                if (frame.value < rect.y)
                    rect.y = frame.value;
            }

            return rect;
        }
    }
}
