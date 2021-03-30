using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : CardEffect
{
    int damage = 7;

    public override void ActivateEffect()
    {
        Debug.Log("DealDamageEffect: " + this.gameObject.name);
        EffectManager.Instance.DealDamage(target, damage);
    }
}
