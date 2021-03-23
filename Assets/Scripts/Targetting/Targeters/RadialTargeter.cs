using UnityEngine;

namespace DKH
{
    public class RadialTargeter : Targeter
    {
        public Vector3 offset = Vector3.zero;

        public override Collider[] GetTargets(LayerMask targetMask, float maxDistance)
        {
            return Physics.OverlapSphere(transform.position + offset, maxDistance, targetMask);
        }
    }

}
