using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class StateMachine : MonoBehaviour, ISourceUser
    {
        [SerializeField] private State initialState;
        private State currentState = null;
        private State nextState = null;
        private Dictionary<IdSO, State> myState = new Dictionary<IdSO, State>();

        private void Awake()
        {
            currentState = initialState;
        }
        private void Start()
        {
            currentState.Enter();
        }
        public void SetSource(GameObject source)
        {
            State[] states = source.GetComponentsInChildren<State>();
            for (int i = 0; i < states.Length; i++)
            {
                myState.Add(states[i].GetID(), states[i]);
            }
        }
        private void Update()
        {
            nextState = currentState.Run();
            ChangeState(nextState);
        }
        public State GetCurrentState()
        {
            return currentState;
        }
        public void ChangeState(State newState)
        {
            if (newState != currentState)
            {
                currentState.Exit();
                currentState = newState;
                currentState.Enter();
            }
        }
        public void ChangeState(IdSO stateID)
        {
            if (myState.ContainsKey(stateID))
            {
                currentState.Exit();
                currentState = myState[stateID];
                currentState.Enter();
            }
        }
        public bool HasState(IdSO stateID)
        {
            return myState.ContainsKey(stateID);
        }
    }
}