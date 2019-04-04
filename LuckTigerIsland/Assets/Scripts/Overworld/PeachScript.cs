using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachScript : GenericInteract {

    [SerializeField]
    InventoryObject peachObject;

    public override void OnInteract(PlayerManager player)
    {
        Inventory.Instance.AddToInventory(peachObject);
        gameObject.SetActive(false);
    }
}
