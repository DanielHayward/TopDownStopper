using UnityEngine;
using System;

namespace DKH
{
    public abstract class IFloatSource : MonoBehaviour
    {
        public abstract float GetValue();
    }

    [Serializable]
    public class FloatLink
    {
        public float defaultValue = 0;
        public IFloatSource floatSource;

        public float GetValue()
        {
            if(floatSource == null)
            {
                return defaultValue;
            }
            else
            {
                return floatSource.GetValue();
            }
        }
    }
}
