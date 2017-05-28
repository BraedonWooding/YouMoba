using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AdvancedInspector
{
    /// <summary>
    /// Define an attribute that stores a method name
    /// Which should be turned into a delegate at runtime.
    /// </summary>
    public interface IRuntimeAttribute
    {
        /// <summary>
        /// Name of the MethodInfo to retrieve at runtime.
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// Prototype template of the delegate to create
        /// </summary>
        Type Template { get; }

        /// <summary>
        /// List of delegates to invoke.
        /// </summary>
        List<Delegate> Delegates { get; set; }
    }
}