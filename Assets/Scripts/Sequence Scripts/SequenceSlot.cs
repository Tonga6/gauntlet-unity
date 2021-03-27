using System.Collections;
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
                if (card.GetComponent<BaseCard>().isMoving) { 
                    if (PlayerManager.Instance.PlayCard(card))
                    {
                        data.pointerDrag.GetComponent<RectTransform>().parent = GetComponent<RectTransform>();
                        data.pointerDrag.GetComponent<BaseCard>().isPlayed = true;
                        data.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                        card = data.selectedObject;
                        sm.NewCard(card);
                        sm.ActivateCards();
                    }
                }
                
            }
            
        }   
    }
    #endregion
}
