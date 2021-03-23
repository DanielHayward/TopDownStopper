using UnityEngine;

namespace DKH
{
    public class RaycastTargeter2D : Targeter2D
    {
        public Vector2 rayDirection = new Vector2(0, 1);
        public bool useForward = true;

        public override Collider2D[] GetTargets(LayerMask targetMask, float maxDistance)
        {
            if (useForward)
                rayDirection = transform.forward * maxDistance;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, maxDistance, targetMask);
            if (hit.collider == null)
            {
                return null;
            }
            Collider2D[] result = new Collider2D[1];
            result[0] = hit.collider;
            return result;
        }
    }
}
