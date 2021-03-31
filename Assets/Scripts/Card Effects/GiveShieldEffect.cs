using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveShieldEffect : CardEffect
{
    public int shield;
    public override void ActivateEffect()
    {
        EffectManager.Instance.GiveShield(target, shield);
    }
}
