using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class TopDownMoveBehavior : UnitBehavior
    {
        [SerializeField] private IdSO inputLogicID;
        private IInputMoveLogic[] inputLogic;

        public override void CacheSource(GameObject source)
        {
            inputLogic = IdSO.FindComponents<IInputMoveLogic>(source, inputLogicID);
        }

        public override void Run(int currentStage, float duration, Vector2 vector2)
        {
            for (int i = 0; i < inputLogic.Length; i++)
            {
                inputLogic[i].SetInputDirection(vector2);
            }
            if (currentStage == 0)
            {
                //OnStartMovingByInput
            }
            else if (currentStage == 2)
            {
                //OnStopMovingByInput
            }
        }

        public override void Run()
        {
            for (int i = 0; i < inputLogic.Length; i++)
            {
                inputLogic[i].SetInputDirection(Vector3.zero);
            }
        }
    }

}
