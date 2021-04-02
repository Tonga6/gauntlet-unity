using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Details")]
    public int manaCost;
	public new string name;
	public string description;
    public cardType cardType;
    public int damage;
    public int shield;
    public int draw;
	public bool isMoving;
    public character owner;
    public bool isExhausted;

    [Header("Card Visuals")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descripText;
    public TextMeshProUGUI manaText;
    public Sprite artwork;
    public CanvasGroup cs;
    public int nameSize;
    public int descripSize;
    public Canvas canvas;
    public bool isPlayed = false;

    public List<CardEffect> cardEffects = new List<CardEffect>();
    float yPos;
    public Vector3 startPos;
    bool isMag = false;
    public Sequence seq;
    
    private void Awake()
    {
        if(nameText != null)
        {
            nameText.text = name;
            nameText.fontSize = nameSize;
        }
        description = description.Replace("X", damage.ToString());
        description = description.Replace("Y", shield.ToString());
        description = description.Replace("Z", draw.ToString());

        descripText.text = description;
        descripText.fontSize = descripSize;

        manaText.text = manaCost.ToString();
    }

    public void ActivateEffects()
    {
        seq = DOTween.Sequence();
        int activated = 0;
        foreach (CardEffect effect in cardEffects)
        {
            if (effect.CanActivate())
            {
                effect.hasActivated = true;
                effect.ActivateEffect();
                activated++;
            }
        }
        if (activated == cardEffects.Count)
            isExhausted = true;
    }
    public void MoveTo(RectTransform parent)
    {
        canvas.overrideSorting = true;
        RectTransform rt = GetComponent<RectTransform>();
        seq.Append(transform.DOMove(parent.position, 1));
        rt.parent = parent;
        rt.localScale = GameManager.Instance.cardScale;
        startPos = rt.parent.position;
    }
    public void ResetCard()
    {
        foreach (CardEffect effect in cardEffects)
        {
            effect.hasActivated = false;
        }
        isPlayed = false;
        isExhausted = false;
        gameObject.SetActive(false);
    }

    public void ResetPos()
    {
        Debug.Log("Reset to " + startPos);
        transform.DOMove(startPos, 0.3f);
    }

    #region IBeginDragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        //If is player turn and card is in hand
        if (GameManager.Instance.phase == turnPhase.PLAYER && !isPlayed)
        {
            cs.blocksRaycasts = false;
            this.transform.localScale = GameManager.Instance.cardScale;
            GameManager.Instance.canMag = false;
            isMoving = true;
            canvas.overrideSorting = true;

        }
    }
    #endregion

    #region IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        if (isMoving && GameManager.Instance.phase == turnPhase.PLAYER)
        {
            if (!canvas.overrideSorting)
                canvas.overrideSorting = true;
            transform.position = Input.mousePosition;
        }
    }
    #endregion

    #region IEndDragHandler

    public void OnEndDrag(PointerEventData eventData)
    {
        
        canvas.overrideSorting = false;
        if (GameManager.Instance.phase == turnPhase.PLAYER && !isPlayed)
        {
                isMoving = false;
                cs.blocksRaycasts = true;
                if (!isPlayed)
                    ResetPos();
            GameManager.Instance.canMag = true;
        }
    }
    #endregion

    #region OnPointerEnter
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.position == startPos && GameManager.Instance.canMag)
        {

            yPos = transform.position.y;

            if (owner == character.PLAYER)
            {

                if (GameManager.Instance.phase == turnPhase.PLAYER)
                {
                    if (!isPlayed)
                    {
                        Vector2 temp = transform.position;
                        temp.y = yPos + GameManager.Instance.adjustVar;
                        transform.DOMove(temp, 0.3f);
                    }
                    //seq.Append(transform.DOScale(GameManager.Instance.magScale, 0.4f));
                    //this.transform.localScale = GameManager.Instance.magScale;

                    canvas.overrideSorting = true;
                }
            }
        }
    }
    #endregion

    #region OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        if(owner == character.PLAYER && !isMoving)
        {
                if (!isPlayed && transform.position != startPos)
                {
                    transform.DOMove(startPos, 0.3f);
                }
                canvas.overrideSorting = false;
            //seq.Pause();
            //    seq.Append(transform.DOScale(GameManager.Instance.cardScale, 0.1f));
            
        }
    }
    #endregion

}
public enum cardType
{
    Attack,
    Defense,
    Utility
}

