using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Fighter
{
    public void Start()
    {
        base.attacks.Add(
            new Attack(AttackTypes.basic, 5, Mathf.Infinity, 1)
        );
        setLevel(base.stats.level);
    }

    public void setLevel(int level)
    {
        base.stats.level = level;
        switch (base.stats.level)
        {
            case 1:
                base.stats.setStats(20, 5, 1);
                break;
            case 2:
                base.stats.setStats(30, 10, 2);
                break;
            default:
                base.stats.setStats(35, 20, 3);
                break;
        }
        if (level >= 2 && base.attacks.Find(attack => attack.level == 2) == null)
            base.attacks.Add(
                AttackTypes.getHeavyAttack(this.name)
            );
        if (level == 3 && base.attacks.Find(attack => attack.level == 3) == null)
            base.attacks.Add(
                AttackTypes.getPowerfulAttack(this.name)
            );
    }
}