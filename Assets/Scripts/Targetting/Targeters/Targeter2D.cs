using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public abstract class Targeter2D : MonoBehaviour
    {
        public abstract Collider2D[] GetTargets(LayerMask targetMask, float maxDistance);
    }
}

