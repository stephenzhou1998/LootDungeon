using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public float additionalHealth;

    public override void equip(GameObject g)
    {
        g.GetComponent<Player>().health += additionalHealth;
        g.GetComponent<Player>().maxhealth += additionalHealth;
    }

    public override void unequip(GameObject g)
    {
        g.GetComponent<Player>().maxhealth -= additionalHealth;
        if (g.GetComponent<Player>().health > g.GetComponent<Player>().maxhealth)
        {
            g.GetComponent<Player>().health = g.GetComponent<Player>().maxhealth;
        }
    }
}
