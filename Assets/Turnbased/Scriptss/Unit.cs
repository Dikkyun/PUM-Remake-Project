using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int unitLevel;  // Add this property
    public int currentGauge;  // Add this property if not already present

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        return currentHP <= 0;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}


