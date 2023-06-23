using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable] public class Stats
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float attack;
    [SerializeField] private float defense;
    [SerializeField] private int level;

    public StatsPanel statsPanel;

    public void setStats(float maxHealth, float attack, float defense, int level)
    {
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.level = level;
        currentHealth = maxHealth;
        statsPanel.HealthLabel.text = $"{Mathf.RoundToInt(currentHealth)} / {Mathf.RoundToInt(currentHealth)}";
        statsPanel.LevelLabel.text = $"Nv. {level}";
    }

    public void setHealth(float health)
    {
        string color = CombatGlobales.healthGreen;
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
        if (currentHealth == 0f)
        {
            color = CombatGlobales.healthNone;
            statsPanel.Image.GetComponent<Image>().color = ColorUtility.TryParseHtmlString("#FFFFFF32", out Color nc) ? nc : Color.black;
        }
        else if (currentHealth < maxHealth * 0.2f)
        {
            color = CombatGlobales.healthRed;
        }
        else if (currentHealth < maxHealth * 0.4f)
        {
            color = CombatGlobales.healthYellow;
        }
        statsPanel.Slider.GetComponent<Slider>().value = currentHealth / maxHealth;
        statsPanel.Slider.transform.GetChild(1).GetComponentInChildren<Image>().color = ColorUtility.TryParseHtmlString(color, out Color newColor) ? newColor : Color.black;
        statsPanel.HealthLabel.text = $"{currentHealth} / {maxHealth}";
    }

    public float getHealth()
    {
        return currentHealth;
    }
}
