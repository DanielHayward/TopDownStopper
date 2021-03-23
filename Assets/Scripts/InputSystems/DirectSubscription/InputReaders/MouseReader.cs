using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace DKH
{
    public class MouseReader : InputReader
    {
        private InputAction vectorInput;
        public event EventHandler<Vector2InputEventArgs> OnInteract;

        public MouseReader(InputAction vectorInput)
        {
            this.vectorInput = vectorInput;
            this.vectorInput.performed += Released;
        }

        //If for some reason using an event for movement turns out to be to much we'll seperate movement from the typical command system and poll the value.
        private void Released(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke(this, new Vector2InputEventArgs { passedValue = context.ReadValue<Vector2>() });
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

        }

        public override void EndInteraction()
        {
            throw new NotImplementedException();
        }
    }
}