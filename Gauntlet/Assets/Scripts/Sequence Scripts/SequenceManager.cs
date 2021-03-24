using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public List<SequenceSlot> slots = new List<SequenceSlot>();

    public void ActivateCards()
    {
        foreach(SequenceSlot slot in slots)
        {
            if (slot.card != null)
                slot.card.GetComponent<BaseCard>().ActivateEffects();
        }
    }
}
