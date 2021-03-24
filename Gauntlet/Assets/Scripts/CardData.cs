using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public int manaCost;

    public List<CardEffect> cardEffects = new List<CardEffect>();

    public void ActivateEffects()
    {
        foreach(CardEffect effect in cardEffects){
            effect.CanActivate();
        }
    }
}
