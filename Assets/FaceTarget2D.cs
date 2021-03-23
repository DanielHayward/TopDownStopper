using UnityEngine;

namespace DKH
{
    public class FaceTarget2D : MonoBehaviour, ISourceUser
    {
        GameObject source;
        [Tooltip("ID not required")]
        public IdSO headingIdToSet = null;
        private IHeadingLogic[] headingLogicToSet;
        [SerializeField] private TargeterController2D targeter;

        public void SetSource(GameObject source)
        {
            this.source = source;
            headingLogicToSet = IdSO.FindComponents<IHeadingLogic>(source, headingIdToSet);
            targeter.OnTriggered += Face;
        }

        private void Face(object sender, ConditionResultsEventArgs e)
        {
            if (e.value)
            {
                for (int headingIndex = 0; headingIndex < headingLogicToSet.Length; headingIndex++)
                {
                    headingLogicToSet[headingIndex].SetHeading(targeter.lastResults[0].transform.position - source.transform.position);
                }
            }
        }
    }
}

