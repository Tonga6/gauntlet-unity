using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEffect : CardEffect
{
    public int draw;

    public override void ActivateEffect()
    {
        EffectManager.Instance.DrawCard(target, draw);
    }
}
