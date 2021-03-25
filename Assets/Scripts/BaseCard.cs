using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Details")]
    public int manaCost;
	public new string name;
	public string description;
    public cardType cardType;
	public bool isMoving;

    [Header("Card Visuals")]
    public Sprite artwork;
    public CanvasGroup cs;

    public int magnify;
    public int adjust;
    public Canvas canvas;

    public List<CardEffect> cardEffects = new List<CardEffect>();

    public void ActivateEffects()
    {
        foreach (CardEffect effect in cardEffects)
        {
            if (effect.CanActivate())
            {
                effect.hasActivated = true;
                effect.ActivateEffect();
            }
        }
    }

    #region IBeginDragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        cs.blocksRaycasts = false;
    }
    #endregion

    #region IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    #endregion

    #region IEndDragHandler

    public void OnEndDrag(PointerEventData eventData)
    {
        //GetComponent<RectTransform>().localPosition = startPos;
        cs.blocksRaycasts = true;
    }
    #endregion

    #region OnPointerEnter
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale *= magnify;
        Vector2 temp = transform.position;
        temp.y += adjust;
        transform.position = temp;
        //canvas.overrideSorting = true;
    }
    #endregion

    #region OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exit");
        Vector2 temp = transform.position;
        temp.y -= adjust;
        transform.position = temp;
        this.transform.localScale = Vector3.one;
    }
    #endregion

}
public enum cardType
{
    Attack,
    Defense
}

