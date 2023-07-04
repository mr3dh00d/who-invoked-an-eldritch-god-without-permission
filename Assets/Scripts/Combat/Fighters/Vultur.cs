using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vultur : Enemy
{
    public void Start()
    {
        base.addAttack(
            new Attack("Pluma cosmica", 10, Mathf.Infinity, 1)
        );
        base.addAttack(
            new Attack("Avalancha estelar", 12, 0, 2)
        );
        base.addAttack(
            new Attack("Colapso cósmico", 18, 0, 3)
        );
        setLevel(base.getLevel());
        
    }

    public override void setLevel(int level)
    {
        base.getStats().level = level;
        Attack heavy;
        Attack powerful;
        switch (level)
        {
            case 1:
                base.setStats(40, 5, 1);
                break;
            case 2:
                base.setStats(50, 5, 2);
                heavy = base.getAttacks().Find(a => a.level == 2);
                if(heavy != null)
                    heavy.maxUses = 3;
                break;
            default:
                base.setStats(60, 5, 3);
                heavy = base.getAttacks().Find(a => a.level == 2);
                if(heavy != null)
                    heavy.maxUses = 3;
                powerful = base.getAttacks().Find(a => a.level == 3);
                if(powerful != null)
                    powerful.maxUses = 2;
                break;
        }
            
    }
    
}