using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedInspector
{
#if UNITY_4_5 || UNITY_4_6
    [Serializable]
    public struct RangeInt
    {
        public int min;
        public int max;

        public RangeInt(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
#else
    [Serializable]
    public class RangeInt
    {
        public int min;
        public int max;

        public RangeInt(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
#endif
}