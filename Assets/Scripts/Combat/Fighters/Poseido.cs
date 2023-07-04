using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poseido : Enemy
{
    public void Start()
    {
        base.addAttack(
            new Attack("Picotazo", 7, Mathf.Infinity, 1)
        );
        base.addAttack(
            new Attack("Placaje", 10, 0, 2)
        );
        base.addAttack(
            new Attack("Brecha Oscura", 12, 0, 3)
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
                base.setStats(25, 5, 1);
                break;
            case 2:
                base.setStats(30, 5, 2);
                heavy = base.getAttacks().Find(a => a.level == 2);
                if(heavy != null)
                    heavy.maxUses = 3;
                break;
            default:
                base.stats.setStats(35, 5, 3);
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