using UnityEngine;

namespace DKH
{
    //[CreateAssetMenu(menuName = "ScriptableObjects/Commands/Despawn")]
    //public class DespawningCommandData : CommandDataSO
    //{
    //    public float DespawnDelay;

    //    public override CommandFactory GetFactory(GameObject source)
    //    {
    //        if (source == null)
    //        {
    //            return null;
    //        }
    //        return new DespawningFactory(source, this);
    //    }
    //}

    //public class DespawningFactory : CommandFactory
    //{
    //    private GameObject source;
    //    private DespawningCommandData data;

    //    public DespawningFactory(GameObject source, DespawningCommandData data)
    //    {
    //        this.source = source;
    //        this.data = data;
    //    }

    //    public override Command CreateCommand(GameObject target, Vector3 location, Vector3 passValue, int stage = 0, float duration = 0)
    //    {
    //        Spawnable spawnable = source.GetComponentInChildren<Spawnable>();
    //        if(spawnable == null)
    //        {
    //            Debug.Log("No spawnable monobehavior on object to despawn.");
    //        }    
    //        if ((data.firePeroids & (TriggerPeroids)(1 << stage)) != TriggerPeroids.None)
    //        {
    //            return new DespawningCommand(target, spawnable, data);
    //        }
    //        return null;
    //    }
    //}

    //public class DespawningCommand : Command
    //{
    //    GameObject source;
    //    Spawnable spawnable;
    //    private DespawningCommandData data;

    //    public DespawningCommand(GameObject source, Spawnable spawnable, DespawningCommandData data)
    //    {
    //        this.source = source;
    //        this.spawnable = spawnable;
    //        baseData = this.data = data;
    //    }

    //    public override void Execute()
    //    {
    //        spawnable.Off();
    //    }

    //    public override GameObject GetSource()
    //    {
    //        return source;
    //    }

    //    public override void Rejected()
    //    {

    //    }

    //    public override void Undo()
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}