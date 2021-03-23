using System;

//Tells all Unit Behaviors on this unit that it is the object they act on.
namespace DKH
{
    public class GameplayUnitSource : BehaviorSource
    {
        private void Awake()
        {
            AssignToBehaviors();
        }
        public override void AssignToBehaviors()
        {
            ISourceUser[] unitBehaviors = GetComponentsInChildren<ISourceUser>();
            foreach(ISourceUser unitBehavior in unitBehaviors)
            {
                unitBehavior.SetSource(gameObject);
            }
        }
    }
}
