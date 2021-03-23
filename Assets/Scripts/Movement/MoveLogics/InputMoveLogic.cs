using System;
using UnityEngine;

namespace DKH
{
    public class InputMoveLogic : MoveLogicElement, IInputMoveLogic, IHeadingLogic
    {
        public Vector3 Heading => heading;
        private Vector3 heading = Vector3.zero;
        public Vector3 VectorInput => vectorInput;
        private Vector3 vectorInput = Vector3.zero;
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
            Vector3 targetVelocity = vectorInput * speedSource.GetValue();
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
        public void SetInputDirection(Vector3 direction, bool worldSpace = true, float weight = 1)
        {
            if (!worldSpace)
            {
                vectorInput = Camera.main.transform.InverseTransformPoint(direction);
            }
            else
            {
                vectorInput = direction;
            }
        }
        public void SetHeading(Vector3 direction, bool worldSpace = true)
        {
            if(!worldSpace)
            {
                heading = Camera.main.transform.InverseTransformPoint(direction);
            }
            else
            {
                heading = direction;
            }
        }
        public void Stop()
        {
            vectorInput = Vector3.zero;
        }
    }
}
