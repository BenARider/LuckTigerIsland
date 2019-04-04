using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EObjectType
{
    Weapon,
    Armour,
    Consumable,
    Other
}

public class InventoryObject : ScriptableObject {

    public string objectName = "New Object";
    public Sprite Image;
    public string Description;
    public int Price;
    public bool sellable = true;
    public string Equippable;//Yes or No
    public string ItemType;
    public bool isEquipped = false;
    public EObjectType objectType;
}

