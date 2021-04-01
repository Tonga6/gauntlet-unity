using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMana : CardEffect
{
    public int mana;

    public override void ActivateEffect()
    {
        EffectManager.Instance.GainMana(target, mana);
    }
}
