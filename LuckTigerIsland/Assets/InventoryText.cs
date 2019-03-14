using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class InventoryText : MonoBehaviour, ISelectHandler
{
    private TextMeshProUGUI itemTitle;
    private TextMeshProUGUI itemDescription;
    [SerializeField]
    Armour[] m_armourItem; //For equipping
    [SerializeField]
    Weapon m_weaponItem; //For equipping
    // Use this for initialization
    void Start () {
        itemTitle = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnSelect(BaseEventData _eventData)
    {
        if (this.gameObject.name == "Chainmail")
        {
            itemDescription.text = m_armourItem[0].Description;
            itemTitle.text = m_armourItem[0].objectName;
        }
        if(this.gameObject.name == "Breastplate")
        {
            itemDescription.text = m_armourItem[1].Description;
            itemTitle.text = m_armourItem[1].objectName;
        }
        if (this.gameObject.name == "Shortsword")
        {
            itemTitle.text = m_weaponItem.objectName;
            itemDescription.text = m_weaponItem.Description;
        }

    }

    public void OnDeselect(BaseEventData _eventData)
    {
        itemTitle.text = "";
        itemDescription.text = "";
    }
}
