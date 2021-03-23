using System.Collections.Generic;
using UnityEngine;


namespace DKH
{
    public interface ITargetUser
    {
        void SetTargets(List<GameObject> targets);
        void ClearTargets();
    }

    public interface ITargeter
    {
        GameObject[] GetTargets();
    }

}
