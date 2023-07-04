using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Stats stats;
    public List<Attack> attacks;
    private bool isDefending = false;

    // public float getAttack()
    // {
    //     return 0f;
    // }

    public float takeDamage(float damage)
    {
        if (isDefending)
        {
            damage = Mathf.Round(damage - stats.getDefense());
            if (damage < 0f)
            {
                damage = 0f;
            }
        }
        stats.setHealth(stats.getHealth() - damage);
        return damage;
    }

    public void setIsDefending(bool isDefending)
    {
        this.isDefending = isDefending;
    }

    public bool getIsDefending()
    {
        return isDefending;
    }

}