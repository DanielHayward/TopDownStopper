using UnityEngine;

namespace DKH
{
    public abstract class Targeter : MonoBehaviour, IHaveID
    {
        public IdSO myId;
        public IdSO GetID()
        {
            return myId;
        }

        public abstract Collider[] GetTargets(LayerMask targetMask, float maxDistance);
    }
}

