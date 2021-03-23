using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class PositionMover : MonoBehaviour, IMover
    {
        public void Move(Vector3 velocity)
        {
            transform.localPosition += velocity;
        }
    }
}