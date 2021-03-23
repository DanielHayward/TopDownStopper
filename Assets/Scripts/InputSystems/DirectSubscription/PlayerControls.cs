using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DKH
{
    public class PlayerControls : MonoBehaviour
    {
        public GameControls gameControls;

        private void Awake()
        {
            gameControls = new GameControls();
        }

        private void OnEnable()
        {
            gameControls.Enable();
        }

        private void OnDisable()
        {
            gameControls.Disable();
        }
    }
}
