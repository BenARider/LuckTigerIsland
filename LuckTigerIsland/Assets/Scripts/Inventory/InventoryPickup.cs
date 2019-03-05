using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour {

	public InventoryObject iobject;

    public void PickupObject() {
        Inventory.Instance.AddToInventory(iobject);
        EventManager.Instance.ItemToInventory(iobject);
    }
}
