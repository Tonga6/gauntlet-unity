using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    #region IBeginDropHandler implementation
    SequenceManager sm;

    private void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("PlayerSequenceBoard").GetComponent<SequenceManager>();
    }
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
