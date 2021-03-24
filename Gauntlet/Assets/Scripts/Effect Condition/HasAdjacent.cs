using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasAdjacent : EffectCondition
{
    public override bool checkCondition()
    {
        Debug.Log("Checking HasAdjacent condition");
        return true;
    }
}
