using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop : InteractEvent {

    [SerializeField]
    public List<ShopItem> shopInventory;

    //For inspector use only.
    [SerializeField]
    private InventoryObject o_item;
    [SerializeField]
    private int o_price;

    public void BuyItem(ShopItem _item)
    {
        if(Inventory.Instance.gold >= _item.Price)
        {
            Inventory.Instance.AddToInventory(_item.Item);
            Inventory.Instance.gold -= (_item.Price);
        } else
        {
            Debug.Log("Too expensive!");
        }
    }

    public void AddItem(InventoryObject _item, int _price)
    {
        ShopItem item = new ShopItem();
        item.Item = _item;
        item.Price = _price;
        shopInventory.Add(item);
    }
}
