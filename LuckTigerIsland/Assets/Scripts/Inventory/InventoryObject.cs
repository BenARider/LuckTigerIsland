using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryObject : ScriptableObject {

    [SerializeField]
    public string objectName = "New Object";
    [SerializeField]
    public string objectDescription = "Description";
}

