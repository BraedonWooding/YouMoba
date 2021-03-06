﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AdvancedInspector
{
    /// <summary>
    /// Restrict an object field to a list of object define by a delegate from the owner.
    /// In essence, turn any field into a drop down list of choices.
    /// Attributes cannot recieve a delegate, instead you pass the name of the method.
    /// The method itself is resolved when creating the field to know which instance to invoke.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RestrictAttribute : Attribute, IListAttribute, IRuntimeAttribute
    {
        public delegate IList RestrictDelegate();

        private RestrictDisplay display = RestrictDisplay.DropDown;

        /// <summary>
        /// Should this restricted field use the toolbox instead of a drop down popup.
        /// </summary>
        public RestrictDisplay Display
        {
            get { return display; }
            set { display = value; }
        }

        private int maxItemsPerRow = 6;

        /// <summary>
        /// When display is using Button, limits the number of items per row.
        /// </summary>
        public int MaxItemsPerRow
        {
            get { return maxItemsPerRow; }
            set { maxItemsPerRow = value; }
        }

        #region IRuntime Implementation
        private string methodName = "";

        public string MethodName
        {
            get { return methodName; }
        }

        public Type Template
        {
            get { return typeof(RestrictDelegate); }
        }

        private List<Delegate> delegates = new List<Delegate>();

        public List<Delegate> Delegates
        {
            get { return delegates; }
            set { delegates = value; }
        }
        #endregion

        public RestrictAttribute(string methodName)
            : this(methodName, RestrictDisplay.DropDown) { }

        public RestrictAttribute(string methodName, RestrictDisplay display)
        {
            this.methodName = methodName;
            this.display = display;
        }

        public RestrictAttribute(Delegate method)
            : this(method, RestrictDisplay.DropDown) { }

        public RestrictAttribute(Delegate method, RestrictDisplay display)
        {
            this.delegates.Add(method);
            this.display = display;
        }
    }

    public enum RestrictDisplay
    { 
        DropDown,
        Toolbox,
        Button
    }
}