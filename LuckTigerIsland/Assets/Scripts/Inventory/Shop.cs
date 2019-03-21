using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShopItem
{
    public InventoryObject sItem;
    public int sPrice;
}

public class Shop : InteractEvent
{

    [SerializeField]
    public List<ShopItem> shop;
    public GameObject shopUI;

    public void BuyItem(ShopItem _item)
    {
        if (Inventory.Instance.gold >= _item.sPrice)
        {
            Inventory.Instance.AddToInventory(_item.sItem);
            Inventory.Instance.gold -= (_item.sPrice);
        }
        else
        {
            Debug.Log("Too expensive!");
        }
    }

    public override void Interact(int argID)
    {
        shopUI.SetActive(true);
    }
}
