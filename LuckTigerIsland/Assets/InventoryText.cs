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
    EquipEntity m_equipEntity;


    // Use this for initialization
    void Start()
    {
        itemTitle = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        itemStats = GameObject.Find("ItemStats").GetComponent<TextMeshProUGUI>();
        m_equipEntity = GameObject.Find("InventoryScreen").GetComponent<EquipEntity>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSelect(BaseEventData _eventData)
    {

        itemTitle.text = this.gameObject.name;
        if (this.gameObject.name == "Chainmail")
        {
            itemDescription.text = m_equipEntity.m_armourItem[0].Description;

            itemStats.text = "Defence: " + m_equipEntity.m_armourItem[0].defence;
        }
        if (this.gameObject.name == "Breastplate")
        {

            itemDescription.text = m_equipEntity.m_armourItem[1].Description;
            itemStats.text = "Defence: " + m_equipEntity.m_armourItem[1].defence;
        }
        if (this.gameObject.name == "Shortsword")
        {
            itemDescription.text = m_equipEntity.m_weaponItem[0].Description;


            itemStats.text = "Attack: " + m_equipEntity.m_weaponItem[0].attack;
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
