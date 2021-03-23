using System;
using UnityEngine;
using DKH.Utils.Timers;

namespace DKH
{
    public class TargeterController : MonoBehaviour, ISourceUser
    {
        public EventHandler<ConditionResultsEventArgs> OnTriggered;

        [SerializeField] private IdSO targeterID;
        private Targeter targeter = null;
        [SerializeField] private LayerMask targetLayers = new LayerMask();
        [SerializeField] private float checkDistance = 0;
        [SerializeField] private float interationDuration = 0;
        private CountingTimer executingTimer = new CountingTimer();
        public Collider[] lastResults { get; private set; } = new Collider[0];

        private void Awake()
        {
            executingTimer.duration = interationDuration;
            executingTimer.loop = true;
            executingTimer.OnTimer += (object sender, EventArgs e) => Check();
            executingTimer.Restart();
        }
        public void SetSource(GameObject source)
        {
            targeter = IdSO.FindComponents<Targeter>(source, targeterID)[0];
        }

        private void Update()
        {
            executingTimer.Update();
        }

        public bool Check()
        {
            lastResults = targeter.GetTargets(targetLayers, checkDistance);
            if (lastResults != null && lastResults.Length > 0)
            {
                OnTriggered?.Invoke(this, new ConditionResultsEventArgs { value = true });
                return true;
            }
            OnTriggered?.Invoke(this, new ConditionResultsEventArgs { value = false });
            return false;
        }

    }
}

