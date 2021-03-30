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
        Debug.Log("Enemy Action");
        //Activate action
        if (actionSeq.cards.Count != 0)
        {

            actionSeq.cards[0].GetComponent<BaseCard>().ActivateEffects();
            actionPile.Push(actionSeq.cards[0]);
            actionSeq.slots[0].card = null;

            if (actionSeq.cards[0].GetComponent<BaseCard>().cardType == cardType.Attack)
            {
                animator.SetTrigger("isAttacking");
            }
            actionSeq.cards.RemoveAt(0);
        }

        //Queue next turn's action
        actionSeq.NewCard(actionPile.Pop());

        Debug.Log("Action Pile Size: " + actionPile.cards.Count);
        if (actionPile.cards.Count > 0)
        {
            actionSeq.NewCard(reactionPile.Pop());
        }
        EnemyReaction();
    }
    public void EnemyReaction()
    {
        Debug.Log("Enemy Reaction");
        if (reactionPile.cards.Count != 0)
            reactionSeq.NewCard(reactionPile.Pop());
        //Check reaction condition
        if (reactionSeq.cards.Count != 0)
        {
            for(int i = 0; i < reactionSeq.cards.Count; i++)
            {
                reactionSeq.cards[i].GetComponent<BaseCard>().ActivateEffects();

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
