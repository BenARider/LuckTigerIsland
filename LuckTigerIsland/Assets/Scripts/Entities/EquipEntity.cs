using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class EquipEntity : MonoBehaviour
{
   
    public PlayerEntity[] m_partyMembers;

    [SerializeField]
    Weapon m_currentWeapon;
    [SerializeField]
    Armour m_currentArmour;
    InventoryListButton m_inventoryListButton;
    protected bool equipped = false;
    private Color m_itemFadeColour;
    [SerializeField]
    private Image[] m_equipImages;


    Inventory m_inventory;

    private TextMeshProUGUI m_partyMemberName;
    private int m_partyImageIndex;
    private int[] tempDefence;
    private int[] tempAttack;
    private bool m_unEquip;
    [SerializeField]
    TextMeshProUGUI m_justEquippedText;

    // Use this for initialization

    void Start()
    {

        m_partyMembers = new PlayerEntity[4];
        m_partyMembers[0] = GameObject.Find("Player").GetComponent<PlayerEntity>();
        m_partyMembers[1] = GameObject.Find("Player (1)").GetComponent<PlayerEntity>();
        m_partyMembers[2] = GameObject.Find("Player (2)").GetComponent<PlayerEntity>();
        m_partyMembers[3] = GameObject.Find("Player (3)").GetComponent<PlayerEntity>();
        
        m_partyMemberName = GameObject.Find("PartyMemberName").GetComponent<TextMeshProUGUI>();
        m_partyMemberName.text = "Luck";
        m_itemFadeColour.a = 0.0f;
        tempDefence = new int[4];
        tempAttack = new int[4];
        tempDefence[0] = m_partyMembers[0].GetDefence();
        tempAttack[0] = m_partyMembers[0].GetStrength();
        tempDefence[1] = m_partyMembers[1].GetDefence();
        tempAttack[1] = m_partyMembers[1].GetStrength();
        tempDefence[2] = m_partyMembers[2].GetDefence();
        tempAttack[2] = m_partyMembers[2].GetStrength();
        tempDefence[3] = m_partyMembers[3].GetDefence();
        tempAttack[3] = m_partyMembers[3].GetStrength();
    }
    // Update is called once per frame
    void Update()
    {
        
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
    public void UnEquip(string _slotName)
    {
        if (_slotName == "Armour")
        {
           // m_inventory.AddToInventory(m_currentArmour);          
            
            m_currentArmour = null;
            m_equipImages[0].sprite = m_equipImages[2].sprite;
            if (m_partyImageIndex == 0)
            {
                m_partyMembers[0].SetDefence(tempDefence[0]);
            }
            if (m_partyImageIndex == 1)
            {
                m_partyMembers[1].SetDefence(tempDefence[1]);
            }
            if (m_partyImageIndex == 2)
            {
                m_partyMembers[2].SetDefence(tempDefence[2]);
            }
            if (m_partyImageIndex == 3)
            {
                m_partyMembers[3].SetDefence(tempDefence[3]);
            }

        }
        if (_slotName == "Weapon")
        {
          //  m_inventory.AddToInventory(m_currentWeapon);
            m_currentWeapon = null;
            m_equipImages[1].sprite = m_equipImages[3].sprite;
            if (m_partyImageIndex == 0)
            {
                m_partyMembers[0].SetStrength(tempAttack[0]);
            }
            if (m_partyImageIndex == 1)
            {
                m_partyMembers[1].SetStrength(tempAttack[1]);
            }
            if (m_partyImageIndex == 2)
            {
                m_partyMembers[2].SetStrength(tempAttack[2]);
            }
            if (m_partyImageIndex == 3)
            {
                m_partyMembers[3].SetStrength(tempAttack[3]);
            }

        }
    }
    public void EquipWeapon(Weapon _object)
    {
        if (m_equipImages[1].sprite.name == "equipment_preview_10")
        {
            m_equipImages[1].sprite = _object.Image;

                m_currentWeapon = _object;
              
                if (m_partyImageIndex == 0)
                {
                    m_partyMembers[0].SetStrength(_object.attack);

                }
                if (m_partyImageIndex == 1)
                {
                    m_partyMembers[1].SetStrength(_object.attack);

                }
                if (m_partyImageIndex == 2)
                {
                    m_partyMembers[2].SetStrength(_object.attack);
    
                }
                if (m_partyImageIndex == 3)
                {
                    m_partyMembers[3].SetStrength(_object.attack);
 
                }
                m_justEquippedText.text = "Just equipped: " + _object.objectName;
                StartCoroutine(HideText());
            
        }
    }
    public void EquipArmour(Armour _object)
    {
        if (m_equipImages[0].sprite.name == "equipment_preview_1")
        {
            m_equipImages[0].sprite = _object.Image;
          
                m_currentArmour = _object;
              
                if (m_partyImageIndex == 0)
                {
                    m_partyMembers[0].SetDefence(_object.defence);
               
                }
                if (m_partyImageIndex == 1)
                {
                    m_partyMembers[1].SetDefence(_object.defence);
          
                }
                if (m_partyImageIndex == 2)
                {
                    m_partyMembers[2].SetDefence(_object.defence);
             
                }
                if (m_partyImageIndex == 3)
                {
                    m_partyMembers[3].SetDefence(_object.defence);
            
                }
                m_justEquippedText.text = "Just equipped: " + _object.objectName;
                StartCoroutine(HideText());

            
        }
    }
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(2.0f);
        m_justEquippedText.text = "";
    }
    public void SetPlayerImageId(int _index)
    {
        m_partyImageIndex += _index;
    }
}
