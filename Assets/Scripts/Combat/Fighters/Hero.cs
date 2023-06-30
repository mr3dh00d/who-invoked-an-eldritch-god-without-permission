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
        base.attacks.Add(
            AttackTypes.getHeavyAttack(this.name)
        );
        base.attacks.Add(
            AttackTypes.getPowerfulAttack(this.name)
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
                base.attacks.Find(attack => attack.level == 2).maxUses = 1;
                base.stats.setStats(25, 7, 2);
                break;
            case 3:
                base.attacks.Find(attack => attack.level == 2).maxUses = 2;
                base.stats.setStats(30, 9, 3);
                break;
            case 4:
                base.attacks.Find(attack => attack.level == 2).maxUses = 2;
                base.attacks.Find(attack => attack.level == 3).maxUses = 1;
                base.stats.setStats(35, 11, 4);
                break;
            case 5:
                base.attacks.Find(attack => attack.level == 2).maxUses = 3;
                base.attacks.Find(attack => attack.level == 3).maxUses = 1;
                base.stats.setStats(40, 13, 5);
                break;
            default:
                base.attacks.Find(attack => attack.level == 2).maxUses = 3;
                base.attacks.Find(attack => attack.level == 3).maxUses = 2;
                base.stats.setStats(45, 15, 6);
                break;
        }
            
    }
}