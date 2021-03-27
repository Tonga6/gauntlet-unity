﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : CardEffect
{
    public int damage = 5;

    public override void ActivateEffect()
    {
        Debug.Log("Activated Effect: " + this.gameObject.name);
        EffectManager.Instance.DealDamage(target, damage);
    }
}
