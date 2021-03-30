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
    public int damage;
    public int shield;
	public bool isMoving;
    public targetCharacter owner;
    public bool isExhausted;

    [Header("Card Visuals")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descripText;
    public Sprite artwork;
    public CanvasGroup cs;
    public int nameSize;
    public int descripSize;
    public Canvas canvas;
    public bool isPlayed = false;

    public List<CardEffect> cardEffects = new List<CardEffect>();

    public Vector2 startPos;
    bool isMag = false;
    
    private void Awake()
    {
        if(nameText != null)
        {
            nameText.text = name;
            nameText.fontSize = nameSize;
        }
        if(owner == targetCharacter.ENEMY)
            Debug.Log(description.Contains("X"));
        description = description.Replace("X", damage.ToString());
        description = description.Replace("Y", shield.ToString());

        descripText.text = description;
        descripText.fontSize = descripSize;
    }
    //private void Update()
    //{
    //    if ()
    //}
    public void ActivateEffects()
    {
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
        transform.position = startPos;
    }

    #region IBeginDragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        //If is player turn and card is in hand
        if (GameManager.Instance.phase == turnPhase.PLAYER && !isPlayed)
        {
            startPos = this.transform.position;
            startPos.y -= GameManager.Instance.adjustVar;
            this.transform.localScale = GameManager.Instance.cardScale;
            GameManager.Instance.canMag = false;
            isMoving = true;
            cs.blocksRaycasts = false;
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
            if (isMoving)
            {

                isMoving = false;
                cs.blocksRaycasts = true;
                if (!isPlayed)
                    ResetPos();
            }
        }
        GameManager.Instance.canMag = true;
    }
    #endregion

    #region OnPointerEnter
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(owner == targetCharacter.PLAYER)
        {

            if (GameManager.Instance.canMag && GameManager.Instance.phase == turnPhase.PLAYER)
            {
                isMag = true;
                this.transform.localScale = GameManager.Instance.magScale;

                canvas.overrideSorting = true;
                if (!isPlayed){
                    Vector2 temp = transform.position;
                    temp.y += GameManager.Instance.adjustVar;
                    transform.position = temp;
                }
            }
        }
    }
    #endregion

    #region OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        if(owner == targetCharacter.PLAYER && isMag)
        {
            
            if (isMag)
            {
                isMag = false;
                canvas.overrideSorting = false;
                this.transform.localScale = GameManager.Instance.cardScale;
                if (!isPlayed)
                {
                    Vector2 temp = transform.position;
                    temp.y -= GameManager.Instance.adjustVar;
                    transform.position = temp;
                }
        
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

