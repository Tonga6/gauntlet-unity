using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    public static EnemyManager Instance { get; private set; }

    [Header("Enemy Card Systems")]
    public CardPile actionPile;
    public CardPile reactionPile;

    public SequenceManager actionSeq;
    public SequenceManager reactionSeq;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialise();
        }
        else
            Destroy(this);
    }

    public void EnemyAction()
    {
        //Queue next turn's action
        if (actionPile.cards.Count > 0)
        {
            actionSeq.NewCard(actionPile.Pop());
        }
        //Check action condition
        if (actionSeq.cards.Count > 0)
        {
            actionSeq.ActivateCards();

            //If is attack card and was activation conditions were met
            if (actionSeq.cards[0].GetComponent<BaseCard>().cardType == cardType.Attack && actionSeq.cards[0].GetComponent<BaseCard>().isExhausted)
            {
                Debug.Log("Perform Action");
                animator.SetTrigger("isAttacking");
            }
            actionPile.Push(actionSeq.cards[0]);
            actionSeq.slots[0].card = null;
            actionSeq.cards.RemoveAt(0);
        }
        

        EnemyReaction();
    }
    public void EnemyReaction()
    {
        if (reactionPile.cards.Count != 0)
            reactionSeq.NewCard(reactionPile.Pop());
        //Check reaction condition
        if (reactionSeq.cards.Count != 0)
        {
            for(int i = 0; i < reactionSeq.cards.Count; i++)
            {
                reactionSeq.ActivateCards();

                //If reaction conditions were met
                if (reactionSeq.cards[i].GetComponent<BaseCard>().isExhausted)
                {

                    reactionPile.Push(reactionSeq.cards[i]);
                    reactionSeq.cards.RemoveAt(i);
                    reactionSeq.slots[i].card = null;
                    //Queue next turn's action
                    reactionSeq.NewCard(reactionPile.Pop());
                    if (reactionSeq.cards[0].GetComponent<BaseCard>().cardType == cardType.Attack)
                    {
                        animator.SetTrigger("isAttacking");
                    }
                }

            }
            
        }


        
    }
}
