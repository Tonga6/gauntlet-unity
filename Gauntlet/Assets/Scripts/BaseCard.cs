using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int manaCost;
	public new string name;
	public string description;
	public bool isMoving;
	public Sprite artwork;
    public CanvasGroup cs;
    Vector2 startPos;

    public List<CardEffect> cardEffects = new List<CardEffect>();

    public void ActivateEffects()
    {
        foreach (CardEffect effect in cardEffects)
        {
            if (effect.CanActivate())
                effect.ActivateEffect();
        }
    }

    #region IBeginDragHandler implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = GetComponent<RectTransform>().localPosition;
        cs.blocksRaycasts = false;
    }
    #endregion

    #region IDragHandler implementation
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        //GetComponent<RectTransform>().localPosition = startPos;
        cs.blocksRaycasts = true;
    }
    #endregion

}

