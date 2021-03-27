using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public bool hasActivated;

    public targetCharacter target;

    public EffectCondition condition;
    private void Awake()
    {
        hasActivated = false;
    }
    public virtual bool CanActivate()
    {
        if (condition == null)
            if (!hasActivated)
                return true;

        if (!hasActivated && condition.checkCondition())
        {
            return true;
        }

        return false;
    }
    public virtual void ActivateEffect()
    {

    }
}
