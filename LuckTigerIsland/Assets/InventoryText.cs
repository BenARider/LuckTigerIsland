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
    private TextMeshProUGUI itemStats;
    [SerializeField]
    Armour[] m_armour;
    [SerializeField]
    Weapon[] m_weapon;

    // Use this for initialization
    void Start()
    {
        itemTitle = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        itemStats = GameObject.Find("ItemStats").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSelect(BaseEventData _eventData)
    {
        if (this.gameObject.name == "Chainmail")
        {

            itemDescription.text = m_armour[0].Description;
            itemTitle.text = m_armour[0].objectName;
            itemStats.text = "Defence: " + m_armour[0].defence;

        }
        if (this.gameObject.name == "Breastplate")
        {

            itemDescription.text = m_armour[1].Description;
            itemTitle.text = m_armour[1].objectName;
            itemStats.text = "Defence: " + m_armour[1].defence;
        }
        if (this.gameObject.name == "Shortsword")
        {
            itemTitle.text = m_weapon[0].objectName;
            itemDescription.text = m_weapon[0].Description;
            itemStats.text = "Attack: " + m_weapon[0].attack;
        }
        if(this.gameObject.name == "EmptyInventorySlot")
        {
            itemTitle.text = "";
            itemDescription.text = "";
            itemStats.text = "";
        }


    }

    public void OnDeselect(BaseEventData _eventData)
    {
        itemTitle.text = "";
        itemDescription.text = "";
        itemStats.text = "";
    }
}
