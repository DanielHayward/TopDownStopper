using UnityEngine;

namespace DKH
{
    public class FaceTarget : MonoBehaviour, ISourceUser
    {
        GameObject source;
           [Tooltip("ID not required")]
        public IdSO headingIdToSet = null;
        private IHeadingLogic[] headingLogicToSet;
        [SerializeField] private TargeterController targeter;

        public void SetSource(GameObject source)
        {
            this.source = source;
            headingLogicToSet = IdSO.FindComponents<IHeadingLogic>(source, headingIdToSet);
            targeter.OnTriggered += Face;
        }

        private void Face(object sender, ConditionResultsEventArgs e)
        {
            if(e.value)
            {
                int closestIndex = 0;
                float closestDistance = float.MaxValue;
                for (int resultIndex = 0; resultIndex < targeter.lastResults.Length; resultIndex++)
                {
                    float distance = (targeter.lastResults[resultIndex].transform.position - source.transform.position).magnitude;
                    if(distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestIndex = resultIndex;
                    }
                }
                for (int headingIndex = 0; headingIndex < headingLogicToSet.Length; headingIndex++)
                {
                    headingLogicToSet[headingIndex].SetHeading(targeter.lastResults[closestIndex].transform.position - source.transform.position);
                }
            }
        }

        public IdSO GetID()
        {
            return headingIdToSet;
        }

    }
}

