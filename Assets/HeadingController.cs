using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class HeadingController : MonoBehaviour, IHeadingLogic
    {
        [Tooltip("ID not required")]
        public IdSO Id = null;
        public Vector3 Heading => heading;
        private Vector3 heading = Vector3.zero;

        public IdSO GetID()
        {
            return Id;
        }

        public void SetHeading(Vector3 direction, bool worldSpace = true)
        {
            if (!worldSpace)
            {
                //heading = Camera.main.transform.InverseTransformPoint(direction);
            }
            else
            {
                heading = direction;
            }
        }
    }
}

