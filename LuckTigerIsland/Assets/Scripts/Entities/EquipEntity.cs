using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class EquipEntity : MonoBehaviour, ISelectHandler
{
    PlayerEntity m_player;
    public List<InventoryObject> inventory;
    [SerializeField]
    Armour m_armourItem; //For equipping
    [SerializeField]
    Weapon m_weaponItem; //For equipping
    [SerializeField]
    private List<Image> m_inventorySlots = new List<Image>();// Try to remove and use just the inventory object list
    private int m_id = -1;
    protected bool equipped = false;
    private TextMeshProUGUI itemTitle;
    private TextMeshProUGUI itemDescription;
    private Color m_itemFadeColour;
    // Use this for initialization
    void Start()
    { 
     
        itemTitle = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        m_player = GameObject.Find("Luck").GetComponent<PlayerEntity>();
        m_itemFadeColour.a = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnSelect(BaseEventData _eventData)
    {
        
        if (this.gameObject.name == "Chainmail")
        {
            itemDescription.text = m_armourItem.Description;
            itemTitle.text = m_armourItem.objectName;
            //  itemDescription.text = _object.Description;
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
    public void SetStats()
    {
        if (itemTitle.text == "Chainmail")
        {
            m_player.SetDefence(m_armourItem.defence);
            print("Set defence stat ");
            print("Image is" + this.gameObject.name);
        
        }
        if (itemTitle.text == "Shortsword")
        {
            m_player.SetStrength(m_weaponItem.attack);
            print("Set attack stat ");
            print("Image is" + this.gameObject.name);
        }
    }
    public void ClearItem()
    {
        for(int i =0; i< inventory.Count; ++i)
        {
            m_itemFadeColour.a = 0.0f;
            m_itemFadeColour.r = 0.0f;
            m_itemFadeColour.g = 0.0f;
            m_itemFadeColour.b = 0.0f;
            inventory.RemoveAt(i);
            m_inventorySlots[i].sprite = null;
            m_inventorySlots[i].name = "EmptyInventorySlot";
            m_inventorySlots[i].color = m_itemFadeColour;


        }
     
       
    }
    public void AddItemToInventory(InventoryObject _object)
    {
        m_id++;
     
            if (m_inventorySlots[m_id].sprite == null)
            {
                inventory.Add(_object);
                m_itemFadeColour.a = 1.0f;
                m_itemFadeColour.r = 1.0f;
                m_itemFadeColour.g = 1.0f;
                m_itemFadeColour.b = 1.0f;

                print("Added " + inventory[m_id]);
                m_inventorySlots[m_id].sprite = _object.Image;
                m_inventorySlots[m_id].color = m_itemFadeColour;
                m_inventorySlots[m_id].name = _object.objectName;

            }
 
    }
}
