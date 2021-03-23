using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DKH
{
    public class Vector2ReaderTimedThreshold
    {
        public bool active;
        public float duration;
        public EventHandler<Vector2InputEventArgs> threholdEvent;
    }

    public class Vector2InputEventArgs : EventArgs
    {
        public int currentStage;
        public float duration;
        public Vector2 passedValue;
    }
    public class Vector2Reader : InputReader
    {
        private InputAction vectorInput;
        public event EventHandler<Vector2InputEventArgs> OnInteract;

        public Vector2Reader(InputAction vectorInput)
        {
            this.vectorInput = vectorInput;
            this.vectorInput.performed += Released;
        }

        //If for some reason using an event for movement turns out to be to much we'll seperate movement from the typical command system and poll the value.
        private void Released(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke(this, new Vector2InputEventArgs { passedValue = context.ReadValue<Vector2>() });
        }
        public override void EndInteraction()
        {
            OnInteract?.Invoke(this, new Vector2InputEventArgs { passedValue = Vector2.zero });
        }

        public override void AddBehavior(InputBehavior inputBehavior)
        {
            OnInteract += inputBehavior.Execute;
        }

        public override void RemoveBehavior(InputBehavior inputBehavior)
        {
            OnInteract -= inputBehavior.Execute;
        }

        public override void Update()
        {

        }
    }
}
