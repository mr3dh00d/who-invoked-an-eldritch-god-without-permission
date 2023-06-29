using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public void Start()
    {
        base.attacks.Add(
            new Attack(AttackTypes.basic, 7, Mathf.Infinity, 1)
        );
        setLevel(base.stats.level);
        
    }

    public void setLevel(int level)
    {
        base.stats.level = level;
        switch (level)
        {
            case 1:
                base.stats.setStats(25, 5, 1);
                break;
            case 2:
                base.stats.setStats(50, 5, 2);
                break;
            default:
                base.stats.setStats(75, 5, 3);
                break;
        }

        if(level >= 2)
            base.attacks.Add(
                new Attack("Oscuridad", 10, 3, 2)
            );
        if(level >= 3)
            base.attacks.Add(
                new Attack("Intensa Oscuridad", 20, 3, 3)
            );
    }
    
}