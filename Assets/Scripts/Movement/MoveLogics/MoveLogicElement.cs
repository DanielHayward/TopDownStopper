using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public abstract class MoveLogicElement : MonoBehaviour, ISourceUser, IHaveID
    {
        protected MovementLogic movementLogic;
        protected bool paused;

        [Tooltip("ID not required")]
        public IdSO Id = null;
        public IdSO GetID()
        {
            return Id;
        }
        public virtual void SetSource(GameObject source)
        {
            movementLogic = source.GetComponentInChildren<MovementLogic>();
        }
        public virtual void FixedUpdate()
        {
            if (!paused)
            {
                movementLogic.AddVelocity(GetVelocity());
            }
        }
        public abstract Vector3 GetVelocity();

        public void PauseMovement(bool paused)
        {
            this.paused = paused;
        }
    }
}

