using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DKH
{
    public class ControlLayout : MonoBehaviour
    {
        public PlayerControls playerControls;
        public Dictionary<InputSource, InputReader> sources = new Dictionary<InputSource, InputReader>();
        private List<InputReader> inputReaders = new List<InputReader>();

        [SerializeField] private InputBehaviorSet defaultInputBehaviorSet;

        private void Start()
        {
            if(defaultInputBehaviorSet != null)
            {
                LoadCommandSet(defaultInputBehaviorSet);
            }
        }

        private void Update()
        {
            for (int i = 0; i < inputReaders.Count; i++)
            {
                inputReaders[i].Update();
            }
        }


        public void LoadCommandSet(InputBehaviorSet set)
        {
            foreach (InputBehavior inputBehavior in set.behaviors)
            {
                if (!sources.ContainsKey(inputBehavior.inputSource))
                {
                    InputAction inputAction = playerControls.gameControls.asset.FindAction(inputBehavior.inputSource.ToString());
                    if (inputAction == null)
                    {
                        Debug.LogError("Input Action Enum Name is Wrong Double Check your InputSource Enum and command sets.");
                        return;
                    }
                    sources.Add(inputBehavior.inputSource, GetReader(inputBehavior.inputParameters, inputAction));
                    inputReaders.Add(sources[inputBehavior.inputSource]);
                }

                sources[inputBehavior.inputSource].AddBehavior(inputBehavior);
            }
        }
        public void UnloadCommandSet(InputBehaviorSet set)
        {
            foreach (InputBehavior inputBehavior in set.behaviors)
            {
                if (sources[inputBehavior.inputSource].isActive)
                {
                    sources[inputBehavior.inputSource].EndInteraction();
                }
                sources[inputBehavior.inputSource].RemoveBehavior(inputBehavior);
            }
        }

        public InputReader GetReader(InputParameters inputParameters, InputAction inputAction)
        {
            switch (inputParameters)
            {
                case InputParameters.floatRangeReader:
                    return new FloatRangeReader(inputAction);
                case InputParameters.floatAxisReader:
                    return new FloatAxisReader(inputAction);
                case InputParameters.vector2Reader:
                    return new Vector2Reader(inputAction);
            }
            return new FloatRangeReader(inputAction);
        }
    }

    public abstract class InputReader
    {
        public bool isActive;
        public abstract void Update();
        public abstract void AddBehavior(InputBehavior inputCommand);
        public abstract void RemoveBehavior(InputBehavior inputCommand);
        public abstract void EndInteraction();
    }
}


