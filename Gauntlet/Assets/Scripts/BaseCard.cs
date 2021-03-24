using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    public int manaCost;
	public new string name;
	public string description;
	public bool isMoving;
	public Sprite artwork;

	//public CardData cardData;


    public List<CardEffect> cardEffects = new List<CardEffect>();

    public void ActivateEffects()
    {
        foreach (CardEffect effect in cardEffects)
        {
            if (effect.CanActivate())
                effect.ActivateEffect();
        }
    }

    private void Update()
    {
		if (isMoving)
			Move();
    }
	public void SetMoving(bool isMouseDown)
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		    isMoving = isMouseDown;
    }
    public void Move()
    {
		this.transform.position = Input.mousePosition;
    }
}

