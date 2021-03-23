using UnityEngine;

namespace DKH
{
    public class RadialTargeter2D : Targeter2D
    {
        public Vector3 offset = Vector3.zero;

        public override Collider2D[] GetTargets(LayerMask targetMask, float maxDistance)
        {
            return Physics2D.OverlapCircleAll(transform.position + offset, maxDistance, targetMask);
        }
    }
}
