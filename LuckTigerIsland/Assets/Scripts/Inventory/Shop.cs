﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

public class Shop : InteractEvent
{

    [SerializeField]
    public List<ShopItem> shop;
    public GameObject shopUI;
    public GameObject closeObject;
    [SerializeField]
    private float m_buyMod = 1.1f;
    [SerializeField]
    private float m_sellMod = 0.9f;
    EventSystem m_eventSystem;
    private void Start()
    {
        m_eventSystem = EventSystem.current;
        foreach (ShopItem _si in shop)
        {
            _si.ApplyPriceMod(m_buyMod);
        }

    }

    public void BuyItem(ShopItem _item)
    {

        if (Inventory.Instance.GetGold() >= _item.sItem.Price)
        {
            Inventory.Instance.AddToInventory(_item.sItem);
            Inventory.Instance.ReduceGold(_item.sItem.Price);
        }
        else
        {
            Debug.Log("Too expensive!");
        }
    }

    public void SellItem(InventoryObject _object)
    {
        if (Inventory.Instance.inventory.Find(x => x.iObject == _object).amount > 0)
        {
            Inventory.Instance.IncreaseGold(Mathf.CeilToInt(_object.Price * m_sellMod));
            Inventory.Instance.inventory.Find(x => x.iObject == _object).DecreaseAmount(1);
        } else
        {
            Debug.Log("Not enough left to sell!");
        }
    }

    public override void Interact(int argID)
    {
        shopUI.SetActive(true);
        m_eventSystem.SetSelectedGameObject(closeObject);
        //PlayerManager.Instance.inDialogue = true;
        PlayerManager.Instance.inShop = shopUI;
    }
}
