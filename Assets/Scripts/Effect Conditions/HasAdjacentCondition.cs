using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasAdjacentCondition : EffectCondition
{
    public int adjacency;
    public List<cardType> typeCombo = new List<cardType>();
    public override bool checkCondition()
    {
        Debug.Log("Checking HasAdjacentCondition condition");
        return sm.AdjacencyCheck(this.gameObject, typeCombo, adjacency);
    }
}
