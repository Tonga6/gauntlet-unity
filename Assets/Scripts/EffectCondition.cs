using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectCondition : MonoBehaviour
{
    public SequenceManager sm;
    private void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("PlayerSequenceBoard").GetComponent<SequenceManager>();
    }
    public virtual bool checkCondition()
    {
        return false;
    }
}
