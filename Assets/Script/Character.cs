using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public int attackPower;
    public int defensePower;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        int damageToTake = Mathf.Max(damage - defensePower, 0);
        currentHealth -= damageToTake;
        Debug.Log(characterName + " took " + damageToTake + " damage!");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log(characterName + " has been defeated!");
            // Handle character death
        }
    }

    public void Attack(Character target)
    {
        Debug.Log(characterName + " is attacking " + target.characterName);
        target.TakeDamage(attackPower);
    }
}


