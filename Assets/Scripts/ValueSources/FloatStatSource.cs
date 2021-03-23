using UnityEngine;

namespace DKH
{
    public class FloatStatSource : IFloatSource, ISourceUser
    {
        [SerializeField] private StatTypeSO speedStatData;
        private StatSheet statSheet;
        private Stat speedStat;
        private float currentSpeed = 0;

        public void SetSource(GameObject source)
        {
            statSheet = source.GetComponentInChildren<StatSheet>();
        }
        public void Start()
        {
            if (statSheet != null)
            {
                speedStat = statSheet.GetStat(speedStatData);
                if (speedStat != null)
                {
                    currentSpeed = speedStat.GetValue();
                }
            }
        }
        public override float GetValue()
        {
            return currentSpeed;
        }
    }
}
