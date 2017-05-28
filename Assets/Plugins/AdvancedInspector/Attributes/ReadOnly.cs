using System;
using System.Collections.Generic;

namespace AdvancedInspector
{
    /// <summary>
    /// Makes a Property read only (cannot be modified)
    /// It's grayed out in the inspector, even if there's a setter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class ReadOnlyAttribute : Attribute, IListAttribute, IRuntimeAttribute
    {
        public delegate bool ReadOnlyDelegate();

        #region IRuntime Implementation
        private string methodName = "";

        public string MethodName
        {
            get { return methodName; }
        }

        public Type Template
        {
            get { return typeof(ReadOnlyDelegate); }
        }

        private List<Delegate> delegates = new List<Delegate>();

        public List<Delegate> Delegates
        {
            get { return delegates; }
            set { delegates = value; }
        }
        #endregion

        public ReadOnlyAttribute() { }

        public ReadOnlyAttribute(Delegate method)
        {
            this.delegates.Add(method);
        }

        public ReadOnlyAttribute(string methodName)
        {
            this.methodName = methodName;
        }
    }
}