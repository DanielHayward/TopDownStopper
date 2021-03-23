using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.Utils
{
    //This is useless if I can't change it to any enum bitmask.
    public class Flags
    {
        public bool SharesFlag(CollisionFlags LookingFor, CollisionFlags LookingIn)
        {
            return (LookingFor & LookingIn) != CollisionFlags.None;
        }
        public bool HasOnlyFlagsIn(CollisionFlags LookingFor, CollisionFlags LookingIn)
        {
            return (LookingFor & LookingIn) == LookingIn;
        }
        public bool HasAllFlagsIn(CollisionFlags LookingFor, CollisionFlags LookingIn)
        {
            return (LookingFor & LookingIn) == LookingFor;
        }
        public bool ExactMatchesFlags(CollisionFlags LookingFor, CollisionFlags LookingIn)
        {
            return LookingFor == LookingIn;
        }
        public void SetFlag(CollisionFlags LookingFor, ref CollisionFlags LookingIn)
        {
            LookingIn = LookingIn | LookingFor;
        }
        public void UnsetFlag(CollisionFlags LookingFor, ref CollisionFlags LookingIn)
        {
            LookingIn = LookingIn & (~LookingFor);
        }
    }
}

