using System;
using UnityEngine;

namespace DKH.Utils.Timers
{
    //Timer has seperate values for Duration and TimeElapsed.  Allow the user more control over the time range and values than the CountdownTimer.
    public class CountingTimer
    {
        public bool active { get; private set; } = false;
        public float timeElapsed = 0;
        public float timeSpeed = 1;
        public float duration = 0;
        public bool loop = false;
        public event EventHandler<EventArgs> OnTimer;

        public void Start()
        {
            active = true;
        }
        public void Restart()
        {
            active = true;
            timeElapsed = 0;
        }
        public void Pause()
        {
            active = false;
        }
        public void Toggle()
        {
            active = !active;
        }
        public void StopTimer()
        {
            active = false;
            timeElapsed = 0;
        }
        public void TriggerTimerEvents(bool stopTimer)
        {
            OnTimer.Invoke(this, EventArgs.Empty);
            if (stopTimer)
            {
                StopTimer();
            }
        }
        public float GetTimeRemaining()
        {
            return duration - timeElapsed;
        }
        public bool Update()
        {
            if (active)
            {
                timeElapsed += (Time.deltaTime * timeSpeed);
                if (timeElapsed >= duration)
                {
                    if (OnTimer != null)
                    {
                        OnTimer?.Invoke(this, EventArgs.Empty);
                    }
                    if (loop)
                    {
                        timeElapsed = 0;
                    }
                    else
                    {
                        active = false;
                    }
                }
            }
            return !active;
        }
    }

    public class CountdownTimer
    {
        private bool active = false;
        public float timeRemaining = 0;
        public float timeSpeed = 1;
        public Action OnTimer;

        public void Clear()
        {
            OnTimer = null;
        }
        public void Start()
        {
            active = true;
        }
        public void Pause()
        {
            active = false;
        }
        public void Toggle()
        {
            active = !active;
        }
        public void StopTimer()
        {
            active = false;
            timeRemaining = 0;
        }
        public void TriggerTimerEvents(bool stopTimer)
        {
            OnTimer();
            if (stopTimer)
            {
                StopTimer();
            }
        }
        public bool Update()
        {
            if (active)
            {
                timeRemaining -= (Time.deltaTime * timeSpeed);
                if (timeRemaining <= 0)
                {
                    if (OnTimer != null)
                    {
                        OnTimer();
                    }
                    active = false;
                }
            }
            return !active;
        }
    }
}
