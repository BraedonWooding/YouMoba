using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvancedInspector
{
    /// <summary>
    /// Forces a field to display a FieldEditor related to its current runtime type instead of the field type.
    /// The Runtime version supply the type itself. Useful when the field value is null or for an unknown object picker.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RuntimeResolveAttribute : Attribute, IListAttribute, IRuntimeAttribute
    {
        public delegate Type RuntimeResolveDelegate();

        #region IRuntime Implementation
        private string methodName = "";

        public string MethodName
        {
            get { return methodName; }
        }

        public Type Template
        {
            get { return typeof(RuntimeResolveDelegate); }
        }

        private List<Delegate> delegates = new List<Delegate>();

        public List<Delegate> Delegates
        {
            get { return delegates; }
            set { delegates = value; }
        }
        #endregion

        public RuntimeResolveAttribute() { }

        public RuntimeResolveAttribute(string methodName)
        {
            this.methodName = methodName;
        }

        public RuntimeResolveAttribute(Delegate method)
        {
            this.delegates.Add(method);
        }
    }
}