﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Doozy.Engine.UI;

public class SequenceSlot : MonoBehaviour, IDropHandler
{
    public SequenceManager sm;
    [SerializeField]
    public GameObject card;
    #region IBeginDropHandler implementation
    public void OnDrop(PointerEventData data)
    {
        if (data != null)
        {
            GameObject card = data.pointerDrag;
            if (sm.CompareTag("PlayerSequenceBoard"))
            {
                if (card.GetComponent<BaseCard>().isMoving && GameManager.Instance.phase == turnPhase.PLAYER)
                {
                    PlayerManager.Instance.PlayCard(card);
                }

            }

        }
    }
    #endregion
}
