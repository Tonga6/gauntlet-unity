using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    public static EnemyManager Instance { get; private set; }

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

    }
}
