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

    public void EnemyPhase()
    {
        //Check action condition
        if (actionSeq.cards.Count > 0)
        {
            actionSeq.ActivateCards();

            //If is attack card and was activation conditions were met
            if (actionSeq.cards[0].GetComponent<BaseCard>().cardType == cardType.Attack && actionSeq.cards[0].GetComponent<BaseCard>().isExhausted)
            {
                animator.SetTrigger("isAttacking");
            }
            actionPile.Push(actionSeq.cards[0]);
            actionSeq.slots[0].card = null;
            actionSeq.cards.RemoveAt(0);
        }
        
        //Queue next turn's action
        if (actionPile.cards.Count > 0)
        {
            actionPile.ShufflePile();
            actionSeq.NewCard(actionPile.Pop());
        }

        //Queue next Reaction first so only one reaction can occur per turn
        if (reactionPile.cards.Count > 0)
        {
            if (reactionSeq.cards.Count > 0)
            {
                reactionPile.Push(reactionSeq.cards[0]);
                reactionSeq.cards.RemoveAt(0);
                reactionSeq.slots[0].card = null;
            }
            reactionPile.ShufflePile();
            reactionSeq.NewCard(reactionPile.Pop());
        }

        EnemyReaction();
        //yield return new WaitForSeconds(2f);
    }
    public void EnemyReaction()
    {
        

        //Check reaction condition
        if (reactionSeq.cards.Count != 0)
        {
            for(int i = 0; i < reactionSeq.cards.Count; i++)
            {
                reactionSeq.ActivateCards();

                //If reaction conditions were met
                if (reactionSeq.cards[i].GetComponent<BaseCard>().isExhausted)
                {
                    if (reactionSeq.cards[0].GetComponent<BaseCard>().cardType == cardType.Attack)
                    {
                        animator.SetTrigger("isAttacking");
                    }
                    reactionPile.Push(reactionSeq.cards[i]);
                    reactionSeq.cards.RemoveAt(i);
                    reactionSeq.slots[i].card = null;
                }
            }
            
        }

        

    }
}
