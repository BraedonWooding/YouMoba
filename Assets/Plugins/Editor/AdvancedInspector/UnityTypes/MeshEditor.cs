﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEditor;
using UnityEngine;

namespace AdvancedInspector
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Mesh), true)]
    public class MeshEditor : InspectorEditor
    {
        protected override void RefreshFields()
        {
            Type type = typeof(Mesh);

            fields.Add(new InspectorField(type, Instances, type.GetProperty("subMeshCount"),
                new DescriptorAttribute("Sub Mesh Count", "The number of submeshes. Every material has a separate triangle list.", "http://docs.unity3d.com/ScriptReference/Mesh-subMeshCount.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("vertices"),
                new DescriptorAttribute("Vertices", "Returns a copy of the vertex positions or assigns a new vertex positions array.", "http://docs.unity3d.com/ScriptReference/Mesh-vertices.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("triangles"),
                new DescriptorAttribute("Triangles", "An array containing all triangles in the mesh.", "http://docs.unity3d.com/ScriptReference/Mesh-triangles.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("uv"),
                new DescriptorAttribute("UV", "The base texture coordinates of the mesh.", "http://docs.unity3d.com/ScriptReference/Mesh-uv.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("uv1"), 
                new DescriptorAttribute("UV1", "Lightmap UVs.")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("uv2"),
                new DescriptorAttribute("UV2", "The second texture coordinate set of the mesh, if present.", "http://docs.unity3d.com/ScriptReference/Mesh-uv2.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("boneWeights"),
                new DescriptorAttribute("Bone Weight", "The bone weights of each vertex.", "http://docs.unity3d.com/ScriptReference/Mesh-boneWeights.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("colors"),
                new DescriptorAttribute("Colors", "Vertex colors of the mesh.", "http://docs.unity3d.com/ScriptReference/Mesh-colors.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("normals"),
                new DescriptorAttribute("Normals", "The normals of the mesh.", "http://docs.unity3d.com/ScriptReference/Mesh-normals.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("tangents"),
                new DescriptorAttribute("Tangents", "The tangents of the mesh.", "http://docs.unity3d.com/ScriptReference/Mesh-tangents.html")));
            fields.Add(new InspectorField(type, Instances, type.GetProperty("bounds"),
                new DescriptorAttribute("Bounds", "The bounding volume of the mesh.", "http://docs.unity3d.com/ScriptReference/Mesh-bounds.html")));
        }
    }
}