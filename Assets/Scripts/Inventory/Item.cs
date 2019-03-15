using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public string type;
    public string description;

    public abstract void equip(GameObject g);

    public abstract void unequip(GameObject g);
}
