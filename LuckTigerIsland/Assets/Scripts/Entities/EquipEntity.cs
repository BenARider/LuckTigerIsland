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
    [SerializeField]
    Health_Potion m_health_Potion;

    [SerializeField]
    InventoryObject m_armour;
    [SerializeField]
    InventoryObject m_weapon;
    protected bool equipped = false;
    private bool m_addedItem = false;
    private bool m_removeHealthPotion = false;
    private Color m_itemFadeColour;
    [SerializeField]
    private Image[] m_equipImages;
    private TextMeshProUGUI m_partyMemberName;
    private int m_partyImageIndex;
    // Use this for initialization

    void Start()
    {
        m_player = GameObject.Find("Luck").GetComponent<PlayerEntity>();
        m_partyMemberName = GameObject.Find("PartyMemberName").GetComponent<TextMeshProUGUI>();
        m_itemFadeColour.a = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true)
        {
            m_equipImages[0].sprite = m_armour.Image;
            m_equipImages[1].sprite = m_weapon.Image;
            if (m_partyImageIndex == 0)
            {

                m_partyMemberName.text = "Luck";
            }
            if (m_partyImageIndex == 1)
            {

                m_partyMemberName.text = "Duck";
            }
            if (m_partyImageIndex == 2)
            {

                m_partyMemberName.text = "Buck";
            }
            if (m_partyImageIndex == 3)
            {
                m_partyMemberName.text = "Phil";
            }
            if (m_partyImageIndex == 4)
            {
                m_partyImageIndex = 0;
            }
        }

    }
    public void UnEquip(string _slot)
    {
        if(_slot == "Armour")
        {
            m_armour = null;
            m_equipImages[0].sprite = null;
        }
        if(_slot == "Weapon")
        {
            m_weapon = null;
            m_equipImages[1].sprite = null;
        }
     


    }
    public void Equip(InventoryObject _object)
    {
        if(this.gameObject.activeSelf == true)
        {
            for (int i = 0; i < Inventory.Instance.inventory.Count; ++i)
            {
                if (_object.Equippable == "Yes")
                {
                    if (m_equipImages[0].sprite == null)
                    {
                        if (_object.ItemType == "Armour")
                        {
                            m_armour = _object;
                        }
                    }
                    if (m_equipImages[1].sprite == null)
                    {
                        if (_object.ItemType == "Weapon")
                        {
                            m_weapon = _object;

                        }
                    }
                }
                   
            }
        }
        
    }
    public void SetPlayerImageId(int _index)
    {
        m_partyImageIndex += _index;
    }

    public void SetRemoveHpPotionState(bool _remove)
    {
        m_removeHealthPotion = _remove;
    }
}
