using UnityEngine;

namespace DKH
{
    public class JoystickMoveBehavior : MonoBehaviour, ISourceUser
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private UnitBehavior[] behaviors;
        private float lastPressed = 0;
        private bool pressed = true;

        public void SetSource(GameObject source)
        {
            for (int behaviorIndex = 0; behaviorIndex < behaviors.Length; behaviorIndex++)
            {
                behaviors[behaviorIndex].SetSource(source);
            }
        }

        private void Update()
        {
            int stage = 0;
            if (joystick.Direction != Vector2.zero)
            {
                if(!pressed)
                {
                    pressed = true;
                    lastPressed = Time.time;
                }
                else
                {
                    stage = 1;      //We have been moving
                }
            }
            else if(pressed)
            {
                stage = 2;         //We are ending our press
                for (int behaviorIndex = 0; behaviorIndex < behaviors.Length; behaviorIndex++)
                {
                    behaviors[behaviorIndex].Run(stage, Time.time - lastPressed, joystick.Direction);
                }
                pressed = false;
            }

            if(pressed)
            {
                for (int behaviorIndex = 0; behaviorIndex < behaviors.Length; behaviorIndex++)
                {
                    behaviors[behaviorIndex].Run(stage, Time.time-lastPressed, joystick.Direction);
                }
            }
        }
    }
}
