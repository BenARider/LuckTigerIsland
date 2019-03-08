using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    [SerializeField]
    public InventoryObject Item;
    [SerializeField]
    public int Price;
}