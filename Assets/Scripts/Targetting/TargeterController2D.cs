using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DKH.Utils.Timers;

namespace DKH
{
    public class TargeterController2D : MonoBehaviour
    {
        public EventHandler<ConditionResultsEventArgs> OnTriggered;

        [SerializeField] private Targeter2D targeter = null;
        [SerializeField] private LayerMask targetLayers = new LayerMask();
        [SerializeField] private float checkDistance = 0;
        [SerializeField] private float interationDuration = 0;
        private CountingTimer executingTimer = new CountingTimer();
        public Collider2D[] lastResults { get; private set; } = new Collider2D[0];

        private void Awake()
        {
            executingTimer.duration = interationDuration;
            executingTimer.loop = true;
            executingTimer.OnTimer += (object sender, EventArgs e) => Check();
            executingTimer.Restart();
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

