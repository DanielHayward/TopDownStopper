using UnityEngine;

namespace DKH
{
    public class RaycastTargeter : Targeter
    {
        public Vector3 rayDirection = new Vector3(0, 1, 0);
        public bool useForward = true;

        public override Collider[] GetTargets(LayerMask targetMask, float maxDistance)
        {
            if (useForward)
                rayDirection = transform.forward * maxDistance;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, rayDirection, out hit, maxDistance, targetMask))
            {
                Collider[] result = new Collider[1];
                result[0] = hit.collider;
                return result;
            }
            return null;
        }
    }
}
