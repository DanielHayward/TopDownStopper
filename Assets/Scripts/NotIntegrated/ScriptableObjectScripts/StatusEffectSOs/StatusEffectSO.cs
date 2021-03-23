using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Effects/StatusEffect")]
public class StatusEffectSO : ScriptableObject
{
    public int tickCount;
    public string myName;
    public Sprite uiSprite;
    public float tickDuration;

    public virtual StatusEffect GetEffect(StatSheet user, StatSheet target) { return new StatusEffect(this); }
}
