using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class EquipEntity : MonoBehaviour
{
    PlayerEntity m_player;
    public List<InventoryObject> inventory;
    
   public Armour[] m_armourItem; //For equipping
  
    public Weapon[] m_weaponItem; //For equipping
    [SerializeField]
    Health_Potion m_health_Potion;
    [SerializeField]
    private List<Image> m_inventorySlots = new List<Image>();// Try to remove and use just the inventory object list
    private int m_id = 0;
    protected bool equipped = false;
    private bool m_addedItem = false;
    private bool m_removeHealthPotion = false;
    private Color m_itemFadeColour;
    // Use this for initialization

    void Start()
    {
        m_player = GameObject.Find("Player").GetComponent<PlayerEntity>();
        m_itemFadeColour.a = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ClearItem(int _id)
    {
        if (m_inventorySlots[_id].name != "EmptyInventorySlot")
        {
          
        }
    
    }
   public Armour[] GetArmourItem()
    {
       
        return m_armourItem;
    }
    public Weapon[] GetWeaponItem()
    {

        return m_weaponItem;
    }

    public void SetStats(int _id)
    {
        if (m_inventorySlots[_id].name != "EmptyInventorySlot")
        {
            if (m_inventorySlots[_id].gameObject.name == "Chainmail")
            {
                m_player.SetDefence(m_armourItem[0].defence);
                print("Set defence stat ");
                print("Image is" + this.gameObject.name);
                equipped = true;
            }
            if (m_inventorySlots[_id].gameObject.name == "Breastplate")
            {
                m_player.SetDefence(m_armourItem[1].defence);
                print("Set defence stat ");
                print("Image is" + this.gameObject.name);
                equipped = true;
            }

            if (m_inventorySlots[_id].gameObject.name == "Shortsword")
            {
                m_player.SetStrength(m_weaponItem[0].attack);
                print("Set attack stat ");
                print("Image is" + this.gameObject.name);
                equipped = true;
            }
            inventory.RemoveAt(0);
            m_itemFadeColour.a = 0.5f;
            m_itemFadeColour.r = 1.0f;
            m_itemFadeColour.g = 1.0f;
            m_itemFadeColour.b = 1.0f;
            m_inventorySlots[_id].sprite = null;
            m_inventorySlots[_id].name = "EmptyInventorySlot";
            m_inventorySlots[_id].color = m_itemFadeColour;
        }

    }
    public void AddItemToInventory(InventoryObject _object)
    {
        m_id++;
        for (int i = 0; i < m_inventorySlots.Count; ++i)
        {
            if (m_inventorySlots[i].name == "EmptyInventorySlot" && m_addedItem == false)
            {
                inventory.Add(_object);
                m_itemFadeColour.a = 1.0f;
                m_itemFadeColour.r = 1.0f;
                m_itemFadeColour.g = 1.0f;
                m_itemFadeColour.b = 1.0f;
                m_addedItem = true;
                print("Added " + inventory[i]);
                m_inventorySlots[i].sprite = _object.Image;
                m_inventorySlots[i].color = m_itemFadeColour;
                m_inventorySlots[i].name = _object.objectName;

            }
        }
        m_addedItem = false;
    }
    public void SetRemoveHpPotionState(bool _remove)
    {
        m_removeHealthPotion = _remove;
    }
}
