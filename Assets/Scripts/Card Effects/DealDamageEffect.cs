using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : CardEffect
{
    public int damage;

    public override void ActivateEffect()
    {
        EffectManager.Instance.DealDamage(target, damage);
    }
}
