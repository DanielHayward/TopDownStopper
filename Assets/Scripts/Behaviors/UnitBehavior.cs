using System.Collections.Generic;
using UnityEngine;


namespace DKH
{
    public interface IRunnable
    {
        void Run();
    }
    public interface IInputRunnable
    {
        void Run(int currentStage, float duration, Vector2 vector2);
    }
    public interface ISourceUser
    {
        void SetSource(GameObject source);
    }

    public abstract class UnitBehavior : MonoBehaviour, ISourceUser, IRunnable, IInputRunnable
    {
        protected GameObject source;

        public virtual void SetSource(GameObject actor)
        {
            if(source == null)
            {
                this.source = actor;
                CacheSource(actor);
            }
        }
        public GameObject GetSource()
        {
            return source;
        }
        public abstract void Run();
        public virtual void Run(int currentStage, float duration, Vector2 vector2)
        {
            Run();
        }
        public abstract void CacheSource(GameObject source);
    }
}