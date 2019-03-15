using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item
{
    public float additionalDamage;
    public float attackSpeedMultiplier;
    
    public override void equip(GameObject g)
    {
        Debug.Log("equipped");
        g.GetComponent<Player>().damage += additionalDamage;
        g.GetComponent<Player>().attackSpeed /= attackSpeedMultiplier;
    }

    public override void unequip(GameObject g)
    {
        g.GetComponent<Player>().damage -= additionalDamage;
        g.GetComponent<Player>().attackSpeed *= attackSpeedMultiplier;
    }
}
