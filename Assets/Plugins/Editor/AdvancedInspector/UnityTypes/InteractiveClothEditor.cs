using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEditor;
using UnityEngine;

namespace AdvancedInspector
{
    /*[CanEditMultipleObjects]
    [CustomEditor(typeof(InteractiveCloth), true)]
    public class InteractiveClothEditor : ClothEditor
    {
        protected override void RefreshFields()
        {
            base.RefreshFields();
            Type type = typeof(InteractiveCloth);
            SerializedObject so = new SerializedObject(targets);

            fields.Add(new InspectorField(type, Instances, type.GetProperty("mesh"),
                new DescriptorAttribute("Mesh", "Is the collider a trigger?", "http://docs.unity3d.com/ScriptReference/Collider-isTrigger.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("friction"),
                new DescriptorAttribute("Friction", "", "")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("density"),
                new DescriptorAttribute("Density", "", "")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("pressure"),
                new DescriptorAttribute("Pressure", "", "")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("collisionResponse"),
                new DescriptorAttribute("Collision Response", "", "")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("attachmentTearFactor"),
                new DescriptorAttribute("Attachment Tear Factor", "", "")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("attachmentResponse"),
                new DescriptorAttribute("Attachment Response", "", "")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("tearFactor"),
                new DescriptorAttribute("Tear Factor", "", "")));
            
            // This fails because "AttachedCollider" is not a proper managed type.
            fields.Add(new InspectorField(type, Instances, so.FindProperty("m_AttachedColliders"),
                new DescriptorAttribute("Attached Colliders", "", "")));

            fields.Add(new InspectorField(type, Instances, type.GetProperty("isTeared"), new InspectAttribute(InspectorLevel.Advanced),
                new DescriptorAttribute("Is Teared", "", "")));
        }
    }*/
}