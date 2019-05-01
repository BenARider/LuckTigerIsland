using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : GenericInteract
{
    [SerializeField]
    private InventoryObject m_iobject;

    [SerializeField]
    private int m_amount;

    public override void OnInteract(PlayerManager player)
    {
        if (m_amount > 0)
        {
            Inventory.Instance.AddToInventory(m_iobject, m_amount);
            m_amount = 0;
        }
    }
}
