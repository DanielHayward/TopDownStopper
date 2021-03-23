using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class HeadingMoveLogic : MoveLogicElement, IHeadingLogic
    {
        public Vector3 Heading => heading;
        private Vector3 heading = Vector3.zero;
        public Vector3 Velocity => velocity;
        private Vector3 velocity = Vector3.zero;

        [SerializeField] private FloatLink speedSource;

        [SerializeField] private float accelerationTime = 0.2f;
        [SerializeField] private float deccelerationTime = 0.1f;

        private Vector3 velocitySmoothing;
        private float accelTime;

        public override Vector3 GetVelocity()
        {
            if (paused)
            {
                return Vector3.zero;
            }
            CalculateVelocity();
            return velocity;
        }
        private void CalculateVelocity()
        {
            Vector3 targetVelocity = heading * speedSource.GetValue();
            if (targetVelocity == Vector3.zero)
            {
                accelTime = deccelerationTime;
            }
            else
            {
                accelTime = accelerationTime;
            }
            velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref velocitySmoothing, accelTime);

        }
        public void SetHeading(Vector3 direction, bool worldSpace = true)
        {
            if (!worldSpace)
            {
                heading = Camera.main.transform.InverseTransformPoint(direction);
            }
            else
            {
                heading = direction;
            }
        }
    }
}
