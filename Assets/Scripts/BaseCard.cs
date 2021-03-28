using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public TextMeshProUGUI nameText;
    public Sprite artwork;
    public CanvasGroup cs;

    public Canvas canvas;
    public bool isPlayed = false;

    public List<CardEffect> cardEffects = new List<CardEffect>();

    public Vector2 startPos;
    
    private void Awake()
    {
        nameText.text = name;
        nameText.fontSize = 8;
    }
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
            this.transform.localScale = GameManager.Instance.cardScale;
            GameManager.Instance.canMag = false;
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
        GameManager.Instance.canMag = true;
        if (GameManager.Instance.phase == turnPhase.PLAYER && !isPlayed)
        {
            if (isMoving)
            {
                isMoving = false;
                cs.blocksRaycasts = true;
                if (!isPlayed)
                    ResetPos();
            }
        }
    }
    #endregion

    #region OnPointerEnter
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.Instance.canMag)
        {
            this.transform.localScale = GameManager.Instance.magScale;

            canvas.overrideSorting = true;
            if (!isPlayed){
                Vector2 temp = transform.position;
                temp.y += GameManager.Instance.adjustVar;
                transform.position = temp;
            }
        }
    }
    #endregion

    #region OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        if(GameManager.Instance.canMag)
        {

            this.transform.localScale = GameManager.Instance.cardScale;
            canvas.overrideSorting = false;
            if (!isPlayed)
            {
                Vector2 temp = transform.position;
                temp.y -= GameManager.Instance.adjustVar;
                transform.position = temp;
            }
        
        }
    }
    #endregion

}
public enum cardType
{
    Attack,
    Defense
}

