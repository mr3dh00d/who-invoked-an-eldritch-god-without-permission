using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backslash))
        {
            base.stats.setHealth(0);
        }
    }

    public virtual void setLevel(int level)
    {

    }

    public int getLevel()
    {
        return base.stats.level;
    }

    public void setStats(int health, int defense, int level)
    {
        base.stats.setStats(health, defense, level);
    }

    public Stats getStats()
    {
        return base.stats;
    }

    public void setHealth(int health)
    {
        base.stats.setHealth(health);
    }

    public void addAttack(Attack attack)
    {
        base.attacks.Add(attack);
    }

    public List<Attack> getAttacks()
    {
        return base.attacks;
    }

    
}