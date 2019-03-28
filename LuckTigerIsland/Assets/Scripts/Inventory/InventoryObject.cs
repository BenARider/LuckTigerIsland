using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryObject : ScriptableObject {

    public string objectName = "New Object";
    public Sprite Image;
    public string Description;
    public int Price;
    public string Equippable;//Yes or No
    public string ItemType;
}

