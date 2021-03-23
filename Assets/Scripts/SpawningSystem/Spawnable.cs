using UnityEngine;
using DKH.Utils.Timers;

namespace DKH
{
    public class Spawnable : MonoBehaviour, IPoolableComponent
    {
        public SpawnableDataSO spawnable = null;
        [SerializeField] private float offDelay = 0.02f;

        private PooledObject po = null;
        private CountdownTimer delayTimer = new CountdownTimer();

        public void Start()
        {
            po = GetComponent<PooledObject>();
        }

        public void InitialSpawn()
        {
            delayTimer = new CountdownTimer();
            po = GetComponent<PooledObject>();
        }
        public void Respawn()
        {
            delayTimer.Clear();
        }
        public void Despawn()
        {
            SpawningManager.ReduceActiveCount(spawnable);
        }

        private void Update()
        {
            delayTimer.Update();
        }

        private void TurnOff()
        {
            po.Despawn();
        }

        public void Off()
        {
            delayTimer.Clear();
            delayTimer.timeRemaining = offDelay;
            delayTimer.OnTimer += TurnOff;
            delayTimer.Start();
        }
    }
}
