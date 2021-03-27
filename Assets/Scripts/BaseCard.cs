﻿using System.Collections;
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

    public Vector2 startPos;
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
    public void ResetCard()
    {
        foreach (CardEffect effect in cardEffects)
        {
            effect.hasActivated = false;
        }
        isPlayed = false;
        gameObject.SetActive(false);
    }

    public void ResetPos()
    {
        transform.localPosition = startPos;
    }

    #region IBeginDragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(GameManager.Instance.phase == turnPhase.PLAYER && !isPlayed)
        {
            isMoving = true;
            cs.blocksRaycasts = false;

            Vector2 temp = transform.position;
            temp.y -= GameManager.Instance.adjustVar;
            transform.position = temp;

            startPos = transform.localPosition;
        }
    }
    #endregion

    #region IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        if (isMoving && !isPlayed)
            transform.position = Input.mousePosition;
    }
    #endregion

    #region IEndDragHandler

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMoving && !isPlayed)
        {
            isMoving = false;
            cs.blocksRaycasts = true;
            if (!isPlayed)
                ResetPos();
        }
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

