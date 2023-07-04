using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluckNThulu : Enemy
{
    public void Start()
    {
        base.addAttack(
            new Attack("Garra de la Abominación", 20, Mathf.Infinity, 1)
        );
        base.addAttack(
            new Attack("Rugido Cósmico", 25, 3, 2)
        );
        base.addAttack(
            new Attack("Tormenta Primigenia", 30, 2, 3)
        );
        setLevel(base.stats.level);
        
    }

    public override void setLevel(int level)
    {
        base.getStats().level = level;
        base.setStats(75, 5, 1);       
    }
    
}