using UnityEngine;

namespace DKH
{
    public class MovementLogic : MonoBehaviour, ISourceUser
    {
        [SerializeField] private float maxMagnitude = 99;

        public Vector3 Velocity { get; private set; }
        private Vector3 velocity = Vector3.zero;

        private IMover mover;

        public void SetSource(GameObject source)
        {
            mover = source.GetComponentInChildren<IMover>();
        }
        private void FixedUpdate()
        {
            ApplyLimits();
            Debug.Log(velocity);
            mover.Move(velocity * Time.deltaTime);
            Velocity = velocity;
            velocity = Vector3.zero;
        }
        protected virtual void ApplyLimits()
        {
            velocity = Vector3.ClampMagnitude(velocity, maxMagnitude);
        }
        public virtual void AddVelocity(Vector3 velocity)
        {
            this.velocity += velocity;
        }
        public void SetMaxMovementSpeed(float maxMoveSpeed)
        {
            maxMagnitude = maxMoveSpeed;
        }
        public void StopAll()
        {
            velocity = Vector3.zero;
        }
    }
}