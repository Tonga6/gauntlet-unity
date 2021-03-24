using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public string effectText;
    public bool hasActivated;

    public EffectCondition condition;
    private void Awake()
    {
        hasActivated = false;
    }
    public virtual bool CanActivate()
    {
        Debug.Log("Check CanActivate");
        if (condition == null)
            if (!hasActivated)
                return true;

        if (!hasActivated && condition.checkCondition())
        {
            return true;
        }
        Debug.Log("CanActivate returned false");

        return false;
    }
    public virtual void ActivateEffect()
    {

    }
}
