using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DKH
{
    public class SphereCastTargeter : Targeter
    {
        public Vector3 rayDirection = new Vector3(0, 0, 1);
        public bool useForward = true;
        public Vector3 offset = Vector3.zero;
        public float radius;
        public override Collider[] GetTargets(LayerMask targetMask, float maxDistance)
        {
            if (useForward)
                rayDirection = transform.forward;
            RaycastHit hitInfo;
            if(Physics.SphereCast(transform.position + offset, radius, rayDirection, out hitInfo, maxDistance, targetMask))
            {
                Collider[] result = new Collider[1];
                result[0] = hitInfo.collider;
                return result;
            }
            return null;
        }
    }
}


