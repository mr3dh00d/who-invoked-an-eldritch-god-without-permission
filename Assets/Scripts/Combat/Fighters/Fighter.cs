using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Stats stats;
    public Attack[] attacks;

    private bool isDefending = false;

    public void Start() {
        stats.setStats(25, 5, 3, 1);
    }

    public void Update() {
        if(Input.GetKeyDown("space")){
            stats.setHealth(stats.getHealth() - Random.Range(0, 5));
        }
    }

    public float getAttack()
    {
        return stats.getAttack();
    }

    public void takeDamage(float damage)
    {
        if (isDefending)
        {
            damage = damage / 2;
        }
        stats.setHealth(stats.getHealth() - damage);
    }

    public void setIsDefending(bool isDefending)
    {
        this.isDefending = isDefending;
    }

}