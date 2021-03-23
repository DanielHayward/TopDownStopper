using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.Utils
{
    public class MovementUtils : MonoBehaviour
    {
        public float MapToRange(float rotation)
        {
            rotation %= 360.0f;
            if (Mathf.Abs(rotation) > 180.0f)
            {
                if (rotation < 0.0f)
                {
                    rotation += 360.0f;
                }
                else
                {
                    rotation -= 360.0f;
                }
            }
            return rotation;
        }
    }
}

