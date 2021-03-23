using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DKH
{
    public class CircleCastTargeter : Targeter2D
    {
        public Vector3 rayDirection = new Vector3(0, 0, 1);
        public bool useForward = true;
        public Vector3 offset = Vector3.zero;
        public float radius;
        public override Collider2D[] GetTargets(LayerMask targetMask, float maxDistance)
        {
            if (useForward)
            {
                rayDirection = transform.forward;
            }
            RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position + offset, radius, rayDirection, maxDistance, targetMask);
            if (hitInfo.collider == null)
            {
                return null;
            }
            Collider2D[] result = new Collider2D[1];
            result[0] = hitInfo.collider;
            return result;
        }
    }
}


