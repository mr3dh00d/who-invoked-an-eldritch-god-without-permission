using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string type;
    public Fighter attacker;
    public Attack attack;
    public Fighter target;
    public Item item;

    public Action(string type, Fighter attacker, Attack attack = null, Fighter target = null, Item item = null)
    {
        this.type = type;
        this.attacker = attacker;
        this.attack = attack;
        this.target = target;
        this.item = item;
    }
}