using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterManager : MonoBehaviour
{
    [Header("Character Text Displays")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;


    [Header("Character Attributes")]
    public int maxHealth;
    public int health;
    public int maxShield; 
    public int shield;

    [Header("Hand Attributes")]
    public int maxHandSize;
    public int handSize;

    [Header("External References")]
    public SequenceManager sm;

    public void Initialise()
    {
        healthText.text = health.ToString();
        shieldText.text = shield.ToString();
    }

    public void TakeDamage (int damage)
    {
        Debug.Log("Dealing " + damage + " damage");
        if (damage > shield)
        {
            health -= damage - shield;
            shield = 0;
        }
        else
            shield -= damage;
        healthText.text = health.ToString();
        shieldText.text = shield.ToString();
    }
    public void GiveShield(int bonus)
    {

        this.shield += bonus;
        shieldText.text = shield.ToString();

    }
}
