using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace DKH
{
    public class FloatReaderTimedThreshold
    {
        public bool active;
        public float duration;
        public EventHandler<FloatInputEventArgs> threholdEvent;
    }

    public class FloatInputEventArgs : EventArgs
    {
        public int currentStage;
        public float duration;
        public float passedValue;
    }

    public class FloatRangeReader : InputReader
    {
        private InputAction floatInput;
        public event EventHandler<FloatInputEventArgs> OnDown;
        public event EventHandler<FloatInputEventArgs> OnUp;
        public event EventHandler<FloatInputEventArgs> OnThreshold;
        public List<FloatReaderTimedThreshold> Thresholds = new List<FloatReaderTimedThreshold>();


        public bool isDown;
        public float currentDownTimeStamp;
        public float lastUpTimeStamp;
        public float CurrentDuration
        {
            get { return Time.time - currentDownTimeStamp;}
        }


        public FloatRangeReader(InputAction floatInput) 
        {
            this.floatInput = floatInput;
        
            floatInput.started += StartPress;
            floatInput.performed += Shift;
            floatInput.canceled += EndPress;
        }

        private void StartPress(InputAction.CallbackContext context)
        {
            Debug.Log("Pres");
            isDown = true;
            isActive = true;
            currentDownTimeStamp = Time.time;
            OnDown?.Invoke(this, new FloatInputEventArgs { currentStage = 0, duration = CurrentDuration, passedValue = context.ReadValue<float>() });
            for (int i = 0; i < Thresholds.Count; i++)
            {
                Thresholds[i].active = true;
            }
        }

        private void Shift(InputAction.CallbackContext context)
        {
            Debug.Log("ValueShifted");
            OnUp?.Invoke(this, new FloatInputEventArgs { currentStage = 1, duration = CurrentDuration, passedValue = context.ReadValue<float>() });
        }

        private void EndPress(InputAction.CallbackContext context)
        {
            Debug.Log("EndPress");
            isDown = false;
            isActive = false;
            OnUp?.Invoke(this, new FloatInputEventArgs { currentStage = 2, duration = CurrentDuration, passedValue = context.ReadValue<float>() });
            lastUpTimeStamp = Time.time;
            for (int i = 0; i < Thresholds.Count; i++)
            {
                Thresholds[i].active = false;
            }
        }

        public override void EndInteraction()
        {
            Debug.Log("EndPress");
            isDown = false;
            isActive = false;
            OnUp?.Invoke(this, new FloatInputEventArgs { currentStage = 2, duration = CurrentDuration, passedValue = 0 });
            lastUpTimeStamp = Time.time;
            for (int i = 0; i < Thresholds.Count; i++)
            {
                Thresholds[i].active = false;
            }
        }

        public override void Update()
        {
            for (int i = 0; i < Thresholds.Count; i++)
            {
                if (Thresholds[i].active && CurrentDuration > Thresholds[i].duration)
                {
                    Thresholds[i].active = false;
                    OnThreshold = Thresholds[i].threholdEvent;
                    OnThreshold?.Invoke(this, new FloatInputEventArgs { currentStage = i+2, duration = CurrentDuration, passedValue = floatInput.ReadValue<float>() });
                }
            }
        }

        public override void AddBehavior(InputBehavior inputCommand)
        {
            OnDown += inputCommand.Execute;
            OnUp += inputCommand.Execute;
            //for (int i = 0; i < inputCommand.behavior.thresholds.Length; i++)
            //{
            //    Thresholds.Add(new FloatReaderTimedThreshold());
            //    Thresholds[i].duration = inputCommand.behavior.thresholds[i];
            //    Thresholds[i].active = false;
            //    Thresholds[i].threholdEvent += inputCommand.Execute;
            //}
        }

        public override void RemoveBehavior(InputBehavior inputCommand)
        {
            OnDown -= inputCommand.Execute;
            OnUp -= inputCommand.Execute;
            //for (int i = 0; i < Thresholds.Count; i++)
            //{
            //    Thresholds[i].threholdEvent -= inputCommand.Execute;
            //}
            //Thresholds.Clear();
        }
    }
    public class FloatAxisReader : InputReader
    {
        private InputAction floatInput;
        public event EventHandler<FloatInputEventArgs> OnInteract;
        private bool continuous = false;
        private bool down = false;
        private float value;

        public FloatAxisReader(InputAction floatInput, bool continuous = false) 
        {
            this.floatInput = floatInput;
            this.continuous = continuous;
            floatInput.started += Started;
            floatInput.performed += Pressed;
            floatInput.canceled += Released;
        }

        //If for some reason using an event for movement turns out to be to much we'll seperate movement from the typical command system and poll the value.
        private void Started(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke(this, new FloatInputEventArgs { currentStage=0, passedValue = context.ReadValue<float>() });
            value = context.ReadValue<float>();
            down = true;
        }        
        private void Pressed(InputAction.CallbackContext context)
        {
            if(!continuous)
            {
                OnInteract?.Invoke(this, new FloatInputEventArgs { currentStage = 1, passedValue = context.ReadValue<float>() });
            }
        }    
        private void Released(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke(this, new FloatInputEventArgs { currentStage = 2, passedValue = context.ReadValue<float>() });
            value = context.ReadValue<float>();
            down = false;
        }
    
        public override void EndInteraction()
        {
            value = 0;
            OnInteract?.Invoke(this, new FloatInputEventArgs { passedValue = 0 });
        }

        public override void AddBehavior(InputBehavior inputCommand)
        {
            //OnInteract += inputCommand.Execute;
        }

        public override void RemoveBehavior(InputBehavior inputCommand)
        {
            //OnInteract -= inputCommand.Execute;
        }

        public override void Update()
        {
            if(continuous && down)
            {
                OnInteract?.Invoke(this, new FloatInputEventArgs { currentStage = 1, passedValue = value });
            }
        }
    }
}
