using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Condition", menuName = "New Condition")]
public class ConditionsData : ScriptableObject
{
    public EffectConditions condition;
    public string conditionName;
    public float conditionDescrip;
}
