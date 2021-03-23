using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInflicterCollision : MonoBehaviour
{
    [SerializeField] private StatusEffectSO[] statusEffects = new StatusEffectSO[0];
    [SerializeField] private StatSheet caster = null;
    [SerializeField] private LayerMask targetedLayers = new LayerMask();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Inflict(collision.collider);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Inflict(collider);
    }

    private void Inflict(Collider2D collider)
    {
        if (targetedLayers == (targetedLayers | (1 << collider.gameObject.layer)))
        {
            StatSheet targetUnit = collider.GetComponentInChildren<StatSheet>();
            if (targetUnit != null)
            {
                for (int i = 0; i < statusEffects.Length; i++)
                {
                    StatusEffect statusEffect = statusEffects[i].GetEffect(caster, targetUnit);
                    statusEffect.Apply(targetUnit);
                }
            }
        }
    }

}