using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachScript : GenericInteract {

    [SerializeField]
    InventoryObject peachObject;
    [SerializeField]
    Quest peachQuest;


    public override void OnInteract(PlayerManager player)
    {
        if (peachQuest.isActive)
        {
            Inventory.Instance.AddToInventory(peachObject);
            gameObject.SetActive(false);
        }
    }
}
