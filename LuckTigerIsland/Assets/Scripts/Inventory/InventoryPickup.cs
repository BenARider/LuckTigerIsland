using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour
{

    private InventoryObject m_iobject;

    private int m_amount;

    public void PickupObject()
    {
        Inventory.Instance.AddToInventory(m_iobject);
        EventManager.Instance.ItemToInventory(m_iobject, m_amount);
    }
}
