using System;
using UnityEngine;

namespace DKH
{
    public class DestinationMoveLogic : MoveLogicElement, IDesinationMoveLogic, IMoveSpeedLogic, IHeadingLogic
    {
        public Action<Vector3> OnReachDestination;
        public Vector3[] Destinations { get; private set; } = new Vector3[] { };
        public Transform[] Targets { get; private set; }
        public Vector3 Velocity { get; set; } = new Vector3 { x = 1, y = 0, z = 0 };
        public float Speed { get { return speed; } }
        public Vector2 Location => transform.position;

        public Vector3 Heading => throw new NotImplementedException();

        public float HeadingX => throw new NotImplementedException();

        public float HeadingY => throw new NotImplementedException();

        public float HeadingZ => throw new NotImplementedException();

        [SerializeField] private float speed = 3;
        [SerializeField] private bool loop = false;

        //private Vector3 targetPosition = Vector3.zero;
        private int currentWaypoint = 0;

        //private Utilis.Timers.CountdownTimer timer = new Utilis.Timers.CountdownTimer();
        //[SerializeField] private float waitAtDestinationDuration = 0;
        [SerializeField] private float stoppingDistance = 0.2f;
        private Vector3 distance;
        private bool ignoreHorizontalAxis;
        private bool ignoreVerticalAxis;
        private bool ignoreDepthAxis;

        void IDesinationMoveLogic.AddListenerOnDestination(Action<Vector3> func)
        {
            OnReachDestination += func;
        }

        void IDesinationMoveLogic.RemoveListenerOnDestination(Action<Vector3> func)
        {
            OnReachDestination -= func;
        }
        public void Awake()
        {
            //timer.OnTimer += DoneWaiting;
        }
        public void Update()
        {
            if (!paused && currentWaypoint < Destinations.Length)
            {
                //timer.Update();
            }
        }
        public override Vector3 GetVelocity()
        {
            if (paused)
            {
                Velocity = Vector3.zero;
                return Velocity;
            }
            else if (Destinations.Length > currentWaypoint)
            {
                //Axis codes
                distance = (Destinations[currentWaypoint] - transform.position);
                if (ignoreHorizontalAxis)
                {
                    distance.x = 0;
                }
                if (ignoreVerticalAxis)
                {
                    distance.y = 0;
                }
                if (ignoreDepthAxis)
                {
                    distance.z = 0;
                }
            }
            if (Speed * Time.fixedDeltaTime > Mathf.Abs(distance.magnitude))
            {
                Velocity = distance;
            }
            else if (distance.magnitude < stoppingDistance)
            {
                paused = true;
                //timer.timeRemaining = waitAtDestinationDuration;
                //timer.Start();
                Velocity = Vector3.zero;
            }
            else
            {
                Velocity = (distance.normalized * Speed);
            }
            return Velocity;
        }
        public void DoneWaiting()
        {
            if (currentWaypoint + 1 >= Destinations.Length)
            {
                if (loop)
                {
                    currentWaypoint = 0;
                }
            }
            else
            {
                paused = false;
                currentWaypoint++;
            }
        }

        public void SetDestination(Vector3 destination)
        {
            Destinations = new Vector3[] { destination };
            SetDestinations(Destinations);
        }
        public void SetDestinations(Vector3[] destinations, bool loop = false)
        {
            this.Destinations = destinations;
            currentWaypoint = 0;
            //targetPosition = Destinations[currentWaypoint];
            paused = false;
            this.loop = loop;
            distance = (Destinations[currentWaypoint] - transform.position);
        }
        public void ClearDestinations()
        {
            currentWaypoint = 0;
            this.Destinations = new Vector3[] { };
        }
        public void SetDestinationWeight(float weight)
        {

        }
        public void SetTarget(Transform target)
        {
            this.Targets = new Transform[] { target };
        }
        public void SetTargets(Transform[] targets)
        {
            this.Targets = targets;
        }
        public void ClearTargets()
        {
            currentWaypoint = 0;
            Targets = new Transform[] { };
        }
        public void SetTargetWeight(float weight)
        {

        }
        

        public void SetMovementSpeed(float moveSpeed)
        {
            speed = moveSpeed;
        }

        public void AddMovementSpeed(float moveSpeed)
        {
            speed += moveSpeed;
        }

        public void SetVerticalSpeed(float moveSpeed)
        {
        }
        public void AddVerticalSpeed(float moveSpeed)   //No capping a this point, go back to the behavior or command to control capping.  Use a stat from a statsheet if it seems unit dependant.
        {
        }

        public void SetHorizontalSpeed(float moveSpeed)
        {
        }
        public void AddHorizontalSpeed(float moveSpeed)   //No capping a this point, go back to the behavior or command to control capping.  Use a stat from a statsheet if it seems unit dependant.
        {
        }

        public void IgnoreAxis(bool ignoreHorizontalAxis, bool ignoreVerticalAxis, bool ignoreDepthAxis)
        {
            this.ignoreHorizontalAxis = ignoreHorizontalAxis;
            this.ignoreVerticalAxis = ignoreVerticalAxis;
            this.ignoreDepthAxis = ignoreDepthAxis;
        }

        public void Stop()
        {
            this.paused = true;
            Velocity = Vector3.zero;
        }
        public void SetDepthSpeed(float moveSpeed)
        {
            throw new NotImplementedException();
        }

        public void AddDepthSpeed(float moveSpeed)
        {
            throw new NotImplementedException();
        }

        public void SetHeading(Vector3 direction, bool worldSpace = true)
        {
            throw new NotImplementedException();
        }

        public void SetYHeading(float direction)
        {
            throw new NotImplementedException();
        }

        public void SetXHeading(float direction)
        {
            throw new NotImplementedException();
        }

        public void SetZHeading(float direction)
        {
            throw new NotImplementedException();
        }
    }


    //private void Update()
    //{
    //    CalculateVelocity();
    //    collisionResolution.AttemptMovement((velocity + impactForces) * Time.deltaTime);

    //    //if(impactForces.magnitude < velocity.magnitude+stoppingPower)
    //    //{
    //    //    impactForces = Vector2.zero;
    //    //}
    //    //else
    //    //{
    //    impactForces = Vector2.Lerp(impactForces, Vector2.zero, Mathf.Min(impactForceDecayRatio + (Time.deltaTime * ((collisionResolution.collisions.SharesFlag(CollisionFlags.Below)) ? impactDecayTimeGrounded : impactDecayTmeAirborne)), 100));

    //}
            //{
            //  if(attemptingToPush)
            //  {
            //      AttemptPush();
            //  }
            //  else
            //  {
            //      pushing = false;
            //}
            //if (collisionResolution.collisions.above || collisionResolution.collisions.below)
            //{
            //    velocity.y = 0;
            //      if(mover.collisions.below)
            //      {
            //          animator.SetTrigger("Land");
            //          if(!landed)
            //          {
            //              landed = true;
            ////              PooledObject po = SpawningManager.Spawn(landingDust, (transform.position), 1);
            //        //      po.GetComponent<TimedSpawnable>().SetHeading((horizontalInput.x > 0) ? 1 : 0);
            //          }
            //      }
            //}
            //  if (mover.collisions.below && jumping)
            //  {
            //      velocity.y = jumpVelocity;
            //      animator.ResetTrigger("Land");
            //      landed = false;
            //  }
            //}

    //private void CalculateVelocity()
    //{
    //    {
    //        //if(verticalInput > 0)
    //        //{
    //        //    targetVelocityY = verticalInput * jumpVelocity;
    //        //}
    //        //else
    //        //{
    //        //targetVelocityY = gravity * Time.deltaTime;
    //        //}


    //        //if((mover.collisions.left || mover.collisions.right) && pushing && !dead)
    //        //{
    //        //    targetVelocityX = horizontalInput.x * (moveSpeed/1.2f);
    //        //    animator.SetBool("Pushing", pushing);
    //        //}
    //        //else
    //        //{
    //        //    targetVelocityX = horizontalInput.x * moveSpeed;
    //        //    pushing = false;
    //        //    animator.SetBool("Pushing", false);
    //        //}

    //        //if(dashing && !dead)
    //        //{
    //        //    velocity.y = 0;
    //        //    targetVelocityX = heading * dashPower;
    //        //    dashingTimer.Update();
    //        //    animator.SetFloat("VertVelocity", velocity.y);
    //        //}
    //        //else if(climbing && !dead)
    //        //{
    //        //    AttemptClimb();
    //        //    velocity.y = (verticalInput * climbingSpeed);
    //        //    animator.SetFloat("VertVelocity", velocity.y);
    //        //}
    //        //else
    //        //{
    //        //    animator.SetFloat("VertVelocity", velocity.y);
    //        //    velocity.y += gravity * Time.deltaTime;
    //        //}
    //    }


    //    velocity = new Vector2(Mathf.SmoothDamp(Velocity.x, horizontalInput * horizontalMoveSpeed, ref velocityXSmoothing, (collisionResolution.collisions.SharesFlag(CollisionFlags.Below)) ? accelerationTimeGrounded : accelerationTimeAirborne),// maximumHoriztonalAcceleration),
    //    Mathf.SmoothDamp(Velocity.y, verticalInput * verticalMoveSpeed, ref velocityYSmoothing, accelerationTimeAirborne));//, maximumVerticalAcceleration));
    //    if (impactForces.y > 0 && collisionResolution.collisions.SharesFlag(CollisionFlags.Above))
    //    {
    //        impactForces = Vector2.zero;
    //    }
    //    animator.SetBool("Moving", (velocity.x > 0.1f || velocity.x < -0.1f));
    //    if (!collisionResolution.collisions.SharesFlag(CollisionFlags.Below))
    //    {
    //        animator.SetFloat("VertVelocity", (velocity.y + impactForces.y));
    //    }
    //    else
    //    {
    //        animator.SetFloat("VertVelocity", 0);
    //    }
    //    animator.SetFloat("Heading", Heading.x);
    //}

}






//public void SetMoveDirection(Vector2 direction)
//    {
//        this.horizontalInput = direction.normalized.x;
//        this.verticalInput = direction.normalized.y;
//    }

//    //public void SetHorizontalMoveDirection(float horizontalInput)
//    //{
//    //    //if (horizontalInput != 0 && !isDashing)
//    //    //{
//    //    //    Heading = horizontalInput;
//    //    //    //animator.SetFloat("Heading", heading);
//    //    //}
//    //}

//    public void SetVerticalMoveDirection(float verticalInput)
//    {
//        verticalInput = Mathf.Clamp(verticalInput, -1, 1);
//        this.verticalInput = verticalInput;
//    }

//    public void SetHeading(Vector3 heading)
//    {
//        if (heading.x != 0)
//        { 
//            Heading = heading;
//        }
//    }











//    public void AddMovementSpeed(float moveSpeed, float weight, float accelerationRate, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void SetVerticalSpeed(float moveSpeed, float weight, float accelerationRate, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void AddVerticalSpeed(float moveSpeed, float weight, float accelerationRate, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void SetHorizontalSpeed(float moveSpeed, float weight, float accelerationRate, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void AddHorizontalSpeed(float moveSpeed, float weight, float accelerationRate, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void SetMaxMovementSpeed(float maxMagnitude)
//    {
//        throw new NotImplementedException();
//    }

//    public void PauseMovement(bool pause)
//    {
//        throw new NotImplementedException();
//    }

//    public void SetDirection(Vector3[] direction, bool worldSpace = true, float weight = 1)
//    {
//        throw new NotImplementedException();
//    }



//    public void AddSteeringDirection(Vector3[] destination, bool worldSpace = true, float weight = 1)
//    {
//        throw new NotImplementedException();
//    }

//    public void SetMovementSpeed(float moveSpeed, float weight, float accelerationRate)
//    {
//        throw new NotImplementedException();
//    }



//    public void SetMovementSpeed(float moveSpeed, float accelerationRate)
//    {
//        this.moveSpeed = moveSpeed;
//    }

//    public void AddMovementSpeed(float moveSpeed, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void SetVerticalSpeed(float moveSpeed, float accelerationRate, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void AddVerticalSpeed(float moveSpeed, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void SetHorizontalSpeed(float moveSpeed, float accelerationRate, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }

//    public void AddHorizontalSpeed(float moveSpeed, bool worldSpace = true)
//    {
//        throw new NotImplementedException();
//    }


//private float gravity;
//private float jumpVelocity;
//[SerializeField] private float jumpHeight = 4;
//[SerializeField] private float timeToJumpApex = .4f;
//[SerializeField] private float climbingSpeed = 4;

//public float Heading { get; private set; } = 1;     //Must be -1 or 1.  Value is pushed to animator.
//public bool pushing { get; private set; } = false;
//public bool attemptingToPush { get; set; } = false;
//private bool isDashing = false;

// private bool jumping = false;
// private Utilis.Timers.CountdownTimer dashingTimer = new Utilis.Timers.CountdownTimer();
//// public SpawnableDataSO landingDust;
// private bool landed = false;

// private float lastPush = 0;
// //[SerializeField] GameObject camDummy;
// [SerializeField] PerspectiveManager perspectiveManager;
// private Animator animator;
// private bool climbing = false;
// private bool dead = false;


//public void Death()
//{
//    //verticalInput = 0;
//    //horizontalInput = new Vector3(0, 0, 0);
//    //velocity.x = 0;
//    //velocity.y = 0;
//    //dead = true;
//    //jumping = false;
//    //climbing = false;
//    //dashing = false;
//    //animator.SetBool("Dashing", false);
//    //animator.SetBool("Jumping", false);
//    //animator.SetBool("Climbing", false);
//    //animator.SetBool("Death", true);
//    //camDummy.transform.position = transform.position;
//    //perspectiveManager.SwitchToCharacter(3);
//}

//public void Restart()
//{
//    //dead = false;
//    //animator.SetBool("Death", false);
//    //transform.position = Checkpoint.lastCheckpoint;
//    //EventMessenger<FloatEventParams>.TriggerEvent("NinjaRespawned", new FloatEventParams());
//    //perspectiveManager.SwitchToCharacter(0);
//}

//private void OnEnable()
//{
//    //dashingTimer.OnTimer += StopDashing;
//}

//private void OnDisable()
//{
//    //dashingTimer.OnTimer -= StopDashing;
//}

//private void StopDashing()
//{
//    //dashing = false;
//    //velocity.x = 0;
//    //animator.SetBool("Dashing", false);
//    //dashingTimer.StopTimer();
//}
//public void AttemptPush()
//{
//    //attemptingToPush = true;
//    //if (mover.collisions.pushableLeft || mover.collisions.pushableRight)
//    //{
//    //    pushing = true;

//    //    if(Time.time - lastPush > 0.5f)
//    //    {
//    //        lastPush = Time.time;
//    //        PooledObject po = SpawningManager.Spawn(GameAssets.Instance.pushingDust, (transform.position - new Vector3(0, -0.0f, 0)), 1);
//    //        po.GetComponent<TimedSpawnable>().SetHeading((horizontalInput.x > 0) ? 1 : 0);
//    //    }
//    //}
//    //else
//    //{
//    //    pushing = false;
//    //}
//}

//public void AttemptClimb()
//{
//    //if(!dead)
//    //{
//    //    Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.one, 0);
//    //    for(int i = 0; i < colliders.Length; i++)
//    //    {
//    //        if(colliders[i].CompareTag("Rope"))
//    //        {
//    //            velocity.x = 0;
//    //            climbing = true;
//    //            dashing = false;
//    //            jumping = false;
//    //            animator.SetBool("Climbing", true);
//    //            animator.SetBool("Dashing", false);
//    //            animator.SetBool("Jumping", false);
//    //            return;
//    //        }
//    //    }
//    //    climbing = false;
//    //    animator.SetBool("Climbing", false);
//    //}
//}

//public void Dash()
//{
//    //if (!dead)
//    //{
//    //    heading = horizontalInput.x;
//    //    dashing = true;
//    //    jumping = false;
//    //    climbing = false;
//    //    dashingTimer.timeRemaining = dashDuration;
//    //    dashingTimer.Start();
//    //    animator.SetBool("Dashing", true);
//    //    animator.SetBool("Climbing", false);
//    //    animator.SetBool("Jumping", false);
//    //}
//}

//public void MeleeAction()
//{
//    //animator.SetTrigger("PrimaryAbility");
//}


////public void EndClimb()
////{
////    //climbing = false;
////    //animator.SetBool("Climbing", false);
////}

//public void StartJump()
//{
//    //if (!dead)
//    //{
//    //    pushing = false;
//    //    jumping = true;
//    //    climbing = false;
//    //    animator.SetBool("Climbing", false);
//    //    animator.SetTrigger("Jumping");
//    //}
//}
//public void EndJump()
//{
//    //pushing = false;
//    //jumping = false;
//    //animator.ResetTrigger("Jumping");
//}