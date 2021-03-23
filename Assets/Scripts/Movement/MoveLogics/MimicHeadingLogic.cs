using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class MimicHeadingLogic : MonoBehaviour, IHeadingLogic, ISourceUser
    {
        public Vector3 Heading { get { return headingLogic.Heading; } }

        private IHeadingLogic headingLogic;

        public IdSO myHeadingLogicID;
        public IdSO headingLogicIDToMimic;
        public IdSO GetID()
        {
            return myHeadingLogicID;
        }

        public void SetSource(GameObject source)
        {
            IHeadingLogic[] headingLogics = IdSO.FindComponentsWithID<IHeadingLogic>(source, headingLogicIDToMimic);
            headingLogic = headingLogics[0];
            //int selectedIndex = 0;
            //IHeadingLogic[] headingLogics = source.GetComponentsInChildren<IHeadingLogic>();
            //if(headingLogics != null)
            //{
            //    for (int i = 0; i < headingLogics.Length; i++)
            //    {
            //        if (headingLogics[i].GetID() == headingLogicIDToMimic)
            //        {
            //            selectedIndex = i;
            //            break;
            //        }
            //    }
            //}
            //if (headingLogics == null)
            //{
            //    Debug.LogError("Cannot issue movement command to object without a move logic class.");
            //}
            //headingLogic = headingLogics[selectedIndex];
        }

        public void SetHeading(Vector3 direction, bool worldSpace = true) {}
        public void SetYHeading(float direction){}
        public void SetXHeading(float direction){}
        public void SetZHeading(float direction){}
    }
}

