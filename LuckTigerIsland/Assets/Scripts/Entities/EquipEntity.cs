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
    private Image[] m_inventorySlots;
    private int m_id = -1;
    protected bool equipped = false;
    public TextMeshProUGUI itemTitle;
    public TextMeshProUGUI itemDescription;
    private Color m_itemFadeColour;

    // Use this for initialization
    void Start()
    {
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
            itemTitle.text = m_armourItem.objectName;
            itemDescription.text = m_armourItem.armourDescription;
        }
        if (this.gameObject.name == "Shortsword")
        {
            itemTitle.text = m_weaponItem.objectName;
            itemDescription.text = m_weaponItem.weaponDescription;
        }
    }
    public void OnDeselect(BaseEventData _eventData)
    {
        itemTitle.text = "";
        itemDescription.text = "";
    }
    public void SetStats()
    {

        if (this.gameObject.name == "Chainmail")
        {
            m_player.SetDefence(m_armourItem.defence);
            print("Set defence stat ");
        }
        if (this.gameObject.name == "Shortsword")
        {
            m_player.SetStrength(m_weaponItem.attack);
            print("Set attack stat ");
        }
    }
    public void AddArmourToInventory(Armour _armour)
    {
        m_id++;
        if (m_inventorySlots[m_id].sprite == null)
        {
            m_itemFadeColour.a = 1.0f;
            m_itemFadeColour.r = 1.0f;
            m_itemFadeColour.g = 1.0f;
            m_itemFadeColour.b = 1.0f;
            inventory[m_id] = _armour;
            print("Added " + inventory[m_id]);
            m_inventorySlots[m_id].sprite = _armour.armourImage;
            m_inventorySlots[m_id].color = m_itemFadeColour;
            m_inventorySlots[m_id].name = _armour.objectName;
            print("Image is" + m_inventorySlots[m_id].sprite);
        }
    }
    public void AddWeaponToInventory(Weapon _weapon)
    {
        m_id++;
        if (m_inventorySlots[m_id].sprite == null)
        {
            m_itemFadeColour.a = 1.0f;
            m_itemFadeColour.r = 1.0f;
            m_itemFadeColour.g = 1.0f;
            m_itemFadeColour.b = 1.0f;
            inventory[m_id] = _weapon;
            print("Added " + inventory[m_id]);
            m_inventorySlots[m_id].sprite = _weapon.weaponImage;
            m_inventorySlots[m_id].color = m_itemFadeColour;
            m_inventorySlots[m_id].name = _weapon.objectName;
            print("Image is" + m_inventorySlots[m_id].sprite);
        }
    }
}
