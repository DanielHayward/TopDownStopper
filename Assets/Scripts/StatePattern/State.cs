using System.Collections.Generic;
using UnityEngine;


namespace DKH
{
    public class State : MonoBehaviour
    {
        [SerializeField] protected IdSO StateID;
        [SerializeField] protected List<UnitBehavior> entryBehaviors = new List<UnitBehavior>();
        [SerializeField] protected List<UnitBehavior> runBehaviors = new List<UnitBehavior>();
        [SerializeField] protected List<UnitBehavior> exitBehaviors = new List<UnitBehavior>();

        public IdSO GetID()
        {
            return StateID;
        }
        public virtual void Enter()
        {
            for (int i = 0; i < entryBehaviors.Count; i++)
            {
                entryBehaviors[i].Run();
            }
        }
        public virtual State Run()
        {
            for (int i = 0; i < runBehaviors.Count; i++)
            {
                runBehaviors[i].Run();
            }
            return this;
        }
        public virtual void Exit()
        {
            for (int i = 0; i < exitBehaviors.Count; i++)
            {
                exitBehaviors[i].Run();
            }
        }
    }
}
