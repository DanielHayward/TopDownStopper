using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class DespawnOnCollision : MonoBehaviour, ISourceUser
    {
        private Spawnable spawnable;
        public void SetSource(GameObject source)
        {
            spawnable = GetComponent<Spawnable>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            spawnable.Off();
        }

        private void OnTriggerEnter(Collider other)
        {
            spawnable.Off();
        }
    } 
}

