using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Attack
{
    public string name;
    public string phrase;
    public float damage;
    public int level;
    public float uses;
    public float maxUses;

    public Attack(string name, float damage, float maxUses, int level, string phrase = null)
    {
        this.name = name;
        this.phrase = phrase;
        this.damage = damage;
        this.level = level;
        this.maxUses = maxUses;
        this.uses = maxUses;
    }

    public Attack Clone()
    {
        return new Attack(name, damage, maxUses, level, phrase);
    }

}