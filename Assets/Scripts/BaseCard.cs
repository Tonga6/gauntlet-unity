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

    public Canvas canvas;
    public bool isPlayed = false;

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
        this.transform.localScale *= GameManager.Instance.magScale;
       
        
        canvas.overrideSorting = true;
        if (!isPlayed){
            Vector2 temp = transform.position;
            temp.y += GameManager.Instance.adjustVar;
            transform.position = temp;
        }
    }
    #endregion

    #region OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        
        this.transform.localScale = Vector3.one;
        canvas.overrideSorting = false;
        if (!isPlayed)
        {
            Vector2 temp = transform.position;
            temp.y -= GameManager.Instance.adjustVar;
            transform.position = temp;
        }
    }
    #endregion

}
public enum cardType
{
    Attack,
    Defense
}

