using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedInspector
{
#if UNITY_4_5 || UNITY_4_6
    [Serializable]
    public struct RangeFloat
    {
        public float min;
        public float max;

        public RangeFloat(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
#else
    [Serializable]
    public class RangeFloat
    {
        public float min;
        public float max;

        public RangeFloat(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
#endif
}