using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable] public class Stats
{
    private float currentHealth;
    private float maxHealth;
    private float defense;
    [SerializeField] public int level;
    public StatsPanel statsPanel;
    public void setStats(float maxHealth, float defense, int level)
    {
        this.maxHealth = maxHealth;
        this.defense = defense;
        this.level = level;
        currentHealth = maxHealth;
        if (statsPanel.getIsUIset())
        {
            statsPanel.setHealthUI(currentHealth, maxHealth);
            statsPanel.setLevelUI(level);
        }
    }
    public void setHealth(float health)
    {
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
        if (statsPanel.getIsUIset()) statsPanel.setHealthUI(currentHealth, maxHealth);
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
    public float getHealth()
    {
        return currentHealth;
    }
    public float getDefense()
    {
        return defense;
    }
}
