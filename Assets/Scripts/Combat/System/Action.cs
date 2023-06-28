using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string type;
    public Fighter attacker;
    public Fighter target;
    public Item item;

    public Action(string type, Fighter attacker, Fighter target = null, Item item = null)
    {
        this.type = type;
        this.attacker = attacker;
        this.target = target;
        this.item = item;
    }
}