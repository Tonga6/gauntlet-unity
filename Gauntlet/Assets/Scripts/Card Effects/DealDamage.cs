﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : CardEffect
{
    public int damage = 5;

    public override void ActivateEffect()
    {
        Debug.Log("Activate DealDamage effect. Deal: " + damage);
    }
}
