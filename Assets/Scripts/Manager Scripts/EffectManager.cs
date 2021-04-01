using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public List<CharacterManager> targets;
    public CamShake camShake;
    private void Awake()
    {
        if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
        //camShake = Camera.main.GetComponent<CamShake>();
        
        camShake = GameObject.FindGameObjectWithTag("MasterCanvas").GetComponent<CamShake>();
        Instance = this;
        //use for simple targeting
        if (PlayerManager.Instance != null && EnemyManager.Instance != null)
        {
            Instance.targets.Add(PlayerManager.Instance);
            Instance.targets.Add(EnemyManager.Instance);
        }
    }

    public void DealDamage(character target, int damage)
    {
        camShake.Shake();
        if (target == character.ENEMY)
        {
            EnemyManager.Instance.TakeDamage(damage);
        }
        else
            PlayerManager.Instance.TakeDamage(damage);
    }
    public void GiveShield(character target, int shield)
    {
        if (target == character.ENEMY)
        {
            EnemyManager.Instance.GiveShield(shield);
        }
        else
            PlayerManager.Instance.GiveShield(shield);
    }
    public void DrawCard(character target, int draw)
    {
        if(target == character.PLAYER)
        {
            for (int i = 0; i < draw; i++)
            {
                PlayerManager.Instance.DrawCard();
            }
        }
    }
    public void GainMana (character target, int mana)
    {
        if (target == character.PLAYER)
        {
            Debug.Log("Gain mana: EM");
            PlayerManager.Instance.GainMana(mana);
        }
    }
    public void InflictStatus()
    {

    }
}

public enum character
{
    PLAYER,
    ENEMY
}
