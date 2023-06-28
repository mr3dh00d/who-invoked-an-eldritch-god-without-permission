using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable] public class Stats
{
    private float currentHealth;
    private float maxHealth;
    private float attack;
    private float defense;
    private int level;
    public StatsPanel statsPanel;

    public void setStats(float maxHealth, float attack, float defense, int level)
    {
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.level = level;
        currentHealth = maxHealth;
        statsPanel.setLevelUI(level);
        statsPanel.setHealthUI(currentHealth, maxHealth);
    }

    public void setHealth(float health)
    {
        string color = ColorTypes.healthGreen;
        if (health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (health < 0f)
        {
            currentHealth = 0f;
        }
        else
        {
            currentHealth = health;
        }
        statsPanel.setHealthUI(currentHealth, maxHealth);
    }

    public float getHealth()
    {
        return currentHealth;
    }

    public float getAttack()
    {
        return attack;
    }

    public float getDefense()
    {
        return defense;
    }
}
