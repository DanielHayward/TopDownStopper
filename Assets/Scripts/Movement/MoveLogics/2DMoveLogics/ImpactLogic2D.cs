using UnityEngine;

namespace DKH
{
    public class ImpactLogic2D : MoveLogicElement//, IForceMoveLogic
    {
        //[SerializeField] private Vector3VelocityCommandData gravityData;
        //public Vector3 Forces { get { return impactForces; } }
        private Vector3 impactForces = Vector3.zero;
        //private float decayRate = 1;

        //private ICollisionResolution collisionResolution;

        public override void SetSource(GameObject source)
        {
            base.SetSource(source);
            //collisionResolution = source.GetComponentInChildren<ICollisionResolution>();
        }
        public void AddVelocity(Vector3 velocity, bool worldSpace = true)
        {
            //impactForces += velocity;
        }
        public void SetVelocity(Vector3 velocity, bool worldSpace = true)
        {
            //impactForces = velocity;
        }
        public override Vector3 GetVelocity()
        {
            //if (collisionResolution.Collisions.SharesFlag(CollisionFlags.Below))
            //{
            //    impactForces = new Vector3(impactForces.x * 0.05f, -0.01f, 0);
            //}
            //if (collisionResolution.Collisions.SharesFlag(CollisionFlags.Above))
            //{
            //    impactForces = new Vector3(impactForces.x * 0.95f, -0.01f, 0);
            //}
            //if (collisionResolution.Collisions.SharesFlag(CollisionFlags.LeftRight))
            //{
            //    impactForces = new Vector3(0, impactForces.y, 0);  //Bounce(ref returnValue);
            //}
            //if (impactForces.y < gravityData.constantVelocity.y || impactForces.y > 0)
            //{
            //    impactForces *= (1.0f - (decayRate * Time.deltaTime));
            //}
            ////Vector3 returnValue = impactForces;
            return impactForces;
        }

        public void Stop()
        {
            //impactForces = Vector3.zero;
        }


        private void Bounce(ref Vector3 bounceVector)
        {
            bounceVector = Vector3.zero;
        }
    }
}