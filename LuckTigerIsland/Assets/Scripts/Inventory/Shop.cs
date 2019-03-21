using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShopItem
{
    public InventoryObject sItem;
    [HideInInspector]
    public int sPrice;

    public void ApplyPriceMod(float _mod)
    {
        sPrice = Mathf.CeilToInt(sItem.Price * _mod);
    }
}

public class Shop : MonoBehaviour
{

    [SerializeField]
    public List<ShopItem> shop;

    [SerializeField]
    private float m_buyMod = 1.1f;
    [SerializeField]
    private float m_sellMod = 0.9f;

    private void Start()
    {
        foreach(ShopItem _si in shop)
        {
            _si.ApplyPriceMod(m_buyMod);
        }
    }

    public void BuyItem(ShopItem _item)
    {
        if (Inventory.Instance.GetGold() >= _item.sPrice)
        {
            Inventory.Instance.AddToInventory(_item.sItem);
            Inventory.Instance.ReduceGold(_item.sPrice);
        }
        else
        {
            Debug.Log("Too expensive!");
        }
    }

    public void SellItem(ref InventoryObject _object)
    {
        Inventory.Instance.IncreaseGold(Mathf.CeilToInt(_object.Price * m_sellMod));
       // Inventory.Instance.

    }
}
