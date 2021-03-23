using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class SpawnPeriodicly : MonoBehaviour, IPoolableComponent
    {
        private float timer = 0;
        [SerializeField] private float timerDuration = 1;
        [SerializeField] private int maxSpawn = 1;
        [SerializeField] private SpawnableDataSO mySpawnable = null;

        public void InitialSpawn()
        {
            this.enabled = true;
        }
        public void Respawn()
        {
            this.enabled = true;
        }

        public void Despawn()
        {
            this.enabled = false;
        }

        private void Start()
        {
            timer = 0;
        }

        private void Update()
        {
            if (timer < timerDuration)
            {
                timer += Time.deltaTime;

                if (timer > timerDuration)
                {
                    SpawningManager.Spawn(mySpawnable, transform.position, maxSpawn);
                    timer = 0;
                }
            }
        }
    }
}