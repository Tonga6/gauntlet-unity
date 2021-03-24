using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectCondition : MonoBehaviour
{
   
    public virtual bool checkCondition()
    {
        return false;
    }
}
