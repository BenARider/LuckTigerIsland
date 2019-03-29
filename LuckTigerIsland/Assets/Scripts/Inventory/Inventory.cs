using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class InventoryObjectStruct
{
    [SerializeField]
    public InventoryObject iObject;
    [SerializeField]
    public int amount;

    public void IncreaseAmount(int _amount)
    {
        amount += _amount;
    }
    public void DecreaseAmount(int _amount)
    {
        amount -= _amount;
        if(amount <= 0)
        {
            int index = Inventory.Instance.inventory.FindIndex(x => x.iObject == iObject);
            Inventory.Instance.inventory.RemoveAt(index);
        }
    }
}

public class Inventory : LTI.Singleton<Inventory>
{
    [SerializeField]
    private int m_gold;

    private int m_maxInventorySize = 20;

    [SerializeField]
    public List<InventoryObjectStruct> inventory;

    public void Start()
    {
        instance = this;
    }

    //Checks for an instance of the struct in the inventory. If one exists, increase its amount by the amount of item picked up. If it dosent exists, create it and give it an amount.
    public void AddToInventory(InventoryObject _object, int _amount = 1)
    {

        InventoryObjectStruct iobjstruct = new InventoryObjectStruct
        {
            iObject = _object,
            amount = _amount
        };

        bool contains = inventory.Exists(x => x.iObject == _object);

        //If the item struct already exists, increase the amount instead of making a new one.
        if (contains)
        {
            inventory[inventory.FindIndex(x => x.iObject == _object)].IncreaseAmount(_amount);
        }
        else //Else, add it to the list.
        {
            inventory.Add(iobjstruct);
        }
    }

    public void RemoveFromInventory()
    {
        inventory.RemoveAt(0);
    }

    public int GetGold()
    {
        return m_gold;
    }

    public void ReduceGold(int _amount)
    {
        m_gold -= _amount;
    }
    public void IncreaseGold(int _amount)
    {
        m_gold += _amount;
    }
    
    public int GetMaxInvSize()
    {
        return m_maxInventorySize;
    }
}
