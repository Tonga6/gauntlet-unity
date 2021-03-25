using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    CardPile playerDrawPile;
    CardPile playerDiscardPile;
    
    turnPhase phase;

}

public enum turnPhase{
    START,
    ENEMY,
    PLAYER,
    END
}